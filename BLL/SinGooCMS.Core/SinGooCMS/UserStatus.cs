using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum UserStatus
	{
		[Description("发生未知报错")]
		Error = -1,
		[Description("用户名非法")]
		UserNameNoValidate = 1,
		[Description("邮箱地址非法")]
		EmailNoValidate,
		[Description("手机号码非法")]
		MobileNoValidate,
		[Description("用户名已存在")]
		ExistsUserName,
		[Description("邮箱地址已存在")]
		ExistsEmail,
		[Description("手机号码已存在")]
		ExistsMobile,
		[Description("待审核")]
		WaitingForAudit = 0,
		[Description("已审核")]
		Success = 99
	}
}
