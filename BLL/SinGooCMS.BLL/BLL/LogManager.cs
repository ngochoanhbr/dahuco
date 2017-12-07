using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;

namespace SinGooCMS.BLL
{
	public class LogManager : JObject
	{
		public void AddEvent(string strManagerName, string strMsg)
		{
			this.AddEvent(strManagerName, strMsg, 2);
		}

		public void AddEvent(string strManagerName, string strMsg, int intType)
		{
			EventLogInfo eventLogInfo = new EventLogInfo();
			eventLogInfo.UserName = strManagerName;
			eventLogInfo.EventType = intType;
			eventLogInfo.Lang = JObject.cultureLang;
			string iP = IPUtils.GetIP();
			if (!string.IsNullOrEmpty(iP))
			{
				eventLogInfo.IPAddress = iP;
				if (intType.Equals(1))
				{
					eventLogInfo.IPArea = IPUtils.GetIPAreaFromPcOnline(eventLogInfo.IPAddress);
				}
				else
				{
					eventLogInfo.IPArea = string.Empty;
				}
			}
			else
			{
				eventLogInfo.IPAddress = "127.0.0.1";
				eventLogInfo.IPArea = "未知地址或者获取地址失败";
			}
			eventLogInfo.EventInfo = strMsg;
			eventLogInfo.AutoTimeStamp = DateTime.Now;
			EventLog.Add(eventLogInfo);
		}

		public void AddLoginLog(string strUserName, bool isLogined)
		{
			this.AddLoginLog(UserType.Manager, strUserName, isLogined);
		}

		public void AddLoginLog(UserType userType, string strUserName, bool isLogined)
		{
			LoginLogInfo last = LoginLog.GetLast(userType, strUserName);
			LoginLogInfo entity = new LoginLogInfo
			{
				UserType = userType.ToString(),
				UserName = strUserName,
				IPAddress = IPUtils.GetIP(),
				IPArea = ((userType == UserType.Manager) ? IPUtils.GetIPAreaFromPcOnline(IPUtils.GetIP()) : string.Empty),
				LoginStatus = (isLogined ? 1 : 0),
				LoginFailCount = ((!isLogined) ? ((last == null) ? 1 : (last.LoginFailCount + 1)) : 0),
				Lang = JObject.cultureLang,
				AutoTimeStamp = DateTime.Now
			};
			LoginLog.Add(entity);
		}

		public void AddErrLog(string strMsg, string strStackTrace)
		{
			VisitorInfo visitorInfo = STATClient.GetVisitorInfo();
			if (visitorInfo != null)
			{
				visitorInfo.ErrMessage = strMsg;
				visitorInfo.StackTrace = strStackTrace;
				Visitor.Add(visitorInfo);
			}
		}
	}
}
