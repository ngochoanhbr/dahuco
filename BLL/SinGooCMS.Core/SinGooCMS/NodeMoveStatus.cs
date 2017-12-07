using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum NodeMoveStatus
	{
		[Description("移动栏目出错")]
		Error,
		[Description("源节点不存在")]
		SourceNodeNotExist
	}
}
