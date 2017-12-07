using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum AdsType
	{
		[Description("文本广告")]
		Text,
		[Description("图片广告")]
		Images,
		[Description("Flash广告")]
		Flash,
		[Description("视频广告")]
		Video
	}
}
