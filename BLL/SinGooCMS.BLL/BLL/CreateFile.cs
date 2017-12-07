using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class CreateFile : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_CreateFile ");
			}
		}

		public static int Add(CreateFileInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<CreateFileInfo>(entity);
			}
			return result;
		}

		public static bool Update(CreateFileInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<CreateFileInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_CreateFile WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_CreateFile WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static CreateFileInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CreateFileInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CreateFileInfo>(" SELECT AutoID,Title,TemplateFile,FilePath,FileName,Dicts,CountSQL,RecordCount,PageSize,IsPage,IsCreated,AutoTimeStamp FROM cms_CreateFile WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CreateFileInfo GetTopData()
		{
			return CreateFile.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CreateFileInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,Title,TemplateFile,FilePath,FileName,Dicts,CountSQL,RecordCount,PageSize,IsPage,IsCreated,AutoTimeStamp FROM cms_CreateFile ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CreateFileInfo>(text);
		}

		public static IList<CreateFileInfo> GetAllList()
		{
			return CreateFile.GetList(0, string.Empty);
		}

		public static IList<CreateFileInfo> GetTopNList(int intTopCount)
		{
			return CreateFile.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CreateFileInfo> GetTopNList(int intTopCount, string strSort)
		{
			return CreateFile.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CreateFileInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return CreateFile.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CreateFileInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,Title,TemplateFile,FilePath,FileName,Dicts,CountSQL,RecordCount,PageSize,IsPage,IsCreated,AutoTimeStamp from cms_CreateFile ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CreateFileInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_CreateFile", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_CreateFile", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return CreateFile.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return CreateFile.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return CreateFile.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return CreateFile.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,Title,TemplateFile,FilePath,FileName,Dicts,CountSQL,RecordCount,PageSize,IsPage,IsCreated,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_CreateFile";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CreateFileInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return CreateFile.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CreateFileInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CreateFileInfo> result = new List<CreateFileInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,Title,TemplateFile,FilePath,FileName,Dicts,CountSQL,RecordCount,PageSize,IsPage,IsCreated,AutoTimeStamp";
			pager.PagerTable = "cms_CreateFile";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CreateFileInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_CreateFile SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_CreateFile SET Sort =",
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
