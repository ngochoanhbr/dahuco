using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ModifyField : H5ManagerPageBase
	{
		public string ShowGroup = "group1";

		public string DataSource = "Text";

		public int intModelID = 0;

		public UserGroupInfo modelParent = null;

		protected Literal labModelName;

		protected DropDownList DropDownList5;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected TextBox TextBox6;

		protected H5TextBox ExtTextBox1;

		protected H5TextBox ExtTextBox2;

		protected RadioButtonList ExtRadioButtonList3;

		protected TextBox ExtTextBox4;

		protected H5TextBox ExtTextBox5;

		protected DropDownList ExtDropDownList6;

		protected TextBox ExtTextBox7;

		protected TextBox ExtTextBox8;

		protected TextBox ExtTextBox9;

		protected TextBox ExtTextBox11;

		protected HtmlInputCheckBox CheckBox7;

		protected HtmlInputCheckBox CheckBox9;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.intModelID = WebUtils.GetQueryInt("GroupID");
			this.modelParent = SinGooCMS.BLL.UserGroup.GetCacheUserGroupById(this.intModelID);
			if (this.modelParent == null)
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", "<script>alert('没有找到会员组信息');history.go(-1);</script>");
			}
			else
			{
				this.labModelName.Text = this.modelParent.GroupName;
				if (base.IsEdit && !base.IsPostBack)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			UserFieldInfo dataById = SinGooCMS.BLL.UserField.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.TextBox2.Text = dataById.FieldName;
				this.TextBox2.Enabled = false;
				this.TextBox3.Text = dataById.Alias;
				this.TextBox4.Text = dataById.Tip;
				ListItem listItem = this.DropDownList5.Items.FindByValue(((FieldType)dataById.FieldType).ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				if (dataById.IsSystem)
				{
					this.DropDownList5.Enabled = false;
				}
				switch (dataById.FieldType)
				{
				case 0:
					this.ShowGroup = "group1";
					break;
				case 1:
					this.ShowGroup = "group2";
					break;
				case 2:
					this.ShowGroup = "group7";
					break;
				case 4:
				case 5:
				case 6:
					this.ShowGroup = "group3";
					break;
				case 7:
					this.ShowGroup = "group6";
					break;
				case 8:
				case 10:
					this.ShowGroup = "group4";
					break;
				case 9:
				case 11:
					this.ShowGroup = "group5";
					break;
				}
				this.TextBox6.Text = dataById.DefaultValue;
				this.CheckBox9.Checked = dataById.EnableNull;
				this.ExtTextBox5.Text = dataById.DataLength.ToString();
				if (dataById.IsSystem)
				{
					this.ExtTextBox5.Enabled = false;
				}
				SinGooCMS.Control.FieldSetting fieldSetting = XmlSerializerUtils.Deserialize<SinGooCMS.Control.FieldSetting>(dataById.Setting);
				if (fieldSetting != null)
				{
					this.ExtTextBox1.Text = fieldSetting.ControlWidth.ToString();
					this.ExtTextBox2.Text = fieldSetting.ControlHeight.ToString();
					ListItem listItem2 = this.ExtRadioButtonList3.Items.FindByValue(fieldSetting.TextMode);
					if (listItem2 != null)
					{
						listItem2.Selected = true;
					}
					this.ExtTextBox4.Text = fieldSetting.DataFormat;
					ListItem listItem3 = this.ExtDropDownList6.Items.FindByValue(fieldSetting.DataFrom);
					if (listItem3 != null)
					{
						listItem3.Selected = true;
					}
					string dataFrom = fieldSetting.DataFrom;
					if (dataFrom != null)
					{
						if (!(dataFrom == "Text"))
						{
							if (!(dataFrom == "DataDictionary"))
							{
								if (!(dataFrom == "SQLQuery"))
								{
									if (dataFrom == "AjaxData")
									{
										this.ExtTextBox11.Text = fieldSetting.DataSource;
										this.DataSource = "AjaxData";
									}
								}
								else
								{
									this.ExtTextBox9.Text = fieldSetting.DataSource;
									this.DataSource = "SQLQuery";
								}
							}
							else
							{
								this.ExtTextBox8.Text = fieldSetting.DataSource;
								this.DataSource = "DataDictionary";
							}
						}
						else
						{
							this.ExtTextBox7.Text = fieldSetting.DataSource;
							this.DataSource = "Text";
						}
					}
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
				UserFieldInfo userFieldInfo = new UserFieldInfo();
				if (base.IsEdit)
				{
					userFieldInfo = SinGooCMS.BLL.UserField.GetDataById(base.OpID);
				}
				userFieldInfo.UserGroupID = this.modelParent.AutoID;
				userFieldInfo.FieldName = WebUtils.GetString(this.TextBox2.Text);
				userFieldInfo.Alias = WebUtils.GetString(this.TextBox3.Text);
				userFieldInfo.Tip = WebUtils.GetString(this.TextBox4.Text);
				userFieldInfo.FieldType = (int)((FieldType)System.Enum.Parse(typeof(FieldType), this.DropDownList5.SelectedValue));
				userFieldInfo.DataLength = 50;
				if (string.IsNullOrEmpty(userFieldInfo.FieldName) || string.IsNullOrEmpty(userFieldInfo.Alias))
				{
					base.ShowMsg("字段名称/Tên hiển thị không thể để trống");
				}
				else
				{
					userFieldInfo.DefaultValue = WebUtils.GetString(this.TextBox6.Text);
					SinGooCMS.Control.FieldSetting fieldSetting = new SinGooCMS.Control.FieldSetting();
					fieldSetting.ControlWidth = WebUtils.GetInt(this.ExtTextBox1.Text);
					fieldSetting.ControlHeight = WebUtils.GetInt(this.ExtTextBox2.Text);
					fieldSetting.TextMode = this.ExtRadioButtonList3.SelectedValue;
					fieldSetting.IsDataType = userFieldInfo.FieldType.Equals(FieldType.DateTimeType);
					fieldSetting.DataFormat = WebUtils.GetString(this.ExtTextBox4.Text);
					FieldType fieldType = (FieldType)userFieldInfo.FieldType;
					if (fieldType != FieldType.MultipleHtmlType)
					{
						if (fieldType != FieldType.DateTimeType)
						{
							userFieldInfo.DataType = "nvarchar";
							userFieldInfo.DataLength = WebUtils.GetInt(this.ExtTextBox5.Text, 50);
						}
						else
						{
							userFieldInfo.DataType = "datetime";
						}
					}
					else
					{
						userFieldInfo.DataType = "ntext";
					}
					fieldSetting.DataFrom = this.ExtDropDownList6.SelectedValue;
					string dataFrom = fieldSetting.DataFrom;
					if (dataFrom != null)
					{
						if (!(dataFrom == "Text"))
						{
							if (!(dataFrom == "DataDictionary"))
							{
								if (!(dataFrom == "SQLQuery"))
								{
									if (dataFrom == "AjaxData")
									{
										fieldSetting.DataSource = WebUtils.GetString(this.ExtTextBox11.Text);
									}
								}
								else
								{
									fieldSetting.DataSource = WebUtils.GetString(this.ExtTextBox9.Text);
								}
							}
							else
							{
								fieldSetting.DataSource = WebUtils.GetString(this.ExtTextBox8.Text);
							}
						}
						else
						{
							fieldSetting.DataSource = this.ExtTextBox7.Text;
						}
					}
					userFieldInfo.Setting = XmlSerializerUtils.Serialize<SinGooCMS.Control.FieldSetting>(fieldSetting);
					userFieldInfo.IsUsing = this.CheckBox7.Checked;
					userFieldInfo.EnableNull = this.CheckBox9.Checked;
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						userFieldInfo.AutoTimeStamp = System.DateTime.Now;
						userFieldInfo.IsSystem = false;
						userFieldInfo.Sort = SinGooCMS.BLL.UserField.MaxSort + 1;
						FieldAddState fieldAddState = SinGooCMS.BLL.UserField.Add(userFieldInfo);
						FieldAddState fieldAddState2 = fieldAddState;
						switch (fieldAddState2)
						{
						case FieldAddState.Error:
							base.ShowMsg("Tạo trường thất bại");
							break;
						case FieldAddState.FieldNameIsUsing:
							base.ShowMsg("Tên trường đã được sử dụng");
							break;
						case FieldAddState.FieldNameExists:
							base.ShowMsg("Tên trường đã tồn tại");
							break;
						case FieldAddState.ModelNotExists:
							base.ShowMsg("没有找到会员组");
							break;
						case FieldAddState.CreateColumnError:
							base.ShowMsg("Tạo cột dữ liệu thất bại");
							break;
						default:
							if (fieldAddState2 != FieldAddState.Success)
							{
								base.ShowMsg("Lỗi Unknown");
							}
							else
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加会员组字段[" + userFieldInfo.FieldName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"UserField.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&GroupID=",
									this.intModelID,
									"&action=View"
								}));
							}
							break;
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.UserField.Update(userFieldInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改会员组字段[" + userFieldInfo.FieldName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"UserField.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&GroupID=",
								this.intModelID,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("修改会员组字段失败");
						}
					}
				}
			}
		}
	}
}
