using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFrameCore.BLL;
using MyFrameCore.Model;
using MyFrameCore.Common;
using Microsoft.AspNetCore.Mvc;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class OrganizationController : BaseController
    {
        Sys_DictionaryBLL bll;
        public OrganizationController(Sys_DictionaryBLL bll)
        {
            this.bll = bll;
        }
        [AdminPermission(PermissionEnum.Enforce)]
        public IActionResult Index()
        {
            return View();
        }
        //新增修改
        public IActionResult Create(string KeyId, string PId, string PName)
        {
            sys_organization model = null;
            if (!string.IsNullOrEmpty(KeyId))
            {
                model = bll.GetModelById<sys_organization>(KeyId);
            }
            model = model ?? new sys_organization();
            ViewBag.PId = PId;
            ViewBag.PName = PName;
            return View(model);
        }
        //新增修改
        [HttpPost]
        public JsonResult Create(sys_organization model)
        {
            ExecuteResult er = new ExecuteResult();
            string pids = "";
            GetPIds(ref pids, model.ParentId);
            if (string.IsNullOrEmpty(model.KeyID))
            {
                int count = bll.GetList<sys_organization>(item => item.ParentId == model.ParentId && item.FullName == model.FullName && item.IsDeleted == false).Count;
                if (count > 0)
                {
                    er.Result = false;
                    er.Message = "同级别已存在相同项";
                }
                else
                {
                    #region 新增
                    model.KeyID = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.IsDeleted = false;
                    model.PIds = pids;
                    er.Result = bll.Insert<sys_organization>(model) > 0;
                    er.Message = er.Result ? "新增成功" : "新增失败";
                    #endregion
                }
            }
            else
            {
                int count = bll.GetList<sys_organization>(item => item.ParentId == model.ParentId && item.FullName == model.FullName && item.KeyID != model.KeyID && item.IsDeleted == false).Count;
                if (count > 0)
                {
                    er.Result = false;
                    er.Message = "同级别已存在相同项";
                }
                else
                {
                    #region 修改
                    var old = bll.GetModelById<sys_organization>(model.KeyID);
                    model.IsDeleted = old.IsDeleted;
                    model.CreateDate = old.CreateDate;
                    model.PIds = pids;
                    er.Result = bll.Update<sys_organization>(model) > 0;
                    er.Message = er.Result ? "编辑成功" : "编辑失败";
                    #endregion
                }
            }
            return Json(er);
        }

        //删除组织
        public JsonResult Delete(string KeyId)
        {
            var old = bll.GetModelById<sys_organization>(KeyId);
            old.IsDeleted = true;
            var Result = bll.Update<sys_organization>(old) > 0;
            return Json(new { Result = Result });
        }
        /// <summary>
        /// 获取父级ID集合
        /// </summary>
        /// <param name="pids">父ID集合</param>
        /// <param name="pid">当前父ID</param>
        private void GetPIds(ref string pids, string pid)
        {
            if (!string.IsNullOrEmpty(pid))
            {
                pids = string.Format("{0}|{1}", pids, pid);
                var model = bll.GetModelById<sys_organization>(pid);
                if (model != null)
                {
                    GetPIds(ref pids, model.ParentId);
                }
            }
        }
        //获取组织数据
        public JsonResult GetOrganization()
        {
            var list = bll.GetList<sys_organization>(item => item.IsDeleted == false);
            list = list.OrderBy(item => item.CreateDate).ToList();
            List<ZTree> zlist = new List<ZTree>();
            GetZtreeList(list, ref zlist, null);
            return Json(zlist);
        }
        /// <summary>
        /// 递归获取ZTree数据
        /// </summary>
        /// <param name="list">原数据集合</param>
        /// <param name="zlist">树形数据集合</param>
        /// <param name="Pid">父级ID</param>
        private void GetZtreeList(List<sys_organization> list, ref List<ZTree> zlist, string Pid)
        {
            var templist = list.Where(item => item.ParentId == Pid).OrderBy(item => item.SortNum).ToList();
            foreach (var item in templist)
            {
                zlist.Add(new ZTree
                {
                    id = item.KeyID,
                    name = item.FullName,
                    pId = item.ParentId,
                    open = true
                });
                GetZtreeList(list, ref zlist, item.KeyID);
            }
        }
    }
}