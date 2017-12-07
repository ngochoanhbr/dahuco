/*
 * 1）MD5加密
 * 2）Base64加密
 * 3）DES加密
 * 4) 异或加密
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SinGooCMS.Utility
{
    public static class DEncryptUtils
    {
        private const string strKey = "SinGooCMS-CreateByLiqiang665@163.com-20120510pm1445";

        #region MD5加密
        /// <summary>
        /// md5加密，返回第6至13位置中的字符,输入字符串 strText
        /// </summary>
        /// <param name="password">密码明文</param>
        /// <returns></returns>
        public static string MD5EnCode(string strText)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(strText))).Replace("-", "").Substring(6, 13);
        }
        #endregion

        #region Base64加密
        public static string Base64Encode(string strTxt)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(strTxt);
            return Convert.ToBase64String(encbuff);
        }

        public static string Base64Decode(string strTxt)
        {
            byte[] decbuff = Convert.FromBase64String(strTxt);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        #endregion

        #region DES
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESEncode(string strTxt, string strKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(strKey.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(strKey.Substring(0, 8));
            //byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(strTxt); 20120530pm1734
            byte[] bytes = Encoding.UTF8.GetBytes(strTxt);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            stream.Close();
            return builder.ToString();
        }
        public static string DESEncode(string strTxt)
        {
            return DESEncode(strTxt, strKey);
        }
        /// <summary>
        /// Des 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESDecode(string strTxt, string strKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            provider.Key = Encoding.ASCII.GetBytes(strKey.Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(strKey.Substring(0, 8));
            byte[] buffer = new byte[strTxt.Length / 2];
            for (int i = 0; i < (strTxt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(strTxt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num2;
            }
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream.Close();
            //return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
            //20120510pm1449修改
            return Encoding.UTF8.GetString(stream.ToArray());
        }
        public static string DESDecode(string strTxt)
        {
            try
            {
                return DESDecode(strTxt, strKey);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region 异或加密
        /// <summary>
        /// 异或加密,对原文加密成密文,对密文加密成原文
        /// </summary>
        /// <param name="strTxt"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetXORCode(string strTxt, string strKey)
        {
            byte[] bStr = (new UnicodeEncoding()).GetBytes(strTxt);
            byte[] bKey = (new UnicodeEncoding()).GetBytes(strKey);

            for (int i = 0; i < bStr.Length; i += 2)
            {
                for (int j = 0; j < bKey.Length; j += 2)
                {
                    bStr[i] = Convert.ToByte(bStr[i] ^ bKey[j]);
                }
            }

            return (new UnicodeEncoding()).GetString(bStr).TrimEnd('\0');
        }
        /// <summary>
        /// 异或加密
        /// </summary>
        /// <param name="strTxt"></param>
        /// <returns></returns>
        public static string GetXORCode(string strTxt)
        {
            return GetXORCode(strTxt, strKey);
        }
        #endregion

        #region SHA512加密 20120510pm1450添加
        /// <summary>
        /// SHA512 加密 
        /// </summary>
        /// <param name="strTxt"></param>
        /// <returns></returns>
        public static string SHA512Encrypt(string strTxt)
        {
            byte[] byteTemp;
            SHA512 sha512 = new SHA512Managed();


            UTF8Encoding utf8 = new UTF8Encoding();
            byteTemp = utf8.GetBytes(strTxt);
            byteTemp = sha512.ComputeHash(byteTemp);

            String strTemp = "";
            for (int i = 0; i < byteTemp.Length; i++)
                strTemp += byteTemp[i].ToString("x2"); //返回密码串小写x2 大写X2

            return strTemp;
        }
        #endregion
    }
}
