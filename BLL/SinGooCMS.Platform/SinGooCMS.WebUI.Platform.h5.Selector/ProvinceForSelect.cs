using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Selector
{
	public class ProvinceForSelect : H5ManagerPageBase
	{
		protected Repeater Repeater1;

		public string SelectType = "single";

		public string Original_Data = string.Empty;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = false;
			base.NeedAuthorized = false;
			this.SelectType = WebUtils.GetQueryString("type", "single").ToLower();
			this.Original_Data = WebUtils.GetQueryString("original_data");
			this.BindData();
		}

		private void BindData()
		{
			System.Collections.Generic.IList<ZoneInfo> dataSource = (from p in Zone.GetProvinceList()
			orderby p.AutoID
			select p).ToList<ZoneInfo>();
			this.Repeater1.DataSource = dataSource;
			this.Repeater1.DataBind();
		}
	}
}
