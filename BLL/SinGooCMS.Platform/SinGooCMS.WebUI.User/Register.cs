using SinGooCMS.BLL;

using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinGooCMS.WebUI.User
{
    public class Register : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsPost)
			{
				RegType regType = (RegType)System.Enum.Parse(typeof(RegType), WebUtils.GetFormString("_regtype", "Normal"));
				string strGroupName = WebUtils.GetFormString("_usergroupname", "个人会员");
				System.Collections.Generic.IEnumerable<UserGroupInfo> source = from p in UserGroup.GetCacheUserGroupList()
				where p.GroupName.Equals(strGroupName)
				select p;
				System.Collections.Generic.IEnumerable<UserLevelInfo> source2 = (from p in UserLevel.GetCacheUserLevelList()
				orderby p.Integral
				select p).Take(1);
				TaoBaoAreaInfo iPAreaFromTaoBao = IPUtils.GetIPAreaFromTaoBao(IPUtils.GetIP());
				UserInfo userInfo = new UserInfo
				{
					UserName = WebUtils.GetFormString("_regname"),
					Password = WebUtils.GetFormString("_regpwd"),
					GroupID = ((source.Count<UserGroupInfo>() > 0) ? source.First<UserGroupInfo>().AutoID : 0),
					LevelID = ((source2.Count<UserLevelInfo>() > 0) ? source2.First<UserLevelInfo>().AutoID : 0),
					Email = ((regType == RegType.ByEmail) ? WebUtils.GetFormString("_regname") : WebUtils.GetFormString("_regemail")),
					Mobile = ((regType == RegType.ByMobile) ? WebUtils.GetFormString("_regname") : WebUtils.GetFormString("_regmobile")),
					Country = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.country),
					Province = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.region),
					City = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.city),
					County = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.country),
					Status = 99,
					AutoTimeStamp = System.DateTime.Now
				};
				string[] array = PageBase.config.SysUserName.Split(new char[]
				{
					','
				});
				string strA = string.Empty;
				switch (regType)
				{
				case RegType.ByMobile:
				{
					SMSInfo lastCheckCode = SMS.GetLastCheckCode(userInfo.Mobile);
					if (lastCheckCode != null)
					{
						strA = lastCheckCode.ValidateCode;
					}
					goto IL_27A;
				}
				case RegType.ByEmail:
				{
					SMSInfo lastCheckCode = SMS.GetLastCheckCode(userInfo.Email);
					if (lastCheckCode != null)
					{
						strA = lastCheckCode.ValidateCode;
					}
					goto IL_27A;
				}
				}
				strA = base.ValidateCode;
				IL_27A:
				if (PageBase.config.VerifycodeForReg && string.Compare(strA, WebUtils.GetFormString("_regyzm"), true) != 0)
				{
					base.WriteJsonTip(base.GetCaption("ValidateCodeIncorrect"));
				}
				else if (userInfo.GroupID.Equals(0) || userInfo.LevelID.Equals(0))
				{
					base.WriteJsonTip(base.GetCaption("Reg_MemberInfoNotComplete"));
				}
				else if (!SinGooCMS.BLL.User.IsValidUserName(userInfo.UserName) && !ValidateUtils.IsEmail(userInfo.UserName) && !ValidateUtils.IsMobilePhone(userInfo.UserName))
				{
					base.WriteJsonTip(base.GetCaption("Reg_InvalidUserName"));
				}
				else if (array.Length > 0 && array.Contains(userInfo.UserName))
				{
					base.WriteJsonTip(base.GetCaption("Reg_SystemRetainsUserName").Replace("${username}", userInfo.UserName));
				}
				else if (SinGooCMS.BLL.User.IsExistsByName(userInfo.UserName))
				{
					base.WriteJsonTip(base.GetCaption("Reg_UserNameAlreadyExists"));
				}
				else if (userInfo.Password.Length < 6)
				{
					base.WriteJsonTip(base.GetCaption("Reg_UserPwdLenNeed"));
				}
				else if (userInfo.Password != WebUtils.GetFormString("_regpwdagain"))
				{
					base.WriteJsonTip(base.GetCaption("Reg_2PwdInputNoMatch"));
				}
				else if (!string.IsNullOrEmpty(userInfo.Email) && !ValidateUtils.IsEmail(userInfo.Email))
				{
					base.WriteJsonTip(base.GetCaption("Reg_EmailTypeError"));
				}
				else if (!string.IsNullOrEmpty(userInfo.Email) && SinGooCMS.BLL.User.IsExistsByEmail(userInfo.Email))
				{
					base.WriteJsonTip(base.GetCaption("Reg_EmailAddressAlreadyExists"));
				}
				else if (!string.IsNullOrEmpty(userInfo.Mobile) && !ValidateUtils.IsMobilePhone(userInfo.Mobile))
				{
					base.WriteJsonTip(base.GetCaption("Reg_MobileTypeError"));
				}
				else if (!string.IsNullOrEmpty(userInfo.Mobile) && SinGooCMS.BLL.User.IsExistsByMobile(userInfo.Mobile))
				{
					base.WriteJsonTip(base.GetCaption("Reg_MobileAlreadyExists"));
				}
				else
				{
					int autoID = 0;
					System.Collections.Generic.IList<UserFieldInfo> fieldListByModelID = UserField.GetFieldListByModelID(userInfo.GroupID, true);
					System.Collections.Generic.Dictionary<string, UserFieldInfo> dictionary = new System.Collections.Generic.Dictionary<string, UserFieldInfo>();
					foreach (UserFieldInfo current in fieldListByModelID)
					{
						current.Value = WebUtils.GetFormString(current.FieldName);
						dictionary.Add(current.FieldName, current);
					}
					UserStatus userStatus = SinGooCMS.BLL.User.Reg(userInfo, dictionary, ref autoID);
					if (userStatus.Equals(UserStatus.Success))
					{
						new MsgService(userInfo).SendRegMsg();
						new MsgService().SendRegMsg2Mger();
						if (PageBase.config.RegGiveIntegral > 0)
						{
							userInfo.AutoID = autoID;
							AccountDetail.AddIntegral(userInfo, 1, (double)PageBase.config.RegGiveIntegral, "注册赠送积分");
						}
						UserInfo userInfo2 = new UserInfo();
						LoginStatus loginStatus = SinGooCMS.BLL.User.UserLogin(userInfo.UserName, userInfo.Password, ref userInfo2);
						string text = base.Server.UrlDecode(WebUtils.GetFormString("_regreturnurl"));
						if (!string.IsNullOrEmpty(text))
						{
							base.WriteJsonTip(true, base.GetCaption("Reg_Success"), text);
						}
						else
						{
							base.WriteJsonTip(true, base.GetCaption("Reg_Success"), "/user/regsuccess.html?info=" + base.Server.UrlEncode(base.GetCaption("Reg_SuccessWillJumpMemberCenter")));
						}
					}
					else
					{
						base.WriteJsonTip(base.GetCaption("Reg_FailWithMsg").Replace("${msg}", base.GetCaption("UserStatus_" + userStatus.ToString())));
					}
				}
			}
			else
			{
				System.Collections.Generic.IEnumerable<UserGroupInfo> source3 = from p in UserGroup.GetCacheUserGroupList()
				where p.GroupName.Equals("个人会员")
				select p;
				base.Put("usermodel", UserField.GetFieldListByModelID((source3.Count<UserGroupInfo>() > 0) ? source3.First<UserGroupInfo>().AutoID : 0, true));
				base.Put("returnurl", WebUtils.GetQueryString("returnurl"));
				base.UsingClient("user/注册.html");
			}
		}
	}
}
