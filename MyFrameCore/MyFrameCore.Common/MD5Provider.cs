using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyFrameCore.Common
{
    public class MD5Provider
    {
        /// <summary>
        /// 字符串使用MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getStringMd5Hash(string str)
        {
            List<string> ls = new List<string>();
            using (MD5 md5 = MD5.Create())
            {
                byte[] strbytes = Encoding.UTF8.GetBytes(str);
                //  byte[] strbytes = Encoding.GetEncoding("gb2312").GetBytes(str);
                strbytes = md5.ComputeHash(strbytes);
                for (int i = 0; i < strbytes.Length; i++)
                {
                    ls.Add(strbytes[i].ToString("x2"));//将字节转换为字符串
                }
                // return  Encoding.UTF8.GetString(strbytes);
                return string.Concat(ls);
            }

        }

        /// <summary>
        /// 文件流使用MD5
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getFileMd5Hash(string fileName)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    List<string> lis = new List<string>();
                    byte[] b1 = md5.ComputeHash(fs);
                    for (int i = 0; i < b1.Length; i++)
                    {
                        lis.Add(b1[i].ToString("x2"));
                    }
                    return string.Concat(lis);
                }
            }

        }

        /// <summary>
        /// 中文使用MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getChineseMd5Hash(string str)
        {
            List<string> ls = new List<string>();
            using (MD5 md5 = MD5.Create())
            {
                byte[] strbytes = Encoding.GetEncoding("gb2312").GetBytes(str);
                strbytes = md5.ComputeHash(strbytes);
                for (int i = 0; i < strbytes.Length; i++)
                {
                    ls.Add(strbytes[i].ToString("x2"));//将字节转换为字符串
                }
                // return  Encoding.UTF8.GetString(strbytes);
                return string.Concat(ls);

            }
        }
    }
}
