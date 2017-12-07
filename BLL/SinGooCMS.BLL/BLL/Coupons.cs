using SinGooCMS.Config;
using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Coupons : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Coupons ");
			}
		}

		public static string CreateSN()
		{
			SettingInfo settingInfo = ConfigProvider.Configs.Get("CouponsFmt");
			string result;
			if (settingInfo != null && !string.IsNullOrEmpty(settingInfo.KeyValue))
			{
				result = settingInfo.KeyValue.Replace("${year}", DateTime.Now.ToString("yyyy")).Replace("${month}", DateTime.Now.ToString("MM")).Replace("${day}", DateTime.Now.ToString("dd")).Replace("${hour}", DateTime.Now.ToString("HH")).Replace("${minute}", DateTime.Now.ToString("mm")).Replace("${second}", DateTime.Now.ToString("ss")).Replace("${millisecond}", DateTime.Now.ToString("ffff")).Replace("${rnd}", StringUtils.GetRandomNumber(5, false));
			}
			else
			{
				result = DateTime.Now.ToString("yyyyMMddHHffff") + StringUtils.GetRandomNumber(5, false);
			}
			return result;
		}

		public static bool Copy(CouponsInfo entity)
		{
			CouponsInfo entity2 = new CouponsInfo
			{
				Title = entity.Title,
				SN = Coupons.CreateSN(),
				Notes = entity.Notes,
				Touch = entity.Touch,
				StartTime = entity.StartTime,
				EndTime = entity.EndTime,
				UserName = string.Empty,
				IsUsed = false,
				Sort = 999,
				Lang = JObject.cultureLang,
				AutoTimeStamp = DateTime.Now
			};
			bool flag;
			if (Coupons.ExistsSN(entity.SN))
			{
				do
				{
					entity.SN = Coupons.CreateSN();
				}
				while (Coupons.ExistsSN(entity.SN));
				flag = true;
			}
			else
			{
				flag = true;
			}
			return flag && Coupons.Add(entity2) > 0;
		}

		public static bool ExistsSN(string strSN)
		{
			return Coupons.GetCount(" SN='" + StringUtils.ChkSQL(strSN) + "' ") > 0;
		}

		public static CouponsInfo GetDataBySN(string strSN)
		{
			return BizBase.dbo.GetModel<CouponsInfo>(" SELECT TOP 1 * FROM shop_Coupons WHERE SN='" + StringUtils.ChkSQL(strSN) + "'  ");
		}

		public static IList<CouponsInfo> GetValidCoup(string strUserName)
		{
			return BizBase.dbo.GetList<CouponsInfo>(" select * from shop_Coupons where UserName='" + strUserName + "' AND IsUsed=0 AND GETDATE() between StartTime and EndTime ");
		}

		public static int Add(CouponsInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<CouponsInfo>(entity);
			}
			return result;
		}

		public static bool Update(CouponsInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<CouponsInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Coupons WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Coupons WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static CouponsInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CouponsInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CouponsInfo>(" SELECT TOP 1 AutoID,Title,SN,Notes,Touch,StartTime,EndTime,UserName,IsUsed,Sort,Lang,AutoTimeStamp FROM shop_Coupons WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CouponsInfo GetTopData()
		{
			return Coupons.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CouponsInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,Title,SN,Notes,Touch,StartTime,EndTime,UserName,IsUsed,Sort,Lang,AutoTimeStamp FROM shop_Coupons ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CouponsInfo>(text);
		}

		public static IList<CouponsInfo> GetAllList()
		{
			return Coupons.GetList(0, string.Empty);
		}

		public static IList<CouponsInfo> GetTopNList(int intTopCount)
		{
			return Coupons.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CouponsInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Coupons.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CouponsInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Coupons.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CouponsInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,Title,SN,Notes,Touch,StartTime,EndTime,UserName,IsUsed,Sort,Lang,AutoTimeStamp from shop_Coupons ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CouponsInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Coupons", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Coupons", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Coupons.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Coupons.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Coupons.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Coupons.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,Title,SN,Notes,Touch,StartTime,EndTime,UserName,IsUsed,Sort,Lang,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Coupons";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CouponsInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Coupons.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CouponsInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CouponsInfo> result = new List<CouponsInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,Title,SN,Notes,Touch,StartTime,EndTime,UserName,IsUsed,Sort,Lang,AutoTimeStamp";
			pager.PagerTable = "shop_Coupons";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CouponsInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Coupons SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Coupons SET Sort =",
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
