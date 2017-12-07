using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class OrderItemInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _OrderID = 0;

		private int _ProID = 0;

		private string _ProName = string.Empty;

		private string _ProSN = string.Empty;

		private string _ProImg = string.Empty;

		private decimal _Price = 0.0m;

		private int _Integral = 0;

		private int _Quantity = 0;

		private decimal _SubTotal = 0.0m;

		private string _GuiGePath = string.Empty;

		private bool _IsVirtual = false;

		private bool _IsEva = false;

		private string _Remark = string.Empty;

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

		public int OrderID
		{
			get
			{
				return this._OrderID;
			}
			set
			{
				this._OrderID = value;
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

		public string ProSN
		{
			get
			{
				return this._ProSN;
			}
			set
			{
				this._ProSN = value;
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

		public int Integral
		{
			get
			{
				return this._Integral;
			}
			set
			{
				this._Integral = value;
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

		public string GuiGePath
		{
			get
			{
				return this._GuiGePath;
			}
			set
			{
				this._GuiGePath = value;
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

		public bool IsEva
		{
			get
			{
				return this._IsEva;
			}
			set
			{
				this._IsEva = value;
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
				return "shop_OrderItem";
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
				return "AutoID,OrderID,ProID,ProName,ProSN,ProImg,Price,Integral,Quantity,SubTotal,GuiGePath,IsVirtual,IsEva,Remark,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (OrderItemInfo._listField == null)
				{
					OrderItemInfo._listField = new List<string>();
					OrderItemInfo._listField.Add("AutoID");
					OrderItemInfo._listField.Add("OrderID");
					OrderItemInfo._listField.Add("ProID");
					OrderItemInfo._listField.Add("ProName");
					OrderItemInfo._listField.Add("ProSN");
					OrderItemInfo._listField.Add("ProImg");
					OrderItemInfo._listField.Add("Price");
					OrderItemInfo._listField.Add("Integral");
					OrderItemInfo._listField.Add("Quantity");
					OrderItemInfo._listField.Add("SubTotal");
					OrderItemInfo._listField.Add("GuiGePath");
					OrderItemInfo._listField.Add("IsVirtual");
					OrderItemInfo._listField.Add("IsEva");
					OrderItemInfo._listField.Add("Remark");
					OrderItemInfo._listField.Add("AutoTimeStamp");
				}
				return OrderItemInfo._listField;
			}
		}
	}
}
