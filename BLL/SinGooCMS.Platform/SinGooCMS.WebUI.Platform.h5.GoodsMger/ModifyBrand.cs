using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.GoodsMger
{
	public class ModifyBrand : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected DropDownList DropDownList4;

		protected H5TextBox TextBox3;

		protected TextBox TextBox5;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindIndustry();
				if (base.IsEdit)
				{
					this.InitForModify();
				}
			}
		}

		private void InitForModify()
		{
			BrandInfo dataById = Brand.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.BrandName;
			this.TextBox3.Text = dataById.OfficialSite;
			ListItem listItem = this.DropDownList4.Items.FindByValue(dataById.IndName);
			if (listItem != null)
			{
				listItem.Selected = true;
			}
			this.TextBox5.Text = dataById.BrandDesc;
		}

		private void BindIndustry()
		{
			this.DropDownList4.DataSource = Industry.GetList();
			this.DropDownList4.DataTextField = "IndName";
			this.DropDownList4.DataValueField = "IndName";
			this.DropDownList4.DataBind();
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
				BrandInfo brandInfo = new BrandInfo();
				if (base.IsEdit)
				{
					brandInfo = Brand.GetDataById(base.OpID);
				}
				brandInfo.BrandName = WebUtils.GetString(this.TextBox1.Text);
				brandInfo.CompanyName = string.Empty;
				brandInfo.OfficialSite = WebUtils.GetString(this.TextBox3.Text);
				brandInfo.IndName = WebUtils.GetString(this.DropDownList4.SelectedValue);
				brandInfo.BrandDesc = WebUtils.GetString(this.TextBox5.Text);
				brandInfo.Sort = Brand.MaxSort + 1;
				brandInfo.IsRecommend = false;
				brandInfo.AutoTimeStamp = System.DateTime.Now;
				if (string.IsNullOrEmpty(brandInfo.BrandName))
				{
					base.ShowMsg("品牌名称不为空!");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						if (Brand.Add(brandInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加品牌[" + brandInfo.BrandDesc + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加品牌失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (Brand.Update(brandInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改品牌[" + brandInfo.BrandName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改品牌失败");
						}
					}
				}
			}
		}
	}
}
