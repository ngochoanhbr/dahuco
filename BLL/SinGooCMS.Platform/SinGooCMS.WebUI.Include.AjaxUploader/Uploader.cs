using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web;
using System.Web.UI;

namespace SinGooCMS.WebUI.Include.AjaxUploader
{
	public class Uploader : Page
	{
		protected string CurrentModuleCode
		{
			get
			{
				return WebUtils.GetQueryString("Module");
			}
		}

		protected int CurrentModuleID
		{
			get
			{
				int result;
				try
				{
					result = WebUtils.GetInt(DEncryptUtils.DESDecode(this.CurrentModuleCode));
				}
				catch
				{
					result = 0;
				}
				return result;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string empty = string.Empty;
			string text = string.Empty;
			if (base.Request.RequestType.ToUpper() == "POST")
			{
				text = base.Request.Form["t"];
				string text2 = text;
				if (text2 != null)
				{
					if (!(text2 == "uploadbymanager"))
					{
						if (text2 == "uploadbyUser")
						{
							this.UploadbyUser();
						}
					}
					else
					{
						this.UploadByManager();
					}
				}
			}
		}

		private void UploadByManager()
		{
			this.UploadByTool(UserType.Manager);
		}

		private void UploadbyUser()
		{
			this.UploadByTool(UserType.User);
		}

		private void UploadByTool(UserType userType)
		{
			string empty = string.Empty;
			string text = string.Empty;
			AccountInfo accountInfo = (userType == UserType.Manager) ? Account.GetLoginAccount() : null;
			UserInfo userInfo = (userType == UserType.User) ? SinGooCMS.BLL.User.GetLoginUser() : null;
			HttpPostedFile postFile = HttpContext.Current.Request.Files["Filedata"];
			if (userType == UserType.Manager && accountInfo == null)
			{
				HttpContext.Current.Response.Write("{\"status\":0,\"filename\":\"\",\"thumb\":\"\",\"errmsg\":\"未登录\"}");
			}
			else if (userType == UserType.User && userInfo == null)
			{
				HttpContext.Current.Response.Write("{\"status\":0,\"filename\":\"\",\"thumb\":\"\",\"errmsg\":\"未登录\"}");
			}
			else
			{
				switch (userType)
				{
				case UserType.Manager:
					text = new PageBase().UploadFile(postFile, WebUtils.GetFormInt("vfolderid", -1), UserType.Manager, accountInfo.AccountName, ref empty);
					break;
				case UserType.User:
					text = new PageBase().UploadFileByUser(postFile, userInfo, ref empty);
					break;
				}
				if (!string.IsNullOrEmpty(text))
				{
					HttpContext.Current.Response.Write(string.Concat(new string[]
					{
						"{\"status\":1,\"filename\":\"",
						text,
						"\",\"thumb\":\"",
						WebUtils.GetThumb(text),
						"\",\"errsmg\":\"\"}"
					}));
				}
				else
				{
					HttpContext.Current.Response.Write("{\"status\":0,\"filename\":\"\",\"thumb\":\"\",\"errmsg\":\"" + empty + "\"}");
				}
			}
		}
	}
}
