using System;
using System.Data;

namespace SinGooCMS.Entity
{
    public partial class UserInfo
    {
        public UserGroupInfo UserGroup
        {
            get;
            set;
        }
        public UserLevelInfo UserLevel
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
                if (this.CustomTable.Rows.Count > 0)
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
    }
}
