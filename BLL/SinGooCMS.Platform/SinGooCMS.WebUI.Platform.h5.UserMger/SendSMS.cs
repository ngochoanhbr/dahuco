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
	public class SendSMS : H5ManagerPageBase
	{
		private ISMS sms = SMSProvider.Create();

		protected DropDownList ddlUserGroup;

		protected DropDownList ddlUserLevel;

		protected TextBox TextBox2;

		protected TextBox TextBox4;

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
				string text = this.TextBox4.Text;
				string text2 = WebUtils.GetString(this.TextBox2.Text);
				if (this.sms == null)
				{
					base.ShowMsg("尚未配置短信接口");
				}
				else if (value == "ToCustomUser" && string.IsNullOrEmpty(text2))
				{
					base.ShowMsg("短信接收方不能为空");
				}
				else if (text.IsNullOrEmpty())
				{
					base.ShowMsg("短信内容不能为空");
				}
				else
				{
					string text3 = value;
					if (text3 != null)
					{
						if (!(text3 == "ToAllUser"))
						{
							if (!(text3 == "ToUserGrup"))
							{
								if (!(text3 == "ToUserLevel"))
								{
									if (text3 == "ToCustomUser")
									{
										if (text2.StartsWith(","))
										{
											text2 = text2.Substring(1, text2.Length - 1);
										}
										if (text2.EndsWith(","))
										{
											text2 = text2.Substring(0, text2.Length - 1);
										}
										string text4 = this.SendSMSToCustom(text2, text);
										if (string.Empty == text4)
										{
											PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送短信成功(发送给邮箱[" + text2 + "])");
											base.ShowMsg("发送短信成功");
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
									string text4 = this.SendSMSToMutiUser(text, strCondition);
									if (string.Empty == text4)
									{
										PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送短信成功(会员等级[" + this.ddlUserLevel.SelectedItem.Text + "])");
										base.ShowMsg("发送短信成功");
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
								string text4 = this.SendSMSToMutiUser(text, strCondition);
								if (string.Empty == text4)
								{
									PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送短信成功(会员组[" + this.ddlUserGroup.SelectedItem.Text + "])");
									base.ShowMsg("发送短信成功");
								}
								else
								{
									base.ShowMsg(text4);
								}
							}
						}
						else
						{
							string text4 = this.SendSMSToMutiUser(text, string.Empty);
							if (string.Empty == text4)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送短信成功(所有会员)");
								base.ShowMsg("发送短信成功");
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

		private string SendSMSToMutiUser(string strCont, string strCondition)
		{
			ISMS iSMS = SMSProvider.Create();
			string text = string.Empty;
			string text2 = " SELECT AutoID,UserName,Email,Mobile FROM cms_User WHERE Mobile<>'' ";
			if (!strCondition.IsNullOrEmpty())
			{
				text2 = text2 + " AND " + strCondition;
			}
			DataTable dataTable = PageBase.dbo.GetDataTable(text2);
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					string text3 = dataRow["Mobile"].ToString();
					if (!string.IsNullOrEmpty(text3) && text3.Length.Equals(11))
					{
						if (MsgService.SendSMS(text3, strCont) == 0)
						{
							text = text + "发送" + text3 + "短信失败;";
						}
					}
				}
			}
			return text;
		}

		private string SendSMSToCustom(string strRevicer, string strCont)
		{
			string text = string.Empty;
			string[] array = strRevicer.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (MsgService.SendSMS(array[i], strCont) == 0)
				{
					text = text + "发送" + array[i] + "短信失败;";
				}
			}
			return text;
		}
	}
}
