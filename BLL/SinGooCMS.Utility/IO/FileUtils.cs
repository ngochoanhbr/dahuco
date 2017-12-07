using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace SinGooCMS.Utility
{
	public class FileUtils
	{
		public static void CreateDirectory(string filePath)
		{
			if (!System.IO.Directory.Exists(filePath))
			{
				System.IO.Directory.CreateDirectory(filePath);
			}
		}

		public static void CreateFile(string filePath, string fileContent)
		{
			FileUtils.CreateFile(filePath, fileContent, "utf-8");
		}

		public static void CreateFile(string filePath, string fileContent, string strCodeType)
		{
			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(strCodeType);
			System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(filePath, false, encoding);
			streamWriter.WriteLine(fileContent);
			streamWriter.Flush();
			streamWriter.Close();
		}

		public static string ReadFileContent(string filePath)
		{
			return FileUtils.ReadFileContent(filePath, System.Text.Encoding.UTF8);
		}

		public static string ReadFileContent(string filePath, System.Text.Encoding encoding)
		{
			string result = string.Empty;
			using (System.IO.StreamReader streamReader = new System.IO.StreamReader(filePath, encoding))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		public static void WriteFileContent(string filePath, string fileContent, bool isAppend)
		{
			FileUtils.WriteFileContent(filePath, fileContent, System.Text.Encoding.UTF8, isAppend);
		}

		public static void WriteFileContent(string filePath, string fileContent, System.Text.Encoding codeType, bool isAppend)
		{
			if (!System.IO.File.Exists(filePath))
			{
				FileUtils.CreateFile(filePath, string.Empty);
			}
			System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(filePath, isAppend, codeType);
			streamWriter.WriteLine(fileContent);
			streamWriter.Flush();
			streamWriter.Close();
			streamWriter.Dispose();
		}

		public static void DeleteFile(string filePath)
		{
			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}
		}

		public static void DeleteDirectory(string directoryPath)
		{
			if (System.IO.Directory.Exists(directoryPath))
			{
				System.IO.Directory.Delete(directoryPath, true);
			}
		}

		public static void ReNameFile(string filePath, string oldName, string newName, int fileType)
		{
			if (fileType.Equals(0))
			{
				if (System.IO.Directory.Exists(filePath + "\\" + oldName))
				{
					System.IO.Directory.Move(filePath + "\\" + oldName, filePath + "\\" + newName.Replace(".", ""));
				}
			}
			else if (System.IO.File.Exists(filePath + "\\" + oldName))
			{
				System.IO.File.Move(filePath + "\\" + oldName, filePath + "\\" + newName);
			}
		}

		public static string GetExtension(string strFileName)
		{
			return System.IO.Path.GetExtension(strFileName);
		}

		public static long GetDirectoryLength(string dirPath)
		{
			long result;
			if (!System.IO.Directory.Exists(dirPath))
			{
				result = 0L;
			}
			else
			{
				long num = 0L;
				System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(dirPath);
				System.IO.FileInfo[] files = directoryInfo.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					System.IO.FileInfo fileInfo = files[i];
					num += fileInfo.Length;
				}
				System.IO.DirectoryInfo[] directories = directoryInfo.GetDirectories();
				if (directories.Length > 0)
				{
					for (int j = 0; j < directories.Length; j++)
					{
						num += FileUtils.GetDirectoryLength(directories[j].FullName);
					}
				}
				result = num;
			}
			return result;
		}

		private static void CopyFolder(string strSources, string strDest)
		{
			System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(strSources);
			System.IO.FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			for (int i = 0; i < fileSystemInfos.Length; i++)
			{
				System.IO.FileSystemInfo fileSystemInfo = fileSystemInfos[i];
				string text = System.IO.Path.Combine(strDest, fileSystemInfo.Name);
				if (fileSystemInfo is System.IO.FileInfo)
				{
					System.IO.File.Copy(fileSystemInfo.FullName, text, true);
				}
				else
				{
					System.IO.Directory.CreateDirectory(text);
					FileUtils.CopyFolder(fileSystemInfo.FullName, text);
				}
			}
		}

		public static void CopyDirectory(string SourcePath, string DestinationPath)
		{
			if (!System.IO.Directory.Exists(DestinationPath))
			{
				System.IO.Directory.CreateDirectory(DestinationPath);
			}
			string[] array = System.IO.Directory.GetDirectories(SourcePath, "*", System.IO.SearchOption.AllDirectories);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				System.IO.Directory.CreateDirectory(text.Replace(SourcePath, DestinationPath));
			}
			array = System.IO.Directory.GetFiles(SourcePath, "*.*", System.IO.SearchOption.AllDirectories);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				System.IO.File.Copy(text2, text2.Replace(SourcePath, DestinationPath), true);
			}
		}

		public static string GetFileSize(decimal length)
		{
			decimal num = System.Math.Round(length / 1024m, 2, System.MidpointRounding.AwayFromZero);
			string str = "KB";
			if (length >= 1048576m)
			{
				num = System.Math.Round(length / 1048576m, 2, System.MidpointRounding.AwayFromZero);
				str = "MB";
			}
			else if (length >= 1073741824m)
			{
				num = System.Math.Round(length / 1073741824m, 2, System.MidpointRounding.AwayFromZero);
				str = "GB";
			}
			return num.ToString() + str;
		}

		public static string GetFileBaseName()
		{
			return StringUtils.GetNewFileName();
		}

		public static void AddDirectorySecurity(string strPath, string Account, System.Security.AccessControl.FileSystemRights Rights, System.Security.AccessControl.AccessControlType ControlType)
		{
			System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(strPath);
			System.Security.AccessControl.DirectorySecurity accessControl = directoryInfo.GetAccessControl();
			accessControl.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(Account, Rights, ControlType));
			directoryInfo.SetAccessControl(accessControl);
		}

		public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
		{
			if (!System.IO.File.Exists(sourceFileName))
			{
				throw new System.IO.FileNotFoundException(sourceFileName + "文件不存在！");
			}
			bool result;
			if (!overwrite && System.IO.File.Exists(destFileName))
			{
				result = false;
			}
			else
			{
				try
				{
					System.IO.File.Copy(sourceFileName, destFileName, true);
					result = true;
				}
				catch (System.Exception ex)
				{
					throw ex;
				}
			}
			return result;
		}

		public static bool BackupFile(string sourceFileName, string destFileName)
		{
			return FileUtils.BackupFile(sourceFileName, destFileName, true);
		}

		public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
		{
			try
			{
				if (!System.IO.File.Exists(backupFileName))
				{
					throw new System.IO.FileNotFoundException(backupFileName + "文件不存在！");
				}
				if (backupTargetFileName != null)
				{
					if (!System.IO.File.Exists(targetFileName))
					{
						throw new System.IO.FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
					}
					System.IO.File.Copy(targetFileName, backupTargetFileName, true);
				}
				System.IO.File.Delete(targetFileName);
				System.IO.File.Copy(backupFileName, targetFileName);
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return true;
		}

		public static bool RestoreFile(string backupFileName, string targetFileName)
		{
			return FileUtils.RestoreFile(backupFileName, targetFileName, null);
		}
	}
}
