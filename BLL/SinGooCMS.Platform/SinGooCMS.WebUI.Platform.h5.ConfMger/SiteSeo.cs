using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class SiteSeo : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

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
				this.TextBox1.Text = cacheBaseConfig.SEOKey;
				this.TextBox2.Text = cacheBaseConfig.SEODescription;
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
				baseConfigInfo.SEOKey = WebUtils.GetString(this.TextBox1.Text);
				baseConfigInfo.SEODescription = WebUtils.GetString(this.TextBox2.Text);
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
                    PageBase.log.AddEvent(base.LoginAccount.AccountName, "Cập nhật thành công tối ưu hóa việc phân bổ tìm kiếm toàn cầu");
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
