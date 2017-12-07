using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class SeoSet : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected Button btn_SetKey;

		protected Button btn_SetDesc;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " Sort ASC,AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
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
				text = text + " AND ProductName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SinGooPager1.PageIndex = 1;
			this.BindData();
		}

		protected void btn_SetKey_Click(object sender, System.EventArgs e)
		{
			string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
			if (!string.IsNullOrEmpty(repeaterCheckIDs))
			{
				if (PageBase.dbo.UpdateTable(" update shop_Product set SEOKey=ProductName where AutoID in (" + repeaterCheckIDs + ") "))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置商品（ID:" + repeaterCheckIDs + "）搜索关键字为商品名称");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_SetDesc_Click(object sender, System.EventArgs e)
		{
			string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
			if (!string.IsNullOrEmpty(repeaterCheckIDs))
			{
				if (PageBase.dbo.UpdateTable(" update shop_Product set SEODescription=ProductName where AutoID in (" + repeaterCheckIDs + ") "))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置商品（ID:" + repeaterCheckIDs + "）搜索描述为商品名称");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}
	}
}
