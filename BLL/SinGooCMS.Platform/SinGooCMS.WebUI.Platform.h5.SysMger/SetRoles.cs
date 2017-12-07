using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class SetRoles : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected Button btnok;

		public AccountInfo account = new AccountInfo();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.account = SinGooCMS.BLL.Account.GetDataById(base.OpID);
			if (this.account == null)
			{
				base.ShowMsg("账户不存在");
			}
			else if (this.account.AccountName == "superadmin")
			{
				base.ShowMsg("不能设置超级管理员的角色");
			}
			else if (!base.IsPost)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " AutoID ASC ";
			this.Repeater1.DataSource = SinGooCMS.BLL.Role.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			return " 1=1 ";
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				CheckBox checkBox = e.Item.FindControl("checkbox") as CheckBox;
				HiddenField hiddenField = e.Item.FindControl("autoid") as HiddenField;
				if (this.account.Roles.Split(new char[]
				{
					','
				}).Contains(hiddenField.Value))
				{
					checkBox.Checked = true;
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("SetRole"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				this.account.Roles = base.GetRepeaterCheckIDs(this.Repeater1, "checkbox", "autoid");
				if (SinGooCMS.BLL.Account.Update(this.account))
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置帐户[" + this.account.AccountName + "]的角色成功");
					MessageUtils.DialogCloseAndParentReload(this);
				}
				else
				{
					base.ShowMsg("Thao tác thất bại");
				}
			}
		}
	}
}
