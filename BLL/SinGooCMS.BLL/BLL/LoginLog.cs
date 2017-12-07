using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class LoginLog : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_LoginLog ");
			}
		}

		public static int Add(LoginLogInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<LoginLogInfo>(entity);
			}
			return result;
		}

		public static bool Update(LoginLogInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<LoginLogInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_LoginLog WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_LoginLog WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static LoginLogInfo GetDataById(int intPrimaryKeyIDValue)
		{
			LoginLogInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<LoginLogInfo>(" SELECT TOP 1 AutoID,UserType,UserName,IPAddress,IPArea,LoginStatus,LoginFailCount,Lang,AutoTimeStamp FROM sys_LoginLog WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static LoginLogInfo GetTopData()
		{
			return LoginLog.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static LoginLogInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserType,UserName,IPAddress,IPArea,LoginStatus,LoginFailCount,Lang,AutoTimeStamp FROM sys_LoginLog ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<LoginLogInfo>(text);
		}

		public static IList<LoginLogInfo> GetAllList()
		{
			return LoginLog.GetList(0, string.Empty);
		}

		public static IList<LoginLogInfo> GetTopNList(int intTopCount)
		{
			return LoginLog.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<LoginLogInfo> GetTopNList(int intTopCount, string strSort)
		{
			return LoginLog.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<LoginLogInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return LoginLog.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<LoginLogInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserType,UserName,IPAddress,IPArea,LoginStatus,LoginFailCount,Lang,AutoTimeStamp from sys_LoginLog ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<LoginLogInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_LoginLog", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_LoginLog", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return LoginLog.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return LoginLog.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return LoginLog.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return LoginLog.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserType,UserName,IPAddress,IPArea,LoginStatus,LoginFailCount,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_LoginLog";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<LoginLogInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return LoginLog.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<LoginLogInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<LoginLogInfo> result = new List<LoginLogInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserType,UserName,IPAddress,IPArea,LoginStatus,LoginFailCount,Lang,AutoTimeStamp";
			pager.PagerTable = "sys_LoginLog";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<LoginLogInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_LoginLog SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_LoginLog SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static LoginLogInfo GetLast(string strUserName)
		{
			return LoginLog.GetLast(UserType.Manager, strUserName);
		}

		public static LoginLogInfo GetLast(UserType userType, string strUserName)
		{
			return BizBase.dbo.GetModel<LoginLogInfo>(string.Concat(new string[]
			{
				"SELECT TOP 1 * FROM sys_LoginLog WHERE UserType='",
				userType.ToString(),
				"' AND UserName='",
				StringUtils.ChkSQL(strUserName),
				"' ORDER BY AutoID desc"
			}));
		}
	}
}
