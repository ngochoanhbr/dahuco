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
	public class SettingCategory : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_SettingCategory ");
			}
		}

		public static IList<SettingCategoryInfo> GetCacheSettingCategoryList()
		{
			IList<SettingCategoryInfo> list = (IList<SettingCategoryInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetSettingCategory");
			if (list == null)
			{
				list = BizBase.dbo.GetList<SettingCategoryInfo>(" select * from sys_SettingCategory order by sort asc ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetSettingCategory", list);
			}
			return list;
		}

		public static SettingCategoryInfo GetCacheSettingCategory(int intCateID)
		{
			IList<SettingCategoryInfo> cacheSettingCategoryList = SettingCategory.GetCacheSettingCategoryList();
			SettingCategoryInfo result;
			if (cacheSettingCategoryList != null && cacheSettingCategoryList.Count > 0)
			{
				result = (from p in cacheSettingCategoryList
				where p.AutoID.Equals(intCateID)
				select p).FirstOrDefault<SettingCategoryInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool ExistsSettingCateByName(string strCateName)
		{
			IList<SettingCategoryInfo> cacheSettingCategoryList = SettingCategory.GetCacheSettingCategoryList();
			bool result;
			if (cacheSettingCategoryList != null && cacheSettingCategoryList.Count > 0)
			{
				result = ((from p in cacheSettingCategoryList
				where p.CateName == strCateName
				select p).FirstOrDefault<SettingCategoryInfo>() != null);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool ExistsChildSetting(int intSettingCateID)
		{
			return BizBase.dbo.GetValue<int>("SELECT COUNT(*) FROM sys_Setting WHERE CateID=" + intSettingCateID) > 0;
		}

		public static int Add(SettingCategoryInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<SettingCategoryInfo>(entity);
			}
			return result;
		}

		public static bool Update(SettingCategoryInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<SettingCategoryInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_SettingCategory WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_SettingCategory WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static SettingCategoryInfo GetDataById(int intPrimaryKeyIDValue)
		{
			SettingCategoryInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<SettingCategoryInfo>(" SELECT AutoID,CateName,CateDesc,Sort,IsUsing,AutoTimeStamp FROM sys_SettingCategory WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static SettingCategoryInfo GetTopData()
		{
			return SettingCategory.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static SettingCategoryInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,CateName,CateDesc,Sort,IsUsing,AutoTimeStamp FROM sys_SettingCategory ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<SettingCategoryInfo>(text);
		}

		public static IList<SettingCategoryInfo> GetAllList()
		{
			return SettingCategory.GetList(0, string.Empty);
		}

		public static IList<SettingCategoryInfo> GetTopNList(int intTopCount)
		{
			return SettingCategory.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<SettingCategoryInfo> GetTopNList(int intTopCount, string strSort)
		{
			return SettingCategory.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<SettingCategoryInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return SettingCategory.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<SettingCategoryInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,CateName,CateDesc,Sort,IsUsing,AutoTimeStamp from sys_SettingCategory ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<SettingCategoryInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_SettingCategory", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_SettingCategory", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return SettingCategory.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SettingCategory.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SettingCategory.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SettingCategory.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,CateName,CateDesc,Sort,IsUsing,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_SettingCategory";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<SettingCategoryInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return SettingCategory.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<SettingCategoryInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<SettingCategoryInfo> result = new List<SettingCategoryInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,CateName,CateDesc,Sort,IsUsing,AutoTimeStamp";
			pager.PagerTable = "sys_SettingCategory";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<SettingCategoryInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_SettingCategory SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_SettingCategory SET Sort =",
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
