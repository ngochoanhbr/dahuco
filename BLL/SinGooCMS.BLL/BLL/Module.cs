using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Module : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Module ");
			}
		}

		public static bool DeleteAll(int intModuleID)
		{
			string strSQL = string.Concat(new object[]
			{
				"delete from sys_Purview where ModuleID=",
				intModuleID,
				";delete from sys_Operate where ModuleID=",
				intModuleID,
				";delete from sys_Module where AutoID=",
				intModuleID,
				";"
			});
			return BizBase.dbo.ExecSQL(strSQL);
		}

		public static ModuleInfo GetModuleByCode(string strModuleCode)
		{
			return BizBase.dbo.GetModel<ModuleInfo>(" SELECT TOP 1 * FROM sys_Module WHERE ModuleCode='" + strModuleCode + "' ");
		}

		public static IList<ModuleInfo> GetListByCatalogID(int intCatalogID)
		{
			return BizBase.dbo.GetList<ModuleInfo>(" SELECT * FROM sys_Module WHERE CatalogID=" + intCatalogID + " ORDER BY Sort asc ");
		}

		public static int Add(ModuleInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<ModuleInfo>(entity);
			}
			return result;
		}

		public static bool Update(ModuleInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<ModuleInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Module WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Module WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ModuleInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ModuleInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ModuleInfo>(" SELECT TOP 1 AutoID,CatalogID,ModuleName,ModuleCode,FilePath,ImagePath,Remark,IsSystem,IsDefault,Sort,AutoTimeStamp FROM sys_Module WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ModuleInfo GetTopData()
		{
			return Module.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ModuleInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,CatalogID,ModuleName,ModuleCode,FilePath,ImagePath,Remark,IsSystem,IsDefault,Sort,AutoTimeStamp FROM sys_Module ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ModuleInfo>(text);
		}

		public static IList<ModuleInfo> GetAllList()
		{
			return Module.GetList(0, string.Empty);
		}

		public static IList<ModuleInfo> GetTopNList(int intTopCount)
		{
			return Module.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ModuleInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Module.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ModuleInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Module.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ModuleInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,CatalogID,ModuleName,ModuleCode,FilePath,ImagePath,Remark,IsSystem,IsDefault,Sort,AutoTimeStamp from sys_Module ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ModuleInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Module", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Module", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Module.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Module.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Module.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Module.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,CatalogID,ModuleName,ModuleCode,FilePath,ImagePath,Remark,IsSystem,IsDefault,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Module";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ModuleInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Module.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ModuleInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ModuleInfo> result = new List<ModuleInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,CatalogID,ModuleName,ModuleCode,FilePath,ImagePath,Remark,IsSystem,IsDefault,Sort,AutoTimeStamp";
			pager.PagerTable = "sys_Module";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ModuleInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Module SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Module SET Sort =",
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
