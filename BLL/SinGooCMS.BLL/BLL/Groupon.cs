using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Groupon : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Groupon ");
			}
		}

		public static bool UpdateJoinNum(int intGroupID, int intAddNum)
		{
			return BizBase.dbo.UpdateTable(string.Concat(new object[]
			{
				" UPDATE shop_Groupon SET JoinNum=JoinNum+",
				intAddNum,
				" WHERE AutoID=",
				intGroupID.ToString()
			}));
		}

		public static int Add(GrouponInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<GrouponInfo>(entity);
			}
			return result;
		}

		public static bool Update(GrouponInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<GrouponInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Groupon WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Groupon WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static GrouponInfo GetDataById(int intPrimaryKeyIDValue)
		{
			GrouponInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<GrouponInfo>(" SELECT TOP 1 AutoID,ProID,ActName,ActImage,StartTime,EndTime,PriceLadder,JoinNum,CashDeposit,ActDesc,Sort,AutoTimeStamp FROM shop_Groupon WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static GrouponInfo GetTopData()
		{
			return Groupon.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static GrouponInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ProID,ActName,ActImage,StartTime,EndTime,PriceLadder,JoinNum,CashDeposit,ActDesc,Sort,AutoTimeStamp FROM shop_Groupon ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<GrouponInfo>(text);
		}

		public static IList<GrouponInfo> GetAllList()
		{
			return Groupon.GetList(0, string.Empty);
		}

		public static IList<GrouponInfo> GetTopNList(int intTopCount)
		{
			return Groupon.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<GrouponInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Groupon.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<GrouponInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Groupon.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<GrouponInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ProID,ActName,ActImage,StartTime,EndTime,PriceLadder,JoinNum,CashDeposit,ActDesc,Sort,AutoTimeStamp from shop_Groupon ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<GrouponInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Groupon", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Groupon", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Groupon.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Groupon.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Groupon.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Groupon.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ProID,ActName,ActImage,StartTime,EndTime,PriceLadder,JoinNum,CashDeposit,ActDesc,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Groupon";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<GrouponInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Groupon.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<GrouponInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<GrouponInfo> result = new List<GrouponInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ProID,ActName,ActImage,StartTime,EndTime,PriceLadder,JoinNum,CashDeposit,ActDesc,Sort,AutoTimeStamp";
			pager.PagerTable = "shop_Groupon";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<GrouponInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Groupon SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Groupon SET Sort =",
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
