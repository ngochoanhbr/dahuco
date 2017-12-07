using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SinGooCMS.BLL
{
	public class FileSystemManager : BizBase
	{
		private static string strRootFolder;

		static FileSystemManager()
		{
			FileSystemManager.strRootFolder = ConfigUtils.GetAppSetting<string>("UploadFolder");
			if (string.IsNullOrEmpty(FileSystemManager.strRootFolder))
			{
				FileSystemManager.strRootFolder = "/Upload/";
			}
			FileSystemManager.strRootFolder = FileSystemManager.strRootFolder.TrimEnd(new char[]
			{
				'/'
			});
			FileSystemManager.strRootFolder = HttpContext.Current.Server.MapPath(FileSystemManager.strRootFolder);
		}

		public static string GetRootPath()
		{
			return FileSystemManager.strRootFolder;
		}

		public static void SetRootPath(string path)
		{
			FileSystemManager.strRootFolder = path;
		}

		public static List<FileSystemItem> GetItems()
		{
			return FileSystemManager.GetItems(FileSystemManager.strRootFolder);
		}

		public static List<FileSystemItem> GetItems(string path)
		{
			string[] directories = Directory.GetDirectories(path);
			string[] files = Directory.GetFiles(path);
			List<FileSystemItem> list = new List<FileSystemItem>();
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = directories;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				FileSystemItem fileSystemItem = new FileSystemItem();
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				fileSystemItem.Name = directoryInfo.Name;
				fileSystemItem.FullName = directoryInfo.FullName;
				fileSystemItem.CreationDate = directoryInfo.CreationTime;
				fileSystemItem.IsFolder = true;
				fileSystemItem.DBID = 0;
				fileSystemItem.UpLoadUser = string.Empty;
				list.Add(fileSystemItem);
			}
			array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				FileSystemItem fileSystemItem = new FileSystemItem();
				FileInfo fileInfo = new FileInfo(text);
				fileSystemItem.Name = fileInfo.Name;
				fileSystemItem.FullName = fileInfo.FullName;
				fileSystemItem.CreationDate = fileInfo.CreationTime;
				fileSystemItem.IsFolder = false;
				fileSystemItem.Size = fileInfo.Length;
				fileSystemItem.DBID = 0;
				fileSystemItem.UpLoadUser = string.Empty;
				list.Add(fileSystemItem);
				stringBuilder.Append("'" + fileSystemItem.Name + "',");
			}
			string text2 = stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
			if (!string.IsNullOrEmpty(text2))
			{
				IList<FileUploadInfo> list2 = BizBase.dbo.GetList<FileUploadInfo>(" select * from sys_FileUpload where [FileName] in (" + text2 + ") ");
				if (list2 != null && list2.Count > 0)
				{
					foreach (FileSystemItem item in list)
					{
						FileUploadInfo fileUploadInfo = (from p in list2
						where p.FileName.Equals(item.Name)
						select p).Take(1).FirstOrDefault<FileUploadInfo>();
						if (fileUploadInfo != null)
						{
							item.DBID = fileUploadInfo.AutoID;
							item.UpLoadUser = fileUploadInfo.UserName;
						}
					}
				}
			}
			if (path.ToLower() != FileSystemManager.strRootFolder.ToLower())
			{
				FileSystemItem fileSystemItem2 = new FileSystemItem();
				DirectoryInfo parent = new DirectoryInfo(path).Parent;
				fileSystemItem2.Name = "[上一级]";
				fileSystemItem2.FullName = parent.FullName;
				list.Insert(0, fileSystemItem2);
				FileSystemItem fileSystemItem3 = new FileSystemItem();
				DirectoryInfo directoryInfo2 = new DirectoryInfo(FileSystemManager.strRootFolder);
				fileSystemItem3.Name = "[根目录]";
				fileSystemItem3.FullName = directoryInfo2.FullName;
				list.Insert(0, fileSystemItem3);
			}
			return list;
		}

		public static void CreateFolder(string name, string parentName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(parentName);
			directoryInfo.CreateSubdirectory(name);
		}

		public static bool DeleteFolder(string path)
		{
			bool result;
			try
			{
				Directory.Delete(path);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool MoveFolder(string oldPath, string newPath)
		{
			bool result;
			try
			{
				Directory.Move(oldPath, newPath);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool CreateFile(string filename, string path)
		{
			bool result;
			try
			{
				FileStream fileStream = File.Create(path + "\\" + filename);
				fileStream.Close();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool CreateFile(string filename, string path, byte[] contents)
		{
			bool result;
			try
			{
				FileStream fileStream = File.Create(path + "\\" + filename);
				fileStream.Write(contents, 0, contents.Length);
				fileStream.Close();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static string OpenText(string parentName)
		{
			StreamReader streamReader = File.OpenText(parentName);
			StringBuilder stringBuilder = new StringBuilder();
			string value;
			while ((value = streamReader.ReadLine()) != null)
			{
				stringBuilder.Append(value);
			}
			streamReader.Close();
			return stringBuilder.ToString();
		}

		public static bool WriteAllText(string parentName, string contents)
		{
			bool result;
			try
			{
				File.WriteAllText(parentName, contents, Encoding.Unicode);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool DeleteFile(string path)
		{
			bool result;
			try
			{
				File.Delete(path);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool MoveFile(string oldPath, string newPath)
		{
			bool result;
			try
			{
				File.Move(oldPath, newPath);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static FileSystemItem GetItemInfo(string path)
		{
			FileSystemItem fileSystemItem = new FileSystemItem();
			if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				fileSystemItem.Name = directoryInfo.Name;
				fileSystemItem.FullName = directoryInfo.FullName;
				fileSystemItem.CreationDate = directoryInfo.CreationTime;
				fileSystemItem.IsFolder = true;
				fileSystemItem.LastAccessDate = directoryInfo.LastAccessTime;
				fileSystemItem.LastWriteDate = directoryInfo.LastWriteTime;
				fileSystemItem.FileCount = (long)directoryInfo.GetFiles().Length;
				fileSystemItem.SubFolderCount = (long)directoryInfo.GetDirectories().Length;
			}
			else
			{
				FileInfo fileInfo = new FileInfo(path);
				fileSystemItem.Name = fileInfo.Name;
				fileSystemItem.FullName = fileInfo.FullName;
				fileSystemItem.CreationDate = fileInfo.CreationTime;
				fileSystemItem.LastAccessDate = fileInfo.LastAccessTime;
				fileSystemItem.LastWriteDate = fileInfo.LastWriteTime;
				fileSystemItem.IsFolder = false;
				fileSystemItem.Size = fileInfo.Length;
			}
			return fileSystemItem;
		}

		public static bool CopyFolder(string source, string destination)
		{
			bool result;
			try
			{
				if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
				{
					destination += Path.DirectorySeparatorChar;
				}
				if (!Directory.Exists(destination))
				{
					Directory.CreateDirectory(destination);
				}
				string[] fileSystemEntries = Directory.GetFileSystemEntries(source);
				string[] array = fileSystemEntries;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (Directory.Exists(text))
					{
						FileSystemManager.CopyFolder(text, destination + Path.GetFileName(text));
					}
					else
					{
						File.Copy(text, destination + Path.GetFileName(text), true);
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool IsSafeName(string strExtension)
		{
			strExtension = strExtension.ToLower();
			if (strExtension.LastIndexOf(".") >= 0)
			{
				strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
			}
			else
			{
				strExtension = ".txt";
			}
			string[] array = new string[]
			{
				".htm",
				".html",
				".txt",
				".js",
				".css",
				".xml",
				".sitemap",
				".jpg",
				".gif",
				".png",
				".rar",
				".zip"
			};
			bool result;
			for (int i = 0; i < array.Length; i++)
			{
				if (strExtension.Equals(array[i]))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static bool IsUnsafeName(string strExtension)
		{
			strExtension = strExtension.ToLower();
			if (strExtension.LastIndexOf(".") >= 0)
			{
				strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
			}
			else
			{
				strExtension = ".txt";
			}
			string[] array = new string[]
			{
				".",
				".asp",
				".aspx",
				".cs",
				".net",
				".dll",
				".config",
				".ascx",
				".master",
				".asmx",
				".asax",
				".cd",
				".browser",
				".rpt",
				".ashx",
				".xsd",
				".mdf",
				".resx",
				".xsd"
			};
			bool result;
			for (int i = 0; i < array.Length; i++)
			{
				if (strExtension.Equals(array[i]))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static bool IsCanEdit(string strExtension)
		{
			strExtension = strExtension.ToLower();
			if (strExtension.LastIndexOf(".") >= 0)
			{
				strExtension = strExtension.Substring(strExtension.LastIndexOf("."));
			}
			else
			{
				strExtension = ".txt";
			}
			string[] array = new string[]
			{
				".htm",
				".html",
				".txt",
				".js",
				".css",
				".xml",
				".sitemap"
			};
			bool result;
			for (int i = 0; i < array.Length; i++)
			{
				if (strExtension.Equals(array[i]))
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
	}
}
