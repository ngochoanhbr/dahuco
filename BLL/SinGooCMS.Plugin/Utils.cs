using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SinGooCMS.Config;

namespace SinGooCMS.Plugin
{
    public class Utils
    {
        /// <summary>
        /// 获取带域名的全路径 如 http://www.sz3w.net/Upload/2015/7c/20150722109687678.jpg
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static string GetUrlWithDomain(string strUrl)
        {
            if (!strUrl.StartsWith("http://") && !strUrl.StartsWith("https://"))
            {
                string strDomain = ConfigProvider.Configs.SiteDomain;
                if (!strDomain.EndsWith("/"))
                    strDomain = strDomain + "/";

                return strDomain + strUrl.Replace("//", "/").TrimStart('/');
            }

            return string.Empty;
        }
        /// <summary>
        /// 创建一个付款的请求表单，并立即提交
        /// </summary>
        /// <param name="dicPara"></param>
        /// <param name="strMethod"></param>
        /// <param name="strButtonValue"></param>
        /// <param name="strGATEWAY"></param>
        /// <returns></returns>
        public static string BuildRequest(Dictionary<string, string> dicPara, string strMethod, string strButtonValue, string strGATEWAY)
        {
            StringBuilder sbHtml = new StringBuilder();

            string payTempID = SinGooCMS.Utility.StringUtils.GetNewFileName();
            sbHtml.Append("<form id='" + payTempID + "' name='" + payTempID + "' action='" + strGATEWAY + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            sbHtml.Append("<script>document.getElementById('" + payTempID + "').submit();</script>");

            return sbHtml.ToString();
        }
    }
}
