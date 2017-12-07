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
	public class GoodsClass : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_GoodsClass ");
			}
		}

		public static int AddExt(GoodsClassInfo entity)
		{
			int num = GoodsClass.Add(entity);
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
					GoodsClassInfo dataById = GoodsClass.GetDataById(entity.ParentID);
					if (dataById != null)
					{
						entity.RootID = dataById.RootID;
						entity.Depth = dataById.Depth + 1;
						entity.ChildCount = 0;
						dataById.ChildCount++;
						GoodsClass.Update(dataById);
					}
				}
				entity.AutoID = num;
				GoodsClass.Update(entity);
			}
			return num;
		}

		public static bool UpdateExt(GoodsClassInfo entity)
		{
			return false;
		}

		public static bool DeleteExt(int intPrimaryKeyIDValue)
		{
			GoodsClassInfo dataById = GoodsClass.GetDataById(intPrimaryKeyIDValue);
			bool result;
			if (dataById != null)
			{
				if (GoodsClass.Delete(intPrimaryKeyIDValue))
				{
					BizBase.dbo.UpdateTable("update shop_GoodsClass set ChildCount=ChildCount-1 where ChildCount>0 AND AutoID=" + dataById.ParentID);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static List<GoodsClassInfo> GetCateTreeList()
		{
			List<GoodsClassInfo> obj = (List<GoodsClassInfo>)GoodsClass.GetAllList();
			List<GoodsClassInfo> list = JObject.DeepClone<List<GoodsClassInfo>>(obj);
			return GoodsClass.GetRelationList(list, 0);
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

		public static List<GoodsClassInfo> GetRelationList(List<GoodsClassInfo> list, int intParentID)
		{
			List<GoodsClassInfo> list2 = list.FindAll((GoodsClassInfo parameterA) => parameterA.ParentID == intParentID);
			List<GoodsClassInfo> list3 = new List<GoodsClassInfo>();
			int num = 0;
			foreach (GoodsClassInfo current in list2)
			{
				if (num == list2.Count - 1)
				{
					current.ClassName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, true)) + current.ClassName;
				}
				else
				{
					current.ClassName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, false)) + current.ClassName;
				}
				list3.Add(current);
				if (current.ChildCount > 0)
				{
					list3.AddRange(GoodsClass.GetRelationList(list, current.AutoID));
				}
				num++;
			}
			return list3;
		}

		public static GoodsClassInfo Get(int proID)
		{
			return BizBase.dbo.GetModel<GoodsClassInfo>(" select * from shop_GoodsClass where AutoID=(select top 1 ClassID from shop_Product where AutoID=" + proID + ") ");
		}

		public static int Add(GoodsClassInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<GoodsClassInfo>(entity);
			}
			return result;
		}

		public static bool Update(GoodsClassInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<GoodsClassInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_GoodsClass WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_GoodsClass WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static GoodsClassInfo GetDataById(int intPrimaryKeyIDValue)
		{
			GoodsClassInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<GoodsClassInfo>(" SELECT TOP 1 AutoID,ClassName,ParentID,Depth,RootID,ChildCount,GuiGeCollection,Sort,Lang,Creator,AutoTimeStamp FROM shop_GoodsClass WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static GoodsClassInfo GetTopData()
		{
			return GoodsClass.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static GoodsClassInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ClassName,ParentID,Depth,RootID,ChildCount,GuiGeCollection,Sort,Lang,Creator,AutoTimeStamp FROM shop_GoodsClass ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<GoodsClassInfo>(text);
		}

		public static IList<GoodsClassInfo> GetAllList()
		{
			return GoodsClass.GetList(0, string.Empty);
		}

		public static IList<GoodsClassInfo> GetTopNList(int intTopCount)
		{
			return GoodsClass.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<GoodsClassInfo> GetTopNList(int intTopCount, string strSort)
		{
			return GoodsClass.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<GoodsClassInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return GoodsClass.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<GoodsClassInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ClassName,ParentID,Depth,RootID,ChildCount,GuiGeCollection,Sort,Lang,Creator,AutoTimeStamp from shop_GoodsClass ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<GoodsClassInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_GoodsClass", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_GoodsClass", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return GoodsClass.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsClass.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsClass.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsClass.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ClassName,ParentID,Depth,RootID,ChildCount,GuiGeCollection,Sort,Lang,Creator,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_GoodsClass";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<GoodsClassInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return GoodsClass.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<GoodsClassInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<GoodsClassInfo> result = new List<GoodsClassInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ClassName,ParentID,Depth,RootID,ChildCount,GuiGeCollection,Sort,Lang,Creator,AutoTimeStamp";
			pager.PagerTable = "shop_GoodsClass";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<GoodsClassInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_GoodsClass SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_GoodsClass SET Sort =",
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
