using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class SMS : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_SMS ");
			}
		}

		public static int Add(SMSInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<SMSInfo>(entity);
			}
			return result;
		}

		public static bool Update(SMSInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<SMSInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_SMS WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_SMS WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static SMSInfo GetDataById(int intPrimaryKeyIDValue)
		{
			SMSInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<SMSInfo>(" SELECT TOP 1 AutoID,SMSMob,SMSText,SMSType,ValidateCode,ReciverID,ReciverName,Status,ReturnMsg,AutoTimeStamp FROM sys_SMS WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static SMSInfo GetTopData()
		{
			return SMS.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static SMSInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,SMSMob,SMSText,SMSType,ValidateCode,ReciverID,ReciverName,Status,ReturnMsg,AutoTimeStamp FROM sys_SMS ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<SMSInfo>(text);
		}

		public static IList<SMSInfo> GetAllList()
		{
			return SMS.GetList(0, string.Empty);
		}

		public static IList<SMSInfo> GetTopNList(int intTopCount)
		{
			return SMS.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<SMSInfo> GetTopNList(int intTopCount, string strSort)
		{
			return SMS.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<SMSInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return SMS.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<SMSInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,SMSMob,SMSText,SMSType,ValidateCode,ReciverID,ReciverName,Status,ReturnMsg,AutoTimeStamp from sys_SMS ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<SMSInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_SMS", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_SMS", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return SMS.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SMS.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SMS.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SMS.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,SMSMob,SMSText,SMSType,ValidateCode,ReciverID,ReciverName,Status,ReturnMsg,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_SMS";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<SMSInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return SMS.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<SMSInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<SMSInfo> result = new List<SMSInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,SMSMob,SMSText,SMSType,ValidateCode,ReciverID,ReciverName,Status,ReturnMsg,AutoTimeStamp";
			pager.PagerTable = "sys_SMS";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<SMSInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_SMS SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_SMS SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static SMSInfo GetLastSMS(string strMobile)
		{
			return BizBase.dbo.GetModel<SMSInfo>("SELECT TOP 1 * FROM sys_SMS WHERE SMSMob='" + strMobile + "' ORDER BY AutoID DESC ");
		}

		public static IList<SMSInfo> GetListByMobile(string strMobile)
		{
			return SMS.GetList(1000, "SMSMob='" + strMobile + "'", "AutoID desc");
		}

		public static SMSInfo GetLastCheckCode(string strMobile)
		{
			return BizBase.dbo.GetModel<SMSInfo>("SELECT TOP 1 * FROM sys_SMS WHERE SMSMob='" + strMobile + "' AND SMSType='CheckCode' ORDER BY AutoID DESC");
		}
	}
}
