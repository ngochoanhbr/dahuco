using SinGooCMS.Utility;
using System;
using System.Data;

namespace SinGooCMS.BLL
{
	public class OrderStatusSTAT
	{
		private delegate DataTable GetSTATDelegate(int intUserID);

		private DataTable dt = new DataTable();

		private OrderStatusSTAT.GetSTATDelegate gs = new OrderStatusSTAT.GetSTATDelegate(Orders.GetOrderStatusSTAT);

		public int AllCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["AllCount"]) : 0;
			}
			private set
			{
			}
		}

		public int WaitAuditCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["DaiSHeCount"]) : 0;
			}
			private set
			{
			}
		}

		public int WaitPayCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["DaiFKCount"]) : 0;
			}
			private set
			{
			}
		}

		public int WaitSendGoodsCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["DaiFHCount"]) : 0;
			}
			private set
			{
			}
		}

		public int WaitSignGoodsCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["DaiSHCount"]) : 0;
			}
			private set
			{
			}
		}

		public int WaitEvaCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["DaiPJCount"]) : 0;
			}
			private set
			{
			}
		}

		public int SuccessCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["SuccessCount"]) : 0;
			}
			private set
			{
			}
		}

		public int CancelCount
		{
			get
			{
				return (this.dt != null && this.dt.Rows.Count > 0) ? WebUtils.GetInt(this.dt.Rows[0]["CancelCount"]) : 0;
			}
			private set
			{
			}
		}

		public OrderStatusSTAT()
		{
			this.dt = this.gs(0);
		}

		public OrderStatusSTAT(int intUserID)
		{
			this.dt = this.gs(intUserID);
		}
	}
}
