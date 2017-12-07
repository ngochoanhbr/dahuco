using System;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class SelectNode : H5ManagerPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.NeedAuthorized = false;
		}
	}
}
