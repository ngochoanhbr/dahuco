using SinGooCMS.DAL;
using SinGooCMS.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SinGooCMS.BLL
{
	public class Stat : BizBase
	{
		public static DataTable GetTrafficStat(string strType, DateTime dtStart, DateTime dtEnd)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@type", strType),
				new SqlParameter("@timestart", dtStart.ToString("yyyy-MM-dd HH:mm:ss")),
				new SqlParameter("@timeend", dtEnd.ToString("yyyy-MM-dd HH:mm:ss"))
			};
			return BizBase.dbo.ExecProcReDT("p_System_TrafficStat", arrParam);
		}

		public static DataTable GetOrderStat(string strType, DateTime dtStart, DateTime dtEnd)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@type", strType),
				new SqlParameter("@timestart", dtStart.ToString("yyyy-MM-dd HH:mm:ss")),
				new SqlParameter("@timeend", dtEnd.ToString("yyyy-MM-dd HH:mm:ss"))
			};
			return BizBase.dbo.ExecProcReDT("p_System_OrderStat", arrParam);
		}

		public static DataTable GetSellStat()
		{
			DataTable dataTable = BizBase.dbo.ExecProcReDT("p_System_ProductSellStat", null);
			double num = 0.0;
			foreach (DataRow dataRow in dataTable.Rows)
			{
				num += WebUtils.GetDouble(dataRow["bfb"]);
			}
			if (num < 100.0)
			{
				double num2 = 100.0 - num;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2.ItemArray = new object[]
				{
					0,
					"Khác",
					num2.ToString("f2")
				};
				dataTable.Rows.Add(dataRow2);
			}
			return dataTable;
		}

		public static DataSet GetPurchaseRate(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "v_System_GetPurchaseRate";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static DataSet GetUserBuyRate(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "v_System_GetUserBuyRate";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static DataTable GetUserAreaStat()
		{
			return BizBase.dbo.GetDataTable(" select Province,COUNT(*) as TotalUser from cms_User where Province is not null and Province<>'' group by Province order by COUNT(*) desc ");
		}

		public static DataTable GetUserGrowthStat(string strType, DateTime dtStart, DateTime dtEnd)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@type", strType),
				new SqlParameter("@timestart", dtStart.ToString("yyyy-MM-dd HH:mm:ss")),
				new SqlParameter("@timeend", dtEnd.ToString("yyyy-MM-dd HH:mm:ss"))
			};
			return BizBase.dbo.ExecProcReDT("p_System_UserGrowthStat", arrParam);
		}
	}
}
