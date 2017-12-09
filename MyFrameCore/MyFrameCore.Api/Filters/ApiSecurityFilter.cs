using Microsoft.AspNetCore.Mvc.Filters;
using MyFrameCore.Api.Common;
using MyFrameCore.Common;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Web;
using System.Linq;


namespace MyFrameCore.Api.Filters
{
    public class ApiSecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var request = actionContext.HttpContext.Request;
            var appsetting = ConfigHelper.GetAppSetting();
            var rh = new RedisHelper(appsetting);
            string passid = String.Empty, timestamp = string.Empty, salt = string.Empty, signature = string.Empty;
            //处理循环引用问题
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //设置不处理循环引用

            if (request.Headers.ContainsKey("passid"))
            {
                passid = HttpUtility.UrlDecode(request.Headers["passid"]);
            }
            if (request.Headers.ContainsKey("timestamp"))
            {
                timestamp = HttpUtility.UrlDecode(request.Headers["timestamp"]);
            }
            if (request.Headers.ContainsKey("salt"))
            {
                salt = HttpUtility.UrlDecode(request.Headers["salt"]);
            }

            if (request.Headers.ContainsKey("signature"))
            {
                signature = HttpUtility.UrlDecode(request.Headers["signature"]);
            }

            //welcome方法不需要进行签名验证
            var urlarray = actionContext.ActionDescriptor.AttributeRouteInfo.Template.Split('/');
            if (urlarray[urlarray.Length - 1].ToLower().Equals("welcome"))
            {
                if (!string.IsNullOrEmpty(salt))
                {
                    rh.Remove(salt);
                }
                base.OnActionExecuting(actionContext);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(passid) || string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(signature))
                {
                    actionContext.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new ExecuteResult
                    {
                        Result = false,
                        Message = "请求参数不完整或不正确"
                    }, settings);
                    if (!string.IsNullOrEmpty(salt))
                    {
                        rh.Remove(salt);
                    }
                    base.OnActionExecuting(actionContext);
                    return;
                }
            }

            //判断timespan是否有效
            double ts1 = 0;
            double ts2 = (DateTime.UtcNow - new DateTime(2000, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            bool timespanvalidate = double.TryParse(timestamp, out ts1);
            double ts = ts2 - ts1;
            bool falg = ts > 60 * 1000;//设置过期时间1分钟
            if (falg || (!timespanvalidate))
            {
                actionContext.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new ExecuteResult
                {
                    Result = false,
                    Message = "此链接已经失效"
                }, settings);
                if (!string.IsNullOrEmpty(salt))
                {
                    rh.Remove(salt);
                }
                base.OnActionExecuting(actionContext);
                return;
            }
            //根据请求类型拼接参数
            string data = rh.Get(salt);
            //验证参数是否篡改
            bool result = Validate(timestamp, salt, passid, data, signature);
            if (!result)
            {
                actionContext.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new ExecuteResult
                {
                    Result = false,
                    Message = "http请求参数可能被篡改"
                }, settings);
                if (!string.IsNullOrEmpty(salt))
                {
                    rh.Remove(salt);
                }
                base.OnActionExecuting(actionContext);
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(salt))
                {
                    rh.Remove(salt);
                }
                base.OnActionExecuting(actionContext);
            }
        }

        private bool Validate(string timestamp, string salt, string passid, string data, string signature)
        {
            var hash = System.Security.Cryptography.MD5.Create();
            //拼接签名数据
            var signStr = $"{timestamp}{salt}{passid}{data}"; //timestamp + salt + passid + data;
            //将字符串中字符按升序排序
            var sortStr = string.Concat(signStr.OrderBy(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            //使用MD5加密
            var md5Val = hash.ComputeHash(bytes);
            //把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val)
            {
                result.Append(c.ToString("X2"));
            }
            return result.ToString().ToUpper() == signature;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

    }
}