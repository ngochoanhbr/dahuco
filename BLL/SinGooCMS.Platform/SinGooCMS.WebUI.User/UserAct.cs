using SinGooCMS.BLL;

using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class UserAct : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string value = "fail";
			string text = WebUtils.GetQueryString("key");
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					text = DEncryptUtils.DESDecode(text);
				}
				catch
				{
					text = string.Empty;
				}
				UserInfo userByName = SinGooCMS.BLL.User.GetUserByName(text);
				if (userByName == null)
				{
					this.Alert(base.GetCaption("UserAct_MemberNotExistOrDeleted"), "/");
				}
				else
				{
					userByName.Status = 99;
					if (SinGooCMS.BLL.User.Update(userByName))
					{
						value = "success";
					}
				}
			}
			else
			{
				this.Alert(base.GetCaption("UserAct_InvalidParameter"));
			}
			base.Put("actresult", value);
			base.UsingClient("user/会员激活.html");
		}
	}
}
