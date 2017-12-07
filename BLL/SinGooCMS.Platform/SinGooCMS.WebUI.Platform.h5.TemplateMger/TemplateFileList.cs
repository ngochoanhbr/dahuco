using SinGooCMS.BLL;
using SinGooCMS.Common;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.TemplateMger
{
	public class TemplateFileList : H5ManagerPageBase
	{
		public string currentPath = string.Empty;

		public string CurrentTemplatePath = string.Empty;

		public string UpUrl = string.Empty;

		public string ViewUp = "none";

		public SiteTemplateInfo siteTemplate = null;

		private string[] arrCanEditFile = new string[]
		{
			".shtml",
			".html",
			".htm",
			".vml",
			".css",
			".js",
			".txt",
			".xml",
			".json"
		};

		private string[] arrImageFile = new string[]
		{
			".jpg",
			".jpeg",
			".gif",
			".png",
			".bmp"
		};

		protected TextBox search_text;

		protected Literal lblMsg;

		protected Repeater list_folder;

		protected Repeater list_file;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.siteTemplate = SiteTemplate.GetCacheSiteTemplateByID(base.OpID);
			if (this.siteTemplate == null)
			{
				MessageUtils.ShowAndRedirect(this, "没有找到模板信息", "TemplateList.aspx?Module=" + base.CurrentModuleCode + "&action=View");
			}
			else
			{
				this.BindData();
			}
		}

		private void BindData()
		{
			this.currentPath = WebUtils.GetQueryString("folder");
			if (this.currentPath.Length > 0)
			{
				this.ViewUp = "";
				this.UpUrl = "?folder=" + this.currentPath;
			}
			System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(this.GetTempletPath(this.currentPath));
			if (!System.IO.Directory.Exists(this.GetTempletPath(this.currentPath)))
			{
				this.lblMsg.Text = this.GetTempletPath(this.currentPath) + "：路径未找到";
			}
			else
			{
				System.IO.DirectoryInfo[] directories = directoryInfo.GetDirectories();
				this.list_folder.DataSource = directories;
				this.list_folder.DataBind();
				System.IO.FileInfo[] files = directoryInfo.GetFiles("*");
				this.list_file.DataSource = files;
				this.list_file.DataBind();
				this.CurrentTemplatePath = System.IO.Path.Combine(this.siteTemplate.TemplatePath, this.currentPath);
				this.CurrentTemplatePath = this.CurrentTemplatePath.Replace("\\", "/");
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		public string GetBaseFolder()
		{
			string queryString = WebUtils.GetQueryString("folder");
			string result;
			if (queryString.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				result = queryString + "/";
			}
			return result;
		}

		public string GetParentFolder()
		{
			string queryString = WebUtils.GetQueryString("folder");
			string result;
			if (queryString.Length == 0)
			{
				result = "#";
			}
			else
			{
				string text = string.Concat(new object[]
				{
					"?CatalogID=",
					base.CurrentCatalogID,
					"&Module=",
					base.CurrentModuleCode,
					"&action=View&opid=",
					base.OpID,
					"&folder="
				});
				if (queryString.IndexOf("\\") > 0)
				{
					text += queryString.Substring(0, queryString.LastIndexOf("\\"));
				}
				result = text;
			}
			return result;
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

		public string GetFileNameLink(string strFilePath, string strExt)
		{
			strExt = strExt.ToLower();
			string result;
			if (this.arrCanEditFile.Contains(strExt))
			{
				result = string.Concat(new string[]
				{
					"<a href=\"javascript:void(0)\" title='文本编辑' id='",
					base.Server.UrlEncode(strFilePath),
					"'><img src=\"../Images/Folder/",
					this.GetIcon(strExt),
					"\" width=\"20\" height=\"16\" />",
					strFilePath,
					"</a>"
				});
			}
			else if (this.arrImageFile.Contains(strExt))
			{
				result = string.Concat(new string[]
				{
					"<a href='javascript:void(0)' onclick=\"showimg('",
					this.siteTemplate.TemplatePath,
					this.currentPath,
					"/",
					strFilePath,
					"')\"><img src=\"../Images/Folder/",
					this.GetIcon(strExt),
					"\" width=\"20\" height=\"16\" />",
					strFilePath,
					"</a>"
				});
			}
			else
			{
				result = "<a href=\"javascript:void(0)\" onclick=\"$.dialog('不可编辑的文件类型')\"><img src=\"../Images/Folder/file.gif\" width=\"20\" height=\"16\" />" + strFilePath + "</a>";
			}
			return result;
		}

		public string GetDisplayLink(string strFilePath, string strExt)
		{
			strExt = strExt.ToLower();
			string result;
			if (this.arrCanEditFile.Contains(strExt))
			{
				result = "<a href=\"javascript:void(0)\" title='文本编辑' id='" + base.Server.UrlEncode(strFilePath) + "'>文本编辑</a>";
			}
			else if (this.arrImageFile.Contains(strExt))
			{
				result = string.Concat(new string[]
				{
					"<a href='javascript:void(0)' onclick=\"showimg('",
					this.siteTemplate.TemplatePath,
					this.currentPath,
					"/",
					strFilePath,
					"')\">查看图片</a>"
				});
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public string GetIcon(string strExt)
		{
			string text = "file.gif";
			strExt = strExt.ToLower();
			string[] array = this.arrCanEditFile;
			string result;
			for (int i = 0; i < array.Length; i++)
			{
				string a = array[i];
				if (a == strExt)
				{
					result = "html.gif";
					return result;
				}
			}
			array = this.arrImageFile;
			for (int i = 0; i < array.Length; i++)
			{
				string a = array[i];
				if (a == strExt)
				{
					result = "img.gif";
					return result;
				}
			}
			result = text;
			return result;
		}

		protected void list_folder_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
		}

		protected void list_file_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "DelFile")
			{
				string path = this.GetTempletPath(this.GetBaseFolder()) + e.CommandArgument.ToString();
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}
				this.BindData();
			}
		}
	}
}
