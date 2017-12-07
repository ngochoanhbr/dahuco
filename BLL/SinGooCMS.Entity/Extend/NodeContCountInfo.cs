using System;
using System.Collections.Generic;
using System.Text;

namespace SinGooCMS.Entity
{
    /// <summary>
    /// 统计栏目的内容数量实体类
    /// </summary>
    public class NodeContCountInfo:JObject,IEntity
    {
        public int NodeID { get; set; }
        public string NodeName { get; set; }
        public int ContCount { get; set; }

        public string DBTableName
        {
            get { throw new NotImplementedException(); }
        }

        public string PKName
        {
            get { throw new NotImplementedException(); }
        }

        public string Fields
        {
            get { throw new NotImplementedException(); }
        }

        public List<string> FieldList
        {
            get { throw new NotImplementedException(); }
        }
    }
}
