using SinGooCMS.BLL;
using SinGooCMS.Config;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;

namespace SinGooCMS.Plugin
{
	public class MsgService : JObject
	{
		private static UserInfo user = new UserInfo();

		public MsgService()
		{
		}

		public MsgService(UserInfo paramUser)
		{
			MsgService.user = paramUser;
		}

		public void SendRegMsg()
		{
			MessageSet messageSet = MessageSet.Get("RegMsgSetting");
			if (MsgService.user != null && messageSet != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${email}",
						MsgService.user.Email
					},
					{
						"${mobile}",
						MsgService.user.Mobile
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendChangPwdMsg()
		{
			MessageSet messageSet = MessageSet.Get("ChangePwdMsgSetting");
			if (MsgService.user != null && messageSet != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendFindPwdMsg()
		{
			MessageSet messageSet = MessageSet.Get("FindPwdMsgSetting");
			if (MsgService.user != null && messageSet != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendBindEmail()
		{
			MessageSet messageSet = MessageSet.Get("BindEmailMsgSetting");
			if (MsgService.user != null && messageSet != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${email}",
						MsgService.user.Email
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendBindMobile()
		{
			MessageSet messageSet = MessageSet.Get("BindMobileMsgSetting");
			if (MsgService.user != null && messageSet != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${mobile}",
						MsgService.user.Mobile
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendOrderCreated(OrdersInfo order)
		{
			MessageSet messageSet = MessageSet.Get("OrderCreatedMsgSetting");
			if (MsgService.user != null && messageSet != null && order != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${orderno}",
						order.OrderNo
					},
					{
						"${totalfee}",
						order.OrderTotalAmount.ToString("f2")
					},
					{
						"${consignee}",
						order.Consignee
					},
					{
						"${shippingaddr}",
						string.Concat(new string[]
						{
							order.Country,
							",",
							order.Province,
							",",
							order.City,
							",",
							order.County,
							" ",
							order.Address
						})
					},
					{
						"${contactphone}",
						order.Mobile
					},
					{
						"${expire}",
						"24"
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendOrderPay(OrdersInfo order, decimal decPayAmout)
		{
			MessageSet messageSet = MessageSet.Get("OrderPayMsgSetting");
			if (MsgService.user != null && messageSet != null && order != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${orderno}",
						order.OrderNo
					},
					{
						"${totalfee}",
						order.OrderTotalAmount.ToString("f2")
					},
					{
						"${payamount}",
						decPayAmout.ToString("f2")
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendGoodsFH(OrdersInfo order, string kuaidiCompany, string kuaidiNo)
		{
			MessageSet messageSet = MessageSet.Get("GoodsFHMsgSetting");
			if (MsgService.user != null && messageSet != null && order != null)
			{
				this.SendSystemMsg2User(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${orderno}",
						order.OrderNo
					},
					{
						"${kuaidicompany}",
						kuaidiCompany
					},
					{
						"${kuaidino}",
						kuaidiNo
					},
					{
						"${consignee}",
						order.Consignee
					},
					{
						"${shippingaddr}",
						string.Concat(new string[]
						{
							order.Country,
							",",
							order.Province,
							",",
							order.City,
							",",
							order.County,
							" ",
							order.Address
						})
					},
					{
						"${contactphone}",
						order.Mobile
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		protected void SendSystemMsg2User(MessageSet setting, Dictionary<string, string> msgParams)
		{
			if (setting.IsSendMsg)
			{
				Message.SendS2UMsg(MsgService.user.UserName, setting.MessageTitle, this.ReplaceTemplate(setting.MessageTemplate, msgParams));
			}
			if (setting.IsSendMail)
			{
				MsgService.SendMail(MsgService.user.Email, setting.MailTitle, this.ReplaceTemplate(setting.MailTemplate, msgParams));
			}
			if (setting.IsSendSMS)
			{
				MsgService.SendSMS(MsgService.user.Mobile, this.ReplaceTemplate(setting.SMSTemplate, msgParams));
			}
		}

		private string ReplaceTemplate(string templateSource, Dictionary<string, string> msgParams)
		{
			foreach (KeyValuePair<string, string> current in msgParams)
			{
				templateSource = templateSource.Replace(current.Key, current.Value);
			}
			return templateSource;
		}

		public void SendRegMsg2Mger()
		{
			MessageSet messageSet = MessageSet.Get("RegNewUser");
			if (messageSet != null)
			{
				this.SendSystemMsg2Mger(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${email}",
						MsgService.user.Email
					},
					{
						"${mobile}",
						MsgService.user.Mobile
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendNewOrder2Mger(OrdersInfo order)
		{
			MessageSet messageSet = MessageSet.Get("CreateNewOrder");
			if (messageSet != null && order != null)
			{
				this.SendSystemMsg2Mger(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${orderno}",
						order.OrderNo
					},
					{
						"${totalfee}",
						order.OrderTotalAmount.ToString("f2")
					},
					{
						"${consignee}",
						order.Consignee
					},
					{
						"${shippingaddr}",
						string.Concat(new string[]
						{
							order.Country,
							",",
							order.Province,
							",",
							order.City,
							",",
							order.County,
							" ",
							order.Address
						})
					},
					{
						"${contactphone}",
						order.Mobile
					},
					{
						"${expire}",
						"24"
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		public void SendOrderPay2Mger(OrdersInfo order, decimal decPayAmout)
		{
			MessageSet messageSet = MessageSet.Get("PayTheOrder");
			if (messageSet != null && order != null)
			{
				this.SendSystemMsg2Mger(messageSet, new Dictionary<string, string>
				{
					{
						"${username}",
						MsgService.user.UserName
					},
					{
						"${orderno}",
						order.OrderNo
					},
					{
						"${totalfee}",
						order.OrderTotalAmount.ToString("f2")
					},
					{
						"${payamount}",
						decPayAmout.ToString("f2")
					},
					{
						"${sitename}",
						ConfigProvider.Configs.SiteName
					}
				});
			}
		}

		protected void SendSystemMsg2Mger(MessageSet setting, Dictionary<string, string> msgParams)
		{
			if (setting.IsSendMsg)
			{
				Message.SendSysMsg(setting.MessageTitle, this.ReplaceTemplate(setting.MessageTemplate, msgParams));
			}
			if (setting.IsSendMail && !string.IsNullOrEmpty(ConfigProvider.Configs.ManagerMail))
			{
				string[] array = ConfigProvider.Configs.ManagerMail.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (ValidateUtils.IsEmail(text))
					{
						MsgService.SendMail(text, setting.MailTitle, this.ReplaceTemplate(setting.MailTemplate, msgParams));
					}
				}
			}
			if (setting.IsSendSMS && !string.IsNullOrEmpty(ConfigProvider.Configs.ManagerMobile))
			{
				string[] array = ConfigProvider.Configs.ManagerMobile.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (ValidateUtils.IsMobilePhone(text))
					{
						MsgService.SendSMS(text, this.ReplaceTemplate(setting.SMSTemplate, msgParams));
					}
				}
			}
		}

		public static bool SendMail(string strReciveMail, string strSubject, string strMailBody)
		{
			string empty = string.Empty;
			return MsgService.SendMail(strReciveMail, strSubject, strMailBody, out empty);
		}

		public static bool SendMail(string strReciveMail, string strSubject, string strMailBody, out string strResult)
		{
			return MsgService.SendMail(strReciveMail, strSubject, strMailBody, false, out strResult);
		}

		public static bool SendMail(string strReciveMail, string strSubject, string strMailBody, bool boolIsSSL, out string strResult)
		{
			return MsgService.SendMail(ConfigProvider.Configs.SiteName, strReciveMail, strSubject, strMailBody, boolIsSSL, out strResult);
		}

		public static bool SendMail(string strFromDisplayName, string strReciveMail, string strSubject, string strMailBody, bool boolIsSSL, out string strResult)
		{
			EmailUtils emailUtils = new EmailUtils();
			emailUtils.FromMail = ConfigProvider.Configs.ServMailAccount;
			emailUtils.ToMail = strReciveMail;
			emailUtils.SmtpServer = ConfigProvider.Configs.ServMailSMTP;
			emailUtils.MailUserName = ConfigProvider.Configs.ServMailUserName;
			emailUtils.MailPassWord = ConfigProvider.Configs.ServMailUserPwd;
			emailUtils.MailFormat = EmailType.html;
			emailUtils.Subject = strSubject;
			emailUtils.Body = strMailBody;
            emailUtils.IsSSL = ConfigProvider.Configs.ServMailIsSSL;
            
			if (!string.IsNullOrEmpty(strFromDisplayName))
			{
				emailUtils.FromDisplayName = ConfigProvider.Configs.SiteName;
			}
			return emailUtils.SendEmail(out strResult);
		}

		public static int SendSMS(string strToMobile, string strContent)
		{
			string empty = string.Empty;
			return MsgService.SendSMS(strToMobile, "Normal", strContent, string.Empty, ref empty);
		}

		public static int SendSMSCheckCode(string strToMobile, string strContent, string strValidateCode)
		{
			string empty = string.Empty;
			return MsgService.SendSMS(strToMobile, "CheckCode", strContent, strValidateCode, ref empty);
		}

		public static int SendSMS(string strToMobile, string strType, string strContent, string strValidateCode, ref string retResult)
		{
			int result = 0;
			ISMS iSMS = SMSProvider.Create();
			if (iSMS != null)
			{
				iSMS.Mobile = strToMobile;
				iSMS.SMSContent = strContent;
				retResult = iSMS.SendMsg();
				if (iSMS.IsSuccess)
				{
					SMSInfo entity = new SMSInfo
					{
						SMSMob = iSMS.Mobile,
						SMSText = iSMS.SMSContent,
						SMSType = strType,
						ValidateCode = strValidateCode,
						ReturnMsg = retResult,
						Status = 1,
						AutoTimeStamp = DateTime.Now
					};
					result = BLL.SMS.Add(entity);
				}
			}
			return result;
		}
	}
}
