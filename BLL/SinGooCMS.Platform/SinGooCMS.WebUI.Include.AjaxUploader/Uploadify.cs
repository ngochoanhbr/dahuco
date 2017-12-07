using SinGooCMS.Common;
using System;

namespace SinGooCMS.WebUI.Include.AjaxUploader
{
	public class Uploadify : ManagerPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedAuthorized = false;
		}
	}
}
