using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class SetOperation : ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected Button btnok;

		protected LinkButton btn_AddDefault;

		private ModuleInfo module = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.module = SinGooCMS.BLL.Module.GetDataById(base.OpID);
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = "Sort asc,AutoID asc";
			this.Repeater1.DataSource = Operate.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
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
			return " 1=1 AND ModuleID=" + base.OpID;
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
				OperateInfo dataById = Operate.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Không tìm thấy dữ liệu, dữ liệu không tồn tại hoặc đã bị xóa ");
				}
				else if (Operate.Delete(@int))
				{
					Purview.Delete(this.module.AutoID, dataById.OperateCode);
					CacheUtils.Del("JsonLeeCMS_CacheForGetAccountMenuDT");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
					{
						"删除模块[",
						(this.module == null) ? string.Empty : this.module.ModuleName,
						"]的操作种类[",
						dataById.OperateName,
						"] thành công"
					}));
					this.BindData();
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("AddOperate"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				OperateInfo operateInfo = new OperateInfo
				{
					ModuleID = base.OpID,
					OperateName = WebUtils.GetString(this.TextBox1.Text),
					OperateCode = WebUtils.GetString(this.TextBox2.Text),
					AutoTimeStamp = System.DateTime.Now
				};
				if (string.IsNullOrEmpty(operateInfo.OperateName) || string.IsNullOrEmpty(operateInfo.OperateCode))
				{
					base.ShowMsg("操作名称和操作代码不能为空");
				}
				else if (Operate.Add(operateInfo) > 0)
				{
					this.BindData();
					PageBase.log.AddEvent(base.AccountName, string.Concat(new string[]
					{
						"添加模板[",
						this.module.ModuleName,
						"]的操作[",
						operateInfo.OperateName,
						"] thành công"
					}));
					base.ShowMsg("添加成功");
				}
				else
				{
					base.ShowMsg("添加失败");
				}
			}
		}

		protected void btn_AddDefault_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("AddOperate"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else if (Operate.AddDefaultOperate(base.OpID))
			{
				this.BindData();
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加模板[" + this.module.ModuleName + "]的默认Thao tác thành công");
				base.ShowMsg("添加成功");
			}
			else
			{
				base.ShowMsg("添加失败");
			}
		}
	}
}
