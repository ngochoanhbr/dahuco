using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum FieldAddState
	{
		[Description("发生错误")]
		Error,
		[Description("字段名称已使用")]
		FieldNameIsUsing,
		[Description("字段名称已存在")]
		FieldNameExists,
		[Description("模板不存在")]
		ModelNotExists,
		[Description("创建字段错误")]
		CreateColumnError,
		[Description("完成")]
		Success = 99
	}
}
