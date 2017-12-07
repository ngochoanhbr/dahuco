using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Entity
{
    public class SmallUser
    {
        public int AutoID { get; set; }
        public string UserName { get; set; }
        public int GroupID { get; set; }
        public int LevelID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public decimal Amount { get; set; }
        public int Integral { get; set; }
        public string RealName { get; set; }
        public string Gender { get; set; }
        public int Status { get; set; }
    }
}
