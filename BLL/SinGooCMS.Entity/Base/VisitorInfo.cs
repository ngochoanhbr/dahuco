using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity
{
	[Serializable]
	public class VisitorInfo : JObject, IEntity
	{
		private int _AutoID = 0;

		private string _IPAddress = string.Empty;

		private string _OPSystem = string.Empty;

		private string _CustomerLang = string.Empty;

		private string _Navigator = string.Empty;

		private string _Resolution = string.Empty;

		private string _UserAgent = string.Empty;

		private bool _IsMobileDevice = false;

		private bool _IsSupportActiveX = false;

		private bool _IsSupportCookie = false;

		private bool _IsSupportJavascript = false;

		private string _NETVer = string.Empty;

		private bool _IsCrawler = false;

		private string _Engine = string.Empty;

		private string _KeyWord = string.Empty;

		private string _ApproachUrl = string.Empty;

		private string _VPage = string.Empty;

		private string _GETParameter = string.Empty;

		private string _POSTParameter = string.Empty;

		private string _CookieParameter = string.Empty;

		private string _ErrMessage = string.Empty;

		private string _StackTrace = string.Empty;

		private string _Lang = string.Empty;

		private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

		private static List<string> _listField = null;

		public int AutoID
		{
			get
			{
				return this._AutoID;
			}
			set
			{
				this._AutoID = value;
			}
		}

		public string IPAddress
		{
			get
			{
				return this._IPAddress;
			}
			set
			{
				this._IPAddress = value;
			}
		}

		public string OPSystem
		{
			get
			{
				return this._OPSystem;
			}
			set
			{
				this._OPSystem = value;
			}
		}

		public string CustomerLang
		{
			get
			{
				return this._CustomerLang;
			}
			set
			{
				this._CustomerLang = value;
			}
		}

		public string Navigator
		{
			get
			{
				return this._Navigator;
			}
			set
			{
				this._Navigator = value;
			}
		}

		public string Resolution
		{
			get
			{
				return this._Resolution;
			}
			set
			{
				this._Resolution = value;
			}
		}

		public string UserAgent
		{
			get
			{
				return this._UserAgent;
			}
			set
			{
				this._UserAgent = value;
			}
		}

		public bool IsMobileDevice
		{
			get
			{
				return this._IsMobileDevice;
			}
			set
			{
				this._IsMobileDevice = value;
			}
		}

		public bool IsSupportActiveX
		{
			get
			{
				return this._IsSupportActiveX;
			}
			set
			{
				this._IsSupportActiveX = value;
			}
		}

		public bool IsSupportCookie
		{
			get
			{
				return this._IsSupportCookie;
			}
			set
			{
				this._IsSupportCookie = value;
			}
		}

		public bool IsSupportJavascript
		{
			get
			{
				return this._IsSupportJavascript;
			}
			set
			{
				this._IsSupportJavascript = value;
			}
		}

		public string NETVer
		{
			get
			{
				return this._NETVer;
			}
			set
			{
				this._NETVer = value;
			}
		}

		public bool IsCrawler
		{
			get
			{
				return this._IsCrawler;
			}
			set
			{
				this._IsCrawler = value;
			}
		}

		public string Engine
		{
			get
			{
				return this._Engine;
			}
			set
			{
				this._Engine = value;
			}
		}

		public string KeyWord
		{
			get
			{
				return this._KeyWord;
			}
			set
			{
				this._KeyWord = value;
			}
		}

		public string ApproachUrl
		{
			get
			{
				return this._ApproachUrl;
			}
			set
			{
				this._ApproachUrl = value;
			}
		}

		public string VPage
		{
			get
			{
				return this._VPage;
			}
			set
			{
				this._VPage = value;
			}
		}

		public string GETParameter
		{
			get
			{
				return this._GETParameter;
			}
			set
			{
				this._GETParameter = value;
			}
		}

		public string POSTParameter
		{
			get
			{
				return this._POSTParameter;
			}
			set
			{
				this._POSTParameter = value;
			}
		}

		public string CookieParameter
		{
			get
			{
				return this._CookieParameter;
			}
			set
			{
				this._CookieParameter = value;
			}
		}

		public string ErrMessage
		{
			get
			{
				return this._ErrMessage;
			}
			set
			{
				this._ErrMessage = value;
			}
		}

		public string StackTrace
		{
			get
			{
				return this._StackTrace;
			}
			set
			{
				this._StackTrace = value;
			}
		}

		public string Lang
		{
			get
			{
				return this._Lang;
			}
			set
			{
				this._Lang = value;
			}
		}

		public DateTime AutoTimeStamp
		{
			get
			{
				return this._AutoTimeStamp;
			}
			set
			{
				this._AutoTimeStamp = value;
			}
		}

		public string DBTableName
		{
			get
			{
				return "sys_Visitor";
			}
		}

		public string PKName
		{
			get
			{
				return "AutoID";
			}
		}

		public string Fields
		{
			get
			{
				return "AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp";
			}
		}

		public List<string> FieldList
		{
			get
			{
				if (VisitorInfo._listField == null)
				{
					VisitorInfo._listField = new List<string>();
					VisitorInfo._listField.Add("AutoID");
					VisitorInfo._listField.Add("IPAddress");
					VisitorInfo._listField.Add("OPSystem");
					VisitorInfo._listField.Add("CustomerLang");
					VisitorInfo._listField.Add("Navigator");
					VisitorInfo._listField.Add("Resolution");
					VisitorInfo._listField.Add("UserAgent");
					VisitorInfo._listField.Add("IsMobileDevice");
					VisitorInfo._listField.Add("IsSupportActiveX");
					VisitorInfo._listField.Add("IsSupportCookie");
					VisitorInfo._listField.Add("IsSupportJavascript");
					VisitorInfo._listField.Add("NETVer");
					VisitorInfo._listField.Add("IsCrawler");
					VisitorInfo._listField.Add("Engine");
					VisitorInfo._listField.Add("KeyWord");
					VisitorInfo._listField.Add("ApproachUrl");
					VisitorInfo._listField.Add("VPage");
					VisitorInfo._listField.Add("GETParameter");
					VisitorInfo._listField.Add("POSTParameter");
					VisitorInfo._listField.Add("CookieParameter");
					VisitorInfo._listField.Add("ErrMessage");
					VisitorInfo._listField.Add("StackTrace");
					VisitorInfo._listField.Add("Lang");
					VisitorInfo._listField.Add("AutoTimeStamp");
				}
				return VisitorInfo._listField;
			}
		}
	}
}
