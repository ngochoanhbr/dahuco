using System;

namespace SinGooCMS.Entity
{
    [Serializable]
    public class NodeSetting
    {
        private string customManageUrl;
        private bool enableAddInParent = true;
        private string fileExt = ".html";
        private bool isCreateHtml = true;
        private bool isCreateNodeContentHtml = true;
        private bool isCreateNodeListHtml = true;
        private bool mAllowComment=true;
        private string mCustomTitle;
        private bool mNeedLogin=false;
        private string mQueryCountSql;
        private bool mShowOnPath;
        private string pagePatternOfNodeContent = string.Empty;
        private string pagePatternOfNodeList = string.Empty;
        private string templateOfNodeContent = string.Empty;
        private string templateOfNodeIndex = string.Empty;
        private string templateOfNodeList = string.Empty;
        //允许访问会员组
        private string mEnableViewUGroups;
        //允许访问会员等级
        private string mEnableViewULevel;

        /// <summary>
        /// 栏目首页模板
        /// </summary>
        public string TemplateOfNodeIndex
        {
            get
            {
                return this.templateOfNodeIndex;
            }
            set
            {
                this.templateOfNodeIndex = value;
            }
        }
        /// <summary>
        /// 栏目列表模板
        /// </summary>
        public string TemplateOfNodeList
        {
            get
            {
                return this.templateOfNodeList;
            }
            set
            {
                this.templateOfNodeList = value;
            }
        }
        /// <summary>
        /// 栏目内容模板
        /// </summary>
        public string TemplateOfNodeContent
        {
            get
            {
                return this.templateOfNodeContent;
            }
            set
            {
                this.templateOfNodeContent = value;
            }
        }
        /// <summary>
        /// 是否生成静态页
        /// </summary>
        public bool IsCreateHtml
        {
            get
            {
                return this.isCreateHtml;
            }
            set
            {
                this.isCreateHtml = value;
            }
        }
        /// <summary>
        /// 是否生成静态栏目列表
        /// </summary>
        public bool IsCreateNodeListHtml
        {
            get
            {
                return this.isCreateNodeListHtml;
            }
            set
            {
                this.isCreateNodeListHtml = value;
            }
        }
        /// <summary>
        /// 是否生成栏目内容静态页
        /// </summary>
        public bool IsCreateNodeContentHtml
        {
            get
            {
                return this.isCreateNodeContentHtml;
            }
            set
            {
                this.isCreateNodeContentHtml = value;
            }
        }
        /// <summary>
        /// 静态页文件扩展名
        /// </summary>
        public string FileExt
        {
            get
            {
                return this.fileExt;
            }
            set
            {
                this.fileExt = value;
            }
        }
        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool AllowComment
        {
            get
            {
                return this.mAllowComment;
            }
            set
            {
                this.mAllowComment = value;
            }
        }
        /// <summary>
        /// 自定义管理地址
        /// </summary>
        public string CustomManageUrl
        {
            get
            {
                return this.customManageUrl;
            }
            set
            {
                this.customManageUrl = value;
            }
        }

        public string CustomTitle
        {
            get
            {
                return this.mCustomTitle;
            }
            set
            {
                this.mCustomTitle = value;
            }
        }

        public bool EnableAddInParent
        {
            get
            {
                return this.enableAddInParent;
            }
            set
            {
                this.enableAddInParent = value;
            }
        }

        public bool NeedLogin
        {
            get
            {
                return this.mNeedLogin;
            }
            set
            {
                this.mNeedLogin = value;
            }
        }

        public string PagePatternOfNodeContent
        {
            get
            {
                return this.pagePatternOfNodeContent;
            }
            set
            {
                this.pagePatternOfNodeContent = value;
            }
        }

        public string PagePatternOfNodeList
        {
            get
            {
                return this.pagePatternOfNodeList;
            }
            set
            {
                this.pagePatternOfNodeList = value;
            }
        }

        public string QueryCountSql
        {
            get
            {
                return this.mQueryCountSql;
            }
            set
            {
                this.mQueryCountSql = value;
            }
        }

        public bool ShowOnPath
        {
            get
            {
                return this.mShowOnPath;
            }
            set
            {
                this.mShowOnPath = value;
            }
        }
        /// <summary>
        /// 允许访问会员组
        /// </summary>
        public string EnableViewUGroups
        {
            get
            {
                return this.mEnableViewUGroups;
            }
            set
            {
                this.mEnableViewUGroups = value;
            }
        }
        /// <summary>
        /// 允许访问会员等级
        /// </summary>
        public string EnableViewULevel
        {
            get
            {
                return this.mEnableViewULevel;
            }
            set
            {
                this.mEnableViewULevel = value;
            }
        }

    }
}

