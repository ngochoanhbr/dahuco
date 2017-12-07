using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class Menu : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_SaveSort;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPost)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " Sort ASC,AutoID desc ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = Catalog.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SinGooPager1.PageIndex = 1;
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND CatalogName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
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
				CatalogInfo cacheCatalogById = Catalog.GetCacheCatalogById(@int);
				if (cacheCatalogById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "没有找到此栏目,栏目不存在或者已删除");
				}
				else if (cacheCatalogById.IsSystem)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "系统栏目不可删除");
				}
				else if (Catalog.ExistsChildModule(@int))
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "栏目正被子模块引用");
				}
				else if (Catalog.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除栏目[" + cacheCatalogById.CatalogName + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (Catalog.UpdateSort(repeaterSortDict))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetAccountMenuDT");
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置管理栏目排序成功");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}
	}
}
