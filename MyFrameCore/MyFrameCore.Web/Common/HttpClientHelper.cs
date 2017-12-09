using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Collections.Generic;
using MyFrameCore.Common;
using MyFrameCore.Model.Extend;
using System.Linq;

namespace MyFrameCore.Web
{
    public class HttpClientHelper
    {
        /// <summary>
        /// 发送方调用api认证方式
        /// </summary>
        public enum ApiAuthenticationEnum
        {
            /// <summary>
            /// 无身份验证
            /// </summary>
            None,
            /// <summary>
            /// 用户调用WebApi接口
            /// </summary>
            User
        }
        /// <summary>
        /// 创建HttpClient
        /// </summary>
        /// <param name="apiAuthentication"></param>
        /// <returns>HttpClient</returns>
        private static HttpClient PrepareHttpClient(ApiAuthenticationEnum apiAuthentication, string data = "")
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            // ssl请求
            //handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;正式环境报错：他处理程序不支持自定义与此结合证书处理
            var httpClient = new HttpClient(handler);
            var authentication = GetAuthenticationHeaderValue(apiAuthentication);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string salt = getsalt(), passid = getpassid(salt, data), timestamp = gettime(), signature = getsign(timestamp, salt, passid, data);
            httpClient.DefaultRequestHeaders.Add("passid", passid);
            httpClient.DefaultRequestHeaders.Add("timestamp", timestamp);
            httpClient.DefaultRequestHeaders.Add("salt", salt);
            httpClient.DefaultRequestHeaders.Add("signature", signature);
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            httpClient.DefaultRequestHeaders.Authorization = authentication;

            return httpClient;
        }
        /// <summary>
        /// 签名
        /// </summary>
        /// <returns></returns>
        private static string getsign(string timestamp, string salt, string passid, string data)
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
            return result.ToString();
        }
        /// <summary>
        /// 随机盐(唯一标识)
        /// </summary>
        /// <returns></returns>

        private static string getsalt()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 时间标记
        /// </summary>
        /// <returns></returns>
        private static string gettime()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(2000, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        /// <summary>
        /// 通行ID
        /// </summary>
        /// <returns></returns>
        private static string getpassid(string salt, string data)
        {
            ApiPassModel model = null;
            var appsetting = ConfigHelper.GetAppSetting();
            var rh = new RedisHelper(appsetting);
            string result = rh.Get("api_passid");
            if (string.IsNullOrEmpty(result))
            {
                model = new ApiPassModel { PassId = Guid.NewGuid().ToString(), LastTime = DateTime.Now };
                rh.Set("api_passid", JsonConvert.SerializeObject(model));
            }
            else
            {
                model = JsonConvert.DeserializeObject<ApiPassModel>(result);
                //passid1小时过期
                if (model == null || string.IsNullOrEmpty(model.PassId) || (DateTime.Now - Convert.ToDateTime(model.LastTime)).TotalMinutes > 60)
                {
                    model = new ApiPassModel { PassId = Guid.NewGuid().ToString(), LastTime = DateTime.Now };
                    rh.Set("api_passid", JsonConvert.SerializeObject(model));
                }
            }
            rh.Set(salt, data);
            return model.PassId;
        }

        /// <summary>
        /// 获取认证方式
        /// </summary>
        /// <param name="apiAuthentication">认证方式</param>
        /// <returns></returns>
        private static AuthenticationHeaderValue GetAuthenticationHeaderValue(ApiAuthenticationEnum apiAuthentication)
        {
            AuthenticationHeaderValue authentication;
            switch (apiAuthentication)
            {
                default:
                    authentication = null;
                    break;
                case ApiAuthenticationEnum.User:
                    //此身份验证方式安全性较低
                    authentication = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.UTF8.GetBytes("MyFrameCore:U2FsdGVkX1+EMHCSK8N6i8IOZOCj3hfGEb2DcesfWk4=")));
                    break;
            }
            return authentication;
        }
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponse(string url, Dictionary<string, string> parames)
        {
            try
            {
                if (url.StartsWith("https"))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                }
                string query = string.Empty, queryStr = string.Empty;
                if (parames != null && parames.Count > 0)
                {
                    Tuple<string, string> parameters = GetQueryString(parames);
                    query = parameters.Item1;
                    queryStr = parameters.Item2;
                }
                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User, query);
                HttpResponseMessage response = httpClient.GetAsync(!string.IsNullOrEmpty(queryStr) ? $"{url}?{queryStr}" : $"{url}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }

            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }

            return null;
        }

        public static T GetResponse<T>(string url, Dictionary<string, string> parames)
            where T : class, new()
        {
            T result = default(T);
            try
            {
                if (url.StartsWith("https"))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                }
                string query = string.Empty, queryStr = string.Empty;
                if (parames != null && parames.Count > 0)
                {
                    Tuple<string, string> parameters = GetQueryString(parames);
                    query = parameters.Item1;
                    queryStr = parameters.Item2;
                }
                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User, query);
                HttpResponseMessage response = httpClient.GetAsync(!string.IsNullOrEmpty(queryStr) ? $"{url}?{queryStr}" : $"{url}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    result = JsonConvert.DeserializeObject<T>(s);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }

            return result;
        }

        /// <summary>
        /// 拼接get参数
        /// </summary>
        /// <param name="parames"></param>
        /// <returns></returns>
        private static Tuple<string, string> GetQueryString(Dictionary<string, string> parames)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parames);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");  //签名字符串
            StringBuilder queryStr = new StringBuilder(""); //url参数
            if (parames == null || parames.Count == 0)
                return new Tuple<string, string>("", "");

            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    query.Append(key).Append(value);
                    queryStr.Append("&").Append(key).Append("=").Append(value);
                }
            }

            return new Tuple<string, string>(query.ToString(), queryStr.ToString().Substring(1, queryStr.Length - 1));
        }


        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static string PostResponse(string url, object postData = null)
        {
            try
            {
                if (url.StartsWith("https"))
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                if (postData == null)
                {
                    postData = new { };
                }
                string data = JsonConvert.SerializeObject(postData);
                HttpContent httpContent = new StringContent(data, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User, data);
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
            return null;
        }

        /// <summary>
        /// 发起post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url</param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static T PostResponse<T>(string url, object postData = null)
            where T : class, new()
        {
            T result = default(T);
            try
            {
                if (url.StartsWith("https"))
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                if (postData == null)
                {
                    postData = new { };
                }
                string data = JsonConvert.SerializeObject(postData);
                HttpContent httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User, data);

                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;

                    result = JsonConvert.DeserializeObject<T>(s);
                }

            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
            return result;
        }


        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex"></param>
        private static void ErrorLog(Exception ex)
        {
            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH时mm分ss秒");
            var datetDay = DateTime.Now.ToString("yyyy-MM-dd");
            var fileurl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorInfo");
            var urlFiName = Path.Combine(fileurl, string.Format("{0}.txt", datetDay));
            if (!System.IO.Directory.Exists(fileurl))
                System.IO.Directory.CreateDirectory(fileurl);
            if (!System.IO.File.Exists(urlFiName))
                System.IO.File.CreateText(urlFiName).Dispose();
            using (var sws = System.IO.File.AppendText(urlFiName))
            {
                sws.WriteLine("-------------------{0}-----------------", datetime);
                sws.WriteLine("api失败：{0}", ex.Message);
                sws.WriteLine(ex.StackTrace);
                sws.WriteLine("-------------------------------------------------------");
                sws.WriteLine();
            }
        }

        /// <summary>
        /// V3接口全部为Xml形式，故有此方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T PostXmlResponse<T>(string url, string xmlString)
            where T : class, new()
        {
            T result = default(T);
            try
            {
                if (url.StartsWith("https"))
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpContent httpContent = new StringContent(xmlString);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;

                    result = XmlDeserialize<T>(s);
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
            return result;
        }

        /// <summary>
        /// 反序列化Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(string xmlString)
            where T : class, new()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xmlString))
                {
                    return (T)ser.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("XmlDeserialize发生异常：xmlString:" + xmlString + "异常信息：" + ex.Message);
            }

        }
    }
}
