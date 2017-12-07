
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class Integral : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.ReturnUrl = base.Request.RawUrl;
			string text = WebUtils.GetQueryString("opid").TrimEnd(new char[]
			{
				','
			});
			if (!string.IsNullOrEmpty(text))
			{
				string action = this.Action;
				if (action != null)
				{
					if (action == "delete")
					{
						if (PageBase.dbo.UpdateTable(" delete from cms_AccountDetail where AutoID in (" + text + ") "))
						{
							base.Response.Redirect("/User/Integral.aspx");
						}
					}
				}
			}
			this.PageSize = 15;
			this.Condition = " 1=1 AND Unit='Integral' and UserID= " + base.UserID.ToString();
			this.UrlPattern = UrlRewrite.Get("integral_url") + "?page=$page";
			base.AutoPageing<AccountDetailInfo>(new AccountDetailInfo());
			base.UsingClient("user/我的积分.html");
		}
	}
}
