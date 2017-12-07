using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class MessageInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _MsgType = string.Empty;

		private string _SenderType = string.Empty;

		private string _Sender = string.Empty;

		private string _ReceiverType = string.Empty;

		private string _Receiver = string.Empty;

		private string _MsgTitle = string.Empty;

		private string _MsgBody = string.Empty;

		private DateTime _SendTime = new DateTime(1900, 1, 1);

		private bool _IsRead = false;

		private DateTime _ReadTime = new DateTime(1900, 1, 1);

		private string _Lang = string.Empty;

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

		public string MsgType
		{
			get
			{
				return this._MsgType;
			}
			set
			{
				this._MsgType = value;
			}
		}

		public string SenderType
		{
			get
			{
				return this._SenderType;
			}
			set
			{
				this._SenderType = value;
			}
		}

		public string Sender
		{
			get
			{
				return this._Sender;
			}
			set
			{
				this._Sender = value;
			}
		}

		public string ReceiverType
		{
			get
			{
				return this._ReceiverType;
			}
			set
			{
				this._ReceiverType = value;
			}
		}

		public string Receiver
		{
			get
			{
				return this._Receiver;
			}
			set
			{
				this._Receiver = value;
			}
		}

		public string MsgTitle
		{
			get
			{
				return this._MsgTitle;
			}
			set
			{
				this._MsgTitle = value;
			}
		}

		public string MsgBody
		{
			get
			{
				return this._MsgBody;
			}
			set
			{
				this._MsgBody = value;
			}
		}

		public DateTime SendTime
		{
			get
			{
				return this._SendTime;
			}
			set
			{
				this._SendTime = value;
			}
		}

		public bool IsRead
		{
			get
			{
				return this._IsRead;
			}
			set
			{
				this._IsRead = value;
			}
		}

		public DateTime ReadTime
		{
			get
			{
				return this._ReadTime;
			}
			set
			{
				this._ReadTime = value;
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

		public string DBTableName
		{
			get
			{
				return "sys_Message";
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
				return "AutoID,MsgType,SenderType,Sender,ReceiverType,Receiver,MsgTitle,MsgBody,SendTime,IsRead,ReadTime,Lang";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (MessageInfo._listField == null)
				{
					MessageInfo._listField = new List<string>();
					MessageInfo._listField.Add("AutoID");
					MessageInfo._listField.Add("MsgType");
					MessageInfo._listField.Add("SenderType");
					MessageInfo._listField.Add("Sender");
					MessageInfo._listField.Add("ReceiverType");
					MessageInfo._listField.Add("Receiver");
					MessageInfo._listField.Add("MsgTitle");
					MessageInfo._listField.Add("MsgBody");
					MessageInfo._listField.Add("SendTime");
					MessageInfo._listField.Add("IsRead");
					MessageInfo._listField.Add("ReadTime");
					MessageInfo._listField.Add("Lang");
				}
				return MessageInfo._listField;
			}
		}
	}
}
