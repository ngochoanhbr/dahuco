using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class AuctionInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _ProID = 0;

		private string _ActName = string.Empty;

		private string _ActImage = string.Empty;

		private DateTime _StartTime = new DateTime(1900, 1, 1);

		private DateTime _EndTime = new DateTime(1900, 1, 1);

		private decimal _BasePrice = 0.0m;

		private decimal _StepPrice = 0.0m;

		private decimal _ProtectPrice = 0.0m;

		private decimal _TopPrice = 0.0m;

		private decimal _CashDeposit = 0.0m;

		private string _ActDesc = string.Empty;

		private int _Sort = 0;

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

		public string ActName
		{
			get
			{
				return this._ActName;
			}
			set
			{
				this._ActName = value;
			}
		}

		public string ActImage
		{
			get
			{
				return this._ActImage;
			}
			set
			{
				this._ActImage = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				this._StartTime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				this._EndTime = value;
			}
		}

		public decimal BasePrice
		{
			get
			{
				return this._BasePrice;
			}
			set
			{
				this._BasePrice = value;
			}
		}

		public decimal StepPrice
		{
			get
			{
				return this._StepPrice;
			}
			set
			{
				this._StepPrice = value;
			}
		}

		public decimal ProtectPrice
		{
			get
			{
				return this._ProtectPrice;
			}
			set
			{
				this._ProtectPrice = value;
			}
		}

		public decimal TopPrice
		{
			get
			{
				return this._TopPrice;
			}
			set
			{
				this._TopPrice = value;
			}
		}

		public decimal CashDeposit
		{
			get
			{
				return this._CashDeposit;
			}
			set
			{
				this._CashDeposit = value;
			}
		}

		public string ActDesc
		{
			get
			{
				return this._ActDesc;
			}
			set
			{
				this._ActDesc = value;
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
				return "shop_Auction";
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
				return "AutoID,ProID,ActName,ActImage,StartTime,EndTime,BasePrice,StepPrice,ProtectPrice,TopPrice,CashDeposit,ActDesc,Sort,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (AuctionInfo._listField == null)
				{
					AuctionInfo._listField = new List<string>();
					AuctionInfo._listField.Add("AutoID");
					AuctionInfo._listField.Add("ProID");
					AuctionInfo._listField.Add("ActName");
					AuctionInfo._listField.Add("ActImage");
					AuctionInfo._listField.Add("StartTime");
					AuctionInfo._listField.Add("EndTime");
					AuctionInfo._listField.Add("BasePrice");
					AuctionInfo._listField.Add("StepPrice");
					AuctionInfo._listField.Add("ProtectPrice");
					AuctionInfo._listField.Add("TopPrice");
					AuctionInfo._listField.Add("CashDeposit");
					AuctionInfo._listField.Add("ActDesc");
					AuctionInfo._listField.Add("Sort");
					AuctionInfo._listField.Add("AutoTimeStamp");
				}
				return AuctionInfo._listField;
			}
		}
	}
}
