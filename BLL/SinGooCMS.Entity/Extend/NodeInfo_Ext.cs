using System;

using SinGooCMS.Config;

namespace SinGooCMS.Entity
{
    public partial class NodeInfo
    {

        public string GetAspxUrl()
        {
            string text = "/article/node.aspx?nid=" + this.AutoID.ToString();
            BrowseType browseType = (BrowseType)Enum.Parse(typeof(BrowseType), ConfigProvider.Configs.BrowseType);
            string str = string.IsNullOrEmpty(this.UrlRewriteName) ? this.AutoID.ToString() : this.UrlRewriteName;
            switch (browseType)
            {
                case BrowseType.UrlRewriteAndAspx:
                    text = "/article/" + str + ".aspx";
                    break;
                case BrowseType.UrlRewriteNoAspx:
                    text = "/article/" + str;
                    break;
                case BrowseType.HtmlRewrite:
                    text = "/article/" + str + ".html";
                    break;
            }
            return text.Replace("//", "/");
        }

        private string GetNodeUrl()
        {
            string result = "/article/node.aspx?nid=" + this.AutoID.ToString();
            string str = string.IsNullOrEmpty(this.UrlRewriteName) ? this.AutoID.ToString() : this.UrlRewriteName;
            if (this._CustomLink.Length > 0)
            {
                result = this._CustomLink;
            }
            else if (ConfigProvider.Configs.BrowseType == BrowseType.Html.ToString())
            {
                result = ("/html/article/" + str + ConfigProvider.Configs.HtmlFileExt).Replace("//", "/");
            }
            else
            {
                result = this.GetAspxUrl();
            }
            return result;
        }
    }
}