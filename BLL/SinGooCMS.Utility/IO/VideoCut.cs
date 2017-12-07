using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// 视频剪切工具.上传视频后可截取视频存储为图片
    /// </summary>
    public static class VideoCut
    {
        #region 公共属性

        /// <summary>
        /// 工具所在路径
        /// </summary>
        public static string ToolsPath { get; set; }
        /// <summary> 
        /// 截图起始时间段 格式 00:00:06
        /// </summary>
        public static string StartPoint { get; set; }
        /// <summary>
        /// 截图长度
        /// </summary>
        public static int CutImgWidth { get; set; }
        /// <summary>
        /// 截图宽度
        /// </summary>
        public static int CutImgHeight { get; set; }
        /// <summary>
        /// 截图文件格式
        /// </summary>
        public static string CutImgExtend { get; set; }

        #endregion

        static VideoCut()
        {
            ToolsPath = System.Web.HttpContext.Current.Server.MapPath("~/Plugin/Tools/ffmpeg.exe");
            StartPoint = "00:00:06";
            CutImgWidth = 500;
            CutImgHeight = 400;
            CutImgExtend = ".jpg";
        }
        public static bool Save(string strFullVideoFile)
        {
            //截图保存在同文件夹下面.名称仅扩展名不同
            string strOutputImg = strFullVideoFile.Substring(0, strFullVideoFile.LastIndexOf(".")) + CutImgExtend;
            return Save(strFullVideoFile, strOutputImg);
        }
        /// <summary>
        /// 保存截图
        /// </summary>
        /// <param name="strFullFileName"></param>
        public static bool Save(string strFullVideoFile, string strFullOutputImg)
        {
            return ExeCommand(ToolsPath + " -i " + strFullVideoFile + " -y -f image2 -ss " + StartPoint + " -t 0.001 -s " + CutImgWidth + "x" + CutImgHeight + " " + strFullOutputImg);
        }
        public static bool ExeCommand(string commandText)
        {
            return ExeCommand(new string[] { commandText });
        }
        public static bool ExeCommand(string[] commandTexts)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            try
            {
                p.Start();
                foreach (string item in commandTexts)
                {
                    p.StandardInput.WriteLine(item);
                }
                p.StandardInput.WriteLine("exit");
                p.WaitForExit();
                p.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
