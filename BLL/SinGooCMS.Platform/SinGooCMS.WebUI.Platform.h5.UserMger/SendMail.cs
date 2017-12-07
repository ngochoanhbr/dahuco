using CKEditor.NET;
using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Extensions;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class SendMail : H5ManagerPageBase
	{
		protected DropDownList ddlUserGroup;

		protected DropDownList ddlUserLevel;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected CKEditorControl TextBox4;

		protected Button btnok;

		protected HiddenField TargetType;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindUserGroup();
				this.BindUserLevel();
			}
		}

		private void BindUserGroup()
		{
			this.ddlUserGroup.DataSource = SinGooCMS.BLL.UserGroup.GetCacheUserGroupList();
			this.ddlUserGroup.DataTextField = "GroupName";
			this.ddlUserGroup.DataValueField = "AutoID";
			this.ddlUserGroup.DataBind();
		}

		private void BindUserLevel()
		{
			this.ddlUserLevel.DataSource = SinGooCMS.BLL.UserLevel.GetCacheUserLevelList();
			this.ddlUserLevel.DataTextField = "LevelName";
			this.ddlUserLevel.DataValueField = "AutoID";
			this.ddlUserLevel.DataBind();
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				string value = this.TargetType.Value;
				string @string = WebUtils.GetString(this.TextBox3.Text);
				string text = this.TextBox4.Text;
				if (PageBase.config.ServMailAccount.IsNullOrEmpty())
				{
					base.ShowMsg("尚未配置服务邮箱");
				}
				else if (@string.IsNullOrEmpty() || text.IsNullOrEmpty())
				{
					base.ShowMsg("邮件标题及邮件内容不能为空");
				}
				else
				{
					string text2 = value;
					if (text2 != null)
					{
						if (!(text2 == "ToAllUser"))
						{
							if (!(text2 == "ToUserGrup"))
							{
								if (!(text2 == "ToUserLevel"))
								{
									if (text2 == "ToCustomUser")
									{
										string text3 = WebUtils.GetString(this.TextBox2.Text);
										if (text3.StartsWith(","))
										{
											text3 = text3.Substring(1, text3.Length - 1);
										}
										if (text3.EndsWith(","))
										{
											text3 = text3.Substring(0, text3.Length - 1);
										}
										string text4 = this.SendMailToCustom(text3, @string, text);
										if (string.Empty == text4)
										{
											PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送邮件成功(发送给邮箱[" + text3 + "])");
											base.ShowMsg("发送邮件成功");
										}
										else
										{
											base.ShowMsg(text4);
										}
									}
								}
								else
								{
									string strCondition = " LevelID=" + WebUtils.StringToInt(this.ddlUserLevel.SelectedValue);
									string text4 = this.SendMailToMutiUser(@string, text, strCondition);
									if (string.Empty == text4)
									{
										PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送邮件成功(会员等级[" + this.ddlUserLevel.SelectedItem.Text + "])");
										base.ShowMsg("发送邮件成功");
									}
									else
									{
										base.ShowMsg(text4);
									}
								}
							}
							else
							{
								string strCondition = " GroupID=" + WebUtils.StringToInt(this.ddlUserGroup.SelectedValue);
								string text4 = this.SendMailToMutiUser(@string, text, strCondition);
								if (string.Empty == text4)
								{
									PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送邮件成功(会员组[" + this.ddlUserGroup.SelectedItem.Text + "])");
									base.ShowMsg("发送邮件成功");
								}
								else
								{
									base.ShowMsg(text4);
								}
							}
						}
						else
						{
							string text4 = this.SendMailToMutiUser(@string, text, string.Empty);
							if (string.Empty == text4)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送邮件成功(所有会员)");
								base.ShowMsg("发送邮件成功");
							}
							else
							{
								base.ShowMsg(text4);
							}
						}
					}
				}
			}
		}

		private string SendMailToMutiUser(string strTitle, string strCont, string strCondition)
		{
			string text = string.Empty;
			string text2 = " SELECT AutoID,UserName,Email,Mobile FROM cms_User WHERE Email<>'' ";
			if (!strCondition.IsNullOrEmpty())
			{
				text2 = text2 + " AND " + strCondition;
			}
			DataTable dataTable = PageBase.dbo.GetDataTable(text2);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					string empty = string.Empty;
					MsgService.SendMail(dataRow["Email"].ToString(), strTitle, strCont, out empty);
					if (empty != "success")
					{
						text = text + "发送" + dataRow["Email"].ToString() + "邮件失败;";
					}
				}
			}
			return text;
		}

		private string SendMailToCustom(string strRevicer, string strTitle, string strCont)
		{
			string text = string.Empty;
			string[] array = strRevicer.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string empty = string.Empty;
				MsgService.SendMail(array[i], strTitle, strCont, out empty);
				if (empty != "success")
				{
					text = text + "发送" + array[i] + "邮件失败;";
				}
			}
			return text;
		}
	}
}
