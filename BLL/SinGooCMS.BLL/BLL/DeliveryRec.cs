using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class DeliveryRec : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_DeliveryRec ");
			}
		}

		public static int Add(DeliveryRecInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<DeliveryRecInfo>(entity);
			}
			return result;
		}

		public static bool Update(DeliveryRecInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<DeliveryRecInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_DeliveryRec WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_DeliveryRec WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static DeliveryRecInfo GetDataById(int intPrimaryKeyIDValue)
		{
			DeliveryRecInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<DeliveryRecInfo>(" SELECT TOP 1 AutoID,OrderNo,ProductName,ShippingType,KuaidiCom,ShippingNo,ShippingFee,Sender,AutoTimeStamp FROM shop_DeliveryRec WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static DeliveryRecInfo GetTopData()
		{
			return DeliveryRec.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static DeliveryRecInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderNo,ProductName,ShippingType,KuaidiCom,ShippingNo,ShippingFee,Sender,AutoTimeStamp FROM shop_DeliveryRec ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<DeliveryRecInfo>(text);
		}

		public static IList<DeliveryRecInfo> GetAllList()
		{
			return DeliveryRec.GetList(0, string.Empty);
		}

		public static IList<DeliveryRecInfo> GetTopNList(int intTopCount)
		{
			return DeliveryRec.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<DeliveryRecInfo> GetTopNList(int intTopCount, string strSort)
		{
			return DeliveryRec.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<DeliveryRecInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return DeliveryRec.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<DeliveryRecInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderNo,ProductName,ShippingType,KuaidiCom,ShippingNo,ShippingFee,Sender,AutoTimeStamp from shop_DeliveryRec ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<DeliveryRecInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_DeliveryRec", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_DeliveryRec", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return DeliveryRec.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return DeliveryRec.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return DeliveryRec.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return DeliveryRec.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderNo,ProductName,ShippingType,KuaidiCom,ShippingNo,ShippingFee,Sender,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_DeliveryRec";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<DeliveryRecInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return DeliveryRec.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<DeliveryRecInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<DeliveryRecInfo> result = new List<DeliveryRecInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderNo,ProductName,ShippingType,KuaidiCom,ShippingNo,ShippingFee,Sender,AutoTimeStamp";
			pager.PagerTable = "shop_DeliveryRec";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<DeliveryRecInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_DeliveryRec SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_DeliveryRec SET Sort =",
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
