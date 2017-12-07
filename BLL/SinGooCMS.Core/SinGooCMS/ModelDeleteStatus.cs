using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum ModelDeleteStatus
	{
		[Description("发生未知错误")]
		Error,
		[Description("模型不存在")]
		ModelNotExists,
		[Description("正被栏目引用")]
		NodesRef,
		[Description("正被内容引用")]
		ContentRef,
		[Description("正被会员引用")]
		UserRef,
		[Description("成功")]
		Success = 99
	}
}
