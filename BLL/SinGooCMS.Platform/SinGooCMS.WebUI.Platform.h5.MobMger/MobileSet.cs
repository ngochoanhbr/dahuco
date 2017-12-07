using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.MobMger
{
	public class MobileSet : H5ManagerPageBase
	{
		protected HtmlInputCheckBox ismobopen;

		protected Button btn_Save;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			this.ismobopen.Checked = PageBase.config.EnabledMobile;
		}

		protected void btn_Save_Click(object sender, System.EventArgs e)
		{
			BaseConfigInfo cacheBaseConfig = ConfigProvider.GetCacheBaseConfig();
			cacheBaseConfig.EnabledMobile = this.ismobopen.Checked;
			if (ConfigProvider.Update(cacheBaseConfig))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
				PageBase.log.AddEvent(base.LoginAccount.AccountName, cacheBaseConfig.EnabledMobile ? "启用移动端网站" : "关闭移动端网站");
				base.ShowMsg("Thao tác thành công");
			}
			else
			{
				base.ShowMsg("Thao tác thất bại");
			}
		}
	}
}
