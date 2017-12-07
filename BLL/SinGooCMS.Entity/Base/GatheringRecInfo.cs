using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GatheringRecInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _OrderNo = string.Empty;

		private decimal _OrderAmount = 0.0m;

		private decimal _RealPayAmount = 0.0m;

		private string _PayTypeName = string.Empty;

		private string _PayUserName = string.Empty;

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

		public string OrderNo
		{
			get
			{
				return this._OrderNo;
			}
			set
			{
				this._OrderNo = value;
			}
		}

		public decimal OrderAmount
		{
			get
			{
				return this._OrderAmount;
			}
			set
			{
				this._OrderAmount = value;
			}
		}

		public decimal RealPayAmount
		{
			get
			{
				return this._RealPayAmount;
			}
			set
			{
				this._RealPayAmount = value;
			}
		}

		public string PayTypeName
		{
			get
			{
				return this._PayTypeName;
			}
			set
			{
				this._PayTypeName = value;
			}
		}

		public string PayUserName
		{
			get
			{
				return this._PayUserName;
			}
			set
			{
				this._PayUserName = value;
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
				return "shop_GatheringRec";
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
				return "AutoID,OrderNo,OrderAmount,RealPayAmount,PayTypeName,PayUserName,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (GatheringRecInfo._listField == null)
				{
					GatheringRecInfo._listField = new List<string>();
					GatheringRecInfo._listField.Add("AutoID");
					GatheringRecInfo._listField.Add("OrderNo");
					GatheringRecInfo._listField.Add("OrderAmount");
					GatheringRecInfo._listField.Add("RealPayAmount");
					GatheringRecInfo._listField.Add("PayTypeName");
					GatheringRecInfo._listField.Add("PayUserName");
					GatheringRecInfo._listField.Add("AutoTimeStamp");
				}
				return GatheringRecInfo._listField;
			}
		}
	}
}
