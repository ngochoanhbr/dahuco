using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Entity
{
    /// <summary>
    /// 套装产品
    /// </summary>
    public class SuitProductItemInfo : JObject
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public ProductInfo Product { get; set; }
        public int Quantity { get; set; }
    }
}
