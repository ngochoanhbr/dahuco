using SinGooCMS.BLL;

using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI
{
    public class TuiDing : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DingYueInfo byEmail = DingYue.GetByEmail(WebUtils.GetQueryString("email"));
			if (byEmail != null)
			{
				byEmail.IsTuiDing = true;
				if (DingYue.Update(byEmail))
				{
					base.Response.Write("已取消订阅！");
				}
				else
				{
					base.Response.Write("Thao tác thất bại！");
				}
			}
			else
			{
				base.Response.Write("无效的邮件！");
			}
		}
	}
}
