using System;
using System.ComponentModel;

namespace JsonLeeCMS.Control
{
    [AttributeUsage(AttributeTargets.All)]
    internal class ANPDefaultValueAttribute : DefaultValueAttribute
    {
        private bool localized;

        public ANPDefaultValueAttribute(string name)
            : base(name)
        {
            this.localized = false;
        }

        public override object Value
        {
            get
            {
                if (!this.localized)
                {
                    this.localized = true;
                    string str = (string)base.Value;
                    if (!string.IsNullOrEmpty(str))
                    {
                        return JsonLeeCMS.Control.SR.GetString(str);
                    }
                }
                return (string)base.Value;
            }
        }
    }
}

