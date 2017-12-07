using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ModifyUserAmount : H5ManagerPageBase
	{
		protected H5TextBox TextBox1;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
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
				decimal @decimal = WebUtils.GetDecimal(this.TextBox1.Text);
				UserInfo userById = SinGooCMS.BLL.User.GetUserById(WebUtils.GetQueryInt("UserID"));
				if (userById == null)
				{
					base.ShowMsg("会员不存在或者已删除！");
				}
				else if (@decimal.Equals(0m))
				{
					base.ShowMsg("请输入有效的数值!");
				}
				else if (base.Action.Equals(ActionType.Modify.ToString()))
				{
					string queryString = WebUtils.GetQueryString("Type");
					if (queryString != null)
					{
						if (!(queryString == "Amount"))
						{
							if (queryString == "Integral")
							{
								SinGooCMS.BLL.AccountDetail.AddIntegral(userById, (@decimal > 0m) ? 1 : 0, (double)System.Math.Abs(@decimal), string.Concat(new string[]
								{
									"管理员[",
									base.LoginAccount.AccountName,
									"]对会员[",
									userById.UserName,
									"]账户进行积分充值"
								}));
							}
						}
						else
						{
							SinGooCMS.BLL.AccountDetail.AddAmount(userById, (@decimal > 0m) ? 1 : 0, (double)System.Math.Abs(@decimal), string.Concat(new string[]
							{
								"管理员[",
								base.LoginAccount.AccountName,
								"]对会员[",
								userById.UserName,
								"]账户进行金额充值"
							}));
						}
					}
					PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new object[]
					{
						"管理员[",
						base.LoginAccount.AccountName,
						"]对会员[",
						userById.UserName,
						"]账户进行",
						WebUtils.GetQueryString("Type").Equals("Amount") ? "金额" : "积分",
						"充值，充值数值为：",
						@decimal
					}));
					MessageUtils.DialogCloseAndParentReload(this);
				}
			}
		}
	}
}
