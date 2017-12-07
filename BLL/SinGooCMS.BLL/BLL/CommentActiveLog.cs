using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class CommentActiveLog : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_CommentActiveLog ");
			}
		}

		public static bool HasDing(int intUserID, int intCommentID)
		{
			CommentActiveLogInfo model = BizBase.dbo.GetModel<CommentActiveLogInfo>(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM cms_CommentActiveLog WHERE IsDing=1 AND UserID=",
				intUserID,
				" AND CommentID=",
				intCommentID,
				" ORDER BY AutoID DESC "
			}));
			return model != null;
		}

		public static bool HasDing(int intCommentID)
		{
			CommentActiveLogInfo model = BizBase.dbo.GetModel<CommentActiveLogInfo>(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM cms_CommentActiveLog WHERE IsDing=1 AND CommentID=",
				intCommentID,
				" AND IPAddress='",
				IPUtils.GetIP(),
				"' ORDER BY AutoID DESC "
			}));
			return model != null;
		}

		public static bool HasCai(int intUserID, int intCommentID)
		{
			CommentActiveLogInfo model = BizBase.dbo.GetModel<CommentActiveLogInfo>(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM cms_CommentActiveLog WHERE IsCai=1 AND UserID=",
				intUserID,
				" AND CommentID=",
				intCommentID,
				" ORDER BY AutoID DESC "
			}));
			return model != null;
		}

		public static bool HasCai(int intCommentID)
		{
			CommentActiveLogInfo model = BizBase.dbo.GetModel<CommentActiveLogInfo>(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM cms_CommentActiveLog WHERE IsCai=1 AND CommentID=",
				intCommentID,
				" AND IPAddress='",
				IPUtils.GetIP(),
				"' ORDER BY AutoID DESC "
			}));
			return model != null;
		}

		public static int Add(CommentActiveLogInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<CommentActiveLogInfo>(entity);
			}
			return result;
		}

		public static bool Update(CommentActiveLogInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<CommentActiveLogInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_CommentActiveLog WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_CommentActiveLog WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static CommentActiveLogInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CommentActiveLogInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CommentActiveLogInfo>(" SELECT AutoID,UserID,UserName,ContentID,CommentID,IPAddress,IPArea,IsDing,IsCai,Lang,AutoTimeStamp FROM cms_CommentActiveLog WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CommentActiveLogInfo GetTopData()
		{
			return CommentActiveLog.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CommentActiveLogInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserID,UserName,ContentID,CommentID,IPAddress,IPArea,IsDing,IsCai,Lang,AutoTimeStamp FROM cms_CommentActiveLog ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CommentActiveLogInfo>(text);
		}

		public static IList<CommentActiveLogInfo> GetAllList()
		{
			return CommentActiveLog.GetList(0, string.Empty);
		}

		public static IList<CommentActiveLogInfo> GetTopNList(int intTopCount)
		{
			return CommentActiveLog.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CommentActiveLogInfo> GetTopNList(int intTopCount, string strSort)
		{
			return CommentActiveLog.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CommentActiveLogInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return CommentActiveLog.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CommentActiveLogInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserID,UserName,ContentID,CommentID,IPAddress,IPArea,IsDing,IsCai,Lang,AutoTimeStamp from cms_CommentActiveLog ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CommentActiveLogInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_CommentActiveLog", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_CommentActiveLog", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return CommentActiveLog.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return CommentActiveLog.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return CommentActiveLog.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return CommentActiveLog.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserID,UserName,ContentID,CommentID,IPAddress,IPArea,IsDing,IsCai,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_CommentActiveLog";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CommentActiveLogInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return CommentActiveLog.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CommentActiveLogInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CommentActiveLogInfo> result = new List<CommentActiveLogInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserID,UserName,ContentID,CommentID,IPAddress,IPArea,IsDing,IsCai,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_CommentActiveLog";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CommentActiveLogInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_CommentActiveLog SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_CommentActiveLog SET Sort =",
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
