using System;

using SinGooCMS.Config;

namespace SinGooCMS.Entity
{
    public partial class ContArchives
    {
        //归档URL
        public string ArchivesUrl
        {
            get
            {
                return GetArchivesUrl();
            }
        }
        /// <summary>
        /// 获取栏目URL路径
        /// </summary>
        /// <returns></returns>
        private string GetArchivesUrl()
        {
            string url = "/Archives.aspx?ym=" + this.ArchivesDate;
            if (Config.ConfigProvider.Configs.BrowseType == BrowseType.Html.ToString())
            {
                url = ("/Html/Archives/" + this.ArchivesDate + "/index" + ConfigProvider.Configs.HtmlFileExt).Replace("//", "/");
            }
            else if (Config.ConfigProvider.Configs.BrowseType == BrowseType.UrlRewriteAndAspx.ToString())
            {
                url = "/Archives/" + this.ArchivesDate + "/Default.aspx".Replace("//", "/");
            }
            else if (Config.ConfigProvider.Configs.BrowseType == BrowseType.UrlRewriteNoAspx.ToString())
            {
                url = "/Archives/" + this.ArchivesDate + "/Default".Replace("//", "/");
            }

            return url;
        }
        //日期
        public string ArchivesDate { get; set; }
        //文章数量
        public int ArchivesNum { get; set; }
    }
}
