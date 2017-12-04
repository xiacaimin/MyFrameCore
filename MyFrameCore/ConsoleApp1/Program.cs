using System;
using MyFrameCore.Common;
using CSRedis;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string pwd = "xcm199202126754";
            string random = new Random().Next(10000, 99999).ToString();
            Console.WriteLine(string.Format("原密码：{0}     随机数：{1}", pwd, random));
            Console.WriteLine(string.Format("AES加密：{0}", Encrypter.EncryptByAES(random)));
            Console.WriteLine(string.Format("AES解密：{0}", Encrypter.DecryptByAES(Encrypter.EncryptByAES(random))));
            Console.WriteLine(string.Format("SHA1加密：{0}", Encrypter.EncryptBySHA1(random + pwd)));
            //QuickHelperBase.Remove("9964d81e-7ef5-4d33-b693-ec0c4fee3b25");
            QuickHelperBase.Instance = new ConnectionPool("127.0.0.1",6379);
            string v = QuickHelperBase.Get(null);
            //Console.WriteLine(v);
        }
    }
}
