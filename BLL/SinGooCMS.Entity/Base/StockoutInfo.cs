using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class StockoutInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _Type = 0;

		private int _ProID = 0;

		private string _ProName = string.Empty;

		private string _UserName = string.Empty;

		private int _CurrStock = 0;

		private bool _IsRead = false;

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

		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
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

		public int CurrStock
		{
			get
			{
				return this._CurrStock;
			}
			set
			{
				this._CurrStock = value;
			}
		}

		public bool IsRead
		{
			get
			{
				return this._IsRead;
			}
			set
			{
				this._IsRead = value;
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
				return "shop_Stockout";
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
				return "AutoID,Type,ProID,ProName,UserName,CurrStock,IsRead,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (StockoutInfo._listField == null)
				{
					StockoutInfo._listField = new List<string>();
					StockoutInfo._listField.Add("AutoID");
					StockoutInfo._listField.Add("Type");
					StockoutInfo._listField.Add("ProID");
					StockoutInfo._listField.Add("ProName");
					StockoutInfo._listField.Add("UserName");
					StockoutInfo._listField.Add("CurrStock");
					StockoutInfo._listField.Add("IsRead");
					StockoutInfo._listField.Add("AutoTimeStamp");
				}
				return StockoutInfo._listField;
			}
		}
	}
}
