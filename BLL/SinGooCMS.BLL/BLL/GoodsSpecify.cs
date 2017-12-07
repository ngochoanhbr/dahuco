using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class GoodsSpecify : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_GoodsSpecify ");
			}
		}

		public static int Add(GoodsSpecifyInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<GoodsSpecifyInfo>(entity);
			}
			return result;
		}

		public static bool Update(GoodsSpecifyInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<GoodsSpecifyInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_GoodsSpecify WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_GoodsSpecify WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static GoodsSpecifyInfo GetDataById(int intPrimaryKeyIDValue)
		{
			GoodsSpecifyInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<GoodsSpecifyInfo>(" SELECT TOP 1 AutoID,Specification,ProID,ProductSN,PreviewImg,SellPrice,MemberPriceSet,PromotePrice,Stock,AlarmNum,Sort,Lang,AutoTimeStamp FROM shop_GoodsSpecify WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static GoodsSpecifyInfo GetTopData()
		{
			return GoodsSpecify.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static GoodsSpecifyInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,Specification,ProID,ProductSN,PreviewImg,SellPrice,MemberPriceSet,PromotePrice,Stock,AlarmNum,Sort,Lang,AutoTimeStamp FROM shop_GoodsSpecify ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<GoodsSpecifyInfo>(text);
		}

		public static IList<GoodsSpecifyInfo> GetAllList()
		{
			return GoodsSpecify.GetList(0, string.Empty);
		}

		public static IList<GoodsSpecifyInfo> GetTopNList(int intTopCount)
		{
			return GoodsSpecify.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<GoodsSpecifyInfo> GetTopNList(int intTopCount, string strSort)
		{
			return GoodsSpecify.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<GoodsSpecifyInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return GoodsSpecify.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<GoodsSpecifyInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,Specification,ProID,ProductSN,PreviewImg,SellPrice,MemberPriceSet,PromotePrice,Stock,AlarmNum,Sort,Lang,AutoTimeStamp from shop_GoodsSpecify ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<GoodsSpecifyInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_GoodsSpecify", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_GoodsSpecify", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return GoodsSpecify.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsSpecify.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsSpecify.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsSpecify.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,Specification,ProID,ProductSN,PreviewImg,SellPrice,MemberPriceSet,PromotePrice,Stock,AlarmNum,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_GoodsSpecify";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<GoodsSpecifyInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsSpecify.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<GoodsSpecifyInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<GoodsSpecifyInfo> result = new List<GoodsSpecifyInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,Specification,ProID,ProductSN,PreviewImg,SellPrice,MemberPriceSet,PromotePrice,Stock,AlarmNum,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "shop_GoodsSpecify";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<GoodsSpecifyInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_GoodsSpecify SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_GoodsSpecify SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static GoodsSpecifyInfo Get(int proID, string strGuiGe)
		{
			GoodsSpecifyInfo model = BizBase.dbo.GetModel<GoodsSpecifyInfo>(string.Concat(new object[]
			{
				" select top 1 * from shop_GoodsSpecify where ProID=",
				proID,
				" and Specification='",
				StringUtils.ChkSQL(strGuiGe),
				"' "
			}));
			GoodsSpecifyInfo result;
			if (model != null)
			{
				result = GoodsSpecify.Get(model);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static GoodsSpecifyInfo Get(int guigeID)
		{
			GoodsSpecifyInfo dataById = GoodsSpecify.GetDataById(guigeID);
			GoodsSpecifyInfo result;
			if (dataById != null)
			{
				result = GoodsSpecify.Get(GoodsSpecify.GetDataById(guigeID));
			}
			else
			{
				result = null;
			}
			return result;
		}

		private static GoodsSpecifyInfo Get(GoodsSpecifyInfo guige)
		{
			UserInfo user = User.GetLoginUser();
			if (user != null)
			{
				guige.MemberPriceSets = MemberPriceSet.GetList(guige.MemberPriceSet, guige.SellPrice);
				MemberPriceSetInfo memberPriceSetInfo = (from p in guige.MemberPriceSets
				where p.UserLevelID.Equals(user.LevelID)
				select p).FirstOrDefault<MemberPriceSetInfo>();
				if (memberPriceSetInfo != null)
				{
					guige.MemberPrice = ((memberPriceSetInfo.Price > 0m) ? memberPriceSetInfo.Price : memberPriceSetInfo.DiscoutPrice);
					if (guige.MemberPrice == 0m)
					{
						guige.MemberPrice = guige.SellPrice;
					}
				}
			}
			return guige;
		}

		public static bool DelByProID(int proID)
		{
			return BizBase.dbo.DeleteTable(" delete from shop_GoodsSpecify where ProID=" + proID);
		}

		public static IList<GoodsSpecifyInfo> GetListByProID(int proID)
		{
			return BizBase.dbo.GetList<GoodsSpecifyInfo>(" select * from shop_GoodsSpecify where ProID=" + proID);
		}

		public static List<decimal> GetPriceRange(ProductInfo pro)
		{
			List<decimal> list = new List<decimal>();
			string value = BizBase.dbo.GetValue<string>("select CONVERT(nvarchar,MIN(SellPrice))+'-'+CONVERT(nvarchar,MAX(SellPrice)) from shop_GoodsSpecify where ProID=" + pro.AutoID);
			if (!string.IsNullOrEmpty(value) && value.IndexOf("-") != -1)
			{
				list.Add(WebUtils.GetDecimal(value.Split(new char[]
				{
					'-'
				})[0]));
				list.Add(WebUtils.GetDecimal(value.Split(new char[]
				{
					'-'
				})[1]));
			}
			else
			{
				list.Add(pro.SellPrice);
				list.Add(pro.SellPrice);
			}
			return list;
		}

		public static bool Exists(int proID)
		{
			return BizBase.dbo.GetValue<int>(" select COUNT(*) from shop_GoodsSpecify where ProID=" + proID) > 0;
		}
	}
}
