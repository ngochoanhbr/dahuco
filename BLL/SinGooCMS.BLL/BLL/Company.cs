using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
    public class Company : BizBase
    {
        public static int MaxSort
        {
            get
            {
                return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM dh_Company ");
            }
        }

        public static IList<CompanyInfo> GetCompanys()
        {
            return BizBase.dbo.GetList<CompanyInfo>(" SELECT * FROM dh_Company ORDER BY IsRecommend ASC,Sort ASC,AutoID DESC ");
        }

        public static CompanyInfo GetCompany(int intCompanyID)
        {
            return (from p in Company.GetCompanys()
                    where p.AutoID.Equals(intCompanyID)
                    select p).FirstOrDefault<CompanyInfo>();
        }

        public static int Add(CompanyInfo entity)
        {
            int result;
            if (entity == null)
            {
                result = 0;
            }
            else
            {
                result = BizBase.dbo.InsertModel<CompanyInfo>(entity);
            }
            return result;
        }

        public static bool Update(CompanyInfo entity)
        {
            return entity != null && BizBase.dbo.UpdateModel<CompanyInfo>(entity);
        }

        public static bool Delete(int intPrimaryKeyIDValue)
        {
            return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM dh_Company WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
        }

        public static bool Delete(string strArrIdList)
        {
            return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM dh_Company WHERE AutoID in (" + strArrIdList + ") ");
        }

        public static CompanyInfo GetDataById(int intPrimaryKeyIDValue)
        {
            CompanyInfo result;
            if (intPrimaryKeyIDValue <= 0)
            {
                result = null;
            }
            else
            {
                result = BizBase.dbo.GetModel<CompanyInfo>(" SELECT AutoID, Name, Address, Phone, IsRecommend, Sort, AutoTimeStamp FROM dh_Company WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
            }
            return result;
        }

        public static CompanyInfo GetTopData()
        {
            return Company.GetTopData(" Sort ASC,AutoID desc ");
        }

        public static CompanyInfo GetTopData(string strSort)
        {
            string text = " SELECT TOP 1 AutoID, Name, Address, Phone, IsRecommend, Sort, AutoTimeStamp FROM dh_Company ";
            if (!string.IsNullOrEmpty(strSort))
            {
                text = text + " order by " + strSort;
            }
            return BizBase.dbo.GetModel<CompanyInfo>(text);
        }

        public static IList<CompanyInfo> GetAllList()
        {
            return Company.GetList(0, string.Empty);
        }

        public static IList<CompanyInfo> GetTopNList(int intTopCount)
        {
            return Company.GetTopNList(intTopCount, string.Empty);
        }

        public static IList<CompanyInfo> GetTopNList(int intTopCount, string strSort)
        {
            return Company.GetList(intTopCount, string.Empty, strSort);
        }

        public static IList<CompanyInfo> GetList(int intTopCount, string strCondition)
        {
            string strSort = " Sort asc,AutoID desc ";
            return Company.GetList(intTopCount, strCondition, strSort);
        }

        public static IList<CompanyInfo> GetList(int intTopCount, string strCondition, string strSort)
        {
            StringBuilder stringBuilder = new StringBuilder(" select ");
            if (intTopCount > 0)
            {
                stringBuilder.Append(" top " + intTopCount.ToString());
            }
            stringBuilder.Append(" AutoID, Name, Address, Phone, IsRecommend, Sort, AutoTimeStamp from dh_Company ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                stringBuilder.Append(" where " + strCondition);
            }
            if (!string.IsNullOrEmpty(strSort))
            {
                stringBuilder.Append(" order by " + strSort);
            }
            return BizBase.dbo.GetList<CompanyInfo>(stringBuilder.ToString());
        }

        public static int GetCount()
        {
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Company", "", "") ?? 0;
        }

        public static int GetCount(string strCondition)
        {
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Company", strCondition, "") ?? 0;
        }

        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
        {
            int num = 0;
            int num2 = 0;
            return Company.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
        }

        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Company.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Company.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Company.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            DataSet result = new DataSet();
            if (strFilter == string.Empty || strFilter == "*")
            {
                strFilter = "AutoID, Name, Address, Phone, IsRecommend, Sort, AutoTimeStamp";
            }
            Pager pager = new Pager();
            pager.PagerFilter = strFilter;
            pager.PagerTable = "dh_Company";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetData();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;
            return result;
        }

        public static IList<CompanyInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            return Company.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
        }

        public static IList<CompanyInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            IList<CompanyInfo> result = new List<CompanyInfo>();
            Pager pager = new Pager();
            pager.PagerFilter = "AutoID, Name, Address, Phone, IsRecommend, Sort, AutoTimeStamp";
            pager.PagerTable = "dh_Company";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetPagerList<CompanyInfo>();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;
            return result;
        }

        public static void UpdateSort(int intPrimaryKeyValue, int intSort)
        {
            BizBase.dbo.ExecSQL(" UPDATE dh_Company SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE dh_Company SET Sort =",
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
