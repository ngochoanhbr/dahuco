using SinGooCMS.Config;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.Plugin
{
	public class Kuaidi100
	{
		public static string Get(string strExpressCompanyCode, string strExpressNo)
		{
			return Kuaidi100.Get(ConfigProvider.Configs.Kuaidi100Key, strExpressCompanyCode, strExpressNo);
		}

		public static string Get(string strKey, string strExpressCompanyCode, string strExpressNo)
		{
			return NetWorkUtils.HttpGet(string.Concat(new string[]
			{
				"http://www.kuaidi100.com/applyurl?key=",
				strKey,
				"&com=",
				strExpressCompanyCode,
				"&nu=",
				strExpressNo
			}));
		}
	}
}
