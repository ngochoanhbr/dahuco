using System;
using System.Collections.Generic;
using System.Text;

namespace SinGooCMS.Entity
{
    /// <summary>
    /// FileSystemItem
    /// </summary>
    public class FileSystemItem
    {
        private string _Name;
        private string _FullName;

        private DateTime _CreationDate;
        private DateTime _LastAccessDate;
        private DateTime _LastWriteDate;

        private bool _IsFolder;

        private long _Size;
        private long _FileCount;
        private long _SubFolderCount;

        private string _Version;

        private int _DBID; //数据库中对应的ID
        private string _UploadUser; //上传者

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// 完整目录
        /// </summary>
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return _CreationDate;
            }
            set
            {
                _CreationDate = value;
            }
        }

        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return _IsFolder;
            }
            set
            {
                _IsFolder = value;
            }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public long Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime LastAccessDate
        {
            get
            {
                return _LastAccessDate;
            }
            set
            {
                _LastAccessDate = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastWriteDate
        {
            get
            {
                return _LastWriteDate;
            }
            set
            {
                _LastWriteDate = value;
            }
        }

        /// <summary>
        /// 文件数
        /// </summary>
        public long FileCount
        {
            get
            {
                return _FileCount;
            }
            set
            {
                _FileCount = value;
            }
        }

        /// <summary>
        /// 文件夹数
        /// </summary>
        public long SubFolderCount
        {
            get
            {
                return _SubFolderCount;
            }
            set
            {
                _SubFolderCount = value;
            }
        }

        /// <summary>
        /// 版本
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            if (_Version == null)
                _Version = GetType().Assembly.GetName().Version.ToString();

            return _Version;
        }

        /// <summary>
        /// 数据库中对应的文件ID
        /// </summary>
        public int DBID
        {
            get
            {
                return _DBID;
            }
            set
            {
                _DBID = value;
            }
        }

        /// <summary>
        /// 上传者
        /// </summary>
        public string UpLoadUser
        {
            get
            {
                return _UploadUser;
            }
            set
            {
                _UploadUser = value;
            }
        }
        /// <summary>
        /// 根据绝对路径获取虚拟路径
        /// </summary>
        public string VirtualPath
        {
            get
            {
                if (string.IsNullOrEmpty(FullName))
                    return string.Empty;
                else
                {
                    string temp = FullName.Substring(System.Web.HttpContext.Current.Server.MapPath("/").Length).Replace("///", "/").Replace("//", "/");
                    if (!temp.StartsWith("/"))
                        temp = "/" + temp;

                    return temp.TrimEnd('/');
                }
            }
        }
    }
}