using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class OrderAction : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_OrderAction ");
			}
		}

		public static int AddLog(OrdersInfo order, string strOperator, string strEventContent)
		{
			return OrderAction.AddLog(order, strOperator, strEventContent, string.Empty);
		}

		public static int AddLog(OrdersInfo order, string strOperator, string strEventContent, string strCase)
		{
			return OrderAction.Add(new OrderActionInfo
			{
				OrderID = order.AutoID,
				Operator = strOperator,
				OrderStatus = order.OrderStatus,
				ActionNote = strEventContent,
				Remark = strCase,
				AutoTimeStamp = DateTime.Now
			});
		}

		public static int Add(OrderActionInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<OrderActionInfo>(entity);
			}
			return result;
		}

		public static bool Update(OrderActionInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<OrderActionInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_OrderAction WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_OrderAction WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static OrderActionInfo GetDataById(int intPrimaryKeyIDValue)
		{
			OrderActionInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<OrderActionInfo>(" SELECT TOP 1 AutoID,OrderID,Operator,OrderStatus,ActionNote,Remark,AutoTimeStamp FROM shop_OrderAction WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static OrderActionInfo GetTopData()
		{
			return OrderAction.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static OrderActionInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderID,Operator,OrderStatus,ActionNote,Remark,AutoTimeStamp FROM shop_OrderAction ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<OrderActionInfo>(text);
		}

		public static IList<OrderActionInfo> GetAllList()
		{
			return OrderAction.GetList(0, string.Empty);
		}

		public static IList<OrderActionInfo> GetTopNList(int intTopCount)
		{
			return OrderAction.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<OrderActionInfo> GetTopNList(int intTopCount, string strSort)
		{
			return OrderAction.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<OrderActionInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return OrderAction.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<OrderActionInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderID,Operator,OrderStatus,ActionNote,Remark,AutoTimeStamp from shop_OrderAction ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<OrderActionInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_OrderAction", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_OrderAction", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return OrderAction.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderAction.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderAction.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderAction.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderID,Operator,OrderStatus,ActionNote,Remark,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_OrderAction";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<OrderActionInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return OrderAction.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<OrderActionInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<OrderActionInfo> result = new List<OrderActionInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderID,Operator,OrderStatus,ActionNote,Remark,AutoTimeStamp";
			pager.PagerTable = "shop_OrderAction";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<OrderActionInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_OrderAction SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_OrderAction SET Sort =",
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
