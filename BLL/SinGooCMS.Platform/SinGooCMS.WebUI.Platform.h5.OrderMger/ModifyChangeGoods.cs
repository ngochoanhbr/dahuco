using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.OrderMger
{
	public class ModifyChangeGoods : H5ManagerPageBase
	{
		public ExchangeApplyInfo apply = null;

		protected TextBox bz;

		protected Button btnok;

		protected Button btncancel;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.apply = ExchangeApply.GetDataById(base.OpID);
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
				this.DealHuangHuo(true);
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
				this.DealHuangHuo(false);
			}
		}

		private void DealHuangHuo(bool isArgee)
		{
			ExchangeApplyInfo dataById = ExchangeApply.GetDataById(base.OpID);
			dataById.AdminRemark = WebUtils.GetString(this.bz.Text);
			dataById.Status = (isArgee ? 1 : -1);
			if (base.Action.Equals(ActionType.Modify.ToString()))
			{
				if (ExchangeApply.Update(dataById))
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, string.Concat(new string[]
					{
						isArgee ? "同意" : "拒绝",
						"订单[",
						dataById.OrderNo,
						"]的商品[",
						dataById.GoodsName,
						"]换货申请"
					}));
					base.Response.Redirect(string.Concat(new object[]
					{
						"ChangeGoods.aspx?CatalogID=",
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
