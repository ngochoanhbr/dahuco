using SinGooCMS.DAL;
using System;

namespace SinGooCMS.BLL
{
	public class BizBase : JObject
	{
		private static AbstrctFactory _dbo = null;

		protected static AbstrctFactory dbo
		{
			get
			{
				AbstrctFactory dbo;
				if (BizBase._dbo != null)
				{
					dbo = BizBase._dbo;
				}
				else
				{
					BizBase._dbo = DataFactory.CreateDataFactory("mssql", null);
					dbo = BizBase._dbo;
				}
				return dbo;
			}
		}
	}
}
