using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Control;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class BaseConfig : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected TextBox TextBox4;

		protected TextBox TextBox10;

		protected TextBox TextBox5;

		protected TextBox TextBox6;

		protected DropDownList showlang;

		protected DropDownList DropDownList10;

		protected H5TextBox globalpagesize;

		protected TextBox nodehtmlrule;

		protected TextBox contenthtmlrule;

		protected TextBox htmlext;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindLanguage();
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			BaseConfigInfo cacheBaseConfig = ConfigProvider.GetCacheBaseConfig();
			if (cacheBaseConfig != null)
			{
				this.TextBox1.Text = cacheBaseConfig.SiteName;
				this.TextBox2.Text = (string.IsNullOrEmpty(cacheBaseConfig.SiteDomain) ? ("http://" + base.Request.Url.Host) : cacheBaseConfig.SiteDomain);
				this.TextBox4.Text = cacheBaseConfig.SiteLogo;
				this.TextBox10.Text = cacheBaseConfig.SiteBanner;
				this.TextBox5.Text = cacheBaseConfig.CopyRight;
				this.TextBox6.Text = cacheBaseConfig.IcpNo;
				ListItem listItem = this.showlang.Items.FindByValue(cacheBaseConfig.DefaultLang);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				ListItem listItem2 = this.DropDownList10.Items.FindByValue(cacheBaseConfig.BrowseType);
				if (listItem2 != null)
				{
					listItem2.Selected = true;
				}
				this.globalpagesize.Text = cacheBaseConfig.GlobalPageSize.ToString();
				this.nodehtmlrule.Text = cacheBaseConfig.HtmlNodeFileRule;
				this.contenthtmlrule.Text = cacheBaseConfig.HtmlFileRule;
				this.htmlext.Text = cacheBaseConfig.HtmlFileExt;
			}
		}

		private void BindLanguage()
		{
			this.showlang.DataSource = Language.AllLang;
			this.showlang.DataTextField = "Alias";
			this.showlang.DataValueField = "LangName";
			this.showlang.DataBind();
            this.showlang.Items.Insert(0, new ListItem("Không xác định", string.Empty));
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
				baseConfigInfo.SiteName = WebUtils.GetString(this.TextBox1.Text);
				baseConfigInfo.SiteDomain = WebUtils.GetString(this.TextBox2.Text).TrimEnd(new char[]
				{
					'/'
				});
				baseConfigInfo.SiteLogo = WebUtils.GetString(this.TextBox4.Text);
				baseConfigInfo.SiteBanner = WebUtils.GetString(this.TextBox10.Text);
				baseConfigInfo.CopyRight = WebUtils.GetString(this.TextBox5.Text);
				baseConfigInfo.IcpNo = WebUtils.GetString(this.TextBox6.Text);
				baseConfigInfo.DefaultLang = this.showlang.SelectedValue;
				baseConfigInfo.BrowseType = this.DropDownList10.SelectedValue;
				baseConfigInfo.GlobalPageSize = WebUtils.GetInt(this.globalpagesize.Text, 10);
				baseConfigInfo.HtmlNodeFileRule = WebUtils.GetString(this.nodehtmlrule.Text);
				baseConfigInfo.HtmlFileRule = WebUtils.GetString(this.contenthtmlrule.Text);
				baseConfigInfo.HtmlFileExt = WebUtils.GetString(this.htmlext.Text);
				if (ConfigProvider.Update(baseConfigInfo))
				{
					base.SetLanguage(baseConfigInfo.DefaultLang);
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					CacheUtils.Del("JsonLeeCMS_CacheForVER");
					CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
					CacheUtils.Del("JsonLeeCMS_CacheForGetShopCategory");
					CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
					CacheUtils.Del("JsonLeeCMS_CacheForUrlRewriteCustom");
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Cập nhật cấu hình cơ bản成功");
                    base.ShowMsg("Cập nhật成功");
				}
				else
				{
                    base.ShowMsg("Cập nhật thất bại");
				}
			}
		}
	}
}
