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

namespace SinGooCMS.WebUI.Platform.h5.OrderMger
{
	public class OrderList : H5ManagerPageBase
	{
		private ShopFunction shop = new ShopFunction();

		public string strStatus = string.Empty;

		public OrderStatusSTAT ztstat = new OrderStatusSTAT();

		protected UpdatePanel UpdatePanel1;

		protected DropDownList ddlType;

		protected TextBox timestart;

		protected TextBox timeend;

		protected TextBox search_text;

		protected Button searchbtn;

		protected LinkButton btn_DelBat;

		protected LinkButton btn_Export;

		protected DropDownList drpPageSize;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Orders.CancelExpireOrder(0);
			Orders.AutoSignOrder(0);
			this.strStatus = WebUtils.GetQueryString("orderstatus");
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " AutoID DESC ";
			this.SinGooPager1.PageSize = WebUtils.GetInt(this.drpPageSize.SelectedValue);
			this.Repeater1.DataSource = Orders.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
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
			int queryInt = WebUtils.GetQueryInt("orderstatus");
			if (!string.IsNullOrEmpty(this.strStatus))
			{
				if (queryInt == 12)
				{
					text += " AND ( OrderStatus>11 and OrderStatus<=99 and exists(select 1 from shop_OrderItem where OrderID=shop_Orders.AutoID and IsEva=0) ) ";
				}
				else
				{
					text = text + " AND OrderStatus=" + queryInt.ToString();
				}
			}
			int @int = WebUtils.GetInt(this.ddlType.SelectedValue);
			if (@int != -1)
			{
				text = text + " AND OrderType=" + @int.ToString();
			}
			System.DateTime dateTime = WebUtils.GetDateTime(this.timestart.Text);
			System.DateTime dateTime2 = WebUtils.GetDateTime(this.timeend.Text);
			if (dateTime > new System.DateTime(1900, 1, 1))
			{
				text = text + " AND OrderAddTime>=convert(datetime,'" + dateTime.ToString("yyyy-MM-dd") + "') ";
			}
			if (dateTime2 > new System.DateTime(1900, 1, 1))
			{
				text = text + " AND OrderAddTime<=convert(datetime,'" + dateTime2.ToString("yyyy-MM-dd") + "') ";
			}
			if (!string.IsNullOrEmpty(this.search_text.Text))
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" AND (OrderNo like '%",
					WebUtils.GetString(this.search_text.Text),
					"%' OR UserName like '%",
					WebUtils.GetString(this.search_text.Text),
					"%' ) "
				});
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

		protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HiddenField hiddenField = e.Item.FindControl("hdfStatus") as HiddenField;
				int @int = WebUtils.GetInt(hiddenField.Value);
				OrderStatus orderStatus = (OrderStatus)@int;
				int int2 = WebUtils.GetInt((e.Item.FindControl("autoid") as HiddenField).Value);
				LinkButton linkButton = (LinkButton)e.Item.FindControl("lnk_Confirm");
				LinkButton linkButton2 = (LinkButton)e.Item.FindControl("lnk_ChangeAmount");
				LinkButton linkButton3 = (LinkButton)e.Item.FindControl("lnk_Pay");
				LinkButton linkButton4 = (LinkButton)e.Item.FindControl("lnk_SendGoods");
				LinkButton linkButton5 = (LinkButton)e.Item.FindControl("lnk_SignGoods");
				LinkButton linkButton6 = (LinkButton)e.Item.FindControl("lnk_Cancel");
				LinkButton linkButton7 = (LinkButton)e.Item.FindControl("lnk_Del");
				OrderStatus orderStatus2 = orderStatus;
				switch (orderStatus2)
				{
				case OrderStatus.WaitAudit:
					if (base.IsAuthorizedOp("Delete"))
					{
						linkButton7.Visible = true;
					}
					if (base.IsAuthorizedOp("Audit"))
					{
						linkButton.Visible = true;
						linkButton.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=Audit&opid=",
							int2.ToString(),
							"',{title:'确认订单',width:600,height:185},false);"
						});
					}
					if (base.IsAuthorizedOp("Cancel"))
					{
						linkButton6.Visible = true;
						linkButton6.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=Cancel&opid=",
							int2.ToString(),
							"',{title:'关闭订单',width:680,height:185},false);"
						});
					}
					break;
				case OrderStatus.WaitPay:
					if (base.IsAuthorizedOp("Cancel"))
					{
						linkButton6.Visible = true;
						linkButton6.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=Cancel&opid=",
							int2.ToString(),
							"',{title:'关闭订单',width:600,height:185},false);"
						});
					}
					if (base.IsAuthorizedOp("Delete"))
					{
						linkButton7.Visible = true;
					}
					if (base.IsAuthorizedOp("ChangeAmount"))
					{
						linkButton2.Visible = true;
						linkButton2.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=ChangeAmount&opid=",
							int2.ToString(),
							"',{title:'订单改价',width:600,height:275},false);"
						});
					}
					if (base.IsAuthorizedOp("Pay"))
					{
						linkButton3.Visible = true;
						linkButton3.OnClientClick = string.Concat(new string[]
						{
							" $.dialog.open('OrderAction.aspx?Module=",
							base.CurrentModuleCode,
							"&action=Pay&opid=",
							int2.ToString(),
							"',{title:'确认收款',width:600,height:235},false);"
						});
					}
					break;
				default:
					switch (orderStatus2)
					{
					case OrderStatus.WaitDelivery:
						if (base.IsAuthorizedOp("SendGoods"))
						{
							linkButton4.Visible = true;
							linkButton4.OnClientClick = string.Concat(new string[]
							{
								" $.dialog.open('OrderAction.aspx?Module=",
								base.CurrentModuleCode,
								"&action=SendGoods&opid=",
								int2.ToString(),
								"',{title:'订单发货',width:600,height:310},false);"
							});
						}
						break;
					case OrderStatus.WaitSignGoods:
						if (base.IsAuthorizedOp("SignGoods"))
						{
							linkButton5.Visible = true;
							linkButton5.OnClientClick = string.Concat(new string[]
							{
								" $.dialog.open('OrderAction.aspx?Module=",
								base.CurrentModuleCode,
								"&action=SignGoods&opid=",
								int2.ToString(),
								"',{title:'订单送达',width:600,height:185},false);"
							});
						}
						break;
					default:
						switch (orderStatus2)
						{
						case OrderStatus.OrderSuccess:
							if (base.IsAuthorizedOp("Delete"))
							{
								linkButton7.Visible = true;
							}
							break;
						case OrderStatus.OrderCancel:
							if (base.IsAuthorizedOp("Delete"))
							{
								linkButton7.Visible = true;
							}
							break;
						}
						break;
					}
					break;
				}
			}
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
				OrdersInfo dataById = Orders.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else if (dataById.OrderStatus != 101 && dataById.OrderStatus != 99)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "只有关闭的或者完结的订单才能删除");
				}
				else if (Orders.Delete(@int))
				{
					OrderItem.DeleteItemByOrderID(@int);
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除订单[" + dataById.OrderNo + "] thành công");
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
				string text = string.Empty;
				System.Collections.Generic.IList<OrdersInfo> list = Orders.GetList(1000, " AutoID in (" + repeaterCheckIDs + ") AND OrderStatus>=99 ");
				if (list != null && list.Count > 0)
				{
					foreach (OrdersInfo current in list)
					{
						text = text + current.AutoID.ToString() + ",";
					}
					text = text.TrimEnd(new char[]
					{
						','
					});
					if (PageBase.dbo.DeleteTable(" delete from shop_Orders where AutoID in (" + text + ") "))
					{
						PageBase.dbo.DeleteTable(" delete from shop_OrderItem where OrderID in (" + text + ") ");
						this.BindData();
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "批量删除订单(ID:" + text + ")成功");
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
					}
					else
					{
						base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
					}
				}
			}
		}

		protected void btn_Export_Click(object sender, System.EventArgs e)
		{
			DataTable dataTable = PageBase.dbo.GetDataTable("SELECT AutoID AS 自动编号,OrderNo AS 订单编号,UserName AS 会员名称,  CASE OrderStatus WHEN 0 THEN '待审核' WHEN 1 THEN '待付款' WHEN 10 THEN '配货中' WHEN 11 THEN '已发货' WHEN 12 THEN '已签收' WHEN 99 THEN '已完结' WHEN -1 THEN '订单作废' WHEN -2 THEN '退货' END  AS 订单状态,   AddOrderMethod AS 添加方式,Consignee AS 收件人,Province AS 省份,City AS 城市,   Address AS 地址,Phone AS 电话,Mobile AS 手机,PayName AS 支付方式,Remark AS 备注,   OrderTotalAmount AS 订单金额,OrderShippingFee AS 订单运费,OrderAddTime AS 创建时间   FROM shop_Orders WHERE " + this.GetCondition());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				string path = base.Server.MapPath(base.ExportFolder + "Orders.xls");
				DataToXSL.CreateXLS(dataTable, path, true);
				base.Response.Redirect("/include/download?file=" + DEncryptUtils.DESEncode(base.ExportFolder + "Orders.xls"));
			}
			else
			{
				base.ShowMsg("没有找到任何记录");
			}
		}
	}
}
