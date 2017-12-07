using SinGooCMS.Utility;
using System;
using System.Collections.Generic;

namespace SinGooCMS.Plugin
{
	public class UENews
	{
		public static List<UENewsInfo> Get()
		{
			string jSON = UENews.GetJSON();
			List<UENewsInfo> result;
			if (!string.IsNullOrEmpty(jSON))
			{
				result = JsonUtils.JsonToObject<List<UENewsInfo>>(jSON);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static string GetJSON()
		{
			string result;
			try
			{
				result = NetWorkUtils.HttpGet("http://www.ue.net.cn/api/uenews.json");
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
