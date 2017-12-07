using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ContMger
{
	public class ContModelList : H5ManagerPageBase
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
			string strSort = "Sort asc, AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = ContModel.GetPagerData("*", this.GetCondition(), strSort, this.SinGooPager1.PageSize, this.SinGooPager1.PageIndex, ref recordCount, ref num);
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
				text = text + " AND ModelName like '%" + WebUtils.GetString(this.search_text.Text) + "%' ";
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
				ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(@int);
				if (cacheModelByID == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else
				{
					ModelDeleteStatus modelDeleteStatus = ContModel.Delete(@int);
					ModelDeleteStatus modelDeleteStatus2 = modelDeleteStatus;
					switch (modelDeleteStatus2)
					{
					case ModelDeleteStatus.Error:
						base.ShowAjaxMsg(this.UpdatePanel1, "删除内容模型失败");
						break;
					case ModelDeleteStatus.ModelNotExists:
						base.ShowAjaxMsg(this.UpdatePanel1, "模型不存在或者已经被删除");
						break;
					case ModelDeleteStatus.NodesRef:
						base.ShowAjaxMsg(this.UpdatePanel1, "无法删除,模型正被栏目引用");
						break;
					case ModelDeleteStatus.ContentRef:
						base.ShowAjaxMsg(this.UpdatePanel1, "无法删除,模型正被内容引用");
						break;
					default:
						if (modelDeleteStatus2 == ModelDeleteStatus.Success)
						{
							this.BindData();
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除内容模型[" + cacheModelByID.ModelName + "] thành công");
							base.ShowAjaxMsg(this.UpdatePanel1, "删除内容模型成功");
						}
						break;
					}
				}
			}
		}

		protected void btn_DelBat_Click(object sender, System.EventArgs e)
		{
		}
	}
}
