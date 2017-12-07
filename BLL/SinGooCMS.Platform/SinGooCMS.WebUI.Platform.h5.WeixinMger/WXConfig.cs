using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using SinGooCMS.Weixin;
using System;
using System.Text;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.WeixinMger
{
	public class WXConfig : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected TextBox TextBox5;

		protected H5TextBox TextBox6;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			this.TextBox1.Text = SinGooCMS.Weixin.Config.AppID;
            this.TextBox2.Text = SinGooCMS.Weixin.Config.AppSecret;
            this.TextBox3.Text = SinGooCMS.Weixin.Config.URL;
            this.TextBox4.Text = SinGooCMS.Weixin.Config.Token;
            this.TextBox5.Text = SinGooCMS.Weixin.Config.EncodingAESKey;
            this.TextBox6.Text = SinGooCMS.Weixin.Config.ExpireMinutes.ToString();
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><Config>");
				stringBuilder.Append("<AppID>");
				stringBuilder.Append(WebUtils.GetString(this.TextBox1.Text).Trim());
				stringBuilder.Append("</AppID>");
				stringBuilder.Append("<AppSecret>");
				stringBuilder.Append(WebUtils.GetString(this.TextBox2.Text).Trim());
				stringBuilder.Append("</AppSecret>");
				stringBuilder.Append("<URL>");
				stringBuilder.Append(WebUtils.GetString(this.TextBox3.Text).Trim());
				stringBuilder.Append("</URL>");
				stringBuilder.Append("<Token>");
				stringBuilder.Append(WebUtils.GetString(this.TextBox4.Text).Trim());
				stringBuilder.Append("</Token>");
				stringBuilder.Append("<EncodingAESKey>");
				stringBuilder.Append(WebUtils.GetString(this.TextBox5.Text).Trim());
				stringBuilder.Append("</EncodingAESKey>");
				stringBuilder.Append("<ExpireMinutes>");
				stringBuilder.Append(WebUtils.GetInt(this.TextBox6.Text).ToString());
				stringBuilder.Append("</ExpireMinutes>");
				stringBuilder.Append("</Config>");
				try
				{
					FileUtils.WriteFileContent(base.Server.MapPath("/Config/weixin.config"), stringBuilder.ToString().Trim(), false);
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新微信公众号配置成功");
					base.ShowMsg("Cập nhật thành công");
				}
				catch
				{
					base.ShowMsg("Cập nhật thất bại，请检查文件[/Config/weixinconfig.xml]是否存在并且可写！");
				}
			}
		}
	}
}
