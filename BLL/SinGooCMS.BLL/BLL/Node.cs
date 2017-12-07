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
using System.Web;

namespace SinGooCMS.BLL
{
	public class Node : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_Node ");
			}
		}

		public static NodeAddStatus Add(NodeInfo node)
		{
			int num = 0;
			return Node.Add(node, out num);
		}

		public static NodeAddStatus Add(NodeInfo node, out int newNodeID)
		{
			newNodeID = 0;
			NodeAddStatus result;
			if (Node.GetMainNodeCount() >= Ver.GetVer().NodeLimit && node.IsShowOnMenu)
			{
				result = NodeAddStatus.ToMoreNode;
			}
			else
			{
				node.Creator = Account.GetLoginAccount().AccountName;
				node.ChildCount = 0;
				int num = Node.ExistsNode(node.ParentID, node.NodeName, node.UrlRewriteName, "Add", 0);
				if (num > 0)
				{
					result = (NodeAddStatus)num;
				}
				else
				{
					if (node.ParentID > 0)
					{
						NodeInfo cacheNodeById = Node.GetCacheNodeById(node.ParentID);
						if (cacheNodeById == null)
						{
							result = NodeAddStatus.ParentNodeNotExists;
							return result;
						}
						node.ParentPath = cacheNodeById.ParentPath + "," + cacheNodeById.AutoID.ToString();
						node.RootID = cacheNodeById.RootID;
						node.Depth = cacheNodeById.Depth + 1;
						node.Sort = BizBase.dbo.GetValue<int>(" SELECT max(Sort) FROM cms_Node WHERE ParentID=" + node.RootID) + 1;
					}
					else
					{
						node.ParentPath = "0";
						node.Depth = 1;
						node.Sort = BizBase.dbo.GetValue<int>(" SELECT max(Sort) FROM cms_Node WHERE ParentID=0") + 1;
					}
					node.Setting = XmlSerializerUtils.Serialize<NodeSetting>(node.NodeSetting);
					newNodeID = BizBase.dbo.InsertModel<NodeInfo>(node);
					if (newNodeID > 0)
					{
						node.AutoID = newNodeID;
						BizBase.dbo.ExecSQL(string.Concat(new object[]
						{
							" UPDATE cms_Node SET ChildList = '",
							newNodeID,
							"' WHERE AutoID=",
							newNodeID
						}));
						if (node.ParentID > 0)
						{
							BizBase.dbo.ExecSQL(" UPDATE CMS_Node SET ChildCount = ChildCount + 1 WHERE AutoID =" + node.ParentID);
							Node.UpdateNowParents(node, "Add");
						}
						else
						{
							BizBase.dbo.ExecSQL(string.Concat(new object[]
							{
								" UPDATE cms_Node SET RootID = ",
								newNodeID,
								" WHERE AutoID=",
								newNodeID
							}));
						}
						CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
						result = NodeAddStatus.Success;
					}
					else
					{
						CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
						result = NodeAddStatus.Error;
					}
				}
			}
			return result;
		}

		public static NodeUpdateStatus Update(NodeInfo node)
		{
			NodeInfo dataById = Node.GetDataById(node.AutoID);
			node.Setting = XmlSerializerUtils.Serialize<NodeSetting>(node.NodeSetting);
			int num = Node.ExistsNode(node.ParentID, node.NodeName, node.UrlRewriteName, "Modify", dataById.AutoID);
			NodeUpdateStatus result;
			if (num > 0)
			{
				result = (NodeUpdateStatus)num;
			}
			else if (!dataById.IsShowOnMenu && node.IsShowOnMenu && Node.GetMainNodeCount() >= Ver.GetVer().NodeLimit)
			{
				result = NodeUpdateStatus.ToMoreNode;
			}
			else if (BizBase.dbo.UpdateModel<NodeInfo>(node))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
				result = NodeUpdateStatus.Success;
			}
			else
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
				result = NodeUpdateStatus.Error;
			}
			return result;
		}

		public static NodeDeleteStatus Delete(int nodeID)
		{
			NodeInfo cacheNodeById = Node.GetCacheNodeById(nodeID);
			NodeDeleteStatus result;
			if (cacheNodeById == null)
			{
				result = NodeDeleteStatus.NotExists;
			}
			else if (cacheNodeById.ChildCount > 0)
			{
				result = NodeDeleteStatus.HasChildNode;
			}
			else if (Node.ExistsContent(nodeID))
			{
				result = NodeDeleteStatus.HasContent;
			}
			else if (BizBase.dbo.DeleteModel<NodeInfo>(cacheNodeById))
			{
				if (cacheNodeById.ParentID > 0)
				{
					NodeInfo cacheNodeById2 = Node.GetCacheNodeById(cacheNodeById.ParentID);
					if (cacheNodeById2 != null)
					{
						BizBase.dbo.UpdateTable(string.Concat(new object[]
						{
							" UPDATE cms_Node SET ChildCount=ChildCount-1 WHERE AutoID=",
							cacheNodeById.ParentID,
							";UPDATE cms_Node SET ChildList=REPLACE(ChildList,',",
							cacheNodeById.AutoID,
							"','') WHERE AutoID IN (",
							cacheNodeById.ParentPath,
							"); "
						}));
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
				result = NodeDeleteStatus.Success;
			}
			else
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
				result = NodeDeleteStatus.Error;
			}
			return result;
		}

		public static IList<NodeInfo> GetCacheAllNodes()
		{
			IList<NodeInfo> list = (IList<NodeInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetCMSNode");
			if (list == null)
			{
				list = BizBase.dbo.GetList<NodeInfo>(" select *,ContCount=(select COUNT(*) from cms_Content where NodeID in (select * from dbo.f_System_Split(ChildList,',')) and [Status]=99) from cms_Node order by Sort asc,Depth asc,RootID asc ");
				foreach (NodeInfo current in list)
				{
					current.NodeSetting = Node.LoadNodeSetting(current.Setting);
				}
				CacheUtils.Insert("JsonLeeCMS_CacheForGetCMSNode", list);
			}
			return list;
		}

		public static IList<NodeInfo> GetCacheNodeListByModeID(int intModelID)
		{
			IEnumerable<NodeInfo> enumerable = from p in Node.GetCacheAllNodes()
			where p.ModelID.Equals(intModelID)
			select p;
			IList<NodeInfo> result;
			if (enumerable != null)
			{
				result = enumerable.ToList<NodeInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<NodeInfo> GetCachedNodeListByIds(string strIDs)
		{
			IList<NodeInfo> cacheAllNodes = Node.GetCacheAllNodes();
			IList<NodeInfo> list = new List<NodeInfo>();
			string[] array = strIDs.Split(new char[]
			{
				','
			});
			if (cacheAllNodes != null && cacheAllNodes.Count > 0)
			{
				foreach (NodeInfo current in cacheAllNodes)
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text = array2[i];
						if (ValidateUtils.IsNumber(text) && current.AutoID.Equals(int.Parse(text)))
						{
							list.Add(current);
						}
					}
				}
			}
			return list;
		}

		public static IList<NodeInfo> GetCacheNodeListByRootIdentifier(string strIdentifier)
		{
			NodeInfo cacheNodeByIdentifier = Node.GetCacheNodeByIdentifier(strIdentifier);
			return Node.GetCacheNodeListByRootId((cacheNodeByIdentifier == null) ? 0 : cacheNodeByIdentifier.RootID);
		}

		public static NodeInfo GetCacheNodeByCode(string strCode)
		{
			return string.IsNullOrEmpty(strCode) ? null : (from p in Node.GetCacheAllNodes()
			where p.UrlRewriteName.Equals(strCode)
			select p).FirstOrDefault<NodeInfo>();
		}

		public static IList<NodeInfo> GetCacheNodeListByRootId(int intRootID)
		{
			IEnumerable<NodeInfo> enumerable = from p in Node.GetCacheAllNodes()
			where p.RootID.Equals(intRootID)
			select p;
			IList<NodeInfo> result;
			if (enumerable != null)
			{
				result = enumerable.ToList<NodeInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<NodeInfo> GetCacheChildNode(string strParentIdentifier)
		{
			NodeInfo cacheNodeByIdentifier = Node.GetCacheNodeByIdentifier(strParentIdentifier);
			return Node.GetCacheChildNode((cacheNodeByIdentifier == null) ? 0 : cacheNodeByIdentifier.AutoID);
		}

		public static IList<NodeInfo> GetCacheChildNode(int intParentID)
		{
			IEnumerable<NodeInfo> enumerable = from p in Node.GetCacheAllNodes()
			where p.ParentID.Equals(intParentID)
			select p;
			IList<NodeInfo> result;
			if (enumerable != null)
			{
				result = enumerable.ToList<NodeInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<NodeInfo> GetCacheAllChildNode(string strParentIdentifier)
		{
			NodeInfo cacheNodeByIdentifier = Node.GetCacheNodeByIdentifier(strParentIdentifier);
			return Node.GetCacheAllChildNode((cacheNodeByIdentifier == null) ? 0 : cacheNodeByIdentifier.AutoID);
		}

		public static IList<NodeInfo> GetCacheAllChildNode(int intParentID)
		{
			NodeInfo nodeParent = Node.GetCacheNodeById(intParentID);
			IList<NodeInfo> result;
			if (nodeParent != null)
			{
				result = (from p in Node.GetCacheAllNodes()
				where nodeParent.ChildList.Split(new char[]
				{
					','
				}).Contains(p.AutoID.ToString())
				select p).ToList<NodeInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static NodeInfo GetCacheNodeById(int intNodeID)
		{
			return (intNodeID == 0) ? null : (from p in Node.GetCacheAllNodes()
			where p.AutoID.Equals(intNodeID)
			select p).FirstOrDefault<NodeInfo>();
		}

		public static NodeInfo GetCacheNodeByIdentifier(string strIdentifier)
		{
			return (from p in Node.GetCacheAllNodes()
			where p.UrlRewriteName.Equals(strIdentifier)
			select p).FirstOrDefault<NodeInfo>();
		}

		public static NodeInfo GetCacheRootNode(string strIdentifier)
		{
			NodeInfo cacheNodeByIdentifier = Node.GetCacheNodeByIdentifier(strIdentifier);
			NodeInfo result;
			if (cacheNodeByIdentifier != null)
			{
				result = Node.GetCacheNodeById(cacheNodeByIdentifier.RootID);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static NodeInfo GetCacheRootNode(int intNodeID)
		{
			NodeInfo cacheNodeById = Node.GetCacheNodeById(intNodeID);
			NodeInfo result;
			if (cacheNodeById != null)
			{
				result = Node.GetCacheNodeById(cacheNodeById.RootID);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static NodeInfo GetDefaultNode()
		{
			return BizBase.dbo.GetModel<NodeInfo>(" SELECT TOP 1 * FROM cms_Node WHERE ParentID=0 ORDER BY Sort ASC ");
		}

		public static NodeInfo GetCacheDefultNode(int intRootID, int intDepth)
		{
			List<NodeInfo> list = (List<NodeInfo>)Node.GetCacheNodeListByRootId(intRootID);
			List<NodeInfo> list2 = new List<NodeInfo>();
			if (list != null && list.Count > 0)
			{
				foreach (NodeInfo current in list)
				{
					if (current.Depth == intDepth)
					{
						list2.Add(current);
					}
				}
			}
			NodeInfo result;
			if (list2.Count > 0)
			{
				result = list2[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static List<NodeInfo> GetNodeTreeList()
		{
			List<NodeInfo> obj = (List<NodeInfo>)Node.GetCacheAllNodes();
			List<NodeInfo> list = JObject.DeepClone<List<NodeInfo>>(obj);
			return Node.GetRelationNodeList(list, 0);
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

		public static List<NodeInfo> GetRelationNodeList(List<NodeInfo> list, int intParentID)
		{
			List<NodeInfo> list2 = list.FindAll((NodeInfo parameterA) => parameterA.ParentID == intParentID);
			List<NodeInfo> list3 = new List<NodeInfo>();
			int num = 0;
			foreach (NodeInfo current in list2)
			{
				if (num == list2.Count - 1)
				{
					current.NodeName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, true)) + current.NodeName;
				}
				else
				{
					current.NodeName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, false)) + current.NodeName;
				}
				list3.Add(current);
				if (current.ChildCount > 0)
				{
					list3.AddRange(Node.GetRelationNodeList(list, current.AutoID));
				}
				num++;
			}
			return list3;
		}

		public static List<NodeInfo> GetNodeListInParentPath(string strParentPath)
		{
			List<NodeInfo> list = new List<NodeInfo>();
			string commandText = " SELECT AutoID,ParentPath,ChildList,ChildCount,NodeName,UrlRewriteName,Depth  FROM cms_Node WHERE AutoID IN (" + strParentPath + ") ORDER BY Depth ASC,Sort ASC,AutoID DESC ";
			using (SafeDataReader safeDataReader = new SafeDataReader(DBHelper.ExecuteReader(commandText)))
			{
				while (safeDataReader.Read())
				{
					NodeInfo item = new NodeInfo
					{
						AutoID = safeDataReader.GetInt32("AutoID"),
						ParentPath = safeDataReader.GetString("ParentPath"),
						ChildList = safeDataReader.GetString("ChildList"),
						ChildCount = safeDataReader.GetInt32("ChildCount"),
						NodeName = safeDataReader.GetString("NodeName"),
						UrlRewriteName = safeDataReader.GetString("UrlRewriteName"),
						Depth = safeDataReader.GetInt32("Depth")
					};
					list.Add(item);
				}
			}
			return list;
		}

		public static IList<NodeContCountInfo> GetCacheNormalCountList()
		{
			IList<NodeContCountInfo> list = (List<NodeContCountInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetNodesContentCountNoraml");
			if (list == null)
			{
				list = BizBase.dbo.GetList<NodeContCountInfo>(" SELECT NodeID,NodeName,COUNT(*) AS ContCount FROM cms_Content WHERE Status=99 GROUP BY NodeID,NodeName ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetNodesContentCountNoraml", list);
			}
			return list;
		}

		public static IList<NodeContCountInfo> GetCacheRecycleCountList()
		{
			IList<NodeContCountInfo> list = (List<NodeContCountInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetNodesContentCountRecycle");
			if (list == null)
			{
				list = BizBase.dbo.GetList<NodeContCountInfo>(" SELECT NodeID,NodeName,COUNT(*) AS ContCount FROM cms_Content WHERE Status=-1 GROUP BY NodeID,NodeName ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetNodesContentCountRecycle", list);
			}
			return list;
		}

		public static IList<NodeContCountInfo> GetCacheDraftCountList()
		{
			IList<NodeContCountInfo> list = (List<NodeContCountInfo>)CacheUtils.Get("JsonLeeCMS_CacheForGetNodesContentCountForDraft");
			if (list == null)
			{
				list = BizBase.dbo.GetList<NodeContCountInfo>(" SELECT NodeID,NodeName,COUNT(*) AS ContCount FROM cms_Content WHERE Status=0 GROUP BY NodeID,NodeName ");
				CacheUtils.Insert("JsonLeeCMS_CacheForGetNodesContentCountForDraft", list);
			}
			return list;
		}

		public static void NodeMove(NodeInfo nodeSource, int intTargetID)
		{
			if (intTargetID == 0)
			{
				nodeSource.ParentID = 0;
				nodeSource.Depth = 1;
				nodeSource.ParentPath = "0";
				nodeSource.RootID = nodeSource.AutoID;
			}
			else
			{
				NodeInfo cacheNodeById = Node.GetCacheNodeById(intTargetID);
				nodeSource.ParentID = cacheNodeById.AutoID;
				nodeSource.ParentPath = cacheNodeById.ParentPath + "," + cacheNodeById.AutoID.ToString();
				nodeSource.RootID = cacheNodeById.RootID;
				nodeSource.Depth = cacheNodeById.Depth + 1;
			}
			NodeInfo dataById = Node.GetDataById(nodeSource.AutoID);
			if (dataById.ParentID > 0)
			{
				NodeInfo dataById2 = Node.GetDataById(dataById.ParentID);
				dataById2.ChildCount--;
				BizBase.dbo.UpdateModel<NodeInfo>(dataById2);
				Node.UpdateOrigleParents(dataById);
			}
			if (nodeSource.ParentPath != "0")
			{
				NodeInfo dataById3 = Node.GetDataById(nodeSource.ParentID);
				dataById3.ChildCount++;
				BizBase.dbo.UpdateModel<NodeInfo>(dataById3);
				Node.UpdateNowParents(nodeSource, "Modify");
			}
			int depth = dataById.Depth;
			int depth2 = nodeSource.Depth;
			int intDepthCha = depth2 - depth;
			Node.UpdateChildList(nodeSource, intDepthCha);
			BizBase.dbo.UpdateModel<NodeInfo>(nodeSource);
			CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
		}

		private static void UpdateNowParents(NodeInfo nodeSource, string strType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (SafeDataReader safeDataReader = new SafeDataReader(BizBase.dbo.GetDataReader(" SELECT AutoID,ChildList FROM CMS_Node WHERE AutoID IN (" + nodeSource.ParentPath + ") ")))
			{
				while (safeDataReader.Read())
				{
					string text = safeDataReader.GetString("ChildList") + "," + ((strType == "Add") ? nodeSource.AutoID.ToString() : nodeSource.ChildList);
					stringBuilder.Append(string.Concat(new object[]
					{
						" UPDATE cms_Node SET ChildList ='",
						text,
						"' WHERE AutoID =",
						safeDataReader.GetInt32("AutoID"),
						";"
					}));
				}
			}
			string text2 = stringBuilder.ToString().Trim();
			if (!string.IsNullOrEmpty(text2))
			{
				BizBase.dbo.ExecSQL(text2);
			}
		}

		private static void UpdateOrigleParents(NodeInfo nodeSource)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (SafeDataReader safeDataReader = new SafeDataReader(BizBase.dbo.GetDataReader(" SELECT AutoID,ChildList FROM CMS_Node WHERE AutoID IN (" + nodeSource.ParentPath + ") ")))
			{
				while (safeDataReader.Read())
				{
					string childList = nodeSource.ChildList;
					string @string = safeDataReader.GetString("ChildList");
					string[] arrSource = childList.Split(new char[]
					{
						','
					});
					string[] array = @string.Split(new char[]
					{
						','
					});
					string text = string.Empty;
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text2 = array2[i];
						if (!StringUtils.Contain(arrSource, text2))
						{
							text = text + text2 + ",";
						}
					}
					if (text.LastIndexOf(",") != -1)
					{
						text = text.Substring(0, text.Length - 1);
					}
					stringBuilder.Append(string.Concat(new object[]
					{
						" UPDATE cms_Node SET ChildList ='",
						text,
						"' WHERE AutoID =",
						safeDataReader.GetInt32("AutoID"),
						";"
					}));
				}
			}
			string text3 = stringBuilder.ToString().Trim();
			if (!string.IsNullOrEmpty(text3))
			{
				BizBase.dbo.ExecSQL(text3);
			}
		}

		private static void UpdateChildList(NodeInfo nodeParent, int intDepthCha)
		{
			using (SafeDataReader safeDataReader = new SafeDataReader(BizBase.dbo.GetDataReader(" SELECT AutoID,Depth FROM CMS_Node WHERE ParentID=" + nodeParent.AutoID)))
			{
				while (safeDataReader.Read())
				{
					int num = safeDataReader.GetInt32("Depth");
					num += intDepthCha;
					string text = nodeParent.ParentPath + "," + nodeParent.AutoID.ToString();
					BizBase.dbo.ExecSQL(string.Concat(new object[]
					{
						" UPDATE cms_Node SET Depth =",
						num,
						",RootID=",
						nodeParent.RootID,
						",ParentPath='",
						text,
						"' WHERE AutoID =",
						WebUtils.GetInt(safeDataReader.GetInt32("AutoID")),
						";"
					}));
					NodeInfo dataById = Node.GetDataById(safeDataReader.GetInt32("AutoID"));
					if (dataById.ChildCount > 0)
					{
						Node.UpdateChildList(dataById, intDepthCha);
					}
				}
			}
		}

		public static int GetMainNodeCount()
		{
			return BizBase.dbo.GetCount("cms_Node", "IsShowOnMenu=1") ?? 0;
		}

		public static int ExistsNode(int intNodeParentID, string strNodeName, string strUrlRewriteName, string strType, int intAutoID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ParentID", intNodeParentID),
				new SqlParameter("@NodeName", strNodeName),
				new SqlParameter("@UrlRewriteName", strUrlRewriteName),
				new SqlParameter("@ActionType", strType),
				new SqlParameter("@AutoID", intAutoID)
			};
			return (int)BizBase.dbo.ExecProcReValue("p_System_ExistsNode", arrParam);
		}

		public static bool ExistsContent(int intNodeID)
		{
			return BizBase.dbo.GetValue<int>(" SELECT TOP 1 1 FROM cms_Content WHERE NodeID=" + intNodeID.ToString()) > 0;
		}

		public static NodeSetting LoadNodeSetting(string settings)
		{
			NodeSetting result;
			if (string.IsNullOrEmpty(settings))
			{
				result = new NodeSetting();
			}
			else
			{
				NodeSetting nodeSetting = XmlSerializerUtils.Deserialize<NodeSetting>(settings);
				if (nodeSetting == null)
				{
					nodeSetting = new NodeSetting();
				}
				result = nodeSetting;
			}
			return result;
		}

		public static NodeInfo GetDataById(int intPrimaryKeyIDValue)
		{
			NodeInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<NodeInfo>(" SELECT TOP 1 AutoID,NodeName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,NodeImage,NodeBanner,SeoKey,SeoDescription,ItemPageSize,CustomLink,Setting,Remark,IsShowOnMenu,IsShowOnNav,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp FROM cms_Node WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static NodeInfo GetTopData()
		{
			return Node.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static NodeInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,NodeName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,NodeImage,NodeBanner,SeoKey,SeoDescription,ItemPageSize,CustomLink,Setting,Remark,IsShowOnMenu,IsShowOnNav,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp FROM cms_Node ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<NodeInfo>(text);
		}

		public static IList<NodeInfo> GetAllList()
		{
			return Node.GetList(0, string.Empty);
		}

		public static IList<NodeInfo> GetTopNList(int intTopCount)
		{
			return Node.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<NodeInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Node.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<NodeInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Node.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<NodeInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,NodeName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,NodeImage,NodeBanner,SeoKey,SeoDescription,ItemPageSize,CustomLink,Setting,Remark,IsShowOnMenu,IsShowOnNav,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp from cms_Node ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<NodeInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Node", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_Node", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Node.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Node.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Node.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Node.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,NodeName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,NodeImage,NodeBanner,SeoKey,SeoDescription,ItemPageSize,CustomLink,Setting,Remark,IsShowOnMenu,IsShowOnNav,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_Node";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<NodeInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Node.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<NodeInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<NodeInfo> result = new List<NodeInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,NodeName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,NodeImage,NodeBanner,SeoKey,SeoDescription,ItemPageSize,CustomLink,Setting,Remark,IsShowOnMenu,IsShowOnNav,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "cms_Node";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<NodeInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_Node SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_Node SET Sort =",
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
