//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     Author:liqiang665@163.com www.sz3w.net
//     生成时间为:2014-9-1 15:51:27
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity 
{
    /// <summary>
    /// 栏目实体类
    /// </summary>
    [Serializable]
    public partial class NodeInfo : JObject, IEntity
    {
        private string _NodeUrl = string.Empty;

        private NodeSetting _NodeSetting = new NodeSetting();

        private int _AutoID = 0;

        private string _NodeName = string.Empty;

        private int _ModelID = 0;

        private int _ParentID = 0;

        private string _ParentPath = string.Empty;

        private int _Depth = 0;

        private int _RootID = 0;

        private int _ChildCount = 0;

        private string _ChildList = string.Empty;

        private string _UrlRewriteName = string.Empty;

        private string _NodeImage = string.Empty;

        private string _NodeBanner = string.Empty;

        private string _SeoKey = string.Empty;

        private string _SeoDescription = string.Empty;

        private int _ItemPageSize = 0;

        private string _CustomLink = string.Empty;

        private string _Setting = string.Empty;

        private string _Remark = string.Empty;

        private bool _IsShowOnMenu = false;

        private bool _IsShowOnNav = false;

        private bool _IsTop = false;

        private bool _IsRecommend = false;

        private bool _IsNew = false;

        private string _Creator = string.Empty;

        private int _Sort = 0;

        private string _Lang = string.Empty;

        private DateTime _AutoTimeStamp = new DateTime(1900, 1, 1);

        private static List<string> _listField = null;

        public bool IsExtLink
        {
            get
            {
                return this._CustomLink.Length > 0;
            }
        }

        public string NodeUrl
        {
            get
            {
                if (string.IsNullOrEmpty(this._NodeUrl))
                {
                    this._NodeUrl = this.GetNodeUrl();
                }
                return this._NodeUrl;
            }
        }

        public NodeSetting NodeSetting
        {
            get
            {
                return this._NodeSetting;
            }
            set
            {
                this._NodeSetting = value;
            }
        }

        public int ContCount
        {
            get;
            set;
        }

        public int AutoID
        {
            get
            {
                return this._AutoID;
            }
            set
            {
                this._AutoID = value;
            }
        }

        public string NodeName
        {
            get
            {
                return this._NodeName;
            }
            set
            {
                this._NodeName = value;
            }
        }

        public int ModelID
        {
            get
            {
                return this._ModelID;
            }
            set
            {
                this._ModelID = value;
            }
        }

        public int ParentID
        {
            get
            {
                return this._ParentID;
            }
            set
            {
                this._ParentID = value;
            }
        }

        public string ParentPath
        {
            get
            {
                return this._ParentPath;
            }
            set
            {
                this._ParentPath = value;
            }
        }

        public int Depth
        {
            get
            {
                return this._Depth;
            }
            set
            {
                this._Depth = value;
            }
        }

        public int RootID
        {
            get
            {
                return this._RootID;
            }
            set
            {
                this._RootID = value;
            }
        }

        public int ChildCount
        {
            get
            {
                return this._ChildCount;
            }
            set
            {
                this._ChildCount = value;
            }
        }

        public string ChildList
        {
            get
            {
                return this._ChildList;
            }
            set
            {
                this._ChildList = value;
            }
        }

        public string UrlRewriteName
        {
            get
            {
                return this._UrlRewriteName;
            }
            set
            {
                this._UrlRewriteName = value;
            }
        }

        public string NodeImage
        {
            get
            {
                return this._NodeImage;
            }
            set
            {
                this._NodeImage = value;
            }
        }

        public string NodeBanner
        {
            get
            {
                return this._NodeBanner;
            }
            set
            {
                this._NodeBanner = value;
            }
        }

        public string SeoKey
        {
            get
            {
                return this._SeoKey;
            }
            set
            {
                this._SeoKey = value;
            }
        }

        public string SeoDescription
        {
            get
            {
                return this._SeoDescription;
            }
            set
            {
                this._SeoDescription = value;
            }
        }

        public int ItemPageSize
        {
            get
            {
                return this._ItemPageSize;
            }
            set
            {
                this._ItemPageSize = value;
            }
        }

        public string CustomLink
        {
            get
            {
                return this._CustomLink;
            }
            set
            {
                this._CustomLink = value;
            }
        }

        public string Setting
        {
            get
            {
                return this._Setting;
            }
            set
            {
                this._Setting = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public bool IsShowOnMenu
        {
            get
            {
                return this._IsShowOnMenu;
            }
            set
            {
                this._IsShowOnMenu = value;
            }
        }

        public bool IsShowOnNav
        {
            get
            {
                return this._IsShowOnNav;
            }
            set
            {
                this._IsShowOnNav = value;
            }
        }

        public bool IsTop
        {
            get
            {
                return this._IsTop;
            }
            set
            {
                this._IsTop = value;
            }
        }

        public bool IsRecommend
        {
            get
            {
                return this._IsRecommend;
            }
            set
            {
                this._IsRecommend = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this._IsNew;
            }
            set
            {
                this._IsNew = value;
            }
        }

        public string Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }

        public int Sort
        {
            get
            {
                return this._Sort;
            }
            set
            {
                this._Sort = value;
            }
        }

        public string Lang
        {
            get
            {
                return this._Lang;
            }
            set
            {
                this._Lang = value;
            }
        }

        public DateTime AutoTimeStamp
        {
            get
            {
                return this._AutoTimeStamp;
            }
            set
            {
                this._AutoTimeStamp = value;
            }
        }

        public string DBTableName
        {
            get
            {
                return "cms_Node";
            }
        }

        public string PKName
        {
            get
            {
                return "AutoID";
            }
        }

        public string Fields
        {
            get
            {
                return "AutoID,NodeName,ModelID,ParentID,ParentPath,Depth,RootID,ChildCount,ChildList,UrlRewriteName,NodeImage,NodeBanner,SeoKey,SeoDescription,ItemPageSize,CustomLink,Setting,Remark,IsShowOnMenu,IsShowOnNav,IsTop,IsRecommend,IsNew,Creator,Sort,Lang,AutoTimeStamp";
            }
        }

        public List<string> FieldList
        {
            get
            {
                if (NodeInfo._listField == null)
                {
                    NodeInfo._listField = new List<string>();
                    NodeInfo._listField.Add("AutoID");
                    NodeInfo._listField.Add("NodeName");
                    NodeInfo._listField.Add("ModelID");
                    NodeInfo._listField.Add("ParentID");
                    NodeInfo._listField.Add("ParentPath");
                    NodeInfo._listField.Add("Depth");
                    NodeInfo._listField.Add("RootID");
                    NodeInfo._listField.Add("ChildCount");
                    NodeInfo._listField.Add("ChildList");
                    NodeInfo._listField.Add("UrlRewriteName");
                    NodeInfo._listField.Add("NodeImage");
                    NodeInfo._listField.Add("NodeBanner");
                    NodeInfo._listField.Add("SeoKey");
                    NodeInfo._listField.Add("SeoDescription");
                    NodeInfo._listField.Add("ItemPageSize");
                    NodeInfo._listField.Add("CustomLink");
                    NodeInfo._listField.Add("Setting");
                    NodeInfo._listField.Add("Remark");
                    NodeInfo._listField.Add("IsShowOnMenu");
                    NodeInfo._listField.Add("IsShowOnNav");
                    NodeInfo._listField.Add("IsTop");
                    NodeInfo._listField.Add("IsRecommend");
                    NodeInfo._listField.Add("IsNew");
                    NodeInfo._listField.Add("Creator");
                    NodeInfo._listField.Add("Sort");
                    NodeInfo._listField.Add("Lang");
                    NodeInfo._listField.Add("AutoTimeStamp");
                }
                return NodeInfo._listField;
            }
        }
        
    }
}