using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.OrderMger
{
	public class OrderDetail : H5ManagerPageBase
	{
		protected Button btn_Confirm;

		protected Button btn_ChangeAmount;

		protected Button btn_PayOk;

		protected Button btn_SendGoods;

		protected Button btn_Delivered;

		protected Button btn_Cancel;

		protected Repeater rpt_Pros;

		protected Repeater rpt_Log;

		public OrdersInfo order = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.order = Orders.GetDataById(base.OpID);
			if (this.order == null)
			{
				base.Response.Write(string.Concat(new object[]
				{
					"<script>alert('读取订单信息出错');location='OrderList.aspx?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View';</script>"
				}));
			}
			else if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindPro()
		{
			this.rpt_Pros.DataSource = OrderItem.GetList(100, "OrderID=" + base.OpID, "AutoID desc");
			this.rpt_Pros.DataBind();
		}

		private void BindActionLog()
		{
			this.rpt_Log.DataSource = SinGooCMS.BLL.OrderAction.GetList(100, "OrderID=" + base.OpID, "AutoID desc");
			this.rpt_Log.DataBind();
		}

		private void ControlView()
		{
			OrderStatus orderStatus = (OrderStatus)this.order.OrderStatus;
			switch (orderStatus)
			{
			case OrderStatus.WaitAudit:
				if (base.IsAuthorizedOp("Audit"))
				{
					this.btn_Confirm.Visible = true;
					this.btn_Confirm.OnClientClick = string.Concat(new string[]
					{
						"$.dialog.open('OrderAction.aspx?Module=",
						base.CurrentModuleCode,
						"&action=Audit&opid=",
						this.order.AutoID.ToString(),
						"',{title:'确认订单',width:600,height:185},false);return false;"
					});
				}
				if (base.IsAuthorizedOp("Cancel"))
				{
					this.btn_Cancel.Visible = true;
					this.btn_Cancel.OnClientClick = string.Concat(new string[]
					{
						"$.dialog.open('OrderAction.aspx?Module=",
						base.CurrentModuleCode,
						"&action=Cancel&opid=",
						this.order.AutoID.ToString(),
						"',{title:'关闭订单',width:680,height:185},false);return false;"
					});
				}
				break;
			case OrderStatus.WaitPay:
				if (base.IsAuthorizedOp("Cancel"))
				{
					this.btn_Cancel.Visible = true;
					this.btn_Cancel.OnClientClick = string.Concat(new string[]
					{
						"$.dialog.open('OrderAction.aspx?Module=",
						base.CurrentModuleCode,
						"&action=Cancel&opid=",
						this.order.AutoID.ToString(),
						"',{title:'关闭订单',width:680,height:185},false);return false;"
					});
				}
				if (base.IsAuthorizedOp("ChangeAmount"))
				{
					this.btn_ChangeAmount.Visible = true;
					this.btn_ChangeAmount.OnClientClick = string.Concat(new string[]
					{
						" $.dialog.open('OrderAction.aspx?Module=",
						base.CurrentModuleCode,
						"&action=ChangeAmount&opid=",
						this.order.AutoID.ToString(),
						"',{title:'订单改价',width:600,height:275},false);return false;"
					});
				}
				if (base.IsAuthorizedOp("Pay"))
				{
					this.btn_PayOk.Visible = true;
					this.btn_PayOk.OnClientClick = string.Concat(new string[]
					{
						" $.dialog.open('OrderAction.aspx?Module=",
						base.CurrentModuleCode,
						"&action=Pay&opid=",
						this.order.AutoID.ToString(),
						"',{title:'确认收款',width:600,height:235},false);return false;"
					});
				}
				break;
			default:
				switch (orderStatus)
				{
				case OrderStatus.WaitDelivery:
					if (base.IsAuthorizedOp("SendGoods"))
					{
						this.btn_SendGoods.Visible = true;
						this.btn_SendGoods.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=SendGoods&opid=",
							this.order.AutoID.ToString(),
							"',{title:'订单发货',width:600,height:310},false);return false;"
						});
					}
					break;
				case OrderStatus.WaitSignGoods:
					if (base.IsAuthorizedOp("SignGoods"))
					{
						this.btn_Delivered.Visible = true;
						this.btn_Delivered.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=SignGoods&opid=",
							this.order.AutoID.ToString(),
							"',{title:'订单送达',width:600,height:185},false);return false;"
						});
					}
					break;
				}
				break;
			}
		}

		private void BindData()
		{
			this.BindPro();
			this.BindActionLog();
			this.ControlView();
		}

		public string GetKuaidi100Url()
		{
			SinGooCMS.Plugin.KuaidiComInfo kuaidiComInfo = KuaidiCom.Get(this.order.ShippingID);
			string result;
			if (kuaidiComInfo != null && !string.IsNullOrEmpty(this.order.ShippingNo))
			{
				result = Kuaidi100.Get(kuaidiComInfo.CompanyCode, this.order.ShippingNo);
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
