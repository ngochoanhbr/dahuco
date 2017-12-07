using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Payments;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.OrderMger
{
	public class OrderAction : H5ManagerPageBase
	{
		public OrdersInfo order = null;

		public string strAction = string.Empty;

		protected Panel panelGJ;

		protected H5TextBox newamount;

		protected Panel panelPay;

		protected DropDownList paymenttype;

		protected Panel panelCompnay;

		protected DropDownList kuaidicompany;

		protected Panel panelNo;

		protected TextBox shippingno;

		protected TextBox txtRemark;

		protected Button btn_Confirm;

		protected Button btn_ChangeAmount;

		protected Button btn_PayOk;

		protected Button btn_SendGoods;

		protected Button btn_Delivered;

		protected Button btn_Cancel;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.order = Orders.GetDataById(base.OpID);
			this.strAction = WebUtils.GetQueryString("action");
			if (!string.IsNullOrEmpty(this.strAction) && this.order != null)
			{
				if (!base.IsPostBack)
				{
					this.BindPayments();
					this.BindShippingCompany();
				}
				this.ControlView();
			}
			else
			{
				base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", "<script>alert('参数不正确');$.dialog.open.origin.location.reload();</script>");
			}
		}

		private void BindPayments()
		{
			this.paymenttype.DataSource = Payment.GetAll();
			this.paymenttype.DataTextField = "PayName";
			this.paymenttype.DataValueField = "PayCode";
			this.paymenttype.DataBind();
			this.paymenttype.Items.Insert(0, new ListItem("无需支付", "NoPay"));
		}

		private void BindShippingCompany()
		{
			this.kuaidicompany.DataSource = KuaidiCom.Load();
			this.kuaidicompany.DataTextField = "CompanyName";
			this.kuaidicompany.DataValueField = "CompanyCode";
			this.kuaidicompany.DataBind();
			this.kuaidicompany.Items.Insert(0, new ListItem("无需货运", "NoShipping"));
		}

		private void ControlView()
		{
			OrderStatus orderStatus = (OrderStatus)this.order.OrderStatus;
			switch (orderStatus)
			{
			case OrderStatus.WaitAudit:
				if (base.IsAuthorizedOp("Cancel") && this.strAction.Equals("Cancel"))
				{
					this.btn_Cancel.Visible = true;
				}
				if (base.IsAuthorizedOp("Audit") && this.strAction.Equals("Audit"))
				{
					this.btn_Confirm.Visible = true;
				}
				break;
			case OrderStatus.WaitPay:
				if (base.IsAuthorizedOp("Cancel") && this.strAction.Equals("Cancel"))
				{
					this.btn_Cancel.Visible = true;
				}
				if (base.IsAuthorizedOp("ChangeAmount") && this.strAction.Equals("ChangeAmount"))
				{
					this.btn_ChangeAmount.Visible = true;
					this.panelGJ.Visible = true;
				}
				if (base.IsAuthorizedOp("Pay") && this.strAction.Equals("Pay"))
				{
					this.btn_PayOk.Visible = true;
					this.panelPay.Visible = true;
				}
				break;
			default:
				switch (orderStatus)
				{
				case OrderStatus.WaitDelivery:
					if (base.IsAuthorizedOp("SendGoods") && this.strAction.Equals("SendGoods"))
					{
						this.btn_SendGoods.Visible = true;
						if (this.NeedShipping())
						{
							this.panelCompnay.Visible = true;
							this.panelNo.Visible = true;
						}
					}
					break;
				case OrderStatus.WaitSignGoods:
					if (base.IsAuthorizedOp("SignGoods") && this.strAction.Equals("SignGoods"))
					{
						this.btn_Delivered.Visible = true;
					}
					break;
				}
				break;
			}
		}

		protected void btn_Confirm_Click(object sender, System.EventArgs e)
		{
			this.UpdateOrder(eumOrderAction.Audit);
		}

		protected void btn_ChangeAmount_Click(object sender, System.EventArgs e)
		{
			this.UpdateOrder(eumOrderAction.ChangeAmount);
		}

		protected void btn_PayOk_Click(object sender, System.EventArgs e)
		{
			this.UpdateOrder(eumOrderAction.Pay);
		}

		protected void btn_SendGoods_Click(object sender, System.EventArgs e)
		{
			this.UpdateOrder(eumOrderAction.SendGoods);
		}

		protected void btn_Delivered_Click(object sender, System.EventArgs e)
		{
			this.UpdateOrder(eumOrderAction.SignGoods);
		}

		protected void btn_Cancel_Click(object sender, System.EventArgs e)
		{
			this.UpdateOrder(eumOrderAction.Cancel);
		}

		private void UpdateOrder(eumOrderAction action)
		{
			OrdersInfo dataById = Orders.GetDataById(base.OpID);
			dataById.LastModifyTime = System.DateTime.Now;
			string strEventContent = string.Empty;
			string @string = WebUtils.GetString(this.txtRemark.Text);
			if (string.IsNullOrEmpty(@string))
			{
				base.ShowMsg("请填写本次操作事由");
			}
			else
			{
				switch (action)
				{
				case eumOrderAction.Audit:
					dataById.OrderStatus = 1;
					dataById.OrderAuditTime = System.DateTime.Now;
					break;
				case eumOrderAction.ChangeAmount:
					dataById.OrderTotalAmount = WebUtils.GetDecimal(this.newamount.Text);
					strEventContent = "管理员[" + base.AccountName + "]修改订单价格为：" + dataById.OrderTotalAmount.ToString("f2");
					break;
				case eumOrderAction.Pay:
				{
                    SinGooCMS.Payments.PaymentInfo byPayCode = Payment.GetByPayCode(this.paymenttype.SelectedValue);
					dataById.PayID = ((byPayCode == null) ? -1 : byPayCode.AutoID);
					dataById.PayName = ((byPayCode == null) ? "无" : byPayCode.PayName);
					dataById.OrderStatus = 10;
					dataById.OrderPayTime = System.DateTime.Now;
					strEventContent = "管理员[" + base.AccountName + "]已收款,收款方式为:" + dataById.PayName;
					break;
				}
				case eumOrderAction.SendGoods:
				{
                    SinGooCMS.Plugin.KuaidiComInfo kuaidiComInfo = KuaidiCom.Get(this.kuaidicompany.SelectedValue);
					dataById.ShippingID = ((kuaidiComInfo == null) ? -1 : kuaidiComInfo.AutoID);
					dataById.ShippingName = ((kuaidiComInfo == null) ? "无" : kuaidiComInfo.CompanyName);
					dataById.ShippingNo = WebUtils.GetString(this.shippingno.Text);
					dataById.OrderStatus = 11;
					dataById.GoodsSendTime = System.DateTime.Now;
					strEventContent = string.Concat(new string[]
					{
						"管理员[",
						base.AccountName,
						"]已发货,发件方式为:",
						dataById.ShippingName,
						"(快递单号:",
						dataById.ShippingNo,
						")"
					});
					break;
				}
				case eumOrderAction.SignGoods:
					dataById.OrderStatus = 99;
					dataById.GoodsServedTime = System.DateTime.Now;
					strEventContent = "管理员[" + base.AccountName + "]代签收";
					break;
				case eumOrderAction.Cancel:
					dataById.OrderStatus = 101;
					strEventContent = "管理员[" + base.AccountName + "]关闭了订单";
					break;
				}
				if (Orders.Update(dataById))
				{
					System.Collections.Generic.IList<OrderItemInfo> listByOID = OrderItem.GetListByOID(dataById.AutoID);
					if (eumOrderAction.Cancel == action)
					{
						if (listByOID.Count > 0)
						{
							foreach (OrderItemInfo current in listByOID)
							{
								Orders.ReBackStock(Product.GetDataById(current.ProID), GoodsSpecify.Get(current.ProID, current.GuiGePath), current.Quantity);
							}
						}
					}
					if (eumOrderAction.SendGoods == action)
					{
						UserInfo dataById2 = SinGooCMS.BLL.User.GetDataById(dataById.UserID);
						this.SendMsgOnFH(dataById, dataById2);
						this.SendEmailOnFH(dataById, dataById2);
						this.SendMobileOnFH(dataById, dataById2);
						new MsgService(SinGooCMS.BLL.User.GetDataById(dataById.UserID)).SendGoodsFH(dataById, dataById.ShippingName, dataById.ShippingNo);
					}
					if (eumOrderAction.SignGoods == action)
					{
						foreach (OrderItemInfo current in listByOID)
						{
							ProductInfo dataById3 = Product.GetDataById(current.ProID);
							if (dataById3 != null)
							{
								AccountDetail.AddIntegral(base.LoginUser, 1, (double)(dataById3.GiveIntegral * current.Quantity), string.Concat(new object[]
								{
									"成功购买商品[",
									current.ProName,
									"] x ",
									current.Quantity,
									"，获得赠送积分"
								}));
							}
						}
					}
					SinGooCMS.BLL.OrderAction.AddLog(dataById, base.AccountName, strEventContent, @string);
					base.ClientScript.RegisterClientScriptBlock(base.GetType(), "alertandredirect", "<script>$.dialog.open.origin.location.reload();</script>");
				}
				else
				{
					base.ShowMsg("Thao tác thất bại");
				}
			}
		}

		private void SendMsgOnFH(OrdersInfo order, UserInfo user)
		{
			bool @bool = WebUtils.GetBool(base.GetConfigValue("IsSendMsgOnGoodsSend"));
			string configValue = base.GetConfigValue("MsgTmpOnGoodsSend");
			if (@bool && !string.IsNullOrEmpty(configValue) && user != null)
			{
				Message.SendS2UMsg(user.UserName, "您的订单已经发货", configValue.Replace("${username}", user.UserName).Replace("${orderno}", order.OrderNo));
			}
		}

		private void SendEmailOnFH(OrdersInfo order, UserInfo user)
		{
			bool @bool = WebUtils.GetBool(base.GetConfigValue("IsSendMailOnGoodsSend"));
			string configValue = base.GetConfigValue("MailTmpOnGoodsSend");
			if (@bool && !string.IsNullOrEmpty(configValue) && !string.IsNullOrEmpty(user.Email) && ValidateUtils.IsEmail(user.Email) && user != null)
			{
				MsgService.SendMail(user.Email, "您的订单已经发货", configValue.Replace("${username}", user.UserName).Replace("${orderno}", order.OrderNo).Replace("${sitename}", PageBase.config.SiteName).Replace("${send_date}", System.DateTime.Now.ToString()));
			}
		}

		private void SendMobileOnFH(OrdersInfo order, UserInfo user)
		{
			bool @bool = WebUtils.GetBool(base.GetConfigValue("IsSendSMSOnGoodsSend"));
			string configValue = base.GetConfigValue("SMSTmpOnGoodsSend");
			if (@bool && !string.IsNullOrEmpty(configValue) && !string.IsNullOrEmpty(user.Mobile) && user.Mobile.Length == 11 && user != null)
			{
				MsgService.SendSMS(user.Mobile, configValue.Replace("${username}", user.UserName).Replace("${orderno}", order.OrderNo));
			}
		}

		private bool NeedShipping()
		{
			System.Collections.Generic.IList<OrderItemInfo> listByOID = OrderItem.GetListByOID(this.order.AutoID);
			bool result;
			if (listByOID != null && listByOID.Count > 0)
			{
				foreach (OrderItemInfo current in listByOID)
				{
					if (!current.IsVirtual)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}
	}
}
