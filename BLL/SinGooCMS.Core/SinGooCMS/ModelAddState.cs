using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum ModelAddState
	{
		[Description("发生错误")]
		Error,
		[Description("模型名字已存在")]
		ModelNameExists,
		[Description("自定义表名已使用")]
		TableNameIsUsing,
		[Description("自定义表已存在")]
		TableExists,
		[Description("创建表错误")]
		CreateTableError,
		[Description("成功")]
		Success = 99
	}
}
