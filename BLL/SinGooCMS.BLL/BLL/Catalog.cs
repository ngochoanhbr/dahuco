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
	public class Catalog : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Catalog ");
			}
		}

		public static IList<CatalogInfo> GetCacheCatalogList()
		{
			IList<CatalogInfo> list = (IList<CatalogInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetCMSCatalog");
			if (list == null)
			{
				list = BizBase.dbo.GetList<CatalogInfo>(" SELECT * FROM sys_Catalog ORDER BY Sort ASC,AutoID desc ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetCMSCatalog", list);
			}
			return list;
		}

		public static CatalogInfo GetCacheCatalogById(int intCatalogID)
		{
			IEnumerable<CatalogInfo> source = from p in Catalog.GetCacheCatalogList()
			where p.AutoID.Equals(intCatalogID)
			select p;
			return (source.Count<CatalogInfo>() > 0) ? source.First<CatalogInfo>() : null;
		}

		public static CatalogInfo GetCacheCatalogByCode(string strCatalogCode)
		{
			IEnumerable<CatalogInfo> source = from p in Catalog.GetCacheCatalogList()
			where p.CatalogCode.Equals(strCatalogCode)
			select p;
			return (source.Count<CatalogInfo>() > 0) ? source.First<CatalogInfo>() : null;
		}

		public static bool ExistsChildModule(int intCatalogID)
		{
			return BizBase.dbo.GetValue<int>(" SELECT TOP 1 1 FROM sys_Module WHERE CatalogID=" + intCatalogID) > 0;
		}

		public static int Add(CatalogInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				int num = BizBase.dbo.InsertModel<CatalogInfo>(entity);
				if (num > 0)
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetCMSCatalog");
				}
				result = num;
			}
			return result;
		}

		public static bool Update(CatalogInfo entity)
		{
			bool result;
			if (entity == null)
			{
				result = false;
			}
			else if (BizBase.dbo.UpdateModel<CatalogInfo>(entity))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSCatalog");
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			bool result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = false;
			}
			else if (BizBase.dbo.DeleteTable("sys_Catalog", "AutoID=" + intPrimaryKeyIDValue.ToString()))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSCatalog");
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool Delete(string strArrIdList)
		{
			bool result;
			if (string.IsNullOrEmpty(strArrIdList))
			{
				result = false;
			}
			else
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSCatalog");
				result = BizBase.dbo.DeleteTable("sys_Catalog", " AutoID in (" + strArrIdList + ") ");
			}
			return result;
		}

		public static CatalogInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CatalogInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CatalogInfo>(" SELECT TOP 1 AutoID,CatalogName,CatalogCode,ImagePath,IsSystem,Remark,Sort,AutoTimeStamp FROM sys_Catalog WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CatalogInfo GetTopData()
		{
			return Catalog.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CatalogInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,CatalogName,CatalogCode,ImagePath,IsSystem,Remark,Sort,AutoTimeStamp FROM sys_Catalog ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CatalogInfo>(text);
		}

		public static IList<CatalogInfo> GetAllList()
		{
			return Catalog.GetList(0, string.Empty);
		}

		public static IList<CatalogInfo> GetTopNList(int intTopCount)
		{
			return Catalog.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CatalogInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Catalog.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CatalogInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Catalog.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CatalogInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,CatalogName,CatalogCode,ImagePath,IsSystem,Remark,Sort,AutoTimeStamp from sys_Catalog ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CatalogInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Catalog", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Catalog", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Catalog.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Catalog.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Catalog.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Catalog.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,CatalogName,CatalogCode,ImagePath,IsSystem,Remark,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Catalog";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CatalogInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Catalog.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CatalogInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CatalogInfo> result = new List<CatalogInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,CatalogName,CatalogCode,ImagePath,IsSystem,Remark,Sort,AutoTimeStamp";
			pager.PagerTable = "sys_Catalog";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CatalogInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Catalog SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Catalog SET Sort =",
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
