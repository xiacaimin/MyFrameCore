using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFrameCore.BLL;
using MyFrameCore.Common;
using MyFrameCore.Model;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MyFrameCore.Model.Extend;

namespace MyFrameCore.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        Sys_UserBLL userbll;
        AppSettings appsettings;
        public LoginController(Sys_UserBLL userbll)
        {
            this.userbll = userbll;
        }

        /// <summary>
        /// 按后台参数大小写不变json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private JsonResult Json(object data)
        {
            //处理循环引用问题
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //设置不处理循环引用
            return Json(data, settings);
        }

        [Area("Admin")]
        public IActionResult Index()
        {
            HttpContext.Session.SetString(ConstConfig.AdminSession, string.Empty);
            HttpContext.Response.Cookies.Append(ConstConfig.AdminCookie, string.Empty, new CookieOptions
            {
                Expires = DateTime.Now.AddSeconds(1)
            });
            return View();
        }

        [Area("Admin")]
        public IActionResult UpgradeBrowser()
        {
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public JsonResult CheckLogin(sys_user model)
        {
            bool Result = false;
            var UserInfo = userbll.GetUserInfo(model.Account, MD5Provider.getStringMd5Hash(model.PassWord));
            if (UserInfo != null)
            {
                Result = true;

                #region 保存登陆信息
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                string result = string.Empty;
                using (var streamReader = System.IO.File.OpenText(filePath))
                {
                    result = streamReader.ReadToEnd();
                }
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result.Replace("\r\n", ""));
                appsettings = JsonConvert.DeserializeObject<AppSettings>(jobject["AppSettings"].ToString());
                if (Convert.ToBoolean(appsettings.Redis.IsEnable))
                {
                    //redis方式
                    RedisHelper rh = new RedisHelper(appsettings);
                    UserExtend ue = new UserExtend
                    {
                        KeyId = UserInfo.KeyId,
                        Account = UserInfo.Account,
                        FullName = UserInfo.FullName,
                        HeadImg = UserInfo.HeadImg,
                        WebLastTime = DateTime.Now
                    };
                    rh.Set(UserInfo.KeyId, JsonConvert.SerializeObject(ue));

                }
                else
                {
                    //session方式
                    HttpContext.Session.SetString(ConstConfig.AdminSession, JsonConvert.SerializeObject(UserInfo));

                }

                /****用cookie保存登录id，即使session丢失也不必重新登录****/
                HttpContext.Response.Cookies.Append(ConstConfig.AdminCookie, UserInfo.KeyId, new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(5)
                });
                //全局配置的静态类成员再赋值，保证配置文件被更改后立即登陆就不用重启才生效(折中办法)
                WebJsonConfig.JsonInfo = JsonConvert.SerializeObject(appsettings);
                #endregion
            }
            return Json(new { Result = Result });
        }
    }
}