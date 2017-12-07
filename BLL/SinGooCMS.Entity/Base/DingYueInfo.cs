using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class DingYueInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _UserName = string.Empty;

		private string _Email = string.Empty;

		private bool _IsTuiDing = false;

		private string _Lang = string.Empty;

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

		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
			}
		}

		public bool IsTuiDing
		{
			get
			{
				return this._IsTuiDing;
			}
			set
			{
				this._IsTuiDing = value;
			}
		}

		public string Lang
		{
			get
			{
				return this._Lang;
			}
			set
			{
				this._Lang = value;
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
				return "cms_DingYue";
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
				return "AutoID,UserName,Email,IsTuiDing,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (DingYueInfo._listField == null)
				{
					DingYueInfo._listField = new List<string>();
					DingYueInfo._listField.Add("AutoID");
					DingYueInfo._listField.Add("UserName");
					DingYueInfo._listField.Add("Email");
					DingYueInfo._listField.Add("IsTuiDing");
					DingYueInfo._listField.Add("Lang");
					DingYueInfo._listField.Add("AutoTimeStamp");
				}
				return DingYueInfo._listField;
			}
		}
	}
}
