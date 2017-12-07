using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class ModifyIPStrategy : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected DropDownList DropDownList3;

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
			IPStrategyInfo dataById = SinGooCMS.BLL.IPStrategy.GetDataById(base.OpID);
			if (dataById.IPAddress.IndexOf('-') == -1)
			{
				this.TextBox1.Text = dataById.IPAddress;
			}
			else
			{
				this.TextBox1.Text = dataById.IPAddress.Split(new char[]
				{
					'-'
				})[0];
				this.TextBox2.Text = dataById.IPAddress.Split(new char[]
				{
					'-'
				})[1];
			}
			ListItem listItem = this.DropDownList3.Items.FindByValue(dataById.Strategy);
			if (listItem != null)
			{
				listItem.Selected = true;
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
				IPStrategyInfo iPStrategyInfo = new IPStrategyInfo();
				if (base.IsEdit)
				{
					iPStrategyInfo = SinGooCMS.BLL.IPStrategy.GetDataById(base.OpID);
				}
				string @string = WebUtils.GetString(this.TextBox1.Text);
				string string2 = WebUtils.GetString(this.TextBox2.Text);
				iPStrategyInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(@string) && string.IsNullOrEmpty(string2))
				{
					base.ShowMsg("IP地址不能为空");
				}
				else if (!string.IsNullOrEmpty(@string) && !ValidateUtils.IsIP(@string))
				{
					base.ShowMsg("起始IP不是有效的IP格式");
				}
				else if (!string.IsNullOrEmpty(string2) && !ValidateUtils.IsIP(string2))
				{
					base.ShowMsg("结束IP不是有效的IP格式");
				}
				else
				{
					if (!string.IsNullOrEmpty(@string) && !string.IsNullOrEmpty(string2))
					{
						iPStrategyInfo.IPAddress = @string + "-" + string2;
					}
					else if (!string.IsNullOrEmpty(@string))
					{
						iPStrategyInfo.IPAddress = @string;
					}
					else if (!string.IsNullOrEmpty(string2))
					{
						iPStrategyInfo.IPAddress = string2;
					}
					iPStrategyInfo.Strategy = this.DropDownList3.SelectedValue;
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (SinGooCMS.BLL.IPStrategy.Add(iPStrategyInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加IP策略[" + iPStrategyInfo.IPAddress + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加IP策略失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.IPStrategy.Update(iPStrategyInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改IP策略[" + iPStrategyInfo.IPAddress + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改IP策略失败");
						}
					}
				}
			}
		}
	}
}
