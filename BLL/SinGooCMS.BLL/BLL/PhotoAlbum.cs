using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class PhotoAlbum : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_PhotoAlbum ");
			}
		}

		public static int Add(PhotoAlbumInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<PhotoAlbumInfo>(entity);
			}
			return result;
		}

		public static bool Update(PhotoAlbumInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<PhotoAlbumInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_PhotoAlbum WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_PhotoAlbum WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static PhotoAlbumInfo GetDataById(int intPrimaryKeyIDValue)
		{
			PhotoAlbumInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<PhotoAlbumInfo>(" SELECT AutoID,ProID,ImgSrc,ImgThumbSrc,Remark,Sort,AutoTimeStamp FROM shop_PhotoAlbum WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static PhotoAlbumInfo GetTopData()
		{
			return PhotoAlbum.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static PhotoAlbumInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,ProID,ImgSrc,ImgThumbSrc,Remark,Sort,AutoTimeStamp FROM shop_PhotoAlbum ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<PhotoAlbumInfo>(text);
		}

		public static IList<PhotoAlbumInfo> GetAllList()
		{
			return PhotoAlbum.GetList(0, string.Empty);
		}

		public static IList<PhotoAlbumInfo> GetTopNList(int intTopCount)
		{
			return PhotoAlbum.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<PhotoAlbumInfo> GetTopNList(int intTopCount, string strSort)
		{
			return PhotoAlbum.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<PhotoAlbumInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return PhotoAlbum.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<PhotoAlbumInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,ProID,ImgSrc,ImgThumbSrc,Remark,Sort,AutoTimeStamp from shop_PhotoAlbum ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<PhotoAlbumInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_PhotoAlbum", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_PhotoAlbum", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return PhotoAlbum.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return PhotoAlbum.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return PhotoAlbum.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return PhotoAlbum.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,ProID,ImgSrc,ImgThumbSrc,Remark,Sort,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_PhotoAlbum";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<PhotoAlbumInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return PhotoAlbum.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<PhotoAlbumInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<PhotoAlbumInfo> result = new List<PhotoAlbumInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,ProID,ImgSrc,ImgThumbSrc,Remark,Sort,AutoTimeStamp";
			pager.PagerTable = "shop_PhotoAlbum";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<PhotoAlbumInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_PhotoAlbum SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_PhotoAlbum SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static IList<PhotoAlbumInfo> GetPhotoAlbumByPID(int intProID)
		{
			return BizBase.dbo.GetList<PhotoAlbumInfo>(" SELECT * FROM shop_PhotoAlbum WHERE ProID=" + intProID);
		}

		public static void AddPhotoList(List<PhotoAlbumInfo> listProPhoto)
		{
			StringBuilder stringBuilder = new StringBuilder(" INSERT INTO shop_PhotoAlbum (ProID,ImgSrc,ImgThumbSrc,Remark,Sort,AutoTimeStamp) ");
			for (int i = 0; i < listProPhoto.Count; i++)
			{
				if (i == listProPhoto.Count - 1)
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						" SELECT ",
						listProPhoto[i].ProID,
						",'",
						listProPhoto[i].ImgSrc,
						"','",
						listProPhoto[i].ImgThumbSrc,
						"','",
						listProPhoto[i].Remark,
						"',999,getdate() "
					}));
				}
				else
				{
					stringBuilder.Append(string.Concat(new object[]
					{
						" SELECT ",
						listProPhoto[i].ProID,
						",'",
						listProPhoto[i].ImgSrc,
						"','",
						listProPhoto[i].ImgThumbSrc,
						"','",
						listProPhoto[i].Remark,
						"',999,getdate()  union all "
					}));
				}
			}
			if (listProPhoto != null && listProPhoto.Count > 0)
			{
				BizBase.dbo.ExecSQL(stringBuilder.ToString().Trim());
			}
		}

		public static void DelPhotoByProID(int intProID)
		{
			BizBase.dbo.ExecSQL(" DELETE shop_PhotoAlbum WHERE ProID=" + intProID);
		}
	}
}
