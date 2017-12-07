using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
    public partial class PaymentInfo
    {
        private IList<PayTypeSetting> _lstSet = new List<PayTypeSetting>();
        public IList<PayTypeSetting> PayTypeSettings
        {
            get
            {
                return this._lstSet;
            }
            set
            {
                this._lstSet = value;
            }
        }
    }
}
