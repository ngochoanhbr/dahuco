using System;
using System.Text;
using System.IO;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// 简繁转换
    /// </summary>
    public class ConvertST
    {
        //简体字库
        private static string _sCharactorLib = string.Empty;
        //繁体字库
        private static string _tCharactorLib = string.Empty;

        static ConvertST()
        {
            string strSimplifiedLibPath = System.Web.HttpContext.Current.Server.MapPath("~/Include/Language/lib/simplified.txt");
            string strTraditionalLibPath = System.Web.HttpContext.Current.Server.MapPath("~/Include/Language/lib/traditional.txt");
            if (File.Exists(strSimplifiedLibPath))
                _sCharactorLib = new StreamReader(strSimplifiedLibPath, System.Text.Encoding.UTF8).ReadToEnd();
            if (File.Exists(strTraditionalLibPath))
                _tCharactorLib = new StreamReader(strTraditionalLibPath, System.Text.Encoding.UTF8).ReadToEnd();
        }

        /// <summary>
        /// 转化为简体中文
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string ToSimplified(string strSource)
        {
            string str = "", ch;
            int i, index;
            byte[] bt;
            for (i = 0; i < strSource.Length; i++)
            {
                ch = strSource.Substring(i, 1);
                bt = Encoding.Default.GetBytes(ch);
                if (bt.Length > 1)
                {
                    index = _tCharactorLib.IndexOf(ch);
                    if (index != -1)
                    {
                        str += _sCharactorLib.Substring(index, 1);
                    }
                    else
                    {
                        str += ch;
                    }
                }
                else
                {
                    str += ch;
                }
            }

            return str;
        }

        /// <summary>
        /// 转化为繁体中文
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string ToTraditional(string strSource)
        {
            string str = "", ch;
            int i, index;
            byte[] bt;
            for (i = 0; i < strSource.Length; i++)
            {
                ch = strSource.Substring(i, 1);
                bt = Encoding.Default.GetBytes(ch);
                if (bt.Length > 1)
                {
                    index = _sCharactorLib.IndexOf(ch);
                    if (index != -1)
                    {
                        str += _tCharactorLib.Substring(index, 1);
                    }
                    else
                    {
                        str += ch;
                    }
                }
                else
                {
                    str += ch;
                }
            }

            return str;
        }
    }
}
