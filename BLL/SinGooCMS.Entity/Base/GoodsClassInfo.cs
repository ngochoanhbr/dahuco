using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GoodsClassInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _ClassName = string.Empty;

		private int _ParentID = 0;

		private int _Depth = 0;

		private int _RootID = 0;

		private int _ChildCount = 0;

		private string _GuiGeCollection = string.Empty;

		private int _Sort = 0;

		private string _Lang = string.Empty;

		private string _Creator = string.Empty;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

		public List<GuiGeSet> GeiGeSets
		{
			get
			{
				return JsonUtils.JsonToObject<List<GuiGeSet>>(this.GuiGeCollection);
			}
		}

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

		public string ClassName
		{
			get
			{
				return this._ClassName;
			}
			set
			{
				this._ClassName = value;
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

		public int Depth
		{
			get
			{
				return this._Depth;
			}
			set
			{
				this._Depth = value;
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

		public string GuiGeCollection
		{
			get
			{
				return this._GuiGeCollection;
			}
			set
			{
				this._GuiGeCollection = value;
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

		public string Creator
		{
			get
			{
				return this._Creator;
			}
			set
			{
				this._Creator = value;
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
				return "shop_GoodsClass";
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
				return "AutoID,ClassName,ParentID,Depth,RootID,ChildCount,GuiGeCollection,Sort,Lang,Creator,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (GoodsClassInfo._listField == null)
				{
					GoodsClassInfo._listField = new List<string>();
					GoodsClassInfo._listField.Add("AutoID");
					GoodsClassInfo._listField.Add("ClassName");
					GoodsClassInfo._listField.Add("ParentID");
					GoodsClassInfo._listField.Add("Depth");
					GoodsClassInfo._listField.Add("RootID");
					GoodsClassInfo._listField.Add("ChildCount");
					GoodsClassInfo._listField.Add("GuiGeCollection");
					GoodsClassInfo._listField.Add("Sort");
					GoodsClassInfo._listField.Add("Lang");
					GoodsClassInfo._listField.Add("Creator");
					GoodsClassInfo._listField.Add("AutoTimeStamp");
				}
				return GoodsClassInfo._listField;
			}
		}

		public GuiGeSet GetGuiGeSet(string key)
		{
			return (from p in this.GeiGeSets
			where p.GuiGeName == key
			select p).FirstOrDefault<GuiGeSet>();
		}
	}
}
