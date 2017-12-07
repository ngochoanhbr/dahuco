using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.WeixinMger
{
	public class MessageRly : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected FullImage Image1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

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
			AutoRlyInfo focusRly = AutoRly.GetFocusRly();
			if (focusRly != null)
			{
				this.TextBox1.Text = focusRly.MsgText;
				this.TextBox2.Text = focusRly.MediaPath;
				this.Image1.ImageUrl = focusRly.MediaPath;
				this.Image1.Attributes.Add("data-original", focusRly.MediaPath);
				this.TextBox3.Text = focusRly.Description;
				this.TextBox4.Text = focusRly.LinkUrl;
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
				string a = "update";
				AutoRlyInfo autoRlyInfo = AutoRly.GetFocusRly();
				if (autoRlyInfo == null)
				{
					a = "add";
					autoRlyInfo = new AutoRlyInfo();
					autoRlyInfo.RlyType = "关注回复";
					autoRlyInfo.MsgKey = "FoucusRly";
				}
				autoRlyInfo.MsgText = WebUtils.GetString(this.TextBox1.Text);
				autoRlyInfo.MediaPath = WebUtils.GetString(this.TextBox2.Text);
				autoRlyInfo.Description = WebUtils.GetString(this.TextBox3.Text);
				autoRlyInfo.LinkUrl = WebUtils.GetString(this.TextBox4.Text);
				autoRlyInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(autoRlyInfo.MsgText))
				{
					base.ShowMsg("文本内容不能为空");
				}
				else if ((a == "add" && AutoRly.Add(autoRlyInfo) > 0) || (a == "update" && AutoRly.Update(autoRlyInfo)))
				{
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新关注回复成功");
					base.ShowMsg("Thao tác thành công");
				}
				else
				{
					base.ShowMsg("Thao tác thất bại");
				}
			}
		}
	}
}
