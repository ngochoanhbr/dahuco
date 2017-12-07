using System;

namespace SinGooCMS.Upgrade
{
    /// <summary>
    /// 服务器版本信息
    /// </summary>
    [Serializable]
    public class ServVerInfo
    {
        /// <summary>
        /// 版本号 v1.1.0
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本代号 110
        /// </summary>
        public int VerCode { get; set; }

        /// <summary>
        /// 更新文件包 v1.1.0.rar
        /// </summary>
        public string ZipFile { get; set; }

        /// <summary>
        /// 最后更新日期 2014-11-15
        /// </summary>
        public string LastUpdateDate { get; set; }
    }
}
