using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class LoginLogInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _UserType = string.Empty;

		private string _UserName = string.Empty;

		private string _IPAddress = string.Empty;

		private string _IPArea = string.Empty;

		private int _LoginStatus = 0;

		private int _LoginFailCount = 0;

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

		public string UserType
		{
			get
			{
				return this._UserType;
			}
			set
			{
				this._UserType = value;
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

		public string IPAddress
		{
			get
			{
				return this._IPAddress;
			}
			set
			{
				this._IPAddress = value;
			}
		}

		public string IPArea
		{
			get
			{
				return this._IPArea;
			}
			set
			{
				this._IPArea = value;
			}
		}

		public int LoginStatus
		{
			get
			{
				return this._LoginStatus;
			}
			set
			{
				this._LoginStatus = value;
			}
		}

		public int LoginFailCount
		{
			get
			{
				return this._LoginFailCount;
			}
			set
			{
				this._LoginFailCount = value;
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
				return "sys_LoginLog";
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
				return "AutoID,UserType,UserName,IPAddress,IPArea,LoginStatus,LoginFailCount,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (LoginLogInfo._listField == null)
				{
					LoginLogInfo._listField = new List<string>();
					LoginLogInfo._listField.Add("AutoID");
					LoginLogInfo._listField.Add("UserType");
					LoginLogInfo._listField.Add("UserName");
					LoginLogInfo._listField.Add("IPAddress");
					LoginLogInfo._listField.Add("IPArea");
					LoginLogInfo._listField.Add("LoginStatus");
					LoginLogInfo._listField.Add("LoginFailCount");
					LoginLogInfo._listField.Add("Lang");
					LoginLogInfo._listField.Add("AutoTimeStamp");
				}
				return LoginLogInfo._listField;
			}
		}
	}
}
