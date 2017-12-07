using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinGooCMS.BLL
{
	public class MemberPriceSet
	{
		public static List<MemberPriceSetInfo> GetDefault(decimal defPrice)
		{
			return MemberPriceSet.GetList(null, defPrice);
		}

		public static List<MemberPriceSetInfo> GetList(string memberPriceSet, decimal defPrice)
		{
			List<MemberPriceSetInfo> list = new List<MemberPriceSetInfo>();
			IList<UserLevelInfo> list2 = (from p in UserLevel.GetCacheUserLevelList()
			orderby p.Integral
			select p).ToList<UserLevelInfo>();
			List<MemberPriceSetInfo> list3 = null;
			if (!string.IsNullOrEmpty(memberPriceSet))
			{
				list3 = JsonUtils.JsonToObject<List<MemberPriceSetInfo>>(memberPriceSet);
			}
			foreach (UserLevelInfo item in list2)
			{
				decimal price = 0.0m;
				if (list3 != null && list3.Count > 0)
				{
					IEnumerable<MemberPriceSetInfo> enumerable = from p in list3
					where p.UserLevelID.Equals(item.AutoID)
					select p;
					if (enumerable != null && enumerable.Count<MemberPriceSetInfo>() > 0)
					{
						price = enumerable.First<MemberPriceSetInfo>().Price;
					}
				}
				list.Add(new MemberPriceSetInfo
				{
					UserLevelID = item.AutoID,
					UserLevelName = item.LevelName,
					Price = price,
					DiscoutPrice = defPrice * WebUtils.GetDecimal(item.Discount)
				});
			}
			return list;
		}
	}
}
