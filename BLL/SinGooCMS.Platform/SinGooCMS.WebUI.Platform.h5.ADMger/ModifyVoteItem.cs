using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class ModifyVoteItem : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

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
			VoteItemInfo dataById = SinGooCMS.BLL.VoteItem.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.VoteOption;
			this.TextBox2.Text = dataById.VoteNum.ToString();
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
				int queryInt = WebUtils.GetQueryInt("voteid", 0);
				VoteItemInfo voteItemInfo = new VoteItemInfo();
				if (base.IsEdit)
				{
					voteItemInfo = SinGooCMS.BLL.VoteItem.GetDataById(base.OpID);
				}
				voteItemInfo.VoteOption = WebUtils.GetString(this.TextBox1.Text);
				voteItemInfo.VoteNum = WebUtils.GetInt(this.TextBox2.Text);
				if (string.IsNullOrEmpty(voteItemInfo.VoteOption))
				{
					base.ShowMsg("请输入在在线调查选项名称");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						voteItemInfo.VoteID = queryInt;
						voteItemInfo.AutoTimeStamp = System.DateTime.Now;
						voteItemInfo.Lang = base.cultureLang;
						if (SinGooCMS.BLL.VoteItem.Add(voteItemInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加在线调查选项[" + voteItemInfo.VoteOption + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加在线调查选项失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.VoteItem.Update(voteItemInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改在线调查选项[" + voteItemInfo.VoteOption + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改在线调查选项失败");
						}
					}
				}
			}
		}
	}
}
