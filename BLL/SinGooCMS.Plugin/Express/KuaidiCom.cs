using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SinGooCMS.Plugin
{
	public class KuaidiCom
	{
		public static KuaidiComInfo Get(string strCompanyCode)
		{
			return (from p in KuaidiCom.Load()
			where p.CompanyCode.Equals(strCompanyCode)
			select p).FirstOrDefault<KuaidiComInfo>();
		}

		public static KuaidiComInfo Get(int intCompanyID)
		{
			return (from p in KuaidiCom.Load()
			where p.AutoID.Equals(intCompanyID)
			select p).FirstOrDefault<KuaidiComInfo>();
		}

		public static List<KuaidiComInfo> Load()
		{
			List<KuaidiComInfo> list = new List<KuaidiComInfo>();
			return JsonUtils.JsonToObject<List<KuaidiComInfo>>(File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/kuaidicom.json")));
		}
	}
}
