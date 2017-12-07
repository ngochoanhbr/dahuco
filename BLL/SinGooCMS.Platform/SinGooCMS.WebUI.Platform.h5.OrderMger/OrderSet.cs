using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.OrderMger
{
	public class OrderSet : H5ManagerPageBase
	{
		protected H5TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected H5TextBox TextBox3;

		protected H5TextBox TextBox4;

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
				this.TextBox1.Text = cacheBaseConfig.LimitBuyPayExpire.ToString();
				this.TextBox2.Text = cacheBaseConfig.BuyPayExpire.ToString();
				this.TextBox3.Text = cacheBaseConfig.SignExpire.ToString();
				this.TextBox4.Text = cacheBaseConfig.AfterSaleExpire.ToString();
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
				baseConfigInfo.LimitBuyPayExpire = WebUtils.GetInt(this.TextBox1.Text);
				baseConfigInfo.BuyPayExpire = WebUtils.GetInt(this.TextBox2.Text);
				baseConfigInfo.SignExpire = WebUtils.GetInt(this.TextBox3.Text);
				baseConfigInfo.AfterSaleExpire = WebUtils.GetInt(this.TextBox4.Text);
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新基本配置的订单设置成功");
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
