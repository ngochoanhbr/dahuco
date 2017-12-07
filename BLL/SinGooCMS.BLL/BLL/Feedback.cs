using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Feedback : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_Feedback ");
			}
		}

		public static int Add(FeedbackInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<FeedbackInfo>(entity);
			}
			return result;
		}

		public static bool Update(FeedbackInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<FeedbackInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_Feedback WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_Feedback WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static FeedbackInfo GetDataById(int intPrimaryKeyIDValue)
		{
			FeedbackInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<FeedbackInfo>(" SELECT TOP 1 AutoID,UserName,Title,Content,IPaddress,Email,Telephone,Mobile,Replier,ReplyContent,ReplyDate,IsAudit,Sort,Lang,AutoTimeStamp FROM cms_Feedback WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static FeedbackInfo GetTopData()
		{
			return Feedback.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static FeedbackInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserName,Title,Content,IPaddress,Email,Telephone,Mobile,Replier,ReplyContent,ReplyDate,IsAudit,Sort,Lang,AutoTimeStamp FROM cms_Feedback ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<FeedbackInfo>(text);
		}

		public static IList<FeedbackInfo> GetAllList()
		{
			return Feedback.GetList(0, string.Empty);
		}

		public static IList<FeedbackInfo> GetTopNList(int intTopCount)
		{
			return Feedback.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<FeedbackInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Feedback.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<FeedbackInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Feedback.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<FeedbackInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserName,Title,Content,IPaddress,Email,Telephone,Mobile,Replier,ReplyContent,ReplyDate,IsAudit,Sort,Lang,AutoTimeStamp from cms_Feedback ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<FeedbackInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Feedback", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Feedback", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Feedback.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Feedback.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Feedback.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Feedback.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserName,Title,Content,IPaddress,Email,Telephone,Mobile,Replier,ReplyContent,ReplyDate,IsAudit,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_Feedback";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<FeedbackInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Feedback.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<FeedbackInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<FeedbackInfo> result = new List<FeedbackInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserName,Title,Content,IPaddress,Email,Telephone,Mobile,Replier,ReplyContent,ReplyDate,IsAudit,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_Feedback";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<FeedbackInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_Feedback SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_Feedback SET Sort =",
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
