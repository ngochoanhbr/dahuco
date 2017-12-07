using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI;

namespace SinGooCMS.WebUI.Log
{
	public class ShowLogDetail : Page
	{
		public VisitorInfo error = new VisitorInfo();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.error = Visitor.GetDataById(WebUtils.GetQueryInt("opid"));
		}
	}
}
