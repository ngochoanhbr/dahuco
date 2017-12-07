using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Ads : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_Ads ");
			}
		}

		public static bool DelByPlaceID(int placeID)
		{
			return BizBase.dbo.DeleteTable(" DELETE FROM cms_Ads where PlaceID=" + placeID);
		}

		public static IList<AdsInfo> GetCacheAds()
		{
			return BizBase.dbo.GetList<AdsInfo>(" SELECT * FROM cms_Ads ORDER BY Sort asc,AutoID desc ");
		}

		public static IList<AdsInfo> GetCacheAdsByPlaceID(int intPlaceID)
		{
			IEnumerable<AdsInfo> enumerable = from p in Ads.GetCacheAds()
			where p.BeginDate <= DateTime.Now && p.EndDate >= DateTime.Now && p.IsAudit && p.PlaceID.Equals(intPlaceID)
			select p;
			IList<AdsInfo> result;
			if (enumerable != null)
			{
				result = enumerable.ToList<AdsInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static AdsInfo GetCacheAdByID(int intAdID)
		{
			return (from p in Ads.GetCacheAds()
			where p.AutoID.Equals(intAdID)
			select p).FirstOrDefault<AdsInfo>();
		}

		public static int Add(AdsInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<AdsInfo>(entity);
			}
			return result;
		}

		public static bool Update(AdsInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<AdsInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_Ads WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_Ads WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static AdsInfo GetDataById(int intPrimaryKeyIDValue)
		{
			AdsInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<AdsInfo>(" SELECT AutoID,PlaceID,AdName,AdType,AdLink,BeginDate,EndDate,AdText,AdMediaPath,AdDesc,IsNewWin,Hit,IsAudit,Sort,Lang,AutoTimeStamp FROM cms_Ads WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static AdsInfo GetTopData()
		{
			return Ads.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static AdsInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,PlaceID,AdName,AdType,AdLink,BeginDate,EndDate,AdText,AdMediaPath,AdDesc,IsNewWin,Hit,IsAudit,Sort,Lang,AutoTimeStamp FROM cms_Ads ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<AdsInfo>(text);
		}

		public static IList<AdsInfo> GetAllList()
		{
			return Ads.GetList(0, string.Empty);
		}

		public static IList<AdsInfo> GetTopNList(int intTopCount)
		{
			return Ads.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<AdsInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Ads.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<AdsInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Ads.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<AdsInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,PlaceID,AdName,AdType,AdLink,BeginDate,EndDate,AdText,AdMediaPath,AdDesc,IsNewWin,Hit,IsAudit,Sort,Lang,AutoTimeStamp from cms_Ads ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<AdsInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Ads", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Ads", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Ads.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Ads.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Ads.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Ads.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,PlaceID,AdName,AdType,AdLink,BeginDate,EndDate,AdText,AdMediaPath,AdDesc,IsNewWin,Hit,IsAudit,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_Ads";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<AdsInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Ads.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<AdsInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<AdsInfo> result = new List<AdsInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,PlaceID,AdName,AdType,AdLink,BeginDate,EndDate,AdText,AdMediaPath,AdDesc,IsNewWin,Hit,IsAudit,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_Ads";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<AdsInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_Ads SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_Ads SET Sort =",
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
