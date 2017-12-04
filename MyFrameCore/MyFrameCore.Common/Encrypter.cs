using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Globalization;
using System.Xml.Linq;
using System.Collections.Generic;

namespace MyFrameCore.Common
{
    /// <summary>
    /// 加密、解密
    /// </summary>
    public class Encrypter
    {
        /// <summary>
        /// 获取DES密钥(8位)
        /// </summary>
        private static string DES_Key
        {
            get { return @"P@+#wX+Z"; }
        }

        /// <summary>
        /// 获取DES向量(16位)
        /// </summary>
        private static string DES_IV
        {
            get { return @"C%n67}G\Mk@k%:~Y"; }
        }

        /// <summary>
        /// 获取AES密钥(32位)
        /// </summary>
        private static string AES_Key
        {
            get { return @")O[NB]6,XC}+efcaj{+oESb9d8>Z'e9M"; }
        }

        /// <summary>
        /// 获取AES向量(16位)
        /// </summary>
        private static string AES_IV
        {
            get { return @"M+\~f4,Ir)b$=pkf"; }
        }

        #region MD5
        /// <summary>
        /// MD5加密为32字符长度的16进制字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptByMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            //将每个字节转为16进制
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion

        #region SHA1
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input">待处理字符串</param>
        /// <returns></returns>
        public static string EncryptBySHA1(string input)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] result = sha.ComputeHash(bytes);
            return BitConverter.ToString(result);
        }
        #endregion

        #region DES
        /// <summary>
        /// DES加密方法
        /// </summary>
        /// <param name="input">待处理字符串</param>
        /// <returns></returns>
        public static string EncryptByDES(string input)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(DES_Key);
            byte[] bIV = Encoding.UTF8.GetBytes(DES_IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            string encrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch { }
            des.Clear();
            return encrypt;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input">待处理字符串</param>
        /// <returns></returns>
        public static string DecryptByDES(string input)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(DES_Key);
            byte[] bIV = Encoding.UTF8.GetBytes(DES_IV);
            byte[] byteArray = Convert.FromBase64String(input);
            string decrypt = null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch (Exception e) { throw e; }
            des.Clear();
            return decrypt;
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="input">待处理字符串</param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string EncryptString(string input, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return BitConverter.ToString(result);
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="input">待处理字符串</param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DecryptString(string input, string sKey)
        {
            string[] sInput = input.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = des.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.UTF8.GetString(result);
            }
        }
        #endregion

        #region AES


        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="input">待处理字符串</param>  
        /// <returns>字符串</returns>  
        public static string EncryptByAES(string input)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(AES_Key);
            byte[] bIV = Encoding.UTF8.GetBytes(AES_IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            string encrypt = null;
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                }
            }
            catch { }
            aes.Clear();

            return encrypt;
        }
        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="input">待处理字符串</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static string DecryptByAES(string input)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(AES_Key);
            byte[] bIV = Encoding.UTF8.GetBytes(AES_IV);
            byte[] byteArray = Convert.FromBase64String(input);

            string decrypt = null;
            Rijndael aes = Rijndael.Create();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch { }
            aes.Clear();

            return decrypt;
        }
        #endregion



        #region RSA
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>密文字符串</returns>
        public static string EncryptByRSA(string plaintext, string publicKey)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes(plaintext);
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(publicKey);
                byte[] encryptedData = RSA.Encrypt(dataToEncrypt, false);
                return Convert.ToBase64String(encryptedData);
            }
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>明文字符串</returns>
        public static string DecryptByRSA(string ciphertext, string privateKey)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(privateKey);
                byte[] encryptedData = Convert.FromBase64String(ciphertext);
                byte[] decryptedData = RSA.Decrypt(encryptedData, false);
                return byteConverter.GetString(decryptedData);
            }
        }


        /// <summary>
        /// 数字签名
        /// </summary>
        /// <param name="plaintext">原文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>签名</returns>
        public static string HashAndSignString(string plaintext, string privateKey)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = ByteConverter.GetBytes(plaintext);

            using (RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider())
            {
                RSAalg.FromXmlString(privateKey);
                //使用SHA1进行摘要算法，生成签名
                byte[] encryptedData = RSAalg.SignData(dataToEncrypt, new SHA1CryptoServiceProvider());
                return Convert.ToBase64String(encryptedData);
            }
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="plaintext">原文</param>
        /// <param name="SignedData">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static bool VerifySigned(string plaintext, string SignedData, string publicKey)
        {
            using (RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider())
            {
                RSAalg.FromXmlString(publicKey);
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] dataToVerifyBytes = ByteConverter.GetBytes(plaintext);
                byte[] signedDataBytes = Convert.FromBase64String(SignedData);
                return RSAalg.VerifyData(dataToVerifyBytes, new SHA1CryptoServiceProvider(), signedDataBytes);
            }
        }
        /// <summary>
        /// 获取Key
        /// 键为公钥，值为私钥
        /// </summary>
        /// <returns></returns>
        public static KeyValuePair<string, string> CreateRSAKey()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            string privateKey = RSA.ToXmlString(true);
            string publicKey = RSA.ToXmlString(false);

            return new KeyValuePair<string, string>(publicKey, privateKey);
        }
        #endregion

        #region other
        /// <summary>
        /// 获取Des8位密钥
        /// </summary>
        /// <param name="key">Des密钥字符串</param>
        /// <returns>Des8位密钥</returns>
        static byte[] GetDesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Des密钥不能为空");
            }
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            if (key.Length < 8)
            {
                // 不足8补全
                key = key.PadRight(8, '0');
            }
            return Encoding.UTF8.GetBytes(key);
        }
        #endregion
    }
}
