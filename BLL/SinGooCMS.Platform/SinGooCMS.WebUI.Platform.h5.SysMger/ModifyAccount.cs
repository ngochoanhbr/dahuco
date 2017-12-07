using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class ModifyAccount : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected H5TextBox TextBox3;

		protected TextBox TextBox4;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsEdit)
			{
				this.TextBox2.Attributes.Add("required", "required");
			}
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			AccountInfo dataById = SinGooCMS.BLL.Account.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.AccountName;
			if (dataById.AccountName == "superadmin")
			{
				this.TextBox1.Enabled = false;
			}
			this.TextBox2.Text = string.Empty;
			this.TextBox3.Text = dataById.Email;
			this.TextBox4.Text = dataById.Mobile;
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
				AccountInfo accountInfo = new AccountInfo();
				if (base.IsEdit)
				{
					accountInfo = SinGooCMS.BLL.Account.GetDataById(base.OpID);
				}
				string @string = WebUtils.GetString(this.TextBox2.Text);
				if (accountInfo.AccountName != "superadmin")
				{
					accountInfo.AccountName = WebUtils.GetString(this.TextBox1.Text);
				}
				accountInfo.Email = WebUtils.GetString(this.TextBox3.Text);
				accountInfo.Mobile = WebUtils.GetString(this.TextBox4.Text);
				accountInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(accountInfo.AccountName))
				{
					base.ShowMsg("帐户名称不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (string.IsNullOrEmpty(@string))
						{
							base.ShowMsg("帐户密码不为空");
							return;
						}
						accountInfo.Password = DEncryptUtils.SHA512Encrypt(@string);
						accountInfo.IsSystem = false;
						if (SinGooCMS.BLL.Account.Add(accountInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加角色[" + accountInfo.AccountName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加角色失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (!string.IsNullOrEmpty(@string))
						{
							accountInfo.Password = DEncryptUtils.SHA512Encrypt(@string);
						}
						if (SinGooCMS.BLL.Account.Update(accountInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改角色[" + accountInfo.AccountName + "] thành công");
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
