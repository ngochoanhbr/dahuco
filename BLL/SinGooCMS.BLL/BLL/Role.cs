using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Role : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Role ");
			}
		}

		public static int Add(RoleInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<RoleInfo>(entity);
			}
			return result;
		}

		public static bool Update(RoleInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<RoleInfo>(entity);
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Role WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static RoleInfo GetDataById(int intPrimaryKeyIDValue)
		{
			RoleInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<RoleInfo>(" SELECT TOP 1 AutoID,RoleName,Remark,IsSystem,IsManager,Sort,AutoTimeStamp FROM sys_Role WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static RoleInfo GetTopData()
		{
			return Role.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static RoleInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,RoleName,Remark,IsSystem,IsManager,Sort,AutoTimeStamp FROM sys_Role ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<RoleInfo>(text);
		}

		public static IList<RoleInfo> GetAllList()
		{
			return Role.GetList(0, string.Empty);
		}

		public static IList<RoleInfo> GetTopNList(int intTopCount)
		{
			return Role.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<RoleInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Role.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<RoleInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Role.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<RoleInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,RoleName,Remark,IsSystem,IsManager,Sort,AutoTimeStamp from sys_Role ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<RoleInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Role", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Role", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Role.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Role.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Role.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Role.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,RoleName,Remark,IsSystem,IsManager,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Role";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<RoleInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Role.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<RoleInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<RoleInfo> result = new List<RoleInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,RoleName,Remark,IsSystem,IsManager,Sort,AutoTimeStamp";
			pager.PagerTable = "sys_Role";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<RoleInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Role SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Role SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static RoleStatus Delete(int intRoleID)
		{
			RoleStatus result;
			if (BizBase.dbo.GetValue<int>(" select COUNT(*) from sys_Account where CHARINDEX('," + intRoleID + ",',','+Roles+',')>0 ") > 0)
			{
				result = RoleStatus.RefByAccount;
			}
			else if (BizBase.dbo.DeleteTable("sys_Role", "AutoID=" + intRoleID.ToString()))
			{
				result = RoleStatus.Success;
			}
			else
			{
				result = RoleStatus.Error;
			}
			return result;
		}

		public static RoleInfo GetDataByName(string strRoleName)
		{
			return BizBase.dbo.GetModel<RoleInfo>(" SELECT * FROM sys_Role WHERE RoleName='" + strRoleName + "' ");
		}
	}
}
