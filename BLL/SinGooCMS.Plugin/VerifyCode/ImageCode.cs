using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using SinGooCMS.Utility;

namespace SinGooCMS.Plugin.VerifyCode
{
    /// <summary>
    /// 检验码
    /// </summary>
    public class ImageCode : IVerifyCode
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageCode()
        {

        }

        #region 生成四个随机数
        /// <summary>
        /// 生成四个随机数
        /// </summary>
        /// <returns></returns>
        public string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                //if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
                //else
                //    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            return checkCode;
        }
        #endregion

        /// <summary>
        /// 创建图片并保存在内存中
        /// </summary>
        /// <param name="checkCode"></param>
        public void CreateCheckCodeImage()
        {
            string checkCode = GenerateCheckCode();
            _codestring = checkCode;
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 10.5)), 18);
            Graphics g = Graphics.FromImage(image);


            //生成随机生成器
            Random random = new Random();

            //清空图片背景色
            g.Clear(Color.White);

            #region
            //画图片的背景噪音线
            for (int i = 0; i < 20; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            #endregion

            Font font = new System.Drawing.Font("Arial", 11, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(checkCode, font, brush, 2, 2);

            #region
            //画图片的前景噪音点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            #endregion
            
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                if (_codetype == VerifyCodeType.Web)
                {
                    //清除该页输出缓存，设置该页无缓存 
                    System.Web.HttpContext.Current.Response.Buffer = true;
                    System.Web.HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
                    System.Web.HttpContext.Current.Response.Expires = 0;
                    System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
                    System.Web.HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
                    System.Web.HttpContext.Current.Response.ClearContent();
                    System.Web.HttpContext.Current.Response.ContentType = "image/Gif";
                    System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());

                    CookieUtils.SetCookie("gif", DEncryptUtils.DESEncode(checkCode), 3600 * 24 * 30);
                }
                else
                {
                    _codeimg = image;
                }
            }
            finally
            {
                //显式释放资源 
                image.Dispose();
                g.Dispose();
            }            
        }
    }
}
