//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     Author:liqiang665@163.com www.sz3w.net
//     生成时间为:2014-9-1 15:51:41
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity 
{
    /// <summary>
    /// 投票记录实体类
    /// </summary>
    [Serializable]
    public partial class VoteLogInfo : JObject, IEntity
    {        
        #region 私有字段
        
        private System.Int32 _AutoID=0;
        
        private System.Int32 _VoteItemID=0;
        
        private System.Int32 _UserID=0;
        
        private System.String _UserName=string.Empty;
        
        private System.String _IpAddress=string.Empty;
        
        private System.String _IpArea=string.Empty;
        
        private System.String _Lang=string.Empty;
        
        private System.DateTime _AutoTimeStamp=new DateTime(1900, 1, 1);
        
        private static List<System.String> _listField = null;
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public VoteLogInfo() 
        {
            //
        }
        
        #region 公共属性
        
        /// <summary>
        /// 主键
        /// </summary>
        public System.Int32 AutoID 
        {
            get { return _AutoID; }
            set { _AutoID = value; }
        }
        
        /// <summary>
        /// 投票项ID
        /// </summary>
        public System.Int32 VoteItemID 
        {
            get { return _VoteItemID; }
            set { _VoteItemID = value; }
        }
        
        /// <summary>
        /// 会员ID
        /// </summary>
        public System.Int32 UserID 
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        
        /// <summary>
        /// 会员名称
        /// </summary>
        public System.String UserName 
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        
        /// <summary>
        /// IP地址
        /// </summary>
        public System.String IpAddress 
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        
        /// <summary>
        /// IP所在地区
        /// </summary>
        public System.String IpArea 
        {
            get { return _IpArea; }
            set { _IpArea = value; }
        }
        
        /// <summary>
        /// 语言版本
        /// </summary>
        public System.String Lang 
        {
            get { return _Lang; }
            set { _Lang = value; }
        }
        
        /// <summary>
        /// 投票日期
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
                return "cms_VoteLog";
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
                return "AutoID,VoteItemID,UserID,UserName,IpAddress,IpArea,Lang,AutoTimeStamp";
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
                    
                    _listField.Add("VoteItemID");
                    
                    _listField.Add("UserID");
                    
                    _listField.Add("UserName");
                    
                    _listField.Add("IpAddress");
                    
                    _listField.Add("IpArea");
                    
                    _listField.Add("Lang");
                    
                    _listField.Add("AutoTimeStamp");
                    
                }

                return _listField;
            }
        }
        
        #endregion
        
    }
}