using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using System;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        Sys_UserBLL userbll;
        Sys_DictionaryBLL dicbll;
        AppSettings appsettings;
        public UserController(Sys_UserBLL userbll, Sys_DictionaryBLL dicbll)
        {
            this.userbll = userbll;
            this.dicbll = dicbll;
            this.appsettings = base.AppSettings;
        }
        [AdminPermission(PermissionEnum.Enforce)]
        public IActionResult Index()
        {
            return View();
        }
        //修改密码
        public IActionResult Password()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpdatePwd(string OldPwd, string NewPwd)
        {
            ExecuteResult Er = new ExecuteResult();
            var model = userbll.GetModelById<sys_user>(base.SysUser.KeyId);
            if (model != null && model.PassWord == MD5Provider.getStringMd5Hash(OldPwd))
            {
                model.PassWord = MD5Provider.getStringMd5Hash(NewPwd);
                Er.Result = userbll.Update<sys_user>(model) > 0;
                Er.Message = Er.Result ? "密码更新成功" : "密码更新失败";
            }
            else
            {
                Er.Result = false;
                Er.Message = "您输入的旧密码与系统原密码不匹配";
            }
            return Json(Er);
        }
        //分页
        public JsonResult PageData(int PageIndex, int PageSize, sys_user model)
        {
            var page = userbll.UserPage(PageIndex, PageSize, model);
            if (page != null && page.rows != null)
            {
                string json = JsonConvert.SerializeObject(page.rows);
                JArray ja = (JArray)JsonConvert.DeserializeObject(json);
                for (int i = 0; i < ja.Count; i++)
                {
                    ja[i]["BirthDay"]= ja[i]["BirthDay"]==null ? "" : Convert.ToDateTime(ja[i]["BirthDay"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    ja[i]["CreateDate"] = ja[i]["CreateDate"] == null ? "" : Convert.ToDateTime(ja[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                }
                page.rows = ja;
            }
            return Json(page);
        }

        public IActionResult Role(string UserId)
        {
            var list = userbll.GetList<sys_role>(item => item.IsDeleted == false).ToList();
            ViewBag.UserId = UserId;
            return View(list);
        }

        public IActionResult Create(string KeyId)
        {
            sys_user model = null;
            if (!string.IsNullOrEmpty(KeyId))
            {
                model = userbll.GetModelById<sys_user>(KeyId);
            }
            model = model ?? new sys_user();
            ViewBag.JobList = dicbll.GetConfigList(ConstConfig.Job);
            ViewBag.EducationalList = dicbll.GetConfigList(ConstConfig.Educational);
            //获取组织名称
            if (!string.IsNullOrEmpty(model.OrgId))
            {
                var Org = userbll.GetModelById<sys_organization>(model.OrgId);
                if (Org != null)
                {
                    ViewBag.OrgName = Org.FullName;
                }
            }
            return View(model);
        }
        //新增修改
        [HttpPost]
        public JsonResult Create(sys_user model)
        {
            ExecuteResult Er = new ExecuteResult();
            if (string.IsNullOrEmpty(model.KeyId))
            {
                //验证是否有重复账号
                int count = userbll.GetList<sys_user>(item => item.Account == model.Account && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "已存在相同账号";
                }
                else
                {
                    #region 新增
                    model.KeyId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.IsDeleted = false;
                    model.PassWord = MD5Provider.getStringMd5Hash(model.PassWord);
                    Er.Result = userbll.Insert<sys_user>(model) > 0;
                    Er.Message = "新增成功";
                    #endregion
                }

            }
            else
            {
                //验证是否有重复账号
                int count = userbll.GetList<sys_user>(item => item.Account == model.Account && item.KeyId != model.KeyId && item.IsDeleted == false).Count();
                if (count > 0)
                {
                    Er.Result = false;
                    Er.Message = "已存在相同账号";
                }
                else
                {
                    #region 修改
                    var old = userbll.GetModelById<sys_user>(model.KeyId);
                    model.CreateDate = old.CreateDate;
                    model.IsDeleted = old.IsDeleted;
                    model.PassWord = old.PassWord;
                    Er.Result = userbll.Update<sys_user>(model) > 0;
                    Er.Message = "更新成功";
                    #endregion
                }

            }
            return Json(Er);
        }
        //删除角色
        public JsonResult Delete(string KeyId)
        {
            ExecuteResult Er = new ExecuteResult();
            if (KeyId.Equals("48435A5D-9C92-4E66-96DF-CABD1E54E4D6"))
            {
                Er.Result = false;
                Er.Message = "为保证系统的可用性，当前用户是唯一不能删的数据";
            }
            else
            {
                var old = userbll.GetModelById<sys_user>(KeyId);
                old.IsDeleted = true;
                Er.Result = userbll.Update<sys_user>(old) > 0;
                Er.Message = Er.Result ? "删除成功" : "删除失败";
            }
            return Json(Er);
        }
        //保存用户角色
        [HttpPost]
        public JsonResult SaveUserRole(string RoleIds, string UserId)
        {
            bool Result = userbll.SaveUserRole(RoleIds, UserId);
            return Json(new { Result = Result });
        }
        //获取用户角色
        public JsonResult GetRole(string UserId)
        {
            var list = userbll.GetList<sys_userrole>(item => item.UserId == UserId).ToList();
            return Json(list);
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
                ChiPath = "HeadImg",
                FileName = Guid.NewGuid().ToString(),
                Postfix = Postfix,
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