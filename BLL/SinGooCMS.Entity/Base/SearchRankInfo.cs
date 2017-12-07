using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class SearchRankInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _SearchKey = string.Empty;

		private int _Times = 0;

		private bool _IsRecommend = false;

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

		public string SearchKey
		{
			get
			{
				return this._SearchKey;
			}
			set
			{
				this._SearchKey = value;
			}
		}

		public int Times
		{
			get
			{
				return this._Times;
			}
			set
			{
				this._Times = value;
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

		public string DBTableName
		{
			get
			{
				return "shop_SearchRank";
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
				return "AutoID,SearchKey,Times,IsRecommend";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (SearchRankInfo._listField == null)
				{
					SearchRankInfo._listField = new List<string>();
					SearchRankInfo._listField.Add("AutoID");
					SearchRankInfo._listField.Add("SearchKey");
					SearchRankInfo._listField.Add("Times");
					SearchRankInfo._listField.Add("IsRecommend");
				}
				return SearchRankInfo._listField;
			}
		}
	}
}
