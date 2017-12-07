using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.StatMger
{
	public class TraAnaly : H5ManagerPageBase
	{
		protected TextBox timestart;

		protected TextBox timeend;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.timestart.Text = System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
			this.timeend.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
		}
	}
}
