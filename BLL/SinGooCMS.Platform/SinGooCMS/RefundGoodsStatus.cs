using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum RefundGoodsStatus
	{
		[Description("申请退货")]
		InApply,
		[Description("拒绝退货")]
		Refused2Return = -1,
		[Description("同意退货")]
		AgreeRefund = 1
	}
}
