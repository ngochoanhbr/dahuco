using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum RoleStatus
	{
		[Description("被管理帐号引用")]
		RefByAccount = 1,
		[Description("发生未知错误")]
		Error = 0,
		[Description("成功")]
		Success = 99
	}
}
