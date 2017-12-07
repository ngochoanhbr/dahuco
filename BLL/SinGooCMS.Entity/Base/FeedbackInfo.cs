using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class FeedbackInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _UserName = string.Empty;

		private string _Title = string.Empty;

		private string _Content = string.Empty;

		private string _IPaddress = string.Empty;

		private string _Email = string.Empty;

		private string _Telephone = string.Empty;

		private string _Mobile = string.Empty;

		private string _Replier = string.Empty;

		private string _ReplyContent = string.Empty;

		private DateTime _ReplyDate = new DateTime(1900, 1, 1);

		private bool _IsAudit = false;

		private int _Sort = 0;

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

		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}

		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				this._Content = value;
			}
		}

		public string IPaddress
		{
			get
			{
				return this._IPaddress;
			}
			set
			{
				this._IPaddress = value;
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

		public string Telephone
		{
			get
			{
				return this._Telephone;
			}
			set
			{
				this._Telephone = value;
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

		public string Replier
		{
			get
			{
				return this._Replier;
			}
			set
			{
				this._Replier = value;
			}
		}

		public string ReplyContent
		{
			get
			{
				return this._ReplyContent;
			}
			set
			{
				this._ReplyContent = value;
			}
		}

		public DateTime ReplyDate
		{
			get
			{
				return this._ReplyDate;
			}
			set
			{
				this._ReplyDate = value;
			}
		}

		public bool IsAudit
		{
			get
			{
				return this._IsAudit;
			}
			set
			{
				this._IsAudit = value;
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
				return "cms_Feedback";
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
				return "AutoID,UserName,Title,Content,IPaddress,Email,Telephone,Mobile,Replier,ReplyContent,ReplyDate,IsAudit,Sort,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (FeedbackInfo._listField == null)
				{
					FeedbackInfo._listField = new List<string>();
					FeedbackInfo._listField.Add("AutoID");
					FeedbackInfo._listField.Add("UserName");
					FeedbackInfo._listField.Add("Title");
					FeedbackInfo._listField.Add("Content");
					FeedbackInfo._listField.Add("IPaddress");
					FeedbackInfo._listField.Add("Email");
					FeedbackInfo._listField.Add("Telephone");
					FeedbackInfo._listField.Add("Mobile");
					FeedbackInfo._listField.Add("Replier");
					FeedbackInfo._listField.Add("ReplyContent");
					FeedbackInfo._listField.Add("ReplyDate");
					FeedbackInfo._listField.Add("IsAudit");
					FeedbackInfo._listField.Add("Sort");
					FeedbackInfo._listField.Add("Lang");
					FeedbackInfo._listField.Add("AutoTimeStamp");
				}
				return FeedbackInfo._listField;
			}
		}
	}
}
