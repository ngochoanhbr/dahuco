using LitJson;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web;

namespace SinGooCMS.Plugin.ThirdLogin
{
	public class SinaAuth
	{
		public static Dictionary<string, object> get_access_token(string code)
		{
			OAuthConfig oAuthConfig = OAuthConfig.GetOAuthConfig("sina");
			string strUrl = "https://api.weibo.com/oauth2/access_token";
			string postData = string.Concat(new string[]
			{
				"grant_type=authorization_code&code=",
				code,
				"&client_id=",
				oAuthConfig.OAuthAppId,
				"&client_secret=",
				oAuthConfig.OAuthAppKey,
				"&redirect_uri=",
				HttpContext.Current.Server.UrlEncode(oAuthConfig.ReturnUri)
			});
			string text = NetWorkUtils.HttpPost(strUrl, postData);
			Dictionary<string, object> result;
			if (text.Contains("error"))
			{
				result = null;
			}
			else
			{
				try
				{
					Dictionary<string, object> dictionary = JsonMapper.ToObject<Dictionary<string, object>>(text);
					result = dictionary;
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		public static Dictionary<string, object> get_token_info(string access_token)
		{
			string strUrl = "https://api.weibo.com/oauth2/get_token_info";
			string postData = "access_token=" + access_token;
			string text = NetWorkUtils.HttpPost(strUrl, postData);
			Dictionary<string, object> result;
			if (text.Contains("error"))
			{
				result = null;
			}
			else
			{
				try
				{
					Dictionary<string, object> dictionary = JsonMapper.ToObject<Dictionary<string, object>>(text);
					result = dictionary;
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		public static JsonData get_info(string access_token, string open_id)
		{
			string strUrl = "https://api.weibo.com/2/users/show.json?access_token=" + access_token + "&uid=" + open_id;
			string text = NetWorkUtils.HttpGet(strUrl);
			JsonData result;
			if (text.Contains("error"))
			{
				result = null;
			}
			else
			{
				try
				{
					JsonData jsonData = JsonMapper.ToObject(text);
					if (jsonData.Count > 0)
					{
						result = jsonData;
						return result;
					}
				}
				catch
				{
					result = null;
					return result;
				}
				result = null;
			}
			return result;
		}
	}
}
