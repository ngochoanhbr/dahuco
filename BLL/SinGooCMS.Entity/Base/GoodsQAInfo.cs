using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GoodsQAInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _ProductID = 0;

		private string _ProductName = string.Empty;

		private string _UserName = string.Empty;

		private string _Question = string.Empty;

		private string _Answer = string.Empty;

		private bool _IsShow = false;

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

		public int ProductID
		{
			get
			{
				return this._ProductID;
			}
			set
			{
				this._ProductID = value;
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

		public string Question
		{
			get
			{
				return this._Question;
			}
			set
			{
				this._Question = value;
			}
		}

		public string Answer
		{
			get
			{
				return this._Answer;
			}
			set
			{
				this._Answer = value;
			}
		}

		public bool IsShow
		{
			get
			{
				return this._IsShow;
			}
			set
			{
				this._IsShow = value;
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
				return "shop_GoodsQA";
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
				return "AutoID,ProductID,ProductName,UserName,Question,Answer,IsShow,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (GoodsQAInfo._listField == null)
				{
					GoodsQAInfo._listField = new List<string>();
					GoodsQAInfo._listField.Add("AutoID");
					GoodsQAInfo._listField.Add("ProductID");
					GoodsQAInfo._listField.Add("ProductName");
					GoodsQAInfo._listField.Add("UserName");
					GoodsQAInfo._listField.Add("Question");
					GoodsQAInfo._listField.Add("Answer");
					GoodsQAInfo._listField.Add("IsShow");
					GoodsQAInfo._listField.Add("AutoTimeStamp");
				}
				return GoodsQAInfo._listField;
			}
		}
	}
}
