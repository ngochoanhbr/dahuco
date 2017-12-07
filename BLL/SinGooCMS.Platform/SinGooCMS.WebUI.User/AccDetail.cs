
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class AccDetail : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.ReturnUrl = base.Request.RawUrl;
			string text = WebUtils.GetQueryString("opid");
			if (!string.IsNullOrEmpty(text))
			{
				text = text.TrimEnd(new char[]
				{
					','
				});
				string action = this.Action;
				if (action != null)
				{
					if (action == "delete")
					{
						if (PageBase.dbo.UpdateTable(" delete from cms_AccountDetail where AutoID in (" + text + ") "))
						{
							base.Response.Redirect(UrlRewrite.Get("accdetail_url"));
						}
					}
				}
			}
			this.PageSize = 15;
			this.Condition = " 1=1 AND Unit='Amount' and UserID= " + base.UserID.ToString();
			this.UrlPattern = UrlRewrite.Get("accdetail_url") + "?page=$page";
			base.AutoPageing<AccountDetailInfo>(new AccountDetailInfo());
			base.UsingClient("user/账户明细.html");
		}
	}
}
