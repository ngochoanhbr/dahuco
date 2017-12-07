using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum LoginStatus
	{
		[Description("用户名不正确")]
		UserNameIncorrect = -3,
		[Description("邮箱地址不正确")]
		EmailIncorrect,
		[Description("手机号码不正确")]
		MobileIncorrect,
		[Description("密码不正确")]
		PasswordIncorrect,
		[Description("连续登录失败多次")]
		MutilLoginFail = 5,
		[Description("会员状态不正确")]
		StatusNotAllow = 10,
		[Description("成功")]
		Success = 99
	}
}
