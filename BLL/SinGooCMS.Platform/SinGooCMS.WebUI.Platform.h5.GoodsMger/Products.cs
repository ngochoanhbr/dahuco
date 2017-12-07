using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class Products : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddlCategory;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected Button btn_AuditOK;

		protected Button btn_AuditCancel;

		protected LinkButton btn_SaveSort;

		protected DropDownList ddlMoveTo;

		protected Button btn_MoveTo;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		public CategoryInfo currCate = null;

		public bool IsAllList = true;

		public int Status = 1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.Status = WebUtils.GetQueryInt("Status", 1);
			if (!base.IsPostBack)
			{
				this.BindCategoryTree();
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " Sort ASC,AutoID desc ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = Product.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 AND Status=" + ((this.Status == 1) ? 99 : 0);
			int @int = WebUtils.GetInt(this.ddlCategory.SelectedValue);
			CategoryInfo categoryInfo = (@int > 0) ? SinGooCMS.BLL.Category.GetCacheCategoryByID(@int) : null;
			if (categoryInfo != null)
			{
				text = text + " AND CateID in (" + categoryInfo.ChildList + ")";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND ProductName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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

		private void BindCategoryTree()
		{
			System.Collections.Generic.List<CategoryInfo> cateTreeList = SinGooCMS.BLL.Category.GetCateTreeList();
			this.ddlMoveTo.DataSource = cateTreeList;
			this.ddlMoveTo.DataTextField = "CategoryName";
			this.ddlMoveTo.DataValueField = "AutoID";
			this.ddlMoveTo.DataBind();
			this.ddlCategory.DataSource = cateTreeList;
			this.ddlCategory.DataTextField = "CategoryName";
			this.ddlCategory.DataValueField = "AutoID";
			this.ddlCategory.DataBind();
			this.ddlCategory.Items.Insert(0, new ListItem("Tất cả các mục", "-1"));
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
				ProductInfo dataById = Product.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không có dữ liệu được tìm thấy, các dữ liệu không tồn tại hoặc bị xóa");
				}
				else if (Product.DelProByID(dataById.AutoID))
				{
					this.BindData();
                    PageBase.log.AddEvent(base.LoginAccount.AccountName, "xóa sản phẩm [" + dataById.ProductName + "] thành công");
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
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					string[] array = repeaterCheckIDs.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string value = array2[i];
						int @int = WebUtils.GetInt(value);
						ProductInfo dataById = Product.GetDataById(@int);
						if (dataById != null && Product.DelProByID(dataById.AutoID))
						{
                            PageBase.log.AddEvent(base.LoginAccount.AccountName, "xóa sản phẩm [" + dataById.ProductName + "] thành công");
						}
					}
					this.BindData();
				}
			}
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (Product.UpdateSort(repeaterSortDict))
				{
					this.BindData();
                    PageBase.log.AddEvent(base.LoginAccount.AccountName, "Cập nhật Sắp xếp thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void lnk_Copy_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				int num = WebUtils.StringToInt(((LinkButton)sender).CommandArgument);
				ProductInfo dataById = Product.GetDataById(num);
				if (Product.CopyProduct(num))
				{
					this.BindData();
                    PageBase.log.AddEvent(base.LoginAccount.AccountName, "sao chép sản phẩm [" + dataById.ProductName + "] thành công");
                    base.ShowAjaxMsg(this.UpdatePanel1, "sao chép thành công");
				}
				else
				{
                    base.ShowAjaxMsg(this.UpdatePanel1, "sao chép thất bại");
				}
			}
		}

		protected void btn_AuditOK_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					if (Product.UpdateStatus(repeaterCheckIDs, true))
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "Kích hoạt [" + repeaterCheckIDs + "] thành công");
						this.BindData();
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_AuditCancel_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					if (Product.UpdateStatus(repeaterCheckIDs, false))
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "Hủy kích hoạt hàng loạt [" + repeaterCheckIDs + "] thành công");
						this.BindData();
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_MoveTo_Click(object sender, System.EventArgs e)
		{
			string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
			if (!string.IsNullOrEmpty(repeaterCheckIDs))
			{
				CategoryInfo cacheCategoryByID = SinGooCMS.BLL.Category.GetCacheCategoryByID(WebUtils.GetInt(this.ddlMoveTo.SelectedValue));
				if (cacheCategoryByID != null)
				{
					if (Product.MoveProduct(cacheCategoryByID.AutoID, repeaterCheckIDs))
					{
						PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
						{
							"Vận chuyển hàng hóa (số：",
							repeaterCheckIDs,
							")để phân loại[",
							cacheCategoryByID.CategoryName,
							"]Thư mục thành công"
						}));
						this.BindData();
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
				}
			}
		}

		public bool IsPromote(System.DateTime dtStart, System.DateTime dtEnd)
		{
			return System.DateTime.Now >= dtStart && dtEnd >= System.DateTime.Now;
		}

		protected void btn_Export_Click(object sender, System.EventArgs e)
		{
            DataTable dataTable = PageBase.dbo.GetDataTable("SELECT \tAutoID AS MaSo,SellType AS Sales type,ProductName,ProductSN,ProImg, Stock,MarketPrice,SellPrice,BuyLimit,ProDetail, SEOKey,SEODescription,case Status when 99 then 'Đã duyệt' else 'Chưa duyệt' end AS trạng thái, Sort,Lang,AutoTimeStamp   FROM shop_Product WHERE " + this.GetCondition());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				string text = base.ExportFolder + StringUtils.GetRandomNumber() + ".xls";
				text = base.Server.MapPath(text);
				DataToXSL.CreateXLS(dataTable, text, true);
				ResponseUtils.ResponseFile(text);
			}
			else
			{
                base.ShowMsg("Không có dữ liệu tìm thấy");
			}
		}
	}
}
