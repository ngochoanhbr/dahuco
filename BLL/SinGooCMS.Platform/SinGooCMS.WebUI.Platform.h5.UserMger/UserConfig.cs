using CKEditor.NET;
using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.UserMger
{
	public class UserConfig : H5ManagerPageBase
	{
		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected CKEditorControl TextBox6;

		protected H5TextBox TextBox7;

		protected H5TextBox TextBox8;

		protected CheckBox CheckBox1;

		protected CheckBox CheckBox2;

		protected CheckBox CheckBox3;

		protected H5TextBox TextBox9;

		protected RadioButtonList RadioButtonList10;

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
				this.TextBox2.Text = cacheBaseConfig.UserNameRule;
				this.TextBox3.Text = cacheBaseConfig.SysUserName;
				this.TextBox6.Text = cacheBaseConfig.RegAgreement;
				this.TextBox7.Text = cacheBaseConfig.RegGiveIntegral.ToString();
				this.TextBox8.Text = cacheBaseConfig.TgIntegral.ToString();
				this.CheckBox1.Checked = cacheBaseConfig.VerifycodeForReg;
				this.CheckBox2.Checked = cacheBaseConfig.VerifycodeForLogin;
				this.CheckBox3.Checked = cacheBaseConfig.VerifycodeForGetPwd;
				this.TextBox9.Text = cacheBaseConfig.TryLoginTimes.ToString();
				ListItem listItem = this.RadioButtonList10.Items.FindByText(cacheBaseConfig.CookieTime.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
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
				baseConfigInfo.UserNameRule = WebUtils.GetString(this.TextBox2.Text);
				baseConfigInfo.SysUserName = WebUtils.GetString(this.TextBox3.Text);
				baseConfigInfo.RegAgreement = StringUtils.ChkSQL(this.TextBox6.Text);
				baseConfigInfo.RegGiveIntegral = WebUtils.GetInt(this.TextBox7.Text, 10);
				baseConfigInfo.TgIntegral = WebUtils.GetInt(this.TextBox8.Text, 50);
				baseConfigInfo.VerifycodeForReg = this.CheckBox1.Checked;
				baseConfigInfo.VerifycodeForLogin = this.CheckBox2.Checked;
				baseConfigInfo.VerifycodeForGetPwd = this.CheckBox3.Checked;
				baseConfigInfo.TryLoginTimes = WebUtils.GetInt(this.TextBox9.Text, 5);
				baseConfigInfo.CookieTime = this.RadioButtonList10.SelectedValue;
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					CacheUtils.Del("JsonLeeCMS_CacheForVER");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新会员配置成功");
					base.ShowMsg("Cập nhật thành công");
				}
				else
				{
					base.ShowMsg("Cập nhật thất bại");
				}
			}
		}
	}
}
