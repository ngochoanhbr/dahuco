using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class ModifyModule : H5ManagerPageBase
	{
		protected DropDownList DropDownList5;

		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected TextBox TextBox4;

		protected HtmlInputCheckBox isdefault;

		protected Button btnok;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindCatalog();
			}
			if (base.IsEdit && !base.IsPostBack)
			{
				this.InitForModify();
			}
		}

		private void BindCatalog()
		{
			System.Collections.Generic.IList<CatalogInfo> cacheCatalogList = Catalog.GetCacheCatalogList();
			this.DropDownList5.DataSource = cacheCatalogList;
			this.DropDownList5.DataTextField = "CatalogName";
			this.DropDownList5.DataValueField = "AutoID";
			this.DropDownList5.DataBind();
		}

		private void InitForModify()
		{
			ModuleInfo dataById = SinGooCMS.BLL.Module.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.ModuleName;
			this.TextBox2.Text = dataById.ModuleCode;
			this.TextBox2.Enabled = false;
			this.TextBox3.Text = dataById.FilePath;
			this.TextBox4.Text = dataById.Remark;
			this.isdefault.Checked = dataById.IsDefault;
			ListItem listItem = this.DropDownList5.Items.FindByValue(dataById.CatalogID.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
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
				ModuleInfo moduleInfo = new ModuleInfo();
				if (base.IsEdit)
				{
					moduleInfo = SinGooCMS.BLL.Module.GetDataById(base.OpID);
				}
				moduleInfo.CatalogID = WebUtils.StringToInt(this.DropDownList5.SelectedValue, 0);
				moduleInfo.ModuleName = WebUtils.GetString(this.TextBox1.Text);
				moduleInfo.FilePath = WebUtils.GetString(this.TextBox3.Text);
				moduleInfo.Remark = WebUtils.GetString(this.TextBox4.Text);
				moduleInfo.IsDefault = this.isdefault.Checked;
				if (!base.IsEdit)
				{
					moduleInfo.ModuleCode = WebUtils.GetString(this.TextBox2.Text);
					ModuleInfo moduleByCode = SinGooCMS.BLL.Module.GetModuleByCode(moduleInfo.ModuleCode);
					if (moduleByCode != null)
					{
						base.ShowMsg("模块代码已存在");
						return;
					}
				}
				if (string.IsNullOrEmpty(moduleInfo.ModuleName) || string.IsNullOrEmpty(moduleInfo.FilePath))
				{
					base.ShowMsg("模块名称和模块路径不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						moduleInfo.AutoTimeStamp = System.DateTime.Now;
						moduleInfo.IsSystem = false;
						moduleInfo.Sort = SinGooCMS.BLL.Module.MaxSort + 1;
						int num = SinGooCMS.BLL.Module.Add(moduleInfo);
						if (num > 0)
						{
							if (moduleInfo.IsDefault)
							{
								PageBase.dbo.UpdateTable(string.Concat(new object[]
								{
									"update sys_Module set IsDefault=0 where CatalogID=",
									moduleInfo.CatalogID,
									" and AutoID<>",
									num
								}));
							}
							CacheUtils.Del("JsonLeeCMS_CacheForGetAccountMenuDT");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加模块[" + moduleInfo.ModuleName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("增加模块失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SinGooCMS.BLL.Module.Update(moduleInfo))
						{
							if (moduleInfo.IsDefault)
							{
								PageBase.dbo.UpdateTable(string.Concat(new object[]
								{
									"update sys_Module set IsDefault=0 where CatalogID=",
									moduleInfo.CatalogID,
									" and AutoID<>",
									moduleInfo.AutoID
								}));
							}
							CacheUtils.Del("JsonLeeCMS_CacheForGetAccountMenuDT");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改模块[" + moduleInfo.ModuleName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改模块失败");
						}
					}
				}
			}
		}
	}
}
