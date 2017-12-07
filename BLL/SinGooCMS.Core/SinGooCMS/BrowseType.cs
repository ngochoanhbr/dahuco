using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum BrowseType
	{
		[Description("动态显示")]
		Aspx,
		[Description("地址重写有后缀")]
		UrlRewriteAndAspx,
		[Description("地址重写无后缀")]
		UrlRewriteNoAspx,
		[Description("伪静态显示")]
		HtmlRewrite,
		[Description("静态显示")]
		Html
	}
}
