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
	public class ProductModel : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_ProductModel ");
			}
		}

		public static ModelAddState Add(ProductModelInfo model)
		{
			int num = ProductModel.ExistsModel(model.ModelName, model.TableName);
			ModelAddState result;
			if (num > 0)
			{
				result = (ModelAddState)num;
			}
			else
			{
				string tableName = "dbo." + model.TableName;
				int num2 = BizBase.dbo.InsertModel<ProductModelInfo>(model);
				if (num2 > 0)
				{
					try
					{
						TableManager.CreateTable(tableName, "AutoID");
						TableManager.AddTableColumn(tableName, "ProID", "INT", false, "0");
						ProductModel.AddDefaultField(num2);
						CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
						result = ModelAddState.Success;
						return result;
					}
					catch
					{
						result = ModelAddState.CreateTableError;
						return result;
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
				result = ModelAddState.Error;
			}
			return result;
		}

		private static void AddDefaultField(int intGroupID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ModelID", intGroupID),
				new SqlParameter("@ModelType", "Product")
			};
			BizBase.dbo.ExecProc("p_System_AddDefaultFieldWithModel", arrParam);
		}

		public static bool Update(ProductModelInfo model)
		{
			CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
			return BizBase.dbo.UpdateModel<ProductModelInfo>(model);
		}

		public static ModelDeleteStatus Delete(int modelID)
		{
			ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(modelID);
			ModelDeleteStatus result;
			if (cacheModelById == null)
			{
				result = ModelDeleteStatus.ModelNotExists;
			}
			else if (ProductModel.IsModelRefingByPro(modelID))
			{
				result = ModelDeleteStatus.UserRef;
			}
			else if (BizBase.dbo.DeleteModel<ProductModelInfo>(cacheModelById))
			{
				BizBase.dbo.ExecSQL(" DROP TABLE " + cacheModelById.TableName);
				CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
				result = ModelDeleteStatus.Success;
			}
			else
			{
				CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
				result = ModelDeleteStatus.Error;
			}
			return result;
		}

		public static IList<ProductModelInfo> GetCacheModelList()
		{
			IList<ProductModelInfo> list = (IList<ProductModelInfo>)CacheUtils.Get("JsonLeeCMS_CacheForPROMODEL");
			if (list == null)
			{
				list = ProductModel.GetAllList();
				CacheUtils.Insert("JsonLeeCMS_CacheForPROMODEL", list);
			}
			return list;
		}

		public static IList<ProductModelInfo> GetCacheUsingModelList()
		{
			IList<ProductModelInfo> cacheModelList = ProductModel.GetCacheModelList();
			IList<ProductModelInfo> list = new List<ProductModelInfo>();
			if (cacheModelList != null && cacheModelList.Count > 0)
			{
				foreach (ProductModelInfo current in cacheModelList)
				{
					if (current.IsUsing)
					{
						list.Add(current);
					}
				}
			}
			return list;
		}

		public static ProductModelInfo GetCacheModelById(int intModelID)
		{
			ProductModelInfo result = null;
			IList<ProductModelInfo> cacheModelList = ProductModel.GetCacheModelList();
			if (cacheModelList != null && cacheModelList.Count > 0)
			{
				foreach (ProductModelInfo current in cacheModelList)
				{
					if (current.AutoID == intModelID)
					{
						result = current;
						break;
					}
				}
			}
			return result;
		}

		public static bool IsModelRefingByPro(int intModelID)
		{
			return BizBase.dbo.GetValue<int>("SELECT COUNT(*) FROM shop_Product WHERE ModelID=" + intModelID.ToString()) > 0;
		}

		public static int ExistsModel(string strModelName, string strTableName)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@modelname", strModelName),
				new SqlParameter("@tablename", strTableName)
			};
			object obj = BizBase.dbo.ExecProcReValue("p_System_ExistsProductModel", arrParam);
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

		public static ProductModelInfo GetModelByName(string strModelName)
		{
			List<ProductModelInfo> list = (List<ProductModelInfo>)ProductModel.GetCacheModelList();
			ProductModelInfo result;
			if (list != null && list.Count > 0)
			{
				result = (from p in list
				where p.ModelName == strModelName
				select p).FirstOrDefault<ProductModelInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_ProductModel WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ProductModelInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ProductModelInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ProductModelInfo>(" SELECT AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp FROM shop_ProductModel WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ProductModelInfo GetTopData()
		{
			return ProductModel.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ProductModelInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp FROM shop_ProductModel ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ProductModelInfo>(text);
		}

		public static IList<ProductModelInfo> GetAllList()
		{
			return ProductModel.GetList(0, string.Empty);
		}

		public static IList<ProductModelInfo> GetTopNList(int intTopCount)
		{
			return ProductModel.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ProductModelInfo> GetTopNList(int intTopCount, string strSort)
		{
			return ProductModel.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ProductModelInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return ProductModel.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ProductModelInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp from shop_ProductModel ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ProductModelInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ProductModel", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ProductModel", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return ProductModel.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductModel.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductModel.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductModel.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_ProductModel";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ProductModelInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductModel.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ProductModelInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ProductModelInfo> result = new List<ProductModelInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ModelName,TableName,ModelDesc,IsUsing,Sort,Creator,AutoTimeStamp";
			pager.PagerTable = "shop_ProductModel";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ProductModelInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_ProductModel SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_ProductModel SET Sort =",
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
