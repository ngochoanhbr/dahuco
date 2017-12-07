using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin.ThirdLogin;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class Login : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsPost)
			{
				UserInfo userInfo = new UserInfo();
				string formString = WebUtils.GetFormString("_loginname");
				string formString2 = WebUtils.GetFormString("_loginpwd");
				bool flag = WebUtils.GetFormInt("_loginremeber").Equals(1);
				if (PageBase.config.VerifycodeForLogin && string.Compare(base.ValidateCode, WebUtils.GetFormString("_loginyzm"), true) != 0)
				{
					base.WriteJsonTip(base.GetCaption("ValidateCodeIncorrect"));
				}
				else
				{
					LoginStatus loginStatus = SinGooCMS.BLL.User.UserLogin(formString, formString2, ref userInfo);
					if (loginStatus == LoginStatus.Success)
					{
						if (flag)
						{
							CookieUtils.SetCookie("_remeberusername", userInfo.UserName, 31536000);
						}
						string text = base.Server.UrlDecode(WebUtils.GetFormString("_loginreturnurl"));
						if (!string.IsNullOrEmpty(text))
						{
							base.WriteJsonTip(true, "Đăng nhập thành công", text);
						}
						else
						{
                            base.WriteJsonTip(true, "Đăng nhập thành công", UrlRewrite.Get("infocenter_url"));
						}
					}
					else if (loginStatus == LoginStatus.MutilLoginFail)
					{
						base.WriteJsonTip(base.GetCaption("Login_LoginFailTooMany").Replace("${num}", PageBase.config.TryLoginTimes.ToString()));
					}
					else
					{
						base.WriteJsonTip(base.GetCaption("Login_FailWithMsg").Replace("${msg}", base.GetCaption("LoginStatus_" + loginStatus.ToString())));
					}
				}
			}
			else
			{
				base.Put("remeberusername", CookieUtils.GetCookie("_remeberusername"));
				base.Put("returnurl", (base.Request.Url.ToString().IndexOf("?returnurl=") == -1) ? "" : base.Request.Url.ToString().Substring(base.Request.Url.ToString().IndexOf("?returnurl=") + "?returnurl=".Length));
				base.Put("thirdlogin", OAuthConfig.Load());
				base.UsingClient("user/login.html");
			}
		}
	}
}
