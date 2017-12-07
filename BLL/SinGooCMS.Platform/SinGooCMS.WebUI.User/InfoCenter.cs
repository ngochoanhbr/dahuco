using SinGooCMS.BLL;
using System;

namespace SinGooCMS.WebUI.User
{
    public class InfoCenter : SinGooCMS.BLL.Custom.UIPageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedLogin = true;
			base.Put("nearorders", Orders.GetList(3, "UserID=" + base.UserID, "AutoID desc"));
			base.Put("nearfavs", Favorites.GetList(3, "UserID=" + base.UserID, "AutoID desc"));
			base.UsingClient("user/会员中心.html");
		}
	}
}
