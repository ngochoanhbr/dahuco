using System;

namespace SinGooCMS.Entity
{
    /// <summary>
    /// 区域信息
    /// </summary>
    [Serializable]
    public class ZoneInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string ZoneName { get; set; }
        /// <summary>
        /// 区域名称拼音
        /// </summary>
        public string ZoneEnName { get; set; }
        /// <summary>
        /// 上级ID
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Depth { get; set; }
    }
}
