using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class EvaluationInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _OrderID = 0;

		private int _ProID = 0;

		private string _ProName = string.Empty;

		private int _UserID = 0;

		private string _UserName = string.Empty;

		private int _Start = 0;

		private string _Content = string.Empty;

		private string _ReplyContent = string.Empty;

		private string _Replier = string.Empty;

		private DateTime _ReplyTime = new DateTime(1900, 1, 1);

		private bool _IsAudit = false;

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

		public int OrderID
		{
			get
			{
				return this._OrderID;
			}
			set
			{
				this._OrderID = value;
			}
		}

		public int ProID
		{
			get
			{
				return this._ProID;
			}
			set
			{
				this._ProID = value;
			}
		}

		public string ProName
		{
			get
			{
				return this._ProName;
			}
			set
			{
				this._ProName = value;
			}
		}

		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
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

		public int Start
		{
			get
			{
				return this._Start;
			}
			set
			{
				this._Start = value;
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

		public DateTime ReplyTime
		{
			get
			{
				return this._ReplyTime;
			}
			set
			{
				this._ReplyTime = value;
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
				return "shop_Evaluation";
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
				return "AutoID,OrderID,ProID,ProName,UserID,UserName,Start,Content,ReplyContent,Replier,ReplyTime,IsAudit,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (EvaluationInfo._listField == null)
				{
					EvaluationInfo._listField = new List<string>();
					EvaluationInfo._listField.Add("AutoID");
					EvaluationInfo._listField.Add("OrderID");
					EvaluationInfo._listField.Add("ProID");
					EvaluationInfo._listField.Add("ProName");
					EvaluationInfo._listField.Add("UserID");
					EvaluationInfo._listField.Add("UserName");
					EvaluationInfo._listField.Add("Start");
					EvaluationInfo._listField.Add("Content");
					EvaluationInfo._listField.Add("ReplyContent");
					EvaluationInfo._listField.Add("Replier");
					EvaluationInfo._listField.Add("ReplyTime");
					EvaluationInfo._listField.Add("IsAudit");
					EvaluationInfo._listField.Add("Lang");
					EvaluationInfo._listField.Add("AutoTimeStamp");
				}
				return EvaluationInfo._listField;
			}
		}
	}
}
