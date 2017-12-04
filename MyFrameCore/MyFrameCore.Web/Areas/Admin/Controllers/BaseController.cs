using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFrameCore.Model;
using MyFrameCore.Model.Extend;
using Newtonsoft.Json;
using MyFrameCore.Common;
using System.Web;
using System;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        /*****************************************/


        //PS:不能再写构造函数，否则取不到HttpContext


        /*****************************************/

        /// <summary>
        /// Json字段按后台格式大小写输出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult Json(object data)
        {
            //处理循环引用问题
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //设置不处理循环引用
            return Json(data, settings);
        }

        protected UserExtend SysUser
        {
            get { return GetCurrentUser(); }
        }

        protected AppSettings AppSettings
        {
            get { return GetAppSetting(); }
        }

        private UserExtend GetCurrentUser()
        {
            string UserJson = string.Empty;
            var appsetting = GetAppSetting();
            if (appsetting != null && Convert.ToBoolean(appsetting.Redis.IsEnable))
            {
                RedisHelper rh = new RedisHelper(appsetting);
                string key = HttpContext.Request.Cookies[ConstConfig.AdminCookie];
                UserJson = rh.Get(key);
            }
            else
            {
                UserJson = HttpContext.Session.GetString(ConstConfig.AdminSession);
            }

            var user = JsonConvert.DeserializeObject<UserExtend>(UserJson);
            return user;
        }

        private AppSettings GetAppSetting()
        {
            return ConfigHelper.GetAppSetting();
        }
    }
}