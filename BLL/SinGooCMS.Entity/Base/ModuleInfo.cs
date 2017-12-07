using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class ModuleInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _CatalogID = 0;

		private string _ModuleName = string.Empty;

		private string _ModuleCode = string.Empty;

		private string _FilePath = string.Empty;

		private string _ImagePath = string.Empty;

		private string _Remark = string.Empty;

		private bool _IsSystem = false;

		private bool _IsDefault = false;

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

		public int CatalogID
		{
			get
			{
				return this._CatalogID;
			}
			set
			{
				this._CatalogID = value;
			}
		}

		public string ModuleName
		{
			get
			{
				return this._ModuleName;
			}
			set
			{
				this._ModuleName = value;
			}
		}

		public string ModuleCode
		{
			get
			{
				return this._ModuleCode;
			}
			set
			{
				this._ModuleCode = value;
			}
		}

		public string FilePath
		{
			get
			{
				return this._FilePath;
			}
			set
			{
				this._FilePath = value;
			}
		}

		public string ImagePath
		{
			get
			{
				return this._ImagePath;
			}
			set
			{
				this._ImagePath = value;
			}
		}

		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				this._Remark = value;
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

		public bool IsDefault
		{
			get
			{
				return this._IsDefault;
			}
			set
			{
				this._IsDefault = value;
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
				return "sys_Module";
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
				return "AutoID,CatalogID,ModuleName,ModuleCode,FilePath,ImagePath,Remark,IsSystem,IsDefault,Sort,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (ModuleInfo._listField == null)
				{
					ModuleInfo._listField = new List<string>();
					ModuleInfo._listField.Add("AutoID");
					ModuleInfo._listField.Add("CatalogID");
					ModuleInfo._listField.Add("ModuleName");
					ModuleInfo._listField.Add("ModuleCode");
					ModuleInfo._listField.Add("FilePath");
					ModuleInfo._listField.Add("ImagePath");
					ModuleInfo._listField.Add("Remark");
					ModuleInfo._listField.Add("IsSystem");
					ModuleInfo._listField.Add("IsDefault");
					ModuleInfo._listField.Add("Sort");
					ModuleInfo._listField.Add("AutoTimeStamp");
				}
				return ModuleInfo._listField;
			}
		}
	}
}
