using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SinGooCMS.BLL
{
	public class Language : BizBase
	{
		public string LangName
		{
			get;
			set;
		}

		public string LangFullName
		{
			get;
			set;
		}

		public string Alias
		{
			get;
			set;
		}

		public static List<Language> AllLang
		{
			get
			{
				List<Language> result;
				try
				{
					string xmlString = File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/language.config"));
					result = XmlSerializerUtils.Deserialize<List<Language>>(xmlString);
				}
				catch
				{
					result = new List<Language>();
				}
				return result;
			}
		}

		public static bool Contain(string strLang)
		{
			return (from p in Language.AllLang
			where p.LangName.Equals(strLang)
			select p).FirstOrDefault<Language>() != null;
		}
	}
}
