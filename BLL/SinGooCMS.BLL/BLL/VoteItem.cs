using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class VoteItem : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_VoteItem ");
			}
		}

		public static int Add(VoteItemInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<VoteItemInfo>(entity);
			}
			return result;
		}

		public static bool Update(VoteItemInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<VoteItemInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_VoteItem WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_VoteItem WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static VoteItemInfo GetDataById(int intPrimaryKeyIDValue)
		{
			VoteItemInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<VoteItemInfo>(" SELECT AutoID,VoteID,VoteOption,VoteNum,Sort,Lang,Creator,AutoTimeStamp FROM cms_VoteItem WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static VoteItemInfo GetTopData()
		{
			return VoteItem.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static VoteItemInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,VoteID,VoteOption,VoteNum,Sort,Lang,Creator,AutoTimeStamp FROM cms_VoteItem ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<VoteItemInfo>(text);
		}

		public static IList<VoteItemInfo> GetAllList()
		{
			return VoteItem.GetList(0, string.Empty);
		}

		public static IList<VoteItemInfo> GetTopNList(int intTopCount)
		{
			return VoteItem.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<VoteItemInfo> GetTopNList(int intTopCount, string strSort)
		{
			return VoteItem.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<VoteItemInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return VoteItem.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<VoteItemInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,VoteID,VoteOption,VoteNum,Sort,Lang,Creator,AutoTimeStamp from cms_VoteItem ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<VoteItemInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_VoteItem", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_VoteItem", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return VoteItem.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return VoteItem.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return VoteItem.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return VoteItem.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,VoteID,VoteOption,VoteNum,Sort,Lang,Creator,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_VoteItem";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<VoteItemInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return VoteItem.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<VoteItemInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<VoteItemInfo> result = new List<VoteItemInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,VoteID,VoteOption,VoteNum,Sort,Lang,Creator,AutoTimeStamp";
			pager.PagerTable = "cms_VoteItem";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<VoteItemInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_VoteItem SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_VoteItem SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<VoteItemInfo> GetItemByVoteID(int intVoteID)
		{
			return BizBase.dbo.GetList<VoteItemInfo>(" SELECT * FROM cms_VoteItem WHERE VoteID=" + intVoteID);
		}

		public static bool DeleteWithLog(int intVoteItemID)
		{
			bool result;
			if (VoteItem.Delete(intVoteItemID))
			{
				BizBase.dbo.DeleteTable(" DELETE FROM cms_VoteLog WHERE VoteItemID=" + intVoteItemID);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}
	}
}
