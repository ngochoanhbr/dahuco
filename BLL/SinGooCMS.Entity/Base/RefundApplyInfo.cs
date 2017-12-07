using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class RefundApplyInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _OrderNo = string.Empty;

		private string _ProductName = string.Empty;

		private string _Reason = string.Empty;

		private decimal _Amount = 0.0m;

		private string _RefundAmountType = string.Empty;

		private int _RefundGoodsNum = 0;

		private string _RefunderName = string.Empty;

		private string _RefunderPhone = string.Empty;

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

		public string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				this._ProductName = value;
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

		public decimal Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				this._Amount = value;
			}
		}

		public string RefundAmountType
		{
			get
			{
				return this._RefundAmountType;
			}
			set
			{
				this._RefundAmountType = value;
			}
		}

		public int RefundGoodsNum
		{
			get
			{
				return this._RefundGoodsNum;
			}
			set
			{
				this._RefundGoodsNum = value;
			}
		}

		public string RefunderName
		{
			get
			{
				return this._RefunderName;
			}
			set
			{
				this._RefunderName = value;
			}
		}

		public string RefunderPhone
		{
			get
			{
				return this._RefunderPhone;
			}
			set
			{
				this._RefunderPhone = value;
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
				return "shop_RefundApply";
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
				return "AutoID,OrderNo,ProductName,Reason,Amount,RefundAmountType,RefundGoodsNum,RefunderName,RefunderPhone,AdminRemark,Status,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (RefundApplyInfo._listField == null)
				{
					RefundApplyInfo._listField = new List<string>();
					RefundApplyInfo._listField.Add("AutoID");
					RefundApplyInfo._listField.Add("OrderNo");
					RefundApplyInfo._listField.Add("ProductName");
					RefundApplyInfo._listField.Add("Reason");
					RefundApplyInfo._listField.Add("Amount");
					RefundApplyInfo._listField.Add("RefundAmountType");
					RefundApplyInfo._listField.Add("RefundGoodsNum");
					RefundApplyInfo._listField.Add("RefunderName");
					RefundApplyInfo._listField.Add("RefunderPhone");
					RefundApplyInfo._listField.Add("AdminRemark");
					RefundApplyInfo._listField.Add("Status");
					RefundApplyInfo._listField.Add("AutoTimeStamp");
				}
				return RefundApplyInfo._listField;
			}
		}
	}
}
