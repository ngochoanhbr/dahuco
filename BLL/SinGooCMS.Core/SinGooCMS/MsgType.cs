using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum MsgType
	{
		[Description("系统消息")]
		SystemMsg = 1,
		[Description("管理员消息")]
		ManagerMsg,
		[Description("用户消息")]
		UserMsg
	}
}
