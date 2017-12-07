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
	public class Module : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddl_lm;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_SaveSort;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindCatalog();
				this.BindData();
			}
		}

		private void BindCatalog()
		{
			System.Collections.Generic.IList<CatalogInfo> cacheCatalogList = Catalog.GetCacheCatalogList();
			this.ddl_lm.DataSource = cacheCatalogList;
			this.ddl_lm.DataTextField = "CatalogName";
			this.ddl_lm.DataValueField = "AutoID";
			this.ddl_lm.DataBind();
			this.ddl_lm.Items.Insert(0, new ListItem("请选择栏目", "0"));
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " CatalogID ASC,Sort ASC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = SinGooCMS.BLL.Module.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
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
			int @int = WebUtils.GetInt(this.ddl_lm.SelectedValue);
			if (@int > 0)
			{
				text = text + " AND CatalogID=" + @int;
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND ModuleName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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
				ModuleInfo dataById = SinGooCMS.BLL.Module.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không tìm thấy dữ liệu, dữ liệu không tồn tại hoặc đã bị xóa ");
				}
				else if (dataById.IsSystem)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "系统模块不可删除");
				}
				else if (SinGooCMS.BLL.Module.DeleteAll(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除模块[" + dataById.ModuleName + "] thành công");
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
				if (SinGooCMS.BLL.Module.UpdateSort(repeaterSortDict))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetAccountMenuDT");
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置管理模块排序成功");
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
