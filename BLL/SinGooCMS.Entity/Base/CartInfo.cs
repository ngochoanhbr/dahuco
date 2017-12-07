using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class CartInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _CartNo = string.Empty;

		private int _UserID = 0;

		private string _UserName = string.Empty;

		private int _ProID = 0;

		private string _ProName = string.Empty;

		private string _ProImg = string.Empty;

		private int _Quantity = 0;

		private decimal _Price = 0.0m;

		private decimal _SubTotal = 0.0m;

		private string _SellType = string.Empty;

		private int _GoodsAttr = 0;

		private string _GoodsAttrStr = string.Empty;

		private string _Remark = string.Empty;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

		public ProductInfo Product
		{
			get;
			set;
		}

		public decimal MemberPrice
		{
			get;
			set;
		}

		public decimal RealSubTotal
		{
			get;
			set;
		}

		public bool IsOffline
		{
			get;
			set;
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

		public string CartNo
		{
			get
			{
				return this._CartNo;
			}
			set
			{
				this._CartNo = value;
			}
		}

		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
			}
		}

		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
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

		public string ProName
		{
			get
			{
				return this._ProName;
			}
			set
			{
				this._ProName = value;
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

		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				this._Quantity = value;
			}
		}

		public decimal Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				this._Price = value;
			}
		}

		public decimal SubTotal
		{
			get
			{
				return this._SubTotal;
			}
			set
			{
				this._SubTotal = value;
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

		public int GoodsAttr
		{
			get
			{
				return this._GoodsAttr;
			}
			set
			{
				this._GoodsAttr = value;
			}
		}

		public string GoodsAttrStr
		{
			get
			{
				return this._GoodsAttrStr;
			}
			set
			{
				this._GoodsAttrStr = value;
			}
		}

		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				this._Remark = value;
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
				return "shop_Cart";
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
				return "AutoID,CartNo,UserID,UserName,ProID,ProName,ProImg,Quantity,Price,SubTotal,SellType,GoodsAttr,GoodsAttrStr,Remark,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (CartInfo._listField == null)
				{
					CartInfo._listField = new List<string>();
					CartInfo._listField.Add("AutoID");
					CartInfo._listField.Add("CartNo");
					CartInfo._listField.Add("UserID");
					CartInfo._listField.Add("UserName");
					CartInfo._listField.Add("ProID");
					CartInfo._listField.Add("ProName");
					CartInfo._listField.Add("ProImg");
					CartInfo._listField.Add("Quantity");
					CartInfo._listField.Add("Price");
					CartInfo._listField.Add("SubTotal");
					CartInfo._listField.Add("SellType");
					CartInfo._listField.Add("GoodsAttr");
					CartInfo._listField.Add("GoodsAttrStr");
					CartInfo._listField.Add("Remark");
					CartInfo._listField.Add("AutoTimeStamp");
				}
				return CartInfo._listField;
			}
		}
	}
}
