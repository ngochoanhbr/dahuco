using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class AccountDetail : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM cms_AccountDetail ");
			}
		}

		public static bool AddAmount(UserInfo user, int intOpType, double douOpValue, string strRemark)
		{
			bool result;
			if (douOpValue > 0.0)
			{
				AccountDetailInfo entity = new AccountDetailInfo
				{
					UserID = user.AutoID,
					UserName = user.UserName,
					Unit = "Amount",
					Before = (double)user.Amount,
					OpValue = douOpValue,
					OpType = intOpType,
					After = ((intOpType == 1) ? ((double)user.Amount + douOpValue) : ((double)user.Amount - douOpValue)),
					Remark = strRemark,
					Operator = user.UserName,
					OperateDate = DateTime.Now
				};
				result = (AccountDetail.Add(entity) > 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool AddIntegral(UserInfo user, int intOpType, double douOpValue, string strRemark)
		{
			bool result;
			if (douOpValue > 0.0)
			{
				AccountDetailInfo entity = new AccountDetailInfo
				{
					UserID = user.AutoID,
					UserName = user.UserName,
					Unit = "Integral",
					Before = (double)user.Integral,
					OpValue = douOpValue,
					OpType = intOpType,
					After = ((intOpType == 1) ? ((double)user.Integral + douOpValue) : ((double)user.Integral - douOpValue)),
					Remark = strRemark,
					Operator = user.UserName,
					OperateDate = DateTime.Now
				};
				result = (AccountDetail.Add(entity) > 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static int Add(AccountDetailInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<AccountDetailInfo>(entity);
			}
			return result;
		}

		public static bool Update(AccountDetailInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<AccountDetailInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM cms_AccountDetail WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM cms_AccountDetail WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static AccountDetailInfo GetDataById(int intPrimaryKeyIDValue)
		{
			AccountDetailInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<AccountDetailInfo>(" SELECT AutoID,UserID,UserName,Unit,Before,OpValue,OpType,After,Remark,Operator,OperateDate FROM cms_AccountDetail WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static AccountDetailInfo GetTopData()
		{
			return AccountDetail.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static AccountDetailInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,UserID,UserName,Unit,Before,OpValue,OpType,After,Remark,Operator,OperateDate FROM cms_AccountDetail ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<AccountDetailInfo>(text);
		}

		public static IList<AccountDetailInfo> GetAllList()
		{
			return AccountDetail.GetList(0, string.Empty);
		}

		public static IList<AccountDetailInfo> GetTopNList(int intTopCount)
		{
			return AccountDetail.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<AccountDetailInfo> GetTopNList(int intTopCount, string strSort)
		{
			return AccountDetail.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<AccountDetailInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return AccountDetail.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<AccountDetailInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,UserID,UserName,Unit,Before,OpValue,OpType,After,Remark,Operator,OperateDate from cms_AccountDetail ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<AccountDetailInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_AccountDetail", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "cms_AccountDetail", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return AccountDetail.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AccountDetail.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AccountDetail.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return AccountDetail.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,UserID,UserName,Unit,Before,OpValue,OpType,After,Remark,Operator,OperateDate";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "cms_AccountDetail";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<AccountDetailInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return AccountDetail.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<AccountDetailInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<AccountDetailInfo> result = new List<AccountDetailInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,UserID,UserName,Unit,Before,OpValue,OpType,After,Remark,Operator,OperateDate";
			pager.PagerTable = "cms_AccountDetail";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<AccountDetailInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE cms_AccountDetail SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE cms_AccountDetail SET Sort =",
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
