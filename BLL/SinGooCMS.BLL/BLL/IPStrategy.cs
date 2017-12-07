using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class IPStrategy : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_IPStrategy ");
			}
		}

		public static int Add(IPStrategyInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<IPStrategyInfo>(entity);
			}
			return result;
		}

		public static bool Update(IPStrategyInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<IPStrategyInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_IPStrategy WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_IPStrategy WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static IPStrategyInfo GetDataById(int intPrimaryKeyIDValue)
		{
			IPStrategyInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<IPStrategyInfo>(" SELECT AutoID,IPAddress,Strategy,AutoTimeStamp FROM sys_IPStrategy WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static IPStrategyInfo GetTopData()
		{
			return IPStrategy.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static IPStrategyInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,IPAddress,Strategy,AutoTimeStamp FROM sys_IPStrategy ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<IPStrategyInfo>(text);
		}

		public static IList<IPStrategyInfo> GetAllList()
		{
			return IPStrategy.GetList(0, string.Empty);
		}

		public static IList<IPStrategyInfo> GetTopNList(int intTopCount)
		{
			return IPStrategy.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<IPStrategyInfo> GetTopNList(int intTopCount, string strSort)
		{
			return IPStrategy.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<IPStrategyInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return IPStrategy.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<IPStrategyInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,IPAddress,Strategy,AutoTimeStamp from sys_IPStrategy ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<IPStrategyInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_IPStrategy", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_IPStrategy", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return IPStrategy.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return IPStrategy.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return IPStrategy.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return IPStrategy.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,IPAddress,Strategy,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_IPStrategy";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<IPStrategyInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return IPStrategy.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<IPStrategyInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<IPStrategyInfo> result = new List<IPStrategyInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,IPAddress,Strategy,AutoTimeStamp";
			pager.PagerTable = "sys_IPStrategy";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<IPStrategyInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_IPStrategy SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_IPStrategy SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static bool ValidateIPStrategy(string strIP)
		{
			bool result = false;
			IList<IPStrategyInfo> allList = IPStrategy.GetAllList();
			if (allList != null && allList.Count > 0)
			{
				if (IPStrategy.IsIPDenyOrAllow(allList, strIP, IPStrategyType.DENY) && !IPStrategy.IsIPDenyOrAllow(allList, strIP, IPStrategyType.ALLOW))
				{
					result = true;
				}
			}
			return result;
		}

		public static bool IsIPDenyOrAllow(IList<IPStrategyInfo> listSource, string strLocalIP, IPStrategyType enuStrategyType)
		{
			bool result = false;
			foreach (IPStrategyInfo current in listSource)
			{
				if (current.Strategy == enuStrategyType.ToString())
				{
					if (current.IPAddress.IndexOf("-") != -1)
					{
						string strBeginIP = current.IPAddress.Split(new char[]
						{
							'-'
						})[0];
						string strEndIP = current.IPAddress.Split(new char[]
						{
							'-'
						})[1];
						if (IPUtils.IsInIPDuan(strLocalIP, strBeginIP, strEndIP))
						{
							result = true;
							break;
						}
					}
					else if (current.IPAddress == strLocalIP)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
	}
}
