using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ProField : H5ManagerPageBase
	{
		protected TextBox search_text;

		protected Button searchbtn;

		protected Button btn_Enabled;

		protected Button btn_UnEnabled;

		protected LinkButton btn_SaveSort;

		protected HtmlInputCheckBox ckViewUnEnabled;

		protected DropDownList drpPageSize;

		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		public int intModelID = 0;

		public ProductModelInfo modelParent = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.intModelID = WebUtils.GetQueryInt("ModelID");
			this.modelParent = ProductModel.GetCacheModelById(this.intModelID);
			if (this.modelParent == null)
			{
				base.ShowMsg("Thông tin mô hình sản phẩm không tìm thấy");
			}
			else if (!base.IsPostBack)
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
			this.Repeater1.DataSource = ProductField.GetPagerData("*", this.GetCondition(), strSort, this.SinGooPager1.PageSize, this.SinGooPager1.PageIndex, ref recordCount, ref num);
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
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" AND ModelID=",
				this.intModelID,
				" "
			});
			if (!this.ckViewUnEnabled.Checked)
			{
				text += " AND IsUsing=1 ";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND Alias like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
			}
			return text;
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		protected void ckViewUnEnabled_CheckedChanged(object sender, System.EventArgs e)
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
				ProductFieldInfo dataById = ProductField.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else if (dataById.IsSystem)
				{
                    base.ShowAjaxMsg(this.UpdatePanel1, "Lĩnh vực thuộc hệ thống không thể bị xóa");
				}
				else if (ProductField.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Xóa trường [" + dataById.FieldName + "] thành công");
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

		protected void btn_Enabled_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("SetEnabled"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					if (ProductField.UpdateIsUsing(repeaterCheckIDs, true))
					{
						this.BindData();
                        PageBase.log.AddEvent(base.LoginAccount.AccountName, "Kích hoạt tính năng trường sản phẩm với số lượng lớn [" + this.modelParent.ModelName + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_UnEnabled_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("SetUnEnabled"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string repeaterCheckIDs = base.GetRepeaterCheckIDs(this.Repeater1, "chk", "autoid");
				if (!string.IsNullOrEmpty(repeaterCheckIDs))
				{
					if (ProductField.UpdateIsUsing(repeaterCheckIDs, false))
					{
						this.BindData();
                        PageBase.log.AddEvent(base.LoginAccount.AccountName, "Trường sản phẩm Disable hàng loạt [" + this.modelParent.ModelName + "] thành công");
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_SaveSort_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			if (repeaterSortDict.Count > 0)
			{
				if (ProductField.UpdateSort(repeaterSortDict))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Trường sản phẩm cập nhật [" + this.modelParent.ModelName + "] thành công");
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
