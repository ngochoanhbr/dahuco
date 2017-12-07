using System;
using System.ComponentModel;

namespace JsonLeeCMS.Control
{
    [AttributeUsage(AttributeTargets.All)]
    internal class ANPCategoryAttribute : CategoryAttribute
    {
        internal ANPCategoryAttribute(string name)
            : base(name)
        {
        }

        protected override string GetLocalizedString(string value)
        {
            string localizedString = base.GetLocalizedString(value);
            if (localizedString == null)
            {
                localizedString = JsonLeeCMS.Control.SR.GetString(value);
            }
            return localizedString;
        }
    }
}

