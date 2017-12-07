using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinGooCMS.WebUI.User
{
    public class MyOrders : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.ReturnUrl = base.Request.RawUrl;
			if (base.IsPost)
			{
				this.Action = WebUtils.GetFormString("action").ToLower();
				OrdersInfo dataById = Orders.GetDataById(WebUtils.GetFormInt("oid"));
				if (!string.IsNullOrEmpty(this.Action) && dataById != null)
				{
					string action = this.Action;
					if (action != null)
					{
						if (!(action == "sign"))
						{
							if (!(action == "cancel"))
							{
								if (action == "delete")
								{
									if (dataById != null && dataById.OrderStatus >= 99)
									{
										Orders.Delete(dataById.AutoID);
										OrderItem.DeleteItemByOrderID(dataById.AutoID);
										base.WriteJsonTip(true, base.GetCaption("OperationSuccess"), UrlRewrite.Get("myorders_url"));
									}
									else
									{
										base.WriteJsonTip(base.GetCaption("Order_StatusIncorrect"));
									}
								}
							}
							else if (dataById != null && dataById.OrderStatus <= 1)
							{
								System.Collections.Generic.IList<OrderItemInfo> listByOID = OrderItem.GetListByOID(dataById.AutoID);
								if (listByOID.Count > 0)
								{
									foreach (OrderItemInfo current in listByOID)
									{
										Orders.ReBackStock(Product.GetDataById(current.ProID), GoodsSpecify.Get(current.ProID, current.GuiGePath), current.Quantity);
									}
								}
								OrderAction.AddLog(dataById, base.UserName, "会员[" + base.UserName + "]取消了订单");
								base.WriteJsonTip(true, base.GetCaption("OperationSuccess"), UrlRewrite.Get("myorders_url"));
							}
							else
							{
								base.WriteJsonTip(base.GetCaption("Order_StatusIncorrect"));
							}
						}
						else if (dataById != null && dataById.OrderStatus == 11)
						{
							dataById.GoodsServedTime = System.DateTime.Now;
							dataById.OrderFinishTime = System.DateTime.Now;
							dataById.OrderStatus = 99;
							if (Orders.Update(dataById))
							{
								OrderAction.AddLog(dataById, base.UserName, "会员[" + base.UserName + "]签收了订单");
								base.WriteJsonTip(true, base.GetCaption("OperationSuccess"), UrlRewrite.Get("myorders_url"));
							}
							else
							{
								base.WriteJsonTip(base.GetCaption("Order_StatusIncorrect"));
							}
						}
						else
						{
							base.WriteJsonTip(base.GetCaption("Order_StatusIncorrect"));
						}
					}
				}
			}
			else
			{
				Orders.CancelExpireOrder(base.UserID);
				Orders.AutoSignOrder(base.UserID);
				this.Condition = this.Condition + " 1=1 AND UserID=" + base.UserID.ToString();
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				System.DateTime dateTime = WebUtils.GetQueryDatetime("filter_dtstart");
				System.DateTime dateTime2 = WebUtils.GetQueryDatetime("filter_dtend");
				if (dateTime == new System.DateTime(1900, 1, 1))
				{
					dateTime = System.DateTime.Now.AddYears(-1);
				}
				if (dateTime2 == new System.DateTime(1900, 1, 1))
				{
					dateTime2 = System.DateTime.Now;
				}
				if (dateTime2 < dateTime)
				{
					dateTime2 = dateTime.AddMonths(1);
				}
				if (dateTime2 > dateTime.AddYears(5))
				{
					dateTime2 = dateTime.AddYears(5);
					this.Alert("仅允许查询5年之内的数据");
				}
				int queryInt = WebUtils.GetQueryInt("filter_status", -1);
				int queryInt2 = WebUtils.GetQueryInt("filter_ordertype", -1);
				string queryString = WebUtils.GetQueryString("filter_searchkey");
				base.Put("filter_dtstart", dateTime.ToString("yyyy-MM-dd"));
				base.Put("filter_dtend", dateTime2.ToString("yyyy-MM-dd"));
				base.Put("filter_status", queryInt);
				base.Put("filter_ordertype", queryInt2);
				base.Put("filter_searchkey", queryString);
				stringBuilder.Append(string.Concat(new string[]
				{
					" AND OrderAddTime between CONVERT(datetime,'",
					dateTime.ToString("yyyy-MM-dd"),
					" 00:00:00') and CONVERT(datetime,'",
					dateTime2.ToString("yyyy-MM-dd"),
					" 23:59:59') "
				}));
				if (queryInt != -1)
				{
					if (queryInt == 12)
					{
						stringBuilder.Append(" AND OrderStatus>11 and OrderStatus<=99 and exists(select 1 from shop_OrderItem where OrderID=shop_Orders.AutoID and IsEva=0) ");
					}
					else
					{
						stringBuilder.Append(" AND OrderStatus= " + queryInt);
					}
				}
				if (queryInt2 != -1)
				{
					stringBuilder.Append(" AND OrderType= " + queryInt2);
				}
				if (!string.IsNullOrEmpty(queryString))
				{
					stringBuilder.Append(" AND OrderNo like '%" + queryString + "%' ");
				}
				this.Condition += stringBuilder.ToString();
				this.UrlPattern = string.Concat(new object[]
				{
					UrlRewrite.Get("myorders_url"),
					"?filter_dtstart=",
					dateTime.ToString("yyyy-MM-dd"),
					"&filter_dtend=",
					dateTime2.ToString("yyyy-MM-dd"),
					"&filter_status=",
					queryInt,
					"&filter_ordertype=",
					queryInt2,
					"&filter_searchkey=",
					queryString,
					"&page=$page"
				});
				OrderStatusSTAT orderStatusSTAT = new OrderStatusSTAT(base.UserID);
				base.Put("AllCount", orderStatusSTAT.AllCount);
				base.Put("DaiFKCount", orderStatusSTAT.WaitPayCount);
				base.Put("DaiFHCount", orderStatusSTAT.WaitSendGoodsCount);
				base.Put("DaiSHCount", orderStatusSTAT.WaitSignGoodsCount);
				base.Put("DaiPJCount", orderStatusSTAT.WaitEvaCount);
				base.Put("SuccessCount", orderStatusSTAT.SuccessCount);
				base.AutoPageing<OrdersInfo>(new OrdersInfo());
				base.UsingClient("user/我的订单.html");
			}
		}
	}
}
