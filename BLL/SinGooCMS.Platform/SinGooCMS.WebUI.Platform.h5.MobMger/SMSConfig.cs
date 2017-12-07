using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MobMger
{
	public class SMSConfig : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected TextBox TextBox5;

		protected Button btnSend;

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
			BaseConfigInfo cacheBaseConfig = ConfigProvider.GetCacheBaseConfig();
			if (cacheBaseConfig != null)
			{
				this.TextBox1.Text = cacheBaseConfig.SMSClass;
				this.TextBox3.Text = cacheBaseConfig.SMSUid;
				this.TextBox4.Text = cacheBaseConfig.SMSPwd;
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				BaseConfigInfo baseConfigInfo = ConfigProvider.GetCacheBaseConfig();
				if (baseConfigInfo == null)
				{
					baseConfigInfo = new BaseConfigInfo();
				}
				baseConfigInfo.SMSClass = WebUtils.GetString(this.TextBox1.Text);
				baseConfigInfo.SMSUid = WebUtils.GetString(this.TextBox3.Text);
				string @string = WebUtils.GetString(this.TextBox4.Text);
				if (!string.IsNullOrEmpty(@string))
				{
					baseConfigInfo.SMSPwd = @string;
				}
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					CacheUtils.Del("JsonLeeCMS_CacheForVER");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新短信配置成功");
					base.ShowMsg("Cập nhật thành công");
				}
				else
				{
					base.ShowMsg("Cập nhật thất bại");
				}
			}
		}

		protected void btnSend_Click(object sender, System.EventArgs e)
		{
			string @string = WebUtils.GetString(this.TextBox5.Text);
			ISMS iSMS = SMSProvider.Create(PageBase.config.SMSClass);
			if (iSMS == null)
			{
				base.ShowMsg("短信接口配置不正确");
			}
			else if (!ValidateUtils.IsMobilePhone(@string))
			{
				base.ShowMsg("无效的手机号码");
			}
			else
			{
				string value = iSMS.SendMsg(@string, "这是一条测试短信，如果您收到此短信，表示短信服务有效！[" + PageBase.config.SiteName + "]");
				if (iSMS.IsSuccess)
				{
					base.ShowMsg("测试短信发送成功");
				}
				else
				{
					base.ShowMsg(WebUtils.GetString(value));
				}
			}
		}
	}
}
