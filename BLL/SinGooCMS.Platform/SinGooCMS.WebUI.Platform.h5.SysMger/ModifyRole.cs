using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class ModifyRole : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			RoleInfo dataById = SinGooCMS.BLL.Role.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.RoleName;
			if (dataById.IsSystem)
			{
				this.TextBox1.Enabled = false;
			}
			this.TextBox2.Text = dataById.Remark;
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				RoleInfo roleInfo = new RoleInfo();
				if (base.IsEdit)
				{
					roleInfo = SinGooCMS.BLL.Role.GetDataById(base.OpID);
				}
				roleInfo.RoleName = WebUtils.GetString(this.TextBox1.Text);
				roleInfo.Remark = WebUtils.GetString(this.TextBox2.Text);
				if (string.IsNullOrEmpty(roleInfo.RoleName))
				{
					base.ShowMsg("角色名称不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						roleInfo.IsSystem = false;
						roleInfo.Sort = SinGooCMS.BLL.Role.MaxSort + 1;
						roleInfo.AutoTimeStamp = System.DateTime.Now;
						if (SinGooCMS.BLL.Role.Add(roleInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加角色[" + roleInfo.RoleName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加角色失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.Role.Update(roleInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改角色[" + roleInfo.RoleName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改角色失败");
						}
					}
				}
			}
		}
	}
}
