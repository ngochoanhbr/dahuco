using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum IPStrategyType
	{
		[Description("拒绝访问")]
		DENY,
		[Description("允许访问")]
		ALLOW
	}
}
