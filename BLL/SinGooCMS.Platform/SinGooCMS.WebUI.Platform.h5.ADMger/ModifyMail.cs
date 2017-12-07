using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class ModifyMail : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected CheckBox istuiding;

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
			DingYueInfo dataById = SinGooCMS.BLL.DingYue.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.TextBox1.Text = dataById.UserName;
				this.TextBox2.Text = dataById.Email;
				this.istuiding.Checked = !dataById.IsTuiDing;
			}
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
				DingYueInfo dingYueInfo = new DingYueInfo();
				if (base.IsEdit)
				{
					dingYueInfo = SinGooCMS.BLL.DingYue.GetDataById(base.OpID);
				}
				dingYueInfo.UserName = WebUtils.GetString(this.TextBox1.Text);
				dingYueInfo.Email = WebUtils.GetString(this.TextBox2.Text);
				dingYueInfo.IsTuiDing = !this.istuiding.Checked;
				if (string.IsNullOrEmpty(dingYueInfo.UserName))
				{
					base.ShowMsg("请输入用户名称");
				}
				if (!ValidateUtils.IsEmail(dingYueInfo.Email))
				{
					base.ShowMsg("请输入有效的邮箱地址");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						dingYueInfo.Lang = base.cultureLang;
						dingYueInfo.AutoTimeStamp = System.DateTime.Now;
						if (SinGooCMS.BLL.DingYue.Add(dingYueInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加订阅邮箱[" + dingYueInfo.Email + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.DingYue.Update(dingYueInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改订阅邮箱[" + dingYueInfo.Email + "] thành công");
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
	}
}
