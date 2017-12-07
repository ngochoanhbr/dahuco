using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Purview : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Purview ");
			}
		}

		public static IList<PurviewInfo> GetListByRoleID(int intRoleID)
		{
			return BizBase.dbo.GetList<PurviewInfo>(" SELECT * FROM sys_Purview WHERE RoleID=" + intRoleID);
		}

		public static void DeleteByRoleID(int intRoleID)
		{
			BizBase.dbo.DeleteTable(" DELETE FROM sys_Purview WHERE RoleID=" + intRoleID);
		}

		public static void Delete(int ModuleID, string strOpCode)
		{
			BizBase.dbo.DeleteTable(string.Concat(new object[]
			{
				" DELETE FROM sys_Purview WHERE OperateCode='",
				strOpCode,
				"' AND ModuleID=",
				ModuleID
			}));
		}

		public static IList<PurviewInfo> GetPurviews(int accountID, int moduleID)
		{
			return BizBase.dbo.GetList<PurviewInfo>(string.Concat(new object[]
			{
				" exec p_System_GetModulePurview ",
				accountID,
				",",
				moduleID
			}));
		}

		public static DataTable GetMenuDt(int accountID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@accountid", accountID)
			};
			return BizBase.dbo.ExecProcReDT("p_System_GetManageMenu", arrParam);
		}

		public static int Add(PurviewInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<PurviewInfo>(entity);
			}
			return result;
		}

		public static bool Update(PurviewInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<PurviewInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Purview WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Purview WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static PurviewInfo GetDataById(int intPrimaryKeyIDValue)
		{
			PurviewInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<PurviewInfo>(" SELECT TOP 1 AutoID,RoleID,ModuleID,OperateCode,AutoTimeStamp FROM sys_Purview WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static PurviewInfo GetTopData()
		{
			return Purview.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static PurviewInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,RoleID,ModuleID,OperateCode,AutoTimeStamp FROM sys_Purview ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<PurviewInfo>(text);
		}

		public static IList<PurviewInfo> GetAllList()
		{
			return Purview.GetList(0, string.Empty);
		}

		public static IList<PurviewInfo> GetTopNList(int intTopCount)
		{
			return Purview.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<PurviewInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Purview.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<PurviewInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Purview.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<PurviewInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,RoleID,ModuleID,OperateCode,AutoTimeStamp from sys_Purview ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<PurviewInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Purview", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Purview", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Purview.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Purview.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Purview.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Purview.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,RoleID,ModuleID,OperateCode,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Purview";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<PurviewInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Purview.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<PurviewInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<PurviewInfo> result = new List<PurviewInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,RoleID,ModuleID,OperateCode,AutoTimeStamp";
			pager.PagerTable = "sys_Purview";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<PurviewInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Purview SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Purview SET Sort =",
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
