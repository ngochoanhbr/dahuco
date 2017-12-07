//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     Author:liqiang665@163.com www.sz3w.net
//     生成时间为:2014-9-1 15:51:11
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity 
{
    /// <summary>
    /// 实体类
    /// </summary>
    [Serializable]
    public partial class AccountsRoleInfo : JObject, IEntity
    {        
        #region 私有字段
        
        private System.Int32 _AutoID=0;
        
        private System.Int32 _AccountID=0;
        
        private System.Int32 _RoleID=0;
        
        private System.DateTime _AutoTimeStamp=new DateTime(1900, 1, 1);
        
        private static List<System.String> _listField = null;
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AccountsRoleInfo() 
        {
            //
        }
        
        #region 公共属性
        
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 AutoID 
        {
            get { return _AutoID; }
            set { _AutoID = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 AccountID 
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 RoleID 
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime AutoTimeStamp 
        {
            get { return _AutoTimeStamp; }
            set { _AutoTimeStamp = value; }
        }
                
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string DBTableName
        {
            get
            {
                return "cms_AccountsRole";
            }
        }
        public string PKName
        {
            get
            {
                return "AutoID";
            }
        }
        /// <summary>
        /// 整合字段为字符串并输出
        /// </summary>
        /// <returns></returns>
        public string Fields
        {
            get
            {
                return "AutoID,AccountID,RoleID,AutoTimeStamp";
            }            
        }
        /// <summary>
        /// 数据表字段
        /// </summary>
        public List<System.String> FieldList
        {
            get
            {
                if (_listField == null)
                {     
                    _listField = new List<System.String>();          
                    
                    _listField.Add("AutoID");
                    
                    _listField.Add("AccountID");
                    
                    _listField.Add("RoleID");
                    
                    _listField.Add("AutoTimeStamp");
                    
                }

                return _listField;
            }
        }
        
        #endregion
        
    }
}