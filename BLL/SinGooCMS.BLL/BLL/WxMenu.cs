using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class WxMenu : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM weixin_WxMenu ");
			}
		}

		public static void EmptyLocal()
		{
			BizBase.dbo.ExecSQL(" truncate table weixin_WxMenu ");
		}

		public static WxStatus Add(WxMenuInfo entity, AutoRlyInfo eventKey)
		{
			int count;
			if (entity.ParentID == 0)
			{
				count = WxMenu.GetCount("ParentID=0");
			}
			else
			{
				count = WxMenu.GetCount("ParentID=" + entity.ParentID);
			}
			WxStatus result;
			if (entity.ParentID.Equals(0) && count > 3)
			{
				result = WxStatus.一级菜单个数不超过3个;
			}
			else if (entity.ParentID > 0 && count > 5)
			{
				result = WxStatus.二级菜单个数不超过5个;
			}
			else
			{
				if (entity.Type == "click" && eventKey != null)
				{
					AutoRly.Add(eventKey);
					entity.EventKey = eventKey.MsgKey;
				}
				int num = WxMenu.Add(entity);
				if (num > 0)
				{
					entity.AutoID = num;
					if (entity.ParentID.Equals(0))
					{
						entity.RootID = num;
						WxMenu.Update(entity);
					}
					else
					{
						WxMenuInfo dataById = WxMenu.GetDataById(entity.ParentID);
						entity.RootID = dataById.RootID;
						dataById.ChildCount++;
						dataById.ChildIDs = dataById.ChildIDs + "," + num;
						WxMenu.Update(entity);
						WxMenu.Update(dataById);
					}
					result = WxStatus.增加成功;
				}
				else
				{
					result = WxStatus.增加失败;
				}
			}
			return result;
		}

		public static WxStatus Update(WxMenuInfo entity, AutoRlyInfo eventKey)
		{
			WxMenuInfo dataById = WxMenu.GetDataById(entity.AutoID);
			WxStatus result;
			if (WxMenu.Update(entity))
			{
				if (entity.Type == "view" && !string.IsNullOrEmpty(dataById.EventKey))
				{
					AutoRly.DelEventKey(dataById.EventKey);
				}
				else if (entity.Type == "click" && dataById.Type != "click" && eventKey != null)
				{
					AutoRly.Add(eventKey);
				}
				else if (entity.Type == "click" && dataById.Type == "click" && eventKey != null)
				{
					AutoRlyInfo eventRly = AutoRly.GetEventRly(dataById.EventKey);
					if (eventRly != null)
					{
						eventRly.MsgText = eventKey.MsgText;
						eventRly.Description = eventKey.Description;
						eventRly.MediaPath = eventKey.MediaPath;
						eventRly.LinkUrl = eventKey.LinkUrl;
						eventRly.AutoTimeStamp = DateTime.Now;
						AutoRly.Update(eventRly);
					}
				}
				result = WxStatus.修改成功;
			}
			else
			{
				result = WxStatus.修改失败;
			}
			return result;
		}

		public static bool Del(int intID)
		{
			WxMenuInfo dataById = WxMenu.GetDataById(intID);
			bool result;
			if (dataById.ChildCount > 0)
			{
				result = false;
			}
			else if (BizBase.dbo.DeleteModel<WxMenuInfo>(dataById))
			{
				if (dataById.ParentID > 0)
				{
					BizBase.dbo.UpdateTable(string.Concat(new object[]
					{
						"update weixin_WxMenu set ChildCount=ChildCount-1,ChildIDs=REPLACE(ChildIDs,',",
						intID,
						"','') where AutoID=",
						dataById.ParentID
					}));
				}
				if (!string.IsNullOrEmpty(dataById.EventKey))
				{
					AutoRly.DelEventKey(dataById.EventKey);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static int Add(WxMenuInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<WxMenuInfo>(entity);
			}
			return result;
		}

		public static bool Update(WxMenuInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<WxMenuInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM weixin_WxMenu WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM weixin_WxMenu WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static WxMenuInfo GetDataById(int intPrimaryKeyIDValue)
		{
			WxMenuInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<WxMenuInfo>(" SELECT TOP 1 AutoID,RootID,ParentID,Type,Name,EventKey,Url,ChildCount,ChildIDs,Sort,AutoTimeStamp FROM weixin_WxMenu WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static WxMenuInfo GetTopData()
		{
			return WxMenu.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static WxMenuInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,RootID,ParentID,Type,Name,EventKey,Url,ChildCount,ChildIDs,Sort,AutoTimeStamp FROM weixin_WxMenu ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<WxMenuInfo>(text);
		}

		public static IList<WxMenuInfo> GetAllList()
		{
			return WxMenu.GetList(0, string.Empty);
		}

		public static IList<WxMenuInfo> GetTopNList(int intTopCount)
		{
			return WxMenu.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<WxMenuInfo> GetTopNList(int intTopCount, string strSort)
		{
			return WxMenu.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<WxMenuInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return WxMenu.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<WxMenuInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,RootID,ParentID,Type,Name,EventKey,Url,ChildCount,ChildIDs,Sort,AutoTimeStamp from weixin_WxMenu ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<WxMenuInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "weixin_WxMenu", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "weixin_WxMenu", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return WxMenu.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return WxMenu.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return WxMenu.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return WxMenu.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,RootID,ParentID,Type,Name,EventKey,Url,ChildCount,ChildIDs,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "weixin_WxMenu";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<WxMenuInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return WxMenu.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<WxMenuInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<WxMenuInfo> result = new List<WxMenuInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,RootID,ParentID,Type,Name,EventKey,Url,ChildCount,ChildIDs,Sort,AutoTimeStamp";
			pager.PagerTable = "weixin_WxMenu";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<WxMenuInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE weixin_WxMenu SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE weixin_WxMenu SET Sort =",
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
