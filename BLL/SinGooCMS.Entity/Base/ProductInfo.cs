using SinGooCMS.Config;
using SinGooCMS.DAL;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class ProductInfo : JObject, IEntity
	{
		private AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);

		private string _ProductUrl = string.Empty;

		private int _AutoID = 0;

		private int _CateID = 0;

		private int _BrandID = 0;

		private int _ModelID = 0;

		private string _SellType = string.Empty;

		private string _ProductName = string.Empty;

		private string _ProductSN = string.Empty;

		private string _ProImg = string.Empty;

		private string _ProIDs = string.Empty;

		private int _Stock = 0;

		private int _AlarmNum = 0;

		private decimal _MarketPrice = 0.0m;

		private decimal _SellPrice = 0.0m;

		private string _MemberPriceSet = string.Empty;

		private string _Unit = string.Empty;

		private int _BuyIntegral = 0;

		private int _GiveIntegral = 0;

		private bool _IsExchange = false;

		private int _BuyLimit = 0;

		private int _ClassID = 0;

		private int _AreaModelID = 0;

		private int _PostageModelID = 0;

		private string _Manufacturer = string.Empty;

		private string _Specifications = string.Empty;

		private string _ModelNum = string.Empty;

		private string _Size = string.Empty;

		private string _Weight = string.Empty;

		private string _Material = string.Empty;

		private string _Color = string.Empty;

		private string _ProducingArea = string.Empty;

		private string _ShortDesc = string.Empty;

		private string _ProDetail = string.Empty;

		private string _RelatedProduct = string.Empty;

		private string _ActiveName = string.Empty;

		private string _Tags = string.Empty;

		private string _SEOKey = string.Empty;

		private string _SEODescription = string.Empty;

		private bool _IsBooking = false;

		private bool _IsVirtual = false;

		private bool _IsTop = false;

		private bool _IsRecommend = false;

		private bool _IsNew = false;

		private int _Sales = 0;

		private int _Sort = 0;

		private int _Hit = 0;

		private string _Lang = string.Empty;

		private int _Status = 0;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

		public IList<PhotoAlbumInfo> PhotoAlbums
		{
			get;
			set;
		}

		public DataTable CustomTable
		{
			get;
			set;
		}

		public DataRow Items
		{
			get
			{
				DataRow result;
				if (this.CustomTable != null && this.CustomTable.Rows.Count > 0)
				{
					result = this.CustomTable.Rows[0];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public int CommentCount
		{
			get;
			set;
		}

		public string PriceRange
		{
			get;
			set;
		}

		public List<MemberPriceSetInfo> MemberPriceSets
		{
			get;
			set;
		}

		public decimal MemberPrice
		{
			get;
			set;
		}

		public IList<GoodsSpecifyInfo> GuiGe
		{
			get;
			set;
		}

		public int RealStock
		{
			get;
			set;
		}

		public IList<SuitProductItemInfo> SuitProductList
		{
			get
			{
				IList<SuitProductItemInfo> result;
				if (!string.IsNullOrEmpty(this.ProIDs))
				{
					IList<SuitProductItemInfo> list = new List<SuitProductItemInfo>();
					string[] array = this.ProIDs.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						string[] array2 = text.Split(new char[]
						{
							'|'
						});
						if (array2.Length == 3)
						{
							list.Add(new SuitProductItemInfo
							{
								ProductName = WebUtils.GetString(array2[0]),
								ProductID = WebUtils.GetInt(array2[1]),
								Product = this.dbo.GetModel<ProductInfo>(" SELECT TOP 1 * FROM shop_Product WHERE AutoID=" + array2[1]),
								Quantity = WebUtils.GetInt(array2[2])
							});
						}
					}
					result = list;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public string ProductUrl
		{
			get
			{
				if (string.IsNullOrEmpty(this._ProductUrl))
				{
					this._ProductUrl = this.GetProductUrl();
				}
				return this._ProductUrl;
			}
		}

		public int AutoID
		{
			get
			{
				return this._AutoID;
			}
			set
			{
				this._AutoID = value;
			}
		}

		public int CateID
		{
			get
			{
				return this._CateID;
			}
			set
			{
				this._CateID = value;
			}
		}

		public int BrandID
		{
			get
			{
				return this._BrandID;
			}
			set
			{
				this._BrandID = value;
			}
		}

		public int ModelID
		{
			get
			{
				return this._ModelID;
			}
			set
			{
				this._ModelID = value;
			}
		}

		public string SellType
		{
			get
			{
				return this._SellType;
			}
			set
			{
				this._SellType = value;
			}
		}

		public string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				this._ProductName = value;
			}
		}

		public string ProductSN
		{
			get
			{
				return this._ProductSN;
			}
			set
			{
				this._ProductSN = value;
			}
		}

		public string ProImg
		{
			get
			{
				return this._ProImg;
			}
			set
			{
				this._ProImg = value;
			}
		}

		public string ProIDs
		{
			get
			{
				return this._ProIDs;
			}
			set
			{
				this._ProIDs = value;
			}
		}

		public int Stock
		{
			get
			{
				return this._Stock;
			}
			set
			{
				this._Stock = value;
			}
		}

		public int AlarmNum
		{
			get
			{
				return this._AlarmNum;
			}
			set
			{
				this._AlarmNum = value;
			}
		}

		public decimal MarketPrice
		{
			get
			{
				return this._MarketPrice;
			}
			set
			{
				this._MarketPrice = value;
			}
		}

		public decimal SellPrice
		{
			get
			{
				return this._SellPrice;
			}
			set
			{
				this._SellPrice = value;
			}
		}

		public string MemberPriceSet
		{
			get
			{
				return this._MemberPriceSet;
			}
			set
			{
				this._MemberPriceSet = value;
			}
		}

		public string Unit
		{
			get
			{
				return this._Unit;
			}
			set
			{
				this._Unit = value;
			}
		}

		public int BuyIntegral
		{
			get
			{
				return this._BuyIntegral;
			}
			set
			{
				this._BuyIntegral = value;
			}
		}

		public int GiveIntegral
		{
			get
			{
				return this._GiveIntegral;
			}
			set
			{
				this._GiveIntegral = value;
			}
		}

		public bool IsExchange
		{
			get
			{
				return this._IsExchange;
			}
			set
			{
				this._IsExchange = value;
			}
		}

		public int BuyLimit
		{
			get
			{
				return this._BuyLimit;
			}
			set
			{
				this._BuyLimit = value;
			}
		}

		public int ClassID
		{
			get
			{
				return this._ClassID;
			}
			set
			{
				this._ClassID = value;
			}
		}

		public int AreaModelID
		{
			get
			{
				return this._AreaModelID;
			}
			set
			{
				this._AreaModelID = value;
			}
		}

		public int PostageModelID
		{
			get
			{
				return this._PostageModelID;
			}
			set
			{
				this._PostageModelID = value;
			}
		}

		public string Manufacturer
		{
			get
			{
				return this._Manufacturer;
			}
			set
			{
				this._Manufacturer = value;
			}
		}

		public string Specifications
		{
			get
			{
				return this._Specifications;
			}
			set
			{
				this._Specifications = value;
			}
		}

		public string ModelNum
		{
			get
			{
				return this._ModelNum;
			}
			set
			{
				this._ModelNum = value;
			}
		}

		public string Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				this._Size = value;
			}
		}

		public string Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				this._Weight = value;
			}
		}

		public string Material
		{
			get
			{
				return this._Material;
			}
			set
			{
				this._Material = value;
			}
		}

		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				this._Color = value;
			}
		}

		public string ProducingArea
		{
			get
			{
				return this._ProducingArea;
			}
			set
			{
				this._ProducingArea = value;
			}
		}

		public string ShortDesc
		{
			get
			{
				return this._ShortDesc;
			}
			set
			{
				this._ShortDesc = value;
			}
		}

		public string ProDetail
		{
			get
			{
				return this._ProDetail;
			}
			set
			{
				this._ProDetail = value;
			}
		}

		public string RelatedProduct
		{
			get
			{
				return this._RelatedProduct;
			}
			set
			{
				this._RelatedProduct = value;
			}
		}

		public string ActiveName
		{
			get
			{
				return this._ActiveName;
			}
			set
			{
				this._ActiveName = value;
			}
		}

		public string Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
			}
		}

		public string SEOKey
		{
			get
			{
				return this._SEOKey;
			}
			set
			{
				this._SEOKey = value;
			}
		}

		public string SEODescription
		{
			get
			{
				return this._SEODescription;
			}
			set
			{
				this._SEODescription = value;
			}
		}

		public bool IsBooking
		{
			get
			{
				return this._IsBooking;
			}
			set
			{
				this._IsBooking = value;
			}
		}

		public bool IsVirtual
		{
			get
			{
				return this._IsVirtual;
			}
			set
			{
				this._IsVirtual = value;
			}
		}

		public bool IsTop
		{
			get
			{
				return this._IsTop;
			}
			set
			{
				this._IsTop = value;
			}
		}

		public bool IsRecommend
		{
			get
			{
				return this._IsRecommend;
			}
			set
			{
				this._IsRecommend = value;
			}
		}

		public bool IsNew
		{
			get
			{
				return this._IsNew;
			}
			set
			{
				this._IsNew = value;
			}
		}

		public int Sales
		{
			get
			{
				return this._Sales;
			}
			set
			{
				this._Sales = value;
			}
		}

		public int Sort
		{
			get
			{
				return this._Sort;
			}
			set
			{
				this._Sort = value;
			}
		}

		public int Hit
		{
			get
			{
				return this._Hit;
			}
			set
			{
				this._Hit = value;
			}
		}

		public string Lang
		{
			get
			{
				return this._Lang;
			}
			set
			{
				this._Lang = value;
			}
		}

		public int Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
			}
		}

		public DateTime AutoTimeStamp
		{
			get
			{
				return this._AutoTimeStamp;
			}
			set
			{
				this._AutoTimeStamp = value;
			}
		}

		public string DBTableName
		{
			get
			{
				return "shop_Product";
			}
		}

		public string PKName
		{
			get
			{
				return "AutoID";
			}
		}

		public string Fields
		{
			get
			{
				return "AutoID,CateID,BrandID,ModelID,SellType,ProductName,ProductSN,ProImg,ProIDs,Stock,AlarmNum,MarketPrice,SellPrice,MemberPriceSet,Unit,BuyIntegral,GiveIntegral,IsExchange,BuyLimit,ClassID,AreaModelID,PostageModelID,Manufacturer,Specifications,ModelNum,Size,Weight,Material,Color,ProducingArea,ShortDesc,ProDetail,RelatedProduct,ActiveName,Tags,SEOKey,SEODescription,IsBooking,IsVirtual,IsTop,IsRecommend,IsNew,Sales,Sort,Hit,Lang,Status,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (ProductInfo._listField == null)
				{
					ProductInfo._listField = new List<string>();
					ProductInfo._listField.Add("AutoID");
					ProductInfo._listField.Add("CateID");
					ProductInfo._listField.Add("BrandID");
					ProductInfo._listField.Add("ModelID");
					ProductInfo._listField.Add("SellType");
					ProductInfo._listField.Add("ProductName");
					ProductInfo._listField.Add("ProductSN");
					ProductInfo._listField.Add("ProImg");
					ProductInfo._listField.Add("ProIDs");
					ProductInfo._listField.Add("Stock");
					ProductInfo._listField.Add("AlarmNum");
					ProductInfo._listField.Add("MarketPrice");
					ProductInfo._listField.Add("SellPrice");
					ProductInfo._listField.Add("MemberPriceSet");
					ProductInfo._listField.Add("Unit");
					ProductInfo._listField.Add("BuyIntegral");
					ProductInfo._listField.Add("GiveIntegral");
					ProductInfo._listField.Add("IsExchange");
					ProductInfo._listField.Add("BuyLimit");
					ProductInfo._listField.Add("ClassID");
					ProductInfo._listField.Add("AreaModelID");
					ProductInfo._listField.Add("PostageModelID");
					ProductInfo._listField.Add("Manufacturer");
					ProductInfo._listField.Add("Specifications");
					ProductInfo._listField.Add("ModelNum");
					ProductInfo._listField.Add("Size");
					ProductInfo._listField.Add("Weight");
					ProductInfo._listField.Add("Material");
					ProductInfo._listField.Add("Color");
					ProductInfo._listField.Add("ProducingArea");
					ProductInfo._listField.Add("ShortDesc");
					ProductInfo._listField.Add("ProDetail");
					ProductInfo._listField.Add("RelatedProduct");
					ProductInfo._listField.Add("ActiveName");
					ProductInfo._listField.Add("Tags");
					ProductInfo._listField.Add("SEOKey");
					ProductInfo._listField.Add("SEODescription");
					ProductInfo._listField.Add("IsBooking");
					ProductInfo._listField.Add("IsVirtual");
					ProductInfo._listField.Add("IsTop");
					ProductInfo._listField.Add("IsRecommend");
					ProductInfo._listField.Add("IsNew");
					ProductInfo._listField.Add("Sales");
					ProductInfo._listField.Add("Sort");
					ProductInfo._listField.Add("Hit");
					ProductInfo._listField.Add("Lang");
					ProductInfo._listField.Add("Status");
					ProductInfo._listField.Add("AutoTimeStamp");
				}
				return ProductInfo._listField;
			}
		}

		public object Get(string name)
		{
			object result;
			if (this.CustomTable != null && this.CustomTable.Rows.Count > 0 && this.CustomTable.Columns.Contains(name))
			{
				result = this.CustomTable.Rows[0][name];
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public string GetProductUrl()
		{
			string text = "/shop/goods.aspx?pid=" + this.AutoID.ToString();
			switch ((BrowseType)Enum.Parse(typeof(BrowseType), ConfigProvider.Configs.BrowseType))
			{
			case BrowseType.UrlRewriteAndAspx:
				text = "/shop/goods/" + this.AutoID.ToString() + ".aspx";
				break;
			case BrowseType.UrlRewriteNoAspx:
				text = "/shop/goods/" + this.AutoID.ToString();
				break;
			case BrowseType.HtmlRewrite:
				text = "/shop/goods/" + this.AutoID.ToString() + ".html";
				break;
			case BrowseType.Html:
				text = ("/html/shop/goods/" + ConfigProvider.Configs.HtmlFileRule + ConfigProvider.Configs.HtmlFileExt).Replace("{$year}", this.AutoTimeStamp.ToString("yyyy")).Replace("{$month}", this.AutoTimeStamp.ToString("MM")).Replace("{$day}", this.AutoTimeStamp.ToString("dd")).Replace("{$id}", this.AutoID.ToString());
				break;
			}
			return text.Replace("//", "/");
		}
	}
}
