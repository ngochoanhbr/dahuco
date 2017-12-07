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
	public class Role : H5ManagerPageBase
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
			string strSort = "Sort Asc,AutoID desc ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.Role.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
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
				text = text + " AND RoleName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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
				RoleInfo dataById = SinGooCMS.BLL.Role.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không tìm thấy dữ liệu, dữ liệu không tồn tại hoặc đã bị xóa ");
				}
				else if (dataById.IsSystem)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "系统角色不可删除");
				}
				else
				{
					RoleStatus roleStatus = SinGooCMS.BLL.Role.Delete(dataById.AutoID);
					if (roleStatus == RoleStatus.RefByAccount)
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "角色被帐户所引用,无法删除");
					}
					else if (roleStatus == RoleStatus.Error)
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "删除失败,发生了不可预料的错误");
					}
					else
					{
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除角色[" + dataById.RoleName + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "删除角色成功");
					}
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
