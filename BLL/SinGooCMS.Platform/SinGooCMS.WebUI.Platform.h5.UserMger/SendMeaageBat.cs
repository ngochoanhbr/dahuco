using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Extensions;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class SendMeaageBat : H5ManagerPageBase
	{
		protected DropDownList ddlUserGroup;

		protected DropDownList ddlUserLevel;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

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
				string accountName = base.LoginAccount.AccountName;
				string @string = WebUtils.GetString(this.TextBox3.Text);
				string string2 = WebUtils.GetString(this.TextBox4.Text);
				if (@string.IsNullOrEmpty() || string2.IsNullOrEmpty())
				{
					base.ShowMsg("消息标题及消息内容不能为空");
				}
				else
				{
					string text = value;
					if (text != null)
					{
						if (!(text == "ToAllUser"))
						{
							if (!(text == "ToUserGrup"))
							{
								if (!(text == "ToUserLevel"))
								{
									if (text == "ToCustomUser")
									{
										string text2 = WebUtils.GetString(this.TextBox2.Text);
										text2 = text2.TrimEnd(new char[]
										{
											','
										});
										this.SendMsgToCustom(text2, @string, string2);
										PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送消息成功[指定会员(" + text2 + ")]");
										base.ShowMsg("发送消息成功");
									}
								}
								else
								{
									System.Collections.Generic.IList<UserInfo> list = PageBase.dbo.GetList<UserInfo>(" select * from cms_User where Statu=99 AND LevelID=" + WebUtils.GetInt(this.ddlUserLevel.SelectedValue));
									System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
									foreach (UserInfo current in list)
									{
										list2.Add(current.UserName);
									}
									this.SendMsgToGroupUser(list2, @string, string2);
									PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送消息成功(会员等级[" + this.ddlUserLevel.SelectedItem.Text + "])");
									base.ShowMsg("发送消息成功");
								}
							}
							else
							{
								System.Collections.Generic.IList<UserInfo> list = PageBase.dbo.GetList<UserInfo>(" select * from cms_User where Statu=99 AND GroupID=" + WebUtils.GetInt(this.ddlUserGroup.SelectedValue));
								System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
								foreach (UserInfo current in list)
								{
									list2.Add(current.UserName);
								}
								this.SendMsgToGroupUser(list2, @string, string2);
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送消息成功(会员组[" + this.ddlUserGroup.SelectedItem.Text + "])");
								base.ShowMsg("发送消息成功");
							}
						}
						else
						{
							System.Collections.Generic.IList<UserInfo> list = PageBase.dbo.GetList<UserInfo>(" select * from cms_User where Statu=99 ");
							System.Collections.Generic.List<string> list2 = new System.Collections.Generic.List<string>();
							foreach (UserInfo current in list)
							{
								list2.Add(current.UserName);
							}
							this.SendMsgToGroupUser(list2, @string, string2);
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "发送消息成功(所有会员)");
							base.ShowMsg("发送消息成功");
						}
					}
				}
			}
		}

		private void SendMsgToGroupUser(System.Collections.Generic.List<string> lstReciever, string strTitle, string strCont)
		{
			SinGooCMS.BLL.Message.SendMsg(MsgType.ManagerMsg, UserType.Manager, base.AccountName, UserType.User, lstReciever, strTitle, strCont);
		}

		private void SendMsgToCustom(string strRevicer, string strTitle, string strCont)
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			list.AddRange(strRevicer.Split(new char[]
			{
				','
			}));
			SinGooCMS.BLL.Message.SendMsg(MsgType.ManagerMsg, UserType.Manager, base.AccountName, UserType.User, list, strTitle, strCont);
		}
	}
}
