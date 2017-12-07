using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class SettingDetail : H5ManagerPageBase
	{
		protected Repeater Repeater1;

		protected Button btnok;

		public SettingCategoryInfo cate = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.cate = SettingCategory.GetCacheSettingCategory(WebUtils.GetQueryInt("cateid"));
			if (this.cate == null)
			{
                base.ShowMsg("Không tìm thấy cấu hình này, cấu hình không tồn tại hoặc đã bị xóa");
			}
			else if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			System.Collections.Generic.List<SettingInfo> settingsList = new System.Collections.Generic.List<SettingInfo>();
			settingsList = (System.Collections.Generic.List<SettingInfo>)SettingProvider.GetCacheSettingByCateID(WebUtils.GetQueryInt("cateid"));
			System.Collections.Generic.List<ContFieldInfo> fieldList = this.GetFieldList(settingsList);
			fieldList.Sort((ContFieldInfo parameterA, ContFieldInfo parameterB) => parameterA.Sort.CompareTo(parameterB.Sort));
			this.Repeater1.DataSource = fieldList;
			this.Repeater1.DataBind();
		}

		private System.Collections.Generic.List<ContFieldInfo> GetFieldList(System.Collections.Generic.List<SettingInfo> settingsList)
		{
			System.Collections.Generic.List<ContFieldInfo> list = new System.Collections.Generic.List<ContFieldInfo>();
			foreach (SettingInfo current in settingsList)
			{
				if (current.IsUsing)
				{
					list.Add(new ContFieldInfo
					{
						AutoID = current.AutoID,
						FieldName = current.KeyName,
						Alias = current.KeyDesc,
						FieldType = (int)((FieldType)System.Enum.Parse(typeof(FieldType), current.ControlType)),
						Setting = current.Setting,
						Sort = current.Sort,
						EnableNull = true,
						DefaultValue = current.DefaultValue,
						Value = current.KeyValue,
						DataLength = current.DataLength
					});
				}
			}
			return list;
		}

		protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				ContFieldInfo contFieldInfo = e.Item.DataItem as ContFieldInfo;
				FieldControl fieldControl = e.Item.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					fieldControl.ControlType = (FieldType)contFieldInfo.FieldType;
					fieldControl.ControlPath = "~/Platform/h5/FieldControls/";
					fieldControl.LoadControlId = ((FieldType)contFieldInfo.FieldType).ToString();
					fieldControl.FieldName = contFieldInfo.FieldName;
					fieldControl.FieldAlias = contFieldInfo.Alias;
					fieldControl.FieldId = contFieldInfo.AutoID;
					fieldControl.EnableNull = contFieldInfo.EnableNull;
					fieldControl.Settings = XmlSerializerUtils.Deserialize<SinGooCMS.Control.FieldSetting>(contFieldInfo.Setting);
					fieldControl.DataLength = contFieldInfo.DataLength;
					if (!string.IsNullOrEmpty(contFieldInfo.Value))
					{
						fieldControl.Value = contFieldInfo.Value;
					}
					else
					{
						fieldControl.Value = contFieldInfo.DefaultValue;
					}
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			SettingProvider.Instance().UpdateSettings(this.GetSettingsListFromRepeater());
			CacheUtils.Del("JsonLeeCMS_CacheForGetSetting");
			CacheUtils.Del("JsonLeeCMS_CacheForSETTINGDIRECTORY");
			CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
            PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi phân loại Cấu hình [" + this.cate.CateDesc + "] thành công");
            base.ShowMsg("Cấu hình thành công");
		}

		private System.Collections.Generic.List<SettingInfo> GetSettingsListFromRepeater()
		{
			System.Collections.Generic.List<SettingInfo> list = new System.Collections.Generic.List<SettingInfo>();
			foreach (RepeaterItem repeaterItem in this.Repeater1.Items)
			{
				FieldControl fieldControl = repeaterItem.FindControl("field") as FieldControl;
				if (fieldControl != null)
				{
					SettingInfo item = new SettingInfo
					{
						AutoID = fieldControl.FieldId,
						KeyName = fieldControl.FieldName,
						KeyDesc = fieldControl.FieldAlias,
						KeyValue = fieldControl.Value
					};
					list.Add(item);
				}
			}
			return list;
		}
	}
}
