using System;
using System.Text.RegularExpressions;
using System.Web;

namespace SinGooCMS.Utility
{
	public static class IPUtils
	{
		public static string GetIP()
		{
			string text = string.Empty;
			text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (text == null || text == string.Empty)
			{
				text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}
			if (text == null || text == string.Empty)
			{
				text = HttpContext.Current.Request.UserHostAddress;
			}
			string result;
			if (text == null || text == string.Empty)
			{
				result = "0.0.0.0";
			}
			else
			{
				result = text;
			}
			return result;
		}

		public static bool IsIPAddress(string str)
		{
			bool result;
			if (str == null || str == string.Empty || str.Length < 7 || str.Length > 15)
			{
				result = false;
			}
			else
			{
				string pattern = "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$";
				Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
				result = regex.IsMatch(str);
			}
			return result;
		}

		public static bool IsInIPDuan(string strCurrIP, string strBeginIP, string strEndIP)
		{
			int[] array = new int[4];
			int[] array2 = IPUtils.IP2IntArr(strCurrIP);
			int[] array3 = IPUtils.IP2IntArr(strBeginIP);
			array = IPUtils.IP2IntArr(strEndIP);
			int i = 0;
			bool result;
			while (i < 4)
			{
				if (array2[i] < array3[i] || array2[i] > array[i])
				{
					result = false;
				}
				else
				{
					if (array2[i] <= array3[i] && array2[i] >= array[i])
					{
						i++;
						continue;
					}
					result = true;
				}
				return result;
			}
			result = true;
			return result;
		}

		public static int[] IP2IntArr(string strIP)
		{
			int[] array = new int[4];
			int i = 0;
			int num = 0;
			while (i < strIP.Length)
			{
				char c = strIP[i];
				if (c != '.')
				{
					array[num] = array[num] * 10 + int.Parse(c.ToString());
				}
				else
				{
					num++;
				}
				i++;
			}
			return array;
		}

		public static SinaIPAreaInfo GetIPAreaFromSina()
		{
			return IPUtils.GetIPAreaFromSina(string.Empty);
		}

		public static SinaIPAreaInfo GetIPAreaFromSina(string strIPAddress)
		{
			SinaIPAreaInfo result;
			if (ConfigUtils.GetAppSetting<bool>("IsReadIPArea"))
			{
				string strUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + strIPAddress;
				result = JsonUtils.JsonToObject<SinaIPAreaInfo>(NetWorkUtils.HttpGet(strUrl));
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static string GetIPAreaFromPcOnline(string strIPAddress)
		{
			string result;
			if (ConfigUtils.GetAppSetting<bool>("IsReadIPArea"))
			{
				try
				{
					result = NetWorkUtils.HttpGet("http://whois.pconline.com.cn/ip.jsp?ip=" + strIPAddress, "gb2312");
					return result;
				}
				catch
				{
					result = "未知地址或者获取地址失败";
					return result;
				}
			}
			result = string.Empty;
			return result;
		}

		public static TaoBaoAreaInfo GetIPAreaFromTaoBao()
		{
			return IPUtils.GetIPAreaFromTaoBao(IPUtils.GetIP());
		}

		public static TaoBaoAreaInfo GetIPAreaFromTaoBao(string strIPAddress)
		{
			TaoBaoAreaInfo result;
			if (ConfigUtils.GetAppSetting<bool>("IsReadIPArea"))
			{
				string strUrl = "http://ip.taobao.com/service/getIpInfo.php?ip=" + strIPAddress;
				result = JsonUtils.JsonToObject<TaoBaoAreaInfo>(NetWorkUtils.HttpGet(strUrl));
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
