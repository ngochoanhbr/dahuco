using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class Setting : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " AutoID ASC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SettingCategory.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND CateDesc like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SinGooPager1.PageIndex = 1;
			this.BindData();
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				SettingCategoryInfo cacheSettingCategory = SettingCategory.GetCacheSettingCategory(@int);
				if (cacheSettingCategory == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không tìm thấy dữ liệu, dữ liệu không tồn tại hoặc đã bị xóa ");
				}
				else if (SettingCategory.ExistsChildSetting(cacheSettingCategory.AutoID))
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không thể bị xóa, bao gồm các thiết lập tùy chỉnh");
				}
				else if (SettingCategory.Delete(cacheSettingCategory.AutoID))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetSettingCategory");
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Xóa các thiết lập tùy chỉnh [" + cacheSettingCategory.CateName + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
		}
	}
}
