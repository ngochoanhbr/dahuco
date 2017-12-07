using System;
using System.ComponentModel;

namespace SinGooCMS
{
	public enum FieldType
	{
		[Description("单文本")]
		SingleTextType,
		[Description("多文本")]
		MultipleTextType,
		[Description("HTML文本编辑器")]
		MultipleHtmlType,
		[Description("列表框")]
		ListBoxType,
		[Description("单选框")]
		RadioButtonType,
		[Description("复选框")]
		CheckBoxType,
		[Description("下拉列表")]
		DropDownListType,
		[Description("日期文本")]
		DateTimeType,
		[Description("单图片域")]
		PictureType,
		[Description("多图片域")]
		MultiPictureType,
		[Description("单文件域")]
		FileType,
		[Description("多文件域")]
		MultiFileType,
		[Description("模板文件域")]
		TemplateFileType,
		[Description("地区域")]
		AreaType
	}
}
