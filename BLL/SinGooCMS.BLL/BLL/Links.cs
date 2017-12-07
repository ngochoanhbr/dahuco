using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Links : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_Links ");
			}
		}

		public static int Add(LinksInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<LinksInfo>(entity);
			}
			return result;
		}

		public static bool Update(LinksInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<LinksInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_Links WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_Links WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static LinksInfo GetDataById(int intPrimaryKeyIDValue)
		{
			LinksInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<LinksInfo>(" SELECT AutoID,LinkName,LinkUrl,LinkType,LinkText,LinkImage,LInkFlash,IsAudit,Sort,Lang,AutoTimeStamp FROM cms_Links WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static LinksInfo GetTopData()
		{
			return Links.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static LinksInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,LinkName,LinkUrl,LinkType,LinkText,LinkImage,LInkFlash,IsAudit,Sort,Lang,AutoTimeStamp FROM cms_Links ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<LinksInfo>(text);
		}

		public static IList<LinksInfo> GetAllList()
		{
			return Links.GetList(0, string.Empty);
		}

		public static IList<LinksInfo> GetTopNList(int intTopCount)
		{
			return Links.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<LinksInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Links.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<LinksInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Links.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<LinksInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,LinkName,LinkUrl,LinkType,LinkText,LinkImage,LInkFlash,IsAudit,Sort,Lang,AutoTimeStamp from cms_Links ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<LinksInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Links", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Links", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Links.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Links.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Links.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Links.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,LinkName,LinkUrl,LinkType,LinkText,LinkImage,LInkFlash,IsAudit,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_Links";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<LinksInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Links.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<LinksInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<LinksInfo> result = new List<LinksInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,LinkName,LinkUrl,LinkType,LinkText,LinkImage,LInkFlash,IsAudit,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_Links";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<LinksInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_Links SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_Links SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<LinksInfo> GetLinks()
		{
			return BizBase.dbo.GetList<LinksInfo>(" select * from cms_Links Order by Sort asc,AutoID desc");
		}

		public static IList<LinksInfo> GetCurrLinks()
		{
			return (from p in Links.GetLinks()
			where p.Lang.Equals(JObject.cultureLang)
			select p).ToList<LinksInfo>();
		}
	}
}
