using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class ModifyCustomSetting : H5ManagerPageBase
	{
		protected DropDownList DropDownList2;

		protected TextBox TextBox1;

		protected TextBox TextBox3;

		protected DropDownList DropDownList5;

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

		protected Button btnok;

		public string ShowGroup = "group1";

		public string DataSource = "Text";

		public SettingCategoryInfo cate = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.cate = SettingCategory.GetDataById(WebUtils.GetQueryInt("cateid"));
			if (!base.IsPostBack)
			{
				this.BindCate();
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			SettingInfo entityById = SettingProvider.GetEntityById(base.OpID);
			if (entityById != null)
			{
				this.TextBox1.Text = entityById.KeyName;
				this.TextBox3.Text = entityById.KeyDesc;
				ListItem listItem = this.DropDownList5.Items.FindByValue(entityById.ControlType);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				switch ((FieldType)System.Enum.Parse(typeof(FieldType), entityById.ControlType))
				{
				case FieldType.SingleTextType:
					this.ShowGroup = "group1";
					break;
				case FieldType.MultipleTextType:
					this.ShowGroup = "group2";
					break;
				case FieldType.MultipleHtmlType:
					this.ShowGroup = "group7";
					break;
				case FieldType.RadioButtonType:
				case FieldType.CheckBoxType:
				case FieldType.DropDownListType:
					this.ShowGroup = "group3";
					break;
				case FieldType.DateTimeType:
					this.ShowGroup = "group6";
					break;
				case FieldType.PictureType:
				case FieldType.FileType:
					this.ShowGroup = "group4";
					break;
				case FieldType.MultiPictureType:
				case FieldType.MultiFileType:
					this.ShowGroup = "group5";
					break;
				}
				this.TextBox6.Text = entityById.DefaultValue;
				this.CheckBox7.Checked = entityById.IsUsing;
				this.ExtTextBox5.Text = entityById.DataLength.ToString();
				SinGooCMS.Control.FieldSetting fieldSetting = XmlSerializerUtils.Deserialize<SinGooCMS.Control.FieldSetting>(entityById.Setting);
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

		private void BindCate()
		{
			this.DropDownList2.DataSource = SettingCategory.GetCacheSettingCategoryList();
			this.DropDownList2.DataTextField = "CateDesc";
			this.DropDownList2.DataValueField = "AutoID";
			this.DropDownList2.DataBind();
			if (this.cate != null)
			{
				ListItem listItem = this.DropDownList2.Items.FindByValue(this.cate.AutoID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
					this.DropDownList2.Enabled = false;
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
				SettingInfo settingInfo = new SettingInfo();
				if (base.IsEdit)
				{
					settingInfo = SettingProvider.GetEntityById(base.OpID);
				}
				settingInfo.KeyName = WebUtils.GetString(this.TextBox1.Text);
				settingInfo.CateID = ((this.cate == null) ? 0 : this.cate.AutoID);
				settingInfo.KeyDesc = WebUtils.GetString(this.TextBox3.Text);
				if (settingInfo.CateID == 0)
				{
					base.ShowMsg("Chọn phân loại Cấu hình");
				}
				else if (string.IsNullOrEmpty(settingInfo.KeyName))
				{
					base.ShowMsg("Cấu hình tên chủ chốt không thể để trống");
				}
				else if (string.IsNullOrEmpty(settingInfo.KeyDesc))
				{
					base.ShowMsg("Tên hiển thị không thể để trống");
				}
				else
				{
					settingInfo.DataLength = WebUtils.GetInt(this.ExtTextBox5.Text, 50);
					settingInfo.ControlType = this.DropDownList5.SelectedValue;
					settingInfo.DefaultValue = WebUtils.GetString(this.TextBox6.Text);
					SinGooCMS.Control.FieldSetting fieldSetting = new SinGooCMS.Control.FieldSetting();
					fieldSetting.ControlWidth = WebUtils.GetInt(this.ExtTextBox1.Text);
					fieldSetting.ControlHeight = WebUtils.GetInt(this.ExtTextBox2.Text);
					fieldSetting.TextMode = this.ExtRadioButtonList3.SelectedValue;
					fieldSetting.IsDataType = settingInfo.ControlType.Equals(FieldType.DateTimeType);
					fieldSetting.DataFormat = WebUtils.GetString(this.ExtTextBox4.Text);
					FieldType fieldType = (FieldType)System.Enum.Parse(typeof(FieldType), settingInfo.ControlType);
					switch (fieldType)
					{
					case FieldType.MultipleTextType:
					case FieldType.MultipleHtmlType:
						settingInfo.DataType = "ntext";
						break;
					default:
						if (fieldType != FieldType.DateTimeType)
						{
							settingInfo.DataType = "nvarchar";
							settingInfo.DataLength = WebUtils.GetInt(this.ExtTextBox5.Text, 50);
						}
						else
						{
							settingInfo.DataType = "datetime";
						}
						break;
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
					settingInfo.Setting = XmlSerializerUtils.Serialize<SinGooCMS.Control.FieldSetting>(fieldSetting);
					settingInfo.IsUsing = this.CheckBox7.Checked;
					settingInfo.IsSystem = false;
					settingInfo.Sort = 9999;
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (SettingProvider.ExistsByName(settingInfo.KeyName))
						{
							base.ShowMsg("Cấu hình tùy chỉnh cùng tên đã tồn tại");
							return;
						}
						settingInfo.AutoTimeStamp = System.DateTime.Now;
						if (SettingProvider.Add(settingInfo) > 0)
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetSetting");
							CacheUtils.Del("JsonLeeCMS_CacheForSETTINGDIRECTORY");
                            PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm cấu hình tùy chỉnh [" + settingInfo.KeyDesc + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"CustomSetting.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&cateid=",
								this.cate.AutoID,
								"&action=View"
							}));
						}
						else
						{
                            base.ShowMsg("Thêm cấu hình tùy chỉnh thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SettingProvider.Update(settingInfo))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetSetting");
							CacheUtils.Del("JsonLeeCMS_CacheForSETTINGDIRECTORY");
                            PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi các cấu hình tùy chỉnh [" + settingInfo.KeyDesc + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"CustomSetting.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&cateid=",
								this.cate.AutoID,
								"&action=View"
							}));
						}
						else
						{
                            base.ShowMsg("Sửa đổi các cấu hình tùy chỉnh thất bại");
						}
					}
				}
			}
		}
	}
}
