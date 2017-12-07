using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.WebIM
{
    public class ShortMessage
    {
        public int MsgID { get; set; }
        public int SenderID { get; set; }
        public string SenderName { get; set; }
        public string SendMsg { get; set; }
        public string DateStr { get; set; }
    }
}
