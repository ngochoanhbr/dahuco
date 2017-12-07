using SinGooCMS.BLL;

using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class ResetPwd : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsPost)
			{
				UserInfo userInfo = new UserInfo();
				userInfo = SinGooCMS.BLL.User.GetUserByName(WebUtils.GetFormString("_uname"));
				string formString = WebUtils.GetFormString("_findpwdtype");
				string strMobile = string.Empty;
				if (formString.Equals("bymobile"))
				{
					strMobile = userInfo.Mobile;
				}
				else
				{
					strMobile = userInfo.Email;
				}
				string formString2 = WebUtils.GetFormString("_newpwd");
				SMSInfo lastCheckCode = SMS.GetLastCheckCode(strMobile);
				if (lastCheckCode == null)
				{
					base.WriteJsonTip(base.GetCaption("GetPwd_NoSendMobileValidateCodeYet"));
				}
				else if (string.Compare(WebUtils.GetFormString("_fourcode"), lastCheckCode.ValidateCode, true) != 0)
				{
					base.WriteJsonTip(base.GetCaption("GetPwd_MobileValidateCodeIncorrect"));
				}
				else if (string.IsNullOrEmpty(formString2))
				{
					base.WriteJsonTip(base.GetCaption("GetPwd_NewPwdNotEmpty"));
				}
				else if (formString2.Length < 6)
				{
					base.WriteJsonTip(base.GetCaption("GetPwd_NewPwdLenCannotLess6"));
				}
				else if (SinGooCMS.BLL.User.UpdatePassword(userInfo.AutoID, formString2))
				{
					new MsgService(userInfo).SendFindPwdMsg();
					base.WriteJsonTip(true, base.GetCaption("ResetPwd_Success"), UrlRewrite.Get("resetsuccess_url"));
				}
				else
				{
					base.WriteJsonTip(base.GetCaption("GetPwd_PasswordResetFailed"));
				}
			}
			else
			{
				UserInfo userInfo = new UserInfo();
				int intPrimaryKeyIDValue = 0;
				try
				{
					intPrimaryKeyIDValue = WebUtils.GetInt(DEncryptUtils.DESDecode(WebUtils.GetQueryString("uid")));
				}
				catch
				{
					intPrimaryKeyIDValue = 0;
				}
				userInfo = SinGooCMS.BLL.User.GetDataById(intPrimaryKeyIDValue);
				base.Put("curruser", userInfo);
				base.Put("useremail", string.IsNullOrEmpty(userInfo.Email) ? "没有绑定邮箱" : StringUtils.GetAnonymous(userInfo.Email));
				base.Put("usermobile", string.IsNullOrEmpty(userInfo.Mobile) ? "没有绑定手机" : StringUtils.GetAnonymous(userInfo.Mobile));
				base.UsingClient("user/找回密码方式.html");
			}
		}
	}
}
