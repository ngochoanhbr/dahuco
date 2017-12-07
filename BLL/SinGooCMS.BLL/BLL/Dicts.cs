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
	public class Dicts : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Dicts ");
			}
		}

		public static IList<DictsInfo> GetCacheDictsList()
		{
			IList<DictsInfo> list = (IList<DictsInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetDicts");
			if (list == null)
			{
				list = BizBase.dbo.GetList<DictsInfo>(" SELECT * FROM sys_Dicts ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetDicts", list);
			}
			return list;
		}

		public static DictsInfo GetCacheDicts(int intDictsID)
		{
			IList<DictsInfo> cacheDictsList = Dicts.GetCacheDictsList();
			DictsInfo result;
			if (cacheDictsList != null && cacheDictsList.Count > 0)
			{
				result = (from p in cacheDictsList
				where p.AutoID.Equals(intDictsID)
				select p).FirstOrDefault<DictsInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static DictsInfo GetCacheDictsByName(string strDictsName)
		{
			IList<DictsInfo> cacheDictsList = Dicts.GetCacheDictsList();
			DictsInfo result;
			if (cacheDictsList != null && cacheDictsList.Count > 0)
			{
				result = (from p in cacheDictsList
				where p.DictName.Equals(strDictsName)
				select p).FirstOrDefault<DictsInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool ExistsForName(string strDictName)
		{
			return BizBase.dbo.GetValue<int>(" SELECT TOP 1 1 FROM sys_Dicts WHERE DictName='" + strDictName + "' ") > 0;
		}

		public static int Add(DictsInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<DictsInfo>(entity);
			}
			return result;
		}

		public static bool Update(DictsInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<DictsInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Dicts WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Dicts WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static DictsInfo GetDataById(int intPrimaryKeyIDValue)
		{
			DictsInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<DictsInfo>(" SELECT TOP 1 AutoID,DictName,DisplayName,DictValue,IsSystem FROM sys_Dicts WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static DictsInfo GetTopData()
		{
			return Dicts.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static DictsInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,DictName,DisplayName,DictValue,IsSystem FROM sys_Dicts ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<DictsInfo>(text);
		}

		public static IList<DictsInfo> GetAllList()
		{
			return Dicts.GetList(0, string.Empty);
		}

		public static IList<DictsInfo> GetTopNList(int intTopCount)
		{
			return Dicts.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<DictsInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Dicts.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<DictsInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Dicts.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<DictsInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,DictName,DisplayName,DictValue,IsSystem from sys_Dicts ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<DictsInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Dicts", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Dicts", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Dicts.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Dicts.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Dicts.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Dicts.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,DictName,DisplayName,DictValue,IsSystem";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Dicts";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<DictsInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Dicts.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<DictsInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<DictsInfo> result = new List<DictsInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,DictName,DisplayName,DictValue,IsSystem";
			pager.PagerTable = "sys_Dicts";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<DictsInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Dicts SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Dicts SET Sort =",
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
