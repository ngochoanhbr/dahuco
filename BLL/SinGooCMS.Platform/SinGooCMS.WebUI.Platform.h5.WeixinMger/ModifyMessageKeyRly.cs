using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.WeixinMger
{
	public class ModifyMessageKeyRly : H5ManagerPageBase
	{
		protected TextBox TextBox5;

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
			AutoRlyInfo dataById = AutoRly.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.TextBox5.Text = dataById.MsgKey;
				this.TextBox1.Text = dataById.MsgText;
				this.TextBox2.Text = dataById.MediaPath;
				this.Image1.ImageUrl = dataById.MediaPath;
				this.Image1.Attributes.Add("data-original", dataById.MediaPath);
				this.TextBox3.Text = dataById.Description;
				this.TextBox4.Text = dataById.LinkUrl;
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
				AutoRlyInfo autoRlyInfo = new AutoRlyInfo();
				if (base.IsEdit)
				{
					autoRlyInfo = AutoRly.GetDataById(base.OpID);
				}
				autoRlyInfo.RlyType = "关键字回复";
				autoRlyInfo.MsgKey = WebUtils.GetString(this.TextBox5.Text);
				autoRlyInfo.MsgText = WebUtils.GetString(this.TextBox1.Text);
				autoRlyInfo.MediaPath = WebUtils.GetString(this.TextBox2.Text);
				autoRlyInfo.Description = WebUtils.GetString(this.TextBox3.Text);
				autoRlyInfo.LinkUrl = WebUtils.GetString(this.TextBox4.Text);
				autoRlyInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(autoRlyInfo.MsgText))
				{
					base.ShowMsg("文本内容不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (AutoRly.Add(autoRlyInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加微信自动回复关键字[" + autoRlyInfo.MsgKey + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"MessageKeyRly.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("添加失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (AutoRly.Update(autoRlyInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改微信自动回复关键字[" + autoRlyInfo.MsgKey + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"MessageKeyRly.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("修改失败");
						}
					}
				}
			}
		}
	}
}
