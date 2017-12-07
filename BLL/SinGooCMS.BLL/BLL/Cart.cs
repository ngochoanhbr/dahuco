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
	public class Cart : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Cart ");
			}
		}

		public static int Add(CartInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<CartInfo>(entity);
			}
			return result;
		}

		public static bool Update(CartInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<CartInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Cart WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Cart WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static CartInfo GetDataById(int intPrimaryKeyIDValue)
		{
			CartInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<CartInfo>(" SELECT TOP 1 AutoID,CartNo,UserID,UserName,ProID,ProName,ProImg,Quantity,Price,SubTotal,SellType,GoodsAttr,GoodsAttrStr,Remark,AutoTimeStamp FROM shop_Cart WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static CartInfo GetTopData()
		{
			return Cart.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static CartInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,CartNo,UserID,UserName,ProID,ProName,ProImg,Quantity,Price,SubTotal,SellType,GoodsAttr,GoodsAttrStr,Remark,AutoTimeStamp FROM shop_Cart ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<CartInfo>(text);
		}

		public static IList<CartInfo> GetAllList()
		{
			return Cart.GetList(0, string.Empty);
		}

		public static IList<CartInfo> GetTopNList(int intTopCount)
		{
			return Cart.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<CartInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Cart.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<CartInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Cart.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<CartInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,CartNo,UserID,UserName,ProID,ProName,ProImg,Quantity,Price,SubTotal,SellType,GoodsAttr,GoodsAttrStr,Remark,AutoTimeStamp from shop_Cart ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<CartInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Cart", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Cart", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Cart.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Cart.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Cart.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Cart.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,CartNo,UserID,UserName,ProID,ProName,ProImg,Quantity,Price,SubTotal,SellType,GoodsAttr,GoodsAttrStr,Remark,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Cart";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<CartInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Cart.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<CartInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<CartInfo> result = new List<CartInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,CartNo,UserID,UserName,ProID,ProName,ProImg,Quantity,Price,SubTotal,SellType,GoodsAttr,GoodsAttrStr,Remark,AutoTimeStamp";
			pager.PagerTable = "shop_Cart";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<CartInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Cart SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Cart SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static void AddToCart(CartInfo entity)
		{
			List<SqlParameter> list = new List<SqlParameter>
			{
				new SqlParameter("@CartNo", entity.CartNo),
				new SqlParameter("@UserID", entity.UserID),
				new SqlParameter("@UserName", entity.UserName),
				new SqlParameter("@ProID", entity.ProID),
				new SqlParameter("@ProName", entity.ProName),
				new SqlParameter("@ProImg", entity.ProImg),
				new SqlParameter("@Quantity", entity.Quantity),
				new SqlParameter("@Price", entity.Price),
				new SqlParameter("@SellType", entity.SellType),
				new SqlParameter("@GoodsAttr", entity.GoodsAttr),
				new SqlParameter("@GoodsAttrStr", entity.GoodsAttrStr),
				new SqlParameter("@Remark", entity.Remark),
				new SqlParameter("@AutoTimeStamp", entity.AutoTimeStamp)
			};
			BizBase.dbo.ExecProcReValue("p_System_AddToCart", list.ToArray());
		}

		public static IList<CartInfo> GetListByCartNo(string strCartNo)
		{
			IList<CartInfo> list = BizBase.dbo.GetList<CartInfo>(" SELECT * FROM shop_Cart WHERE CartNo='" + strCartNo + "' ");
			foreach (CartInfo current in list)
			{
				current.Product = (Product.GetDataById(current.ProID) ?? new ProductInfo());
			}
			return list;
		}

		public static CartInfo GetCart(int intPrimaryID)
		{
			CartInfo dataById = Cart.GetDataById(intPrimaryID);
			if (dataById != null)
			{
				dataById.Product = (Product.GetDataById(dataById.ProID) ?? new ProductInfo());
			}
			return dataById;
		}

		public static CartInfo GetCart(string strCartNo, int intProID)
		{
			CartInfo model = BizBase.dbo.GetModel<CartInfo>(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM shop_Cart WHERE CartNo='",
				strCartNo,
				"' AND ProID=",
				intProID
			}));
			if (model != null)
			{
				model.Product = (Product.GetDataById(model.ProID) ?? new ProductInfo());
			}
			return model;
		}

		public static bool UpdateBatchQuantity(string strIDList, string strNumList)
		{
			string[] array = strIDList.Split(new char[]
			{
				','
			});
			string[] array2 = strNumList.Split(new char[]
			{
				','
			});
			bool result;
			if (array.Length == array2.Length)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.AppendFormat("UPDATE shop_Cart SET Quantity={0} WHERE AutoID={1}; ", array2[i], array[i]);
				}
				BizBase.dbo.ExecSQL(stringBuilder.ToString());
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static void ClearByCartID(string strCartNo)
		{
			BizBase.dbo.ExecSQL(" DELETE FROM shop_Cart WHERE CartNo='" + StringUtils.ChkSQL(strCartNo) + "' ");
		}
	}
}
