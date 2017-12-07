using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// 图片操作类 生成缩略图,生成水印图
    /// </summary>
    public class ImageUtils
    {
        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbPhoto(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 裁切图片
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="showX"></param>
        /// <param name="showY"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        public static void CutPhoto(string originalImagePath, string thumbnailPath, int showX, int showY, int X, int Y, int W, int H)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            if (originalImage != null)
            {
                using (Bitmap bitGet = PercentImage(originalImage, showX, showY))
                {
                    //int ow = originalImage.Width; //原图宽度
                    //int oh = originalImage.Height; //原图高度

                    //新建一个bmp图片
                    System.Drawing.Image bitmap = new System.Drawing.Bitmap(W, H);

                    //新建一个画板
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //清空画布并以透明背景色填充
                    g.Clear(System.Drawing.Color.Transparent);

                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(bitGet, new Rectangle(0, 0, W, H), new Rectangle(X, Y, W, H), GraphicsUnit.Pixel);
                    try
                    {
                        //以jpg格式保存缩略图
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        g.Dispose();
                    }
                }
            }
        }

        /// <summary> 
        /// 按照比例缩小图片 
        /// </summary> 
        /// <param name="srcImage">要缩小的图片</param> 
        /// <param name="LastHeight">要压缩成多大的图片，它的高度</param> 
        /// <param name="LastWidth">要压缩成多大的图片，它的宽度</param> 
        /// <returns>缩小后的结果</returns> 
        public static Bitmap PercentImage(Image srcImage, int LastWidth, int LastHeight)
        {
            double percentHeight = (double)LastHeight / (double)srcImage.Height;
            double percentWidth = (double)LastWidth / (double)srcImage.Width;
            // 缩小后的高度 
            int newH = int.Parse(Math.Round(srcImage.Height * percentHeight).ToString());
            // 缩小后的宽度 
            int newW = int.Parse(Math.Round(srcImage.Width * percentWidth).ToString());

            //int newH = 100;
            //int newW = 100;

            try
            {
                // 要保存到的图片 
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量 
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(srcImage, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 生成水印图
        /// <summary>
        /// 生成图片水印
        /// </summary>
        /// <param name="sourceFilename"></param>
        /// <param name="watermarkPosition"></param>
        /// <param name="watermarkImage"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static string AddImageWatermark(string sourceFilename, int watermarkPosition, string watermarkImage, float alpha)
        {
            return AddWatermark(sourceFilename, "图片水印", watermarkPosition, watermarkImage, string.Empty, 0, string.Empty, string.Empty, alpha);
        }
        /// <summary>
        /// 生成文字水印
        /// </summary>
        /// <param name="sourceFilename"></param>
        /// <param name="watermarkPosition"></param>
        /// <param name="watermarkText"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontFamily"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static string AddTextWatermark(string sourceFilename, int watermarkPosition, string watermarkText, int fontSize, string fontColor, string fontFamily, float alpha)
        {
            return AddWatermark(sourceFilename, "文字水印", watermarkPosition, string.Empty, watermarkText, fontSize, fontColor, fontFamily, alpha);
        }

        /// <summary>
        /// 生成水印图
        /// </summary>
        /// <param name="sourceFilename">源图绝对路径</param>
        /// <param name="watermarkType">水印类型:文字水印,图片水印</param>
        /// <param name="watermarkPosition">水印位置</param>
        /// <param name="watermarkImage">水印图绝对路径</param>
        /// <param name="watermarkText">水印文字</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="alpha">透明度</param>
        /// <returns></returns>
        public static string AddWatermark(string sourceFilename, string watermarkType, int watermarkPosition, string watermarkImage
            , string watermarkText, int fontSize, string fontColor, string fontFamily, float alpha)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(sourceFilename);
            // 封装 GDI+ 位图，此位图由图形图像及其属性的像素数据组成。   
            Bitmap bmPhoto = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppRgb);
            // 设定分辨率
            bmPhoto.SetResolution(72, 72);
            System.Drawing.Graphics g = Graphics.FromImage(bmPhoto);//System.Drawing.Graphics.FromImage(img);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //消除锯齿
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

            //文件扩展名
            string strExt = Path.GetExtension(sourceFilename);
            //生成的水印图文件名
            string strWatermarkFile = sourceFilename.Replace(strExt, "_watermark" + strExt);

            System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();
            System.Drawing.Imaging.ColorMap colorMap = new System.Drawing.Imaging.ColorMap();
            colorMap.OldColor = System.Drawing.Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
            System.Drawing.Imaging.ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, System.Drawing.Imaging.ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
                                                 new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},      
                                                 new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},  //水印透明度    
                                                 new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}      
                                            };
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);

            //水印所在位置
            int xpos = 0;
            int ypos = 0;

            int intWatermarkWidth = 0;
            int intWatermarkHeight = 0;

            System.Drawing.Image watermark = null;
            if (watermarkType.Equals("图片水印") && File.Exists(watermarkImage))
            {
                //加载水印图片
                watermark = new System.Drawing.Bitmap(watermarkImage);
                intWatermarkWidth = watermark.Width;
                intWatermarkHeight = watermark.Height;
            }
            else if (watermarkType.Equals("文字水印") && watermarkText.Trim().Length > 0)
            {
                SizeF size = g.MeasureString(watermarkText, new Font(new FontFamily(fontFamily), fontSize));
                intWatermarkWidth = Convert.ToInt32(size.Width);
                intWatermarkHeight = Convert.ToInt32(size.Height);
            }

            switch (watermarkPosition)
            {
                case 1:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((img.Width * (float).50) - (intWatermarkWidth / 2));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((img.Width * (float).99) - (intWatermarkWidth));
                    ypos = (int)(img.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).50) - (intWatermarkHeight / 2));
                    break;
                case 5:
                    xpos = (int)((img.Width * (float).50) - (intWatermarkWidth / 2));
                    ypos = (int)((img.Height * (float).50) - (intWatermarkHeight / 2));
                    break;
                case 6:
                    xpos = (int)((img.Width * (float).99) - (intWatermarkWidth));
                    ypos = (int)((img.Height * (float).50) - (intWatermarkHeight / 2));
                    break;
                case 7:
                    xpos = (int)(img.Width * (float).01);
                    ypos = (int)((img.Height * (float).99) - intWatermarkHeight);
                    break;
                case 8:
                    xpos = (int)((img.Width * (float).50) - (intWatermarkWidth / 2));
                    ypos = (int)((img.Height * (float).99) - intWatermarkHeight);
                    break;
                case 9:
                    xpos = (int)((img.Width * (float).99) - (intWatermarkWidth));
                    ypos = (int)((img.Height * (float).99) - intWatermarkHeight);
                    break;
            }

            if (watermark != null)
                g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, intWatermarkWidth, intWatermarkHeight), 0, 0, intWatermarkWidth, intWatermarkHeight, System.Drawing.GraphicsUnit.Pixel, imageAttributes);
            else
            {
                System.Drawing.Font font = new System.Drawing.Font(fontFamily, fontSize); //文字字体
                Color fColor = System.Drawing.ColorTranslator.FromHtml(fontColor);
                Color txtColor = System.Drawing.Color.FromArgb(Convert.ToInt32(alpha * 255), fColor);//文字颜色
                System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(txtColor);
                g.DrawString(watermarkText, font, brush, xpos, ypos);
            }

            System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            System.Drawing.Imaging.ImageCodecInfo ici = null;
            foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
            long[] qualityParam = new long[1];
            qualityParam[0] = 80; //图片质量

            System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
            {
                bmPhoto.Save(strWatermarkFile, ici, encoderParams);
            }
            else
            {
                bmPhoto.Save(strWatermarkFile);
            }

            g.Dispose();
            img.Dispose();
            if (watermark != null)
                watermark.Dispose();
            imageAttributes.Dispose();
            return strWatermarkFile;
        }

        #endregion
    }
}
