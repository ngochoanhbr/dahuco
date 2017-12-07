using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyCategory : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected DropDownList DropDownList3;

		protected TextBox TextBox2;

		protected DropDownList DropDownList4;

		protected Image Image1;

		protected TextBox TextBox5;

		protected TextBox TextBox6;

		protected TextBox TextBox7;

		protected TextBox TextBox8;

		protected H5TextBox TextBox9;

		protected CheckBox CheckBox12;

		protected CheckBox CheckBox13;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindCateParent();
				this.BindModel();
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			CategoryInfo cacheCategoryByID = SinGooCMS.BLL.Category.GetCacheCategoryByID(base.OpID);
			if (cacheCategoryByID != null)
			{
				this.TextBox1.Text = cacheCategoryByID.CategoryName;
				this.TextBox2.Text = cacheCategoryByID.UrlRewriteName;
				this.DropDownList3.Enabled = false;
				ListItem listItem = this.DropDownList3.Items.FindByValue(cacheCategoryByID.ParentID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
				ListItem listItem2 = this.DropDownList4.Items.FindByValue(cacheCategoryByID.ModelID.ToString());
				if (listItem2 != null)
				{
					listItem2.Selected = true;
				}
				this.TextBox5.Text = cacheCategoryByID.CategoryImage;
				this.Image1.ImageUrl = cacheCategoryByID.CategoryImage;
				this.TextBox6.Text = cacheCategoryByID.SeoKey;
				this.TextBox7.Text = cacheCategoryByID.SeoDescription;
				this.TextBox8.Text = cacheCategoryByID.Remark;
				this.TextBox9.Text = cacheCategoryByID.ItemPageSize.ToString();
				this.CheckBox12.Checked = cacheCategoryByID.IsTop;
				this.CheckBox13.Checked = cacheCategoryByID.IsRecommend;
			}
		}

		private void BindCateParent()
		{
			System.Collections.Generic.List<CategoryInfo> obj = (System.Collections.Generic.List<CategoryInfo>)SinGooCMS.BLL.Category.GetCacheCategoryList();
			System.Collections.Generic.List<CategoryInfo> list = JObject.DeepClone<System.Collections.Generic.List<CategoryInfo>>(obj);
			this.DropDownList3.DataSource = this.GetRelationNodeList(list, 0);
			this.DropDownList3.DataTextField = "CategoryName";
			this.DropDownList3.DataValueField = "AutoID";
			this.DropDownList3.DataBind();
			this.DropDownList3.Items.Insert(0, new ListItem("根分类", "0"));
			if (base.Action.Equals(ActionType.Add.ToString()))
			{
				if (base.OpID > 0)
				{
					ListItem listItem = this.DropDownList3.Items.FindByValue(base.OpID.ToString());
					if (listItem != null)
					{
						listItem.Selected = true;
					}
					this.DropDownList3.Enabled = false;
				}
			}
		}

		public System.Collections.Generic.List<CategoryInfo> GetRelationNodeList(System.Collections.Generic.List<CategoryInfo> list, int intParentID)
		{
			System.Collections.Generic.List<CategoryInfo> list2 = list.FindAll((CategoryInfo parameterA) => parameterA.ParentID == intParentID);
			System.Collections.Generic.List<CategoryInfo> list3 = new System.Collections.Generic.List<CategoryInfo>();
			int num = 0;
			foreach (CategoryInfo current in list2)
			{
				if (num == list2.Count - 1)
				{
					current.CategoryName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, true)) + StringUtils.GetChineseSpell(current.CategoryName).Substring(0, 1) + current.CategoryName;
				}
				else
				{
					current.CategoryName = ((current.ParentID == 0) ? "" : StringUtils.GetCatePrefix(current.Depth - 1, false)) + StringUtils.GetChineseSpell(current.CategoryName).Substring(0, 1) + current.CategoryName;
				}
				list3.Add(current);
				if (current.ChildCount > 0)
				{
					list3.AddRange(this.GetRelationNodeList(list, current.AutoID));
				}
				num++;
			}
			return list3;
		}

		private void BindModel()
		{
			this.DropDownList4.DataSource = ProductModel.GetCacheModelList();
			this.DropDownList4.DataTextField = "ModelName";
			this.DropDownList4.DataValueField = "AutoID";
			this.DropDownList4.DataBind();
			if (base.Action.Equals(ActionType.Add.ToString()) && base.OpID > 0)
			{
				CategoryInfo cacheCategoryByID = SinGooCMS.BLL.Category.GetCacheCategoryByID(base.OpID);
				CategoryInfo cacheCategoryByID2 = SinGooCMS.BLL.Category.GetCacheCategoryByID(cacheCategoryByID.RootID);
				ListItem listItem = this.DropDownList4.Items.FindByValue(cacheCategoryByID2.ModelID.ToString());
				if (listItem != null)
				{
					listItem.Selected = true;
				}
			}
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
				CategoryInfo categoryInfo = new CategoryInfo();
				if (base.IsEdit)
				{
					categoryInfo = SinGooCMS.BLL.Category.GetCacheCategoryByID(base.OpID);
				}
				categoryInfo.CategoryName = WebUtils.GetString(this.TextBox1.Text);
				categoryInfo.ParentID = WebUtils.StringToInt(this.DropDownList3.SelectedValue);
				categoryInfo.ModelID = WebUtils.StringToInt(this.DropDownList4.SelectedValue);
				categoryInfo.CategoryImage = WebUtils.GetString(this.TextBox5.Text);
				categoryInfo.SeoKey = WebUtils.GetString(this.TextBox6.Text);
				categoryInfo.SeoDescription = WebUtils.GetString(this.TextBox7.Text);
				categoryInfo.Remark = WebUtils.GetString(this.TextBox8.Text);
				categoryInfo.ItemPageSize = WebUtils.StringToInt(this.TextBox9.Text);
				categoryInfo.IsTop = this.CheckBox12.Checked;
				categoryInfo.IsRecommend = this.CheckBox13.Checked;
				categoryInfo.Lang = base.cultureLang;
				categoryInfo.UrlRewriteName = WebUtils.GetString(this.TextBox2.Text);
				if (string.IsNullOrEmpty(categoryInfo.CategoryName))
				{
					base.ShowMsg("分类名称不能为空");
				}
				else if (string.IsNullOrEmpty(categoryInfo.UrlRewriteName))
				{
					base.ShowMsg("分类标识不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						categoryInfo.AutoTimeStamp = System.DateTime.Now;
						NodeAddStatus nodeAddStatus = SinGooCMS.BLL.Category.Add(categoryInfo);
						NodeAddStatus nodeAddStatus2 = nodeAddStatus;
						switch (nodeAddStatus2)
						{
						case NodeAddStatus.Error:
							base.ShowMsg("添加分类失败");
							goto IL_319;
						case NodeAddStatus.ExistsNodeName:
							base.ShowMsg("已存在相同的分类名称");
							goto IL_319;
						case NodeAddStatus.ExistsNodeIdentifier:
							base.ShowMsg("已存在相同的分类标识");
							goto IL_319;
						case NodeAddStatus.ExistsNodeDir:
							break;
						case NodeAddStatus.ParentNodeNotExists:
							base.ShowMsg("上级分类不存在");
							goto IL_319;
						default:
							if (nodeAddStatus2 == NodeAddStatus.Success)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加产品分类[" + categoryInfo.CategoryName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Category.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&action=View"
								}));
								goto IL_319;
							}
							break;
						}
						base.ShowMsg("Lỗi Unknown");
						IL_319:;
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						NodeUpdateStatus nodeUpdateStatus = SinGooCMS.BLL.Category.Update(categoryInfo);
						NodeUpdateStatus nodeUpdateStatus2 = nodeUpdateStatus;
						switch (nodeUpdateStatus2)
						{
						case NodeUpdateStatus.Error:
							base.ShowMsg("修改分类失败");
							goto IL_43B;
						case NodeUpdateStatus.ExistsNodeName:
							base.ShowMsg("已存在相同的分类名称");
							goto IL_43B;
						case NodeUpdateStatus.ExistsNodeIdentifier:
							base.ShowMsg("已存在相同的分类标识");
							goto IL_43B;
						case NodeUpdateStatus.ExistsNodeDir:
							break;
						case NodeUpdateStatus.UnNodeSelf:
							base.ShowMsg("不能做为自己的子分类");
							goto IL_43B;
						default:
							if (nodeUpdateStatus2 == NodeUpdateStatus.Success)
							{
								PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改产品分类[" + categoryInfo.CategoryName + "] thành công");
								base.Response.Redirect(string.Concat(new object[]
								{
									"Category.aspx?CatalogID=",
									base.CurrentCatalogID,
									"&Module=",
									base.CurrentModuleCode,
									"&action=View"
								}));
								goto IL_43B;
							}
							break;
						}
						base.ShowMsg("Lỗi Unknown");
						IL_43B:;
					}
				}
			}
		}
	}
}
