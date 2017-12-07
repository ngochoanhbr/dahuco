using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum NodeUpdateStatus
	{
		[Description("主栏目数量超出最大值")]
		ToMoreNode = -1,
		[Description("发生未知错误")]
		Error,
		[Description("栏目名称已经存在")]
		ExistsNodeName,
		[Description("栏目标识已存在")]
		ExistsNodeIdentifier,
		[Description("栏目目录已存在")]
		ExistsNodeDir,
		[Description("不允许为自己的子栏目")]
		UnNodeSelf,
		[Description("修改栏目成功")]
		Success = 99
	}
}
