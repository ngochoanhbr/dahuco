using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class PromotionInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _ProID = 0;

		private string _ActName = string.Empty;

		private string _ActImage = string.Empty;

		private decimal _PromotePrice = 0.0m;

		private DateTime _StartTime = new DateTime(1900, 1, 1);

		private DateTime _EndTime = new DateTime(1900, 1, 1);

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
				return "shop_Promotion";
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
				return "AutoID,ProID,ActName,ActImage,PromotePrice,StartTime,EndTime,ActDesc,Sort,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (PromotionInfo._listField == null)
				{
					PromotionInfo._listField = new List<string>();
					PromotionInfo._listField.Add("AutoID");
					PromotionInfo._listField.Add("ProID");
					PromotionInfo._listField.Add("ActName");
					PromotionInfo._listField.Add("ActImage");
					PromotionInfo._listField.Add("PromotePrice");
					PromotionInfo._listField.Add("StartTime");
					PromotionInfo._listField.Add("EndTime");
					PromotionInfo._listField.Add("ActDesc");
					PromotionInfo._listField.Add("Sort");
					PromotionInfo._listField.Add("AutoTimeStamp");
				}
				return PromotionInfo._listField;
			}
		}
	}
}
