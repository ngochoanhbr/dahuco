
using System;

namespace SinGooCMS.WebUI.User
{
    public class ResetSuccess : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.UsingClient("user/重置密码结果.html");
		}
	}
}
