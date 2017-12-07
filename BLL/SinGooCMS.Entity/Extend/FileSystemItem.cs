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

        private int _DBID; //���ݿ��ж�Ӧ��ID
        private string _UploadUser; //�ϴ���

        /// <summary>
        /// ����
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
        /// ����Ŀ¼
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
        /// ����ʱ��
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
        /// �Ƿ����ļ���
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
        /// ��С
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
        /// ����ʱ��
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
        /// �޸�ʱ��
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
        /// �ļ���
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
        /// �ļ�����
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
        /// �汾
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            if (_Version == null)
                _Version = GetType().Assembly.GetName().Version.ToString();

            return _Version;
        }

        /// <summary>
        /// ���ݿ��ж�Ӧ���ļ�ID
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
        /// �ϴ���
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
        /// ���ݾ���·����ȡ����·��
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