using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.WeixinMger
{
	public class MessageKeyRly : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

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
			this.Repeater1.DataSource = AutoRly.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 AND RlyType='关键字回复' ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND ( MsgKey like '%" + WebUtils.GetString(this.search_text.Text) + "%' ) ";
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
				AutoRlyInfo dataById = AutoRly.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else if (AutoRly.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除自动回复关键字[" + dataById.MsgKey + "] thành công");
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
					if (AutoRly.Delete(repeaterCheckIDs))
					{
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量删除自动回复关键字成功");
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
}
