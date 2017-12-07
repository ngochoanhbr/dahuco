using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class ModifySettingCate : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

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
			SettingCategoryInfo dataById = SettingCategory.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.CateName;
			this.TextBox2.Text = dataById.CateDesc;
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
				SettingCategoryInfo settingCategoryInfo = new SettingCategoryInfo();
				if (base.IsEdit)
				{
					settingCategoryInfo = SettingCategory.GetDataById(base.OpID);
				}
				settingCategoryInfo.CateName = WebUtils.GetString(this.TextBox1.Text);
				settingCategoryInfo.CateDesc = WebUtils.GetString(this.TextBox2.Text);
				settingCategoryInfo.IsUsing = true;
				settingCategoryInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(settingCategoryInfo.CateName))
				{
					base.ShowMsg("Tên loại Cấu hình không thể để trống");
				}
				else if (string.IsNullOrEmpty(settingCategoryInfo.CateDesc))
				{
					base.ShowMsg("Tên hiển thị không thể để trống");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (SettingCategory.Add(settingCategoryInfo) > 0)
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetSettingCategory");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "Thêm cấu hình tùy chỉnh [" + settingCategoryInfo.CateDesc + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
                            base.ShowMsg("Thêm cấu hình tùy chỉnh thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SettingCategory.Update(settingCategoryInfo))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForGetSettingCategory");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi các cấu hình tùy chỉnh [" + settingCategoryInfo.CateDesc + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
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
