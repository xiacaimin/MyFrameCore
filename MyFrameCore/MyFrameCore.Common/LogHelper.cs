using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFrameCore.Common
{
    public class LogHelper
    {
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex">错误源</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">方法</param>
        public static void ErrorLog(Exception ex, string controller, string action)
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
    }
}
