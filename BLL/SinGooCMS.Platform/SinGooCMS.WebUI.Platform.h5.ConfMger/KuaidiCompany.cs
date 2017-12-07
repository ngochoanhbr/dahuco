using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class KuaidiCompany : H5ManagerPageBase
	{
		protected TextBox AuthKey;

		protected Button btn_SaveKey;

		protected Repeater Repeater1;

		protected Button btn_SaveRule;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			this.AuthKey.Text = PageBase.config.Kuaidi100Key;
			this.Repeater1.DataSource = KuaidiCom.Load();
			this.Repeater1.DataBind();
		}

		protected void btn_SaveRule_Click(object sender, System.EventArgs e)
		{
			try
			{
				string[] array = base.Request.Form["_companyname"].Split(new char[]
				{
					','
				});
				string[] array2 = base.Request.Form["_companycode"].Split(new char[]
				{
					','
				});
				System.Collections.Generic.List<KuaidiComInfo> list = new System.Collections.Generic.List<KuaidiComInfo>();
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!string.IsNullOrEmpty(array[i]) && !string.IsNullOrEmpty(array2[i]))
						{
							list.Add(new KuaidiComInfo
							{
								AutoID = i + 1,
								CompanyName = array[i],
								CompanyCode = array2[i],
								ComSite = string.Empty
							});
						}
					}
				}
				System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("/Config/kuaidicom.json"), JsonUtils.ObjectToJson<System.Collections.Generic.List<KuaidiComInfo>>(list), System.Text.Encoding.UTF8);
				this.BindData();
				base.ShowMsg("Thao tác thành công");
			}
			catch (System.Exception ex)
			{
				base.ShowMsg(ex.Message);
			}
		}

		protected void btn_SaveKey_Click(object sender, System.EventArgs e)
		{
			BaseConfigInfo cacheBaseConfig = ConfigProvider.GetCacheBaseConfig();
			cacheBaseConfig.Kuaidi100Key = WebUtils.GetString(this.AuthKey.Text);
			if (ConfigProvider.Update(cacheBaseConfig))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
				CacheUtils.Del("JsonLeeCMS_CacheForVER");
				CacheUtils.Del("JsonLeeCMS_CacheForGetCMSNode");
				CacheUtils.Del("JsonLeeCMS_CacheForGetShopCategory");
				CacheUtils.Del("JsonLeeCMS_CacheForProCATEGORY");
				CacheUtils.Del("JsonLeeCMS_CacheForUrlRewriteCustom");
                PageBase.log.AddEvent(base.LoginAccount.AccountName, "Update Express 100 Authorize Key Success");
				base.ShowMsg("Thao tác thành công");
			}
			else
			{
				base.ShowMsg("Thao tác thất bại");
			}
		}
	}
}
