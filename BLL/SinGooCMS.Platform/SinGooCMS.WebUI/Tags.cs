
using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.Web;

namespace SinGooCMS.WebUI
{
    public class Tags : SinGooCMS.BLL.Custom.UIPageBase
	{
		private string strCondition = " Status=99 ";

		private string strUrlPattern = "/Tags.aspx";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string queryString = WebUtils.GetQueryString("key");
			string queryString2 = WebUtils.GetQueryString("lang");
			this.strUrlPattern = this.strUrlPattern + "?key=" + HttpUtility.UrlEncode(queryString);
			if (!string.IsNullOrEmpty(queryString2))
			{
				this.strUrlPattern = this.strUrlPattern + "&lang=" + queryString2;
			}
			this.strUrlPattern += "&page=$page";
			if (!string.IsNullOrEmpty(queryString))
			{
				this.strCondition = this.strCondition + " AND charindex('," + StringUtils.ChkSQL(queryString) + ",',','+TagKey+',')>0 ";
			}
			CMSPager pager = this.contents.GetPager(this.contents.GetCount(this.strCondition), this.intCurrentPage, 10, this.strUrlPattern);
			base.Put("contlist", this.contents.GetContents(0, this.strCondition, "Sort asc,AutoID desc", this.intCurrentPage, 10));
			base.Put("pager", pager);
			base.UsingClient("标签.html");
		}
	}
}
