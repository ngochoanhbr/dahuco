using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.StatMger
{
	public class StatSwitch : H5ManagerPageBase
	{
		protected HtmlInputCheckBox isstatopen;

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
			this.isstatopen.Checked = PageBase.config.VisitRec;
		}

		protected void btn_Save_Click(object sender, System.EventArgs e)
		{
			BaseConfigInfo cacheBaseConfig = ConfigProvider.GetCacheBaseConfig();
			cacheBaseConfig.VisitRec = this.isstatopen.Checked;
			if (ConfigProvider.Update(cacheBaseConfig))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
				PageBase.log.AddEvent(base.LoginAccount.AccountName, cacheBaseConfig.VisitRec ? "开启流量统计" : "关闭流量统计");
				base.ShowMsg("Thao tác thành công");
			}
			else
			{
				base.ShowMsg("Thao tác thất bại");
			}
		}
	}
}
