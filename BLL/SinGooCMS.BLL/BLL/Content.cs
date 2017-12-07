using SinGooCMS.DAL;
using SinGooCMS.DAL.Utils;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Content : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_Content ");
			}
		}

		public static int Add(ContentInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<ContentInfo>(entity);
			}
			return result;
		}

		public static bool Update(ContentInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<ContentInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_Content WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_Content WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ContentInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ContentInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ContentInfo>(" SELECT AutoID,NodeID,NodeName,ModelID,TableName,Title,SubTitle,Summary,Author,Editor,Source,SourceUrl,Content,Attachment,TagKey,ContentImage,RelateContent,SeoKey,SeoDescription,Hit,TemplateFile,CustomRecommend,IsTop,IsRecommend,IsNew,Lang,Inputer,Sort,Status,AutoTimeStamp FROM cms_Content WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ContentInfo GetTopData()
		{
			return Content.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ContentInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,NodeID,NodeName,ModelID,TableName,Title,SubTitle,Summary,Author,Editor,Source,SourceUrl,Content,Attachment,TagKey,ContentImage,RelateContent,SeoKey,SeoDescription,Hit,TemplateFile,CustomRecommend,IsTop,IsRecommend,IsNew,Lang,Inputer,Sort,Status,AutoTimeStamp FROM cms_Content ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ContentInfo>(text);
		}

		public static IList<ContentInfo> GetAllList()
		{
			return Content.GetList(0, string.Empty);
		}

		public static IList<ContentInfo> GetTopNList(int intTopCount)
		{
			return Content.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ContentInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Content.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ContentInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Content.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ContentInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,NodeID,NodeName,ModelID,TableName,Title,SubTitle,Summary,Author,Editor,Source,SourceUrl,Content,Attachment,TagKey,ContentImage,RelateContent,SeoKey,SeoDescription,Hit,TemplateFile,CustomRecommend,IsTop,IsRecommend,IsNew,Lang,Inputer,Sort,Status,AutoTimeStamp from cms_Content ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ContentInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Content", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Content", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Content.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Content.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Content.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Content.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,NodeID,NodeName,ModelID,TableName,Title,SubTitle,Summary,Author,Editor,Source,SourceUrl,Content,Attachment,TagKey,ContentImage,RelateContent,SeoKey,SeoDescription,Hit,TemplateFile,CustomRecommend,IsTop,IsRecommend,IsNew,Lang,Inputer,Sort,Status,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_Content";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ContentInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Content.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ContentInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ContentInfo> result = new List<ContentInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,NodeID,NodeName,ModelID,TableName,Title,SubTitle,Summary,Author,Editor,Source,SourceUrl,Content,Attachment,TagKey,ContentImage,RelateContent,SeoKey,SeoDescription,Hit,TemplateFile,CustomRecommend,IsTop,IsRecommend,IsNew,Lang,Inputer,Sort,Status,AutoTimeStamp";
			pager.PagerTable = "cms_Content";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ContentInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_Content SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_Content SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static int Add(ContentInfo content, Dictionary<string, ContFieldInfo> dicField)
		{
			NodeInfo cacheNodeById = Node.GetCacheNodeById(content.NodeID);
			int result;
			if (cacheNodeById == null)
			{
				result = 0;
			}
			else
			{
				ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(cacheNodeById.ModelID);
				if (cacheModelByID == null)
				{
					result = 0;
				}
				else
				{
					content.NodeName = cacheNodeById.NodeName;
					content.ModelID = cacheModelByID.AutoID;
					content.TableName = cacheModelByID.TableName;
					content.Title = Content.GetStringFromDic("Title", dicField);
					if (string.IsNullOrEmpty(content.Title))
					{
						content.Title = "默认的标题";
					}
					content.SubTitle = Content.GetStringFromDic("SubTitle", dicField);
					content.Summary = Content.GetStringFromDic("Summary", dicField);
					content.Author = Content.GetStringFromDic("Author", dicField);
					content.Editor = Content.GetStringFromDic("Editor", dicField);
					content.Source = Content.GetStringFromDic("Source", dicField);
					content.SourceUrl = Content.GetStringFromDic("SourceUrl", dicField);
					content.Content = Content.GetStringFromDic("Content", dicField);
					content.TagKey = Content.GetStringFromDic("TagKey", dicField);
					content.ContentImage = Content.GetStringFromDic("ContentImage", dicField);
					content.Attachment = Content.GetStringFromDic("Attachment", dicField);
					content.RelateContent = Content.GetStringFromDic("RelateContent", dicField);
					content.SeoKey = Content.GetStringFromDic("SeoKey", dicField);
					content.SeoDescription = Content.GetStringFromDic("SeoDescription", dicField);
					content.IsTop = Content.GetBoolFromDic("IsTop", dicField, false);
					content.IsRecommend = Content.GetBoolFromDic("IsRecommend", dicField, false);
					content.IsNew = Content.GetBoolFromDic("IsNew", dicField, false);
					content.TemplateFile = Content.GetStringFromDic("TemplateFile", dicField);
					content.Hit = Content.GetIntFromDic("Hit", dicField, 0);
					content.Sort = Content.GetIntFromDic("Sort", dicField, 999);
					content.Inputer = Account.GetLoginAccount().AccountName;
					content.Lang = JObject.cultureLang;
					if (dicField.ContainsKey("AutoTimeStamp"))
					{
						content.AutoTimeStamp = Content.GetDateTimeFromDic("AutoTimeStamp", dicField, DateTime.Now);
					}
					else
					{
						content.AutoTimeStamp = DateTime.Now;
					}
					int num = Content.Add(content);
					IList<ContFieldInfo> customFieldListByModelID = ContField.GetCustomFieldListByModelID(cacheModelByID.AutoID);
					foreach (ContFieldInfo current in customFieldListByModelID)
					{
						if (current.FieldName == "ContID")
						{
							current.Value = num.ToString();
						}
						else
						{
							current.Value = Content.GetStringFromDic(current.FieldName, dicField);
						}
					}
					Content.AddCustomContent(cacheModelByID, customFieldListByModelID);
					result = num;
				}
			}
			return result;
		}

		public static bool AddCustomContent(ContModelInfo model, IList<ContFieldInfo> fieldList)
		{
			string commandText = Content.GenerateSqlOfInsert(model.TableName, fieldList);
			SqlParameter[] parameters = Content.PrepareSqlParameters(fieldList);
			return DBHelper.ExecuteNonQuery(commandText, parameters) > 0;
		}

		public static string GenerateSqlOfInsert(string tableName, IList<ContFieldInfo> fieldList)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT INTO [");
			stringBuilder.Append(tableName);
			stringBuilder.Append("] (");
			foreach (ContFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(" ) Values ( ");
			num = 0;
			foreach (ContFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append("@");
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		public static void AddGallery(string[] arrGalleryID, int intContID)
		{
			StringBuilder stringBuilder = new StringBuilder(" INSERT INTO cms_Gallery ( ContID, ImageID ) ");
			if (arrGalleryID.Length > 0)
			{
				for (int i = 0; i < arrGalleryID.Length; i++)
				{
					if (i == arrGalleryID.Length - 1)
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							" SELECT ",
							intContID,
							",",
							arrGalleryID[i],
							" "
						}));
					}
					else
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							" SELECT ",
							intContID,
							",",
							arrGalleryID[i],
							" UNION all "
						}));
					}
				}
				BizBase.dbo.ExecSQL(stringBuilder.ToString());
			}
		}

		public static bool UpdateContent(ContentInfo content, Dictionary<string, ContFieldInfo> dicField)
		{
			bool result;
			if (content == null)
			{
				result = false;
			}
			else
			{
				ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(content.ModelID);
				if (cacheModelByID == null)
				{
					result = false;
				}
				else
				{
					content.Title = Content.GetStringFromDic("Title", dicField);
					if (string.IsNullOrEmpty(content.Title))
					{
						content.Title = "默认的标题";
					}
					content.SubTitle = Content.GetStringFromDic("SubTitle", dicField);
					content.Summary = Content.GetStringFromDic("Summary", dicField);
					content.Author = Content.GetStringFromDic("Author", dicField);
					content.Editor = Content.GetStringFromDic("Editor", dicField);
					content.Source = Content.GetStringFromDic("Source", dicField);
					content.SourceUrl = Content.GetStringFromDic("SourceUrl", dicField);
					content.Content = Content.GetStringFromDic("Content", dicField);
					content.TagKey = Content.GetStringFromDic("TagKey", dicField);
					content.ContentImage = Content.GetStringFromDic("ContentImage", dicField);
					content.Attachment = Content.GetStringFromDic("Attachment", dicField);
					content.RelateContent = Content.GetStringFromDic("RelateContent", dicField);
					content.SeoKey = Content.GetStringFromDic("SeoKey", dicField);
					content.SeoDescription = Content.GetStringFromDic("SeoDescription", dicField);
					content.IsTop = Content.GetBoolFromDic("IsTop", dicField, false);
					content.IsRecommend = Content.GetBoolFromDic("IsRecommend", dicField, false);
					content.IsNew = Content.GetBoolFromDic("IsNew", dicField, false);
					content.TemplateFile = Content.GetStringFromDic("TemplateFile", dicField);
					content.Hit = Content.GetIntFromDic("Hit", dicField, 0);
					content.Sort = Content.GetIntFromDic("Sort", dicField, 999);
					content.AutoTimeStamp = Content.GetDateTimeFromDic("AutoTimeStamp", dicField, new DateTime(1900, 1, 1));
					Content.Update(content);
					IList<ContFieldInfo> customFieldListByModelID = ContField.GetCustomFieldListByModelID(content.ModelID);
					foreach (ContFieldInfo current in customFieldListByModelID)
					{
						if (current.FieldName == "ContID")
						{
							current.Value = content.AutoID.ToString();
						}
						else
						{
							current.Value = Content.GetStringFromDic(current.FieldName, dicField);
						}
					}
					if (Content.ExistsCustomTableData(cacheModelByID.TableName, content.AutoID))
					{
						Content.UpdateCustomContent(content.AutoID, content.TableName, customFieldListByModelID);
					}
					else
					{
						Content.AddCustomContent(cacheModelByID, customFieldListByModelID);
					}
					result = true;
				}
			}
			return result;
		}

		public static bool UpdateCustomContent(int intContID, string strTableName, IList<ContFieldInfo> fieldList)
		{
			string commandText = Content.GenerateSqlOfUpdate(intContID, strTableName, fieldList);
			SqlParameter[] parameters = Content.PrepareSqlParameters(fieldList);
			return DBHelper.ExecuteNonQuery(commandText, parameters) > 0;
		}

		public static string GenerateSqlOfUpdate(int intContID, string strTableName, IList<ContFieldInfo> fieldList)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("UPDATE " + strTableName + " SET ");
			foreach (ContFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append(current.FieldName);
				stringBuilder.Append("=@");
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(" WHERE ContID=");
			stringBuilder.Append(intContID);
			return stringBuilder.ToString();
		}

		public static bool UpdateStatus(string strIDList, ContStatus status)
		{
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE cms_Content SET [Status] =",
				(int)status,
				" WHERE AutoID in ( ",
				strIDList,
				" )"
			}));
		}

		public static bool MoveContent(string idsList, int nodeID, string nodeName)
		{
			bool flag = BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE cms_Content SET\tNodeID =",
				nodeID,
				",\tNodeName ='",
				nodeName,
				"' WHERE AutoID IN ( ",
				idsList,
				" )"
			}));
			if (flag)
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
				CacheUtils.Del("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
			}
			return flag;
		}

		public static bool DelToRecycle(string strIds)
		{
			return Content.UpdateStatus(strIds, ContStatus.Recycle);
		}

		public static bool DelFromRecycle(int intContID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ContID", intContID)
			};
			return BizBase.dbo.ExecProc("p_System_ContentDel", arrParam);
		}

		public static void DelFromRecycle(string strIDList)
		{
			string[] array = strIDList.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string value = array2[i];
				Content.DelFromRecycle(Convert.ToInt32(value));
			}
		}

		public static bool Clear()
		{
			bool result;
			try
			{
				DataTable dataTable = BizBase.dbo.GetDataTable(" SELECT AutoID FROM cms_Content WHERE Status=-1 ");
				foreach (DataRow dataRow in dataTable.Rows)
				{
					Content.DelFromRecycle((int)dataRow["AutoID"]);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		internal static string GetStringFromDic(string fieldName, Dictionary<string, ContFieldInfo> dicField)
		{
			string result;
			if (dicField.ContainsKey(fieldName))
			{
				result = dicField[fieldName].Value;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		internal static DateTime GetDateTimeFromDic(string fieldName, Dictionary<string, ContFieldInfo> dicField, DateTime defaultValue)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			DateTime result;
			if (dicField.ContainsKey(fieldName) && DateTime.TryParse(dicField[fieldName].Value, out dateTime))
			{
				result = dateTime;
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static int GetIntFromDic(string fieldName, Dictionary<string, ContFieldInfo> dicField, int defaultValue)
		{
			int result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToInt(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static bool GetBoolFromDic(string fieldName, Dictionary<string, ContFieldInfo> dicField, bool defaultValue)
		{
			bool result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToBool(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		public static DataTable GetContentList(int nodeID, int pageIndex, int pageSize)
		{
			DataSet contentList = Content.GetContentList(" NodeID=" + nodeID + " AND Status=99 ", " Sort asc,AutoID desc ", pageIndex, pageSize);
			DataTable result;
			if (contentList != null && contentList.Tables.Count > 0)
			{
				result = contentList.Tables[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static DataSet GetContentList(string strCondition, string strSort, int pageIndex, int pageSize)
		{
			int num = 0;
			int num2 = 0;
			return Content.GetPagerData("*,(SELECT Count(*) FROM cms_Comment WHERE ContID=cms_Content.AutoID AND IsAudit=1) AS CommentCount", strCondition, strSort, pageSize, pageIndex, ref num, ref num2);
		}

		public static IList<ContentInfo> GetContentPagerList(string strCondition, string strSort, int pageIndex, int pageSize)
		{
			int num = 0;
			int num2 = 0;
			return Content.GetPagerList(strCondition, strSort, pageIndex, pageSize, ref num, ref num2);
		}

		public static DataSet GetContentList(bool containSubNode, int nodeID, int status, string strCondition, string strSort, int pageIndex, int pageSize)
		{
			string nodeIds = nodeID.ToString();
			if (containSubNode)
			{
				NodeInfo cacheNodeById = Node.GetCacheNodeById(nodeID);
				if (cacheNodeById != null)
				{
					nodeIds = cacheNodeById.ChildList;
				}
			}
			return Content.GetContentList(Content.GetFilter(nodeIds, status, strCondition), strSort, pageIndex, pageSize);
		}

		private static string GetFilter(string nodeIds, int status, string strCondition)
		{
			string text = " Status=" + status + " ";
			nodeIds = DBUtils.ToValidId(nodeIds);
			if (!string.IsNullOrEmpty(nodeIds))
			{
				if (nodeIds.IndexOf(",") > 0)
				{
					text = text + " AND NodeID IN (" + nodeIds + ")";
				}
				else if (nodeIds != "0")
				{
					text = text + " AND NodeID=" + nodeIds;
				}
			}
			strCondition = strCondition.Trim();
			if (!string.IsNullOrEmpty(strCondition) && strCondition.StartsWith("AND"))
			{
				text += strCondition;
			}
			else if (!string.IsNullOrEmpty(strCondition) && !strCondition.StartsWith("AND"))
			{
				text = text + " AND " + strCondition;
			}
			return text;
		}

		public static ContentInfo GetContentById(int intContID)
		{
			ContentInfo model = BizBase.dbo.GetModel<ContentInfo>(" SELECT * FROM CMS_Content WHERE AutoID=" + intContID.ToString());
			if (model != null)
			{
				model.Node = Node.GetCacheNodeById(model.NodeID);
				model.ContentUrl = ContHelper.GetContentUrl(model.NodeID, model.AutoID, model.AutoTimeStamp);
				model.CustomTable = Content.GetCustomContentInfo(model.AutoID, model.TableName);
			}
			return model;
		}

		public static ContentInfo GetTopConentByNodeID(int intNodeID)
		{
			return Content.GetTopConentByNodeID(intNodeID, true);
		}

		public static ContentInfo GetTopConentByNodeIdentifier(string strNodeIdentifier)
		{
			return Content.GetTopConentByNodeIdentifier(strNodeIdentifier, true);
		}

		public static ContentInfo GetTopConentByNodeID(int intNodeID, bool boolIsAudit)
		{
			ContentInfo contentInfo = new ContentInfo();
			if (boolIsAudit)
			{
				contentInfo = BizBase.dbo.GetModel<ContentInfo>(" SELECT TOP 1 * FROM cms_Content WHERE NodeID=" + intNodeID + " AND Status=99 ORDER BY Sort ASC,AutoID desc ");
			}
			else
			{
				contentInfo = BizBase.dbo.GetModel<ContentInfo>(" SELECT TOP 1 * FROM cms_Content WHERE NodeID=" + intNodeID + " ORDER BY Sort ASC,AutoID desc ");
			}
			if (contentInfo != null)
			{
				contentInfo.Node = Node.GetCacheNodeById(contentInfo.NodeID);
				contentInfo.ContentUrl = ContHelper.GetContentUrl(contentInfo.NodeID, contentInfo.AutoID, contentInfo.AutoTimeStamp);
				contentInfo.CustomTable = Content.GetCustomContentInfo(contentInfo.AutoID, contentInfo.TableName);
			}
			return contentInfo;
		}

		public static ContentInfo GetTopConentByNodeIdentifier(string strNodeIdentifier, bool boolIsAudit)
		{
			NodeInfo cacheNodeByIdentifier = Node.GetCacheNodeByIdentifier(strNodeIdentifier);
			ContentInfo result;
			if (cacheNodeByIdentifier != null)
			{
				result = Content.GetTopConentByNodeID(cacheNodeByIdentifier.AutoID);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static DataTable GetContentById(int intContID, string tableName, List<ContFieldInfo> usingFieldList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ");
			foreach (ContFieldInfo current in usingFieldList)
			{
				if (current.IsSystem)
				{
					stringBuilder.Append("A.");
				}
				else
				{
					stringBuilder.Append("C.");
				}
				stringBuilder.Append(current.FieldName);
				stringBuilder.Append(",");
			}
			stringBuilder.Append("C.ContID");
			stringBuilder.Append(" FROM cms_Content A INNER JOIN ");
			stringBuilder.Append(tableName);
			stringBuilder.Append(" C ON A.AutoID=C.ContID WHERE A.AutoID=");
			stringBuilder.Append(intContID);
			return BizBase.dbo.GetDataTable(stringBuilder.ToString());
		}

		public static int GetContCount()
		{
			return Content.GetContCount("Status=99");
		}

		public static int GetContCount(string strCondition)
		{
			return BizBase.dbo.GetCount("cms_Content", strCondition) ?? 0;
		}

		private static string GenerateSelectSQL(int nodeId)
		{
			string text = "A.AutoID=C.ContID AND Status=99";
			if (nodeId > 0)
			{
				text = text + " AND A.NodeID=" + nodeId.ToString();
			}
			return text;
		}

		public static DataTable GetCustomContentInfo(int intContID, string strTableName)
		{
			return BizBase.dbo.GetDataTable(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM ",
				strTableName,
				" WHERE ContID=",
				intContID
			}));
		}

		public static bool HasContent(int nodeID)
		{
			return BizBase.dbo.GetValue<int>(" SELECT TOP 1 1 FROM cms_Content WHERE NodeID=" + nodeID.ToString()) > 0;
		}

		public static SqlParameter[] PrepareSqlParameters(IList<ContFieldInfo> fieldList)
		{
			List<SqlParameter> list = new List<SqlParameter>();
			foreach (ContFieldInfo current in fieldList)
			{
				SqlParameter item = new SqlParameter("@" + current.FieldName, current.Value);
				list.Add(item);
			}
			return list.ToArray();
		}

		public static IList<ContFieldInfo> GetFieldListWithValue(int intContID, int intModelID, string strTableName)
		{
			IList<ContFieldInfo> fieldListByModelID = ContField.GetFieldListByModelID(intModelID, true);
			IList<ContFieldInfo> usingFieldList = ContField.GetUsingFieldList(intModelID);
			DataTable contentById = Content.GetContentById(intContID, strTableName, (List<ContFieldInfo>)usingFieldList);
			if (contentById.Rows.Count == 1)
			{
				foreach (ContFieldInfo current in fieldListByModelID)
				{
					if (contentById.Columns.Contains(current.FieldName))
					{
						current.Value = contentById.Rows[0][current.FieldName].ToString();
					}
					else
					{
						current.Value = current.DefaultValue;
					}
				}
			}
			return fieldListByModelID;
		}

		public static DataTable GetContArchives(int intNodeID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@NodeID", intNodeID)
			};
			return BizBase.dbo.ExecProcReDT("p_System_GetContArchives", arrParam);
		}

		public static bool CopyContent(int intContID)
		{
			ContentInfo dataById = Content.GetDataById(intContID);
			bool result;
			if (dataById != null)
			{
				ContModelInfo cacheModelByID = ContModel.GetCacheModelByID(dataById.ModelID);
				string strSQL = string.Concat(new object[]
				{
					"INSERT INTO cms_Content (     NodeID,     NodeName,     ModelID,     TableName,     Title,     SubTitle,     Summary,     Author,     Editor,     Source,     SourceUrl,     Content,     Attachment,     TagKey,     ContentImage,     SeoKey,     SeoDescription,     Lang,     Inputer,     Sort,     Status,     AutoTimeStamp ) SELECT      NodeID,     NodeName,     ModelID,     TableName,     '复制:'+Title,     SubTitle,     Summary,     Author,     Editor,     Source,     SourceUrl,     Content,     Attachment,     TagKey,     ContentImage,     SeoKey,     SeoDescription,     Lang,     '",
					Account.GetLoginAccount().AccountName,
					"',    (SELECT MAX(Sort)+1 FROM cms_Content) AS CurrSort,     0,     getdate() FROM cms_Content WHERE AutoID=",
					intContID,
					";if @@rowcount>0 select @@IDENTITY"
				});
				object @object = BizBase.dbo.GetObject(strSQL);
				int num = (@object == null) ? 0 : Convert.ToInt32(@object);
				if (num > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(" INSERT INTO " + dataById.TableName + " ( ");
					IList<ContFieldInfo> customFieldListByModelID = ContField.GetCustomFieldListByModelID(cacheModelByID.AutoID);
					for (int i = 0; i < customFieldListByModelID.Count; i++)
					{
						if (customFieldListByModelID[i].FieldName != "AutoID")
						{
							if (i == customFieldListByModelID.Count - 1)
							{
								stringBuilder.Append(customFieldListByModelID[i].FieldName);
							}
							else
							{
								stringBuilder.Append(customFieldListByModelID[i].FieldName + ",");
							}
						}
					}
					stringBuilder.Append(" ) ");
					stringBuilder.Append(" SELECT ");
					for (int i = 0; i < customFieldListByModelID.Count; i++)
					{
						if (customFieldListByModelID[i].FieldName != "AutoID")
						{
							if (i == customFieldListByModelID.Count - 1)
							{
								if (customFieldListByModelID[i].FieldName.Equals("ContID"))
								{
									stringBuilder.Append(num.ToString());
								}
								else
								{
									stringBuilder.Append(customFieldListByModelID[i].FieldName);
								}
							}
							else if (customFieldListByModelID[i].FieldName.Equals("ContID"))
							{
								stringBuilder.Append(num.ToString() + ",");
							}
							else
							{
								stringBuilder.Append(customFieldListByModelID[i].FieldName + ",");
							}
						}
					}
					stringBuilder.Append(string.Concat(new object[]
					{
						" FROM ",
						dataById.TableName,
						" WHERE ContID=",
						dataById.AutoID
					}));
					result = BizBase.dbo.ExecSQL(stringBuilder.ToString());
					return result;
				}
			}
			result = false;
			return result;
		}

		public static int UpdateHit(int intContID)
		{
			string strSQL = " UPDATE cms_Content SET Hit=Hit+1 WHERE AutoID=" + intContID.ToString() + ";SELECT Hit FROM cms_Content WHERE AutoID=" + intContID.ToString();
			object @object = BizBase.dbo.GetObject(strSQL);
			return (@object == null) ? 0 : Convert.ToInt32(@object);
		}

		public static bool ExistsCustomTableData(string ModelTable, int ContID)
		{
			return BizBase.dbo.GetValue<int>(" select COUNT(*) from " + ModelTable + " where ContID=" + ContID.ToString()) > 0;
		}
	}
}
