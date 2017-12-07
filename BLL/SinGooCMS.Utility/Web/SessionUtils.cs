using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SinGooCMS.Utility
{
    public class SessionUtils
    {
        /// <summary>
        /// 写入Session值 默认20分钟
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="value"></param>
        public static void SetSession(string strName, object value)
        {
            HttpContext.Current.Session[strName] = value;
        }

        /// <summary>
        /// 写入Session值
        /// </summary>
        /// <param name="strName">键</param>
        /// <param name="value">值</param>
        /// <param name="time">超时时间</param>
        public static void SetSession(string strName, object value, int time)
        {
            HttpContext.Current.Session[strName] = value;
            HttpContext.Current.Session.Timeout = time;
        }

        /// <summary>
        /// 读Session值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>Session值</returns>
        public static object GetSession(string strName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[strName] != null)
                return HttpContext.Current.Session[strName];
            else
                return null;
        }
        public static string GetSessionString(string strName)
        {
            if (GetSession(strName) != null)
                return GetSession(strName).ToString();
            else
                return string.Empty;
        }
        /// <summary>
        /// 删除Session
        /// </summary>
        /// <param name="strName"></param>
        public static void DelSession(string strName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[strName] != null)
            {
                HttpContext.Current.Session[strName] = null;
            }
        }
    }
}
