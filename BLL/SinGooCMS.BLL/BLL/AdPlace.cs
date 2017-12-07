using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class AdPlace : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_AdPlace ");
			}
		}

		public static int Add(AdPlaceInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<AdPlaceInfo>(entity);
			}
			return result;
		}

		public static bool Update(AdPlaceInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<AdPlaceInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_AdPlace WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_AdPlace WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static AdPlaceInfo GetDataById(int intPrimaryKeyIDValue)
		{
			AdPlaceInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<AdPlaceInfo>(" SELECT AutoID,PlaceName,Width,Height,Price,TemplateFile,PlaceDesc,Sort,Lang,AutoTimeStamp FROM cms_AdPlace WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static AdPlaceInfo GetTopData()
		{
			return AdPlace.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static AdPlaceInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,PlaceName,Width,Height,Price,TemplateFile,PlaceDesc,Sort,Lang,AutoTimeStamp FROM cms_AdPlace ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<AdPlaceInfo>(text);
		}

		public static IList<AdPlaceInfo> GetAllList()
		{
			return AdPlace.GetList(0, string.Empty);
		}

		public static IList<AdPlaceInfo> GetTopNList(int intTopCount)
		{
			return AdPlace.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<AdPlaceInfo> GetTopNList(int intTopCount, string strSort)
		{
			return AdPlace.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<AdPlaceInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return AdPlace.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<AdPlaceInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,PlaceName,Width,Height,Price,TemplateFile,PlaceDesc,Sort,Lang,AutoTimeStamp from cms_AdPlace ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<AdPlaceInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_AdPlace", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_AdPlace", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return AdPlace.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AdPlace.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AdPlace.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AdPlace.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,PlaceName,Width,Height,Price,TemplateFile,PlaceDesc,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_AdPlace";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<AdPlaceInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return AdPlace.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<AdPlaceInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<AdPlaceInfo> result = new List<AdPlaceInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,PlaceName,Width,Height,Price,TemplateFile,PlaceDesc,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_AdPlace";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<AdPlaceInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_AdPlace SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_AdPlace SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<AdPlaceInfo> GetCacheAdPlaces()
		{
			return BizBase.dbo.GetList<AdPlaceInfo>(" SELECT * FROM cms_AdPlace Order by Sort asc,AutoID desc ");
		}

		public static IList<AdPlaceInfo> GetCurrCacheAdPlaces()
		{
			return (from p in AdPlace.GetCacheAdPlaces()
			where p.Lang.Equals(JObject.cultureLang)
			select p).ToList<AdPlaceInfo>();
		}

		public static AdPlaceInfo GetCacheAdPlaceById(int intPlaceID)
		{
			return (from p in AdPlace.GetCacheAdPlaces()
			where p.AutoID.Equals(intPlaceID)
			select p).FirstOrDefault<AdPlaceInfo>();
		}

		public static int HasAdsCount(int intPlace)
		{
			return AdPlace.GetCount("PlaceID=" + intPlace);
		}

		public static bool DeleteAll(int placeID)
		{
			bool result;
			if (AdPlace.Delete(placeID))
			{
				Ads.DelByPlaceID(placeID);
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
