using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SinGooCMS.Utility
{
    public class CookieUtils
    {
        /// <summary>
        /// 写cookie值
        /// </summary>
        public static void SetCookie(string strName, string strValue)
        {
            SetCookie(strName, strValue, 0);
        }
        public static void SetCookie(string strName, string strValue, int expires)
        {
            SetCookie(strName, strValue, expires, "/", true);
        }
        public static void SetCookie(string strName, string strValue, int expires, string strPath, bool isHttpOnly)
        {
            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
                if (cookie == null)
                    cookie = new HttpCookie(strName);

                cookie.Value = strValue;
                if (expires > 0)
                    cookie.Expires = DateTime.Now.AddSeconds(expires);
                cookie.Path = strPath;
                cookie.HttpOnly = isHttpOnly;

                if (cookie == null)
                    HttpContext.Current.Response.SetCookie(cookie);
                else
                    HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.Request.Cookies != null &&
                HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <param name="name">name of cookie</param>
        public static void RemoveCookie(string name)
        {
            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
                if (cookie != null)
                {
                    HttpContext.Current.Request.Cookies.Remove(name);
                }
            }
        }
    }
}