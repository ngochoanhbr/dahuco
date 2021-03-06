using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class AdsList : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected LinkButton btn_SaveSort;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		public AdPlaceInfo currAdPlace = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.currAdPlace = AdPlace.GetCacheAdPlaceById(WebUtils.GetQueryInt("PlaceID"));
			if (this.currAdPlace == null)
			{
				this.currAdPlace = AdPlace.GetCacheAdPlaces().FirstOrDefault<AdPlaceInfo>();
			}
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			string strSort = " Sort ASC,AutoID DESC ";
			this.Repeater1.DataSource = Ads.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			string text = " 1=1 AND PlaceID=" + this.currAdPlace.AutoID;
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				text = text + " AND AdName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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
				AdsInfo dataById = Ads.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "没有找到此广告信息,广告不存在或者已删除");
				}
				else if (Ads.Delete(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除广告[" + dataById.AdName + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "删除广告成功");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "删除广告失败");
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
					if (Ads.Delete(repeaterCheckIDs))
					{
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量删除广告成功");
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
				if (Ads.UpdateSort(repeaterSortDict))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置广告排序成功");
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
