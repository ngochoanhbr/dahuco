using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Ver : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Ver ");
			}
		}

		public static VerInfo GetVer()
		{
			VerInfo verInfo = (VerInfo)CacheUtils.Get("JsonLeeCMS_CacheForVER");
			if (verInfo == null)
			{
				verInfo = Ver.GetData();
				CacheUtils.Insert("JsonLeeCMS_CacheForVER", verInfo, 60, 1);
			}
			return verInfo;
		}

		public static VerInfo GetData()
		{
			return BizBase.dbo.GetModel<VerInfo>(" select top 1 * from sys_Ver ");
		}

		public static int Add(VerInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<VerInfo>(entity);
			}
			return result;
		}

		public static bool Update(VerInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<VerInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Ver WHERE SoftName=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Ver WHERE SoftName in (" + strArrIdList + ") ");
		}

		public static VerInfo GetDataById(int intPrimaryKeyIDValue)
		{
			VerInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<VerInfo>(" SELECT SoftName,LicTimeStart,LicTimeEnd,UpdateUrl,Ver,Author,Company,Address,PostCode,CopyRight,Brand,CustomerID,CustomerName,CustomerAddr,Contact,Mobile,Email,ProjectStart,ProjectEnd,SiteCapacity,NodeLimit,ContentLimit,CategoryLimit,ProductLimit,RegTime,DesktopSet,Remark,LastUpdateTime FROM sys_Ver WHERE SoftName=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static VerInfo GetTopData()
		{
			return Ver.GetTopData(" Sort ASC,SoftName desc ");
		}

		public static VerInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 SoftName,LicTimeStart,LicTimeEnd,UpdateUrl,Ver,Author,Company,Address,PostCode,CopyRight,Brand,CustomerID,CustomerName,CustomerAddr,Contact,Mobile,Email,ProjectStart,ProjectEnd,SiteCapacity,NodeLimit,ContentLimit,CategoryLimit,ProductLimit,RegTime,DesktopSet,Remark,LastUpdateTime FROM sys_Ver ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<VerInfo>(text);
		}

		public static IList<VerInfo> GetAllList()
		{
			return Ver.GetList(0, string.Empty);
		}

		public static IList<VerInfo> GetTopNList(int intTopCount)
		{
			return Ver.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<VerInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Ver.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<VerInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,SoftName desc ";
			return Ver.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<VerInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" SoftName,LicTimeStart,LicTimeEnd,UpdateUrl,Ver,Author,Company,Address,PostCode,CopyRight,Brand,CustomerID,CustomerName,CustomerAddr,Contact,Mobile,Email,ProjectStart,ProjectEnd,SiteCapacity,NodeLimit,ContentLimit,CategoryLimit,ProductLimit,RegTime,DesktopSet,Remark,LastUpdateTime from sys_Ver ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<VerInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Ver", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Ver", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Ver.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Ver.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Ver.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Ver.GetPagerData(strFilter, strCondition, " Sort asc,SoftName desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "SoftName,LicTimeStart,LicTimeEnd,UpdateUrl,Ver,Author,Company,Address,PostCode,CopyRight,Brand,CustomerID,CustomerName,CustomerAddr,Contact,Mobile,Email,ProjectStart,ProjectEnd,SiteCapacity,NodeLimit,ContentLimit,CategoryLimit,ProductLimit,RegTime,DesktopSet,Remark,LastUpdateTime";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Ver";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<VerInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Ver.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<VerInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<VerInfo> result = new List<VerInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "SoftName,LicTimeStart,LicTimeEnd,UpdateUrl,Ver,Author,Company,Address,PostCode,CopyRight,Brand,CustomerID,CustomerName,CustomerAddr,Contact,Mobile,Email,ProjectStart,ProjectEnd,SiteCapacity,NodeLimit,ContentLimit,CategoryLimit,ProductLimit,RegTime,DesktopSet,Remark,LastUpdateTime";
			pager.PagerTable = "sys_Ver";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<VerInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Ver SET Sort=" + intSort.ToString() + " WHERE SoftName=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Ver SET Sort =",
						current.Value.ToString(),
						" WHERE SoftName=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}
	}
}
