using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum EventType
	{
		[Description("登录日志")]
		Login = 1,
		[Description("管理日志")]
		Manage,
		[Description("系统日志")]
		System
	}
}
