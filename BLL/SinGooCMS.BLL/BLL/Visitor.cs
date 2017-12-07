using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Visitor : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Visitor ");
			}
		}

		public static int Add(VisitorInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<VisitorInfo>(entity);
			}
			return result;
		}

		public static bool Update(VisitorInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<VisitorInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Visitor WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Visitor WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static VisitorInfo GetDataById(int intPrimaryKeyIDValue)
		{
			VisitorInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<VisitorInfo>(" SELECT TOP 1 AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp FROM sys_Visitor WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static VisitorInfo GetTopData()
		{
			return Visitor.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static VisitorInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp FROM sys_Visitor ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<VisitorInfo>(text);
		}

		public static IList<VisitorInfo> GetAllList()
		{
			return Visitor.GetList(0, string.Empty);
		}

		public static IList<VisitorInfo> GetTopNList(int intTopCount)
		{
			return Visitor.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<VisitorInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Visitor.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<VisitorInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Visitor.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<VisitorInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp from sys_Visitor ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<VisitorInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Visitor", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Visitor", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Visitor.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Visitor.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Visitor.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Visitor.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Visitor";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<VisitorInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Visitor.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<VisitorInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<VisitorInfo> result = new List<VisitorInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp";
			pager.PagerTable = "sys_Visitor";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<VisitorInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Visitor SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Visitor SET Sort =",
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
