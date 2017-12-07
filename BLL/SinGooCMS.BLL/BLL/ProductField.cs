using SinGooCMS.DAL;
using SinGooCMS.DAL.Utils;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class ProductField : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_ProductField ");
			}
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_ProductField WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ProductFieldInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ProductFieldInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ProductFieldInfo>(" SELECT AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp FROM shop_ProductField WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ProductFieldInfo GetTopData()
		{
			return ProductField.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ProductFieldInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp FROM shop_ProductField ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ProductFieldInfo>(text);
		}

		public static IList<ProductFieldInfo> GetAllList()
		{
			return ProductField.GetList(0, string.Empty);
		}

		public static IList<ProductFieldInfo> GetTopNList(int intTopCount)
		{
			return ProductField.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ProductFieldInfo> GetTopNList(int intTopCount, string strSort)
		{
			return ProductField.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ProductFieldInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return ProductField.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ProductFieldInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp from shop_ProductField ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ProductFieldInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ProductField", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_ProductField", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return ProductField.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductField.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductField.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductField.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_ProductField";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ProductFieldInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return ProductField.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ProductFieldInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ProductFieldInfo> result = new List<ProductFieldInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp";
			pager.PagerTable = "shop_ProductField";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ProductFieldInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_ProductField SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_ProductField SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static FieldAddState Add(ProductFieldInfo field)
		{
			ProductModelInfo dataById = ProductModel.GetDataById(field.ModelID);
			FieldAddState result;
			if (dataById == null)
			{
				result = FieldAddState.ModelNotExists;
			}
			else
			{
				int value = BizBase.dbo.GetValue<int>(string.Concat(new object[]
				{
					"SELECT COUNT(*) FROM shop_ProductField WHERE ModelID=",
					field.ModelID,
					" AND FieldName='",
					field.FieldName,
					"'"
				}));
				if (value > 0)
				{
					result = FieldAddState.FieldNameExists;
				}
				else
				{
					if (BizBase.dbo.InsertModel<ProductFieldInfo>(field) > 0)
					{
						try
						{
							string text = field.DataType;
							if (string.Compare(text, "nvarchar", true) == 0)
							{
								object obj = text;
								text = string.Concat(new object[]
								{
									obj,
									"(",
									field.DataLength,
									")"
								});
							}
							TableManager.AddTableColumn(dataById.TableName, field.FieldName, text, true, field.DefaultValue);
						}
						catch
						{
							result = FieldAddState.CreateColumnError;
							return result;
						}
					}
					CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
					result = FieldAddState.Success;
				}
			}
			return result;
		}

		public static bool Update(ProductFieldInfo field)
		{
			ProductModelInfo dataById = ProductModel.GetDataById(field.ModelID);
			ProductFieldInfo dataById2 = ProductField.GetDataById(field.AutoID);
			bool result;
			if (BizBase.dbo.UpdateModel<ProductFieldInfo>(field))
			{
				if (!dataById2.IsSystem)
				{
					try
					{
						string text = field.DataType;
						if (string.Compare(text, "nvarchar", true) == 0)
						{
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								"(",
								field.DataLength,
								")"
							});
						}
						TableManager.AlterTableColumn(dataById.TableName, dataById2.FieldName, field.FieldName, text, true, field.DefaultValue);
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool Delete(int fieldID)
		{
			ProductFieldInfo dataById = ProductField.GetDataById(fieldID);
			ProductModelInfo dataById2 = ProductModel.GetDataById(dataById.ModelID);
			if (BizBase.dbo.DeleteModel<ProductFieldInfo>(dataById))
			{
				TableManager.DropTableColumn(dataById2.TableName, dataById.FieldName);
			}
			CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
			return true;
		}

		public static bool DeleteByModelID(int modelID)
		{
			bool result;
			if (modelID <= 0)
			{
				result = false;
			}
			else
			{
				IList<ProductFieldInfo> fieldListByModelID = ProductField.GetFieldListByModelID(modelID);
				bool flag = BizBase.dbo.ExecSQL(" DELETE FROM shop_ProductField WHERE ModelID=" + modelID);
				if (flag)
				{
					CacheUtils.Del("JsonLeeCMS_CacheForPROMODEL");
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static IList<ProductFieldInfo> GetCustomFieldListByModelID(int intModelID)
		{
			IList<ProductFieldInfo> list = BizBase.dbo.GetList<ProductFieldInfo>(" SELECT * FROM shop_ProductField WHERE ModelID=" + intModelID + " AND IsSystem=0 ");
			list.Add(ProductField.GetExistsFieldInfo());
			return list;
		}

		internal static ProductFieldInfo GetExistsFieldInfo()
		{
			return new ProductFieldInfo
			{
				FieldName = "ProID",
				Alias = "ID sản phẩm",
				FieldType = 0,
				EnableNull = false,
				DefaultValue = "0"
			};
		}

		public static IList<ProductFieldInfo> GetFieldListByModelID(int modelID)
		{
			return BizBase.dbo.GetList<ProductFieldInfo>(" SELECT * FROM shop_ProductField WHERE ModelID=" + modelID);
		}

		public static IList<ProductFieldInfo> GetFieldListByModelID(int modelID, bool isUsing)
		{
			int num = isUsing ? 1 : 0;
			return BizBase.dbo.GetList<ProductFieldInfo>(string.Concat(new object[]
			{
				" SELECT * FROM shop_ProductField WHERE ModelID=",
				modelID,
				" AND IsUsing=",
				num,
				" ORDER BY Sort ASC,AutoID desc "
			}));
		}

		public static IList<ProductFieldInfo> GetUsingFieldList(int modelID)
		{
			return ProductField.GetFieldListByModelID(modelID, true);
		}

		public static bool UpdateIsUsing(string strFieldIds, bool boolIsUsing)
		{
			int num = boolIsUsing ? 1 : 0;
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE shop_ProductField SET IsUsing=",
				num,
				" WHERE AutoID in (",
				strFieldIds,
				" ) "
			}));
		}
	}
}
