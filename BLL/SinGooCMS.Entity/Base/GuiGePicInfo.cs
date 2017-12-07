using SinGooCMS.Utility;
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class GuiGePicInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private int _ProID = 0;

		private int _ClassID = 0;

		private string _GuiGeName = string.Empty;

		private string _ImagesCollection = string.Empty;

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

		public int ClassID
		{
			get
			{
				return this._ClassID;
			}
			set
			{
				this._ClassID = value;
			}
		}

		public string GuiGeName
		{
			get
			{
				return this._GuiGeName;
			}
			set
			{
				this._GuiGeName = value;
			}
		}

		public string ImagesCollection
		{
			get
			{
				return this._ImagesCollection;
			}
			set
			{
				this._ImagesCollection = value;
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
				return "shop_GuiGePic";
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
				return "AutoID,ProID,ClassID,GuiGeName,ImagesCollection,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (GuiGePicInfo._listField == null)
				{
					GuiGePicInfo._listField = new List<string>();
					GuiGePicInfo._listField.Add("AutoID");
					GuiGePicInfo._listField.Add("ProID");
					GuiGePicInfo._listField.Add("ClassID");
					GuiGePicInfo._listField.Add("GuiGeName");
					GuiGePicInfo._listField.Add("ImagesCollection");
					GuiGePicInfo._listField.Add("AutoTimeStamp");
				}
				return GuiGePicInfo._listField;
			}
		}

		public List<GuiGeItemImage> ImagesCollections
		{
			get
			{
				List<GuiGeItemImage> result;
				if (!string.IsNullOrEmpty(this.ImagesCollection))
				{
					result = JsonUtils.JsonToObject<List<GuiGeItemImage>>(this.ImagesCollection);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}
	}
}
