using SinGooCMS.Common;
using SinGooCMS.Plugin.ThirdLogin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class ThirdLoginConfig : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			OAuthConfig oAuthConfig = OAuthConfig.GetOAuthConfig(WebUtils.GetQueryString("key"));
			if (oAuthConfig != null)
			{
				this.TextBox1.Text = oAuthConfig.OAuthAppId;
				this.TextBox2.Text = oAuthConfig.OAuthAppKey;
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()))
			{
				System.Collections.Generic.List<OAuthConfig> list = OAuthConfig.LoadAll();
				if (list != null)
				{
					string strMsg = string.Empty;
					foreach (OAuthConfig current in list)
					{
						if (current.OAuthKey == WebUtils.GetQueryString("key"))
						{
							current.OAuthAppId = WebUtils.GetString(this.TextBox1.Text);
							current.OAuthAppKey = WebUtils.GetString(this.TextBox2.Text);
							strMsg = "配置第三方登录[" + current.OAuthName + "]参数";
							break;
						}
					}
					OAuthConfig.Save(list);
					PageBase.log.AddEvent(base.LoginAccount.AccountName, strMsg);
					MessageUtils.DialogCloseAndParentReload(this);
				}
			}
		}
	}
}
