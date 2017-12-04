using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class MainController : BaseController
    {
        Sys_MenuBLL bll;
        public MainController(Sys_MenuBLL bll)
        {
            this.bll = bll;
        }
        [AdminPermission(PermissionEnum.Enforce)]
        //首页--加载菜单权限
        public IActionResult Index()
        {
            var user = base.SysUser;
            var rootlist = bll.GetRootMenuList(user.KeyId);
            rootlist = Distinct(rootlist);
            var navlist = bll.GetNavigateUrl(user.KeyId);
            navlist = Distinct(navlist);
            var tuple = Tuple.Create(rootlist, navlist);
            ViewBag.LoginName = user.FullName;
            ViewBag.LoginImg = user.HeadImg;
            return View(tuple);
        }
        /// <summary>
        /// 去重
        /// </summary>
        private List<sys_menu> Distinct(List<sys_menu> list)
        {
            List<sys_menu> NewList = new List<sys_menu>();
            foreach (var item in list)
            {
                if (NewList.Where(x => x.KeyId == item.KeyId).Count() == 0)
                {
                    NewList.Add(item);
                }
            }
            return NewList;
        }
        //Main视图
        public IActionResult Main()
        {
            return View();
        }

        //获取根菜单
        public JsonResult TopRight()
        {
            var list = bll.GetRootMenuList(base.SysUser.KeyId);
            return Json(list);
        }

        public IActionResult Error()
        {
            return View();
        }
        //错误日志分页
        public JsonResult ErrorPage(int PageIndex, int PageSize)
        {
            var page = bll.ErrorPage(PageIndex, PageSize);
            return Json(page);
        }
    }
}