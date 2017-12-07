using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.WebUI.User
{
    public class OrderDetail : SinGooCMS.BLL.Custom.UIPageBase
	{
		private OrdersInfo order = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			int queryInt = WebUtils.GetQueryInt("oid");
			string queryString = WebUtils.GetQueryString("osn");
			if (queryInt > 0)
			{
				this.order = Orders.GetDataById(queryInt);
			}
			else if (!string.IsNullOrEmpty(queryString))
			{
				this.order = Orders.GetOrderBySN(queryString);
			}
			if (this.order == null)
			{
				this.Alert(base.GetCaption("Order_NoFindOrderInfo"), UrlRewrite.Get("myorders_url"));
			}
			else
			{
				base.Put("order", this.order);
				base.Put("kuaidi100url", this.GetKuaidi100Url());
				base.UsingClient("user/订单详情.html");
			}
		}

		public string GetKuaidi100Url()
		{
            SinGooCMS.Plugin.KuaidiComInfo kuaidiComInfo = SinGooCMS.Plugin.KuaidiCom.Get((this.order == null) ? 0 : this.order.ShippingID);
			string result;
			if (kuaidiComInfo != null)
			{
                result = SinGooCMS.Plugin.Kuaidi100.Get(kuaidiComInfo.CompanyCode, this.order.ShippingNo);
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
