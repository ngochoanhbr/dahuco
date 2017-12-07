using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Data;

namespace SinGooCMS.WebUI.User
{
    public class MyFavorite : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.ReturnUrl = base.Request.RawUrl;
			int queryInt = WebUtils.GetQueryInt("id");
			if (this.Action.Equals("cancel"))
			{
				FavoritesInfo dataById = Favorites.GetDataById(queryInt);
				if (dataById != null)
				{
					Favorites.Delete(queryInt);
				}
				base.Response.Redirect(UrlRewrite.Get("myfavorite_url"));
			}
			this.PageSize = 12;
			this.Condition = "1=1 AND UserID=" + base.UserID.ToString();
			this.UrlPattern = UrlRewrite.Get("myfavorite_url") + "?page=$page";
			DataSet pagerDataExt = Favorites.GetPagerDataExt(" * ", this.Condition, this.Sort, this.PageSize, this.PageIndex, ref this.TotalCount, ref this.TotalPage);
			DataTable dataTable = (pagerDataExt != null && pagerDataExt.Tables.Count > 0) ? pagerDataExt.Tables[0] : new DataTable();
			CMSPager pager = this.contents.GetPager(this.TotalCount, this.PageIndex, this.PageSize, this.UrlPattern);
			base.Put("pager", pager);
			base.Put("jcdatas", dataTable.Rows);
			base.Put("condition", this.Condition);
			base.UsingClient("user/我的收藏.html");
		}
	}
}
