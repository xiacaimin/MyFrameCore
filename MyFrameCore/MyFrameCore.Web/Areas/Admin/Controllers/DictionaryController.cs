using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class DictionaryController : BaseController
    {
        Sys_DictionaryBLL bll;
        AppSettings appsettings;
        public DictionaryController(Sys_DictionaryBLL bll)
        {
            this.bll = bll;
            this.appsettings = ConfigHelper.GetAppSetting();
        }
        [AdminPermission(PermissionEnum.Enforce)]
        public IActionResult Index()
        {
            return View();
        }

        //新增修改
        public IActionResult Create(string KeyId, string PId, string PName)
        {
            sys_dictionary model = null;
            if (!string.IsNullOrEmpty(KeyId))
            {
                model = bll.GetModelById<sys_dictionary>(KeyId);
            }
            model = model ?? new sys_dictionary();
            ViewBag.PId = PId;
            ViewBag.PName = PName;
            return View(model);
        }
        //新增修改
        [HttpPost]
        public JsonResult Create(sys_dictionary model)
        {
            ExecuteResult er = new ExecuteResult();
            string pids = "";
            GetPIds(ref pids, model.ParentId);
            if (string.IsNullOrEmpty(model.KeyId))
            {
                int count = bll.GetList<sys_dictionary>(item => item.ParentId == model.ParentId && item.FullName == model.FullName && item.IsDeleted == false).Count;
                if (count > 0)
                {
                    er.Result = false;
                    er.Message = "同级别已存在相同项";
                }
                else
                {
                    #region 新增
                    model.KeyId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.IsDeleted = false;
                    model.PIds = pids;
                    er.Result = bll.Insert<sys_dictionary>(model) > 0;
                    er.Message = er.Result ? "新增成功" : "新增失败";
                    #endregion
                }
            }
            else
            {
                int count = bll.GetList<sys_dictionary>(item => item.ParentId == model.ParentId && item.FullName == model.FullName && item.KeyId != model.KeyId && item.IsDeleted == false).Count;
                if (count > 0)
                {
                    er.Result = false;
                    er.Message = "同级别已存在相同项";
                }
                else
                {
                    #region 修改
                    var old = bll.GetModelById<sys_dictionary>(model.KeyId);
                    model.IsDeleted = old.IsDeleted;
                    model.CreateDate = old.CreateDate;
                    model.PIds = pids;
                    er.Result = bll.Update<sys_dictionary>(model) > 0;
                    er.Message = er.Result ? "编辑成功" : "编辑失败";
                    #endregion
                }
            }
            return Json(er);
        }

        //删除字典
        public JsonResult Delete(string KeyId)
        {
            var old = bll.GetModelById<sys_dictionary>(KeyId);
            old.IsDeleted = true;
            var Result = bll.Update<sys_dictionary>(old) > 0;
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
                var model = bll.GetModelById<sys_dictionary>(pid);
                if (model != null)
                {
                    GetPIds(ref pids, model.ParentId);
                }
            }
        }
        //获取字典数据
        public JsonResult GetDictionary()
        {
            var list = bll.GetList<sys_dictionary>(item => item.IsDeleted == false);
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
        private void GetZtreeList(List<sys_dictionary> list, ref List<ZTree> zlist, string Pid)
        {
            var templist = list.Where(item => item.ParentId == Pid).OrderBy(item => item.SortNum).ToList();
            foreach (var item in templist)
            {
                zlist.Add(new ZTree
                {
                    id = item.KeyId,
                    name = item.FullName,
                    pId = item.ParentId,
                    open = true
                });
                GetZtreeList(list, ref zlist, item.KeyId);
            }
        }

        [HttpPost]
        public JsonResult UploadImg(IFormFile HeadImgFile)
        {
            string[] point = HeadImgFile.FileName.Split('.');
            string Postfix = point[point.Length - 1].ToLower();
            if (Postfix != "bmp" && Postfix != "jpg" && Postfix != "jpeg" && Postfix != "png")
            {
                return Json(new { Result = false, Path = "", Message = "文件后缀名必须是bmp,jpg,jpeg,png" });
            }
            byte[] bytes = null;
            Stream stream = HeadImgFile.OpenReadStream();
            using (var binaryReader = new BinaryReader(stream))
            {
                bytes = binaryReader.ReadBytes(Convert.ToInt32(stream.Length));
            }
            string uri = "/api/Img/UploadOneImg";
            ImgUploadModel Ium = new ImgUploadModel()
            {
                Byte = bytes,
                ChiPath = "DictionaryImg",
                FileName = Guid.NewGuid().ToString(),
                Postfix = Postfix
            };
            ExecuteResult Er = HttpClientHelper.PostResponse<ExecuteResult>(appsettings.ApiUrl + uri, Ium);
            if (Er.Result)
            {
                Er.ReturnVal = string.Format("{0}/{1}/{2}.{3}", appsettings.ImgUrl, Ium.ChiPath, Ium.FileName, Ium.Postfix);
            }
            return Json(Er);
        }
    }
}