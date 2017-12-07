using System;

namespace SinGooCMS.Upgrade
{
    /// <summary>
    /// 更新的文件信息
    /// </summary>
    [Serializable]
    public class FileData
    {
        /// <summary>
        /// 文件名称（包含路径）
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 类型：文件夹/文件
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreatedDate { get; set; }
    }
}
