using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Install
{
    [Serializable]
    public class InstallSetting
    {
        public string DB_Server { get; set; }
        public string DB_Name { get; set; }
        public string DB_Uid { get; set; }
        public string DB_Pwd { get; set; }

        /// <summary>
        /// 超级管理员名称
        /// </summary>
        public string SuperManager { get; set; }
        /// <summary>
        /// 超级管理员密码
        /// </summary>
        public string SuperManagerPwd { get; set; }
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string ManagerPwd { get; set; }
    }
}
