using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using SinGooCMS.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.WeixinMger
{
	public class CustomMenu : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected TextBox search_text;

		protected Button reloadX;

		protected Repeater Repeater1;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //if (!this.IsPostBack)
            //{
				this.BindData();
            //}
		}

		private void BindData()
		{
			this.Repeater1.DataSource = BLL.WxMenu.GetList(15, "", " RootID ASC,ParentID ASC,Sort ASC ");
			this.Repeater1.DataBind();
		}

		protected void reloadX_Click(object sender, System.EventArgs e)
		{
			this.LoadFromServer();
		}

		private void LoadFromServer()
		{
			AccessTokenResult token = CommonApi.GetToken(SinGooCMS.Weixin.Config.AppID, SinGooCMS.Weixin.Config.AppSecret, "client_credential");
			string strUrl = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", token.access_token);
			string strValue = NetWorkUtils.HttpGet(strUrl);
			GetMenuResultFull getMenuResultFull = JsonUtils.JsonToObject<GetMenuResultFull>(strValue);
			if (getMenuResultFull != null && getMenuResultFull.menu != null && getMenuResultFull.menu.button.Count > 0)
			{
				WxMenu.EmptyLocal();
				int num = 1;
				foreach (MenuFull_RootButton current in getMenuResultFull.menu.button)
				{
					WxMenuInfo wxMenuInfo = new WxMenuInfo
					{
						RootID = 0,
						ParentID = 0,
						Type = (current.type ?? string.Empty),
						Name = current.name,
						EventKey = (current.key ?? string.Empty),
						Url = (current.url ?? string.Empty),
						ChildCount = 0,
						ChildIDs = string.Empty,
						Sort = num,
						AutoTimeStamp = System.DateTime.Now
					};
					int num2 = WxMenu.Add(wxMenuInfo);
					if (num2 > 0)
					{
						wxMenuInfo.AutoID = num2;
						wxMenuInfo.RootID = num2;
						num++;
						if (current.sub_button != null && current.sub_button.Count > 0)
						{
							wxMenuInfo.ChildCount = current.sub_button.Count;
							int num3 = 1;
							foreach (MenuFull_RootButton current2 in current.sub_button)
							{
								WxMenuInfo entity = new WxMenuInfo
								{
									RootID = num2,
									ParentID = num2,
									Type = (current2.type ?? string.Empty),
									Name = current2.name,
									EventKey = (current2.key ?? string.Empty),
									Url = (current2.url ?? string.Empty),
									ChildCount = 0,
									ChildIDs = string.Empty,
									Sort = num3,
									AutoTimeStamp = System.DateTime.Now
								};
								int num4 = WxMenu.Add(entity);
								if (num4 > 0)
								{
									WxMenuInfo expr_27E = wxMenuInfo;
									expr_27E.ChildIDs = expr_27E.ChildIDs + num4 + ",";
									num3++;
								}
							}
						}
						if (wxMenuInfo.ChildIDs.EndsWith(","))
						{
							wxMenuInfo.ChildIDs = wxMenuInfo.ChildIDs.TrimEnd(new char[]
							{
								','
							});
						}
						WxMenu.Update(wxMenuInfo);
					}
				}
			}
			this.BindData();
		}

		protected void btnok_Click(object sender, System.EventArgs e)
		{
			AccessTokenResult token = CommonApi.GetToken(SinGooCMS.Weixin.Config.AppID, SinGooCMS.Weixin.Config.AppSecret, "client_credential");
			System.Collections.Generic.List<WxMenuInfo> list = WxMenu.GetList(15, "", " RootID ASC,ParentID ASC,Sort ASC ") as System.Collections.Generic.List<WxMenuInfo>;
			System.Collections.Generic.Dictionary<int, int> repeaterSortDict = base.GetRepeaterSortDict(this.Repeater1, "txtsort", "autoid");
			foreach (WxMenuInfo current in list)
			{
				current.Sort = repeaterSortDict[current.AutoID];
			}
			list = (from p in list
			orderby p.RootID
			orderby p.ParentID
			orderby p.Sort
			select p).ToList<WxMenuInfo>();
			if (WxMenu.UpdateSort(repeaterSortDict))
			{
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新微信菜单排序成功");
			}
			ButtonGroup buttonGroup = new ButtonGroup();
			foreach (WxMenuInfo level1 in list)
			{
				if (level1.ParentID.Equals(0))
				{
					if (level1.ChildCount > 0)
					{
						SubButton subButton = new SubButton
						{
							name = level1.Name
						};
						System.Collections.Generic.IEnumerable<WxMenuInfo> enumerable = from p in list
						where p.ParentID.Equals(level1.AutoID)
						select p;
						if (enumerable != null && enumerable.Count<WxMenuInfo>() > 0)
						{
							foreach (WxMenuInfo current2 in enumerable)
							{
								string type = current2.Type;
								if (type != null)
								{
									if (!(type == "click"))
									{
										if (type == "view")
										{
											subButton.sub_button.Add(new SingleViewButton
											{
												name = current2.Name,
												url = current2.Url
											});
										}
									}
									else
									{
										subButton.sub_button.Add(new SingleClickButton
										{
											name = current2.Name,
											key = current2.EventKey
										});
									}
								}
							}
						}
						buttonGroup.button.Add(subButton);
					}
					else
					{
						string type = level1.Type;
						if (type != null)
						{
							if (!(type == "click"))
							{
								if (type == "view")
								{
									buttonGroup.button.Add(new SingleViewButton
									{
										name = level1.Name,
										url = level1.Url
									});
								}
							}
							else
							{
								buttonGroup.button.Add(new SingleClickButton
								{
									name = level1.Name,
									key = level1.EventKey
								});
							}
						}
					}
				}
			}
            Senparc.Weixin.Entities.WxJsonResult wxJsonResult = Senparc.Weixin.MP.CommonAPIs.CommonApi.CreateMenu(token.access_token, buttonGroup, 10000);
			if (wxJsonResult.errcode == ReturnCode.请求成功)
			{
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "更新微信菜单成功");
				base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				this.BindData();
			}
			else
			{
				PageBase.log.AddErrLog("更新微信菜单出错了", wxJsonResult.errmsg);
				base.ShowAjaxMsg(this.UpdatePanel1, wxJsonResult.errmsg);
			}
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				int @int = WebUtils.GetInt((sender as LinkButton).CommandArgument);
				WxMenuInfo dataById = WxMenu.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Những thông tin này không được tìm thấy, các dữ liệu không tồn tại hoặc đã bị xóa");
				}
				else if (dataById.ChildCount > 0)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "包含子菜单，请先删除子菜单");
				}
				else if (WxMenu.Del(@int))
				{
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除微信菜单[" + dataById.Name + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}
	}
}
