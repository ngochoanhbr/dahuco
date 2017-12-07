using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class ModifyCatalog : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

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
			CatalogInfo dataById = Catalog.GetDataById(base.OpID);
			this.TextBox1.Text = dataById.CatalogName;
			this.TextBox2.Text = dataById.CatalogCode;
			this.TextBox2.Enabled = false;
			this.TextBox3.Text = dataById.Remark;
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
				CatalogInfo catalogInfo = new CatalogInfo();
				if (base.IsEdit)
				{
					catalogInfo = Catalog.GetCacheCatalogById(base.OpID);
				}
				catalogInfo.CatalogName = WebUtils.GetString(this.TextBox1.Text);
				catalogInfo.Remark = WebUtils.GetString(this.TextBox3.Text);
				if (!base.IsEdit)
				{
					catalogInfo.CatalogCode = WebUtils.GetString(this.TextBox2.Text);
					CatalogInfo cacheCatalogByCode = Catalog.GetCacheCatalogByCode(catalogInfo.CatalogCode);
					if (cacheCatalogByCode != null)
					{
						base.ShowMsg("栏目代码已存在");
						return;
					}
				}
				if (string.IsNullOrEmpty(catalogInfo.CatalogName) || string.IsNullOrEmpty(catalogInfo.CatalogCode))
				{
					base.ShowMsg("栏目名称和栏目代码不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						catalogInfo.AutoTimeStamp = System.DateTime.Now;
						catalogInfo.IsSystem = false;
						catalogInfo.Sort = Catalog.MaxSort + 1;
						if (Catalog.Add(catalogInfo) > 0)
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加栏目[" + catalogInfo.CatalogName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("添加栏目失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (Catalog.Update(catalogInfo))
						{
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改栏目[" + catalogInfo.CatalogName + "] thành công");
							MessageUtils.DialogCloseAndParentReload(this);
						}
						else
						{
							base.ShowMsg("修改栏目失败");
						}
					}
				}
			}
		}
	}
}
