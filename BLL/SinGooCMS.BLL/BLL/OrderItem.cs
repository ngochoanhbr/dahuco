using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class OrderItem : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_OrderItem ");
			}
		}

		public static void DeleteItemByOrderID(int intOrderID)
		{
			BizBase.dbo.ExecSQL("DELETE FROM shop_OrderItem WHERE OrderID=" + intOrderID);
		}

		public static IList<OrderItemInfo> GetListByOID(int intOrderID)
		{
			return OrderItem.GetList(0, "OrderID=" + intOrderID.ToString(), "AutoID DESC");
		}

		public static int Add(OrderItemInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<OrderItemInfo>(entity);
			}
			return result;
		}

		public static bool Update(OrderItemInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<OrderItemInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_OrderItem WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_OrderItem WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static OrderItemInfo GetDataById(int intPrimaryKeyIDValue)
		{
			OrderItemInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<OrderItemInfo>(" SELECT TOP 1 AutoID,OrderID,ProID,ProName,ProSN,ProImg,Price,Integral,Quantity,SubTotal,GuiGePath,IsVirtual,IsEva,Remark,AutoTimeStamp FROM shop_OrderItem WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static OrderItemInfo GetTopData()
		{
			return OrderItem.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static OrderItemInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderID,ProID,ProName,ProSN,ProImg,Price,Integral,Quantity,SubTotal,GuiGePath,IsVirtual,IsEva,Remark,AutoTimeStamp FROM shop_OrderItem ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<OrderItemInfo>(text);
		}

		public static IList<OrderItemInfo> GetAllList()
		{
			return OrderItem.GetList(0, string.Empty);
		}

		public static IList<OrderItemInfo> GetTopNList(int intTopCount)
		{
			return OrderItem.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<OrderItemInfo> GetTopNList(int intTopCount, string strSort)
		{
			return OrderItem.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<OrderItemInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return OrderItem.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<OrderItemInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderID,ProID,ProName,ProSN,ProImg,Price,Integral,Quantity,SubTotal,GuiGePath,IsVirtual,IsEva,Remark,AutoTimeStamp from shop_OrderItem ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<OrderItemInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_OrderItem", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_OrderItem", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return OrderItem.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderItem.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderItem.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderItem.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderID,ProID,ProName,ProSN,ProImg,Price,Integral,Quantity,SubTotal,GuiGePath,IsVirtual,IsEva,Remark,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_OrderItem";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<OrderItemInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderItem.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<OrderItemInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<OrderItemInfo> result = new List<OrderItemInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderID,ProID,ProName,ProSN,ProImg,Price,Integral,Quantity,SubTotal,GuiGePath,IsVirtual,IsEva,Remark,AutoTimeStamp";
			pager.PagerTable = "shop_OrderItem";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<OrderItemInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_OrderItem SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_OrderItem SET Sort =",
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
