using SinGooCMS.BLL;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Selector
{
	public class GoodsForActSelect : H5ManagerPageBase
	{
		public string SelectType = "single";

		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected SinGooPager SinGooPager1;

		protected Repeater Repeater1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedAuthorized = false;
			this.SelectType = WebUtils.GetQueryString("type").ToLower();
			this.BindData();
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = "AutoID DESC";
			this.Repeater1.DataSource = Product.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND ProductName like '%" + this.search_text.Text + "%' ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}
	}
}
