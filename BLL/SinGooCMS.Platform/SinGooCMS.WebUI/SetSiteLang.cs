using SinGooCMS.BLL;

using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI
{
    public class SetSiteLang : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string queryString = WebUtils.GetQueryString("lang");
			if (Language.Contain(queryString))
			{
				CookieUtils.SetCookie("langcookie", queryString, 2592000);
				string queryString2 = WebUtils.GetQueryString("jumpurl");
				if (!string.IsNullOrEmpty(queryString2))
				{
					base.Response.Redirect(queryString2);
				}
				else if (base.Request.UrlReferrer != null)
				{
					base.Response.Redirect(base.Request.UrlReferrer.ToString());
				}
				else
				{
					base.Response.Redirect("/");
				}
			}
			else
			{
				base.Response.Write(WebUtils.GetCaption("CMS_NotExistLanguageSet"));
				base.Response.End();
			}
		}
	}
}
