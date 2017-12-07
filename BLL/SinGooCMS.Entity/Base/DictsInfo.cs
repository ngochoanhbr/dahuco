using SinGooCMS.Utility;
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class DictsInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _DictName = string.Empty;

		private string _DisplayName = string.Empty;

		private string _DictValue = string.Empty;

		private bool _IsSystem = false;

		private static List<string> _listField = null;

		public List<DictItemInfo> Items
		{
			get
			{
				List<DictItemInfo> result;
				if (!string.IsNullOrEmpty(this.DictValue))
				{
					result = JsonUtils.JsonToObject<List<DictItemInfo>>(this.DictValue);
				}
				else
				{
					result = new List<DictItemInfo>();
				}
				return result;
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

		public string DictName
		{
			get
			{
				return this._DictName;
			}
			set
			{
				this._DictName = value;
			}
		}

		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
			}
		}

		public string DictValue
		{
			get
			{
				return this._DictValue;
			}
			set
			{
				this._DictValue = value;
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

		public string DBTableName
		{
			get
			{
				return "sys_Dicts";
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
				return "AutoID,DictName,DisplayName,DictValue,IsSystem";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (DictsInfo._listField == null)
				{
					DictsInfo._listField = new List<string>();
					DictsInfo._listField.Add("AutoID");
					DictsInfo._listField.Add("DictName");
					DictsInfo._listField.Add("DisplayName");
					DictsInfo._listField.Add("DictValue");
					DictsInfo._listField.Add("IsSystem");
				}
				return DictsInfo._listField;
			}
		}
	}
}
