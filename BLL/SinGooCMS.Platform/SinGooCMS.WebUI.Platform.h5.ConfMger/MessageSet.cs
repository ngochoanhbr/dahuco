using SinGooCMS.Common;
using SinGooCMS.Config;
using SinGooCMS.Plugin;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ConfMger
{
	public class MessageSet : H5ManagerPageBase
	{
		protected Repeater Repeater1;

		protected TextBox managermail;

		protected TextBox managermobile;

		protected Repeater Repeater2;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			this.Repeater1.DataSource = SinGooCMS.Plugin.MessageSet.Load(UserType.User);
			this.Repeater1.DataBind();
			this.Repeater2.DataSource = SinGooCMS.Plugin.MessageSet.Load(UserType.Manager);
			this.Repeater2.DataBind();
		}

		protected void btn_SaveRule_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.Collections.Generic.Dictionary<string, SinGooCMS.Plugin.MessageSet> dictionary = SinGooCMS.Plugin.MessageSet.LoadDict();
				string[] array = base.Request.Form["_setkey"].Split(new char[]
				{
					','
				});
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						dictionary[array[i]].IsSendMsg = (base.Request.Form["_chk_" + array[i] + "_msg"] != null);
						dictionary[array[i]].IsSendMail = (base.Request.Form["_chk_" + array[i] + "_mail"] != null);
						dictionary[array[i]].IsSendSMS = (base.Request.Form["_chk_" + array[i] + "_sms"] != null);
					}
				}
				SinGooCMS.Plugin.MessageSet.Save(dictionary);
				BaseConfigInfo baseConfigInfo = ConfigProvider.GetCacheBaseConfig();
				if (baseConfigInfo == null)
				{
					baseConfigInfo = new BaseConfigInfo();
				}
				baseConfigInfo.ManagerMail = WebUtils.GetString(this.managermail.Text);
				baseConfigInfo.ManagerMobile = WebUtils.GetString(this.managermobile.Text);
				if (ConfigProvider.Update(baseConfigInfo))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForGetBaseConfig");
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "Sửa đổi cài đặt báo có tin nhắn thành công");
					base.ShowMsg("Thao tác thành công");
				}
				else
				{
					base.ShowMsg("Thao tác thất bại");
				}
			}
			catch (System.Exception ex)
			{
				base.ShowMsg("Thao tác thất bại：" + ex.Message);
			}
		}
	}
}
