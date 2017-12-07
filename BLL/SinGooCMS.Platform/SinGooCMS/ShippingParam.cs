using System;
using System.Collections.Generic;

namespace SinGooCMS
{
	public class ShippingParam
	{
		public int addrid
		{
			get;
			set;
		}

		public decimal totalfee
		{
			get;
			set;
		}

		public System.Collections.Generic.List<ProAndNum> pros
		{
			get;
			set;
		}
	}
}
