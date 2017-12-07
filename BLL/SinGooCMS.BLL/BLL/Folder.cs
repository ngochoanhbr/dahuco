using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace SinGooCMS.BLL
{
	public class Folder : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Folder ");
			}
		}

		public static int AddExt(FolderInfo entity)
		{
			int num = Folder.Add(entity);
			if (num > 0)
			{
				if (entity.ParentID.Equals(0))
				{
					entity.RootID = num;
					entity.Depth = 1;
					entity.ChildCount = 0;
				}
				else
				{
					FolderInfo dataById = Folder.GetDataById(entity.ParentID);
					if (dataById != null)
					{
						entity.RootID = dataById.RootID;
						entity.Depth = dataById.Depth + 1;
						entity.ChildCount = 0;
						dataById.ChildCount++;
						Folder.Update(dataById);
					}
				}
				entity.AutoID = num;
				Folder.Update(entity);
			}
			return num;
		}

		public static bool UpdateExt()
		{
			return false;
		}

		public static bool DeleteExt(int intPrimaryKeyIDValue)
		{
			FolderInfo dataById = Folder.GetDataById(intPrimaryKeyIDValue);
			bool result;
			if (dataById != null && Folder.Delete(intPrimaryKeyIDValue) && dataById.ParentID > 0)
			{
				BizBase.dbo.UpdateTable("update sys_Folder set ChildCount=ChildCount-1 where AutoID=" + dataById.ParentID);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static List<FolderInfo> GetCateTreeList()
		{
			List<FolderInfo> obj = (List<FolderInfo>)Folder.GetAllList();
			List<FolderInfo> list = JObject.DeepClone<List<FolderInfo>>(obj);
			return Folder.GetRelationList(list, 0);
		}

		private static string GetTreeChar(int intDepth)
		{
			string text = string.Empty;
			for (int i = 0; i < intDepth; i++)
			{
				text += HttpContext.Current.Server.HtmlDecode("&nbsp;&nbsp;");
			}
			return text;
		}

		public static List<FolderInfo> GetRelationList(List<FolderInfo> list, int intParentID)
		{
			List<FolderInfo> list2 = list.FindAll((FolderInfo parameterA) => parameterA.ParentID == intParentID);
			List<FolderInfo> list3 = new List<FolderInfo>();
			int num = 0;
			foreach (FolderInfo current in list2)
			{
				if (num == list2.Count - 1)
				{
					current.FolderName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, true)) + StringUtils.GetChineseSpell(current.FolderName).Substring(0, 1) + current.FolderName;
				}
				else
				{
					current.FolderName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, false)) + StringUtils.GetChineseSpell(current.FolderName).Substring(0, 1) + current.FolderName;
				}
				list3.Add(current);
				if (current.ChildCount > 0)
				{
					list3.AddRange(Folder.GetRelationList(list, current.AutoID));
				}
				num++;
			}
			return list3;
		}

		public static FolderInfo GetByFileName(string strVirtualPath)
		{
			return BizBase.dbo.GetModel<FolderInfo>(" select top 1 * from sys_Folder where AutoID=(select top 1 FolderID from sys_FileUpload where VirtualPath='" + strVirtualPath + "') ");
		}

		public static int CreateUserFolder()
		{
			FolderInfo folderInfo = BizBase.dbo.GetModel<FolderInfo>(" select top 1 * from sys_Folder where FolderName='会员文件' and ParentID=0 ");
			int result;
			if (folderInfo == null)
			{
				folderInfo = new FolderInfo
				{
					FolderName = "会员文件",
					ParentID = 0,
					Depth = 1,
					ChildCount = 0,
					Remark = "会员文件",
					Lang = JObject.cultureLang,
					AutoTimeStamp = DateTime.Now
				};
				result = Folder.AddExt(folderInfo);
			}
			else
			{
				result = folderInfo.AutoID;
			}
			return result;
		}

		public static bool HasFiles(int intFolderID)
		{
			return BizBase.dbo.GetValue<int>(" select COUNT(*) from sys_FileUpload where FolderID=" + intFolderID.ToString()) > 0;
		}

		public static bool HasChildFolder(int intFolderID)
		{
			return BizBase.dbo.GetValue<int>(" select COUNT(*) from sys_Folder where ParentID=" + intFolderID.ToString()) > 0;
		}

		public static int Add(FolderInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<FolderInfo>(entity);
			}
			return result;
		}

		public static bool Update(FolderInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<FolderInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Folder WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Folder WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static FolderInfo GetDataById(int intPrimaryKeyIDValue)
		{
			FolderInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<FolderInfo>(" SELECT AutoID,FolderName,ParentID,RootID,Depth,ChildCount,ChildList,Remark,Sort,Lang,AutoTimeStamp FROM sys_Folder WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static FolderInfo GetTopData()
		{
			return Folder.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static FolderInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,FolderName,ParentID,RootID,Depth,ChildCount,ChildList,Remark,Sort,Lang,AutoTimeStamp FROM sys_Folder ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<FolderInfo>(text);
		}

		public static IList<FolderInfo> GetAllList()
		{
			return Folder.GetList(0, string.Empty);
		}

		public static IList<FolderInfo> GetTopNList(int intTopCount)
		{
			return Folder.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<FolderInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Folder.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<FolderInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Folder.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<FolderInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,FolderName,ParentID,RootID,Depth,ChildCount,ChildList,Remark,Sort,Lang,AutoTimeStamp from sys_Folder ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<FolderInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Folder", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Folder", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Folder.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Folder.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Folder.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Folder.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,FolderName,ParentID,RootID,Depth,ChildCount,ChildList,Remark,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Folder";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<FolderInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Folder.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<FolderInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<FolderInfo> result = new List<FolderInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,FolderName,ParentID,RootID,Depth,ChildCount,ChildList,Remark,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "sys_Folder";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<FolderInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Folder SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Folder SET Sort =",
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
