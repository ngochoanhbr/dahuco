using System;

namespace SinGooCMS.Entity
{
    [Serializable]
    public class PayTypeSetting
    {
        //支付类型设置

        /// <summary>
        /// 类型名称 如 AlipayDirect
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型显示名称 如 即时到账
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 提交地址
        /// </summary>
        public string SendUrl { get; set; }

        /// <summary>
        /// 返回处理地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 异步返回处理地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 是否移动端
        /// </summary>
        public bool IsMobile { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}

