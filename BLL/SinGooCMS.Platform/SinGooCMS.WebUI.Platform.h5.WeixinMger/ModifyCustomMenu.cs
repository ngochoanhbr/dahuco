using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.WeixinMger
{
	public class ModifyCustomMenu : H5ManagerPageBase
	{
		protected DropDownList parentmenu;

		protected TextBox menuname;

		protected DropDownList menutype;

		protected TextBox TextBox1;

		protected FullImage Image1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected H5TextBox TextBox4;

		protected Button btnok;

		public string InitMenuType = "click";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindParentMenu();
			}
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void InitForModify()
		{
			WxMenuInfo dataById = WxMenu.GetDataById(base.OpID);
			this.InitMenuType = dataById.Type;
			ListItem listItem = this.parentmenu.Items.FindByValue(dataById.ParentID.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
			}
			this.parentmenu.Enabled = false;
			this.menuname.Text = dataById.Name;
			ListItem listItem2 = this.menutype.Items.FindByValue(dataById.Type);
			if (listItem2 != null)
			{
				listItem2.Selected = true;
			}
			if (dataById.Type == "click" && !string.IsNullOrEmpty(dataById.EventKey))
			{
				AutoRlyInfo eventRly = AutoRly.GetEventRly(dataById.EventKey);
				if (eventRly != null)
				{
					this.TextBox1.Text = eventRly.MsgText;
					this.TextBox2.Text = eventRly.MediaPath;
					this.Image1.ImageUrl = eventRly.MediaPath;
					this.TextBox3.Text = eventRly.Description;
					this.TextBox4.Text = eventRly.LinkUrl;
				}
			}
			else
			{
				this.TextBox4.Text = dataById.Url;
			}
		}

		private void BindParentMenu()
		{
			System.Collections.Generic.IList<WxMenuInfo> list = WxMenu.GetList(3, "ParentID=0", "Sort asc");
			if (list != null)
			{
				this.parentmenu.DataSource = list;
				this.parentmenu.DataTextField = "Name";
				this.parentmenu.DataValueField = "AutoID";
				this.parentmenu.DataBind();
			}
			this.parentmenu.Items.Insert(0, new ListItem("做为顶级菜单", "0"));
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
				WxMenuInfo wxMenuInfo = new WxMenuInfo();
				if (base.IsEdit)
				{
					wxMenuInfo = WxMenu.GetDataById(base.OpID);
				}
				wxMenuInfo.ParentID = WebUtils.GetInt(this.parentmenu.SelectedValue);
				wxMenuInfo.Name = WebUtils.GetString(this.menuname.Text);
				wxMenuInfo.Type = this.menutype.SelectedValue;
				wxMenuInfo.Url = WebUtils.GetString(this.TextBox4.Text);
				wxMenuInfo.EventKey = string.Empty;
				AutoRlyInfo autoRlyInfo = new AutoRlyInfo
				{
					RlyType = "事件回复",
					MsgKey = StringUtils.GetRandomNumber(),
					MsgText = WebUtils.GetString(this.TextBox1.Text),
					MediaPath = WebUtils.GetString(this.TextBox2.Text),
					Description = WebUtils.GetString(this.TextBox3.Text),
					LinkUrl = WebUtils.GetString(this.TextBox4.Text)
				};
				if (string.IsNullOrEmpty(wxMenuInfo.Name))
				{
					base.ShowMsg("菜单名称不能为空");
				}
				else if (wxMenuInfo.ParentID == 0 && wxMenuInfo.Name.Length > 4)
				{
					base.ShowMsg("一级菜单文字长度不超过4个汉字");
				}
				else if (wxMenuInfo.ParentID > 0 && wxMenuInfo.Name.Length > 8)
				{
					base.ShowMsg("二级菜单文字长度不超过8个汉字");
				}
				else if (wxMenuInfo.Type == "click" && string.IsNullOrEmpty(autoRlyInfo.MsgText))
				{
					base.ShowMsg("图文推送文本不能为空");
				}
				else if (wxMenuInfo.Type == "click" && (autoRlyInfo.MsgText.Length > 600 || autoRlyInfo.Description.Length > 600))
				{
					base.ShowMsg("推送图文中的文本不能超过600汉字");
				}
				else if (wxMenuInfo.Type == "view" && string.IsNullOrEmpty(autoRlyInfo.LinkUrl))
				{
					base.ShowMsg("地址跳转的地址不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						wxMenuInfo.Sort = WxMenu.MaxSort + 1;
						wxMenuInfo.AutoTimeStamp = System.DateTime.Now;
						WxStatus wxStatus = WxMenu.Add(wxMenuInfo, autoRlyInfo);
						if (wxStatus == WxStatus.增加成功)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加微信菜单[" + wxMenuInfo.Name + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg(wxStatus.ToString());
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						WxStatus wxStatus2 = WxMenu.Update(wxMenuInfo, autoRlyInfo);
						if (wxStatus2 == WxStatus.修改成功)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改微信菜单[" + wxMenuInfo.Name + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg(wxStatus2.ToString());
						}
					}
				}
			}
		}
	}
}
