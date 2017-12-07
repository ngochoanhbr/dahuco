using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GoodsSpecifyInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _Specification = string.Empty;

		private int _ProID = 0;

		private string _ProductSN = string.Empty;

		private string _PreviewImg = string.Empty;

		private decimal _SellPrice = 0.0m;

		private string _MemberPriceSet = string.Empty;

		private decimal _PromotePrice = 0.0m;

		private int _Stock = 0;

		private int _AlarmNum = 0;

		private int _Sort = 0;

		private string _Lang = string.Empty;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

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

		public string Specification
		{
			get
			{
				return this._Specification;
			}
			set
			{
				this._Specification = value;
			}
		}

		public int ProID
		{
			get
			{
				return this._ProID;
			}
			set
			{
				this._ProID = value;
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

		public string PreviewImg
		{
			get
			{
				return this._PreviewImg;
			}
			set
			{
				this._PreviewImg = value;
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

		public decimal PromotePrice
		{
			get
			{
				return this._PromotePrice;
			}
			set
			{
				this._PromotePrice = value;
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
				return "shop_GoodsSpecify";
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
				return "AutoID,Specification,ProID,ProductSN,PreviewImg,SellPrice,MemberPriceSet,PromotePrice,Stock,AlarmNum,Sort,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (GoodsSpecifyInfo._listField == null)
				{
					GoodsSpecifyInfo._listField = new List<string>();
					GoodsSpecifyInfo._listField.Add("AutoID");
					GoodsSpecifyInfo._listField.Add("Specification");
					GoodsSpecifyInfo._listField.Add("ProID");
					GoodsSpecifyInfo._listField.Add("ProductSN");
					GoodsSpecifyInfo._listField.Add("PreviewImg");
					GoodsSpecifyInfo._listField.Add("SellPrice");
					GoodsSpecifyInfo._listField.Add("MemberPriceSet");
					GoodsSpecifyInfo._listField.Add("PromotePrice");
					GoodsSpecifyInfo._listField.Add("Stock");
					GoodsSpecifyInfo._listField.Add("AlarmNum");
					GoodsSpecifyInfo._listField.Add("Sort");
					GoodsSpecifyInfo._listField.Add("Lang");
					GoodsSpecifyInfo._listField.Add("AutoTimeStamp");
				}
				return GoodsSpecifyInfo._listField;
			}
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
	}
}
