using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace SinGooCMS.Plugin
{
	public class MessageSet
	{
		public int AutoID
		{
			get;
			set;
		}

		public string SetKey
		{
			get;
			set;
		}

		public string SetType
		{
			get;
			set;
		}

		public string TagKey
		{
			get;
			set;
		}

		public string TagDesc
		{
			get;
			set;
		}

		public string ToType
		{
			get;
			set;
		}

		public bool IsSendMsg
		{
			get;
			set;
		}

		public string MessageTitle
		{
			get;
			set;
		}

		public string MessageTemplate
		{
			get;
			set;
		}

		public bool IsSendMail
		{
			get;
			set;
		}

		public string MailTitle
		{
			get;
			set;
		}

		public string MailTemplate
		{
			get;
			set;
		}

		public bool IsSendSMS
		{
			get;
			set;
		}

		public string SMSTemplate
		{
			get;
			set;
		}

		public int Sort
		{
			get;
			set;
		}

		public static IList<MessageSet> Load(UserType userType)
		{
			return (from p in MessageSet.Load()
			where p.ToType == userType.ToString()
			select p).ToList<MessageSet>();
		}

		public static IList<MessageSet> Load()
		{
			IList<MessageSet> list = new List<MessageSet>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(HttpContext.Current.Server.MapPath("/Config/messageset.config"));
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("MessageSet/Set");
			if (xmlNodeList.Count > 0)
			{
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					XmlNode xmlNode2 = xmlNode.SelectSingleNode("InnerMessage");
					XmlNode xmlNode3 = xmlNode.SelectSingleNode("MailMessage");
					XmlNode xmlNode4 = xmlNode.SelectSingleNode("SMSMessage");
					list.Add(new MessageSet
					{
						AutoID = WebUtils.GetInt(xmlNode.Attributes["AutoID"].Value),
						SetKey = xmlNode.Attributes["SetKey"].Value,
						SetType = xmlNode.Attributes["SetType"].Value,
						TagKey = xmlNode.Attributes["TagKey"].Value,
						TagDesc = xmlNode.Attributes["TagDesc"].Value,
						ToType = xmlNode.Attributes["ToType"].Value,
						IsSendMsg = (xmlNode2 != null && WebUtils.GetBool(xmlNode2.Attributes["IsSend"].Value)),
						MessageTitle = ((xmlNode2 == null) ? string.Empty : xmlNode2.Attributes["Title"].Value),
						MessageTemplate = ((xmlNode2 == null) ? string.Empty : xmlNode2.SelectSingleNode("BodyTemplate").InnerText),
						IsSendMail = (xmlNode3 != null && WebUtils.GetBool(xmlNode3.Attributes["IsSend"].Value)),
						MailTitle = ((xmlNode3 == null) ? string.Empty : xmlNode3.Attributes["Title"].Value),
						MailTemplate = ((xmlNode3 == null) ? string.Empty : xmlNode3.SelectSingleNode("BodyTemplate").InnerText),
						IsSendSMS = (xmlNode4 != null && WebUtils.GetBool(xmlNode4.Attributes["IsSend"].Value)),
						SMSTemplate = ((xmlNode4 == null) ? string.Empty : xmlNode4.SelectSingleNode("BodyTemplate").InnerText)
					});
				}
			}
			return (from p in list
			orderby p.Sort
			select p).ToList<MessageSet>();
		}

		public static Dictionary<string, MessageSet> LoadDict()
		{
			Dictionary<string, MessageSet> dictionary = new Dictionary<string, MessageSet>();
			foreach (MessageSet current in MessageSet.Load())
			{
				dictionary.Add(current.SetKey, current);
			}
			return dictionary;
		}

		public static MessageSet Get(string key)
		{
			return (from p in MessageSet.Load()
			where p.SetKey.Equals(key)
			select p).First<MessageSet>();
		}

		public static MessageSet Get(int autoID)
		{
			return (from p in MessageSet.Load()
			where p.AutoID.Equals(autoID)
			select p).First<MessageSet>();
		}

		public static void Save(Dictionary<string, MessageSet> dictMsgSet)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><MessageSet>");
			foreach (KeyValuePair<string, MessageSet> current in dictMsgSet)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"<Set AutoID=\"",
					current.Value.AutoID,
					"\" SetKey=\"",
					current.Value.SetKey,
					"\" SetType=\"",
					current.Value.SetType,
					"\" TagKey=\"",
					current.Value.TagKey,
					"\" TagDesc=\"",
					current.Value.TagDesc,
					"\" ToType=\"",
					current.Value.ToType,
					"\" Sort=\"",
					current.Value.Sort,
					"\" > <InnerMessage IsSend=\"",
					current.Value.IsSendMsg ? "1" : "0",
					"\" Title=\"",
					current.Value.MessageTitle,
					"\">    <BodyTemplate><![CDATA[",
					current.Value.MessageTemplate,
					"]]></BodyTemplate> </InnerMessage> <MailMessage IsSend=\"",
					current.Value.IsSendMail ? "1" : "0",
					"\" Title=\"",
					current.Value.MailTitle,
					"\" >   <BodyTemplate><![CDATA[",
					current.Value.MailTemplate,
					"]]></BodyTemplate> </MailMessage> <SMSMessage IsSend=\"",
					current.Value.IsSendSMS ? "1" : "0",
					"\">   <BodyTemplate><![CDATA[",
					current.Value.SMSTemplate,
					"]]></BodyTemplate> </SMSMessage></Set>"
				}));
			}
			stringBuilder.Append("</MessageSet>");
			File.WriteAllText(HttpContext.Current.Server.MapPath("/Config/messageset.config"), stringBuilder.ToString().Trim());
		}
	}
}
