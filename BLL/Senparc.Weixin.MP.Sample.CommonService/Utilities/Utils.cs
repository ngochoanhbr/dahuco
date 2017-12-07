using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SinGooCMS.Config;

namespace Senparc.Weixin
{
    public class Utils
    {
        /// <summary>
        /// 获取带域名的全路径 如 http://www.sz3w.net/Upload/2015/7c/20150722109687678.jpg
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetUrlWithDomain(string strPath)
        {
            if (!strPath.StartsWith("http://") && !strPath.StartsWith("https://"))
            {
                string strDomain = ConfigProvider.Configs.SiteDomain;
                if (!strDomain.EndsWith("/"))
                    strDomain = strDomain + "/";

                return strDomain + strPath.Replace("//", "/").TrimStart('/');
            }

            return string.Empty;
        }
    }
}
