using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace SinGooCMS.BLL
{
	public class Industry : BizBase
	{
		public static IList<IndustryInfo> GetList()
		{
			string strValue = File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/industry.json"), Encoding.UTF8);
			return JsonUtils.JsonToObject<List<IndustryInfo>>(strValue);
		}
	}
}
