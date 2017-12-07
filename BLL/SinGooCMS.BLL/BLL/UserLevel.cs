using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class UserLevel : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_UserLevel ");
			}
		}

		public static int Add(UserLevelInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<UserLevelInfo>(entity);
			}
			return result;
		}

		public static bool Update(UserLevelInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<UserLevelInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_UserLevel WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_UserLevel WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static UserLevelInfo GetDataById(int intPrimaryKeyIDValue)
		{
			UserLevelInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserLevelInfo>(" SELECT TOP 1 AutoID,LevelName,Integral,Discount,LevelDesc,Sort,AutoTimeStamp FROM cms_UserLevel WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static UserLevelInfo GetTopData()
		{
			return UserLevel.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static UserLevelInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,LevelName,Integral,Discount,LevelDesc,Sort,AutoTimeStamp FROM cms_UserLevel ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<UserLevelInfo>(text);
		}

		public static IList<UserLevelInfo> GetAllList()
		{
			return UserLevel.GetList(0, string.Empty);
		}

		public static IList<UserLevelInfo> GetTopNList(int intTopCount)
		{
			return UserLevel.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<UserLevelInfo> GetTopNList(int intTopCount, string strSort)
		{
			return UserLevel.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<UserLevelInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return UserLevel.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<UserLevelInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,LevelName,Integral,Discount,LevelDesc,Sort,AutoTimeStamp from cms_UserLevel ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<UserLevelInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_UserLevel", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_UserLevel", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return UserLevel.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserLevel.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserLevel.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserLevel.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,LevelName,Integral,Discount,LevelDesc,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_UserLevel";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<UserLevelInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return UserLevel.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<UserLevelInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<UserLevelInfo> result = new List<UserLevelInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,LevelName,Integral,Discount,LevelDesc,Sort,AutoTimeStamp";
			pager.PagerTable = "cms_UserLevel";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<UserLevelInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_UserLevel SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_UserLevel SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<UserLevelInfo> GetCacheUserLevelList()
		{
			IList<UserLevelInfo> list = CacheUtils.Get("JsonLeeCMS_CacheForGetUserLevel") as List<UserLevelInfo>;
			if (list == null)
			{
				list = UserLevel.GetAllList();
				CacheUtils.Insert("JsonLeeCMS_CacheForGetUserLevel", list);
			}
			return list;
		}

		public static UserLevelInfo GetCacheUserLevelById(int intUserLevelID)
		{
			IList<UserLevelInfo> cacheUserLevelList = UserLevel.GetCacheUserLevelList();
			UserLevelInfo result;
			if (cacheUserLevelList != null && cacheUserLevelList.Count > 0)
			{
				result = (from p in cacheUserLevelList
				where p.AutoID.Equals(intUserLevelID)
				select p).FirstOrDefault<UserLevelInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
