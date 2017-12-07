using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class ChangePwd : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			if (base.IsPost)
			{
				string formString = WebUtils.GetFormString("_oldpwd");
				string formString2 = WebUtils.GetFormString("_newpwd");
				string formString3 = WebUtils.GetFormString("_newpwdconfirm");
				if (string.IsNullOrEmpty(formString))
				{
					base.WriteJsonTip(base.GetCaption("ChangePwd_OldPwdNotEmpty"));
				}
				else if (string.IsNullOrEmpty(formString2))
				{
					base.WriteJsonTip(base.GetCaption("ChangePwd_NewPwdNotEmpty"));
				}
				else if (!formString2.Equals(formString3))
				{
					base.WriteJsonTip(base.GetCaption("ChangePwd_2PwdInputNoMatch"));
				}
				else if (base.LoginUser.Password != SinGooCMS.BLL.User.GetEncodePwd(formString))
				{
					base.WriteJsonTip(base.GetCaption("ChangePwd_OldPwdIncorrect"));
				}
				else if (SinGooCMS.BLL.User.UpdatePassword(base.UserID, formString2))
				{
					new MsgService(SinGooCMS.BLL.User.GetDataById(base.UserID)).SendChangPwdMsg();
					base.WriteJsonTip(true, base.GetCaption("ChangePwd_ModifyPwdSuccess"), UrlRewrite.Get("logout_url"));
				}
				else
				{
					base.WriteJsonTip(base.GetCaption("ChangePwd_ModifyPwdFail"));
				}
			}
			else
			{
				base.UsingClient("user/修改密码.html");
			}
		}
	}
}
