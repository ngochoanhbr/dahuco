using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Entity
{
    public partial class MemberPriceInfo
    {
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SellPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 会员等级优惠是否开启
        /// </summary>
        public bool IsLvDiscountOn
        {
            get;
            set;
        }
    }
}
