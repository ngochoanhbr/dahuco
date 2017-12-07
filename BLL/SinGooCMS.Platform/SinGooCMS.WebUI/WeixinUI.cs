
using System;

namespace SinGooCMS.WebUI
{
    public class WeixinUI : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.SetWeixinClient();
			base.Using((base.cultureLang.Equals("zh-cn") ? string.Empty : base.cultureLang) + "weixin/index.html");
		}
	}
}
