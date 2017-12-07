using CKEditor.NET;
using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class SendDingYueMail : H5ManagerPageBase
	{
		protected Literal servermailbox;

		protected TextBox TextBox2;

		protected CKEditorControl TextBox3;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.servermailbox.Text = PageBase.config.ServMailAccount;
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("SendMail"))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				DingYueLogInfo dingYueLogInfo = new DingYueLogInfo();
				dingYueLogInfo.SendEmail = PageBase.config.ServMailAccount;
				dingYueLogInfo.MailTitle = WebUtils.GetString(this.TextBox2.Text);
				dingYueLogInfo.MailBody = WebUtils.GetString(this.TextBox3.Text);
				if (string.IsNullOrEmpty(dingYueLogInfo.SendEmail))
				{
					base.ShowMsg("没有可用的服务邮箱");
				}
				else if (string.IsNullOrEmpty(dingYueLogInfo.MailTitle))
				{
					base.ShowMsg("邮件标题不能为空");
				}
				else if (string.IsNullOrEmpty(dingYueLogInfo.MailBody))
				{
					base.ShowMsg("邮件内容不能为空");
				}
				else
				{
					dingYueLogInfo.SendCount = SendDingYueMail.SendMail(dingYueLogInfo.MailTitle, dingYueLogInfo.MailBody);
					dingYueLogInfo.Lang = base.cultureLang;
					dingYueLogInfo.AutoTimeStamp = System.DateTime.Now;
					if (DingYueLog.Add(dingYueLogInfo) > 0)
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送订阅邮件[" + dingYueLogInfo.MailTitle + "] thành công");
						base.ShowMsg("发送邮件成功");
					}
					else
					{
						base.ShowMsg("发送邮件失败");
					}
				}
			}
		}

		private static int SendMail(string strTitle, string strBody)
		{
			string str = string.Empty;
			int num = 0;
			System.Collections.Generic.IList<DingYueInfo> list = SinGooCMS.BLL.DingYue.GetList(0, " IsTuiDing=0 ", "AutoID asc");
			if (list != null && list.Count > 0)
			{
				string empty = string.Empty;
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				for (int i = 0; i < list.Count; i++)
				{
					if (ValidateUtils.IsEmail(list[i].Email))
					{
						stringBuilder.Append(list[i].Email + ",");
					}
					if ((i + 1) % 5 == 0 || i >= list.Count - 1)
					{
						string empty2 = string.Empty;
						string strReciveMail = stringBuilder.ToString().Trim().TrimEnd(new char[]
						{
							','
						});
						MsgService.SendMail(strReciveMail, strTitle, strBody, out empty2);
						if (empty2 != "success")
						{
							str = str + empty2 + ";";
						}
						else
						{
							num += stringBuilder.ToString().Trim().TrimEnd(new char[]
							{
								','
							}).Split(new char[]
							{
								','
							}).Length;
						}
						stringBuilder.Clear();
					}
				}
			}
			return num;
		}
	}
}
