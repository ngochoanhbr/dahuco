using Ionic.Zip;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.SysMger
{
	public class BackAndRestore : H5ManagerPageBase
	{
		private string server;

		private string uid;

		private string pwd;

		private string database;

		private string conn;

		protected Button btn_CreateDBBack;

		protected Button btn_CreateSiteBack;

		protected Button btn_CreateTempateBack;

		protected Button btn_CreateUploadBack;

		protected UpdatePanel UpdatePanel1;

		protected Repeater Repeater1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.SetConnectParameter();
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}

		protected void btn_CreateDBBack_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Back"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				try
				{
					string text = base.Server.MapPath(base.BakFolder + StringUtils.GetNewFileName() + ".bak");
					string strSQL = string.Concat(new string[]
					{
						" Backup Database ",
						this.database,
						" To disk='",
						text,
						"'"
					});
					PageBase.dbo.ExecSQL(strSQL);
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "创建数据库备份文件[" + text + "] thành công");
					this.BindData();
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				catch (System.Exception ex)
				{
					throw ex;
				}
			}
		}

		protected void btn_CreateSiteBack_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Back"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				try
				{
					string text = base.BakFolder + StringUtils.GetNewFileName() + "_site.zip";
					using (ZipFile zipFile = new ZipFile(System.Text.Encoding.UTF8))
					{
						zipFile.AddDirectory(base.Request.ServerVariables["APPL_PHYSICAL_PATH"]);
						zipFile.Save(base.Server.MapPath(text));
					}
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "创建整站备份文件[" + text + "] thành công");
					this.BindData();
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				catch (System.Exception ex)
				{
					throw ex;
				}
			}
		}

		protected void btn_CreateTempateBack_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Back"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				try
				{
					string text = base.BakFolder + StringUtils.GetNewFileName() + "_template.zip";
					using (ZipFile zipFile = new ZipFile(System.Text.Encoding.UTF8))
					{
						zipFile.AddDirectory(base.Server.MapPath(PageBase.defaultTemplate.TemplatePath));
						zipFile.Save(base.Server.MapPath(text));
					}
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "创建模板备份文件[" + text + "] thành công");
					this.BindData();
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				catch (System.Exception ex)
				{
					throw ex;
				}
			}
		}

		protected void btn_CreateUploadBack_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Back"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				try
				{
					string text = base.BakFolder + StringUtils.GetNewFileName() + "_upload.zip";
					using (ZipFile zipFile = new ZipFile(System.Text.Encoding.UTF8))
					{
						zipFile.AddDirectory(base.Server.MapPath(base.UploadFolder));
						zipFile.Save(base.Server.MapPath(text));
					}
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "创建上传文件备份文件[" + text + "] thành công");
					this.BindData();
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				catch (System.Exception ex)
				{
					throw ex;
				}
			}
		}

		private void BindData()
		{
			if (System.IO.Directory.Exists(base.Server.MapPath(base.BakFolder)))
			{
				System.Collections.Generic.List<DBBakFile> list = new System.Collections.Generic.List<DBBakFile>();
				System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(base.Server.MapPath(base.BakFolder));
				int num = 1;
				System.IO.FileInfo[] files = directoryInfo.GetFiles("*");
				for (int i = 0; i < files.Length; i++)
				{
					System.IO.FileInfo fileInfo = files[i];
					list.Add(new DBBakFile
					{
						AutoID = num,
						BakFileName = fileInfo.Name,
						VirtualPath = System.IO.Path.Combine(base.BakFolder, fileInfo.Name),
						BakFilePath = fileInfo.FullName,
						BakFileSize = this.GetFileSize(fileInfo.Length),
						UploadDate = fileInfo.LastWriteTime.ToString()
					});
					num++;
				}
				list.Sort((DBBakFile x, DBBakFile y) => y.UploadDate.CompareTo(x.UploadDate));
				this.Repeater1.DataSource = list;
				this.Repeater1.DataBind();
			}
		}

		protected void searchbtn_Click(object sender, ImageClickEventArgs e)
		{
			this.BindData();
		}

		public string GetFileSize(decimal length)
		{
			decimal d = System.Math.Round(length / 1024m);
			if (d < 1m && length != 0m)
			{
				d = 1m;
			}
			return d.ToString() + " KB";
		}

		protected void lnk_Restore_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp("Restore"))
			{
				base.ShowAjaxMsg(this.UpdatePanel1, "Không có thẩm quyền");
			}
		}

		protected void lnk_Delete_Click(object sender, System.EventArgs e)
		{
			if (!base.IsAuthorizedOp(ActionType.Delete.ToString()))
			{
				base.H5Tip(this.UpdatePanel1, "Không có thẩm quyền");
			}
			else
			{
				string commandArgument = (sender as LinkButton).CommandArgument;
				try
				{
					if (System.IO.File.Exists(commandArgument))
					{
						System.IO.File.Delete(commandArgument);
					}
					this.BindData();
					PageBase.log.AddEvent(base.LoginAccount.AccountName, "删除备份文件[" + commandArgument + "] thành công");
					base.ShowAjaxMsg(this.UpdatePanel1, "Thao tác thành công");
				}
				catch (System.Exception ex)
				{
					throw new System.Exception(ex.Message);
				}
			}
		}

		public string GetBakFileType(string strBakFileName)
		{
			string result;
			if (strBakFileName.Contains(".bak"))
			{
				result = "数据库备份";
			}
			else if (strBakFileName.Contains("_site"))
			{
				result = "整站备份";
			}
			else if (strBakFileName.Contains("_template"))
			{
				result = "模板备份";
			}
			else if (strBakFileName.Contains("_upload"))
			{
				result = "上传文件备份";
			}
			else
			{
				result = "其它备份";
			}
			return result;
		}

		private void SetConnectParameter()
		{
			this.conn = ConfigurationManager.ConnectionStrings["SQLConnSTR"].ConnectionString;
			this.server = this.cut(this.conn, "server=", ";");
			this.uid = this.cut(this.conn, "uid=", ";");
			this.pwd = this.cut(this.conn, "pwd=", ";");
			this.database = this.cut(this.conn, "database=", ";");
		}

		private string cut(string str, string bg, string ed)
		{
			string text = str.Substring(str.IndexOf(bg) + bg.Length);
			return text.Substring(0, text.IndexOf(";"));
		}
	}
}
