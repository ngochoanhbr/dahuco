using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum NodeDeleteStatus
	{
		[Description("发生未知错误")]
		Error,
		[Description("栏目不存在或已经被删除")]
		NotExists,
		[Description("包含下级栏目")]
		HasChildNode,
		[Description("包含内容")]
		HasContent,
		[Description("删除栏目成功")]
		Success = 99
	}
}
