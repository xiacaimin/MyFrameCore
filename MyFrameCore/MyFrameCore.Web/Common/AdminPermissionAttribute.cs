using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using MyFrameCore.Model.Extend;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Protocols;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MyFrameCore.Web
{
    /// <summary>
    /// 后台权限验证
    /// 该特性可以置于类和方法前，可以被继承，不能在同一个方法或类多次叠加使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminPermissionAttribute : Attribute, IActionFilter
    {
        private string TempAction;
        private PermissionEnum EnumModel;
        Sys_UserBLL bll;
        AppSettings appsetting { get; set; }
        //传入权限验证方式
        public AdminPermissionAttribute(PermissionEnum mode)
        {
            Init(mode);
        }



        //传入action验证权限
        public AdminPermissionAttribute(string action)
        {
            Init(PermissionEnum.Enforce);
            TempAction = action;
        }

        private void Init(PermissionEnum mode)
        {
            appsetting = ConfigHelper.GetAppSetting();
            EnumModel = mode;
            bll = new Sys_UserBLL();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限拦截是否忽略
            if (EnumModel == PermissionEnum.Ignore)
            {
                return;
            }

            //登陆者Cookie主键ID
            string KeyId = filterContext.HttpContext.Request.Cookies[ConstConfig.AdminCookie];
            var userJson = string.Empty;
            RedisHelper rh = null;
            if (appsetting != null && Convert.ToBoolean(appsetting.Redis.IsEnable))
            {
                //登陆者Redis信息
                rh = new RedisHelper(appsetting);
                userJson = rh.Get(KeyId);
            }
            else
            {
                //登陆者Session信息
                userJson = filterContext.HttpContext.Session.GetString(ConstConfig.AdminSession);
            }

            UserExtend user = null;
            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<UserExtend>(userJson);
            }

            if (user == null)
            {
                #region 验证cookie数据是否能登录
                var entity = bll.GetUserInfo(KeyId);
                if (entity != null)
                {
                    filterContext.HttpContext.Session.SetString(ConstConfig.AdminSession, JsonConvert.SerializeObject(entity));
                }
                else
                {
                    //跳转到登录页面
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Login", action = "Index" }));
                }
                #endregion

            }
            else
            {
                // 权限拦截与验证
                var area = filterContext.RouteData.Values["area"].ToString().ToLower();
                var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
                var action = filterContext.RouteData.Values["action"].ToString().ToLower();
                action = TempAction == null ? action : TempAction.ToLower();//取方法对应的权限
                var isAllowed = this.IsAllowed(user.KeyId, area, controller, action);
                //权限不允许，也不是超级管理员
                if (!isAllowed)
                {
                    if (appsetting != null && Convert.ToBoolean(appsetting.Redis.IsEnable))
                    {
                        user.WebLastTime = DateTime.Now;
                        rh.Set(user.KeyId, JsonConvert.SerializeObject(user));
                    }
                    //跳转到登录页面
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Login", action = "Index" }));
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        public bool IsAllowed(string userid, string area, string controller, string action)
        {
            StringBuilder url = new StringBuilder("/");
            if (!string.IsNullOrEmpty(area))
            {
                url.Append(area);
                url.Append("/");
            }
            url.Append(controller);
            url.Append("/");
            url.Append(action);
            if (url.ToString().ToLower().StartsWith("/admin/main"))
            {
                return true;
            }
            //该路径是否在权限范围内
            return bll.IsAllowedMenu(userid, url.ToString());
        }
    }
}