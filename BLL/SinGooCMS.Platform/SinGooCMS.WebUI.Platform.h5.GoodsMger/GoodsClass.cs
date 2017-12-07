using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class GoodsClass : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected LinkButton btn_SaveSort;

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
			string strSort = " Sort asc,AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = this.GetWithChilds(SinGooCMS.BLL.GoodsClass.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num));
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		private System.Collections.Generic.IList<GoodsClassInfo> GetWithChilds(System.Collections.Generic.IList<GoodsClassInfo> lstParents)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			foreach (GoodsClassInfo current in lstParents)
			{
				stringBuilder.Append(current.AutoID.ToString() + ",");
			}
			string str = stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
			System.Collections.Generic.List<GoodsClassInfo> list = (System.Collections.Generic.List<GoodsClassInfo>)SinGooCMS.BLL.GoodsClass.GetList(1000, "RootID in (" + str + ")", "RootID asc,Depth asc,Sort asc");
			System.Collections.Generic.IList<GoodsClassInfo> result;
			if (list != null && list.Count > 0)
			{
				System.Collections.Generic.List<GoodsClassInfo> list2 = new System.Collections.Generic.List<GoodsClassInfo>();
				foreach (GoodsClassInfo current in lstParents)
				{
					list2.Add(current);
					if (current.ChildCount > 0)
					{
						list2.AddRange(this.GetAllChilds(current.AutoID, list));
					}
				}
				result = list2;
			}
			else
			{
				result = lstParents;
			}
			return result;
		}

		private System.Collections.Generic.List<GoodsClassInfo> GetAllChilds(int parentID, System.Collections.Generic.List<GoodsClassInfo> lstChilds)
		{
			System.Collections.Generic.List<GoodsClassInfo> list = new System.Collections.Generic.List<GoodsClassInfo>();
			foreach (GoodsClassInfo current in lstChilds)
			{
				if (current.ParentID.Equals(parentID))
				{
					list.Add(current);
					if (current.ChildCount > 0)
					{
						list.AddRange(this.GetAllChilds(current.AutoID, lstChilds));
					}
				}
			}
			return list;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string result = " 1=1 And ParentID=0 ";
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				result = " ClassName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return result;
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
				GoodsClassInfo dataById = SinGooCMS.BLL.GoodsClass.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else if (SinGooCMS.BLL.GoodsClass.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除商品类目[" + dataById.ClassName + "] thành công");
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

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (SinGooCMS.BLL.GoodsClass.UpdateSort(repeaterSortDict))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新商品类目排序成功");
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
