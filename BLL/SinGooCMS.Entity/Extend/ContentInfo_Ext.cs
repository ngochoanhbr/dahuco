using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Entity
{
    public partial class ContentInfo
    {
        private List<int> _NodeExtIDs;

        public NodeInfo Node
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义表
        /// </summary>
        public DataTable CustomTable
        {
            get;
            set;
        }
        public DataRow Items
        {
            get
            {
                if (this.CustomTable != null && this.CustomTable.Rows.Count > 0)
                {
                    return this.CustomTable.Rows[0];
                }
                return null;
            }
        }
        public object Get(string name)
        {
            if (((this.CustomTable != null) && (this.CustomTable.Rows.Count > 0)) && this.CustomTable.Columns.Contains(name))
            {
                return this.CustomTable.Rows[0][name];
            }
            return string.Empty;
        }
        /// <summary>
        /// 内容URL
        /// </summary>
        public string ContentUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 内容所属栏目扩展
        /// </summary>
        public List<int> NodeExtIDs
        {
            get
            {
                if (_NodeExtIDs != null)
                    return _NodeExtIDs;

                return new List<int>();
            }
            set
            {
                _NodeExtIDs = value;
            }
        }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount
        {
            get;
            set;
        }
        /// <summary>
        /// 相关内容ID集合
        /// </summary>
        public string RelateContentIDs
        {
            get
            {
                if (!string.IsNullOrEmpty(RelateContent))
                {
                    StringBuilder builder = new StringBuilder();
                    string[] arrRelate = RelateContent.Split(',');
                    for (int i = 0; i < arrRelate.Length; i++)
                    {
                        int intID = 0;
                        if (arrRelate[i].Split('|').Length == 2 && int.TryParse(arrRelate[i].Split('|')[1], out intID))
                        {
                            if (i == 0) builder.Append(intID.ToString());
                            else builder.Append("," + intID.ToString());
                        }
                        else
                        {
                            if (i == 0) builder.Append(0);
                            else builder.Append("," + 0);
                        }
                    }

                    return builder.ToString();
                }

                return string.Empty;
            }
        }
    }
}
