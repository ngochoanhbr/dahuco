using System;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class GoodsPulish : H5ManagerPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.NeedAuthorized = true;
		}
	}
}
