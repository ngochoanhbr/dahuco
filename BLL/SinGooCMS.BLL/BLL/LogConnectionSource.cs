using SinGooCMS.DAL.Utils;
using SinGooCMS.Utility;
using System;
using System.Web;

namespace SinGooCMS.BLL
{
	public static class LogConnectionSource
	{
		public static string LogDBType
		{
			get;
			set;
		}

		public static string LogConnectionString
		{
			get;
			set;
		}

		static LogConnectionSource()
		{
			string text = ConfigUtils.GetAppSetting<string>("LogDBType");
			text = text.Trim().ToUpper();
			if (!text.Equals("MSSQL") && !text.Equals("SQLITE"))
			{
				text = "MSSQL";
			}
			LogConnectionSource.LogDBType = text;
			string appSetting = ConfigUtils.GetAppSetting<string>("LogDBConnStr");
			string text2 = ConfigUtils.GetAppSetting<string>("LogDBPath");
			if (LogConnectionSource.LogDBType.Equals("MSSQL"))
			{
				if (string.IsNullOrEmpty(appSetting) || appSetting.Trim().ToUpper().Equals("NULL"))
				{
					LogConnectionSource.LogConnectionString = DBHelper.ConnectionString;
				}
				else
				{
					LogConnectionSource.LogConnectionString = appSetting;
				}
			}
			else if (LogConnectionSource.LogDBType.Equals("SQLITE"))
			{
				if (string.IsNullOrEmpty(text2))
				{
					throw new Exception("没有找到有效的SQLite文件路径");
				}
				text2 = HttpContext.Current.Server.MapPath(text2);
				LogConnectionSource.LogConnectionString = "Data Source=" + text2 + ";Initial Catalog=sqlite;Integrated Security=True;min pool size=10;max pool size=512";
			}
		}
	}
}
