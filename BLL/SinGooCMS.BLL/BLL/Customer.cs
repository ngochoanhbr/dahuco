using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
    public class Customer : BizBase
    {
        public static int MaxSort
        {
            get
            {
                return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM dh_Customer ");
            }
        }

        public static IList<CustomerInfo> GetCustomers()
        {
            return BizBase.dbo.GetList<CustomerInfo>(" SELECT * FROM dh_Customer ORDER BY IsRecommend ASC,Sort ASC,AutoID DESC ");
        }

        public static CustomerInfo GetCustomer(int intCustomerID)
        {
            return (from p in Customer.GetCustomers()
                    where p.AutoID.Equals(intCustomerID)
                    select p).FirstOrDefault<CustomerInfo>();
        }

        public static int Add(CustomerInfo entity)
        {
            int result;
            if (entity == null)
            {
                result = 0;
            }
            else
            {
                result = BizBase.dbo.InsertModel<CustomerInfo>(entity);
            }
            return result;
        }

        public static bool Update(CustomerInfo entity)
        {
            return entity != null && BizBase.dbo.UpdateModel<CustomerInfo>(entity);
        }

        public static bool Delete(int intPrimaryKeyIDValue)
        {
            return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM dh_Customer WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
        }

        public static bool Delete(string strArrIdList)
        {
            return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM dh_Customer WHERE AutoID in (" + strArrIdList + ") ");
        }

        public static CustomerInfo GetDataById(int intPrimaryKeyIDValue)
        {
            CustomerInfo result;
            if (intPrimaryKeyIDValue <= 0)
            {
                result = null;
            }
            else
            {
                result = BizBase.dbo.GetModel<CustomerInfo>(" SELECT AutoID, PortName, PortCode, EnglishName, BussinessStatus, Under, PortLocationX, PortLocationY, PilotLocation, Address, Phone, Email, Website, TotalLength, TidalRegime, AverageVariation, " +
                         "MaximumDraft, MaximumSize, TotalPortArea, TotalWarehousingArea, Warehouses, Tank, CargoThroughput, IsRecommend, Sort, AutoTimeStamp FROM dh_Customer WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
            }
            return result;
        }

        public static CustomerInfo GetTopData()
        {
            return Customer.GetTopData(" Sort ASC,AutoID desc ");
        }

        public static CustomerInfo GetTopData(string strSort)
        {
            string text = " SELECT TOP 1 AutoID, PortName, PortCode, EnglishName, BussinessStatus, Under, PortLocationX, PortLocationY, PilotLocation, Address, Phone, Email, Website, TotalLength, TidalRegime, AverageVariation, " +
                         "MaximumDraft, MaximumSize, TotalPortArea, TotalWarehousingArea, Warehouses, Tank, CargoThroughput, IsRecommend, Sort, AutoTimeStamp FROM dh_Customer ";
            if (!string.IsNullOrEmpty(strSort))
            {
                text = text + " order by " + strSort;
            }
            return BizBase.dbo.GetModel<CustomerInfo>(text);
        }

        public static IList<CustomerInfo> GetAllList()
        {
            return Customer.GetList(0, string.Empty);
        }

        public static IList<CustomerInfo> GetTopNList(int intTopCount)
        {
            return Customer.GetTopNList(intTopCount, string.Empty);
        }

        public static IList<CustomerInfo> GetTopNList(int intTopCount, string strSort)
        {
            return Customer.GetList(intTopCount, string.Empty, strSort);
        }

        public static IList<CustomerInfo> GetList(int intTopCount, string strCondition)
        {
            string strSort = " Sort asc,AutoID desc ";
            return Customer.GetList(intTopCount, strCondition, strSort);
        }

        public static IList<CustomerInfo> GetList(int intTopCount, string strCondition, string strSort)
        {
            StringBuilder stringBuilder = new StringBuilder(" select ");
            if (intTopCount > 0)
            {
                stringBuilder.Append(" top " + intTopCount.ToString());
            }
            stringBuilder.Append(" AutoID, PortName, PortCode, EnglishName, BussinessStatus, Under, PortLocationX, PortLocationY, PilotLocation, Address, Phone, Email, Website, TotalLength, TidalRegime, AverageVariation, " +
                         "MaximumDraft, MaximumSize, TotalPortArea, TotalWarehousingArea, Warehouses, Tank, CargoThroughput, IsRecommend, Sort, AutoTimeStamp from dh_Customer ");
            if (!string.IsNullOrEmpty(strCondition))
            {
                stringBuilder.Append(" where " + strCondition);
            }
            if (!string.IsNullOrEmpty(strSort))
            {
                stringBuilder.Append(" order by " + strSort);
            }
            return BizBase.dbo.GetList<CustomerInfo>(stringBuilder.ToString());
        }

        public static int GetCount()
        {
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Customer", "", "") ?? 0;
        }

        public static int GetCount(string strCondition)
        {
            return BizBase.dbo.GetValue<int?>("Count(*)", "dh_Customer", strCondition, "") ?? 0;
        }

        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
        {
            int num = 0;
            int num2 = 0;
            return Customer.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
        }

        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Customer.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Customer.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return Customer.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }

        public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            DataSet result = new DataSet();
            if (strFilter == string.Empty || strFilter == "*")
            {
                strFilter = "AutoID, PortName, PortCode, EnglishName, BussinessStatus, Under, PortLocationX, PortLocationY, PilotLocation, Address, Phone, Email, Website, TotalLength, TidalRegime, AverageVariation, " +
                         "MaximumDraft, MaximumSize, TotalPortArea, TotalWarehousingArea, Warehouses, Tank, CargoThroughput, IsRecommend, Sort, AutoTimeStamp";
            }
            Pager pager = new Pager();
            pager.PagerFilter = strFilter;
            pager.PagerTable = "dh_Customer";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetData();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;
            return result;
        }

        public static IList<CustomerInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            return Customer.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
        }

        public static IList<CustomerInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            IList<CustomerInfo> result = new List<CustomerInfo>();
            Pager pager = new Pager();
            pager.PagerFilter = "AutoID, PortName, PortCode, EnglishName, BussinessStatus, Under, PortLocationX, PortLocationY, PilotLocation, Address, Phone, Email, Website, TotalLength, TidalRegime, AverageVariation, " +
                         "MaximumDraft, MaximumSize, TotalPortArea, TotalWarehousingArea, Warehouses, Tank, CargoThroughput, IsRecommend, Sort, AutoTimeStamp";
            pager.PagerTable = "dh_Customer";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;
            result = pager.GetPagerList<CustomerInfo>();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;
            return result;
        }

        public static void UpdateSort(int intPrimaryKeyValue, int intSort)
        {
            BizBase.dbo.ExecSQL(" UPDATE dh_Customer SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE dh_Customer SET Sort =",
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
