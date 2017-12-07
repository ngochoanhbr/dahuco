using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum UserType
	{
		[Description("系统")]
		System,
		[Description("管理员")]
		Manager,
		[Description("会员")]
		User
	}
}
