using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class DingYueLogInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _SendEmail = string.Empty;

		private string _MailTitle = string.Empty;

		private string _MailBody = string.Empty;

		private int _SendCount = 0;

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

		public string SendEmail
		{
			get
			{
				return this._SendEmail;
			}
			set
			{
				this._SendEmail = value;
			}
		}

		public string MailTitle
		{
			get
			{
				return this._MailTitle;
			}
			set
			{
				this._MailTitle = value;
			}
		}

		public string MailBody
		{
			get
			{
				return this._MailBody;
			}
			set
			{
				this._MailBody = value;
			}
		}

		public int SendCount
		{
			get
			{
				return this._SendCount;
			}
			set
			{
				this._SendCount = value;
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
				return "cms_DingYueLog";
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
				return "AutoID,SendEmail,MailTitle,MailBody,SendCount,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (DingYueLogInfo._listField == null)
				{
					DingYueLogInfo._listField = new List<string>();
					DingYueLogInfo._listField.Add("AutoID");
					DingYueLogInfo._listField.Add("SendEmail");
					DingYueLogInfo._listField.Add("MailTitle");
					DingYueLogInfo._listField.Add("MailBody");
					DingYueLogInfo._listField.Add("SendCount");
					DingYueLogInfo._listField.Add("Lang");
					DingYueLogInfo._listField.Add("AutoTimeStamp");
				}
				return DingYueLogInfo._listField;
			}
		}
	}
}
