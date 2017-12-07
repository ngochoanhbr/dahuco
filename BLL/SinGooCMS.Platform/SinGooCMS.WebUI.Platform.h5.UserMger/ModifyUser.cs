using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Extensions;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ModifyUser : H5ManagerPageBase
	{
		public int intGroupID = 0;

		public UserGroupInfo usergroup = null;

		public UserInfo userEdit = new UserInfo();

		protected Label Label2;

		protected TextBox TextBox1;

		protected TextBox TextBox4;

		protected DropDownList DropDownList3;

		protected H5TextBox TextBox5;

		protected TextBox TextBox6;

		protected TextBox TextBox10;

		protected TextBox TextBox11;

		protected TextBox FileSpace;

		protected Literal hasupload;

		protected DropDownList DropDownList12;

		protected TextBox TextBox13;

		protected Repeater Repeater1;

		protected HtmlInputCheckBox isaudit;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.intGroupID = WebUtils.GetQueryInt("GroupID", 0);
			this.usergroup = SinGooCMS.BLL.UserGroup.GetCacheUserGroupById(this.intGroupID);
			this.userEdit = SinGooCMS.BLL.User.GetDataById(base.OpID);
			if (this.usergroup == null)
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", string.Concat(new object[]
				{
					"<script>alert('没有找到会员组信息');location='UserList.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View'</script>"
				}));
			}
			else if (base.IsEdit && this.userEdit == null)
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", string.Concat(new object[]
				{
					"<script>alert('没有找到会员信息');location='UserList.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View&GroupID=",
					this.intGroupID,
					"'</script>"
				}));
			}
			else
			{
				this.Label2.Text = this.usergroup.GroupName;
				if (!base.IsPostBack)
				{
					this.BindUserLevel();
					this.BindCert();
					this.BindData();
				}
			}
		}

		private void BindUserLevel()
		{
			this.DropDownList3.DataSource = from p in SinGooCMS.BLL.UserLevel.GetCacheUserLevelList()
			orderby p.Integral
			select p;
			this.DropDownList3.DataTextField = "LevelName";
			this.DropDownList3.DataValueField = "AutoID";
			this.DropDownList3.DataBind();
		}

		private void BindCert()
		{
			this.DropDownList12.DataSource = Dicts.GetCacheDictsByName("CertType").Items;
			this.DropDownList12.DataTextField = "KeyValue";
			this.DropDownList12.DataValueField = "KeyName";
			this.DropDownList12.DataBind();
		}

		private void BindData()
		{
			System.Collections.Generic.IList<UserFieldInfo> fieldListWithValue = SinGooCMS.BLL.User.GetFieldListWithValue(base.OpID, this.intGroupID);
			this.Repeater1.DataSource = fieldListWithValue;
			this.Repeater1.DataBind();
			this.hasupload.Text = "0";
			if (base.IsEdit)
			{
				this.TextBox1.Text = this.userEdit.UserName;
				this.Label2.Text = SinGooCMS.BLL.UserGroup.GetCacheUserGroupById(this.userEdit.GroupID).GroupName;
				ListItem listItem = this.DropDownList3.Items.FindByValue(this.userEdit.LevelID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				this.TextBox5.Text = this.userEdit.Email;
				this.TextBox6.Text = this.userEdit.Mobile;
				this.TextBox10.Text = this.userEdit.Amount.ToString();
				this.TextBox11.Text = this.userEdit.Integral.ToString();
				ListItem listItem2 = this.DropDownList12.Items.FindByText(this.userEdit.CertType);
				if (listItem2 != null)
				{
					listItem2.Selected = true;
				}
				this.TextBox13.Text = this.userEdit.CertNo;
				this.isaudit.Checked = this.userEdit.Status.Equals(99);
				this.hasupload.Text = (SinGooCMS.BLL.User.GetFileCapacity(this.userEdit.UserName) / 1048576).ToString("f2");
				this.FileSpace.Text = (this.userEdit.FileSpace / 1048576).ToString("f2");
			}
		}

		protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				UserFieldInfo userFieldInfo = e.Item.DataItem as UserFieldInfo;
				FieldControl fieldControl = e.Item.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					fieldControl.ControlType = (FieldType)userFieldInfo.FieldType;
					fieldControl.ControlPath = "~/Platform/h5/FieldControls/";
					fieldControl.LoadControlId = ((FieldType)userFieldInfo.FieldType).ToString();
					fieldControl.FieldName = userFieldInfo.FieldName;
					fieldControl.FieldAlias = userFieldInfo.Alias;
					fieldControl.FieldId = userFieldInfo.AutoID;
					fieldControl.Settings = XmlSerializerUtils.Deserialize<SinGooCMS.Control.FieldSetting>(userFieldInfo.Setting);
					fieldControl.DataLength = userFieldInfo.DataLength;
					fieldControl.EnableNull = userFieldInfo.EnableNull;
					if (!string.IsNullOrEmpty(userFieldInfo.Value))
					{
						fieldControl.Value = userFieldInfo.Value.Trim();
					}
					else
					{
						fieldControl.Value = (userFieldInfo.DefaultValue ?? string.Empty);
					}
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
            base.ShowMsg("Ờ");
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
				UserInfo userInfo = new UserInfo();
				if (base.IsEdit)
				{
					userInfo = SinGooCMS.BLL.User.GetDataById(base.OpID);
				}
				userInfo.UserName = WebUtils.GetString(this.TextBox1.Text);
				userInfo.GroupID = this.intGroupID;
				userInfo.LevelID = WebUtils.GetInt(this.DropDownList3.SelectedValue);
				userInfo.Email = WebUtils.GetString(this.TextBox5.Text);
				userInfo.Mobile = WebUtils.GetString(this.TextBox6.Text);
				userInfo.Amount = WebUtils.GetDecimal(this.TextBox10.Text);
				userInfo.Integral = WebUtils.GetInt(this.TextBox11.Text);
				userInfo.CertType = this.DropDownList12.SelectedItem.Text;
				userInfo.CertNo = WebUtils.GetString(this.TextBox13.Text);
				userInfo.Status = (this.isaudit.Checked ? 99 : 0);
				userInfo.FileSpace = WebUtils.GetInt(this.FileSpace.Text) * 1024 * 1024;
				System.Collections.Generic.Dictionary<string, UserFieldInfo> fieldDicWithValues = this.GetFieldDicWithValues();
				string[] array = PageBase.config.SysUserName.Split(new char[]
				{
					','
				});
				if (userInfo.GroupID.Equals(0) || userInfo.LevelID.Equals(0))
				{
					base.ShowMsg("会员信息不完整，会员组及会员等级信息不可缺少");
				}
				else if (!SinGooCMS.BLL.User.IsValidUserName(userInfo.UserName) && !ValidateUtils.IsEmail(userInfo.UserName) && !ValidateUtils.IsMobilePhone(userInfo.UserName))
				{
					base.ShowMsg("无效的会员名称");
				}
				else if (array.Length > 0 && array.Contains(userInfo.UserName))
				{
					base.ShowMsg(userInfo.UserName + " 是系统保留会员名称，请选择其它会员名称");
				}
				else if (!base.IsEdit && this.TextBox4.Text.Length < 6)
				{
					base.ShowMsg("密码长度不少于6位");
				}
				else if (base.IsEdit && !string.IsNullOrEmpty(this.TextBox4.Text) && this.TextBox4.Text.Length < 6)
				{
					base.ShowMsg("密码长度不少于6位");
				}
				else if (!string.IsNullOrEmpty(userInfo.Email) && !ValidateUtils.IsEmail(userInfo.Email))
				{
					base.ShowMsg("邮箱格式不正确");
				}
				else if (!string.IsNullOrEmpty(userInfo.Email) && SinGooCMS.BLL.User.IsExistsByEmail(userInfo.Email, userInfo.AutoID))
				{
					base.ShowMsg("邮箱已经存在");
				}
				else if (!string.IsNullOrEmpty(userInfo.Mobile) && !ValidateUtils.IsMobilePhone(userInfo.Mobile))
				{
					base.ShowMsg("手机号码格式不正确");
				}
				else if (!string.IsNullOrEmpty(userInfo.Mobile) && SinGooCMS.BLL.User.IsExistsByMobile(userInfo.Mobile, userInfo.AutoID))
				{
					base.ShowMsg("手机号码已经存在");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						userInfo.Password = SinGooCMS.BLL.User.GetEncodePwd(this.TextBox4.Text);
						UserStatus userStatus = SinGooCMS.BLL.User.Reg(userInfo, fieldDicWithValues);
						switch (userStatus)
						{
						case UserStatus.Error:
							base.ShowMsg("添加会员失败");
							goto IL_54B;
						case UserStatus.WaitingForAudit:
						case UserStatus.MobileNoValidate:
							break;
						case UserStatus.UserNameNoValidate:
							base.ShowMsg("用户名非法");
							goto IL_54B;
						case UserStatus.EmailNoValidate:
							base.ShowMsg("邮箱地址非法");
							goto IL_54B;
						case UserStatus.ExistsUserName:
							base.ShowMsg("用户名已存在");
							goto IL_54B;
						case UserStatus.ExistsEmail:
							base.ShowMsg("邮箱地址已存在");
							goto IL_54B;
						case UserStatus.ExistsMobile:
							base.ShowMsg("手机号码已存在");
							goto IL_54B;
						default:
							if (userStatus == UserStatus.Success)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加会员[" + userInfo.UserName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"UserList.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&GroupID=",
									userInfo.GroupID,
									"&action=View"
								}));
								goto IL_54B;
							}
							break;
						}
						base.ShowMsg("Lỗi Unknown");
						IL_54B:;
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (!this.TextBox4.Text.IsNullOrEmpty())
						{
							userInfo.Password = SinGooCMS.BLL.User.GetEncodePwd(this.TextBox4.Text);
						}
						UserStatus userStatus = SinGooCMS.BLL.User.Update(userInfo, fieldDicWithValues);
						switch (userStatus)
						{
						case UserStatus.Error:
							base.ShowMsg("更新会员失败");
							goto IL_6DC;
						case UserStatus.WaitingForAudit:
						case UserStatus.MobileNoValidate:
							break;
						case UserStatus.UserNameNoValidate:
							base.ShowMsg("用户名非法");
							goto IL_6DC;
						case UserStatus.EmailNoValidate:
							base.ShowMsg("邮箱地址非法");
							goto IL_6DC;
						case UserStatus.ExistsUserName:
							base.ShowMsg("用户名已存在");
							goto IL_6DC;
						case UserStatus.ExistsEmail:
							base.ShowMsg("邮箱地址已存在");
							goto IL_6DC;
						case UserStatus.ExistsMobile:
							base.ShowMsg("手机号码已存在");
							goto IL_6DC;
						default:
							if (userStatus == UserStatus.Success)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新会员[" + userInfo.UserName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"UserList.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&GroupID=",
									userInfo.GroupID,
									"&action=View"
								}));
								goto IL_6DC;
							}
							break;
						}
						base.ShowMsg("Lỗi Unknown");
						IL_6DC:;
					}
				}
			}
		}

		private System.Collections.Generic.Dictionary<string, UserFieldInfo> GetFieldDicWithValues()
		{
			System.Collections.Generic.Dictionary<string, UserFieldInfo> dictionary = new System.Collections.Generic.Dictionary<string, UserFieldInfo>();
			foreach (RepeaterItem repeaterItem in this.Repeater1.Items)
			{
				FieldControl fieldControl = repeaterItem.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					UserFieldInfo dataById = SinGooCMS.BLL.UserField.GetDataById(fieldControl.FieldId);
					if (dataById != null)
					{
						dataById.Value = fieldControl.Value;
						dictionary.Add(dataById.FieldName, dataById);
					}
				}
			}
			return dictionary;
		}
	}
}
