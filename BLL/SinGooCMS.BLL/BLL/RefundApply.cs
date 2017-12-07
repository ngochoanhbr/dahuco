using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class RefundApply : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_RefundApply ");
			}
		}

		public static int Add(RefundApplyInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<RefundApplyInfo>(entity);
			}
			return result;
		}

		public static bool Update(RefundApplyInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<RefundApplyInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_RefundApply WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_RefundApply WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static RefundApplyInfo GetDataById(int intPrimaryKeyIDValue)
		{
			RefundApplyInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<RefundApplyInfo>(" SELECT TOP 1 AutoID,OrderNo,ProductName,Reason,Amount,RefundAmountType,RefundGoodsNum,RefunderName,RefunderPhone,AdminRemark,Status,AutoTimeStamp FROM shop_RefundApply WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static RefundApplyInfo GetTopData()
		{
			return RefundApply.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static RefundApplyInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderNo,ProductName,Reason,Amount,RefundAmountType,RefundGoodsNum,RefunderName,RefunderPhone,AdminRemark,Status,AutoTimeStamp FROM shop_RefundApply ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<RefundApplyInfo>(text);
		}

		public static IList<RefundApplyInfo> GetAllList()
		{
			return RefundApply.GetList(0, string.Empty);
		}

		public static IList<RefundApplyInfo> GetTopNList(int intTopCount)
		{
			return RefundApply.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<RefundApplyInfo> GetTopNList(int intTopCount, string strSort)
		{
			return RefundApply.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<RefundApplyInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return RefundApply.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<RefundApplyInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderNo,ProductName,Reason,Amount,RefundAmountType,RefundGoodsNum,RefunderName,RefunderPhone,AdminRemark,Status,AutoTimeStamp from shop_RefundApply ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<RefundApplyInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_RefundApply", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_RefundApply", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return RefundApply.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return RefundApply.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return RefundApply.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return RefundApply.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderNo,ProductName,Reason,Amount,RefundAmountType,RefundGoodsNum,RefunderName,RefunderPhone,AdminRemark,Status,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_RefundApply";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<RefundApplyInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return RefundApply.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<RefundApplyInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<RefundApplyInfo> result = new List<RefundApplyInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderNo,ProductName,Reason,Amount,RefundAmountType,RefundGoodsNum,RefunderName,RefunderPhone,AdminRemark,Status,AutoTimeStamp";
			pager.PagerTable = "shop_RefundApply";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<RefundApplyInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_RefundApply SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_RefundApply SET Sort =",
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
