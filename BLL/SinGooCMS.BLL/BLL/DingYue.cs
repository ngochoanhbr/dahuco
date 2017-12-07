using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class DingYue : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_DingYue ");
			}
		}

		public static int Add(DingYueInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<DingYueInfo>(entity);
			}
			return result;
		}

		public static bool Update(DingYueInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<DingYueInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_DingYue WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_DingYue WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static DingYueInfo GetDataById(int intPrimaryKeyIDValue)
		{
			DingYueInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<DingYueInfo>(" SELECT TOP 1 AutoID,UserName,Email,IsTuiDing,Lang,AutoTimeStamp FROM cms_DingYue WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static DingYueInfo GetTopData()
		{
			return DingYue.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static DingYueInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserName,Email,IsTuiDing,Lang,AutoTimeStamp FROM cms_DingYue ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<DingYueInfo>(text);
		}

		public static IList<DingYueInfo> GetAllList()
		{
			return DingYue.GetList(0, string.Empty);
		}

		public static IList<DingYueInfo> GetTopNList(int intTopCount)
		{
			return DingYue.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<DingYueInfo> GetTopNList(int intTopCount, string strSort)
		{
			return DingYue.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<DingYueInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return DingYue.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<DingYueInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserName,Email,IsTuiDing,Lang,AutoTimeStamp from cms_DingYue ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<DingYueInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_DingYue", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_DingYue", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return DingYue.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return DingYue.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return DingYue.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return DingYue.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserName,Email,IsTuiDing,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_DingYue";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<DingYueInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return DingYue.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<DingYueInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<DingYueInfo> result = new List<DingYueInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserName,Email,IsTuiDing,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_DingYue";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<DingYueInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_DingYue SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_DingYue SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static int Add(string strEmailAddr)
		{
			DingYueInfo entity = new DingYueInfo
			{
				Email = strEmailAddr,
				IsTuiDing = false,
				Lang = JObject.cultureLang,
				AutoTimeStamp = DateTime.Now
			};
			int result;
			if (DingYue.ExistsEmailAddr(strEmailAddr))
			{
				result = -1;
			}
			else
			{
				result = DingYue.Add(entity);
			}
			return result;
		}

		public static DingYueInfo GetByEmail(string strEmail)
		{
			return BizBase.dbo.GetModel<DingYueInfo>(" select top 1 * from cms_DingYue where Email='" + strEmail + "' ");
		}

		public static bool ExistsEmailAddr(string strEmailAddr)
		{
			return DingYue.GetCount(" Email='" + StringUtils.ChkSQL(strEmailAddr) + "' ") > 0;
		}

		public static void DuiDing(string strEmailAddr)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_DingYue SET IsTuiDing=1 WHERE Email='" + strEmailAddr + "' ");
		}
	}
}
