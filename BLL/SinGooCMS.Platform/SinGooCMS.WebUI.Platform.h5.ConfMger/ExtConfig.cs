using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class ExtConfig : H5ManagerPageBase
	{
		protected TextBox badword;

		protected TextBox bwreplaceword;

		protected H5TextBox txtdefkdfee;

		protected H5TextBox txtdefemsfee;

		protected HtmlInputCheckBox cntwtrans;

		protected TextBox TextBox6;

		protected TextBox TextBox7;

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
				this.badword.Text = cacheBaseConfig.BadWords;
				this.bwreplaceword.Text = cacheBaseConfig.BWReplaceWord;
				this.txtdefkdfee.Text = cacheBaseConfig.DefKuaidiFee.ToString("f2");
				this.txtdefemsfee.Text = cacheBaseConfig.DefEMSFee.ToString("f2");
				this.cntwtrans.Checked = cacheBaseConfig.IsCNTWTrans;
				this.TextBox6.Text = cacheBaseConfig.DefaultHtmlEditor;
				this.TextBox7.Text = cacheBaseConfig.STATLink;
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
				baseConfigInfo.BadWords = WebUtils.GetString(this.badword.Text);
				baseConfigInfo.BWReplaceWord = WebUtils.GetString(this.bwreplaceword.Text);
				baseConfigInfo.DefKuaidiFee = WebUtils.GetDecimal(this.txtdefkdfee.Text, 10.0m);
				baseConfigInfo.DefEMSFee = WebUtils.GetDecimal(this.txtdefemsfee.Text, 30.0m);
				baseConfigInfo.IsCNTWTrans = this.cntwtrans.Checked;
				baseConfigInfo.DefaultHtmlEditor = WebUtils.GetString(this.TextBox6.Text);
				baseConfigInfo.STATLink = WebUtils.GetString(this.TextBox7.Text);
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					CacheUtils.Del("JsonLeeCMS_CacheForVER");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Cấu hình khác được cập nhật thành công");
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
