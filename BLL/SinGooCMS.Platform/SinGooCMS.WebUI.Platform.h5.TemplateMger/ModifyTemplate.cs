using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.TemplateMger
{
	public class ModifyTemplate : H5ManagerPageBase
	{
		protected TextBox TextBox1;

		protected TextBox TextBox2;

		protected TextBox TextBox3;

		protected Image Image1;

		protected TextBox previmg;

		protected TextBox TextBox6;

		protected TextBox TextBox4;

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
			SiteTemplateInfo dataById = SiteTemplate.GetDataById(base.OpID);
			if (dataById != null)
			{
				this.TextBox1.Text = dataById.TemplateName;
				this.TextBox2.Text = dataById.TemplatePath;
				this.TextBox3.Text = dataById.HomePage;
				this.TextBox4.Text = dataById.TemplateDesc;
				this.TextBox6.Text = dataById.Author;
				if (!string.IsNullOrEmpty(dataById.ShowPic))
				{
					this.Image1.ImageUrl = dataById.ShowPic;
					this.previmg.Text = dataById.ShowPic;
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
				SiteTemplateInfo siteTemplateInfo = new SiteTemplateInfo();
				if (base.IsEdit)
				{
					siteTemplateInfo = SiteTemplate.GetDataById(base.OpID);
				}
				siteTemplateInfo.TemplateName = WebUtils.GetString(this.TextBox1.Text);
				siteTemplateInfo.TemplatePath = WebUtils.GetString(this.TextBox2.Text);
				siteTemplateInfo.ShowPic = WebUtils.GetString(this.previmg.Text);
				siteTemplateInfo.HomePage = WebUtils.GetString(this.TextBox3.Text);
				siteTemplateInfo.TemplateDesc = WebUtils.GetString(this.TextBox4.Text);
				siteTemplateInfo.IsAudit = true;
				siteTemplateInfo.Author = WebUtils.GetString(this.TextBox6.Text);
				siteTemplateInfo.CopyRight = string.Empty;
				if (string.IsNullOrEmpty(siteTemplateInfo.TemplateName))
				{
					base.ShowMsg("模板名称不能为空");
				}
				else if (string.IsNullOrEmpty(siteTemplateInfo.TemplatePath))
				{
					base.ShowMsg("模板路径不能为空");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						siteTemplateInfo.AutoTimeStamp = System.DateTime.Now;
						if (SiteTemplate.Add(siteTemplateInfo) > 0)
						{
							CacheUtils.Del("JsonLeeCMS_CacheForSiteTemplate");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "添加模板[" + siteTemplateInfo.TemplateName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"TemplateList.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("添加模板失败");
						}
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (SiteTemplate.Update(siteTemplateInfo))
						{
							CacheUtils.Del("JsonLeeCMS_CacheForSiteTemplate");
							PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改模板[" + siteTemplateInfo.TemplateName + "] thành công");
							base.Response.Redirect(string.Concat(new object[]
							{
								"TemplateList.aspx?CatalogID=",
								base.CurrentCatalogID,
								"&Module=",
								base.CurrentModuleCode,
								"&action=View"
							}));
						}
						else
						{
							base.ShowMsg("修改模板失败");
						}
					}
				}
			}
		}
	}
}
