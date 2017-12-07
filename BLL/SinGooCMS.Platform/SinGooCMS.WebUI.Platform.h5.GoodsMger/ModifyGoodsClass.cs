using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyGoodsClass : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected DropDownList DropDownList2;

		protected Repeater Repeater1;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindCateParent();
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			GoodsClassInfo dataById = SinGooCMS.BLL.GoodsClass.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.ClassName;
			ListItem listItem = this.DropDownList2.Items.FindByValue(dataById.ParentID.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
			}
			this.Repeater1.DataSource = dataById.GeiGeSets;
			this.Repeater1.DataBind();
		}

		private void BindCateParent()
		{
			System.Collections.Generic.List<GoodsClassInfo> obj = (System.Collections.Generic.List<GoodsClassInfo>)SinGooCMS.BLL.GoodsClass.GetAllList();
			System.Collections.Generic.List<GoodsClassInfo> list = JObject.DeepClone<System.Collections.Generic.List<GoodsClassInfo>>(obj);
			this.DropDownList2.DataSource = SinGooCMS.BLL.GoodsClass.GetRelationList(list, 0);
			this.DropDownList2.DataTextField = "ClassName";
			this.DropDownList2.DataValueField = "AutoID";
			this.DropDownList2.DataBind();
			this.DropDownList2.Items.Insert(0, new ListItem("根分类", "0"));
			if (base.Action.Equals(ActionType.Add.ToString()))
			{
				if (base.OpID > 0)
				{
					ListItem listItem = this.DropDownList2.Items.FindByValue(base.OpID.ToString());
					if (listItem != null)
					{
						listItem.Selected = true;
					}
					this.DropDownList2.Enabled = false;
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
				int num = WebUtils.StringToInt(this.DropDownList2.SelectedValue);
				GoodsClassInfo goodsClassInfo = new GoodsClassInfo();
				if (base.IsEdit)
				{
					goodsClassInfo = SinGooCMS.BLL.GoodsClass.GetDataById(base.OpID);
				}
				goodsClassInfo.ClassName = WebUtils.GetString(this.TextBox1.Text);
				goodsClassInfo.GuiGeCollection = WebUtils.GetFormString("hdf_guigejson");
				if (string.IsNullOrEmpty(goodsClassInfo.ClassName))
				{
					base.ShowMsg("商品类目名称不能为空！");
				}
				else if (string.IsNullOrEmpty(goodsClassInfo.GuiGeCollection))
				{
					base.ShowMsg("没有输入任何规格属性！");
				}
				else if (base.IsEdit && goodsClassInfo.AutoID == num)
				{
					base.ShowMsg("上级文件夹不能是自己！");
				}
				else
				{
					goodsClassInfo.ParentID = num;
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						goodsClassInfo.Sort = SinGooCMS.BLL.GoodsClass.MaxSort + 1;
						goodsClassInfo.Lang = base.cultureLang;
						goodsClassInfo.Creator = base.LoginAccount.AccountName;
						goodsClassInfo.AutoTimeStamp = System.DateTime.Now;
						if (SinGooCMS.BLL.GoodsClass.AddExt(goodsClassInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加商品类目[" + goodsClassInfo.ClassName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"GoodsClass.aspx?CatalogID=",
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
						if (SinGooCMS.BLL.GoodsClass.Update(goodsClassInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改商品类目[" + goodsClassInfo.ClassName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"GoodsClass.aspx?CatalogID=",
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
