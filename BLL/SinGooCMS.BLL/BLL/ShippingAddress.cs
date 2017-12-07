using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class ShippingAddress : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_ShippingAddress ");
			}
		}

		public static ShippingAddressInfo GetDefShippingAddrByUID(int intUserID)
		{
			return (from p in ShippingAddress.GetShippingAddrByUID(intUserID)
			where p.IsDefault
			select p).FirstOrDefault<ShippingAddressInfo>();
		}

		public static IList<ShippingAddressInfo> GetShippingAddrByUID(int intUserID)
		{
			return BizBase.dbo.GetList<ShippingAddressInfo>("SELECT * FROM shop_ShippingAddress WHERE UserID=" + intUserID + " ORDER BY IsDefault DESC,AutoID DESC ");
		}

		public static bool SetDefShippingAddr(int intShippingAddrID, int intUserID)
		{
			return BizBase.dbo.ExecSQL(string.Concat(new string[]
			{
				"UPDATE shop_ShippingAddress SET IsDefault = 0 WHERE UserID=",
				intUserID.ToString(),
				";UPDATE shop_ShippingAddress SET IsDefault = 1 WHERE AutoID=",
				intShippingAddrID.ToString(),
				" AND UserID=",
				intUserID.ToString()
			}));
		}

		public static int Add(ShippingAddressInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<ShippingAddressInfo>(entity);
			}
			return result;
		}

		public static bool Update(ShippingAddressInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<ShippingAddressInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_ShippingAddress WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_ShippingAddress WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ShippingAddressInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ShippingAddressInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ShippingAddressInfo>(" SELECT TOP 1 AutoID,UserID,UserName,Consignee,Country,Province,City,County,Address,PostCode,ContactPhone,IsDefault,AutoTimeStamp FROM shop_ShippingAddress WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ShippingAddressInfo GetTopData()
		{
			return ShippingAddress.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ShippingAddressInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserID,UserName,Consignee,Country,Province,City,County,Address,PostCode,ContactPhone,IsDefault,AutoTimeStamp FROM shop_ShippingAddress ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ShippingAddressInfo>(text);
		}

		public static IList<ShippingAddressInfo> GetAllList()
		{
			return ShippingAddress.GetList(0, string.Empty);
		}

		public static IList<ShippingAddressInfo> GetTopNList(int intTopCount)
		{
			return ShippingAddress.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ShippingAddressInfo> GetTopNList(int intTopCount, string strSort)
		{
			return ShippingAddress.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ShippingAddressInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return ShippingAddress.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ShippingAddressInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserID,UserName,Consignee,Country,Province,City,County,Address,PostCode,ContactPhone,IsDefault,AutoTimeStamp from shop_ShippingAddress ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ShippingAddressInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ShippingAddress", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ShippingAddress", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return ShippingAddress.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ShippingAddress.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ShippingAddress.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ShippingAddress.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserID,UserName,Consignee,Country,Province,City,County,Address,PostCode,ContactPhone,IsDefault,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_ShippingAddress";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ShippingAddressInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return ShippingAddress.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ShippingAddressInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ShippingAddressInfo> result = new List<ShippingAddressInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserID,UserName,Consignee,Country,Province,City,County,Address,PostCode,ContactPhone,IsDefault,AutoTimeStamp";
			pager.PagerTable = "shop_ShippingAddress";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ShippingAddressInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_ShippingAddress SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_ShippingAddress SET Sort =",
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
