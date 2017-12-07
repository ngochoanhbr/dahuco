using System;
using System.Collections.Generic;
using System.Text;

namespace SinGooCMS.WebIM
{
    public class ComtBase
    {
        protected static SinGooCMS.DAL.AbstrctFactory dbo = SinGooCMS.DAL.DataFactory.CreateDataFactory("mssql", null);
    }
}
