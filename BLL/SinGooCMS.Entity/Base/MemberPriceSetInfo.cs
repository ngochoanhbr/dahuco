using System;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class MemberPriceSetInfo
	{
		public int UserLevelID
		{
			get;
			set;
		}

		public string UserLevelName
		{
			get;
			set;
		}

		public decimal Price
		{
			get;
			set;
		}

		public decimal DiscoutPrice
		{
			get;
			set;
		}
	}
}
