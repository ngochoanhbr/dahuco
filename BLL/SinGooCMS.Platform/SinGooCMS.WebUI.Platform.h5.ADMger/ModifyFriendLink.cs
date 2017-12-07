using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.ADMger
{
	public class ModifyFriendLink : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected H5TextBox TextBox2;

		protected FullImage Image1;

		protected TextBox TextBox3;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			LinksInfo dataById = Links.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.LinkName;
			this.TextBox2.Text = dataById.LinkUrl;
			this.TextBox3.Text = dataById.LinkImage;
			this.Image1.ImageUrl = dataById.LinkImage;
			this.Image1.Attributes.Add("data-original", dataById.LinkImage);
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			if (base.Action.Equals(ActionType.Add.ToString()) && !base.IsAuthorizedOp(ActionType.Add.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else if (base.Action.Equals(ActionType.Modify.ToString()) && !base.IsAuthorizedOp(ActionType.Modify.ToString()))
			{
				base.ShowMsg("Không có thẩm quyền");
			}
			else
			{
				LinksInfo linksInfo = new LinksInfo();
				if (base.IsEdit)
				{
					linksInfo = Links.GetDataById(base.OpID);
				}
				linksInfo.LinkName = WebUtils.GetString(this.TextBox1.Text);
				linksInfo.LinkUrl = WebUtils.GetString(this.TextBox2.Text);
				linksInfo.LinkImage = WebUtils.GetString(this.TextBox3.Text);
				linksInfo.LInkFlash = string.Empty;
				linksInfo.Sort = Links.MaxSort + 1;
				linksInfo.IsAudit = true;
				linksInfo.Lang = base.cultureLang;
				if (!string.IsNullOrEmpty(linksInfo.LinkImage))
				{
					linksInfo.LinkType = "图片链接";
				}
				else if (!string.IsNullOrEmpty(linksInfo.LInkFlash))
				{
					linksInfo.LinkType = "flash链接";
				}
				else
				{
					linksInfo.LinkType = "文字链接";
				}
				if (string.IsNullOrEmpty(linksInfo.LinkName) || string.IsNullOrEmpty(linksInfo.LinkUrl))
				{
					base.ShowMsg("请输入友链名称和友链地址");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						linksInfo.AutoTimeStamp = System.DateTime.Now;
						if (Links.Add(linksInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加友链[" + linksInfo.LinkName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"FriendLink.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (Links.Update(linksInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改友链[" + linksInfo.LinkName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"FriendLink.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("Thao tác thất bại");
						}
					}
				}
			}
		}
	}
}
