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
	public class UserGroup : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_UserGroup ");
			}
		}

		public static ModelAddState Add(UserGroupInfo model)
		{
			int num = UserGroup.ExistsUserGroup(model.GroupName, model.TableName);
			ModelAddState result;
			if (num > 0)
			{
				result = (ModelAddState)num;
			}
			else
			{
				string tableName = "dbo." + model.TableName;
				int num2 = BizBase.dbo.InsertModel<UserGroupInfo>(model);
				if (num2 > 0)
				{
					try
					{
						TableManager.CreateTable(tableName, "AutoID");
						TableManager.AddTableColumn(tableName, "UserID", "INT", false, "0");
						UserGroup.AddDefaultField(num2);
						CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
						result = ModelAddState.Success;
						return result;
					}
					catch
					{
						result = ModelAddState.CreateTableError;
						return result;
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
				result = ModelAddState.Error;
			}
			return result;
		}

		private static void AddDefaultField(int intGroupID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ModelID", intGroupID),
				new SqlParameter("@ModelType", "User")
			};
			BizBase.dbo.ExecProc("p_System_AddDefaultFieldWithModel", arrParam);
		}

		public static bool Update(UserGroupInfo model)
		{
			CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
			return BizBase.dbo.UpdateModel<UserGroupInfo>(model);
		}

		public static ModelDeleteStatus Delete(int modelID)
		{
			UserGroupInfo cacheUserGroupById = UserGroup.GetCacheUserGroupById(modelID);
			ModelDeleteStatus result;
			if (cacheUserGroupById == null)
			{
				result = ModelDeleteStatus.ModelNotExists;
			}
			else if (UserGroup.IsGroupRefingByUser(modelID))
			{
				result = ModelDeleteStatus.UserRef;
			}
			else if (BizBase.dbo.DeleteModel<UserGroupInfo>(cacheUserGroupById))
			{
				BizBase.dbo.ExecSQL(" DROP TABLE " + cacheUserGroupById.TableName);
				CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
				result = ModelDeleteStatus.Success;
			}
			else
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetUserGroup");
				result = ModelDeleteStatus.Error;
			}
			return result;
		}

		public static IList<UserGroupInfo> GetCacheUserGroupList()
		{
			IList<UserGroupInfo> list = (IList<UserGroupInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetUserGroup");
			if (list == null)
			{
				list = UserGroup.GetAllList();
				CacheUtils.Insert("JsonLeeCMS_CacheForGetUserGroup", list);
			}
			return list;
		}

		public static UserGroupInfo GetCacheUserGroupById(int intUserGroupID)
		{
			UserGroupInfo result = null;
			IList<UserGroupInfo> cacheUserGroupList = UserGroup.GetCacheUserGroupList();
			if (cacheUserGroupList != null && cacheUserGroupList.Count > 0)
			{
				foreach (UserGroupInfo current in cacheUserGroupList)
				{
					if (current.AutoID == intUserGroupID)
					{
						result = current;
						break;
					}
				}
			}
			return result;
		}

		public static bool IsGroupRefingByUser(int intGroupID)
		{
			return BizBase.dbo.GetValue<int>("SELECT count(*) FROM cms_User WHERE GroupID=" + intGroupID.ToString()) > 0;
		}

		public static int ExistsUserGroup(string strGroupName, string strTableName)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@groupname", strGroupName),
				new SqlParameter("@tablename", strTableName)
			};
			object obj = BizBase.dbo.ExecProcReValue("p_System_ExistsUserGroup", arrParam);
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

		public static UserGroupInfo GetGroupByName(string strModelName)
		{
			List<UserGroupInfo> list = (List<UserGroupInfo>)UserGroup.GetCacheUserGroupList();
			UserGroupInfo result;
			if (list != null && list.Count > 0)
			{
				result = (from p in list
				where p.GroupName == strModelName
				select p).FirstOrDefault<UserGroupInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_UserGroup WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static UserGroupInfo GetDataById(int intPrimaryKeyIDValue)
		{
			UserGroupInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<UserGroupInfo>(" SELECT AutoID,GroupName,TableName,GroupDesc,Sort,Creator,AutoTimeStamp FROM cms_UserGroup WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static UserGroupInfo GetTopData()
		{
			return UserGroup.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static UserGroupInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,GroupName,TableName,GroupDesc,Sort,Creator,AutoTimeStamp FROM cms_UserGroup ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<UserGroupInfo>(text);
		}

		public static IList<UserGroupInfo> GetAllList()
		{
			return UserGroup.GetList(0, string.Empty);
		}

		public static IList<UserGroupInfo> GetTopNList(int intTopCount)
		{
			return UserGroup.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<UserGroupInfo> GetTopNList(int intTopCount, string strSort)
		{
			return UserGroup.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<UserGroupInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return UserGroup.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<UserGroupInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,GroupName,TableName,GroupDesc,Sort,Creator,AutoTimeStamp from cms_UserGroup ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<UserGroupInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_UserGroup", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_UserGroup", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return UserGroup.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserGroup.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserGroup.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return UserGroup.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,GroupName,TableName,GroupDesc,Sort,Creator,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_UserGroup";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<UserGroupInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return UserGroup.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<UserGroupInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<UserGroupInfo> result = new List<UserGroupInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,GroupName,TableName,GroupDesc,Sort,Creator,AutoTimeStamp";
			pager.PagerTable = "cms_UserGroup";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<UserGroupInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_UserGroup SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_UserGroup SET Sort =",
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
