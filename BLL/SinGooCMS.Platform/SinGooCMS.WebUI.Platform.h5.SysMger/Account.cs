using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class Account : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

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
			string strSort = " AutoID desc ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.Account.GetPagerData("*,LastLoginTime=(select MAX(AutoTimeStamp) from sys_LoginLog where UserType='manager' and UserName=sys_Account.AccountName) ", this.GetCondition(), strSort, this.SinGooPager1.PageSize, this.SinGooPager1.PageIndex, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SinGooPager1.PageIndex = 1;
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND AccountName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.H5Tip(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				AccountInfo dataById = SinGooCMS.BLL.Account.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không tìm thấy dữ liệu, dữ liệu không tồn tại hoặc đã bị xóa ");
				}
				else if (base.LoginAccount.AutoID == dataById.AutoID)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "当前帐户正在登录,不可删除");
				}
				else if (dataById.IsSystem)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "系统帐户不可删除");
				}
				else if (string.Compare(dataById.AccountName, "superadmin", true) == 0)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "超级管理员(superadmin)不可删除");
				}
				else if (SinGooCMS.BLL.Account.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除管理帐户[" + dataById.AccountName + "] thành công");
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
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
		}
	}
}
