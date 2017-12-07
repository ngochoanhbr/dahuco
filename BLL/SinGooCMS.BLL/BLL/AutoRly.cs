using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class AutoRly : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM weixin_AutoRly ");
			}
		}

		public static int Add(AutoRlyInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<AutoRlyInfo>(entity);
			}
			return result;
		}

		public static bool Update(AutoRlyInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<AutoRlyInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM weixin_AutoRly WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM weixin_AutoRly WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static AutoRlyInfo GetDataById(int intPrimaryKeyIDValue)
		{
			AutoRlyInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<AutoRlyInfo>(" SELECT AutoID,RlyType,MsgKey,MsgText,Description,MediaPath,LinkUrl,AutoTimeStamp FROM weixin_AutoRly WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static AutoRlyInfo GetTopData()
		{
			return AutoRly.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static AutoRlyInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,RlyType,MsgKey,MsgText,Description,MediaPath,LinkUrl,AutoTimeStamp FROM weixin_AutoRly ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<AutoRlyInfo>(text);
		}

		public static IList<AutoRlyInfo> GetAllList()
		{
			return AutoRly.GetList(0, string.Empty);
		}

		public static IList<AutoRlyInfo> GetTopNList(int intTopCount)
		{
			return AutoRly.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<AutoRlyInfo> GetTopNList(int intTopCount, string strSort)
		{
			return AutoRly.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<AutoRlyInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return AutoRly.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<AutoRlyInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,RlyType,MsgKey,MsgText,Description,MediaPath,LinkUrl,AutoTimeStamp from weixin_AutoRly ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<AutoRlyInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "weixin_AutoRly", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "weixin_AutoRly", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return AutoRly.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AutoRly.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AutoRly.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AutoRly.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,RlyType,MsgKey,MsgText,Description,MediaPath,LinkUrl,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "weixin_AutoRly";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<AutoRlyInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return AutoRly.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<AutoRlyInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<AutoRlyInfo> result = new List<AutoRlyInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,RlyType,MsgKey,MsgText,Description,MediaPath,LinkUrl,AutoTimeStamp";
			pager.PagerTable = "weixin_AutoRly";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<AutoRlyInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE weixin_AutoRly SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE weixin_AutoRly SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static AutoRlyInfo GetFocusRly()
		{
			return BizBase.dbo.GetModel<AutoRlyInfo>(" select top 1 * from weixin_AutoRly where RlyType='关注回复' ");
		}

		public static AutoRlyInfo GetDefaultRly()
		{
			return BizBase.dbo.GetModel<AutoRlyInfo>(" select top 1 * from weixin_AutoRly where RlyType='默认回复' ");
		}

		public static AutoRlyInfo GetKeyRly(string strReqKey)
		{
			return AutoRly.GetByKey("关键字回复", strReqKey);
		}

		public static AutoRlyInfo GetEventRly(string strReqKey)
		{
			return AutoRly.GetByKey("事件回复", strReqKey);
		}

		private static AutoRlyInfo GetByKey(string keyType, string strMsgKey)
		{
			return BizBase.dbo.GetModel<AutoRlyInfo>(string.Concat(new string[]
			{
				" select top 1 * from weixin_AutoRly where RlyType='",
				keyType,
				"' and MsgKey='",
				strMsgKey,
				"' "
			}));
		}

		public static bool DelEventKey(string strReqKey)
		{
			return BizBase.dbo.DeleteTable(" delete from weixin_AutoRly where RlyType='事件回复' and MsgKey='" + strReqKey + "' ");
		}
	}
}
