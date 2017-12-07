using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class FindPwd : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsPost)
			{
				UserInfo userInfo = new UserInfo();
				userInfo = SinGooCMS.BLL.User.GetUserByName(WebUtils.GetFormString("_uname"));
				if (userInfo == null)
				{
					base.WriteJsonTip(base.GetCaption("GetPwd_UserNotExist"));
				}
				else
				{
					base.WriteJsonTip(true, "用户名正确", UrlRewrite.Get("resetpwd_url") + "?uid=" + DEncryptUtils.DESEncode(userInfo.AutoID.ToString()));
				}
			}
			else
			{
				base.UsingClient("user/找回密码.html");
			}
		}
	}
}
