using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace SinGooCMS.BLL
{
	public static class STATClient
	{
		public static VisitorInfo GetVisitorInfo()
		{
			VisitorInfo result;
			if (HttpContext.Current != null)
			{
				VisitorInfo visitorInfo = new VisitorInfo();
				HttpContext.Current.Response.Expires = 0;
				NameValueCollection serverVariables = HttpContext.Current.Request.ServerVariables;
				visitorInfo.IPAddress = IPUtils.GetIP();
				string text = serverVariables["HTTP_USER_AGENT"] ?? string.Empty;
				visitorInfo.UserAgent = text;
				visitorInfo.OPSystem = STATClient.GetOPSystem(text);
				visitorInfo.CustomerLang = ((HttpContext.Current.Request.UserLanguages != null) ? HttpContext.Current.Request.UserLanguages[0].ToLower() : string.Empty);
				visitorInfo.Navigator = STATClient.GetBrowser(text);
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				visitorInfo.IsMobileDevice = browser.IsMobileDevice;
				visitorInfo.IsSupportActiveX = browser.ActiveXControls;
				visitorInfo.IsSupportCookie = browser.Cookies;
				visitorInfo.IsSupportJavascript = browser.JavaScript;
				if (browser.ClrVersion == null)
				{
					visitorInfo.NETVer = "不支持";
				}
				else
				{
					visitorInfo.NETVer = browser.ClrVersion.ToString();
				}
				visitorInfo.IsCrawler = browser.Crawler;
				visitorInfo.VPage = HttpContext.Current.Request.Url.ToString();
				if (HttpContext.Current.Request.UrlReferrer != null)
				{
					string text2 = HttpContext.Current.Request.UrlReferrer.ToString();
					if (text2 != "" && text2.Substring(text2.Length - 1, 1) == "/")
					{
						text2 = text2.Substring(0, text2.Length - 1);
					}
					visitorInfo.ApproachUrl = text2;
					string[] array = STATClient.GetSearchEngine(text2).Split(new char[]
					{
						'|'
					});
					if (array.Length == 3)
					{
						visitorInfo.Engine = array[0] + "[" + array[1] + "]";
						visitorInfo.KeyWord = array[2];
					}
				}
				else
				{
					visitorInfo.ApproachUrl = "手动输入地址";
				}
				if (HttpContext.Current.Request.Cookies.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
					{
						if (i == HttpContext.Current.Request.Cookies.Count - 1)
						{
							stringBuilder.Append(HttpContext.Current.Request.Cookies[i].Name + ":" + HttpContext.Current.Request.Cookies[i].Values);
						}
						else
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								HttpContext.Current.Request.Cookies[i].Name,
								":",
								HttpContext.Current.Request.Cookies[i].Values,
								"|"
							}));
						}
					}
					visitorInfo.CookieParameter = stringBuilder.ToString();
				}
				if (HttpContext.Current.Request.QueryString.Count > 0)
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
					{
						if (i == HttpContext.Current.Request.QueryString.Count - 1)
						{
							stringBuilder2.Append(HttpContext.Current.Request.QueryString.AllKeys[i] + ":" + HttpContext.Current.Request.QueryString[i]);
						}
						else
						{
							stringBuilder2.Append(HttpContext.Current.Request.QueryString.AllKeys[i] + ":" + HttpContext.Current.Request.QueryString[i] + "|");
						}
					}
					visitorInfo.GETParameter = stringBuilder2.ToString();
				}
				if (HttpContext.Current.Request.Form.Count > 0)
				{
					StringBuilder stringBuilder3 = new StringBuilder();
					for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
					{
						string a = HttpContext.Current.Request.Form.AllKeys[i];
						if (a != "__VIEWSTATE")
						{
							if (i == HttpContext.Current.Request.Form.Count - 1)
							{
								stringBuilder3.Append(HttpContext.Current.Request.Form.AllKeys[i] + ":" + HttpContext.Current.Request.Form[i]);
							}
							else
							{
								stringBuilder3.Append(HttpContext.Current.Request.Form.AllKeys[i] + ":" + HttpContext.Current.Request.Form[i] + "|");
							}
						}
					}
					visitorInfo.POSTParameter = stringBuilder3.ToString();
				}
				visitorInfo.ErrMessage = string.Empty;
				visitorInfo.StackTrace = string.Empty;
				visitorInfo.Lang = JObject.cultureLang;
				visitorInfo.AutoTimeStamp = DateTime.Now;
				result = visitorInfo;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static string GetOPSystem(string strSource)
		{
			string result = "Other";
			string[,] array = new string[22, 2];
			array[0, 0] = "Windows NT 6.1";
			array[0, 1] = "Windows 7";
			array[1, 0] = "Windows NT 6.0";
			array[1, 1] = "Windows Vista";
			array[2, 0] = "Windows NT 5.2";
			array[2, 1] = "Win2003";
			array[3, 0] = "Windows NT 5.0";
			array[3, 1] = "Win2000";
			array[4, 0] = "Windows NT 5.1";
			array[4, 1] = "WinXP";
			array[5, 0] = "Windows NT";
			array[5, 1] = "WinNT";
			array[6, 0] = "Windows 9";
			array[6, 1] = "Win9x";
			array[7, 0] = "98";
			array[7, 1] = "Win98";
			array[8, 0] = "iPad";
			array[8, 1] = "iPad";
			array[9, 0] = "unix";
			array[9, 1] = "类Unix";
			array[10, 0] = "Linux";
			array[10, 1] = "Linux";
			array[11, 0] = "SunOS";
			array[11, 1] = "类Unix";
			array[12, 0] = "BSD";
			array[12, 1] = "类Unix";
			array[13, 0] = "Mac";
			array[13, 1] = "Mac OS";
			array[14, 0] = "AIX";
			array[14, 1] = "AIX";
			array[15, 0] = "AmigaOS";
			array[15, 1] = "Amiga";
			array[16, 0] = "BEOS";
			array[16, 1] = "BeOS";
			array[17, 0] = "FreeBSD";
			array[17, 1] = "FreeBSD";
			array[18, 0] = "Windows CE";
			array[18, 1] = "Windows CE";
			array[19, 0] = "ME";
			array[19, 1] = "Windows ME";
			array[20, 0] = "HP-UX";
			array[20, 1] = "HP Unix";
			array[21, 0] = "SUN";
			array[21, 1] = "Sun OS";
			string[,] array2 = array;
			for (int i = 0; i < array2.GetLength(0); i++)
			{
				if (strSource.ToUpper().IndexOf(array2[i, 0].ToUpper()) > 0)
				{
					result = array2[i, 1];
					break;
				}
			}
			return result;
		}

		public static string GetBrowser(string strSource)
		{
			string result = "Other";
			string[,] array = new string[12, 2];
			array[0, 0] = "MSIE 10.0";
			array[0, 1] = "IE 10";
			array[1, 0] = "MSIE 9.0";
			array[1, 1] = "IE 9";
			array[2, 0] = "MSIE 8.0";
			array[2, 1] = "IE 8";
			array[3, 0] = "MSIE 7.0";
			array[3, 1] = "IE 7";
			array[4, 0] = "MSIE 6.0";
			array[4, 1] = "IE 6";
			array[5, 0] = "MSIE 5.0";
			array[5, 1] = "IE 5";
			array[6, 0] = "Firefox";
			array[6, 1] = "Firefox";
			array[7, 0] = "Chrome";
			array[7, 1] = "Chrome";
			array[8, 0] = "Safari";
			array[8, 1] = "Safari";
			array[9, 0] = "Netscape";
			array[9, 1] = "Netscape";
			array[10, 0] = "Opera";
			array[10, 1] = "Opera";
			array[11, 0] = "Navigator";
			array[11, 1] = "Navigator";
			string[,] array2 = array;
			for (int i = 0; i < array2.GetLength(0); i++)
			{
				if (strSource.ToUpper().IndexOf(array2[i, 0].ToUpper()) > 0)
				{
					result = array2[i, 1];
					break;
				}
			}
			return result;
		}

		public static string GetSearchEngine(string strSource)
		{
			List<SeoSource> list = new List<SeoSource>
			{
				new SeoSource
				{
					Name = "Google",
					Url = "http://www.google.com",
					Query = "q"
				},
				new SeoSource
				{
					Name = "谷歌中国",
					Url = "http://www.google.com.hk",
					Query = "q"
				},
				new SeoSource
				{
					Name = "百度",
					Url = "http://www.baidu.com",
					Query = "wd"
				},
				new SeoSource
				{
					Name = "雅虎中国",
					Url = "http://www.yahoo.cn",
					Query = "q"
				},
				new SeoSource
				{
					Name = "Yahoo!",
					Url = "http://search.yahoo.com",
					Query = "q"
				},
				new SeoSource
				{
					Name = "Soso",
					Url = "http://www.soso.com",
					Query = "w"
				},
				new SeoSource
				{
					Name = "必应中国",
					Url = "http://cn.bing.com",
					Query = "q"
				},
				new SeoSource
				{
					Name = "Bing",
					Url = "http://www.bing.com",
					Query = "q"
				},
				new SeoSource
				{
					Name = "有道",
					Url = "http://www.youdao.com",
					Query = "q"
				},
				new SeoSource
				{
					Name = "Sogou",
					Url = "http://www.sogou.com",
					Query = "query"
				}
			};
			string result;
			foreach (SeoSource current in list)
			{
				if (strSource.ToUpper().IndexOf(current.Url.ToUpper()) > 0 && strSource.IndexOf("?") > 0)
				{
					string[] array = strSource.Split(new char[]
					{
						'?'
					})[1].Split(new char[]
					{
						'&'
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text = array[i];
						if (text.IndexOf("=") > 0 && text.Split(new char[]
						{
							'='
						})[0] == current.Query)
						{
							result = string.Concat(new string[]
							{
								current.Name,
								"|",
								current.Url,
								"|",
								HttpContext.Current.Server.UrlDecode(text.Split(new char[]
								{
									'='
								})[1])
							});
							return result;
						}
					}
				}
			}
			result = "||";
			return result;
		}
	}
}
