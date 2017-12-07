
using System;

namespace SinGooCMS.WebUI.User
{
    public class Agreement : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.UsingClient("user/注册协议.html");
		}
	}
}
