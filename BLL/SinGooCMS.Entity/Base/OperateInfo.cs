using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class OperateInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _ModuleID = 0;

		private string _OperateName = string.Empty;

		private string _OperateCode = string.Empty;

		private string _Remark = string.Empty;

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

		public int ModuleID
		{
			get
			{
				return this._ModuleID;
			}
			set
			{
				this._ModuleID = value;
			}
		}

		public string OperateName
		{
			get
			{
				return this._OperateName;
			}
			set
			{
				this._OperateName = value;
			}
		}

		public string OperateCode
		{
			get
			{
				return this._OperateCode;
			}
			set
			{
				this._OperateCode = value;
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
				return "sys_Operate";
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
				return "AutoID,ModuleID,OperateName,OperateCode,Remark,Sort,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (OperateInfo._listField == null)
				{
					OperateInfo._listField = new List<string>();
					OperateInfo._listField.Add("AutoID");
					OperateInfo._listField.Add("ModuleID");
					OperateInfo._listField.Add("OperateName");
					OperateInfo._listField.Add("OperateCode");
					OperateInfo._listField.Add("Remark");
					OperateInfo._listField.Add("Sort");
					OperateInfo._listField.Add("AutoTimeStamp");
				}
				return OperateInfo._listField;
			}
		}
	}
}
