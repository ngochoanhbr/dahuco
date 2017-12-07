using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Threading;

namespace SinGooCMS.Utility
{
    public static class ResponseUtils
    {
        /// <summary>
        /// 输出二进制流文件
        /// </summary>
        /// <param name="strFileName">文件所在路径</param>
        public static void ResponseFile(string strFileName)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            if (File.Exists(strFileName))
            {
                context.Response.AppendHeader("Content-Type", "application/octet-stream");
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + context.Server.UrlEncode(Path.GetFileName(strFileName)));
                context.Response.WriteFile(strFileName, false);
            }
            else
            {
                context.Response.Status = "404 File Not Found";
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "File Not Found";
                context.Response.Write("File Not Found");
            }
        }

        #region 下载文件

        /// <summary>
        /// 输入文件,下载文件
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <param name="strFilePath">文件相对路径</param>
        /// <param name="lngSpeed">停顿时间毫秒计算</param>
        /// <returns></returns>
        public static bool ResponseFile(string strFileName, string strFilePath, long lngSpeed)
        {
            try
            {
                FileStream myFile = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    System.Web.HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");
                    System.Web.HttpContext.Current.Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    double pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor(1000 * pack / lngSpeed) + 1;
                    if (System.Web.HttpContext.Current.Request.Headers["Range"] != null)
                    {
                        System.Web.HttpContext.Current.Response.StatusCode = 206;
                        string[] range = System.Web.HttpContext.Current.Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        //Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength-1, fileLength));
                    }
                    System.Web.HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (System.Web.HttpContext.Current.Response.IsClientConnected)
                        {
                            System.Web.HttpContext.Current.Response.BinaryWrite(br.ReadBytes(int.Parse(pack.ToString())));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();

                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool ResponseFile(string strFileName, string strFilePath)
        {
            return ResponseFile(strFileName, strFilePath, 1024000);
        }
        #endregion

    }
}
