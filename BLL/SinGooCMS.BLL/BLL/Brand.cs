using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Brand : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Brand ");
			}
		}

		public static IList<BrandInfo> GetBrands()
		{
			return BizBase.dbo.GetList<BrandInfo>(" SELECT * FROM shop_Brand ORDER BY IsRecommend ASC,Sort ASC,AutoID DESC ");
		}

		public static BrandInfo GetBrand(int intBrandID)
		{
			return (from p in Brand.GetBrands()
			where p.AutoID.Equals(intBrandID)
			select p).FirstOrDefault<BrandInfo>();
		}

		public static int Add(BrandInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<BrandInfo>(entity);
			}
			return result;
		}

		public static bool Update(BrandInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<BrandInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Brand WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Brand WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static BrandInfo GetDataById(int intPrimaryKeyIDValue)
		{
			BrandInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<BrandInfo>(" SELECT AutoID,BrandName,CompanyName,LogoPath,OfficialSite,IndName,BrandDesc,IsRecommend,Sort,AutoTimeStamp FROM shop_Brand WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static BrandInfo GetTopData()
		{
			return Brand.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static BrandInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,BrandName,CompanyName,LogoPath,OfficialSite,IndName,BrandDesc,IsRecommend,Sort,AutoTimeStamp FROM shop_Brand ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<BrandInfo>(text);
		}

		public static IList<BrandInfo> GetAllList()
		{
			return Brand.GetList(0, string.Empty);
		}

		public static IList<BrandInfo> GetTopNList(int intTopCount)
		{
			return Brand.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<BrandInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Brand.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<BrandInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Brand.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<BrandInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,BrandName,CompanyName,LogoPath,OfficialSite,IndName,BrandDesc,IsRecommend,Sort,AutoTimeStamp from shop_Brand ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<BrandInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Brand", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Brand", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Brand.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Brand.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Brand.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Brand.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,BrandName,CompanyName,LogoPath,OfficialSite,IndName,BrandDesc,IsRecommend,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Brand";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<BrandInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Brand.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<BrandInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<BrandInfo> result = new List<BrandInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,BrandName,CompanyName,LogoPath,OfficialSite,IndName,BrandDesc,IsRecommend,Sort,AutoTimeStamp";
			pager.PagerTable = "shop_Brand";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<BrandInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Brand SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Brand SET Sort =",
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
