using SinGooCMS.BLL;
using SinGooCMS.Common;
using System;
using System.Data;

namespace SinGooCMS.WebUI.User
{
    public class Coupons : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.ReturnUrl = base.Request.RawUrl;
			this.PageSize = 12;
			this.Condition = " 1=1 AND UserName='" + base.UserName + "' AND IsUsed=0 AND GETDATE()<EndTime ";
			this.UrlPattern = UrlRewrite.Get("coupons_url") + "?page=$page";
			DataSet pagerData = SinGooCMS.BLL.Coupons.GetPagerData("*,(case when DATEDIFF(HOUR,GETDATE(),EndTime) between 0 and 24 then 1 else 0 end) as willexpire", this.Condition, this.Sort, this.PageSize, this.PageIndex, ref this.TotalCount, ref this.TotalPage);
			DataTable dataTable = (pagerData != null && pagerData.Tables.Count > 0) ? pagerData.Tables[0] : new DataTable();
			CMSPager pager = this.contents.GetPager(this.TotalCount, this.PageIndex, this.PageSize, this.UrlPattern);
			base.Put("pager", pager);
			base.Put("jcdatas", dataTable.Rows);
			base.Put("condition", this.Condition);
			base.UsingClient("user/优惠券.html");
		}
	}
}
