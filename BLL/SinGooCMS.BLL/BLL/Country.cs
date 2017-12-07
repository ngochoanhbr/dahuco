using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
    public class Country : BizBase
    {
        public static int MaxSort
        {
            get
            {
                return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM dh_Country ");
            }
        }

        public static IList<CountryInfo> GetCountrys()
        {
            return BizBase.dbo.GetList<CountryInfo>(" SELECT * FROM dh_Country ORDER BY AutoID DESC ");
        }

        public static CountryInfo GetCountry(int intCountryID)
        {
            return (from p in Country.GetCountrys()
                    where p.AutoID.Equals(intCountryID)
                    select p).FirstOrDefault<CountryInfo>();
        }

        public static int Add(CountryInfo entity)
        {
            int result;
            if (entity == null)
            {
                result = 0;
            }
            else
            {
                result = BizBase.dbo.InsertModel<CountryInfo>(entity);
            }
            return result;
        }

        public static bool Update(CountryInfo entity)
        {
            return entity != null && BizBase.dbo.UpdateModel<CountryInfo>(entity);
        }

        public static bool Delete(int intPrimaryKeyIDValue)
        {
            return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM dh_Country WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
        }

        public static bool Delete(string strArrIdList)
        {
            return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM dh_Country WHERE AutoID in (" + strArrIdList + ") ");
        }

        public static CountryInfo GetDataById(int intPrimaryKeyIDValue)
        {
            CountryInfo result;
            if (intPrimaryKeyIDValue <= 0)
            {
                result = null;
            }
            else
            {
                result = BizBase.dbo.GetModel<CountryInfo>(" SELECT AutoID, Name, AutoTimeStamp FROM dh_Country WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
            }
            return result;
        }

        public static CountryInfo GetTopData()
        {
            return Country.GetTopData(" Sort ASC,AutoID desc ");
        }

        public static CountryInfo GetTopData(string strSort)
        {
            string text = " SELECT TOP 1 AutoID, Name, AutoTimeStamp FROM dh_Country ";
            if (!string.IsNullOrEmpty(strSort))
            {
                text = text + " order by " + strSort;
            }
            return BizBase.dbo.GetModel<CountryInfo>(text);
        }

        public static IList<CountryInfo> GetAllList()
        {
            return Country.GetList(0, string.Empty);
        }

        public static IList<CountryInfo> GetTopNList(int intTopCount)
        {
            return Country.GetTopNList(intTopCount, string.Empty);
        }

        public static IList<CountryInfo> GetTopNList(int intTopCount, string strSort)
        {
            return Country.GetList(intTopCount, string.Empty, strSort);
        }

        public static IList<CountryInfo> GetList(int intTopCount, string strCondition)
        {
            string strSort = " Sort asc,AutoID desc ";
            return Country.GetList(intTopCount, strCondition, strSort);
        }

        public static IList<CountryInfo> GetList(int intTopCount, string strCondition, string strSort)
        {
            StringBuilder stringBuilder = new StringBuilder(" select ");
            if (intTopCount > 0)
            {
                stringBuilder.Append(" top " + intTopCount.ToString());
            }
            stringBuilder.Append(" AutoID, Name, AutoTimeStamp from dh_Country ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                stringBuilder.Append(" where " + strCondition);
            }
            if (!string.IsNullOrEmpty(strSort))
            {
                stringBuilder.Append(" order by " + strSort);
            }
            return BizBase.dbo.GetList<CountryInfo>(stringBuilder.ToString());
        }

        public static int GetCount()
        {
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Country", "", "") ?? 0;
        }

        public static int GetCount(string strCondition)
        {
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Country", strCondition, "") ?? 0;
        }

        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
        {
            int num = 0;
            int num2 = 0;
            return Country.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
        }

        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Country.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Country.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Country.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            DataSet result = new DataSet();
            if (strFilter == string.Empty || strFilter == "*")
            {
                strFilter = "AutoID, Name, AutoTimeStamp";
            }
            Pager pager = new Pager();
            pager.PagerFilter = strFilter;
            pager.PagerTable = "dh_Country";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetData();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;
            return result;
        }

        public static IList<CountryInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            return Country.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
        }

        public static IList<CountryInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            IList<CountryInfo> result = new List<CountryInfo>();
            Pager pager = new Pager();
            pager.PagerFilter = "AutoID, Name, AutoTimeStamp";
            pager.PagerTable = "dh_Country";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetPagerList<CountryInfo>();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;
            return result;
        }

        public static void UpdateSort(int intPrimaryKeyValue, int intSort)
        {
            BizBase.dbo.ExecSQL(" UPDATE dh_Country SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE dh_Country SET Sort =",
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
