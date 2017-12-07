using SinGooCMS.DAL;
using SinGooCMS.DAL.Utils;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class ContModel : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_ContModel ");
			}
		}

		public static ModelAddState Add(ContModelInfo model)
		{
			int num = ContModel.ExistsModel(model.ModelName, model.TableName);
			ModelAddState result;
			if (num > 0)
			{
				result = (ModelAddState)num;
			}
			else
			{
				string tableName = "dbo." + model.TableName;
				int num2 = BizBase.dbo.InsertModel<ContModelInfo>(model);
				if (num2 > 0)
				{
					try
					{
						TableManager.CreateTable(tableName, "AutoID");
						TableManager.AddTableColumn(tableName, "ContID", "INT", false, "0");
						ContModel.AddDefaultField(num2);
						CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
						result = ModelAddState.Success;
						return result;
					}
					catch
					{
						result = ModelAddState.CreateTableError;
						return result;
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
				result = ModelAddState.Error;
			}
			return result;
		}

		public static bool Update(ContModelInfo model)
		{
			CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
			return BizBase.dbo.UpdateModel<ContModelInfo>(model);
		}

		public static ModelDeleteStatus Delete(int modelID)
		{
			ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(modelID);
			ModelDeleteStatus result;
			if (cacheModelByID == null)
			{
				result = ModelDeleteStatus.ModelNotExists;
			}
			else
			{
				int num = ContModel.IsModelRefing(modelID);
				if (num > 0)
				{
					result = (ModelDeleteStatus)num;
				}
				else if (BizBase.dbo.DeleteModel<ContModelInfo>(cacheModelByID))
				{
					ContField.DeleteByModelID(modelID);
					BizBase.dbo.ExecSQL(" DROP TABLE " + cacheModelByID.TableName);
					CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
					result = ModelDeleteStatus.Success;
				}
				else
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
					result = ModelDeleteStatus.Error;
				}
			}
			return result;
		}

		private static void AddDefaultField(int intModelID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ModelID", intModelID),
				new SqlParameter("@ModelType", "Content")
			};
			BizBase.dbo.ExecProc("p_System_AddDefaultFieldWithModel", arrParam);
		}

		public static IList<ContModelInfo> GetCacheModelList()
		{
			IList<ContModelInfo> list = (IList<ContModelInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetContModel");
			if (list == null)
			{
				list = BizBase.dbo.GetList<ContModelInfo>(" SELECT * FROM cms_ContModel Order by Sort asc, AutoID desc ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetContModel", list);
			}
			return list;
		}

		public static IList<ContModelInfo> GetCacheUsingModelList()
		{
			IList<ContModelInfo> cacheModelList = ContModel.GetCacheModelList();
			IList<ContModelInfo> list = new List<ContModelInfo>();
			if (cacheModelList != null && cacheModelList.Count > 0)
			{
				foreach (ContModelInfo current in cacheModelList)
				{
					if (current.IsUsing)
					{
						list.Add(current);
					}
				}
			}
			return list;
		}

		public static ContModelInfo GetCacheModelByID(int modelID)
		{
			ContModelInfo result = null;
			IList<ContModelInfo> cacheModelList = ContModel.GetCacheModelList();
			if (cacheModelList != null && cacheModelList.Count > 0)
			{
				foreach (ContModelInfo current in cacheModelList)
				{
					if (current.AutoID == modelID)
					{
						result = current;
						break;
					}
				}
			}
			return result;
		}

		public static int IsModelRefing(int intModelID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@modelid", intModelID)
			};
			object obj = BizBase.dbo.ExecProcReValue("p_System_ContModelIsRefing", arrParam);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		public static int ExistsModel(string strModelName, string strTableName)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@modelname", strModelName),
				new SqlParameter("@tablename", strTableName)
			};
			object obj = BizBase.dbo.ExecProcReValue("p_System_ExistsContModel", arrParam);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		public static bool UpdateStatus(string strIDs, bool IsUsing)
		{
			return BizBase.dbo.UpdateTable(string.Concat(new object[]
			{
				" UPDATE cms_ContModel SET IsUsing=",
				IsUsing ? 1 : 0,
				" WHERE AutoID IN (",
				strIDs,
				") "
			}));
		}

		public static ContModelInfo GetModelByName(string strModelName)
		{
			List<ContModelInfo> list = (List<ContModelInfo>)ContModel.GetCacheModelList();
			ContModelInfo result;
			if (list != null && list.Count > 0)
			{
				result = (from p in list
				where p.ModelName == strModelName
				select p).FirstOrDefault<ContModelInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_ContModel WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ContModelInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ContModelInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ContModelInfo>(" SELECT AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp FROM cms_ContModel WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ContModelInfo GetTopData()
		{
			return ContModel.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ContModelInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp FROM cms_ContModel ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ContModelInfo>(text);
		}

		public static IList<ContModelInfo> GetAllList()
		{
			return ContModel.GetList(0, string.Empty);
		}

		public static IList<ContModelInfo> GetTopNList(int intTopCount)
		{
			return ContModel.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ContModelInfo> GetTopNList(int intTopCount, string strSort)
		{
			return ContModel.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ContModelInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return ContModel.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ContModelInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp from cms_ContModel ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ContModelInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_ContModel", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_ContModel", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return ContModel.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ContModel.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ContModel.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ContModel.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ModelName,TableName,ModelDesc,IsUsing,Creator,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_ContModel";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ContModelInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return ContModel.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ContModelInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ContModelInfo> result = new List<ContModelInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp";
			pager.PagerTable = "cms_ContModel";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ContModelInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_ContModel SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_ContModel SET Sort =",
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
