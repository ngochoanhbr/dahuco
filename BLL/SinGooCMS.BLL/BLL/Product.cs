using SinGooCMS.Config;
using SinGooCMS.DAL;
using SinGooCMS.DAL.Utils;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Product : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM shop_Product ");
			}
		}

		public static int Add(ProductInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<ProductInfo>(entity);
			}
			return result;
		}

		public static bool Update(ProductInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<ProductInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM shop_Product WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM shop_Product WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static ProductInfo GetDataById(int intPrimaryKeyIDValue)
		{
			ProductInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<ProductInfo>(" SELECT TOP 1 AutoID,CateID,BrandID,ModelID,SellType,ProductName,ProductSN,ProImg,ProIDs,Stock,AlarmNum,MarketPrice,SellPrice,MemberPriceSet,Unit,BuyIntegral,GiveIntegral,IsExchange,BuyLimit,ClassID,AreaModelID,PostageModelID,Manufacturer,Specifications,ModelNum,Size,Weight,Material,Color,ProducingArea,ShortDesc,ProDetail,RelatedProduct,ActiveName,Tags,SEOKey,SEODescription,IsBooking,IsVirtual,IsTop,IsRecommend,IsNew,Sales,Sort,Hit,Lang,Status,AutoTimeStamp FROM shop_Product WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static ProductInfo GetTopData()
		{
			return Product.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static ProductInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,CateID,BrandID,ModelID,SellType,ProductName,ProductSN,ProImg,ProIDs,Stock,AlarmNum,MarketPrice,SellPrice,MemberPriceSet,Unit,BuyIntegral,GiveIntegral,IsExchange,BuyLimit,ClassID,AreaModelID,PostageModelID,Manufacturer,Specifications,ModelNum,Size,Weight,Material,Color,ProducingArea,ShortDesc,ProDetail,RelatedProduct,ActiveName,Tags,SEOKey,SEODescription,IsBooking,IsVirtual,IsTop,IsRecommend,IsNew,Sales,Sort,Hit,Lang,Status,AutoTimeStamp FROM shop_Product ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<ProductInfo>(text);
		}

		public static IList<ProductInfo> GetAllList()
		{
			return Product.GetList(0, string.Empty);
		}

		public static IList<ProductInfo> GetTopNList(int intTopCount)
		{
			return Product.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<ProductInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Product.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<ProductInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Product.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<ProductInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,CateID,BrandID,ModelID,SellType,ProductName,ProductSN,ProImg,ProIDs,Stock,AlarmNum,MarketPrice,SellPrice,MemberPriceSet,Unit,BuyIntegral,GiveIntegral,IsExchange,BuyLimit,ClassID,AreaModelID,PostageModelID,Manufacturer,Specifications,ModelNum,Size,Weight,Material,Color,ProducingArea,ShortDesc,ProDetail,RelatedProduct,ActiveName,Tags,SEOKey,SEODescription,IsBooking,IsVirtual,IsTop,IsRecommend,IsNew,Sales,Sort,Hit,Lang,Status,AutoTimeStamp from shop_Product ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<ProductInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Product", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "shop_Product", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Product.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Product.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Product.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Product.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,CateID,BrandID,ModelID,SellType,ProductName,ProductSN,ProImg,ProIDs,Stock,AlarmNum,MarketPrice,SellPrice,MemberPriceSet,Unit,BuyIntegral,GiveIntegral,IsExchange,BuyLimit,ClassID,AreaModelID,PostageModelID,Manufacturer,Specifications,ModelNum,Size,Weight,Material,Color,ProducingArea,ShortDesc,ProDetail,RelatedProduct,ActiveName,Tags,SEOKey,SEODescription,IsBooking,IsVirtual,IsTop,IsRecommend,IsNew,Sales,Sort,Hit,Lang,Status,AutoTimeStamp";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "shop_Product";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<ProductInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Product.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<ProductInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<ProductInfo> result = new List<ProductInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,CateID,BrandID,ModelID,SellType,ProductName,ProductSN,ProImg,ProIDs,Stock,AlarmNum,MarketPrice,SellPrice,MemberPriceSet,Unit,BuyIntegral,GiveIntegral,IsExchange,BuyLimit,ClassID,AreaModelID,PostageModelID,Manufacturer,Specifications,ModelNum,Size,Weight,Material,Color,ProducingArea,ShortDesc,ProDetail,RelatedProduct,ActiveName,Tags,SEOKey,SEODescription,IsBooking,IsVirtual,IsTop,IsRecommend,IsNew,Sales,Sort,Hit,Lang,Status,AutoTimeStamp";
			pager.PagerTable = "shop_Product";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<ProductInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE shop_Product SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
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
						" UPDATE shop_Product SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static bool Add(ProductInfo proMain, Dictionary<string, ProductFieldInfo> dicField, List<PhotoAlbumInfo> listPhoto)
		{
            proMain.Specifications = Product.GetStringFromDic("Specifications", dicField);
            proMain.ModelNum = Product.GetStringFromDic("ModelNum", dicField);
            proMain.Size = Product.GetStringFromDic("Size", dicField);
            proMain.Weight = Product.GetStringFromDic("Weight", dicField);
            proMain.Color = Product.GetStringFromDic("Color", dicField);
            proMain.Material = Product.GetStringFromDic("Material", dicField);
            proMain.ProducingArea = Product.GetStringFromDic("ProducingArea", dicField);
            proMain.ShortDesc = Product.GetStringFromDic("ShortDesc", dicField);
            proMain.ProDetail = Product.GetStringFromDic("ProDetail", dicField);
            proMain.Sort = Product.MaxSort + 1;
            proMain.AutoTimeStamp = DateTime.Now;
            proMain.Lang = JObject.cultureLang;
			int num = Product.Add(proMain);
			bool result;
			if (num > 0)
			{
				proMain.AutoID = num;
				ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(proMain.ModelID);
				IList<ProductFieldInfo> customFieldListByModelID = ProductField.GetCustomFieldListByModelID(proMain.ModelID);
				foreach (ProductFieldInfo current in customFieldListByModelID)
				{
					if (current.FieldName == "ProID")
					{
						current.Value = num.ToString();
					}
					else
					{
						current.Value = Product.GetStringFromDic(current.FieldName, dicField);
					}
				}
				Product.AddCustomProduct(cacheModelById, customFieldListByModelID);
				if (listPhoto.Count > 0)
				{
					foreach (PhotoAlbumInfo current2 in listPhoto)
					{
						current2.ProID = num;
					}
					PhotoAlbum.AddPhotoList(listPhoto);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool AddCustomProduct(ProductModelInfo model, IList<ProductFieldInfo> fieldList)
		{
			string commandText = Product.GenerateSqlOfInsert(model.TableName, fieldList);
			SqlParameter[] parameters = Product.PrepareSqlParameters(fieldList);
			return DBHelper.ExecuteNonQuery(commandText, parameters) > 0;
		}

		public static string GenerateSqlOfInsert(string tableName, IList<ProductFieldInfo> fieldList)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT INTO [");
			stringBuilder.Append(tableName);
			stringBuilder.Append("] (");
			foreach (ProductFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(" ) Values ( ");
			num = 0;
			foreach (ProductFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append("@");
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		public static SqlParameter[] PrepareSqlParameters(IList<ProductFieldInfo> fieldList)
		{
			List<SqlParameter> list = new List<SqlParameter>();
			foreach (ProductFieldInfo current in fieldList)
			{
				SqlParameter item = new SqlParameter("@" + current.FieldName, current.Value);
				list.Add(item);
			}
			return list.ToArray();
		}

		public static bool ExistsProductSN(string strProductSN, int intProductID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ProductSN", strProductSN),
				new SqlParameter("@ProductID", intProductID)
			};
			object obj = BizBase.dbo.ExecProcReValue("p_System_ExistsProductSN", arrParam);
			return obj != null && int.Parse(obj.ToString()) > 0;
		}

		public static bool Update(ProductInfo proMain, Dictionary<string, ProductFieldInfo> dicField, List<PhotoAlbumInfo> listPhoto)
		{
			proMain.Specifications = Product.GetStringFromDic("Specifications", dicField);
			proMain.ModelNum = Product.GetStringFromDic("ModelNum", dicField);
			proMain.Size = Product.GetStringFromDic("Size", dicField);
			proMain.Weight = Product.GetStringFromDic("Weight", dicField);
			proMain.Color = Product.GetStringFromDic("Color", dicField);
			proMain.Material = Product.GetStringFromDic("Material", dicField);
			proMain.ProducingArea = Product.GetStringFromDic("ProducingArea", dicField);
			proMain.ShortDesc = Product.GetStringFromDic("ShortDesc", dicField);
			proMain.ProDetail = Product.GetStringFromDic("ProDetail", dicField);
			bool result;
			if (Product.Update(proMain))
			{
				ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(proMain.ModelID);
				IList<ProductFieldInfo> customFieldListByModelID = ProductField.GetCustomFieldListByModelID(proMain.ModelID);
				foreach (ProductFieldInfo current in customFieldListByModelID)
				{
					if (current.FieldName == "ProID")
					{
						current.Value = proMain.AutoID.ToString();
					}
					else
					{
						current.Value = Product.GetStringFromDic(current.FieldName, dicField);
					}
				}
				if (Product.ExistsCustomTableData(cacheModelById.TableName, proMain.AutoID))
				{
					Product.UpdateCustomTable(proMain.AutoID, cacheModelById.TableName, customFieldListByModelID);
				}
				else
				{
					Product.AddCustomProduct(cacheModelById, customFieldListByModelID);
				}
				PhotoAlbum.DelPhotoByProID(proMain.AutoID);
				if (listPhoto.Count > 0)
				{
					foreach (PhotoAlbumInfo current2 in listPhoto)
					{
						current2.ProID = proMain.AutoID;
					}
					PhotoAlbum.AddPhotoList(listPhoto);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool UpdateCustomTable(int intProID, string strTableName, IList<ProductFieldInfo> fieldList)
		{
			string commandText = Product.GenerateSqlOfUpdate(intProID, strTableName, fieldList);
			SqlParameter[] parameters = Product.PrepareSqlParameters(fieldList);
			return DBHelper.ExecuteNonQuery(commandText, parameters) > 0;
		}

		public static string GenerateSqlOfUpdate(int intProID, string strTableName, IList<ProductFieldInfo> fieldList)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("UPDATE " + strTableName + " SET ");
			foreach (ProductFieldInfo current in fieldList)
			{
				num++;
				stringBuilder.Append(current.FieldName);
				stringBuilder.Append("=@");
				stringBuilder.Append(current.FieldName);
				if (num < fieldList.Count)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(" WHERE ProID= ");
			stringBuilder.Append(intProID);
			return stringBuilder.ToString();
		}

		public static bool DelProByID(int intProductID)
		{
			SqlParameter[] arrParam = new SqlParameter[]
			{
				new SqlParameter("@ProID", intProductID)
			};
			return BizBase.dbo.ExecProc("p_System_ProductDel", arrParam);
		}

		public static ProductInfo GetProduct(int intProductID)
		{
			ProductInfo dataById = Product.GetDataById(intProductID);
			if (dataById != null)
			{
				ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(dataById.ModelID);
				dataById.CustomTable = Product.GetCustomContentInfo(intProductID, cacheModelById.TableName);
				dataById.PhotoAlbums = PhotoAlbum.GetPhotoAlbumByPID(intProductID);
				UserInfo user = User.GetLoginUser();
				if (user != null)
				{
					dataById.MemberPriceSets = MemberPriceSet.GetList(dataById.MemberPriceSet, dataById.SellPrice);
					MemberPriceSetInfo memberPriceSetInfo = (from p in dataById.MemberPriceSets
					where p.UserLevelID.Equals(user.LevelID)
					select p).FirstOrDefault<MemberPriceSetInfo>();
					if (memberPriceSetInfo != null)
					{
						dataById.MemberPrice = ((memberPriceSetInfo.Price > 0m) ? memberPriceSetInfo.Price : memberPriceSetInfo.DiscoutPrice);
						if (dataById.MemberPrice == 0m)
						{
							dataById.MemberPrice = dataById.SellPrice;
						}
					}
				}
				dataById.PriceRange = dataById.SellPrice.ToString("f2");
				dataById.RealStock = dataById.Stock;
				if (dataById.ClassID > 0)
				{
					List<decimal> priceRange = GoodsSpecify.GetPriceRange(dataById);
					if (priceRange[0] == priceRange[1])
					{
						dataById.PriceRange = priceRange[0].ToString("f2");
					}
					else
					{
						dataById.PriceRange = priceRange[0].ToString("f2") + " - " + priceRange[1].ToString("f2");
					}
					dataById.GuiGe = GoodsSpecify.GetListByProID(dataById.AutoID);
					if (dataById.GuiGe != null && dataById.GuiGe.Count > 0)
					{
						dataById.RealStock = dataById.GuiGe.Sum((GoodsSpecifyInfo p) => p.Stock);
					}
				}
			}
			return dataById;
		}

		public static DataTable GetCustomContentInfo(int intProID, string strTableName)
		{
			return BizBase.dbo.GetDataTable(string.Concat(new object[]
			{
				" SELECT TOP 1 * FROM ",
				strTableName,
				" WHERE ProID=",
				intProID
			}));
		}

		public static DataTable GetProductById(int intProID, string tableName, List<ProductFieldInfo> usingFieldList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ");
			foreach (ProductFieldInfo current in usingFieldList)
			{
				if (current.IsSystem)
				{
					stringBuilder.Append("A.");
				}
				else
				{
					stringBuilder.Append("B.");
				}
				stringBuilder.Append(current.FieldName);
				stringBuilder.Append(",");
			}
			stringBuilder.Append("B.ProID");
			stringBuilder.Append(" FROM shop_Product A INNER JOIN ");
			stringBuilder.Append(tableName);
			stringBuilder.Append(" B ON A.AutoID=B.ProID WHERE A.AutoID=");
			stringBuilder.Append(intProID);
			return BizBase.dbo.GetDataTable(stringBuilder.ToString());
		}

		public static IList<ProductFieldInfo> GetFieldListWithValue(int intProID, int intModelID)
		{
			IList<ProductFieldInfo> fieldListByModelID = ProductField.GetFieldListByModelID(intModelID, true);
			IList<ProductFieldInfo> usingFieldList = ProductField.GetUsingFieldList(intModelID);
			ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(intModelID);
			if (cacheModelById != null)
			{
				DataTable dataTable = BizBase.dbo.GetDataTable(string.Concat(new object[]
				{
					"SELECT * FROM shop_Product AS A,",
					cacheModelById.TableName,
					" AS B WHERE A.AutoID=",
					intProID,
					" AND B.ProID=",
					intProID
				}));
				if (dataTable.Rows.Count == 1)
				{
					foreach (ProductFieldInfo current in fieldListByModelID)
					{
						if (dataTable.Columns.Contains(current.FieldName))
						{
							current.Value = dataTable.Rows[0][current.FieldName].ToString();
						}
						else
						{
							current.Value = current.DefaultValue;
						}
					}
				}
			}
			return fieldListByModelID;
		}

		public static bool CopyProduct(int intProductID)
		{
			ProductInfo dataById = Product.GetDataById(intProductID);
			bool result;
			if (dataById != null)
			{
				ProductModelInfo cacheModelById = ProductModel.GetCacheModelById(dataById.ModelID);
				dataById.ProductName = "复件:" + dataById.ProductName;
				dataById.ProductSN = Product.GetProductSN();
				dataById.Status = 0;
				dataById.AutoTimeStamp = DateTime.Now;
				dataById.Sales = 0;
				dataById.Sort = Product.MaxSort + 1;
				int num = BizBase.dbo.InsertModel<ProductInfo>(dataById);
				if (num > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(" INSERT INTO " + cacheModelById.TableName + " ( ");
					IList<ProductFieldInfo> customFieldListByModelID = ProductField.GetCustomFieldListByModelID(cacheModelById.AutoID);
					for (int i = 0; i < customFieldListByModelID.Count; i++)
					{
						if (customFieldListByModelID[i].FieldName != "AutoID")
						{
							if (i == customFieldListByModelID.Count - 1)
							{
								stringBuilder.Append(customFieldListByModelID[i].FieldName);
							}
							else
							{
								stringBuilder.Append(customFieldListByModelID[i].FieldName + ",");
							}
						}
					}
					stringBuilder.Append(" ) ");
					stringBuilder.Append(" SELECT ");
					for (int i = 0; i < customFieldListByModelID.Count; i++)
					{
						if (customFieldListByModelID[i].FieldName != "AutoID")
						{
							if (i == customFieldListByModelID.Count - 1)
							{
								if (customFieldListByModelID[i].FieldName.Equals("ProID"))
								{
									stringBuilder.Append(num.ToString());
								}
								else
								{
									stringBuilder.Append(customFieldListByModelID[i].FieldName);
								}
							}
							else if (customFieldListByModelID[i].FieldName.Equals("ProID"))
							{
								stringBuilder.Append(num.ToString() + ",");
							}
							else
							{
								stringBuilder.Append(customFieldListByModelID[i].FieldName + ",");
							}
						}
					}
					stringBuilder.Append(string.Concat(new object[]
					{
						" FROM ",
						cacheModelById.TableName,
						" WHERE ProID=",
						dataById.AutoID
					}));
					result = BizBase.dbo.ExecSQL(stringBuilder.ToString());
					return result;
				}
			}
			result = false;
			return result;
		}

		public static bool MoveProduct(int intCateID, string strProIDs)
		{
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE shop_Product SET CateID =",
				intCateID,
				"\tWHERE AutoID IN ( ",
				strProIDs,
				" ) "
			}));
		}

		public static bool UpdateStatus(string strIDList, bool isPutUp)
		{
			return BizBase.dbo.ExecSQL(string.Concat(new object[]
			{
				" UPDATE shop_Product SET [Status] =",
				isPutUp ? 99 : 0,
				" WHERE AutoID in ( ",
				strIDList,
				" )"
			}));
		}

		public static bool UpdateHit(string strIdList)
		{
			return BizBase.dbo.ExecSQL(" UPDATE shop_Product SET Hit=Hit+1 WHERE AutoID IN (" + strIdList + ") ");
		}

		internal static string GetStringFromDic(string fieldName, Dictionary<string, ProductFieldInfo> dicField)
		{
			string result;
			if (dicField.ContainsKey(fieldName))
			{
				result = dicField[fieldName].Value;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		internal static DateTime GetDateTimeFromDic(string fieldName, Dictionary<string, ProductFieldInfo> dicField, DateTime defaultValue)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			DateTime result;
			if (dicField.ContainsKey(fieldName) && DateTime.TryParse(dicField[fieldName].Value, out dateTime))
			{
				result = dateTime;
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static int GetIntFromDic(string fieldName, Dictionary<string, ProductFieldInfo> dicField, int defaultValue)
		{
			int result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToInt(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static decimal GetDecimalFromDic(string fieldName, Dictionary<string, ProductFieldInfo> dicField, decimal defaultValue)
		{
			decimal result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToDecimal(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		internal static bool GetBoolFromDic(string fieldName, Dictionary<string, ProductFieldInfo> dicField, bool defaultValue)
		{
			bool result;
			if (dicField.ContainsKey(fieldName))
			{
				result = WebUtils.StringToBool(dicField[fieldName].Value, defaultValue);
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		public static string GetProductSN()
		{
			SettingInfo settingInfo = ConfigProvider.Configs.Get("ProductSNFmt");
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

		public static int HasBuy(int intUserID, int intProID)
		{
			return BizBase.dbo.GetValue<int>(string.Concat(new string[]
			{
				" SELECT COUNT(*) FROM shop_OrderItem WHERE ProID=",
				intProID.ToString(),
				" AND EXISTS(SELECT 1 FROM shop_Orders WHERE UserID=",
				intUserID.ToString(),
				" AND AutoID=shop_OrderItem.OrderID AND OrderStatus=99) "
			}));
		}

		public static bool ExistsCustomTableData(string ModelTable, int ProID)
		{
			return BizBase.dbo.GetValue<int>(" select COUNT(*) from " + ModelTable + " where ProID=" + ProID.ToString()) > 0;
		}

		public static DataTable GetBuyRec(int proID)
		{
			return BizBase.dbo.GetDataTable(" select a.OrderID,b.UserName,a.Quantity,b.OrderAddTime from shop_OrderItem as a,shop_Orders as b where a.ProID=" + proID + " and a.OrderID=b.AutoID and b.OrderStatus=99 order by b.OrderAddTime desc ");
		}
	}
}
