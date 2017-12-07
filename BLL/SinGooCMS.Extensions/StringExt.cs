using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Extensions
{
    public static class StringExt
    {
        /// <summary>
        /// 判断输入是否为空
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string strInput)
        {
            return string.IsNullOrEmpty(strInput);
        }

        #region 字符串数据类型转换
        /// <summary>
        /// 字符串转化为整型
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static int ToInt(this string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                int result = 0;
                if (Int32.TryParse(strInput, out result))
                    return result;
            }

            return 0;
        }
        /// <summary>
        /// 字符串转化为布尔型
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool ToBool(this string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                if (string.Compare(strInput, "true", true).Equals(0))
                    return true;

                bool result = false;
                if (Boolean.TryParse(strInput, out result))
                    return result;
            }

            return false;
        }
        /// <summary>
        /// 字符串转化为浮点型
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static float ToFloat(this string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                float result = 0.0f;
                if (float.TryParse(strInput, out result))
                    return result;
            }

            return 0.0f;
        }
        /// <summary>
        /// 字符串转化为数据类型
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                decimal result = 0.0m;
                if (Decimal.TryParse(strInput, out result))
                    return result;
            }

            return 0.0m;
        }
        /// <summary>
        /// 字符串转化为日期类型
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string strInput)
        {
            if (!string.IsNullOrEmpty(strInput))
            {
                DateTime result = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(strInput, out result))
                    return result;
            }

            return new DateTime(1900, 1, 1);
        }

        #endregion

        #region 转化为大写的人民币
        public static string ToRMB(this decimal decMoney)
        {
            string[] strN = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] strC = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] strA = { "", "圆", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万亿", "拾", "佰", "仟", "亿亿", };
            int[] nLoc = { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 };

            string strFrom = "";
            string strTo = "";
            string strChar;
            int m, mLast = -1, nCount = 0;

            if (strFrom.Length > strA.GetUpperBound(0) - 1) return "***拜托，这么多钱还需要数吗***";

            if (decMoney < 0)
            {
                decMoney *= -1;
                strTo = "负";
            }

            Int64 n1 = (Int64)decMoney;                   // 元
            strFrom = n1.ToString();

            for (int i = strFrom.Length; i > 0; i--)
            {
                strChar = strFrom.Substring(strFrom.Length - i, 1);
                m = Convert.ToInt32(strChar);
                if (m == 0)
                {
                    // 连续为０时需要补齐中间单位,且只补第一次
                    if (nLoc[i] > 0 && nCount == 0 && strFrom.Length > 1)
                    {
                        strTo = strTo + strA[i];
                        nCount++;
                    }
                }
                else
                {
                    // 补０
                    if (mLast == 0)
                    {
                        strTo = strTo + strC[0];
                    }

                    // 数字转换为大写
                    strTo = strTo + strC[m];
                    // 补足单位
                    strTo = strTo + strA[i];
                    nCount = 0;
                }
                mLast = m;
            }

            Int64 n2 = ((Int64)(decMoney * 100)) % 100;   // 角分
            Int64 n3 = n2 / 10;                     // 角
            Int64 n4 = n2 % 10;                     // 分
            string s2 = "";

            if (n4 > 0)
            {
                s2 = strC[n4] + "分";
                if (n3 > 0)
                {
                    s2 = strC[n3] + "角" + s2;
                }
            }
            else
            {
                if (n3 > 0)
                {
                    s2 = strC[n3] + "角";
                }
            }
            strTo = strTo + s2;

            if (strTo == "") strTo = strC[0];                   // 全0显示为零
            else if (s2 == "") strTo = strTo + "整";            // 无角分显示整
            return strTo;
        }
        #endregion

        #region 去除空格
        public static string RemoveWhiteSpace(this string strInput)
        {
            bool changed = false;
            char[] output = strInput.ToCharArray();
            int cursor = 0;
            for (int i = 0, size = output.Length; i < size; i++)
            {
                char c = output[i];
                if (Char.IsWhiteSpace(c))
                {
                    changed = true;
                    continue;
                }

                output[cursor] = c;
                cursor++;
            }

            return changed ? new string(output, 0, cursor) : strInput;
        }
        #endregion

        #region HTML代码与TXT格式转换

        /// <summary>
        /// 把HTML代码转换成TXT格式
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static String ToTxt(String strInput)
        {
            StringBuilder builder = new StringBuilder(strInput);
            builder.Replace("&nbsp;", " ");
            builder.Replace("<br>", "\r\n");
            builder.Replace("<br>", "\n");
            builder.Replace("<br />", "\n");
            builder.Replace("<br />", "\r\n");
            builder.Replace("&lt;", "<");
            builder.Replace("&gt;", ">");
            builder.Replace("&amp;", "&");
            return builder.ToString();
        }

        /// <summary>
        /// 把TXT代码转换成HTML格式
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static String ToHtml(string strInput)
        {
            StringBuilder sb = new StringBuilder(strInput);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            return sb.ToString();
        }

        #endregion
    }
}
