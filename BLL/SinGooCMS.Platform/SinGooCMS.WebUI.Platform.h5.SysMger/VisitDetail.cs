using SinGooCMS.BLL;
using SinGooCMS.Entity;
using System;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class VisitDetail : H5ManagerPageBase
	{
		public VisitorInfo VInfo = new VisitorInfo();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.VInfo = Visitor.GetDataById(base.OpID);
		}
	}
}
