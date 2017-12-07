/*
 * 1)输入字符串是否数字
 * 2)是否有中文字符
 * 3)是否有效邮箱
 * 4)是否是网络地址
 * 5)是否是中国手机号码
 * 6)是否是时间格式
 * 7)是否为IP
 * 8)检测扩展名的有效性
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace SinGooCMS.Utility
{
    public static class ValidateUtils
    {
        private static Regex RegEn = new Regex("^[a-zA-Z]+$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        private static Regex RegEmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static Regex RegUrl = new Regex("^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        private static Regex RegMobilePhone = new Regex("^13|14|15|17|18|19[0-9]{9}$");
        private static Regex RegIP = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        private static Regex RegUnicode = new Regex(@"^[\u4E00-\u9FA5\uE815-\uFA29]+$");

        #region 只有英文字母
        /// <summary>
        /// 是否只有英文字母
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsEn(string inputData)
        {
            if (!string.IsNullOrEmpty(inputData))
            {
                Match m = RegEn.Match(inputData);
                return m.Success;
            }
            else
                return false;
        }
        #endregion

        #region 数字字符串检查

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            if (!string.IsNullOrEmpty(inputData))
            {
                Match m = RegNumber.Match(inputData);
                return m.Success;
            }
            else
                return false;
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 是否包含中文

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 是否邮件地址
        /// <summary>
        /// 是否是邮箱
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }

        #endregion

        #region 是否是URL地址
        /// <summary>
        /// 是否是网络地址
        /// </summary>
        /// kevin 12.12
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsUrl(string inputDate)
        {
            Match m = RegUrl.Match(inputDate);
            return m.Success;
        }
        #endregion

        #region 是否是手机号码
        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// kevin 12.12
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string inputDate)
        {
            Match m = RegMobilePhone.Match(inputDate);
            return m.Success;
        }
        #endregion

        #region 是否是时间格式
        /// <summary>
        /// 判断是否日期部分
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsDate(string inputData)
        {
            bool _isDate = false;
            string matchStr = "";
            matchStr += @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$ ";
            RegexOptions option = (RegexOptions.IgnoreCase | (RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace));

            if (Regex.IsMatch(inputData, matchStr, option))
                _isDate = true;
            else
                _isDate = false;
            return _isDate;
        }
        /// <summary>
        /// 判断是否时间部分
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsTime(string inputData)
        {
            bool _isDate = false;
            string matchStr = "";
            matchStr += @"^([01]\d|2[0-3]):([0-5]\d):([0-5]\d)$";
            RegexOptions option = (RegexOptions.IgnoreCase | (RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace));

            if (Regex.IsMatch(inputData, matchStr, option))
                _isDate = true;
            else
                _isDate = false;
            return _isDate;
        }
        /// <summary>
        /// 判断是否日期格式
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsDateTime(string inputData)
        {
            bool _isDate = false;
            string matchStr = "";
            matchStr += @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) "; matchStr += @"(\s(((0?[0-9])|([1-2][0-3]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$ ";
            RegexOptions option = (RegexOptions.IgnoreCase | (RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace));

            if (Regex.IsMatch(inputData, matchStr, option))
                _isDate = true;
            else
                _isDate = false;
            return _isDate;
        }

        #endregion

        #region 是否为IP
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            Match m = RegIP.Match(ip);
            return m.Success;
        }
        #endregion

        #region 检测扩展名的有效性
        /// <summary>
        /// 检测扩展名的有效性
        /// </summary>
        /// <param name="extensionLim"></param>
        /// <param name="sExt"></param>
        /// <returns></returns>
        public static bool CheckValidExt(string extensionLim, string sExt)
        {
            bool flag = false;
            string[] aExt = extensionLim.Split('|');
            foreach (string filetype in aExt)
            {
                if (filetype.ToLower() == sExt)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion

        #region 是否Unicode编码
        public static bool IsUnicode(string strValue)
        {
            Match m = RegUnicode.Match(strValue);
            return m.Success;
        }
        #endregion

        #region 是否图片格式
        public static string[] arrImageExt = { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
        public static bool IsImage(string strExtension)
        {
            return arrImageExt.Contains<string>(strExtension.ToLower());
        }
        #endregion
    }
}
