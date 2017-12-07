using CKEditor.NET;
using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.NodeMger
{
	public class NodeModify : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected DropDownList DropDownList3;

		protected DropDownList DropDownList4;

		protected FullImage Image1;

		protected TextBox TextBox3;

		protected FullImage Image2;

		protected TextBox TextBox5;

		protected TextBox TextBox6;

		protected TextBox TextBox7;

		protected CKEditorControl NodeDesc;

		protected H5TextBox TextBox9;

		protected CheckBox CheckBox10;

		protected CheckBox CheckBox11;

		protected CheckBox CheckBox12;

		protected CheckBox CheckBox13;

		protected TextBox TextBox14;

		protected CheckBox CheckBox15;

		protected CheckBox CheckBox16;

		protected CheckBox CheckBox17;

		protected CheckBoxList CheckBoxList18;

		protected CheckBoxList CheckBoxList19;

		protected TextBox TextBox20;

		protected TextBox TextBox21;

		protected TextBox TextBox22;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindNodeParent();
				this.BindModel();
				this.BindUserGroup();
				this.BindUserLevel();
				this.SetDefaultTemplate();
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void SetDefaultTemplate()
		{
			NodeInfo cacheNodeById = SinGooCMS.BLL.Node.GetCacheNodeById(base.OpID);
			if (!base.IsEdit && cacheNodeById != null)
			{
				this.TextBox20.Text = cacheNodeById.NodeSetting.TemplateOfNodeIndex;
				this.TextBox21.Text = cacheNodeById.NodeSetting.TemplateOfNodeList;
				this.TextBox22.Text = cacheNodeById.NodeSetting.TemplateOfNodeContent;
			}
		}

		private void InitForModify()
		{
			this.DropDownList3.Enabled = false;
			NodeInfo cacheNodeById = SinGooCMS.BLL.Node.GetCacheNodeById(base.OpID);
			if (cacheNodeById != null)
			{
				this.TextBox1.Text = cacheNodeById.NodeName;
				this.TextBox2.Text = cacheNodeById.UrlRewriteName;
				ListItem listItem = this.DropDownList3.Items.FindByValue(cacheNodeById.ParentID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				ListItem listItem2 = this.DropDownList4.Items.FindByValue(cacheNodeById.ModelID.ToString());
				if (listItem2 != null)
				{
					listItem2.Selected = true;
				}
				this.TextBox3.Text = cacheNodeById.NodeBanner;
				this.Image1.ImageUrl = cacheNodeById.NodeBanner;
				this.TextBox5.Text = cacheNodeById.NodeImage;
				this.Image2.ImageUrl = cacheNodeById.NodeImage;
				this.TextBox6.Text = cacheNodeById.SeoKey;
				this.TextBox7.Text = cacheNodeById.SeoDescription;
				this.NodeDesc.Text = cacheNodeById.Remark;
				this.TextBox9.Text = cacheNodeById.ItemPageSize.ToString();
				this.CheckBox10.Checked = cacheNodeById.IsShowOnMenu;
				this.CheckBox11.Checked = cacheNodeById.IsShowOnNav;
				this.CheckBox12.Checked = cacheNodeById.IsTop;
				this.CheckBox13.Checked = cacheNodeById.IsRecommend;
				this.TextBox14.Text = cacheNodeById.CustomLink;
				this.CheckBox15.Checked = cacheNodeById.NodeSetting.EnableAddInParent;
				this.CheckBox16.Checked = cacheNodeById.NodeSetting.AllowComment;
				this.CheckBox17.Checked = cacheNodeById.NodeSetting.NeedLogin;
				string[] array = cacheNodeById.NodeSetting.EnableViewUGroups.Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string b = array2[i];
						foreach (ListItem listItem3 in this.CheckBoxList18.Items)
						{
							if (listItem3.Value == b)
							{
								listItem3.Selected = true;
							}
						}
					}
				}
				string[] array3 = cacheNodeById.NodeSetting.EnableViewULevel.Split(new char[]
				{
					','
				});
				if (array3.Length > 0)
				{
					string[] array2 = array3;
					for (int i = 0; i < array2.Length; i++)
					{
						string b = array2[i];
						foreach (ListItem listItem3 in this.CheckBoxList19.Items)
						{
							if (listItem3.Value == b)
							{
								listItem3.Selected = true;
							}
						}
					}
				}
				this.TextBox20.Text = cacheNodeById.NodeSetting.TemplateOfNodeIndex;
				this.TextBox21.Text = cacheNodeById.NodeSetting.TemplateOfNodeList;
				this.TextBox22.Text = cacheNodeById.NodeSetting.TemplateOfNodeContent;
			}
		}

		private void BindNodeParent()
		{
			System.Collections.Generic.List<NodeInfo> obj = (System.Collections.Generic.List<NodeInfo>)SinGooCMS.BLL.Node.GetCacheAllNodes();
			System.Collections.Generic.List<NodeInfo> list = JObject.DeepClone<System.Collections.Generic.List<NodeInfo>>(obj);
			this.DropDownList3.DataSource = SinGooCMS.BLL.Node.GetRelationNodeList(list, 0);
			this.DropDownList3.DataTextField = "NodeName";
			this.DropDownList3.DataValueField = "AutoID";
			this.DropDownList3.DataBind();
			this.DropDownList3.Items.Insert(0, new ListItem("根栏目", "0"));
			if (base.Action.Equals(ActionType.Add.ToString()) && base.OpID > 0)
			{
				ListItem listItem = this.DropDownList3.Items.FindByValue(base.OpID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
					this.DropDownList3.Enabled = false;
				}
			}
		}

		private void BindModel()
		{
			this.DropDownList4.DataSource = ContModel.GetCacheUsingModelList();
			this.DropDownList4.DataTextField = "ModelName";
			this.DropDownList4.DataValueField = "AutoID";
			this.DropDownList4.DataBind();
		}

		private void BindUserGroup()
		{
			this.CheckBoxList18.DataSource = UserGroup.GetCacheUserGroupList();
			this.CheckBoxList18.DataTextField = "GroupName";
			this.CheckBoxList18.DataValueField = "AutoID";
			this.CheckBoxList18.DataBind();
		}

		private void BindUserLevel()
		{
			this.CheckBoxList19.DataSource = UserLevel.GetCacheUserLevelList();
			this.CheckBoxList19.DataTextField = "LevelName";
			this.CheckBoxList19.DataValueField = "AutoID";
			this.CheckBoxList19.DataBind();
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
				ContentPublish contentPublish = new ContentPublish();
				NodeInfo nodeInfo = new NodeInfo();
				if (base.IsEdit)
				{
					nodeInfo = SinGooCMS.BLL.Node.GetCacheNodeById(base.OpID);
				}
				nodeInfo.NodeName = WebUtils.GetString(this.TextBox1.Text);
				nodeInfo.UrlRewriteName = WebUtils.GetString(this.TextBox2.Text);
				nodeInfo.ParentID = WebUtils.GetInt(this.DropDownList3.SelectedValue);
				nodeInfo.ModelID = WebUtils.GetInt(this.DropDownList4.SelectedValue);
				nodeInfo.NodeBanner = WebUtils.GetString(this.TextBox3.Text);
				nodeInfo.NodeImage = WebUtils.GetString(this.TextBox5.Text);
				nodeInfo.SeoKey = WebUtils.GetString(this.TextBox6.Text);
				nodeInfo.SeoDescription = WebUtils.GetString(this.TextBox7.Text);
				nodeInfo.Remark = this.NodeDesc.Text;
				nodeInfo.ItemPageSize = WebUtils.GetInt(this.TextBox9.Text);
				nodeInfo.IsShowOnMenu = this.CheckBox10.Checked;
				nodeInfo.IsShowOnNav = this.CheckBox11.Checked;
				nodeInfo.IsTop = this.CheckBox12.Checked;
				nodeInfo.IsRecommend = this.CheckBox13.Checked;
				nodeInfo.CustomLink = WebUtils.GetString(this.TextBox14.Text);
				nodeInfo.Lang = base.cultureLang;
				nodeInfo.AutoTimeStamp = System.DateTime.Now;
				nodeInfo.NodeSetting.EnableAddInParent = this.CheckBox15.Checked;
				nodeInfo.NodeSetting.AllowComment = this.CheckBox16.Checked;
				nodeInfo.NodeSetting.NeedLogin = this.CheckBox17.Checked;
				string text = string.Empty;
				foreach (ListItem listItem in this.CheckBoxList18.Items)
				{
					if (listItem.Selected)
					{
						text = text + listItem.Value + ",";
					}
				}
				if (text.EndsWith(","))
				{
					text = text.Substring(0, text.Length - 1);
				}
				nodeInfo.NodeSetting.EnableViewUGroups = text;
				string text2 = string.Empty;
				foreach (ListItem listItem2 in this.CheckBoxList19.Items)
				{
					if (listItem2.Selected)
					{
						text2 = text2 + listItem2.Value + ",";
					}
				}
				if (text2.EndsWith(","))
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				nodeInfo.NodeSetting.EnableViewULevel = text2;
				nodeInfo.NodeSetting.TemplateOfNodeIndex = WebUtils.GetString(this.TextBox20.Text);
				nodeInfo.NodeSetting.TemplateOfNodeList = WebUtils.GetString(this.TextBox21.Text);
				nodeInfo.NodeSetting.TemplateOfNodeContent = WebUtils.GetString(this.TextBox22.Text);
				if (string.IsNullOrEmpty(nodeInfo.NodeName))
				{
					base.ShowMsg("栏目名称不能为空");
				}
				if (string.IsNullOrEmpty(nodeInfo.UrlRewriteName))
				{
					base.ShowMsg("栏目标识不能为空");
				}
				else if (nodeInfo.NodeName.IndexOf("&") > 0 || nodeInfo.UrlRewriteName.IndexOf("&") > 0)
				{
					base.ShowMsg("存在非法的字符&");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						NodeAddStatus nodeAddStatus = SinGooCMS.BLL.Node.Add(nodeInfo);
						NodeAddStatus nodeAddStatus2 = nodeAddStatus;
						switch (nodeAddStatus2)
						{
						case NodeAddStatus.ToMoreNode:
							base.ShowMsg("主栏目数量不能超过[" + base.ver.NodeLimit + "]个");
							break;
						case NodeAddStatus.Error:
							base.ShowMsg("添加栏目失败");
							break;
						case NodeAddStatus.ExistsNodeName:
							base.ShowMsg("已存在相同的栏目名称");
							break;
						case NodeAddStatus.ExistsNodeIdentifier:
							base.ShowMsg("已存在相同的栏目标识");
							break;
						case NodeAddStatus.ExistsNodeDir:
							break;
						case NodeAddStatus.ParentNodeNotExists:
							base.ShowMsg("上级栏目不存在");
							break;
						default:
							if (nodeAddStatus2 == NodeAddStatus.Success)
							{
								if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
								{
									contentPublish.CreateIndex();
									contentPublish.CreateNode(nodeInfo.AutoID);
								}
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加栏目[" + nodeInfo.NodeName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Index.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&action=View"
								}));
							}
							break;
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						NodeUpdateStatus nodeUpdateStatus = SinGooCMS.BLL.Node.Update(nodeInfo);
						NodeUpdateStatus nodeUpdateStatus2 = nodeUpdateStatus;
						switch (nodeUpdateStatus2)
						{
						case NodeUpdateStatus.ToMoreNode:
							base.ShowMsg("主栏目数量不能超过[" + base.ver.NodeLimit + "]个");
							break;
						case NodeUpdateStatus.Error:
							base.ShowMsg("修改栏目失败");
							break;
						case NodeUpdateStatus.ExistsNodeName:
							base.ShowMsg("已存在相同的栏目名称");
							break;
						case NodeUpdateStatus.ExistsNodeIdentifier:
							base.ShowMsg("已存在相同的栏目标识");
							break;
						default:
							if (nodeUpdateStatus2 == NodeUpdateStatus.Success)
							{
								if (PageBase.config.BrowseType.Equals(BrowseType.Html.ToString()))
								{
									contentPublish.CreateIndex();
									contentPublish.CreateNode(nodeInfo.AutoID);
								}
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改栏目[" + nodeInfo.NodeName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Index.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&action=View"
								}));
							}
							break;
						}
					}
				}
			}
		}
	}
}
