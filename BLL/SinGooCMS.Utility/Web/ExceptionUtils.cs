using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SinGooCMS.Utility
{
    public static class ExceptionUtils
    {
        public static string FormatMessage(Exception e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            return string.Format("{0}: {1}", e.GetType().Name, e.Message);
        }
    }
}
