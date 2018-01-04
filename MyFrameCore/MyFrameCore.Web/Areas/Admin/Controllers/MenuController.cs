using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        Sys_MenuBLL bll;
        public MenuController(Sys_MenuBLL bll)
        {
            this.bll = bll;
        }
        [AdminPermission(PermissionEnum.Enforce)]
        public IActionResult Index()
        {
            //链接为空的菜单(一级菜单)和跟菜单都可以为父级
            ViewBag.MenuParent = bll.GetList<sys_menu>(item => (item.NavigateUrl == null || item.IsRoot == true) && item.IsDeleted == false).ToList();
            return View();
        }
        //新增编辑
        public IActionResult Create(string KeyId)
        {
            //链接为空的菜单(一级菜单)和跟菜单都可以为父级
            ViewBag.MenuParent = bll.GetList<sys_menu>(item => (item.NavigateUrl == null || item.IsRoot == true) && item.IsDeleted == false).ToList();
            sys_menu model = null;
            if (!string.IsNullOrEmpty(KeyId))
            {
                model = bll.GetModelById<sys_menu>(KeyId);
            }
            model = model ?? new sys_menu();
            return View(model);
        }
        //新增编辑
        [HttpPost]
        public JsonResult Create(sys_menu model)
        {
            ExecuteResult Er = new ExecuteResult();
            if (!string.IsNullOrEmpty(model.KeyId))
            {
                //验证是否有相同同级
                int count = bll.GetList<sys_menu>(item => item.ParentId == model.ParentId && item.FullName == model.FullName && item.KeyId != model.KeyId && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "同级已存在相同菜单命名";
                }
                else
                {
                    #region 编辑
                    var old = bll.GetModelById<sys_menu>(model.KeyId);
                    old.IsRoot = false;
                    if (string.IsNullOrEmpty(model.ParentId))
                    {
                        old.IsRoot = true;
                    }
                    old.FullName = model.FullName;
                    old.Description = model.Description;
                    old.Icon = model.Icon;
                    old.NavigateUrl = model.NavigateUrl;
                    old.ParentId = model.ParentId;
                    old.SortNum = model.SortNum;
                    Er.Result = bll.Update<sys_menu>(old) > 0;
                    Er.Message = Er.Result ? "编辑成功" : "编辑失败";
                    #endregion
                }

            }
            else
            {
                int count = bll.GetList<sys_menu>(item => item.ParentId == model.ParentId && item.FullName == model.FullName && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "同级已存在相同菜单命名";
                }
                else
                {
                    #region 新增
                    model.IsRoot = false;
                    if (string.IsNullOrEmpty(model.ParentId))
                    {
                        model.IsRoot = true;
                    }
                    model.IsDeleted = false;
                    model.CreateDate = DateTime.Now;
                    model.KeyId = Guid.NewGuid().ToString();
                    Er.Result = bll.Insert<sys_menu>(model) > 0;
                    Er.Message = Er.Result ? "新增成功" : "新增失败";
                    #endregion
                }

            }
            return Json(Er);
        }
        //删除菜单

        public JsonResult Delete(string KeyId)
        {
            var old = bll.GetModelById<sys_menu>(KeyId);
            old.IsDeleted = true;
            var Result = bll.Update<sys_menu>(old) > 0;
            return Json(new { Result = Result });
        }
        //分页
        public JsonResult PageData(int PageIndex, int PageSize, sys_menu model)
        {
            var page = bll.MenuPage(PageIndex, PageSize, model);
            if (page != null && page.rows != null)
            {
                string json = JsonConvert.SerializeObject(page.rows);
                JArray ja = (JArray)JsonConvert.DeserializeObject(json);
                for (int i = 0; i < ja.Count; i++)
                {
                    ja[i]["CreateDate"] = ja[i]["CreateDate"] == null ? "" : Convert.ToDateTime(ja[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                page.rows = ja;
            }
            return Json(page);
        }
        //按钮视图
        public IActionResult Button(string MenuId)
        {
            var list = bll.GetList<sys_button>(item => item.IsDeleted == false).OrderBy(item => item.SortNum).ToList();
            ViewBag.MenuId = MenuId;
            return View(list);
        }

        //获取菜单按钮
        public JsonResult GetButton(string MenuId)
        {
            var list = bll.GetList<sys_menubutton>(item => item.MenuId == MenuId).ToList();
            return Json(list);
        }
        //保存菜单按钮
        [HttpPost]
        public JsonResult SaveButton(string MenuId, string Ids)
        {
            bool Result = bll.SaveMenuButton(MenuId, Ids);
            return Json(new { Result = Result });
        }

    }
}