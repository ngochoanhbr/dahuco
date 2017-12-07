using SinGooCMS.BLL;

using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI
{
    public class Search : SinGooCMS.BLL.Custom.UIPageBase
	{
		private string strCondition = " Status=99 ";

		private string strUrlPattern = "/Search.aspx";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string queryString = WebUtils.GetQueryString("key");
			NodeInfo cacheNodeById = SinGooCMS.BLL.Node.GetCacheNodeById(WebUtils.GetQueryInt("nid"));
			int appSetting = ConfigUtils.GetAppSetting<int>("SearchPageSize", 10);
			this.strUrlPattern = this.strUrlPattern + "?key=" + queryString;
			this.strCondition = this.strCondition + " AND Title LIKE '%" + StringUtils.ChkSQL(queryString) + "%' ";
			if (cacheNodeById != null)
			{
				this.strUrlPattern = this.strUrlPattern + "&nid=" + cacheNodeById.AutoID;
				this.strCondition = this.strCondition + " AND NodeID in (" + cacheNodeById.ChildList + ") ";
			}
			this.strUrlPattern += "&page=$page";
			CMSPager pager = this.contents.GetPager(this.contents.GetCount(this.strCondition), this.intCurrentPage, appSetting, this.strUrlPattern);
			base.Put("searchconts", this.contents.GetContents(0, this.strCondition, "Sort asc,AutoID desc", this.intCurrentPage, appSetting));
			base.Put("pager", pager);
			base.UsingClient("搜索.html");
		}
	}
}
