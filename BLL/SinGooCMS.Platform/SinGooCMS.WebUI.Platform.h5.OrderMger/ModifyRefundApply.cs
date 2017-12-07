using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.OrderMger
{
	public class ModifyRefundApply : H5ManagerPageBase
	{
		protected TextBox bz;

		protected Button btnok;

		protected Button btncancel;

		public RefundApplyInfo apply = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.apply = SinGooCMS.BLL.RefundApply.GetDataById(base.OpID);
				if (this.apply != null)
				{
					this.bz.Text = this.apply.AdminRemark;
					if (this.apply.Status != 0)
					{
						this.btnok.Visible = false;
						this.btncancel.Visible = false;
					}
				}
			}
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
				this.DealTuiHuo(true);
			}
		}

		protected void btncancel_Click(object sender, System.EventArgs e)
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
				this.DealTuiHuo(false);
			}
		}

		private void DealTuiHuo(bool isArgee)
		{
			RefundApplyInfo dataById = SinGooCMS.BLL.RefundApply.GetDataById(base.OpID);
			dataById.AdminRemark = WebUtils.GetString(this.bz.Text);
			dataById.Status = (isArgee ? 1 : -1);
			if (base.Action.Equals(ActionType.Modify.ToString()))
			{
				if (SinGooCMS.BLL.RefundApply.Update(dataById))
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
					{
						isArgee ? "同意" : "拒绝",
						"订单[",
						dataById.OrderNo,
						"]的商品[",
						dataById.ProductName,
						"]退货申请"
					}));
					base.Response.Redirect(string.Concat(new object[]
					{
						"RefundApply.aspx?CatalogID=",
						base.CurrentCatalogID,
						"&Module=",
						base.CurrentModuleCode,
						"&action=View"
					}));
				}
				else
				{
					base.ShowMsg("Thao tác thất bại");
				}
			}
		}
	}
}
