
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class Message : SinGooCMS.BLL.Custom.UIPageBase
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
					if (!(action == "read"))
					{
						if (action == "delete")
						{
							if (PageBase.dbo.UpdateTable(" delete from sys_Message where AutoID in (" + text + ") "))
							{
								base.Response.Redirect(UrlRewrite.Get("message_url"));
							}
						}
					}
					else if (PageBase.dbo.UpdateTable(" update sys_Message set IsRead=1,ReadTime=getdate() where AutoID in (" + text + ") "))
					{
						base.Response.Redirect(UrlRewrite.Get("message_url"));
					}
				}
			}
			this.Condition = "1=1 AND ReceiverType='user' AND Receiver='" + base.UserName + "' ";
			this.UrlPattern = UrlRewrite.Get("message_url") + "?page=$page";
			base.AutoPageing<MessageInfo>(new MessageInfo());
			base.UsingClient("user/我的消息.html");
		}
	}
}
