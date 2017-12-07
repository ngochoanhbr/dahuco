using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum PrefRangeType
	{
		[Description("以下分类")]
		ByCategory,
		[Description("以下品牌")]
		ByBrand,
		[Description("以下商品")]
		ByProduct
	}
}
