using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
    public partial class CreateFileInfo
    {
        private List<CustomVariable> _VarList = new List<CustomVariable>();
        public List<CustomVariable> VarList
        {
            get
            {
                return this._VarList;
            }
            set
            {
                this._VarList = value;
            }
        }
        private string _FileUrl = string.Empty;
        public string FileUrl
        {
            get
            {
                if (string.IsNullOrEmpty(this._FileUrl))
                {
                    this._FileUrl =ResolveUrl(this._FileUrl) + this._FileName;
                    this._FileUrl = this._FileUrl.Replace("{page}", "1");
                }
                return this._FileUrl;
            }
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
                {
                    return (System.Web.HttpContext.Current.Request.ApplicationPath + url).Replace("//", "/");
                }
                if (url.StartsWith("~/"))
                {
                    return url.Replace("~/", System.Web.HttpContext.Current.Request.ApplicationPath);
                }
            }
            return url;
        }
    }
}
