using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Control;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.TemplateMger
{
	public class TemplateList : H5ManagerPageBase
	{
		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected SinGooPager SinGooPager1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			int recordCount = 0;
			int num = 0;
			string strSort = " IsDefault DESC, AutoID DESC ";
			this.Repeater1.DataSource = SiteTemplate.GetPagerList(this.GetCondition(), strSort, this.SinGooPager1.PageIndex, this.SinGooPager1.PageSize, ref recordCount, ref num);
			this.Repeater1.DataBind();
			this.SinGooPager1.RecordCount = recordCount;
		}

		protected void SinGooPager1_PageIndexChanged(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private string GetCondition()
		{
			return " 1=1 ";
		}

		protected void searchbtn_Click(object sender, System.EventArgs e)
		{
			this.BindData();
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
				SiteTemplateInfo dataById = SiteTemplate.GetDataById(@int);
				if (dataById == null)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "没有找到此模板信息,模板不存在或者已删除");
				}
				else if (PageBase.defaultTemplate.AutoID == dataById.AutoID)
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "当前模板是默认模板,正在使用中,不可删除!");
				}
				else if (SiteTemplate.Delete(@int))
				{
					CacheUtils.Del("JsonLeeCMS_CacheForSiteTemplate");
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除模板[" + dataById.TemplateName + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				else
				{
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thất bại");
				}
			}
		}

		protected void btn_SetDefault_Click(object sender, System.EventArgs e)
		{
			LinkButton linkButton = (LinkButton)sender;
			int @int = WebUtils.GetInt(linkButton.CommandArgument);
			SiteTemplateInfo dataById = SiteTemplate.GetDataById(@int);
			if (dataById == null)
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "没有找到模板,模板不存在或者已被删除");
			}
			else if (!System.IO.Directory.Exists(base.Server.MapPath(dataById.TemplatePath)))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "找不到模板文件夹,请确认是否存在！");
			}
			else if (SiteTemplate.SetDefaultTemplate(dataById.AutoID))
			{
				CacheUtils.Del("JsonLeeCMS_CacheForSiteTemplate");
				this.BindData();
				PageBase.log.AddEvent(base.LoginAccount.AccountName, "设置模板[" + dataById.TemplateName + "]为当前默认使用的模板");
				base.ShowAjaxMsg(this.UpdatePanel1, "设置默认模板成功");
			}
			else
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "设置默认模板失败");
			}
		}
	}
}
