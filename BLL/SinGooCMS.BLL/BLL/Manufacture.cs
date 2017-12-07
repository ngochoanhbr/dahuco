using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Manufacture : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM dh_Manufacture ");
			}
		}

		public static IList<ManufactureInfo> GetBrands()
		{
            return BizBase.dbo.GetList<ManufactureInfo>(" SELECT * FROM dh_Manufacture ORDER BY ID DESC ");
		}

        public static ManufactureInfo GetBrand(int intBrandID)
		{
			return (from p in Manufacture.GetBrands()
			where p.AutoID.Equals(intBrandID)
			select p).FirstOrDefault<ManufactureInfo>();
		}

        public static int Add(ManufactureInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
                result = BizBase.dbo.InsertModel<ManufactureInfo>(entity);
			}
			return result;
		}

        public static bool Update(ManufactureInfo entity)
		{
            return entity != null && BizBase.dbo.UpdateModel<ManufactureInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
            return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM dh_Manufacture WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
            return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM dh_Manufacture WHERE AutoID in (" + strArrIdList + ") ");
		}

        public static ManufactureInfo GetDataById(int intPrimaryKeyIDValue)
		{
            ManufactureInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
                result = BizBase.dbo.GetModel<ManufactureInfo>(" SELECT AutoID, Name, Country, AutoTimeStamp FROM dh_Manufacture WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

        public static ManufactureInfo GetTopData()
		{
			return Manufacture.GetTopData(" AutoID desc ");
		}

        public static ManufactureInfo GetTopData(string strSort)
		{
            string text = " SELECT TOP 1 AutoID, Name, Country, AutoTimeStamp FROM dh_Manufacture ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
            return BizBase.dbo.GetModel<ManufactureInfo>(text);
		}

        public static IList<ManufactureInfo> GetAllList()
		{
			return Manufacture.GetList(0, string.Empty);
		}

        public static IList<ManufactureInfo> GetTopNList(int intTopCount)
		{
            return Manufacture.GetTopNList(intTopCount, string.Empty);
		}

        public static IList<ManufactureInfo> GetTopNList(int intTopCount, string strSort)
		{
            return Manufacture.GetList(intTopCount, string.Empty, strSort);
		}

        public static IList<ManufactureInfo> GetList(int intTopCount, string strCondition)
		{
            string strSort = " AutoID desc ";
            return Manufacture.GetList(intTopCount, strCondition, strSort);
		}

        public static IList<ManufactureInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
            stringBuilder.Append(" AutoID, Name, Country, AutoTimeStamp from dh_Manufacture ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
            return BizBase.dbo.GetList<ManufactureInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Manufacture", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Manufacture", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
            return Manufacture.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
            return Manufacture.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
            return Manufacture.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
            return Manufacture.GetPagerData(strFilter, strCondition, " AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
                strFilter = "AutoID, Name, Country, AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
            pager.PagerTable = "dh_Manufacture";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

        public static IList<ManufactureInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
            return Manufacture.GetPagerList("", "AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

        public static IList<ManufactureInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
            IList<ManufactureInfo> result = new List<ManufactureInfo>();
			Pager pager = new Pager();
            pager.PagerFilter = "AutoID, Name, Country, AutoTimeStamp";
            pager.PagerTable = "dh_Manufacture";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetPagerList<ManufactureInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
            BizBase.dbo.ExecSQL(" UPDATE dh_Manufacture SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE dh_Manufacture SET Sort =",
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
