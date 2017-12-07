using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Favorites : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Favorites ");
			}
		}

		public static IList<FavoritesInfo> GetFavoritesByUID(int intUserID)
		{
			return BizBase.dbo.GetList<FavoritesInfo>(" SELECT * FROM shop_Favorites WHERE UserID=" + intUserID);
		}

		public static FavoritesInfo GetFavorite(int intProID, int intUserID)
		{
			return BizBase.dbo.GetModel<FavoritesInfo>(string.Concat(new object[]
			{
				" select top 1 * from shop_Favorites where ProductID=",
				intProID,
				" and UserID= ",
				intUserID
			}));
		}

		public static DataSet GetPagerDataExt(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty)
			{
				strFilter = "*";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "v_System_GoodsFavorites";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<FavoritesInfo> GetPagerListExt(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<FavoritesInfo> result = new List<FavoritesInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "*";
			pager.PagerTable = "v_System_GoodsFavorites";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<FavoritesInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static int Add(FavoritesInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<FavoritesInfo>(entity);
			}
			return result;
		}

		public static bool Update(FavoritesInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<FavoritesInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Favorites WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Favorites WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static FavoritesInfo GetDataById(int intPrimaryKeyIDValue)
		{
			FavoritesInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<FavoritesInfo>(" SELECT TOP 1 AutoID,UserID,ProductID,ProductName,ProductImage,Price,Lang,AutoTimeStamp FROM shop_Favorites WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static FavoritesInfo GetTopData()
		{
			return Favorites.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static FavoritesInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserID,ProductID,ProductName,ProductImage,Price,Lang,AutoTimeStamp FROM shop_Favorites ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<FavoritesInfo>(text);
		}

		public static IList<FavoritesInfo> GetAllList()
		{
			return Favorites.GetList(0, string.Empty);
		}

		public static IList<FavoritesInfo> GetTopNList(int intTopCount)
		{
			return Favorites.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<FavoritesInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Favorites.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<FavoritesInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Favorites.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<FavoritesInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserID,ProductID,ProductName,ProductImage,Price,Lang,AutoTimeStamp from shop_Favorites ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<FavoritesInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Favorites", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Favorites", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Favorites.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Favorites.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Favorites.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Favorites.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserID,ProductID,ProductName,ProductImage,Price,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Favorites";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<FavoritesInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Favorites.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<FavoritesInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<FavoritesInfo> result = new List<FavoritesInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserID,ProductID,ProductName,ProductImage,Price,Lang,AutoTimeStamp";
			pager.PagerTable = "shop_Favorites";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<FavoritesInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Favorites SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Favorites SET Sort =",
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
