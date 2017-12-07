using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class GuiGePic : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_GuiGePic ");
			}
		}

		public static GuiGePicInfo GetByProID(int proID)
		{
			return BizBase.dbo.GetModel<GuiGePicInfo>(" select top 1 * from shop_GuiGePic where ProID=" + proID);
		}

		public static void DelByProID(int proID)
		{
			BizBase.dbo.DeleteTable("delete from shop_GuiGePic where ProID=" + proID);
		}

		public static int Add(GuiGePicInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<GuiGePicInfo>(entity);
			}
			return result;
		}

		public static bool Update(GuiGePicInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<GuiGePicInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_GuiGePic WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_GuiGePic WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static GuiGePicInfo GetDataById(int intPrimaryKeyIDValue)
		{
			GuiGePicInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<GuiGePicInfo>(" SELECT TOP 1 AutoID,ProID,ClassID,GuiGeName,ImagesCollection,AutoTimeStamp FROM shop_GuiGePic WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static GuiGePicInfo GetTopData()
		{
			return GuiGePic.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static GuiGePicInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ProID,ClassID,GuiGeName,ImagesCollection,AutoTimeStamp FROM shop_GuiGePic ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<GuiGePicInfo>(text);
		}

		public static IList<GuiGePicInfo> GetAllList()
		{
			return GuiGePic.GetList(0, string.Empty);
		}

		public static IList<GuiGePicInfo> GetTopNList(int intTopCount)
		{
			return GuiGePic.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<GuiGePicInfo> GetTopNList(int intTopCount, string strSort)
		{
			return GuiGePic.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<GuiGePicInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return GuiGePic.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<GuiGePicInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ProID,ClassID,GuiGeName,ImagesCollection,AutoTimeStamp from shop_GuiGePic ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<GuiGePicInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_GuiGePic", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_GuiGePic", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return GuiGePic.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GuiGePic.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GuiGePic.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return GuiGePic.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ProID,ClassID,GuiGeName,ImagesCollection,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_GuiGePic";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<GuiGePicInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return GuiGePic.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<GuiGePicInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<GuiGePicInfo> result = new List<GuiGePicInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ProID,ClassID,GuiGeName,ImagesCollection,AutoTimeStamp";
			pager.PagerTable = "shop_GuiGePic";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<GuiGePicInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_GuiGePic SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_GuiGePic SET Sort =",
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
