using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SinGooCMS.Entity
{
    public partial class AttachmentInfo
    {
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExt
        {
            get { return Path.GetExtension(this.FilePath); }
        }
        /// <summary>
        /// 是否图片
        /// </summary>
        public bool IsPicture
        {
            get
            {
                string[] arrImageExt = { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
                return arrImageExt.Contains(FileExt);
            }
        }
    }
}
