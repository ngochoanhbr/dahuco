using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.Tools
{
	public class ChangePwd : H5ManagerPageBase
	{
		protected TextBox oldpwd;

		protected TextBox newpwd1;

		protected TextBox newpwd2;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.NeedAuthorized = false;
			if (base.LoginAccount != null && !base.IsPost)
			{
				this.TextBox3.Text = base.LoginAccount.Email;
				this.TextBox4.Text = base.LoginAccount.Mobile;
			}
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			bool flag = false;
			AccountInfo dataById = Account.GetDataById(base.LoginAccount.AutoID);
			string text = WebUtils.GetString(this.oldpwd.Text);
			string @string = WebUtils.GetString(this.newpwd1.Text);
			string string2 = WebUtils.GetString(this.newpwd2.Text);
			string string3 = WebUtils.GetString(this.TextBox3.Text);
			string string4 = WebUtils.GetString(this.TextBox4.Text);
			if (!string.IsNullOrEmpty(text))
			{
				text = DEncryptUtils.SHA512Encrypt(text);
				if (!base.LoginAccount.Password.Equals(text))
				{
					base.ShowMsg("原密码不正确！");
				}
				else if (@string.Length < 6)
				{
					base.ShowMsg("新密码不能少于6个字符！");
				}
				else if (string2 != @string)
				{
					base.ShowMsg("两次密码输入不一致！");
				}
				else
				{
					dataById.Password = DEncryptUtils.SHA512Encrypt(@string);
					flag = true;
				}
			}
			dataById.Email = string3;
			dataById.Mobile = string4;
			if (Account.Update(dataById))
			{
				if (flag)
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "管理员修改帐户密码成功", 2);
					HttpContext.Current.Session["Account"] = null;
					HttpContext.Current.Session.Remove("Account");
					FormsAuthentication.SignOut();
					base.Response.Redirect("/Platform/h5/login");
				}
			}
			else
			{
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "管理员修改帐户资料时发生了错误", 2);
				base.ShowMsg("修改失败，发生不可预知的错误");
			}
		}
	}
}
