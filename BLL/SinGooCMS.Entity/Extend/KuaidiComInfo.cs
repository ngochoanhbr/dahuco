using System;

namespace SinGooCMS.Entity
{
    /// <summary>
    /// 快递公司
    /// </summary>
    [Serializable]
    public class KuaidiComInfo
    {
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string CompnayName { get; set; }
        /// <summary>
        /// 快递公司代号
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 快递公司网站
        /// </summary>
        public string ComSite { get; set; }
    }
}
