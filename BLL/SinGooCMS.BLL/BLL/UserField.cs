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
	public class UserField : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_UserField ");
			}
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_UserField WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static UserFieldInfo GetDataById(int intPrimaryKeyIDValue)
		{
			UserFieldInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserFieldInfo>(" SELECT AutoID,UserGroupID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,IsUsing,IsSystem,Sort,AutoTimeStamp FROM cms_UserField WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static UserFieldInfo GetTopData()
		{
			return UserField.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static UserFieldInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserGroupID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,IsUsing,IsSystem,Sort,AutoTimeStamp FROM cms_UserField ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<UserFieldInfo>(text);
		}

		public static IList<UserFieldInfo> GetAllList()
		{
			return UserField.GetList(0, string.Empty);
		}

		public static IList<UserFieldInfo> GetTopNList(int intTopCount)
		{
			return UserField.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<UserFieldInfo> GetTopNList(int intTopCount, string strSort)
		{
			return UserField.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<UserFieldInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return UserField.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<UserFieldInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserGroupID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,IsUsing,IsSystem,Sort,AutoTimeStamp from cms_UserField ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<UserFieldInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_UserField", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_UserField", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return UserField.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserField.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserField.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserField.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserGroupID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,IsUsing,IsSystem,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_UserField";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<UserFieldInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return UserField.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<UserFieldInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<UserFieldInfo> result = new List<UserFieldInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserGroupID,FieldName,FieldType,DataType,DataLength,Alias,Tip,FieldDesc,DefaultValue,Setting,EnableNull,IsUsing,IsSystem,Sort,AutoTimeStamp";
			pager.PagerTable = "cms_UserField";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<UserFieldInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_UserField SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_UserField SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static FieldAddState Add(UserFieldInfo field)
		{
			UserGroupInfo dataById = UserGroup.GetDataById(field.UserGroupID);
			FieldAddState result;
			if (dataById == null)
			{
				result = FieldAddState.ModelNotExists;
			}
			else
			{
				int value = BizBase.dbo.GetValue<int>(string.Concat(new object[]
				{
					"SELECT COUNT(*) FROM cms_UserField WHERE UserGroupID=",
					field.UserGroupID,
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
					if (BizBase.dbo.InsertModel<UserFieldInfo>(field) > 0)
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

		public static bool Update(UserFieldInfo field)
		{
			UserGroupInfo dataById = UserGroup.GetDataById(field.UserGroupID);
			UserFieldInfo dataById2 = UserField.GetDataById(field.AutoID);
			bool result;
			if (BizBase.dbo.UpdateModel<UserFieldInfo>(field))
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
				CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
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
			UserFieldInfo dataById = UserField.GetDataById(fieldID);
			UserGroupInfo dataById2 = UserGroup.GetDataById(dataById.UserGroupID);
			if (BizBase.dbo.DeleteModel<UserFieldInfo>(dataById))
			{
				TableManager.DropTableColumn(dataById2.TableName, dataById.FieldName);
			}
			CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
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
				IList<UserFieldInfo> fieldListByModelID = UserField.GetFieldListByModelID(modelID);
				bool flag = BizBase.dbo.ExecSQL(" DELETE FROM cms_UserField WHERE UserGroupID=" + modelID);
				if (flag)
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static IList<UserFieldInfo> GetCustomFieldListByModelID(int intGroupID)
		{
			IList<UserFieldInfo> list = BizBase.dbo.GetList<UserFieldInfo>(" SELECT * FROM cms_UserField WHERE UserGroupID=" + intGroupID + " AND IsSystem=0 ");
			list.Add(UserField.GetExistsFieldInfo());
			return list;
		}

		internal static UserFieldInfo GetExistsFieldInfo()
		{
			return new UserFieldInfo
			{
				FieldName = "UserID",
				Alias = "会员ID",
				FieldType = 0,
				EnableNull = false,
				DefaultValue = "0"
			};
		}

		public static IList<UserFieldInfo> GetFieldListByModelID(int modelID)
		{
			return BizBase.dbo.GetList<UserFieldInfo>(" SELECT * FROM cms_UserField WHERE UserGroupID=" + modelID);
		}

		public static IList<UserFieldInfo> GetFieldListByModelID(int modelID, bool isUsing)
		{
			int num = isUsing ? 1 : 0;
			return BizBase.dbo.GetList<UserFieldInfo>(string.Concat(new object[]
			{
				" SELECT * FROM cms_UserField WHERE UserGroupID=",
				modelID,
				" AND IsUsing=",
				num,
				" ORDER BY Sort ASC,AutoID desc "
			}));
		}

		public static IList<UserFieldInfo> GetUsingFieldList(int modelID)
		{
			return UserField.GetFieldListByModelID(modelID, true);
		}

		public static bool UpdateIsUsing(string strFieldIds, bool boolIsUsing)
		{
			int num = boolIsUsing ? 1 : 0;
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE cms_UserField SET IsUsing=",
				num,
				" WHERE AutoID in (",
				strFieldIds,
				" ) "
			}));
		}
	}
}
