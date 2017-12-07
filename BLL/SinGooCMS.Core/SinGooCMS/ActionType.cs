using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum ActionType
	{
		[Description("浏览")]
		View,
		[Description("添加")]
		Add,
		[Description("修改")]
		Modify,
		[Description("删除")]
		Delete,
		[Description("发布")]
		Publish,
		[Description("排序")]
		SetSort,
		[Description("审核")]
		Audit,
		[Description("搜索")]
		Search,
		[Description("创建")]
		Create,
		[Description("生成")]
		Generate
	}
}
