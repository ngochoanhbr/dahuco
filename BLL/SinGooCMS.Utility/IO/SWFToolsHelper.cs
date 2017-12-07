using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// pdf2swf-0.9.1.exe调用 在线文档浏览
    /// </summary>
    public static class SWFToolsHelper
    {
        /// <summary>
        /// 转换所有的页，图片质量80%
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <param name="swfPath"></param>
        /// <returns></returns>
        public static bool PDF2SWF(string pdfPath, string swfPath)
        {
            return PDF2SWF(pdfPath, swfPath, 1, GetPageCount(pdfPath), 80);
        }
        /// <summary>
        /// 转换前N页，图片质量80%
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <param name="swfPath"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static bool PDF2SWF(string pdfPath, string swfPath, int page)
        {
            return PDF2SWF(pdfPath, swfPath, 1, page, 80);
        }
        /// <summary>
        /// PDF格式转为SWF
        /// </summary>
        /// <param name="pdfPath">原pdf文件绝对地址，如/a/b/c.pdf</param>
        /// <param name="swfPath">生成后的swf绝对文件地址，如/a/b/c.swf</param>
        /// <param name="beginpage">转换开始页</param>
        /// <param name="endpage">转换结束页</param>
        /// <param name="photoQuality"></param>
        /// <returns></returns>
        public static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality)
        {
            string exe = MapPath("~/Plugin/Tools/pdf2swf-0.9.1.exe");
            if (!System.IO.File.Exists(exe) || !System.IO.File.Exists(pdfPath) || System.IO.File.Exists(swfPath))
            {
                return false;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(" \"" + pdfPath + "\"");//input
            sb.Append(" -o \"" + swfPath + "\"");//output
            //sb.Append(" -z");
            sb.Append(" -s flashversion=9");//flash version
            sb.Append(" -s disablelinks");//禁止PDF里面的链接
            if (endpage > GetPageCount(pdfPath)) endpage = GetPageCount(pdfPath);
            sb.Append(" -p " + "\"" + beginpage + "" + "-" + endpage + "\"");//page range
            sb.Append(" -j " + photoQuality);//SWF中的图片质量
            string Command = sb.ToString();

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = Command;

            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序 启动 线程
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,pdf2swf-0.9.1.exe的所有输出信息,都为错误输出流,用 StandardOutput是捕获不到任何消息的...
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//等待完成
            //p.StandardError.ReadToEnd();//开始同步读取
            p.Close();//关闭进程
            p.Dispose();//释放资源
            return true;
        }


        public static int GetPageCount(string pdfPath)
        {
            byte[] buffer = System.IO.File.ReadAllBytes(pdfPath);
            int length = buffer.Length;
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            string pdfText = Encoding.Default.GetString(buffer);
            System.Text.RegularExpressions.Regex rx1 = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
            System.Text.RegularExpressions.MatchCollection matches = rx1.Matches(pdfText);
            return matches.Count;
        }

        public static string MapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用 
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
    }
}
