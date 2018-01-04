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
    public class RoleController : BaseController
    {
        Sys_RoleBLL bll;
        public RoleController(Sys_RoleBLL bll)
        {
            this.bll = bll;
        }
        [AdminPermission(PermissionEnum.Enforce)]
        public IActionResult Index()
        {
            return View();
        }
        //分页
        public JsonResult PageData(int PageIndex, int PageSize, sys_role model)
        {
            var page = bll.RolePage(PageIndex, PageSize, model);
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
        //新增修改
        public IActionResult Create(string KeyId)
        {
            sys_role model = null;
            if (!string.IsNullOrEmpty(KeyId))
            {
                model = bll.GetModelById<sys_role>(KeyId);
            }
            model = model ?? new sys_role();

            return View(model);
        }
        //新增修改
        [HttpPost]
        public JsonResult Create(sys_role model)
        {
            ExecuteResult Er = new ExecuteResult();
            if (string.IsNullOrEmpty(model.KeyId))
            {
                int count = bll.GetList<sys_role>(item => item.FullName == model.FullName && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "已存在相同角色命名";
                }
                else
                {
                    #region 新增
                    model.KeyId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.IsDeleted = false;
                    Er.Result = bll.Insert<sys_role>(model) > 0;
                    Er.Message = Er.Result ? "新增成功" : "新增失败";
                    #endregion
                }
            }
            else
            {
                int count = bll.GetList<sys_role>(item => item.FullName == model.FullName && item.KeyId != model.KeyId && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "已存在相同角色命名";
                }
                else
                {
                    #region 修改
                    var old = bll.GetModelById<sys_role>(model.KeyId);
                    old.FullName = model.FullName;
                    old.Description = model.Description;
                    Er.Result = bll.Update<sys_role>(old) > 0;
                    Er.Message = Er.Result ? "编辑成功" : "编辑失败";
                    #endregion
                }
            }
            return Json(Er);
        }
        //删除角色
        public JsonResult Delete(string KeyId)
        {
            var old = bll.GetModelById<sys_role>(KeyId);
            old.IsDeleted = true;
            var Result = bll.Update<sys_role>(old) > 0;
            return Json(new { Result = Result });
        }
        //分配权限视图
        public IActionResult Permissions(string KeyId)
        {
            var list = bll.GetList<sys_menu>(item => item.IsDeleted == false).ToList();
            var list2 = bll.GetList<sys_menubutton>(item => item.KeyId != null).ToList();
            var list3 = bll.GetList<sys_button>(item => item.IsDeleted == false).ToList();
            var tuple = Tuple.Create(list, list2, list3);
            ViewBag.KeyId = KeyId;
            return View(tuple);
        }

        //保存角色权限
        [HttpPost]
        public JsonResult Save(string MenuIds, string ButtonIds, string RoleId)
        {
            bool Result = bll.SaveRole(MenuIds, ButtonIds, RoleId);
            return Json(new { Result = Result });
        }
        //获取角色权限
        public IActionResult GetPermissions(string RoleId)
        {
            var RoleMenuButtonList = bll.GetList<sys_rolemenubutton>(item => item.RoleId == RoleId).ToList();
            var RoleMenuList = bll.GetList<sys_rolemenu>(item => item.RoleId == RoleId).ToList();
            List<string> list = new List<string>();
            foreach (var item in RoleMenuList)
            {
                list.Add(item.MenuId);
            }
            foreach (var item in RoleMenuButtonList)
            {
                list.Add(item.ButtonId);
            }
            return Json(new { RoleMenuList = RoleMenuList, RoleMenuButtonList = RoleMenuButtonList });
        }

    }
}