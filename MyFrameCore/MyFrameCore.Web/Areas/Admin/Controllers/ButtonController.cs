using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class ButtonController : BaseController
    {
        Sys_ButtonBLL bll;
        public ButtonController(Sys_ButtonBLL bll)
        {
            this.bll = bll;
        }
        [AdminPermission(PermissionEnum.Enforce)]
        public IActionResult Index()
        {
            return View();
        }
        //分页
        public JsonResult PageData(int PageIndex, int PageSize, sys_button model)
        {
            var page = bll.ButtonPage(PageIndex, PageSize, model);
            return Json(page);
        }
        //新增编辑
        public IActionResult Create(string KeyId)
        {
            sys_button model = null;
            if (!string.IsNullOrEmpty(KeyId))
            {
                model = bll.GetModelById<sys_button>(KeyId);
            }
            model = model ?? new sys_button();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(sys_button model)
        {
            ExecuteResult Er = new ExecuteResult();
            if (string.IsNullOrEmpty(model.KeyId))
            {
                int count = bll.GetList<sys_button>(item => item.FullName == model.FullName && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "已存在相同的按钮名称";
                }
                else
                {
                    #region 新增
                    model.IsDeleted = false;
                    model.CreateDate = DateTime.Now;
                    model.KeyId = Guid.NewGuid().ToString();
                    Er.Result = bll.Insert<sys_button>(model) > 0;
                    Er.Message = Er.Result ? "新增成功" : "新增失败";
                    #endregion
                }

            }
            else
            {
                int count = bll.GetList<sys_button>(item => item.FullName == model.FullName && item.KeyId != model.KeyId && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "已存在相同的按钮名称";
                }
                else
                {
                    #region 修改
                    var old = bll.GetModelById<sys_button>(model.KeyId);
                    old.FullName = model.FullName;
                    old.ButtonEvent = model.ButtonEvent;
                    old.Description = model.Description;
                    old.SortNum = model.SortNum;
                    old.Icon = model.Icon;
                    Er.Result = bll.Update<sys_button>(old) > 0;
                    Er.Message = Er.Result ? "编辑成功" : "编辑失败";
                    #endregion
                }

            }
            return Json(Er);
        }

        //删除按钮
        public JsonResult Delete(string KeyId)
        {
            var old = bll.GetModelById<sys_button>(KeyId);
            old.IsDeleted = true;
            var Result = bll.Update<sys_button>(old) > 0;
            return Json(new { Result = Result });
        }
    }
}