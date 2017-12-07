using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class SiteTemplate : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_SiteTemplate ");
			}
		}

		public static IList<SiteTemplateInfo> GetCacheSiteTemplateList()
		{
			IList<SiteTemplateInfo> list = (IList<SiteTemplateInfo>)CacheUtils.Get("JsonLeeCMS_CacheForSiteTemplate");
			if (list == null)
			{
				list = BizBase.dbo.GetList<SiteTemplateInfo>(" SELECT * FROM cms_SiteTemplate ");
				CacheUtils.Insert("JsonLeeCMS_CacheForSiteTemplate", list);
			}
			return list;
		}

		public static SiteTemplateInfo GetCacheSiteTemplateByID(int intID)
		{
			return (from p in SiteTemplate.GetCacheSiteTemplateList()
			where p.AutoID.Equals(intID)
			select p).FirstOrDefault<SiteTemplateInfo>();
		}

		public static SiteTemplateInfo GetDefaultTemplate()
		{
			return (from p in SiteTemplate.GetCacheSiteTemplateList()
			where p.IsDefault
			select p).FirstOrDefault<SiteTemplateInfo>();
		}

		public static bool SetDefaultTemplate(int intTemplateID)
		{
			string strSQL = string.Format(" UPDATE cms_SiteTemplate SET IsDefault=0;UPDATE cms_SiteTemplate SET IsDefault=1 WHERE AutoID={0}; ", intTemplateID.ToString());
			return BizBase.dbo.ExecSQL(strSQL);
		}

		public static int Add(SiteTemplateInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<SiteTemplateInfo>(entity);
			}
			return result;
		}

		public static bool Update(SiteTemplateInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<SiteTemplateInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_SiteTemplate WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_SiteTemplate WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static SiteTemplateInfo GetDataById(int intPrimaryKeyIDValue)
		{
			SiteTemplateInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<SiteTemplateInfo>(" SELECT AutoID,TemplateName,ShowPic,TemplatePath,HomePage,TemplateDesc,IsAudit,IsExists,Author,CopyRight,IsDefault,Sort,AutoTimeStamp FROM cms_SiteTemplate WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static SiteTemplateInfo GetTopData()
		{
			return SiteTemplate.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static SiteTemplateInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,TemplateName,ShowPic,TemplatePath,HomePage,TemplateDesc,IsAudit,IsExists,Author,CopyRight,IsDefault,Sort,AutoTimeStamp FROM cms_SiteTemplate ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<SiteTemplateInfo>(text);
		}

		public static IList<SiteTemplateInfo> GetAllList()
		{
			return SiteTemplate.GetList(0, string.Empty);
		}

		public static IList<SiteTemplateInfo> GetTopNList(int intTopCount)
		{
			return SiteTemplate.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<SiteTemplateInfo> GetTopNList(int intTopCount, string strSort)
		{
			return SiteTemplate.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<SiteTemplateInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return SiteTemplate.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<SiteTemplateInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,TemplateName,ShowPic,TemplatePath,HomePage,TemplateDesc,IsAudit,IsExists,Author,CopyRight,IsDefault,Sort,AutoTimeStamp from cms_SiteTemplate ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<SiteTemplateInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_SiteTemplate", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_SiteTemplate", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return SiteTemplate.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SiteTemplate.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SiteTemplate.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return SiteTemplate.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,TemplateName,ShowPic,TemplatePath,HomePage,TemplateDesc,IsAudit,IsExists,Author,CopyRight,IsDefault,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_SiteTemplate";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<SiteTemplateInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return SiteTemplate.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<SiteTemplateInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<SiteTemplateInfo> result = new List<SiteTemplateInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,TemplateName,ShowPic,TemplatePath,HomePage,TemplateDesc,IsAudit,IsExists,Author,CopyRight,IsDefault,Sort,AutoTimeStamp";
			pager.PagerTable = "cms_SiteTemplate";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<SiteTemplateInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_SiteTemplate SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_SiteTemplate SET Sort =",
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
