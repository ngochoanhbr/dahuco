using SinGooCMS.Common;
using SinGooCMS.Utility;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace SinGooCMS.WebUI.Platform.h5.TemplateMger
{
	public class TemplateFileListForSelect : H5ManagerPageBase
	{
		protected Repeater list_folder;

		protected Repeater list_file;

		public string currentPath = string.Empty;

		public string CurrentTemplatePath = string.Empty;

		public string UpUrl = string.Empty;

		public string ViewUp = "none";

		public string strElementID = string.Empty;

		private string[] arrCanEditFile = new string[]
		{
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.strElementID = WebUtils.GetQueryString("elementid");
			this.BindData();
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
			System.IO.DirectoryInfo[] directories = directoryInfo.GetDirectories();
			this.list_folder.DataSource = directories;
			this.list_folder.DataBind();
			System.IO.FileInfo[] files = directoryInfo.GetFiles("*");
			this.list_file.DataSource = files;
			this.list_file.DataBind();
			this.CurrentTemplatePath = System.IO.Path.Combine(PageBase.defaultTemplate.TemplatePath, this.currentPath);
			this.CurrentTemplatePath = this.CurrentTemplatePath.Replace("\\", "/");
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

		public string GetFileSize(decimal length)
		{
			decimal d = System.Math.Round(length / 1024m);
			if (d < 1m && length != 0m)
			{
				d = 1m;
			}
			return d.ToString() + " KB";
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
				string str = "?Module=" + base.CurrentModuleCode + "&folder=";
				if (queryString.IndexOf("\\") > 0)
				{
					str += queryString.Substring(0, queryString.LastIndexOf("\\"));
				}
				result = str + "&elementid=" + this.strElementID;
			}
			return result;
		}

		public string GetTempletPath(string currentPath)
		{
			string result;
			if (currentPath.Length == 0)
			{
				result = HttpContext.Current.Server.MapPath(PageBase.defaultTemplate.TemplatePath);
			}
			else
			{
				result = HttpContext.Current.Server.MapPath(System.IO.Path.Combine(PageBase.defaultTemplate.TemplatePath, currentPath));
			}
			return result;
		}

		public string GetDisplayLink(string strFilePath, string strExt, string strShowTitle)
		{
			string[] array = this.arrCanEditFile;
			string result;
			for (int i = 0; i < array.Length; i++)
			{
				string a = array[i];
				if (a == strExt)
				{
					result = string.Concat(new string[]
					{
						"<a href=\"javascript:void(0)\" title='选定' id='",
						this.GetBaseFolder(),
						strFilePath,
						"'>",
						strShowTitle,
						"</a>"
					});
					return result;
				}
			}
			result = "<a href=\"javascript:void(0)\" title='选定' id='" + this.GetBaseFolder() + strFilePath + "'>选择文件</a>";
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
	}
}
