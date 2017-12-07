using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class MailConfig : H5ManagerPageBase
	{
		protected H5TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected H5TextBox TextBox5;

		protected HtmlInputCheckBox CheckBox6;

		protected H5TextBox txtReciver;

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
				this.TextBox1.Text = cacheBaseConfig.ServMailAccount;
				this.TextBox2.Text = cacheBaseConfig.ServMailUserName;
				this.TextBox3.Text = cacheBaseConfig.ServMailUserPwd;
				this.TextBox4.Text = cacheBaseConfig.ServMailSMTP;
				this.TextBox5.Text = cacheBaseConfig.ServMailPort;
				this.CheckBox6.Checked = cacheBaseConfig.ServMailIsSSL;
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
				baseConfigInfo.ServMailAccount = WebUtils.GetString(this.TextBox1.Text);
				baseConfigInfo.ServMailUserName = WebUtils.GetString(this.TextBox2.Text);
				string @string = WebUtils.GetString(this.TextBox3.Text);
				if (!string.IsNullOrEmpty(@string))
				{
					baseConfigInfo.ServMailUserPwd = @string;
				}
				baseConfigInfo.ServMailSMTP = WebUtils.GetString(this.TextBox4.Text);
				baseConfigInfo.ServMailPort = WebUtils.GetString(this.TextBox5.Text);
				baseConfigInfo.ServMailIsSSL = this.CheckBox6.Checked;
				if (!string.IsNullOrEmpty(baseConfigInfo.ServMailAccount) && !ValidateUtils.IsEmail(baseConfigInfo.ServMailAccount))
				{
					base.ShowMsg("Định dạng địa chỉ hệ thống-mail là không chính xác, xin vui lòng để trống nếu không cần thiết");
				}
				else if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					CacheUtils.Del("JsonLeeCMS_CacheForVER");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Cấu hình mail cập nhật thành công");
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
			string @string = WebUtils.GetString(this.txtReciver.Text);
			if (ValidateUtils.IsEmail(@string))
			{
				string empty = string.Empty;
				if (MsgService.SendMail(@string, PageBase.config.SiteName + "Các tin nhắn thử nghiệm", "Đây là một tin nhắn kiểm tra, nếu bạn nhận được thông báo này,Dịch vụ chuyển phát nhanh hiệu quả！", out empty))
				{
					base.ShowMsg("Thư được gửi thử nghiệm thành công");
				}
				else
				{
					base.ShowMsg(empty);
				}
			}
			else
			{
                base.ShowMsg("E-mail người nhận không phải là một địa chỉ e-mail hợp lệ");
			}
		}
	}
}
