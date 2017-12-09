using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using MyFrameCore.Common;
using MyFrameCore.Model;
using Microsoft.Extensions.Options;
using MyFrameCore.Api.Service;
using MySql.Data.MySqlClient.Framework.NetCore10;

namespace MyFrameCore.Api.Controllers
{
    /*
     * 安全验证与[FromBody]不兼容，如果不用[FromBody]，Action参数不能直接获取
     * */
    [Route("api/[controller]/[action]")]
    public class ImgController : Controller
    {
        AppSettings appsettings;
        public ImgController(IOptions<AppSettings> option)
        {
            this.appsettings = option.Value;
        }

        [HttpPost]
        public ExecuteResult GetUserInfo([FromBody]sys_user model)
        {
            UserService service = new UserService();
            ExecuteResult er = new ExecuteResult { Result = false, Message = "用户名或密码错误" };
            var user = service.GetUserInfo(model.Account, model.PassWord);
            if (user != null)
            {
                er.Result = true;
                er.Message = string.Empty;
                er.ReturnVal = user;
            }
            return er;
        }

        [HttpPost]
        public ExecuteResult UploadOneImg([FromBody]ImgUploadModel ium)
        {
            try
            {
                var MyFrameImgRoot = appsettings.MyFrameImgRoot;
                //子目录
                if (!string.IsNullOrEmpty(ium.ChiPath))
                {

                    var array = ium.ChiPath.Trim('/').Split('/');
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(array[i]))
                        {
                            MyFrameImgRoot = Path.Combine(MyFrameImgRoot, array[i]);
                        }
                    }

                }
                if (!Directory.Exists(MyFrameImgRoot))
                {
                    Directory.CreateDirectory(MyFrameImgRoot);
                }
                string filePath = Path.Combine(MyFrameImgRoot, ium.FileName + "." + ium.Postfix);
                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Write(ium.Byte, 0, ium.Byte.Length);
                fs.Flush();
                fs.Close();
                if (System.IO.File.Exists(filePath))//表示图片上传成功
                {
                    return new ExecuteResult()
                    {
                        Result = true,
                        Message = "上传文件成功",
                    };
                }
                else
                {
                    return new ExecuteResult()
                    {
                        Result = false,
                        Message = "上传文件失败",
                    };
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(ex, "Img", "UploadOneImg");
                return new ExecuteResult()
                {
                    Result = false,
                    Message = ex.ToString()
                };
            }

        }


    }
}
