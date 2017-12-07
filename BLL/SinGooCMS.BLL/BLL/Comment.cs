using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Comment : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_Comment ");
			}
		}

		public static int Add(CommentInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<CommentInfo>(entity);
			}
			return result;
		}

		public static bool Update(CommentInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<CommentInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_Comment WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_Comment WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static CommentInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CommentInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CommentInfo>(" SELECT AutoID,ContID,ContName,UserID,UserName,ReplyID,Title,Content,IPAddress,IPArea,EnableAnonymous,IsReply,IsAudit,Ding,Cai,Lang,AutoTimeStamp FROM cms_Comment WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CommentInfo GetTopData()
		{
			return Comment.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CommentInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ContID,ContName,UserID,UserName,ReplyID,Title,Content,IPAddress,IPArea,EnableAnonymous,IsReply,IsAudit,Ding,Cai,Lang,AutoTimeStamp FROM cms_Comment ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CommentInfo>(text);
		}

		public static IList<CommentInfo> GetAllList()
		{
			return Comment.GetList(0, string.Empty);
		}

		public static IList<CommentInfo> GetTopNList(int intTopCount)
		{
			return Comment.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CommentInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Comment.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CommentInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Comment.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CommentInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ContID,ContName,UserID,UserName,ReplyID,Title,Content,IPAddress,IPArea,EnableAnonymous,IsReply,IsAudit,Ding,Cai,Lang,AutoTimeStamp from cms_Comment ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CommentInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Comment", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Comment", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Comment.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Comment.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Comment.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Comment.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ContID,ContName,UserID,UserName,ReplyID,Title,Content,IPAddress,IPArea,EnableAnonymous,IsReply,IsAudit,Ding,Cai,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_Comment";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CommentInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Comment.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CommentInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CommentInfo> result = new List<CommentInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ContID,ContName,UserID,UserName,ReplyID,Title,Content,IPAddress,IPArea,EnableAnonymous,IsReply,IsAudit,Ding,Cai,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_Comment";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CommentInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_Comment SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_Comment SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<CommentInfo> GetCommentByContID(int intContID)
		{
			return BizBase.dbo.GetList<CommentInfo>(" SELECT * FROM cms_Comment WHERE ContID=" + intContID);
		}

		public static IList<CommentInfo> GetValidCommentByContID(int intContID)
		{
			return BizBase.dbo.GetList<CommentInfo>(" SELECT * FROM cms_Comment WHERE IsAudit=1 AND ContID=" + intContID);
		}

		public static IList<CommentInfo> GetCommentByUID(int intUserID)
		{
			return BizBase.dbo.GetList<CommentInfo>(" SELECT * FROM cms_Comment WHERE UserID=" + intUserID);
		}

		public static IList<CommentInfo> GetValidCommentByUID(int intUserID)
		{
			return BizBase.dbo.GetList<CommentInfo>(" SELECT * FROM cms_Comment WHERE IsAudit=1 AND UserID=" + intUserID);
		}

		public static void ShowComment(string strIDs)
		{
			string strSQL = " UPDATE cms_Comment SET IsAudit=1 WHERE AutoID IN (" + strIDs + ") ";
			BizBase.dbo.ExecSQL(strSQL);
		}

		public static void HiddenComment(string strIDs)
		{
			string strSQL = " UPDATE cms_Comment SET IsAudit=0 WHERE AutoID IN (" + strIDs + ") ";
			BizBase.dbo.ExecSQL(strSQL);
		}

		public static CommentInfo GetLastComment(int intUserID)
		{
			return BizBase.dbo.GetModel<CommentInfo>(" SELECT TOP 1 * FROM cms_Comment WHERE UserID=" + intUserID + " ORDER BY AutoID DESC ");
		}

		public static bool IsInfuseWater(int intUserID)
		{
			CommentInfo lastComment = Comment.GetLastComment(intUserID);
			return (DateTime.Now - lastComment.AutoTimeStamp).TotalSeconds < 30.0;
		}
	}
}
