using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Entity
{
    public partial class PostageModelInfo
    {
        #region 扩展字段

        /// <summary>
        /// 邮费规则
        /// </summary>
        public IList<PostageItem> PostageItems
        {
            get
            {
                //由RuleSet转化而来
                if (!string.IsNullOrEmpty(RuleSet))
                {
                    return SinGooCMS.Utility.JsonUtils.JsonToObject<List<PostageItem>>(RuleSet);
                }

                return null;
            }
        }

        #endregion
    }

    /// <summary>
    /// 邮费设置
    /// </summary>
    public class PostageItem
    {
        /// 省份区域ID
        /// </summary>
        public string AreaIDs { get; set; }
        /// 省份区域名称
        /// </summary>
        public string AreaNames { get; set; }
        /// <summary>
        /// 快递费用
        /// </summary>
        public decimal ExpFee { get; set; }
        /// <summary>
        /// 快递每增一件费用
        /// </summary>
        public decimal ExpAddoneFee { get; set; }
    }
}
