using SinGooCMS.Plugin.VerifyCode;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace SinGooCMS.WebUI
{
	public class CheckCodeImg : Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			IVerifyCode verifyCode = VerifyCodeProvider.Create(new System.Collections.Generic.Dictionary<int, string>
			{
				{
					1,
					"ImageCode"
				},
				{
					2,
					"VerifyCode"
				},
				{
					3,
					"VerifyCodePlus"
				}
			}[WebUtils.GetQueryInt("style", 1)]);
			verifyCode.CheckCodeType = VerifyCodeType.Web;
			verifyCode.CreateCheckCodeImage();
		}
	}
}
