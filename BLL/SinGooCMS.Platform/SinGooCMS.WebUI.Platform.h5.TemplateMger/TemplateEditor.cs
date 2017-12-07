using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.TemplateMger
{
	public class TemplateEditor : ManagerPageBase
	{
		protected TextBox txtFolderPath;

		protected TextBox txtFileName;

		protected DropDownList ddlFileType;

		protected Button btnok;

		protected Button btnapply;

		protected TextBox FileContent;

		public string strFolderPath = string.Empty;

		public string strFileName = string.Empty;

		public SiteTemplateInfo siteTemplate = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.siteTemplate = SiteTemplate.GetCacheSiteTemplateByID(base.OpID);
			this.strFolderPath = WebUtils.GetQueryString("folder");
			this.strFileName = WebUtils.GetQueryString("file");
			this.strFolderPath = this.GetTempletPath(this.strFolderPath);
			if (!System.IO.Directory.Exists(this.strFolderPath))
			{
				MessageUtils.ShowAndRedirect(this, "模板文件不存在", "TemplateList.aspx?Module=" + base.CurrentModuleCode + "&action=View");
			}
			else
			{
				this.txtFolderPath.Text = this.strFolderPath;
				string templateFilePath = this.GetTemplateFilePath();
				if (!base.IsPostBack && base.Action == ActionType.Modify.ToString())
				{
					if (base.Action == ActionType.Modify.ToString() && !System.IO.File.Exists(templateFilePath))
					{
						base.ShowMsgAndRdirect("模板文件不存在", "TemplateList.aspx?Module=" + base.CurrentModuleCode + "&action=View");
					}
					else
					{
						this.FileContent.Text = FileUtils.ReadFileContent(templateFilePath);
						this.txtFileName.Text = this.strFileName.Substring(0, this.strFileName.LastIndexOf("."));
						this.txtFileName.Enabled = false;
						ListItem listItem = this.ddlFileType.Items.FindByText(System.IO.Path.GetExtension(this.strFileName));
						if (listItem != null)
						{
							listItem.Selected = true;
						}
						this.ddlFileType.Enabled = false;
					}
				}
			}
		}

		protected void btnapply_Click(object sender, System.EventArgs e)
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
				string text = WebUtils.GetString(this.txtFileName.Text) + this.ddlFileType.SelectedItem;
				if (string.IsNullOrEmpty(WebUtils.GetString(this.txtFileName.Text)))
				{
					base.ShowMsg("请输入模板文件名");
				}
				else if (text.IndexOf(".") == -1)
				{
					base.ShowMsg("模板文件格式不正确,没有找到扩展名");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						this.strFileName = text;
						FileUtils.CreateFile(this.GetTemplateFilePath(), this.FileContent.Text);
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "创建模板文件[" + this.GetTemplateFilePath() + "] thành công");
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (this.strFileName != text)
						{
							FileUtils.ReNameFile(this.strFolderPath, this.strFileName, text, 1);
							this.strFileName = text;
						}
						FileUtils.WriteFileContent(this.GetTemplateFilePath(), this.FileContent.Text, false);
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改模板文件[" + this.GetTemplateFilePath() + "] thành công");
					}
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
				string text = WebUtils.GetString(this.txtFileName.Text) + this.ddlFileType.SelectedItem;
				if (string.IsNullOrEmpty(WebUtils.GetString(this.txtFileName.Text)))
				{
					base.ShowMsg("请输入模板文件名");
				}
				else if (text.IndexOf(".") == -1)
				{
					base.ShowMsg("模板文件格式不正确,没有找到扩展名");
				}
				else
				{
					if (base.Action.Equals(ActionType.Add.ToString()))
					{
						this.strFileName = text;
						FileUtils.CreateFile(this.GetTemplateFilePath(), this.FileContent.Text);
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "创建模板文件[" + this.GetTemplateFilePath() + "] thành công");
						MessageUtils.DialogCloseAndParentReload(this);
					}
					if (base.Action.Equals(ActionType.Modify.ToString()))
					{
						if (this.strFileName != text)
						{
							FileUtils.ReNameFile(this.strFolderPath, this.strFileName, text, 1);
							this.strFileName = text;
						}
						FileUtils.WriteFileContent(this.GetTemplateFilePath(), this.FileContent.Text, false);
						PageBase.log.AddEvent(base.LoginAccount.AccountName, "修改模板文件[" + this.GetTemplateFilePath() + "] thành công");
						MessageUtils.DialogCloseAndParentReload(this);
					}
				}
			}
		}

		private string GetTemplateFilePath()
		{
			return System.IO.Path.Combine(this.strFolderPath, this.strFileName);
		}

		public string GetTempletPath(string currentPath)
		{
			string result;
			if (currentPath.Length == 0)
			{
				if (this.siteTemplate != null)
				{
					result = HttpContext.Current.Server.MapPath(this.siteTemplate.TemplatePath);
				}
				else
				{
					result = HttpContext.Current.Server.MapPath(PageBase.defaultTemplate.TemplatePath);
				}
			}
			else
			{
				result = HttpContext.Current.Server.MapPath(System.IO.Path.Combine(this.siteTemplate.TemplatePath, currentPath));
			}
			return result;
		}
	}
}
