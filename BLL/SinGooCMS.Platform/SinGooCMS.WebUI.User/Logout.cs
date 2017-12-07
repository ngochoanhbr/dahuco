using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.Web;
using System.Web.UI;

namespace SinGooCMS.WebUI.User
{
	public class Logout : Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string text = WebUtils.GetQueryString("tourl");
			if (string.IsNullOrEmpty(text))
			{
				text = UrlRewrite.Get("login_url");
			}
			HttpCookie httpCookie = new HttpCookie("singoouser");
			httpCookie.Expires = System.DateTime.Now.AddDays(-1.0);
			HttpContext.Current.Response.SetCookie(httpCookie);
			HttpContext.Current.Response.Redirect(text);
		}
	}
}
