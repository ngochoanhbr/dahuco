using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum ContStatus
	{
		[Description("待审核")]
		WaittingAudit,
		[Description("回收站")]
		Recycle = -1,
		[Description("审核不通过")]
		AuditFail = -2,
		[Description("审核通过")]
		AuditSuccess = 99
	}
}
