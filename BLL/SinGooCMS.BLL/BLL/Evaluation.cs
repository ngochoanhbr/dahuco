using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Evaluation : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Evaluation ");
			}
		}

		public static int Add(EvaluationInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<EvaluationInfo>(entity);
			}
			return result;
		}

		public static bool Update(EvaluationInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<EvaluationInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Evaluation WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Evaluation WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static EvaluationInfo GetDataById(int intPrimaryKeyIDValue)
		{
			EvaluationInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<EvaluationInfo>(" SELECT TOP 1 AutoID,OrderID,ProID,ProName,UserID,UserName,Start,Content,ReplyContent,Replier,ReplyTime,IsAudit,Lang,AutoTimeStamp FROM shop_Evaluation WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static EvaluationInfo GetTopData()
		{
			return Evaluation.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static EvaluationInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,OrderID,ProID,ProName,UserID,UserName,Start,Content,ReplyContent,Replier,ReplyTime,IsAudit,Lang,AutoTimeStamp FROM shop_Evaluation ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<EvaluationInfo>(text);
		}

		public static IList<EvaluationInfo> GetAllList()
		{
			return Evaluation.GetList(0, string.Empty);
		}

		public static IList<EvaluationInfo> GetTopNList(int intTopCount)
		{
			return Evaluation.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<EvaluationInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Evaluation.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<EvaluationInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Evaluation.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<EvaluationInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,OrderID,ProID,ProName,UserID,UserName,Start,Content,ReplyContent,Replier,ReplyTime,IsAudit,Lang,AutoTimeStamp from shop_Evaluation ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<EvaluationInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Evaluation", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Evaluation", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Evaluation.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Evaluation.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Evaluation.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Evaluation.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,OrderID,ProID,ProName,UserID,UserName,Start,Content,ReplyContent,Replier,ReplyTime,IsAudit,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Evaluation";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<EvaluationInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Evaluation.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<EvaluationInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<EvaluationInfo> result = new List<EvaluationInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,OrderID,ProID,ProName,UserID,UserName,Start,Content,ReplyContent,Replier,ReplyTime,IsAudit,Lang,AutoTimeStamp";
			pager.PagerTable = "shop_Evaluation";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<EvaluationInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Evaluation SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Evaluation SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<EvaluationInfo> GetEvaByProID(int intProID)
		{
			return BizBase.dbo.GetList<EvaluationInfo>(" SELECT * FROM shop_Evaluation WHERE IsAudit=1 AND ProID=" + intProID + " Order by AutoID desc ");
		}

		public static IList<EvaluationInfo> GetEvaByUID(int intUserID)
		{
			return BizBase.dbo.GetList<EvaluationInfo>(" SELECT * FROM shop_Evaluation WHERE IsAudit=1 AND UserID=" + intUserID + " Order by AutoID desc ");
		}

		public static void Show(string strIDs)
		{
			string strSQL = " UPDATE shop_Evaluation SET IsAudit=1 WHERE AutoID IN (" + strIDs + ") ";
			BizBase.dbo.ExecSQL(strSQL);
		}

		public static void Hide(string strIDs)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Evaluation SET IsAudit=0 WHERE AutoID IN (" + strIDs + ") ");
		}

		public static bool HasPJ(int intOrderID, int intProID, int intUserID)
		{
			return BizBase.dbo.GetValue<int>(string.Concat(new object[]
			{
				" SELECT count(*) FROM shop_Evaluation WHERE OrderID=",
				intOrderID,
				" AND ProID=",
				intProID,
				" AND UserID=",
				intUserID,
				" "
			})) > 0;
		}

		public static EvaStatInfo GetEvaSTAT(int proID)
		{
			EvaStatInfo evaStatInfo = new EvaStatInfo();
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@proid", proID)
			};
			IDataReader dataReader = BizBase.dbo.ExecProcReReader("p_System_GoodsEvaStat", arrParam);
			if (dataReader != null)
			{
				while (dataReader.Read())
				{
					evaStatInfo.ZongPing = WebUtils.GetInt(dataReader["总评数"]);
					evaStatInfo.HaoPing = WebUtils.GetInt(dataReader["好评数"]);
					evaStatInfo.ZhongPing = WebUtils.GetInt(dataReader["中评数"]);
					evaStatInfo.ChaPing = WebUtils.GetInt(dataReader["差评数"]);
					evaStatInfo.HaoPingDu = WebUtils.GetDecimal(dataReader["好评度"]);
					evaStatInfo.ZhongPingDu = WebUtils.GetDecimal(dataReader["中评度"]);
					evaStatInfo.ChaPingDu = WebUtils.GetDecimal(dataReader["差评度"]);
				}
				dataReader.Close();
				dataReader.Dispose();
			}
			return evaStatInfo;
		}
	}
}
