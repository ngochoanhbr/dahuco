using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class ShippingAddressInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _UserID = 0;

		private string _UserName = string.Empty;

		private string _Consignee = string.Empty;

		private string _Country = string.Empty;

		private string _Province = string.Empty;

		private string _City = string.Empty;

		private string _County = string.Empty;

		private string _Address = string.Empty;

		private string _PostCode = string.Empty;

		private string _ContactPhone = string.Empty;

		private bool _IsDefault = false;

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

		public string Consignee
		{
			get
			{
				return this._Consignee;
			}
			set
			{
				this._Consignee = value;
			}
		}

		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				this._Country = value;
			}
		}

		public string Province
		{
			get
			{
				return this._Province;
			}
			set
			{
				this._Province = value;
			}
		}

		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
			}
		}

		public string County
		{
			get
			{
				return this._County;
			}
			set
			{
				this._County = value;
			}
		}

		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				this._Address = value;
			}
		}

		public string PostCode
		{
			get
			{
				return this._PostCode;
			}
			set
			{
				this._PostCode = value;
			}
		}

		public string ContactPhone
		{
			get
			{
				return this._ContactPhone;
			}
			set
			{
				this._ContactPhone = value;
			}
		}

		public bool IsDefault
		{
			get
			{
				return this._IsDefault;
			}
			set
			{
				this._IsDefault = value;
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
				return "shop_ShippingAddress";
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
				return "AutoID,UserID,UserName,Consignee,Country,Province,City,County,Address,PostCode,ContactPhone,IsDefault,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (ShippingAddressInfo._listField == null)
				{
					ShippingAddressInfo._listField = new List<string>();
					ShippingAddressInfo._listField.Add("AutoID");
					ShippingAddressInfo._listField.Add("UserID");
					ShippingAddressInfo._listField.Add("UserName");
					ShippingAddressInfo._listField.Add("Consignee");
					ShippingAddressInfo._listField.Add("Country");
					ShippingAddressInfo._listField.Add("Province");
					ShippingAddressInfo._listField.Add("City");
					ShippingAddressInfo._listField.Add("County");
					ShippingAddressInfo._listField.Add("Address");
					ShippingAddressInfo._listField.Add("PostCode");
					ShippingAddressInfo._listField.Add("ContactPhone");
					ShippingAddressInfo._listField.Add("IsDefault");
					ShippingAddressInfo._listField.Add("AutoTimeStamp");
				}
				return ShippingAddressInfo._listField;
			}
		}
	}
}
