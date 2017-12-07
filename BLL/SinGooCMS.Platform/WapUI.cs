
using System;

public class WapUI : SinGooCMS.BLL.Custom.UIPageBase
{
	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.SetMobileClient();
		base.Using((base.cultureLang.Equals("zh-cn") ? string.Empty : base.cultureLang) + "/mobile/index.html");
	}
}
