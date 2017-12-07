using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace SinGooCMS.BLL
{
	public class FileUpload : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_FileUpload ");
			}
		}

		public static int GetUserUpCount(string strUserName)
		{
			return BizBase.dbo.GetCount("sys_FileUpload", string.Concat(new string[]
			{
				" UserType='",
				UserType.User.ToString(),
				"' AND UserName='",
				strUserName,
				"'"
			})) ?? 0;
		}

		public static int GetUserUpTotal(string strUserName)
		{
			return BizBase.dbo.GetValue<int>(string.Concat(new string[]
			{
				" select ISNULL(SUM(FileSize),0) from sys_FileUpload where UserType='",
				UserType.User.ToString(),
				"' AND UserName='",
				strUserName,
				"' "
			}));
		}

		public static int GetManagerUpCount(string strManagerName)
		{
			return BizBase.dbo.GetCount("sys_FileUpload", string.Concat(new string[]
			{
				" UserType='",
				UserType.Manager.ToString(),
				"' AND UserName='",
				strManagerName,
				"'"
			})) ?? 0;
		}

		public static int GetManagerUpTotal(string strUserName)
		{
			return BizBase.dbo.GetValue<int>(string.Concat(new string[]
			{
				" select SUM(FileSize) from sys_FileUpload where UserType='",
				UserType.Manager.ToString(),
				"' AND UserName='",
				strUserName,
				"' "
			}));
		}

		public static bool DelBatAndFile(string strIds)
		{
			bool result;
			if (string.IsNullOrEmpty(strIds))
			{
				result = false;
			}
			else
			{
				IList<FileUploadInfo> list = FileUpload.GetList(0, " AutoID in (" + strIds + ") ", string.Empty);
				if (BizBase.dbo.DeleteTable("sys_FileUpload", " AutoID in (" + strIds + ") "))
				{
					foreach (FileUploadInfo current in list)
					{
						if (File.Exists(HttpContext.Current.Server.MapPath(current.VirtualPath)))
						{
							File.Delete(HttpContext.Current.Server.MapPath(current.VirtualPath));
						}
						if (File.Exists(HttpContext.Current.Server.MapPath(current.Thumb)))
						{
							File.Delete(HttpContext.Current.Server.MapPath(current.Thumb));
						}
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static bool MoveToFolder(string idsList, int folderID)
		{
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" update sys_FileUpload set FolderID=",
				folderID,
				" where AutoID in (",
				idsList,
				") "
			}));
		}

		public static FileUploadInfo GetByFileName(string strVirtualPath)
		{
			return BizBase.dbo.GetModel<FileUploadInfo>(" select top 1 * from sys_FileUpload where VirtualPath='" + strVirtualPath + "' ");
		}

		public static int Add(FileUploadInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<FileUploadInfo>(entity);
			}
			return result;
		}

		public static bool Update(FileUploadInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<FileUploadInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_FileUpload WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_FileUpload WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static FileUploadInfo GetDataById(int intPrimaryKeyIDValue)
		{
			FileUploadInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<FileUploadInfo>(" SELECT AutoID,FolderID,FileName,FileExt,FileSize,VirtualPath,Thumb,OriginalPath,UserType,UserName,DownloadCount,Lang,AutoTimeStamp FROM sys_FileUpload WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static FileUploadInfo GetTopData()
		{
			return FileUpload.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static FileUploadInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,FolderID,FileName,FileExt,FileSize,VirtualPath,Thumb,OriginalPath,UserType,UserName,DownloadCount,Lang,AutoTimeStamp FROM sys_FileUpload ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<FileUploadInfo>(text);
		}

		public static IList<FileUploadInfo> GetAllList()
		{
			return FileUpload.GetList(0, string.Empty);
		}

		public static IList<FileUploadInfo> GetTopNList(int intTopCount)
		{
			return FileUpload.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<FileUploadInfo> GetTopNList(int intTopCount, string strSort)
		{
			return FileUpload.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<FileUploadInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return FileUpload.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<FileUploadInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,FolderID,FileName,FileExt,FileSize,VirtualPath,Thumb,OriginalPath,UserType,UserName,DownloadCount,Lang,AutoTimeStamp from sys_FileUpload ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<FileUploadInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_FileUpload", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_FileUpload", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return FileUpload.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return FileUpload.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return FileUpload.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return FileUpload.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,FolderID,FileName,FileExt,FileSize,VirtualPath,Thumb,OriginalPath,UserType,UserName,DownloadCount,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_FileUpload";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<FileUploadInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return FileUpload.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<FileUploadInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<FileUploadInfo> result = new List<FileUploadInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,FolderID,FileName,FileExt,FileSize,VirtualPath,Thumb,OriginalPath,UserType,UserName,DownloadCount,Lang,AutoTimeStamp";
			pager.PagerTable = "sys_FileUpload";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<FileUploadInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_FileUpload SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_FileUpload SET Sort =",
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
