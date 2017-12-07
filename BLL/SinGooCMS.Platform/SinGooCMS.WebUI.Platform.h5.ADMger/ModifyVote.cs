using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class ModifyVote : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected CheckBox chkdx;

		protected CheckBox chknm;

		protected CheckBox chksh;

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
			VoteInfo dataById = SinGooCMS.BLL.Vote.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.Title;
			this.chkdx.Checked = dataById.IsMultiChoice;
			this.chknm.Checked = dataById.IsAnonymous;
			this.chksh.Checked = dataById.IsAudit;
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
				VoteInfo voteInfo = new VoteInfo();
				if (base.IsEdit)
				{
					voteInfo = SinGooCMS.BLL.Vote.GetDataById(base.OpID);
				}
				voteInfo.Title = WebUtils.GetString(this.TextBox1.Text);
				voteInfo.IsMultiChoice = this.chkdx.Checked;
				voteInfo.IsAnonymous = this.chknm.Checked;
				voteInfo.IsAudit = this.chksh.Checked;
				if (string.IsNullOrEmpty(voteInfo.Title))
				{
					base.ShowMsg("请输入在线调查主题名称");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						voteInfo.Sort = SinGooCMS.BLL.Vote.MaxSort + 1;
						voteInfo.AutoTimeStamp = System.DateTime.Now;
						voteInfo.Lang = base.cultureLang;
						if (SinGooCMS.BLL.Vote.Add(voteInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加在线调查[" + voteInfo.Title + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加在线调查失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.Vote.Update(voteInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改在线调查[" + voteInfo.Title + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改在线调查失败");
						}
					}
				}
			}
		}
	}
}
