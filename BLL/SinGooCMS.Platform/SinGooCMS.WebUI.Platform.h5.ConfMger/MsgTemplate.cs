using CKEditor.NET;
using SinGooCMS.Common;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class MsgTemplate : H5ManagerPageBase
	{
		protected Literal msgtype;

		protected Panel PanelTitle;

		protected TextBox TextBox1;

		protected Literal tagdesc;

		protected Panel Panel1;

		protected TextBox TextBox3;

		protected Panel Panel2;

		protected CKEditorControl TextBox2;

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
			SinGooCMS.Plugin.MessageSet messageSet = SinGooCMS.Plugin.MessageSet.Get(WebUtils.GetQueryString("setkey"));
			string queryString = WebUtils.GetQueryString("type");
			this.msgtype.Text = messageSet.SetType;
			this.tagdesc.Text = messageSet.TagDesc;
			string text = queryString.ToLower().Trim();
			if (text != null)
			{
				if (!(text == "msg"))
				{
					if (!(text == "mail"))
					{
						if (text == "sms")
						{
							this.Panel1.Visible = true;
							this.PanelTitle.Visible = false;
							this.TextBox3.Text = messageSet.SMSTemplate;
						}
					}
					else
					{
						this.Panel2.Visible = true;
						this.TextBox1.Text = messageSet.MailTitle;
						this.TextBox2.Text = messageSet.MailTemplate;
					}
				}
				else
				{
					this.Panel1.Visible = true;
					this.TextBox1.Text = messageSet.MessageTitle;
					this.TextBox3.Text = messageSet.MessageTemplate;
				}
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
				System.Collections.Generic.Dictionary<string, SinGooCMS.Plugin.MessageSet> dictionary = SinGooCMS.Plugin.MessageSet.LoadDict();
				SinGooCMS.Plugin.MessageSet messageSet = dictionary[WebUtils.GetQueryString("setkey")];
				string queryString = WebUtils.GetQueryString("type");
				string text = queryString.ToLower().Trim();
				if (text != null)
				{
					if (!(text == "msg"))
					{
						if (!(text == "mail"))
						{
							if (text == "sms")
							{
								messageSet.SMSTemplate = this.TextBox3.Text;
							}
						}
						else
						{
							messageSet.MailTitle = WebUtils.GetString(this.TextBox1.Text);
							messageSet.MailTemplate = this.TextBox2.Text;
						}
					}
					else
					{
						messageSet.MessageTitle = WebUtils.GetString(this.TextBox1.Text);
						messageSet.MessageTemplate = this.TextBox3.Text;
					}
				}
				if (base.Action.Equals(ActionType.Modify.ToString()))
				{
					try
					{
						SinGooCMS.Plugin.MessageSet.Save(dictionary);
                        PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi Cài đặt tin nhắn [" + messageSet.SetType + "] thành công");
						MessageUtils.DialogCloseAndParentReload(this);
					}
					catch (System.Exception ex)
					{
						base.ShowMsg("Thao tác thất bại：" + ex.Message);
					}
				}
			}
		}
	}
}
