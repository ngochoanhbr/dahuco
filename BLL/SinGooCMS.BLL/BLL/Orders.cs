using SinGooCMS.Config;
using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Orders : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Orders ");
			}
		}

		public static string GetOrderNo()
		{
			object obj = BizBase.dbo.ExecProcReValue("p_System_GetOrderNo", null);
			return (obj == null) ? string.Empty : obj.ToString();
		}

		public static OrdersInfo GetOrderBySN(string strOrderSN)
		{
			return BizBase.dbo.GetModel<OrdersInfo>(" SELECT TOP 1 * FROM shop_Orders WHERE OrderNo='" + strOrderSN + "' ");
		}

		public static void CancelExpireOrder(int intUserID)
		{
			DataTable dataTable = BizBase.dbo.GetDataTable(string.Concat(new object[]
			{
				" select ProID,GuiGePath,SUM(Quantity) as QuantityTotal from(         select * from shop_OrderItem where OrderID in         ( select AutoID from shop_orders where ",
				(intUserID > 0) ? ("UserID=" + intUserID + " and ") : "",
				" OrderStatus=",
				1,
				" and GETDATE()>DATEADD(hour,",
				ConfigProvider.Configs.BuyPayExpire,
				",OrderAddTime))    ) as A group by ProID,GuiGePath "
			}));
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				BizBase.dbo.InsertNoReValue(string.Concat(new object[]
				{
					" insert into shop_OrderAction(OrderID,Operator,OrderStatus,ActionNote,Remark,AutoTimeStamp) select AutoID,'系统',",
					101,
					",'订单创建后超过",
					ConfigProvider.Configs.BuyPayExpire,
					"小时未付款，系统自动关闭订单！','',getdate() from shop_orders where ",
					(intUserID > 0) ? (" UserID=" + intUserID + " and ") : "",
					" OrderStatus=",
					1,
					" and GETDATE()>DATEADD(hour,",
					ConfigProvider.Configs.BuyPayExpire,
					",OrderAddTime) "
				}));
				BizBase.dbo.UpdateTable(string.Concat(new object[]
				{
					" update shop_orders set OrderStatus=",
					101,
					" where ",
					(intUserID > 0) ? (" UserID=" + intUserID + " and ") : "",
					" OrderStatus=",
					1,
					" and GETDATE()>DATEADD(hour,",
					ConfigProvider.Configs.BuyPayExpire,
					",OrderAddTime) "
				}));
				foreach (DataRow dataRow in dataTable.Rows)
				{
					Orders.ReBackStock(Product.GetDataById(WebUtils.GetInt(dataRow["ProID"])), GoodsSpecify.Get(WebUtils.GetInt(dataRow["ProID"]), dataRow["GuiGePath"].ToString()), WebUtils.GetInt(dataRow["QuantityTotal"]));
				}
			}
		}

		public static void AutoSignOrder(int userID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@UserID", userID),
				new SqlParameter("@ExpireDay", ConfigProvider.Configs.SignExpire)
			};
			BizBase.dbo.ExecProc("p_System_AutoSignOrder", arrParam);
		}

		public static void ReBackStock(ProductInfo pro, GoodsSpecifyInfo goodsattr, int intQuantity)
		{
			if (pro != null)
			{
				if (goodsattr != null)
				{
					goodsattr.Stock += intQuantity;
					GoodsSpecify.Update(goodsattr);
				}
				pro.Stock += intQuantity;
				BizBase.dbo.UpdateTable(string.Concat(new object[]
				{
					" update shop_Product set Stock=Stock+",
					intQuantity,
					" where AutoID= ",
					pro.AutoID
				}));
			}
		}

		public static DataTable GetOrderStatusSTAT(int intUserID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@UserID", intUserID)
			};
			return BizBase.dbo.ExecProcReDT("p_System_GetOrderStatusSTAT", arrParam);
		}

		public static int Add(OrdersInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<OrdersInfo>(entity);
			}
			return result;
		}

		public static bool Update(OrdersInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<OrdersInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Orders WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Orders WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static OrdersInfo GetDataById(int intPrimaryKeyIDValue)
		{
			OrdersInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<OrdersInfo>(" SELECT TOP 1 AutoID,OrderNo,OrderType,UserID,UserName,AddOrderMethod,CouponsID,CouponsValue,GoodsTotalAmout,OrderShippingFee,InsuranceFee,OtherFee,OrderTotalAmount,Consignee,Country,Province,City,County,Address,PostCode,Phone,Mobile,ShippingID,ShippingName,ShippingNo,PayID,PayName,TradeNo,SendGoodsNeedNotice,NeedInvoice,InvoiceTitle,InvoiceContent,Invoiced,RecInvoice,Integral,CustomerMsg,Remark,OrderStatus,OrderAddTime,OrderAuditTime,OrderPayTime,GoodsSendTime,GoodsServedTime,OrderFinishTime,LastModifyTime FROM shop_Orders WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static OrdersInfo GetTopData()
		{
			return Orders.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static OrdersInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderNo,OrderType,UserID,UserName,AddOrderMethod,CouponsID,CouponsValue,GoodsTotalAmout,OrderShippingFee,InsuranceFee,OtherFee,OrderTotalAmount,Consignee,Country,Province,City,County,Address,PostCode,Phone,Mobile,ShippingID,ShippingName,ShippingNo,PayID,PayName,TradeNo,SendGoodsNeedNotice,NeedInvoice,InvoiceTitle,InvoiceContent,Invoiced,RecInvoice,Integral,CustomerMsg,Remark,OrderStatus,OrderAddTime,OrderAuditTime,OrderPayTime,GoodsSendTime,GoodsServedTime,OrderFinishTime,LastModifyTime FROM shop_Orders ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<OrdersInfo>(text);
		}

		public static IList<OrdersInfo> GetAllList()
		{
			return Orders.GetList(0, string.Empty);
		}

		public static IList<OrdersInfo> GetTopNList(int intTopCount)
		{
			return Orders.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<OrdersInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Orders.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<OrdersInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Orders.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<OrdersInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderNo,OrderType,UserID,UserName,AddOrderMethod,CouponsID,CouponsValue,GoodsTotalAmout,OrderShippingFee,InsuranceFee,OtherFee,OrderTotalAmount,Consignee,Country,Province,City,County,Address,PostCode,Phone,Mobile,ShippingID,ShippingName,ShippingNo,PayID,PayName,TradeNo,SendGoodsNeedNotice,NeedInvoice,InvoiceTitle,InvoiceContent,Invoiced,RecInvoice,Integral,CustomerMsg,Remark,OrderStatus,OrderAddTime,OrderAuditTime,OrderPayTime,GoodsSendTime,GoodsServedTime,OrderFinishTime,LastModifyTime from shop_Orders ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<OrdersInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Orders", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Orders", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Orders.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Orders.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Orders.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Orders.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderNo,OrderType,UserID,UserName,AddOrderMethod,CouponsID,CouponsValue,GoodsTotalAmout,OrderShippingFee,InsuranceFee,OtherFee,OrderTotalAmount,Consignee,Country,Province,City,County,Address,PostCode,Phone,Mobile,ShippingID,ShippingName,ShippingNo,PayID,PayName,TradeNo,SendGoodsNeedNotice,NeedInvoice,InvoiceTitle,InvoiceContent,Invoiced,RecInvoice,Integral,CustomerMsg,Remark,OrderStatus,OrderAddTime,OrderAuditTime,OrderPayTime,GoodsSendTime,GoodsServedTime,OrderFinishTime,LastModifyTime";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Orders";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<OrdersInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Orders.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<OrdersInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<OrdersInfo> result = new List<OrdersInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderNo,OrderType,UserID,UserName,AddOrderMethod,CouponsID,CouponsValue,GoodsTotalAmout,OrderShippingFee,InsuranceFee,OtherFee,OrderTotalAmount,Consignee,Country,Province,City,County,Address,PostCode,Phone,Mobile,ShippingID,ShippingName,ShippingNo,PayID,PayName,TradeNo,SendGoodsNeedNotice,NeedInvoice,InvoiceTitle,InvoiceContent,Invoiced,RecInvoice,Integral,CustomerMsg,Remark,OrderStatus,OrderAddTime,OrderAuditTime,OrderPayTime,GoodsSendTime,GoodsServedTime,OrderFinishTime,LastModifyTime";
			pager.PagerTable = "shop_Orders";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<OrdersInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Orders SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
		}

		public static bool UpdateSort(Dictionary<int, int> dicIDAndSort)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dicIDAndSort.Count > 0)
			{
				foreach (KeyValuePair<int, int> current in dicIDAndSort)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						" UPDATE shop_Orders SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}
	}
}
