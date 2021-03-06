using SinGooCMS.BLL;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.StatMger
{
	public class UserBuyRate : H5ManagerPageBase
	{
		protected Repeater Repeater1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int num = 0;
			int num2 = 0;
            string strSort = " Purchasespend DESC ";
			this.Repeater1.DataSource = Stat.GetUserBuyRate(" *", this.GetCondition(), strSort, 50, 1, ref num, ref num2);
			this.Repeater1.DataBind();
		}

		private string GetCondition()
		{
			return " 1=1 ";
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}
	}
}
