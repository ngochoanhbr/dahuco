using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class WxMenuInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _RootID = 0;

		private int _ParentID = 0;

		private string _Type = string.Empty;

		private string _Name = string.Empty;

		private string _EventKey = string.Empty;

		private string _Url = string.Empty;

		private int _ChildCount = 0;

		private string _ChildIDs = string.Empty;

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

		public int RootID
		{
			get
			{
				return this._RootID;
			}
			set
			{
				this._RootID = value;
			}
		}

		public int ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				this._ParentID = value;
			}
		}

		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		public string EventKey
		{
			get
			{
				return this._EventKey;
			}
			set
			{
				this._EventKey = value;
			}
		}

		public string Url
		{
			get
			{
				return this._Url;
			}
			set
			{
				this._Url = value;
			}
		}

		public int ChildCount
		{
			get
			{
				return this._ChildCount;
			}
			set
			{
				this._ChildCount = value;
			}
		}

		public string ChildIDs
		{
			get
			{
				return this._ChildIDs;
			}
			set
			{
				this._ChildIDs = value;
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
				return "weixin_WxMenu";
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
				return "AutoID,RootID,ParentID,Type,Name,EventKey,Url,ChildCount,ChildIDs,Sort,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (WxMenuInfo._listField == null)
				{
					WxMenuInfo._listField = new List<string>();
					WxMenuInfo._listField.Add("AutoID");
					WxMenuInfo._listField.Add("RootID");
					WxMenuInfo._listField.Add("ParentID");
					WxMenuInfo._listField.Add("Type");
					WxMenuInfo._listField.Add("Name");
					WxMenuInfo._listField.Add("EventKey");
					WxMenuInfo._listField.Add("Url");
					WxMenuInfo._listField.Add("ChildCount");
					WxMenuInfo._listField.Add("ChildIDs");
					WxMenuInfo._listField.Add("Sort");
					WxMenuInfo._listField.Add("AutoTimeStamp");
				}
				return WxMenuInfo._listField;
			}
		}
	}
}
