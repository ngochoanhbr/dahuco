using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class ExchangeApplyInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _OrderNo = string.Empty;

		private string _GoodsName = string.Empty;

		private string _Reason = string.Empty;

		private string _Consignee = string.Empty;

		private string _ShippingAddr = string.Empty;

		private string _ContactPhone = string.Empty;

		private string _AdminRemark = string.Empty;

		private int _Status = 0;

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

		public string GoodsName
		{
			get
			{
				return this._GoodsName;
			}
			set
			{
				this._GoodsName = value;
			}
		}

		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
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

		public string ShippingAddr
		{
			get
			{
				return this._ShippingAddr;
			}
			set
			{
				this._ShippingAddr = value;
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

		public string AdminRemark
		{
			get
			{
				return this._AdminRemark;
			}
			set
			{
				this._AdminRemark = value;
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
				return "shop_ExchangeApply";
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
				return "AutoID,OrderNo,GoodsName,Reason,Consignee,ShippingAddr,ContactPhone,AdminRemark,Status,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (ExchangeApplyInfo._listField == null)
				{
					ExchangeApplyInfo._listField = new List<string>();
					ExchangeApplyInfo._listField.Add("AutoID");
					ExchangeApplyInfo._listField.Add("OrderNo");
					ExchangeApplyInfo._listField.Add("GoodsName");
					ExchangeApplyInfo._listField.Add("Reason");
					ExchangeApplyInfo._listField.Add("Consignee");
					ExchangeApplyInfo._listField.Add("ShippingAddr");
					ExchangeApplyInfo._listField.Add("ContactPhone");
					ExchangeApplyInfo._listField.Add("AdminRemark");
					ExchangeApplyInfo._listField.Add("Status");
					ExchangeApplyInfo._listField.Add("AutoTimeStamp");
				}
				return ExchangeApplyInfo._listField;
			}
		}
	}
}
