using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;

namespace SinGooCMS.Utility
{
    public class QRCode
    {
        public string CodeText { get; set; }
        public int Size { get; set; }

        public QRCode()
        {
            // 
        }

        public QRCode(string code, int size)
        {
            CodeText = code;
            Size = size;
        }

        public System.Drawing.Bitmap GenerateQrCode()
        {
            QRCodeEncoder qrEntity = new QRCodeEncoder();
            qrEntity.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//二维码编码方式
            qrEntity.QRCodeScale = Size;//每个小方格的宽度
            qrEntity.QRCodeVersion = 5;//二维码版本号
            qrEntity.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//纠错码等级

            System.Drawing.Bitmap qrImage;
            //动态调整二维码版本号,上限40，过长返回空白图片，编码后字符最大字节长度2953
            while (true)
            {
                try
                {
                    qrImage = qrEntity.Encode(CodeText, System.Text.Encoding.UTF8);
                    break;
                }
                catch (IndexOutOfRangeException e)
                {
                    if (qrEntity.QRCodeVersion < 40)
                    {
                        qrEntity.QRCodeVersion = 40;
                    }
                    else
                    {
                        qrImage = new Bitmap(100, 100);
                        break;
                    }
                }
            }

            return qrImage;
        }
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="code">需要生成二维码的文本</param>
        /// <param name="size">图片的大小 100</param>
        public void GreateQrCode()
        {
            System.Drawing.Bitmap qrImage = GenerateQrCode();
            //为生成的二维码图像裁剪白边并调整为请求的高度            
            //清除该页输出缓存，设置该页无缓存
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
            System.Web.HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                qrImage.Save(ms, ImageFormat.Png);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ContentType = "image/Png";
                System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //显式释放资源 
                qrImage.Dispose();
            }

        }
    }
}
