using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class SearchRank : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_SearchRank ");
			}
		}

		public static int Add(SearchRankInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<SearchRankInfo>(entity);
			}
			return result;
		}

		public static bool Update(SearchRankInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<SearchRankInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_SearchRank WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_SearchRank WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static SearchRankInfo GetDataById(int intPrimaryKeyIDValue)
		{
			SearchRankInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<SearchRankInfo>(" SELECT TOP 1 AutoID,SearchKey,Times,IsRecommend FROM shop_SearchRank WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static SearchRankInfo GetTopData()
		{
			return SearchRank.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static SearchRankInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,SearchKey,Times,IsRecommend FROM shop_SearchRank ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<SearchRankInfo>(text);
		}

		public static IList<SearchRankInfo> GetAllList()
		{
			return SearchRank.GetList(0, string.Empty);
		}

		public static IList<SearchRankInfo> GetTopNList(int intTopCount)
		{
			return SearchRank.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<SearchRankInfo> GetTopNList(int intTopCount, string strSort)
		{
			return SearchRank.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<SearchRankInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return SearchRank.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<SearchRankInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,SearchKey,Times,IsRecommend from shop_SearchRank ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<SearchRankInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_SearchRank", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_SearchRank", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return SearchRank.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SearchRank.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SearchRank.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SearchRank.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,SearchKey,Times,IsRecommend";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_SearchRank";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<SearchRankInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return SearchRank.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<SearchRankInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<SearchRankInfo> result = new List<SearchRankInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,SearchKey,Times,IsRecommend";
			pager.PagerTable = "shop_SearchRank";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<SearchRankInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_SearchRank SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_SearchRank SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static void Save(string key)
		{
			SearchRankInfo searchRankInfo = SearchRank.Get(key);
			if (searchRankInfo != null)
			{
				searchRankInfo.Times++;
				SearchRank.Update(searchRankInfo);
			}
			else
			{
				SearchRank.Add(new SearchRankInfo
				{
					SearchKey = key,
					Times = 1,
					IsRecommend = false
				});
			}
		}

		public static SearchRankInfo Get(string key)
		{
			return BizBase.dbo.GetModel<SearchRankInfo>(" select top 1 * from shop_SearchRank where SearchKey='" + StringUtils.ChkSQL(key) + "' order by AutoID desc ");
		}
	}
}
