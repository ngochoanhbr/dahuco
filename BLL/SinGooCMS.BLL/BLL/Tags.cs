using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Tags : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Tags ");
			}
		}

		public static IList<TagsInfo> GetCacheTagsList()
		{
			return BizBase.dbo.GetList<TagsInfo>(" select * from shop_Tags Order by Sort asc,AutoID desc ");
		}

		public static TagsInfo GetCacheTagByID(int intTagID)
		{
			return (from p in Tags.GetCacheTagsList()
			where p.AutoID.Equals(intTagID)
			select p).FirstOrDefault<TagsInfo>();
		}

		public static int Add(TagsInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<TagsInfo>(entity);
			}
			return result;
		}

		public static bool Update(TagsInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<TagsInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Tags WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Tags WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static TagsInfo GetDataById(int intPrimaryKeyIDValue)
		{
			TagsInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<TagsInfo>(" SELECT TOP 1 AutoID,TagName,TagUrl,TagIndex,Remark,IsTop,IsRecommend,Sort,Lang,AutoTimeStamp FROM shop_Tags WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static TagsInfo GetTopData()
		{
			return Tags.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static TagsInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,TagName,TagUrl,TagIndex,Remark,IsTop,IsRecommend,Sort,Lang,AutoTimeStamp FROM shop_Tags ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<TagsInfo>(text);
		}

		public static IList<TagsInfo> GetAllList()
		{
			return Tags.GetList(0, string.Empty);
		}

		public static IList<TagsInfo> GetTopNList(int intTopCount)
		{
			return Tags.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<TagsInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Tags.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<TagsInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Tags.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<TagsInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,TagName,TagUrl,TagIndex,Remark,IsTop,IsRecommend,Sort,Lang,AutoTimeStamp from shop_Tags ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<TagsInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Tags", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Tags", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Tags.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Tags.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Tags.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Tags.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,TagName,TagUrl,TagIndex,Remark,IsTop,IsRecommend,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Tags";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<TagsInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Tags.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<TagsInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<TagsInfo> result = new List<TagsInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,TagName,TagUrl,TagIndex,Remark,IsTop,IsRecommend,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "shop_Tags";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<TagsInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Tags SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Tags SET Sort =",
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
