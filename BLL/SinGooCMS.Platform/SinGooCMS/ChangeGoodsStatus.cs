using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum ChangeGoodsStatus
	{
		[Description("申请换货")]
		InApply,
		[Description("拒绝换货")]
		Refused2Change = -1,
		[Description("同意换货")]
		AgreeChange = 1
	}
}
