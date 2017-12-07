using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MarketMger
{
	public class Promotion : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected LinkButton btn_DelBat;

		protected LinkButton btn_SaveSort;

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
			string strSort = " AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.Promotion.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
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
				text = text + " AND ActName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				PromotionInfo dataById = SinGooCMS.BLL.Promotion.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else if (SinGooCMS.BLL.Promotion.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除优惠促销活动[" + dataById.ActName + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					if (SinGooCMS.BLL.Promotion.Delete(repeaterCheckIDs))
					{
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量删除优惠促销活动成功");
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (SinGooCMS.BLL.Promotion.UpdateSort(repeaterSortDict))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新优惠折扣活动排序成功");
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
