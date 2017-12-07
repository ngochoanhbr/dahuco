using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinGooCMS.Plugin.ThirdLogin
{
	public class Helper
	{
		public static void RegAndLogin(string strUserName, string strSex, string strHeaderPhoto)
		{
			UserInfo userInfo = User.GetUserByName(strUserName);
			if (userInfo == null)
			{
				IEnumerable<UserGroupInfo> source = from p in UserGroup.GetCacheUserGroupList()
				where p.GroupName.Equals("个人会员")
				select p;
				IEnumerable<UserLevelInfo> source2 = from p in UserLevel.GetCacheUserLevelList()
				where p.LevelName.Equals("初级会员")
				select p;
				TaoBaoAreaInfo iPAreaFromTaoBao = IPUtils.GetIPAreaFromTaoBao(IPUtils.GetIP());
				userInfo = new UserInfo
				{
					UserName = strUserName,
					Password = User.GetEncodePwd(StringUtils.GetRandomNumber(3, false)),
					GroupID = ((source.Count<UserGroupInfo>() > 0) ? source.First<UserGroupInfo>().AutoID : 0),
					LevelID = ((source2.Count<UserLevelInfo>() > 0) ? source2.First<UserLevelInfo>().AutoID : 0),
					Email = string.Empty,
					Mobile = string.Empty,
					NickName = strUserName,
					Gender = strSex,
					HeaderPhoto = strHeaderPhoto,
					Country = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.country),
					Province = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.region),
					City = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.city),
					County = ((iPAreaFromTaoBao == null) ? string.Empty : iPAreaFromTaoBao.data.country),
					Status = 99,
					AutoTimeStamp = DateTime.Now
				};
				User.Reg(userInfo);
			}
			if (User.UserLogin(userInfo.UserName, userInfo.Password) == LoginStatus.Success)
			{
				HttpContext.Current.Response.Redirect("/user/infocenter");
			}
		}
	}
}
