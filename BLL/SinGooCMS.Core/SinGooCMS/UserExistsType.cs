using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum UserExistsType
	{
		[Description("不存在")]
		NoExists,
		[Description("用户名已存在")]
		UserName,
		[Description("用户邮箱已存在")]
		Email,
		[Description("用户手机已存在")]
		Mobile
	}
}
