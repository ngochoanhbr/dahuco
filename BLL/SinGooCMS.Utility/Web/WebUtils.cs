using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;

namespace SinGooCMS.Utility
{
    public static class WebUtils
    {
        #region query

        public static string GetQueryString(string key)
        {
            return GetQueryString(key, string.Empty);
        }
        public static string GetQueryString(string key, string defalutValue)
        {
            if (HttpContext.Current == null)
                return defalutValue;
            if (HttpContext.Current.Request.QueryString[key] == null)
                return defalutValue;

            return HttpUtility.HtmlDecode(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString[key])).Trim();
        }

        public static int GetQueryInt(string key)
        {
            return GetQueryInt(key, 0);
        }
        public static int GetQueryInt(string key, int defaultValue)
        {
            return StringToInt(GetQueryString(key), defaultValue);
        }

        public static bool GetQueryBool(string key)
        {
            return GetQueryBool(key, false);
        }
        public static bool GetQueryBool(string key, bool defaultValue)
        {
            return StringToBool(GetQueryString(key), defaultValue);
        }

        public static long GetQueryLong(string key)
        {
            return GetQueryLong(key, 0);
        }
        public static long GetQueryLong(string key, long defaultValue)
        {
            return StringToLong(GetQueryString(key), defaultValue);
        }

        public static float GetQueryFloat(string key)
        {
            return GetQueryFloat(key, 0);
        }
        public static float GetQueryFloat(string key, float defaultValue)
        {
            return StringToFloat(GetQueryString(key), defaultValue);
        }

        public static decimal GetQueryDecimal(string key)
        {
            return GetQueryDecimal(key, 0);
        }
        public static decimal GetQueryDecimal(string key, decimal defaultValue)
        {
            return StringToDecimal(GetQueryString(key), defaultValue);
        }

        public static DateTime GetQueryDatetime(string key)
        {
            return GetQueryDatetime(key, new DateTime(1900, 1, 1));
        }
        public static DateTime GetQueryDatetime(string key, DateTime defaultValue)
        {
            return StringToDateTime(GetQueryString(key), defaultValue);
        }

        #endregion

        #region form

        public static string GetFormString(string key)
        {
            return GetFormString(key, string.Empty);
        }
        public static string GetFormString(string key, string defalutValue)
        {
            if (HttpContext.Current == null)
                return defalutValue;
            if (HttpContext.Current.Request.Form[key] == null)
                return defalutValue;

            return HttpUtility.HtmlDecode(HttpUtility.UrlDecode(HttpContext.Current.Request.Form[key])).Trim();
        }

        public static int GetFormInt(string key)
        {
            return GetFormInt(key, 0);
        }
        public static int GetFormInt(string key, int defaultValue)
        {
            return StringToInt(GetFormString(key), defaultValue);
        }

        public static bool GetFormBool(string key)
        {
            return GetFormBool(key, false);
        }
        public static bool GetFormBool(string key, bool defaultValue)
        {
            return StringToBool(GetFormString(key), defaultValue);
        }

        public static float GetFormFloat(string key)
        {
            return GetFormFloat(key, 0);
        }
        public static float GetFormFloat(string key, float defaultValue)
        {
            return StringToFloat(GetFormString(key), defaultValue);
        }

        public static decimal GetFormDecimal(string key)
        {
            return GetFormDecimal(key, 0);
        }
        public static decimal GetFormDecimal(string key, decimal defaultValue)
        {
            return StringToDecimal(GetFormString(key), defaultValue);
        }

        public static DateTime GetFormDatetime(string key)
        {
            return GetFormDatetime(key, new DateTime(1900, 1, 1));
        }
        public static DateTime GetFormDatetime(string key, DateTime defaultValue)
        {
            return StringToDateTime(GetFormString(key), defaultValue);
        }

        #endregion

        #region 取值
        public static string GetString(object value)
        {
            return GetString(value, string.Empty);
        }
        public static string GetString(object value, string defaultValue)
        {
            if (value == null)
                return defaultValue;

            return StringUtils.InputTexts(value.ToString());
        }

        public static int GetInt(object value)
        {
            return GetInt(value, 0);
        }
        public static int GetInt(object value, int defaultValue)
        {
            if (value == null)
                return defaultValue;

            return StringToInt(value.ToString(), defaultValue);
        }

        public static bool GetBool(object value)
        {
            return GetBool(value, false);
        }
        public static bool GetBool(object value, bool defaultValue)
        {
            if (value == null)
                return defaultValue;

            return StringToBool(value.ToString(), defaultValue);
        }

        public static DateTime GetDateTime(object value)
        {
            return GetDateTime(value, new DateTime(1900, 1, 1));
        }
        public static DateTime GetDateTime(object value, DateTime defaultValue)
        {
            if (value == null)
                return defaultValue;

            return StringToDateTime(value.ToString(), defaultValue);
        }

        public static double GetFloat(object value)
        {
            return GetFloat(value, 0.0f);
        }
        public static double GetFloat(object value, float defaultValue)
        {
            if (value == null)
                return defaultValue;

            return StringToFloat(value.ToString(), defaultValue);
        }

        public static double GetDouble(object value)
        {
            return GetDouble(value, 0d);
        }
        public static double GetDouble(object value, double defaultValue)
        {
            double num;
            if (value == null)
                return defaultValue;
            if (value is double)
                return (double)value;
            if (double.TryParse(value.ToString(), out num))
                return num;

            return defaultValue;
        }

        public static decimal GetDecimal(object value)
        {
            return GetDecimal(value, 0.0m);
        }
        public static decimal GetDecimal(object value, decimal defaultValue)
        {
            if (value == null)
                return defaultValue;

            return StringToDecimal(value.ToString(), defaultValue);
        }

        public static Guid GetGuid(object value, Guid defaultValue)
        {
            if (value == null)
                return defaultValue;
            if (value is Guid)
                return (Guid)value;

            return Guid.Empty;
        }
        #endregion

        #region 数据类型转换

        public static int StringToInt(string value)
        {
            return StringToInt(value, 0);
        }
        public static int StringToInt(string value, int defaultValue)
        {
            int num = new int();
            if (Int32.TryParse(value, out num))
                return num;

            return defaultValue;
        }

        public static long StringToLong(string value)
        {
            return StringToLong(value, 0);
        }
        public static long StringToLong(string value, long defaultValue)
        {
            long num = new long();
            if (Int64.TryParse(value, out num))
                return num;

            return defaultValue;
        }

        public static bool StringToBool(string value)
        {
            return StringToBool(value, false);
        }
        public static bool StringToBool(string value, bool defaultValue)
        {
            if (value != null)
            {
                if (string.Compare(value, "true", true) == 0 || value == "1")
                    return true;
                if (string.Compare(value, "false", true) == 0 || value == "0")
                    return false;
            }

            return defaultValue;
        }

        public static float StringToFloat(string value)
        {
            return StringToFloat(value, 0.0f);
        }
        public static float StringToFloat(string value, float defaultValue)
        {
            float num = new float();
            if (float.TryParse(value, out num))
                return num;

            return defaultValue;
        }

        public static decimal StringToDecimal(string value)
        {
            return StringToDecimal(value, 0.0m);
        }
        public static decimal StringToDecimal(string value, decimal defaultValue)
        {
            decimal num = new decimal();
            if (decimal.TryParse(value, out num))
                return num;

            return defaultValue;
        }

        public static DateTime StringToDateTime(string value)
        {
            return StringToDateTime(value, new DateTime(1900, 1, 1));
        }
        public static DateTime StringToDateTime(string value, DateTime defaultValue)
        {
            DateTime dt = new DateTime(1900, 1, 1);
            if (DateTime.TryParse(value, out dt))
                return dt;

            return defaultValue;
        }

        #endregion

        #region 其它

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        /// <summary>
        /// 查询字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlSearch(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }
        /// <summary>
        /// 获取当前语言设置
        /// </summary>
        /// <returns></returns>
        public static string GetCultureLang()
        {
            string strLang = CookieUtils.GetCookie("langcookie");
            if (!string.IsNullOrEmpty(strLang))
                return strLang;

            return "zh-en";
        }
        /// <summary>
        /// 获取有效完整的URL路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ResolveUrl(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                if (url.StartsWith("/"))
                    return (HttpContext.Current.Request.ApplicationPath + url).Replace("//", "/");

                if (url.StartsWith("~/"))
                    return url.Replace("~/", HttpContext.Current.Request.ApplicationPath);

            }
            return url;
        }
        /// <summary>
        /// 获取多语言标题
        /// </summary>
        /// <param name="strMainCaption"></param>
        /// <returns></returns>
        public static string GetCaption(string strMainCaption)
        {
            return GetCaption(strMainCaption, GetCultureLang());
        }
        /// <summary>
        /// 获取多语言标题
        /// </summary>
        /// <param name="strMainCaption"></param>
        /// <param name="strLanguage"></param>
        /// <returns></returns>
        public static string GetCaption(string strMainCaption, string strLanguage)
        {
            //xml文件路径
            string xmlFilePath = System.Web.HttpContext.Current.Server.MapPath("/Include/Language/" + strLanguage.Trim() + ".xml");
            if (!File.Exists(xmlFilePath))
                File.Create(xmlFilePath);

            XmlUtils xmlTool = new XmlUtils(xmlFilePath);
            string strMsg = xmlTool.GetXmlNodeValue("root//data[@name='" + strMainCaption + "']");
            if (!string.IsNullOrEmpty(strMsg))
                return strMsg;
            else
                return "ths message undefined!";
        }
        /// <summary>
        /// 是否移动端访问
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileVisit()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                HttpRequest request = context.Request;
                if (request.Browser.IsMobileDevice)
                    return true;

                string MobileUserAgent = System.Configuration.ConfigurationManager.AppSettings["MobileUserAgent"];
                Regex MOBILE_REGEX = new Regex(MobileUserAgent, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                if (!string.IsNullOrEmpty(request.UserAgent) && MOBILE_REGEX.IsMatch(request.UserAgent.ToLower()))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// 获取上传文件夹 如 /Upload/2012/12/
        /// </summary>
        /// <returns></returns>
        public static string GetUploadFolder()
        {
            string strFolder = System.IO.Path.Combine("/Upload/", System.DateTime.Now.Year.ToString() + "/" + System.DateTime.Now.Month.ToString() + "/");
            string strAbsoluteFolder = GetMapPath(strFolder);
            if (!System.IO.Directory.Exists(strAbsoluteFolder))
                System.IO.Directory.CreateDirectory(strAbsoluteFolder);

            return strFolder;
        }

        #endregion

        #region 获取缩略图
        /// <summary>
        /// 获取图片的缩略图
        /// </summary>
        /// <param name="strImagePath"></param>
        /// <returns></returns>
        public static string GetThumb(string strImagePath)
        {
            //上传的图片在文件名后面加上_thumb就是缩略图 如 aa.jpg，缩略图是aa_thumb.jpg
            if (!string.IsNullOrEmpty(strImagePath) && strImagePath.IndexOf('.') != -1)
            {
                return strImagePath.Substring(0, strImagePath.LastIndexOf('.')) + "_thumb" + Path.GetExtension(strImagePath);
            }

            return string.Empty;
        }
        #endregion
        public static bool IsWeixinVisit()
        {
            HttpContext current = HttpContext.Current;
            return current != null && !string.IsNullOrEmpty(current.Request.UserAgent) && current.Request.UserAgent.ToLower().IndexOf("micromessenger") != -1;
        }

    }
}

