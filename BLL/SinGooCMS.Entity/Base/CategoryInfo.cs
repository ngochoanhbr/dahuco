using SinGooCMS.Config;
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class CategoryInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _CategoryName = string.Empty;

		private int _ModelID = 0;

		private int _ParentID = 0;

		private string _ParentPath = string.Empty;

		private int _Depth = 0;

		private int _RootID = 0;

		private int _ChildCount = 0;

		private string _ChildList = string.Empty;

		private string _UrlRewriteName = string.Empty;

		private string _CategoryImage = string.Empty;

		private string _SeoKey = string.Empty;

		private string _SeoDescription = string.Empty;

		private int _ItemPageSize = 0;

		private string _Remark = string.Empty;

		private bool _IsTop = false;

		private bool _IsRecommend = false;

		private bool _IsNew = false;

		private string _Creator = string.Empty;

		private int _Sort = 0;

		private string _Lang = string.Empty;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

		private string _CateUrl = string.Empty;

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

		public string CategoryName
		{
			get
			{
				return this._CategoryName;
			}
			set
			{
				this._CategoryName = value;
			}
		}

		public int ModelID
		{
			get
			{
				return this._ModelID;
			}
			set
			{
				this._ModelID = value;
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

		public string ParentPath
		{
			get
			{
				return this._ParentPath;
			}
			set
			{
				this._ParentPath = value;
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

		public string ChildList
		{
			get
			{
				return this._ChildList;
			}
			set
			{
				this._ChildList = value;
			}
		}

		public string UrlRewriteName
		{
			get
			{
				return this._UrlRewriteName;
			}
			set
			{
				this._UrlRewriteName = value;
			}
		}

		public string CategoryImage
		{
			get
			{
				return this._CategoryImage;
			}
			set
			{
				this._CategoryImage = value;
			}
		}

		public string SeoKey
		{
			get
			{
				return this._SeoKey;
			}
			set
			{
				this._SeoKey = value;
			}
		}

		public string SeoDescription
		{
			get
			{
				return this._SeoDescription;
			}
			set
			{
				this._SeoDescription = value;
			}
		}

		public int ItemPageSize
		{
			get
			{
				return this._ItemPageSize;
			}
			set
			{
				this._ItemPageSize = value;
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

		public bool IsTop
		{
			get
			{
				return this._IsTop;
			}
			set
			{
				this._IsTop = value;
			}
		}

		public bool IsRecommend
		{
			get
			{
				return this._IsRecommend;
			}
			set
			{
				this._IsRecommend = value;
			}
		}

		public bool IsNew
		{
			get
			{
				return this._IsNew;
			}
			set
			{
				this._IsNew = value;
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
				return "shop_Category";
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
				return "AutoID,CategoryName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,CategoryImage,SeoKey,SeoDescription,ItemPageSize,Remark,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (CategoryInfo._listField == null)
				{
					CategoryInfo._listField = new List<string>();
					CategoryInfo._listField.Add("AutoID");
					CategoryInfo._listField.Add("CategoryName");
					CategoryInfo._listField.Add("ModelID");
					CategoryInfo._listField.Add("ParentID");
					CategoryInfo._listField.Add("ParentPath");
					CategoryInfo._listField.Add("Depth");
					CategoryInfo._listField.Add("RootID");
					CategoryInfo._listField.Add("ChildCount");
					CategoryInfo._listField.Add("ChildList");
					CategoryInfo._listField.Add("UrlRewriteName");
					CategoryInfo._listField.Add("CategoryImage");
					CategoryInfo._listField.Add("SeoKey");
					CategoryInfo._listField.Add("SeoDescription");
					CategoryInfo._listField.Add("ItemPageSize");
					CategoryInfo._listField.Add("Remark");
					CategoryInfo._listField.Add("IsTop");
					CategoryInfo._listField.Add("IsRecommend");
					CategoryInfo._listField.Add("IsNew");
					CategoryInfo._listField.Add("Creator");
					CategoryInfo._listField.Add("Sort");
					CategoryInfo._listField.Add("Lang");
					CategoryInfo._listField.Add("AutoTimeStamp");
				}
				return CategoryInfo._listField;
			}
		}

		public string CateUrl
		{
			get
			{
				if (string.IsNullOrEmpty(this._CateUrl))
				{
					this._CateUrl = this.GetCateUrl();
				}
				return this._CateUrl;
			}
		}

		public int GoodsCount
		{
			get;
			set;
		}

		private string GetCateUrl()
		{
			string text = "/shop/category.aspx?cid=" + this.AutoID.ToString();
			string str = string.IsNullOrEmpty(this.UrlRewriteName) ? this.AutoID.ToString() : this.UrlRewriteName;
			switch ((BrowseType)Enum.Parse(typeof(BrowseType), ConfigProvider.Configs.BrowseType))
			{
			case BrowseType.UrlRewriteAndAspx:
				text = "/shop/" + str + ".aspx";
				break;
			case BrowseType.UrlRewriteNoAspx:
				text = "/shop/" + str;
				break;
			case BrowseType.HtmlRewrite:
				text = "/shop/" + str + ".html";
				break;
			case BrowseType.Html:
				text = "/html/shop/" + str + ConfigProvider.Configs.HtmlFileExt;
				break;
			}
			return text.Replace("//", "/");
		}
	}
}
