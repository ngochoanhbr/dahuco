using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.SessionState;

using SinGooCMS.Utility;

namespace SinGooCMS.Plugin.VerifyCode
{
    public class VerifyCode : IVerifyCode
    {
        #region 私有属性

        private VerifyCodeType _codetype = VerifyCodeType.Web;
        private System.Drawing.Image _codeimg;
        private string _codestring;

        #endregion

        #region 公共属性
        public VerifyCodeType CheckCodeType
        {
            get
            {
                return _codetype;
            }
            set
            {
                _codetype = value;
            }
        }
        public string CheckCodeString
        {
            get
            {
                return _codestring;
            }
        }
        public System.Drawing.Image CheckCodeImg
        {
            get
            {
                return _codeimg;
            }
        }
        #endregion

        public VerifyCode()
        {
            //
        }
        public string GenerateCheckCode()
        {
            string chkCode = string.Empty;
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }

            return chkCode;
        }

        public void CreateCheckCodeImage()
        {
            string checkCode = GenerateCheckCode();
            _codestring = checkCode;
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            int codeW = 80;
            int codeH = 22;
            int fontSize = 16;

            Random rnd = new Random();
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };

            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < checkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(checkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)0);
            }
            //画噪点 
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }            

            try
            {
                //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Png);
                //写入cookie
                if (_codetype == VerifyCodeType.Web)
                {
                    //清除该页输出缓存，设置该页无缓存 
                    System.Web.HttpContext.Current.Response.Buffer = true;
                    System.Web.HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
                    System.Web.HttpContext.Current.Response.Expires = 0;
                    System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                    System.Web.HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
                    System.Web.HttpContext.Current.Response.ClearContent();
                    System.Web.HttpContext.Current.Response.ContentType = "image/Png";
                    System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                    CookieUtils.SetCookie("gif", DEncryptUtils.DESEncode(checkCode), 3600 * 24 * 30);
                }
            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}
