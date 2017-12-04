using MyFrameCore.Model;
using MyFrameCore.Model.Extend;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFrameCore.Common
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        public static AppSettings GetAppSetting()
        {
            if (string.IsNullOrEmpty(WebJsonConfig.JsonInfo))
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                string result = string.Empty;
                using (var streamReader = System.IO.File.OpenText(filePath))
                {
                    result = streamReader.ReadToEnd();
                }
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result.Replace("\r\n", ""));
                return JsonConvert.DeserializeObject<AppSettings>(jobject["AppSettings"].ToString());
            }
            return JsonConvert.DeserializeObject<AppSettings>(WebJsonConfig.JsonInfo);
        }
    }
}
