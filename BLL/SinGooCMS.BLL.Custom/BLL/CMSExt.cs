using SinGooCMS.DAL;
using SinGooCMS.Entity;
using System;
using System.Collections.Generic;

namespace SinGooCMS.BLL.Custom
{
	public class CMSExt : JObject
	{
		private static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);

		public string ShowSample()
		{
			return "这是一个例子";
		}

		public int GetNewMsgCount(string strUserName)
		{
			return Message.GetNewMsgCount(strUserName);
		}
        public int GetCount()
        {
            return Country.GetCount();
        }
        public IList<CountryInfo> ListCountry()
        {
            IList<CountryInfo> result = new List<CountryInfo>();

            result = Country.GetCountrys();

            return result;
        }
	}
}
