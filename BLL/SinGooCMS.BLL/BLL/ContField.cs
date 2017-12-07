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
	public class ContField : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_ContField ");
			}
		}

		public static FieldAddState Add(ContFieldInfo field)
		{
			ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(field.ModelID);
			FieldAddState result;
			if (cacheModelByID == null)
			{
				result = FieldAddState.ModelNotExists;
			}
			else
			{
				int value = BizBase.dbo.GetValue<int>(string.Concat(new object[]
				{
					"SELECT COUNT(*) FROM cms_ContField WHERE ModelID=",
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
					if (BizBase.dbo.InsertModel<ContFieldInfo>(field) > 0)
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
							TableManager.AddTableColumn(cacheModelByID.TableName, field.FieldName, text, true, field.DefaultValue);
						}
						catch
						{
							result = FieldAddState.CreateColumnError;
							return result;
						}
					}
					CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
					result = FieldAddState.Success;
				}
			}
			return result;
		}

		public static bool Update(ContFieldInfo field)
		{
			ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(field.ModelID);
			ContFieldInfo dataById = ContField.GetDataById(field.AutoID);
			bool result;
			if (BizBase.dbo.UpdateModel<ContFieldInfo>(field))
			{
				if (!dataById.IsSystem)
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
						TableManager.AlterTableColumn(cacheModelByID.TableName, dataById.FieldName, field.FieldName, text, true, field.DefaultValue);
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
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
			ContFieldInfo dataById = ContField.GetDataById(fieldID);
			ContModelInfo dataById2 = ContModel.GetDataById(dataById.ModelID);
			if (BizBase.dbo.DeleteModel<ContFieldInfo>(dataById))
			{
				TableManager.DropTableColumn(dataById2.TableName, dataById.FieldName);
			}
			CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
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
				IList<ContFieldInfo> fieldListByModelID = ContField.GetFieldListByModelID(modelID);
				bool flag = BizBase.dbo.ExecSQL(" DELETE FROM cms_ContField WHERE ModelID=" + modelID);
				if (flag)
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetContModel");
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static IList<ContFieldInfo> GetCustomFieldListByModelID(int modelID)
		{
			IList<ContFieldInfo> list = BizBase.dbo.GetList<ContFieldInfo>(" SELECT * FROM cms_ContField WHERE ModelID=" + modelID + " AND IsSystem=0 ");
			list.Add(ContField.GetExistsFieldInfo());
			return list;
		}

		internal static ContFieldInfo GetExistsFieldInfo()
		{
			return new ContFieldInfo
			{
				FieldName = "ContID",
				Alias = "内容ID",
				FieldType = 0,
				EnableNull = false,
				DefaultValue = "0"
			};
		}

		public static IList<ContFieldInfo> GetFieldListByModelID(int modelID)
		{
			return BizBase.dbo.GetList<ContFieldInfo>(" SELECT * FROM cms_ContField WHERE ModelID=" + modelID);
		}

		public static IList<ContFieldInfo> GetFieldListByModelID(int modelID, bool isUsing)
		{
			int num = isUsing ? 1 : 0;
			return BizBase.dbo.GetList<ContFieldInfo>(string.Concat(new object[]
			{
				" SELECT * FROM cms_ContField WHERE ModelID=",
				modelID,
				" AND IsUsing=",
				num,
				" ORDER BY Sort ASC,AutoID desc "
			}));
		}

		public static IList<ContFieldInfo> GetUsingFieldList(int modelID)
		{
			return ContField.GetFieldListByModelID(modelID, true);
		}

		public static bool UpdateIsUsing(string strFieldIds, bool boolIsUsing)
		{
			int num = boolIsUsing ? 1 : 0;
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE cms_ContField SET IsUsing=",
				num,
				" WHERE AutoID in (",
				strFieldIds,
				" ) "
			}));
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_ContField WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ContFieldInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ContFieldInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ContFieldInfo>(" SELECT AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp FROM cms_ContField WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ContFieldInfo GetTopData()
		{
			return ContField.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ContFieldInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp FROM cms_ContField ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ContFieldInfo>(text);
		}

		public static IList<ContFieldInfo> GetAllList()
		{
			return ContField.GetList(0, string.Empty);
		}

		public static IList<ContFieldInfo> GetTopNList(int intTopCount)
		{
			return ContField.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ContFieldInfo> GetTopNList(int intTopCount, string strSort)
		{
			return ContField.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ContFieldInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return ContField.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ContFieldInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp from cms_ContField ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ContFieldInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_ContField", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_ContField", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return ContField.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ContField.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ContField.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return ContField.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
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
			pager.PagerTable = "cms_ContField";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ContFieldInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return ContField.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ContFieldInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ContFieldInfo> result = new List<ContFieldInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ModelID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,EnableSearch,IsUsing,IsSystem,Sort,AutoTimeStamp";
			pager.PagerTable = "cms_ContField";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ContFieldInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_ContField SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_ContField SET Sort =",
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
