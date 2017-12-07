using SinGooCMS.BLL;

using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SinGooCMS.WebUI.User
{
    public class Profile : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			if (base.IsPost)
			{
				UserInfo dataById = SinGooCMS.BLL.User.GetDataById(base.UserID);
				dataById.NickName = WebUtils.GetFormString("NickName");
				dataById.Gender = WebUtils.GetFormString("Gender");
				dataById.Birthday = WebUtils.GetFormDatetime("Birthday");
				string text = base.UploadFileByUser("headerphoto");
				if (!string.IsNullOrEmpty(text))
				{
					dataById.HeaderPhoto = text;
				}
				if (SinGooCMS.BLL.User.Update(dataById))
				{
					base.Response.Redirect(UrlRewrite.Get("profile_url"));
				}
				else
				{
					this.Alert(base.GetCaption("Profile_UpdateFail"));
				}
			}
			else
			{
				System.Collections.Generic.IEnumerable<UserGroupInfo> enumerable = from p in UserGroup.GetCacheUserGroupList()
				where p.GroupName.Equals(base.LoginUserGroup.GroupName)
				select p;
				base.Put("usermodel", SinGooCMS.BLL.User.GetFieldListWithValue(base.UserID, (enumerable == null) ? 0 : enumerable.FirstOrDefault<UserGroupInfo>().AutoID));
				base.UsingClient("user/个人资料.html");
			}
		}
	}
}
