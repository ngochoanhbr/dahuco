using SinGooCMS.Config;
using System;

namespace SinGooCMS.Plugin
{
	public class SMSProvider
	{
		public static ISMS Create()
		{
			return SMSProvider.Create(ConfigProvider.Configs.SMSClass);
		}

		public static ISMS Create(string smsClassName)
		{
			return (ISMS)Activator.CreateInstance("SinGooCMS.Plugin", "SinGooCMS.Plugin." + smsClassName).Unwrap();
		}

		public static ISMS Create(string strAssemblyName, string smsClassName)
		{
			return (ISMS)Activator.CreateInstance(strAssemblyName, "SinGooCMS.Plugin." + smsClassName).Unwrap();
		}
	}
}
