using SinGooCMS.Common;
using SinGooCMS.Plugin.ThirdLogin;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ThirdLogin : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			this.Repeater1.DataSource = OAuthConfig.LoadAll();
			this.Repeater1.DataBind();
		}

		protected void lnk_Switch_Click(object sender, System.EventArgs e)
		{
			string commandArgument = ((LinkButton)sender).CommandArgument;
			System.Collections.Generic.List<OAuthConfig> list = OAuthConfig.LoadAll();
			string strMsg = string.Empty;
			if (list != null && list.Count > 0)
			{
				foreach (OAuthConfig current in list)
				{
					if (current.OAuthKey == commandArgument)
					{
						current.IsEnabled = !current.IsEnabled;
						strMsg = (current.IsEnabled ? "开启" : "关闭") + "第三方登录：" + current.OAuthName;
						break;
					}
				}
				OAuthConfig.Save(list);
				this.BindData();
				PageBase.log.AddEvent(base.LoginAccount.AccountName, strMsg);
				base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
			}
		}
	}
}
