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
        private static HttpClient PrepareHttpClient(ApiAuthenticationEnum apiAuthentication)
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
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            httpClient.DefaultRequestHeaders.Authorization = authentication;
            return httpClient;
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
        public static string GetResponse(string url)
        {
            try
            {
                if (url.StartsWith("https"))
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User);
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

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

        public static T GetResponse<T>(string url)
            where T : class, new()
        {
            T result = default(T);
            try
            {
                if (url.StartsWith("https"))
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User);
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
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
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(postData));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User);
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
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(postData));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpClient httpClient = PrepareHttpClient(ApiAuthenticationEnum.User);

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
