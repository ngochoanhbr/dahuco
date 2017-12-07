using System;

namespace SinGooCMS.Plugin
{
	public interface ISMS
	{
		string SMSUrl
		{
			get;
			set;
		}

		string SMSUid
		{
			get;
			set;
		}

		string SMSPwd
		{
			get;
			set;
		}

		string Mobile
		{
			get;
			set;
		}

		string SMSContent
		{
			get;
			set;
		}

		bool IsSuccess
		{
			get;
		}

		string SendMsg();

		string SendMsg(string strMobile, string strContent);
	}
}
