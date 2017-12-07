using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Operate : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Operate ");
			}
		}

		public static IList<OperateInfo> GetOperateListBySID(string strSourceCode)
		{
			return BizBase.dbo.GetList<OperateInfo>(" SELECT * FROM sys_Operate WHERE SourceCode='" + strSourceCode + "' ");
		}

		public static IList<OperateInfo> GetOperateListByMID(int intModuleID)
		{
			return BizBase.dbo.GetList<OperateInfo>(" SELECT * FROM sys_Operate WHERE ModuleID=" + intModuleID);
		}

		public static bool AddDefaultOperate(int intModuleID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT sys_Operate\r\n                        (\r\n\t                        ModuleID,\r\n\t                        OperateName,\r\n\t                        OperateCode\r\n                        )\r\n                        ");
			stringBuilder.Append(string.Concat(new object[]
			{
				" select ",
				intModuleID,
				",'查看','",
				ActionType.View.ToString(),
				"' union all "
			}));
			stringBuilder.Append(string.Concat(new object[]
			{
				" select ",
				intModuleID,
				",'增加','",
				ActionType.Add.ToString(),
				"' union all "
			}));
			stringBuilder.Append(string.Concat(new object[]
			{
				" select ",
				intModuleID,
				",'修改','",
				ActionType.Modify.ToString(),
				"' union all "
			}));
			stringBuilder.Append(string.Concat(new object[]
			{
				" select ",
				intModuleID,
				",'删除','",
				ActionType.Delete.ToString(),
				"' "
			}));
			return BizBase.dbo.InsertNoReValue(stringBuilder.ToString());
		}

		public static DataTable GetOperateRelation()
		{
			return BizBase.dbo.ExecProcReDT("p_System_GetModuleAndOperate", null);
		}

		public static int Add(OperateInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<OperateInfo>(entity);
			}
			return result;
		}

		public static bool Update(OperateInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<OperateInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Operate WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Operate WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static OperateInfo GetDataById(int intPrimaryKeyIDValue)
		{
			OperateInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<OperateInfo>(" SELECT TOP 1 AutoID,ModuleID,OperateName,OperateCode,Remark,Sort,AutoTimeStamp FROM sys_Operate WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static OperateInfo GetTopData()
		{
			return Operate.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static OperateInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ModuleID,OperateName,OperateCode,Remark,Sort,AutoTimeStamp FROM sys_Operate ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<OperateInfo>(text);
		}

		public static IList<OperateInfo> GetAllList()
		{
			return Operate.GetList(0, string.Empty);
		}

		public static IList<OperateInfo> GetTopNList(int intTopCount)
		{
			return Operate.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<OperateInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Operate.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<OperateInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Operate.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<OperateInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ModuleID,OperateName,OperateCode,Remark,Sort,AutoTimeStamp from sys_Operate ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<OperateInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Operate", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Operate", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Operate.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Operate.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Operate.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Operate.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ModuleID,OperateName,OperateCode,Remark,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Operate";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<OperateInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Operate.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<OperateInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<OperateInfo> result = new List<OperateInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ModuleID,OperateName,OperateCode,Remark,Sort,AutoTimeStamp";
			pager.PagerTable = "sys_Operate";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<OperateInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Operate SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE sys_Operate SET Sort =",
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
