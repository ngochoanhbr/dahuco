using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class ExchangeApply : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_ExchangeApply ");
			}
		}

		public static int Add(ExchangeApplyInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<ExchangeApplyInfo>(entity);
			}
			return result;
		}

		public static bool Update(ExchangeApplyInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<ExchangeApplyInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_ExchangeApply WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_ExchangeApply WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ExchangeApplyInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ExchangeApplyInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ExchangeApplyInfo>(" SELECT TOP 1 AutoID,OrderNo,GoodsName,Reason,Consignee,ShippingAddr,ContactPhone,AdminRemark,Status,AutoTimeStamp FROM shop_ExchangeApply WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ExchangeApplyInfo GetTopData()
		{
			return ExchangeApply.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ExchangeApplyInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderNo,GoodsName,Reason,Consignee,ShippingAddr,ContactPhone,AdminRemark,Status,AutoTimeStamp FROM shop_ExchangeApply ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ExchangeApplyInfo>(text);
		}

		public static IList<ExchangeApplyInfo> GetAllList()
		{
			return ExchangeApply.GetList(0, string.Empty);
		}

		public static IList<ExchangeApplyInfo> GetTopNList(int intTopCount)
		{
			return ExchangeApply.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ExchangeApplyInfo> GetTopNList(int intTopCount, string strSort)
		{
			return ExchangeApply.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ExchangeApplyInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return ExchangeApply.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ExchangeApplyInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderNo,GoodsName,Reason,Consignee,ShippingAddr,ContactPhone,AdminRemark,Status,AutoTimeStamp from shop_ExchangeApply ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ExchangeApplyInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ExchangeApply", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ExchangeApply", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return ExchangeApply.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ExchangeApply.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ExchangeApply.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ExchangeApply.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderNo,GoodsName,Reason,Consignee,ShippingAddr,ContactPhone,AdminRemark,Status,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_ExchangeApply";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ExchangeApplyInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return ExchangeApply.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ExchangeApplyInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ExchangeApplyInfo> result = new List<ExchangeApplyInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderNo,GoodsName,Reason,Consignee,ShippingAddr,ContactPhone,AdminRemark,Status,AutoTimeStamp";
			pager.PagerTable = "shop_ExchangeApply";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ExchangeApplyInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_ExchangeApply SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_ExchangeApply SET Sort =",
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
