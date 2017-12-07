using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class AccountInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _AccountName = string.Empty;

		private string _Password = string.Empty;

		private string _Roles = string.Empty;

		private string _Email = string.Empty;

		private string _Mobile = string.Empty;

		private bool _IsSystem = false;

		private int _LoginCount = 0;

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

		public string AccountName
		{
			get
			{
				return this._AccountName;
			}
			set
			{
				this._AccountName = value;
			}
		}

		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
			}
		}

		public string Roles
		{
			get
			{
				return this._Roles;
			}
			set
			{
				this._Roles = value;
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

		public string Mobile
		{
			get
			{
				return this._Mobile;
			}
			set
			{
				this._Mobile = value;
			}
		}

		public bool IsSystem
		{
			get
			{
				return this._IsSystem;
			}
			set
			{
				this._IsSystem = value;
			}
		}

		public int LoginCount
		{
			get
			{
				return this._LoginCount;
			}
			set
			{
				this._LoginCount = value;
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
				return "sys_Account";
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
				return "AutoID,AccountName,Password,Roles,Email,Mobile,IsSystem,LoginCount,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (AccountInfo._listField == null)
				{
					AccountInfo._listField = new List<string>();
					AccountInfo._listField.Add("AutoID");
					AccountInfo._listField.Add("AccountName");
					AccountInfo._listField.Add("Password");
					AccountInfo._listField.Add("Roles");
					AccountInfo._listField.Add("Email");
					AccountInfo._listField.Add("Mobile");
					AccountInfo._listField.Add("IsSystem");
					AccountInfo._listField.Add("LoginCount");
					AccountInfo._listField.Add("AutoTimeStamp");
				}
				return AccountInfo._listField;
			}
		}
	}
}
