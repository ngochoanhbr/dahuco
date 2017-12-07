using jZnx9Ho9F0Rg8YaAGk;
using M4SskKXolf7S9GtiQR;
using System;
using System.IO;
using System.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace SinGooCMS
{
	[Serializable]
	public class JObject
	{
		public static string cultureLang
		{
			get
			{
				string result;
				string text;
				if (!8efWEXulHOsQ9D5YBY.AFlj2cBCc(0))
				{
					if (HttpContext.Current == null)
					{
						result = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(0);
						return result;
					}
					if (HttpContext.Current.Request.Cookies[8efWEXulHOsQ9D5YBY.kAdu7xqFh(14)] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[8efWEXulHOsQ9D5YBY.kAdu7xqFh(38)].Value))
					{
						result = HttpContext.Current.Request.Cookies[8efWEXulHOsQ9D5YBY.kAdu7xqFh(62)].Value;
						return result;
					}
					text = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(86);
				}
				HttpCookie httpCookie = new HttpCookie(8efWEXulHOsQ9D5YBY.kAdu7xqFh(100));
				httpCookie.Value = text;
				httpCookie.Expires = DateTime.Now.AddYears(1);
				HttpContext.Current.Response.Cookies.Add(httpCookie);
				result = text;
				return result;
			}
		}

		public static string SystemDrive
		{
			get
			{
				return Environment.GetEnvironmentVariable(8efWEXulHOsQ9D5YBY.kAdu7xqFh(140));
			}
		}

		public static T DeepClone<T>(T obj)
		{
			T result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, obj);
				memoryStream.Position = 0L;
				result = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			return result;
		}

		public static bool IsLicenceOk(string strUnCodeKey)
		{
			return strUnCodeKey.Equals(JObject.AFlj2cBCc());
		}

		public static string ReturnXorValue(string str1, string str2)
		{
			string text;
			string str3;
			byte[] bytes;
			byte[] bytes2;
			int num;
			if (!8efWEXulHOsQ9D5YBY.AFlj2cBCc(4))
			{
				text = string.Empty;
				str3 = string.Empty;
				bytes = Encoding.Default.GetBytes(str1);
				bytes2 = Encoding.Default.GetBytes(str2);
				if (bytes.Length != bytes2.Length)
				{
					throw new ArgumentException(8efWEXulHOsQ9D5YBY.kAdu7xqFh(124));
				}
				num = 0;
				goto IL_8D;
			}
			IL_76:
			int num2;
			str3 = num2.ToString();
			text += str3;
			num++;
			IL_8D:
			if (num >= bytes.Length)
			{
				return text;
			}
			num2 = (int)(bytes[num] ^ bytes2[num]);
			goto IL_76;
		}

		private static string AFlj2cBCc()
		{
			string result;
			try
			{
				ManagementObject managementObject = new ManagementObject(8efWEXulHOsQ9D5YBY.kAdu7xqFh(166) + JObject.SystemDrive + 8efWEXulHOsQ9D5YBY.kAdu7xqFh(226));
				string text = (string)managementObject.GetPropertyValue(8efWEXulHOsQ9D5YBY.kAdu7xqFh(232));
				char[] array = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(272).ToCharArray();
				string text2 = string.Empty;
				char[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					char c = array2[i];
					string text3 = Convert.ToString(Convert.ToInt32(c.ToString()), 2);
					switch (text3.Trim().Length)
					{
					case 1:
						text3 = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(292) + text3;
						break;
					case 2:
						text3 = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(302) + text3;
						break;
					case 3:
						text3 = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(310) + text3;
						break;
					}
					text2 += text3;
				}
				string str = text2;
				string text4 = string.Empty;
				char[] array3 = text.ToCharArray();
				array2 = array3;
				for (int i = 0; i < array2.Length; i++)
				{
					char c = array2[i];
					string text3 = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);
					switch (text3.Trim().Length)
					{
					case 1:
						text3 = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(316) + text3;
						break;
					case 2:
						text3 = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(326) + text3;
						break;
					case 3:
						text3 = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(334) + text3;
						break;
					}
					text4 += text3;
				}
				result = Convert.ToUInt32(JObject.ReturnXorValue(str, text4), 2).ToString();
			}
			catch
			{
				result = 8efWEXulHOsQ9D5YBY.kAdu7xqFh(340);
			}
			return result;
		}

		public JObject()
		{
			8kPNLFBksXGJTEbL2r.SLV0fFIsptsZtjvFft17();
			base..ctor();
		}
	}
}
