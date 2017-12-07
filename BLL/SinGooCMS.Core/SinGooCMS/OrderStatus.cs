using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum OrderStatus
	{
		[Description("等待审核")]
		WaitAudit,
		[Description("等待付款")]
		WaitPay,
		[Description("等待发货")]
		WaitDelivery = 10,
		[Description("等待收货")]
		WaitSignGoods,
		[Description("交易完成")]
		OrderSuccess = 99,
		[Description("交易关闭")]
		OrderCancel = 101
	}
}
