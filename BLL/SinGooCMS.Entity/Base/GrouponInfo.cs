using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GrouponInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _ProID = 0;

		private string _ActName = string.Empty;

		private string _ActImage = string.Empty;

		private DateTime _StartTime = new DateTime(1900, 1, 1);

		private DateTime _EndTime = new DateTime(1900, 1, 1);

		private string _PriceLadder = string.Empty;

		private int _JoinNum = 0;

		private decimal _CashDeposit = 0.0m;

		private string _ActDesc = string.Empty;

		private int _Sort = 0;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

		public List<PriceLadderInfo> PriceLadders
		{
			get
			{
				List<PriceLadderInfo> result;
				if (!string.IsNullOrEmpty(this.PriceLadder))
				{
					result = (from p in JsonUtils.JsonToObject<List<PriceLadderInfo>>(this.PriceLadder)
					orderby p.JoinNum
					select p).ToList<PriceLadderInfo>();
				}
				else
				{
					result = new List<PriceLadderInfo>();
				}
				return result;
			}
		}

		public decimal CurrentPrice
		{
			get
			{
				decimal result = 0.0m;
				if (this.PriceLadders != null && this.PriceLadders.Count > 0)
				{
					foreach (PriceLadderInfo current in this.PriceLadders)
					{
						if (this.JoinNum >= current.JoinNum)
						{
							result = current.Price;
						}
					}
				}
				return result;
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

		public string PriceLadder
		{
			get
			{
				return this._PriceLadder;
			}
			set
			{
				this._PriceLadder = value;
			}
		}

		public int JoinNum
		{
			get
			{
				return this._JoinNum;
			}
			set
			{
				this._JoinNum = value;
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
				return "shop_Groupon";
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
				return "AutoID,ProID,ActName,ActImage,StartTime,EndTime,PriceLadder,JoinNum,CashDeposit,ActDesc,Sort,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (GrouponInfo._listField == null)
				{
					GrouponInfo._listField = new List<string>();
					GrouponInfo._listField.Add("AutoID");
					GrouponInfo._listField.Add("ProID");
					GrouponInfo._listField.Add("ActName");
					GrouponInfo._listField.Add("ActImage");
					GrouponInfo._listField.Add("StartTime");
					GrouponInfo._listField.Add("EndTime");
					GrouponInfo._listField.Add("PriceLadder");
					GrouponInfo._listField.Add("JoinNum");
					GrouponInfo._listField.Add("CashDeposit");
					GrouponInfo._listField.Add("ActDesc");
					GrouponInfo._listField.Add("Sort");
					GrouponInfo._listField.Add("AutoTimeStamp");
				}
				return GrouponInfo._listField;
			}
		}
	}
}
