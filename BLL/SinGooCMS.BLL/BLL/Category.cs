using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace SinGooCMS.BLL
{
	public class Category : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Category ");
			}
		}

		public static CategoryInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CategoryInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CategoryInfo>(" SELECT TOP 1 AutoID,CategoryName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,CategoryImage,SeoKey,SeoDescription,ItemPageSize,Remark,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp FROM shop_Category WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CategoryInfo GetTopData()
		{
			return Category.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CategoryInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,CategoryName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,CategoryImage,SeoKey,SeoDescription,ItemPageSize,Remark,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp FROM shop_Category ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CategoryInfo>(text);
		}

		public static IList<CategoryInfo> GetAllList()
		{
			return Category.GetList(0, string.Empty);
		}

		public static IList<CategoryInfo> GetTopNList(int intTopCount)
		{
			return Category.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CategoryInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Category.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CategoryInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Category.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CategoryInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,CategoryName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,CategoryImage,SeoKey,SeoDescription,ItemPageSize,Remark,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp from shop_Category ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CategoryInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Category", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Category", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Category.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Category.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Category.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Category.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,CategoryName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,CategoryImage,SeoKey,SeoDescription,ItemPageSize,Remark,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Category";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CategoryInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Category.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CategoryInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CategoryInfo> result = new List<CategoryInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,CategoryName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,CategoryImage,SeoKey,SeoDescription,ItemPageSize,Remark,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "shop_Category";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CategoryInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Category SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Category SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static NodeAddStatus Add(CategoryInfo cate)
		{
			int num;
			return Category.Add(cate, out num);
		}

		public static NodeAddStatus Add(CategoryInfo cate, out int newCateID)
		{
			newCateID = 0;
			cate.Creator = Account.GetLoginAccount().AccountName;
			cate.ChildCount = 0;
			NodeAddStatus result;
			if (Category.GetCount("UrlRewriteName='" + cate.UrlRewriteName + "'") > 0)
			{
				result = NodeAddStatus.ExistsNodeIdentifier;
			}
			else
			{
				if (cate.ParentID > 0)
				{
					CategoryInfo cacheCategoryByID = Category.GetCacheCategoryByID(cate.ParentID);
					if (cacheCategoryByID == null)
					{
						result = NodeAddStatus.ParentNodeNotExists;
						return result;
					}
					cate.ParentPath = cacheCategoryByID.ParentPath + "," + cacheCategoryByID.AutoID.ToString();
					cate.RootID = cacheCategoryByID.RootID;
					cate.Depth = cacheCategoryByID.Depth + 1;
					cate.Sort = BizBase.dbo.GetValue<int>(" SELECT max(Sort) FROM shop_Category WHERE ParentID=" + cate.RootID) + 1;
				}
				else
				{
					cate.ParentPath = "0";
					cate.Depth = 1;
					cate.Sort = BizBase.dbo.GetValue<int>(" SELECT max(Sort) FROM shop_Category WHERE ParentID=0") + 1;
				}
				newCateID = BizBase.dbo.InsertModel<CategoryInfo>(cate);
				if (newCateID > 0)
				{
					cate.AutoID = newCateID;
					BizBase.dbo.ExecSQL(string.Concat(new object[]
					{
						" UPDATE shop_Category SET ChildList = '",
						newCateID,
						"' WHERE AutoID=",
						newCateID
					}));
					if (cate.ParentID > 0)
					{
						BizBase.dbo.ExecSQL(" UPDATE shop_Category SET ChildCount = ChildCount + 1 WHERE AutoID =" + cate.ParentID);
						Category.UpdateNowParents(cate, "Add");
					}
					else
					{
						BizBase.dbo.ExecSQL(string.Concat(new object[]
						{
							" UPDATE shop_Category SET RootID = ",
							newCateID,
							" WHERE AutoID=",
							newCateID
						}));
					}
					CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
					result = NodeAddStatus.Success;
				}
				else
				{
					CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
					result = NodeAddStatus.Error;
				}
			}
			return result;
		}

		public static NodeUpdateStatus Update(CategoryInfo cate)
		{
			CategoryInfo dataById = Category.GetDataById(cate.AutoID);
			NodeUpdateStatus result;
			if (Category.GetCount(string.Concat(new object[]
			{
				"UrlRewriteName='",
				cate.UrlRewriteName,
				"' AND AutoID<>",
				cate.AutoID
			})) > 0)
			{
				result = NodeUpdateStatus.ExistsNodeIdentifier;
			}
			else
			{
				if (dataById.ParentID != cate.ParentID)
				{
					if (cate.ParentID.Equals(dataById.AutoID))
					{
						result = NodeUpdateStatus.UnNodeSelf;
						return result;
					}
					if (cate.ParentID == 0)
					{
						cate.ParentID = 0;
						cate.Depth = 1;
						cate.ParentPath = "0";
						cate.RootID = cate.AutoID;
					}
					else
					{
						CategoryInfo cacheCategoryByID = Category.GetCacheCategoryByID(cate.ParentID);
						cate.ParentID = cacheCategoryByID.AutoID;
						cate.ParentPath = cacheCategoryByID.ParentPath + "," + cacheCategoryByID.AutoID.ToString();
						cate.RootID = cacheCategoryByID.RootID;
						cate.Depth = cacheCategoryByID.Depth + 1;
					}
					if (dataById.ParentID > 0)
					{
						CategoryInfo dataById2 = Category.GetDataById(dataById.ParentID);
						dataById2.ChildCount--;
						BizBase.dbo.UpdateModel<CategoryInfo>(dataById2);
						Category.UpdateOrigleParents(dataById);
					}
					if (cate.ParentPath != "0")
					{
						CategoryInfo dataById3 = Category.GetDataById(cate.ParentID);
						dataById3.ChildCount++;
						BizBase.dbo.UpdateModel<CategoryInfo>(dataById3);
						Category.UpdateNowParents(cate, "Modify");
					}
					int depth = dataById.Depth;
					int depth2 = cate.Depth;
					int intDepthCha = depth2 - depth;
					Category.UpdateChildList(cate, intDepthCha);
				}
				if (BizBase.dbo.UpdateModel<CategoryInfo>(cate))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
					result = NodeUpdateStatus.Success;
				}
				else
				{
					CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
					result = NodeUpdateStatus.Error;
				}
			}
			return result;
		}

		public static NodeDeleteStatus Delete(int nodeID)
		{
			CategoryInfo cacheCategoryByID = Category.GetCacheCategoryByID(nodeID);
			NodeDeleteStatus result;
			if (cacheCategoryByID == null)
			{
				result = NodeDeleteStatus.NotExists;
			}
			else if (cacheCategoryByID.ChildCount > 0)
			{
				result = NodeDeleteStatus.HasChildNode;
			}
			else if (Category.ExistsProduct(nodeID))
			{
				result = NodeDeleteStatus.HasContent;
			}
			else if (BizBase.dbo.DeleteModel<CategoryInfo>(cacheCategoryByID))
			{
				if (cacheCategoryByID.ParentID > 0)
				{
					CategoryInfo cacheCategoryByID2 = Category.GetCacheCategoryByID(cacheCategoryByID.ParentID);
					if (cacheCategoryByID2 != null)
					{
						BizBase.dbo.UpdateTable(string.Concat(new object[]
						{
							" UPDATE shop_Category SET ChildCount=ChildCount-1 WHERE ChildCount>0 AND AutoID=",
							cacheCategoryByID.ParentID,
							";UPDATE shop_Category SET ChildList=REPLACE(ChildList,',",
							cacheCategoryByID.AutoID,
							"','') WHERE AutoID IN (",
							cacheCategoryByID.ParentPath,
							"); "
						}));
					}
				}
				CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
				result = NodeDeleteStatus.Success;
			}
			else
			{
				CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
				result = NodeDeleteStatus.Error;
			}
			return result;
		}

		private static void UpdateNowParents(CategoryInfo nodeSource, string strType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (SafeDataReader safeDataReader = new SafeDataReader(BizBase.dbo.GetDataReader(" SELECT AutoID,ChildList FROM shop_Category WHERE AutoID IN (" + nodeSource.ParentPath + ") ")))
			{
				while (safeDataReader.Read())
				{
					string text = safeDataReader.GetString("ChildList") + "," + ((strType == "Add") ? nodeSource.AutoID.ToString() : nodeSource.ChildList);
					stringBuilder.Append(string.Concat(new object[]
					{
						" UPDATE shop_Category SET ChildList ='",
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

		private static void UpdateOrigleParents(CategoryInfo nodeSource)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (SafeDataReader safeDataReader = new SafeDataReader(BizBase.dbo.GetDataReader(" SELECT AutoID,ChildList FROM shop_Category WHERE AutoID IN (" + nodeSource.ParentPath + ") ")))
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
						" UPDATE shop_Category SET ChildList ='",
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

		private static void UpdateChildList(CategoryInfo nodeParent, int intDepthCha)
		{
			using (SafeDataReader safeDataReader = new SafeDataReader(BizBase.dbo.GetDataReader(" SELECT AutoID,Depth FROM shop_Category WHERE ParentID=" + nodeParent.AutoID)))
			{
				while (safeDataReader.Read())
				{
					int num = safeDataReader.GetInt32("Depth");
					num += intDepthCha;
					string text = nodeParent.ParentPath + "," + nodeParent.AutoID.ToString();
					BizBase.dbo.ExecSQL(string.Concat(new object[]
					{
						" UPDATE shop_Category SET Depth =",
						num,
						",RootID=",
						nodeParent.RootID,
						",ParentPath='",
						text,
						"' WHERE AutoID =",
						Convert.ToInt32(safeDataReader.GetInt32("AutoID")),
						";"
					}));
					CategoryInfo dataById = Category.GetDataById(safeDataReader.GetInt32("AutoID"));
					if (dataById.ChildCount > 0)
					{
						Category.UpdateChildList(dataById, intDepthCha);
					}
				}
			}
		}

		public static bool ExistsProduct(int intCateID)
		{
			return BizBase.dbo.GetValue<int>(" SELECT TOP 1 1 FROM shop_Product WHERE CateID=" + intCateID) > 0;
		}

		public static IList<CategoryInfo> GetCacheCategoryList()
		{
			IList<CategoryInfo> list = (IList<CategoryInfo>)CacheUtils.Get("JsonLeeCMS_CacheForProCATEGORY");
			if (list == null)
			{
				list = BizBase.dbo.GetList<CategoryInfo>(" SELECT *,GoodsCount=(select COUNT(*) from shop_Product where CateID in (select * from dbo.f_System_Split(ChildList,',')) and [Status]=99)  FROM shop_Category ORDER BY Sort asc,Depth asc,RootID asc,AutoID DESC ");
				CacheUtils.Insert("JsonLeeCMS_CacheForProCATEGORY", list);
			}
			return list;
		}

		public static IList<CategoryInfo> GetCacheCateListByRootId(int intRootID)
		{
			IList<CategoryInfo> cacheCategoryList = Category.GetCacheCategoryList();
			IList<CategoryInfo> result;
			if (cacheCategoryList != null)
			{
				result = (from p in cacheCategoryList
				where p.RootID.Equals(intRootID)
				select p).ToList<CategoryInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<CategoryInfo> GetCacheAllChildCate(int intCateID)
		{
			CategoryInfo categoryInfo = null;
			IList<CategoryInfo> list = new List<CategoryInfo>();
			IList<CategoryInfo> cacheCategoryList = Category.GetCacheCategoryList();
			if (cacheCategoryList != null)
			{
				categoryInfo = (from p in cacheCategoryList
				where p.AutoID.Equals(intCateID)
				select p).FirstOrDefault<CategoryInfo>();
			}
			if (categoryInfo != null)
			{
				string[] array = categoryInfo.ChildList.Split(new char[]
				{
					','
				});
				foreach (CategoryInfo current in cacheCategoryList)
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string a = array2[i];
						if (a == current.AutoID.ToString())
						{
							list.Add(current);
						}
					}
				}
			}
			return list;
		}

		public static IList<CategoryInfo> GetCacheChildCate(int intParentID)
		{
			IList<CategoryInfo> cacheCategoryList = Category.GetCacheCategoryList();
			IList<CategoryInfo> result;
			if (cacheCategoryList != null)
			{
				result = (from p in cacheCategoryList
				where p.ParentID.Equals(intParentID)
				select p).ToList<CategoryInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static IList<CategoryInfo> GetCacheCates(string strCateIDs)
		{
			IList<CategoryInfo> cacheCategoryList = Category.GetCacheCategoryList();
			IList<CategoryInfo> list = new List<CategoryInfo>();
			string[] array = strCateIDs.Split(new char[]
			{
				','
			});
			if (array.Length > 0)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string value = array2[i];
					int @int = WebUtils.GetInt(value);
					foreach (CategoryInfo current in cacheCategoryList)
					{
						if (current.AutoID == @int)
						{
							list.Add(current);
						}
					}
				}
			}
			return list;
		}

		public static CategoryInfo GetCacheCategoryByID(int intCateID)
		{
			return (intCateID == 0) ? null : (from p in Category.GetCacheCategoryList()
			where p.AutoID.Equals(intCateID)
			select p).FirstOrDefault<CategoryInfo>();
		}

		public static CategoryInfo GetCacheCategoryByCode(string strCode)
		{
			return string.IsNullOrEmpty(strCode) ? null : (from p in Category.GetCacheCategoryList()
			where p.UrlRewriteName.Equals(strCode)
			select p).FirstOrDefault<CategoryInfo>();
		}

		public static CategoryInfo GetDefaultCate()
		{
			return BizBase.dbo.GetModel<CategoryInfo>(" SELECT TOP 1 * FROM shop_Category WHERE ParentID=0 ORDER BY Sort ASC,AutoID DESC ");
		}

		public static List<CategoryInfo> GetCateTreeList()
		{
			List<CategoryInfo> obj = (List<CategoryInfo>)Category.GetCacheCategoryList();
			List<CategoryInfo> list = JObject.DeepClone<List<CategoryInfo>>(obj);
			return Category.GetRelationNodeList(list, 0);
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

		public static List<CategoryInfo> GetRelationNodeList(List<CategoryInfo> list, int intParentID)
		{
			List<CategoryInfo> list2 = list.FindAll((CategoryInfo parameterA) => parameterA.ParentID == intParentID);
			List<CategoryInfo> list3 = new List<CategoryInfo>();
			int num = 0;
			foreach (CategoryInfo current in list2)
			{
				if (num == list2.Count - 1)
				{
					current.CategoryName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, true)) + current.CategoryName;
				}
				else
				{
					current.CategoryName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, false)) + current.CategoryName;
				}
				list3.Add(current);
				if (current.ChildCount > 0)
				{
					list3.AddRange(Category.GetRelationNodeList(list, current.AutoID));
				}
				num++;
			}
			return list3;
		}

		public static void CateMove(CategoryInfo nodeSource, int intTargetID)
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
				CategoryInfo cacheCategoryByID = Category.GetCacheCategoryByID(intTargetID);
				nodeSource.ParentID = cacheCategoryByID.AutoID;
				nodeSource.ParentPath = cacheCategoryByID.ParentPath + "," + cacheCategoryByID.AutoID.ToString();
				nodeSource.RootID = cacheCategoryByID.RootID;
				nodeSource.Depth = cacheCategoryByID.Depth + 1;
			}
			CategoryInfo dataById = Category.GetDataById(nodeSource.AutoID);
			if (dataById.ParentID > 0)
			{
				CategoryInfo dataById2 = Category.GetDataById(dataById.ParentID);
				dataById2.ChildCount--;
				BizBase.dbo.UpdateModel<CategoryInfo>(dataById2);
				Category.UpdateOrigleParents(dataById);
			}
			if (nodeSource.ParentPath != "0")
			{
				CategoryInfo dataById3 = Category.GetDataById(nodeSource.ParentID);
				dataById3.ChildCount++;
				BizBase.dbo.UpdateModel<CategoryInfo>(dataById3);
				Category.UpdateNowParents(nodeSource, "Modify");
			}
			int depth = dataById.Depth;
			int depth2 = nodeSource.Depth;
			int intDepthCha = depth2 - depth;
			Category.UpdateChildList(nodeSource, intDepthCha);
			BizBase.dbo.UpdateModel<CategoryInfo>(nodeSource);
			CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
		}
	}
}
