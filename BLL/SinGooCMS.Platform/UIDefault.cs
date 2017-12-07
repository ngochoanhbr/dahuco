using SinGooCMS;

using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;

public class UIDefault : SinGooCMS.BLL.Custom.UIPageBase
{
	protected void Page_Load(object sender, System.EventArgs e)
	{
        base.NeedLogin = true;
		if (WebUtils.IsMobileVisit() && PageBase.config.EnabledMobile)
		{
			base.Response.Redirect("/Wap.aspx");
		}
		else
		{
			base.SetPcClient();
			if (PageBase.config.BrowseType == BrowseType.Html.ToString())
			{
				base.Response.Redirect("~/Html");
			}
			else
			{
				base.Using(PageBase.defaultTemplate.HomePage);
			}
		}
	}
}
