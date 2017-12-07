/*
 * 1)抓取网页内容
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;

namespace SinGooCMS.Utility
{
    public static class NetWorkUtils
    {
        /// <summary>
        /// 读取页面内容
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        public static string HttpGet(string strUrl)
        {
            return HttpGet(strUrl, "utf-8");
        }
        /// <summary>
        /// 读取页面内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encodingType"></param>
        /// <returns></returns>
        public static string HttpGet(string strUrl, string encodingType)
        {
            try
            {
                StringBuilder dataReturnString = new StringBuilder();
                Stream dataStream;
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(strUrl);
                req.Method = "GET";
                req.Timeout = 6000;
                req.AllowWriteStreamBuffering = true;
                req.AllowAutoRedirect = true;
                System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse();

                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    dataStream = resp.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.GetEncoding(encodingType);
                    StreamReader readStream = new StreamReader(dataStream, encode);
                    char[] cCount = new char[500];
                    int count = readStream.Read(cCount, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(cCount, 0, count);
                        dataReturnString.Append(str);
                        count = readStream.Read(cCount, 0, 256);
                    }
                    resp.Close();
                    return dataReturnString.ToString();
                }
                resp.Close();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// http POST请求url,并返回结果
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="method_name"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Timeout = 20000;

            HttpWebResponse response = null;

            try
            {
                StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postData);

                if (swRequestWriter != null)
                    swRequestWriter.Close();

                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }
}
