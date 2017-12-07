using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JsonLeeCMS.Control
{
    internal class AspNetPagerIDConverter : ControlIDConverter
    {
        protected override bool FilterControl(System.Web.UI.Control control)
        {
            return (control is JsonLeeCMS.Control.AspNetPager);
        }
    }
}

