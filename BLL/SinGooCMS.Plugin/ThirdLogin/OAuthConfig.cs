using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SinGooCMS.Plugin.ThirdLogin
{
	[Serializable]
	public class OAuthConfig
	{
		public string OAuthKey
		{
			get;
			set;
		}

		public string OAuthName
		{
			get;
			set;
		}

		public string ShowImg
		{
			get;
			set;
		}

		public string OAuthAppId
		{
			get;
			set;
		}

		public string OAuthAppKey
		{
			get;
			set;
		}

		public string ReturnUri
		{
			get;
			set;
		}

		public string ApplyUrl
		{
			get;
			set;
		}

		public string APIUrl
		{
			get;
			set;
		}

		public string Remark
		{
			get;
			set;
		}

		public bool IsEnabled
		{
			get;
			set;
		}

		public static List<OAuthConfig> Load()
		{
			List<OAuthConfig> list = OAuthConfig.LoadAll();
			List<OAuthConfig> result;
			if (list == null)
			{
				result = null;
			}
			else
			{
				result = (from p in list
				where p.IsEnabled
				select p).ToList<OAuthConfig>();
			}
			return result;
		}

		public static List<OAuthConfig> LoadAll()
		{
			List<OAuthConfig> result;
			try
			{
				string xmlString = File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/thirdlogin.config"));
				result = XmlSerializerUtils.Deserialize<List<OAuthConfig>>(xmlString);
			}
			catch
			{
				result = new List<OAuthConfig>();
			}
			return result;
		}

		public static OAuthConfig GetOAuthConfig(string key)
		{
			return (from p in OAuthConfig.LoadAll()
			where string.Compare(p.OAuthKey, key, true) == 0
			select p).FirstOrDefault<OAuthConfig>();
		}

		public static void Save(List<OAuthConfig> lstConfig)
		{
			File.WriteAllText(HttpContext.Current.Server.MapPath("/Config/thirdlogin.config"), XmlSerializerUtils.SerializeList<OAuthConfig>(lstConfig));
		}
	}
}
