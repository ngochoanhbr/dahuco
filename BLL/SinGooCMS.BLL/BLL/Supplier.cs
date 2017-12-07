using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Supplier : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM dh_supplier ");
			}
		}

		public static IList<SupplierInfo> GetBrands()
		{
            return BizBase.dbo.GetList<SupplierInfo>(" SELECT * FROM dh_supplier ORDER BY ID DESC ");
		}

        public static SupplierInfo GetBrand(int intBrandID)
		{
			return (from p in Supplier.GetBrands()
			where p.AutoID.Equals(intBrandID)
			select p).FirstOrDefault<SupplierInfo>();
		}

        public static int Add(SupplierInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
                result = BizBase.dbo.InsertModel<SupplierInfo>(entity);
			}
			return result;
		}

        public static bool Update(SupplierInfo entity)
		{
            return entity != null && BizBase.dbo.UpdateModel<SupplierInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
            return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM dh_supplier WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
            return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM dh_supplier WHERE AutoID in (" + strArrIdList + ") ");
		}

        public static SupplierInfo GetDataById(int intPrimaryKeyIDValue)
		{
            SupplierInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
                result = BizBase.dbo.GetModel<SupplierInfo>(" SELECT AutoID, Name, Address, Country, AutoTimeStamp FROM dh_supplier WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

        public static SupplierInfo GetTopData()
		{
			return Supplier.GetTopData(" AutoID desc ");
		}

        public static SupplierInfo GetTopData(string strSort)
		{
            string text = " SELECT TOP 1 AutoID, Name, Address, Country, AutoTimeStamp FROM dh_supplier ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
            return BizBase.dbo.GetModel<SupplierInfo>(text);
		}

        public static IList<SupplierInfo> GetAllList()
		{
			return Supplier.GetList(0, string.Empty);
		}

        public static IList<SupplierInfo> GetTopNList(int intTopCount)
		{
            return Supplier.GetTopNList(intTopCount, string.Empty);
		}

        public static IList<SupplierInfo> GetTopNList(int intTopCount, string strSort)
		{
            return Supplier.GetList(intTopCount, string.Empty, strSort);
		}

        public static IList<SupplierInfo> GetList(int intTopCount, string strCondition)
		{
            string strSort = " AutoID desc ";
            return Supplier.GetList(intTopCount, strCondition, strSort);
		}

        public static IList<SupplierInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
            stringBuilder.Append(" AutoID, Name, Address, Country, AutoTimeStamp from dh_supplier ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
            return BizBase.dbo.GetList<SupplierInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_supplier", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_supplier", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
            return Supplier.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
            return Supplier.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
            return Supplier.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
            return Supplier.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
                strFilter = "AutoID, Name, Address, Country, AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
            pager.PagerTable = "dh_supplier";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

        public static IList<SupplierInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
            return Supplier.GetPagerList("", "AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

        public static IList<SupplierInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
            IList<SupplierInfo> result = new List<SupplierInfo>();
			Pager pager = new Pager();
            pager.PagerFilter = "AutoID, Name, Address, Country, AutoTimeStamp";
            pager.PagerTable = "dh_supplier";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetPagerList<SupplierInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
            BizBase.dbo.ExecSQL(" UPDATE dh_supplier SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE dh_supplier SET Sort =",
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
