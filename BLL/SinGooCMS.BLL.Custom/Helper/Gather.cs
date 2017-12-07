using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace SinGooCMS.BLL.Helper
{
	public class Gather
	{
		private const string SOURCEURL = "http://service.24k99.com/Quote/handler/Datas.ashx";

		private const string POSTDATA = "page=zgjbw-sy&vtype=XHWH";

		private const string REFERERPAGE = "http://service.24k99.com/Quote/Quote.aspx?Page=zgjbw-sy";

		public static string GetGoldInfo()
		{
			HttpWebRequest httpReq = WebRequest.Create("http://service.24k99.com/Quote/handler/Datas.ashx") as HttpWebRequest;
			httpReq.Accept = "text/plain,*/*";
			httpReq.Method = "POST";
			httpReq.Referer = "http://service.24k99.com/Quote/Quote.aspx?Page=zgjbw-sy";
			httpReq.KeepAlive = true;
			httpReq.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			byte[] bufPost = Encoding.UTF8.GetBytes("page=zgjbw-sy&vtype=XHWH");
			httpReq.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
			httpReq.ContentLength = (long)bufPost.Length;
			Stream newStream = httpReq.GetRequestStream();
			newStream.Write(bufPost, 0, bufPost.Length);
			newStream.Close();
			HttpWebResponse httpRes = httpReq.GetResponse() as HttpWebResponse;
			string result;
			if (httpRes.StatusCode.Equals(HttpStatusCode.OK))
			{
				using (Stream resStream = httpRes.GetResponseStream())
				{
					using (StreamReader resStreamReader = new StreamReader(resStream, Encoding.UTF8))
					{
						result = resStreamReader.ReadToEnd();
						return result;
					}
				}
			}
			result = "fail";
			return result;
		}
	}
}
