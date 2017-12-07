using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace SinGooCMS.BLL
{
	public class Account : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Account ");
			}
		}

		public static int Add(AccountInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<AccountInfo>(entity);
			}
			return result;
		}

		public static bool Update(AccountInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<AccountInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Account WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Account WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static AccountInfo GetDataById(int intPrimaryKeyIDValue)
		{
			AccountInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<AccountInfo>(" SELECT TOP 1 AutoID,AccountName,Password,Roles,Email,Mobile,IsSystem,LoginCount,AutoTimeStamp FROM sys_Account WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static AccountInfo GetTopData()
		{
			return Account.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static AccountInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,AccountName,Password,Roles,Email,Mobile,IsSystem,LoginCount,AutoTimeStamp FROM sys_Account ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<AccountInfo>(text);
		}

		public static IList<AccountInfo> GetAllList()
		{
			return Account.GetList(0, string.Empty);
		}

		public static IList<AccountInfo> GetTopNList(int intTopCount)
		{
			return Account.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<AccountInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Account.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<AccountInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Account.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<AccountInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,AccountName,Password,Roles,Email,Mobile,IsSystem,LoginCount,AutoTimeStamp from sys_Account ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<AccountInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Account", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Account", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Account.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Account.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Account.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Account.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,AccountName,Password,Roles,Email,Mobile,IsSystem,LoginCount,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Account";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<AccountInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Account.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<AccountInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<AccountInfo> result = new List<AccountInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,AccountName,Password,Roles,Email,Mobile,IsSystem,LoginCount,AutoTimeStamp";
			pager.PagerTable = "sys_Account";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<AccountInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Account SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Account SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static bool Login(string strAccountName, string strPassword)
		{
			AccountInfo model = BizBase.dbo.GetModel<AccountInfo>(string.Concat(new string[]
			{
				" select top 1 * from sys_Account where AccountName='",
				strAccountName,
				"' and [Password]='",
				strPassword,
				"' "
			}));
			bool result;
			if (model != null)
			{
				SessionUtils.SetSession("Account", model);
				new LogManager().AddLoginLog(model.AccountName, true);
				model.LoginCount++;
				BizBase.dbo.UpdateModel<AccountInfo>(model);
				result = true;
			}
			else
			{
				new LogManager().AddLoginLog(strAccountName, false);
				result = false;
			}
			return result;
		}

		public static AccountInfo GetLoginAccount()
		{
			object session = SessionUtils.GetSession("Account");
			AccountInfo result;
			if (session != null)
			{
				result = (session as AccountInfo);
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
