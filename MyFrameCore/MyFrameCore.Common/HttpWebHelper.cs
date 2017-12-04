using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.Common
{
    /// <summary>
    /// 客户端访问接口专用类
    /// </summary>
    //public class HttpWebHelper
    //{
    //    public static string ApiUrl
    //    {
    //        get
    //        {
    //            return ConfigurationManager.AppSettings["ApiUrl"];
    //        }
    //    }
    //    /// <summary>
    //    /// get访问
    //    /// </summary>
    //    /// <returns>json格式</returns>
    //    public static string Get(string strRequestUri,/* string strReferer = "" ,*/ int timeout = 30*1000)
    //    {
    //        // 初始化HttpWebRequest
    //        HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(ApiUrl + strRequestUri);

    //        // 封装Http Header
    //        httpRequest.Method = "Get";
    //        //httpRequest.Referer = strReferer;
    //        httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.89 Safari/537.36";
    //        httpRequest.Accept = "text/plain, */*; q=0.01";
    //        httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
    //        httpRequest.Timeout = timeout;
    //        httpRequest.KeepAlive = true;
    //        httpRequest.ContentLength = 0;
    //        CredentialCache mycache = new CredentialCache();
    //        mycache.Add(new Uri(ApiUrl + strRequestUri), "Basic", new NetworkCredential("MyFrameCore", "123456"));
    //        httpRequest.Credentials = mycache;

    //        // 获得应答报文
    //        HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
    //        Stream responseStream = httpResponse.GetResponseStream();
    //        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
    //        string JsonData = reader.ReadToEnd();
    //        reader.Close();
    //        responseStream.Close();

    //        return JsonData;
    //    }

    //    /// <summary>
    //    /// post访问
    //    /// </summary>
    //    /// <param name="strRequestUri">地址</param>
    //    /// <param name="postData">参数(url参数格式)</param>
    //    /// <returns>json格式</returns>
    //    public static string Post(string strRequestUri, string postData, int timeout = 30*1000)
    //    {
    //        // 初始化HttpWebRequest
    //        HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(ApiUrl + strRequestUri);
    //        // 封装Http Header
    //        httpRequest.Method = "Post";
    //        httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.89 Safari/537.36";
    //        httpRequest.Accept = "text/plain, */*; q=0.01";

    //        httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
    //        httpRequest.Timeout = timeout;
    //        httpRequest.KeepAlive = true;
    //        //加入Authentication认证信息
    //        httpRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("MyFrameCore:123456")));

    //        // 通过流写入请求数据
    //        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ReplaceSpecialStr(postData)); // 编码形式按照个人需求来设置
    //        httpRequest.ContentLength = bytes.Length;
    //        Stream requestStream = httpRequest.GetRequestStream();
    //        requestStream.Write(bytes, 0, bytes.Length);
    //        requestStream.Close(); // 不要忘记关闭流

    //        // 获得应答报文
    //        HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
    //        Stream responseStream = httpResponse.GetResponseStream();
    //        StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);

    //        string JsonData = reader.ReadToEnd();
    //        reader.Close();
    //        responseStream.Close();
    //        return JsonData;
    //    }

    //    /// <summary>
    //    /// 处理URL中的特殊字符
    //    /// </summary>
    //    /// <param name="url"></param>
    //    /// <returns></returns>
    //    private static string ReplaceSpecialStr(string url)
    //    {
    //        if (!string.IsNullOrEmpty(url))
    //        {
    //            //return url.Replace("%", "%25").Replace("+", "%2B").Replace("/", "%20").Replace("?", "%3F").Replace("#", "%23").Replace("&", "%26").Replace("=", "%3D");
    //            return url.Replace("+", "%2B");
    //        }
    //        return "";
    //    }
    //}
}
