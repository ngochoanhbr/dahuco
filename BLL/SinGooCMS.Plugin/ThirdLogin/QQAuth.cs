using LitJson;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web;

namespace SinGooCMS.Plugin.ThirdLogin
{
	public class QQAuth
	{
		public static Dictionary<string, object> get_access_token(string code, string state)
		{
			OAuthConfig oAuthConfig = OAuthConfig.GetOAuthConfig("qq");
			string strUrl = string.Concat(new string[]
			{
				"https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&client_id=",
				oAuthConfig.OAuthAppId,
				"&client_secret=",
				oAuthConfig.OAuthAppKey,
				"&code=",
				code,
				"&state=",
				state,
				"&redirect_uri=",
				HttpContext.Current.Server.UrlEncode(oAuthConfig.ReturnUri)
			});
			string text = NetWorkUtils.HttpGet(strUrl);
			Dictionary<string, object> result;
			if (text.Contains("error"))
			{
				result = null;
			}
			else
			{
				try
				{
					string[] array = text.Split(new char[]
					{
						'&'
					});
					string value = array[0].Split(new char[]
					{
						'='
					})[1];
					string value2 = array[1].Split(new char[]
					{
						'='
					})[1];
					result = new Dictionary<string, object>
					{
						{
							"access_token",
							value
						},
						{
							"expires_in",
							value2
						}
					};
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		public static Dictionary<string, object> get_open_id(string access_token)
		{
			string strUrl = "https://graph.qq.com/oauth2.0/me?access_token=" + access_token;
			string text = NetWorkUtils.HttpGet(strUrl);
			Dictionary<string, object> result;
			if (text.Contains("error"))
			{
				result = null;
			}
			else
			{
				int num = text.IndexOf('(') + 1;
				int num2 = text.LastIndexOf(')') - 1;
				text = text.Substring(num, num2 - num);
				Dictionary<string, object> dictionary = JsonMapper.ToObject<Dictionary<string, object>>(text);
				result = dictionary;
			}
			return result;
		}

		public static Dictionary<string, object> get_user_info(string access_token, string open_id)
		{
			OAuthConfig oAuthConfig = OAuthConfig.GetOAuthConfig("qq");
			string strUrl = string.Concat(new string[]
			{
				"https://graph.qq.com/user/get_user_info?access_token=",
				access_token,
				"&oauth_consumer_key=",
				oAuthConfig.OAuthAppId,
				"&openid=",
				open_id
			});
			string text = NetWorkUtils.HttpGet(strUrl);
			Dictionary<string, object> result;
			if (text.Contains("error"))
			{
				result = null;
			}
			else
			{
				Dictionary<string, object> dictionary = JsonMapper.ToObject<Dictionary<string, object>>(text);
				result = dictionary;
			}
			return result;
		}

		public static JsonData get_info(string access_token, string open_id)
		{
			OAuthConfig oAuthConfig = OAuthConfig.GetOAuthConfig("qq");
			string strUrl = string.Concat(new string[]
			{
				"https://graph.qq.com/user/get_info?access_token=",
				access_token,
				"&oauth_consumer_key=",
				oAuthConfig.OAuthAppId,
				"&openid=",
				open_id
			});
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
