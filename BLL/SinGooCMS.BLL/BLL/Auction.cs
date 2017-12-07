using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Auction : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Auction ");
			}
		}

		public static int Add(AuctionInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<AuctionInfo>(entity);
			}
			return result;
		}

		public static bool Update(AuctionInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<AuctionInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Auction WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Auction WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static AuctionInfo GetDataById(int intPrimaryKeyIDValue)
		{
			AuctionInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<AuctionInfo>(" SELECT TOP 1 AutoID,ProID,ActName,ActImage,StartTime,EndTime,BasePrice,StepPrice,ProtectPrice,TopPrice,CashDeposit,ActDesc,Sort,AutoTimeStamp FROM shop_Auction WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static AuctionInfo GetTopData()
		{
			return Auction.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static AuctionInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ProID,ActName,ActImage,StartTime,EndTime,BasePrice,StepPrice,ProtectPrice,TopPrice,CashDeposit,ActDesc,Sort,AutoTimeStamp FROM shop_Auction ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<AuctionInfo>(text);
		}

		public static IList<AuctionInfo> GetAllList()
		{
			return Auction.GetList(0, string.Empty);
		}

		public static IList<AuctionInfo> GetTopNList(int intTopCount)
		{
			return Auction.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<AuctionInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Auction.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<AuctionInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Auction.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<AuctionInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ProID,ActName,ActImage,StartTime,EndTime,BasePrice,StepPrice,ProtectPrice,TopPrice,CashDeposit,ActDesc,Sort,AutoTimeStamp from shop_Auction ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<AuctionInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Auction", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Auction", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Auction.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Auction.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Auction.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Auction.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ProID,ActName,ActImage,StartTime,EndTime,BasePrice,StepPrice,ProtectPrice,TopPrice,CashDeposit,ActDesc,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Auction";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<AuctionInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Auction.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<AuctionInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<AuctionInfo> result = new List<AuctionInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ProID,ActName,ActImage,StartTime,EndTime,BasePrice,StepPrice,ProtectPrice,TopPrice,CashDeposit,ActDesc,Sort,AutoTimeStamp";
			pager.PagerTable = "shop_Auction";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<AuctionInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Auction SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Auction SET Sort =",
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
