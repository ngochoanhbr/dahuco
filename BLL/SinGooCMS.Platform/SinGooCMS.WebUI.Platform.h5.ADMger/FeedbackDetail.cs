using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class FeedbackDetail : H5ManagerPageBase
	{
		public FeedbackInfo feedback = new FeedbackInfo();

		protected TextBox txtReply;

		protected CheckBox chkReply2Mail;

		protected H5TextBox txtMail;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			this.feedback = SinGooCMS.BLL.Feedback.GetDataById(base.OpID);
			if (this.feedback != null)
			{
				UserInfo userByName = SinGooCMS.BLL.User.GetUserByName((this.feedback == null) ? string.Empty : this.feedback.UserName);
				if (!string.IsNullOrEmpty(this.feedback.Email) && ValidateUtils.IsEmail(this.feedback.Email))
				{
					this.txtMail.Text = this.feedback.Email;
				}
				else if (this.feedback != null && userByName != null && !string.IsNullOrEmpty(userByName.Email) && ValidateUtils.IsEmail(userByName.Email))
				{
					this.txtMail.Text = userByName.Email;
				}
				this.txtReply.Text = this.feedback.ReplyContent;
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Reply"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				string text = base.Server.HtmlEncode(this.txtReply.Text);
				if (string.IsNullOrEmpty(text))
				{
					base.ShowMsg("请输入回复内容");
				}
				else
				{
					FeedbackInfo dataById = SinGooCMS.BLL.Feedback.GetDataById(base.OpID);
					dataById.Replier = base.LoginAccount.AccountName;
					dataById.ReplyContent = text;
					dataById.ReplyDate = System.DateTime.Now;
					if (SinGooCMS.BLL.Feedback.Update(dataById))
					{
						string @string = WebUtils.GetString(this.txtMail.Text);
						string strMailBody = string.Concat(new string[]
						{
							"来自管理员的回复：<br/><div style='border-bottom:1px solid #ccc'>",
							text,
							"</div><br/>",
							PageBase.config.SiteName,
							"(",
							PageBase.config.SiteDomain,
							")"
						});
						string empty = string.Empty;
						if (this.chkReply2Mail.Checked && ValidateUtils.IsEmail(@string))
						{
							MsgService.SendMail(@string, "来自管理员的回复", strMailBody, out empty);
						}
						if (!string.IsNullOrEmpty(empty) && "success" != empty)
						{
							base.ShowMsg(empty);
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "回复留言[" + this.feedback.Title + "] thành công,但发送邮件失败");
						}
						else
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "回复留言[" + dataById.Title + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
					}
					else
					{
						base.ShowMsg("回复留言失败");
					}
				}
			}
		}
	}
}
