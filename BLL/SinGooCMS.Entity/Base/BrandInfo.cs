//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     Author:liqiang665@163.com www.sz3w.net
//     生成时间为:2014-9-1 15:51:43
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SinGooCMS.Entity 
{
    /// <summary>
    /// 品牌实体类
    /// </summary>
    [Serializable]
    public partial class BrandInfo : JObject, IEntity
    {        
        #region 私有字段
        
        private System.Int32 _AutoID=0;
        
        private System.String _BrandName=string.Empty;
        
        private System.String _CompanyName=string.Empty;
        
        private System.String _LogoPath=string.Empty;
        
        private System.String _OfficialSite=string.Empty;
        
        private System.String _IndName=string.Empty;
        
        private System.String _BrandDesc=string.Empty;
        
        private System.Boolean _IsRecommend=false;
        
        private System.Int32 _Sort=0;
        
        private System.DateTime _AutoTimeStamp=new DateTime(1900, 1, 1);
        
        private static List<System.String> _listField = null;
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BrandInfo() 
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
        /// 品牌名称
        /// </summary>
        public System.String BrandName 
        {
            get { return _BrandName; }
            set { _BrandName = value; }
        }
        
        /// <summary>
        /// 企业名称
        /// </summary>
        public System.String CompanyName 
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        
        /// <summary>
        /// 标志图片
        /// </summary>
        public System.String LogoPath 
        {
            get { return _LogoPath; }
            set { _LogoPath = value; }
        }
        
        /// <summary>
        /// 官方网址
        /// </summary>
        public System.String OfficialSite 
        {
            get { return _OfficialSite; }
            set { _OfficialSite = value; }
        }
        
        /// <summary>
        /// 所在行业
        /// </summary>
        public System.String IndName 
        {
            get { return _IndName; }
            set { _IndName = value; }
        }
        
        /// <summary>
        /// 描述
        /// </summary>
        public System.String BrandDesc 
        {
            get { return _BrandDesc; }
            set { _BrandDesc = value; }
        }
        
        /// <summary>
        /// 是否推荐
        /// </summary>
        public System.Boolean IsRecommend 
        {
            get { return _IsRecommend; }
            set { _IsRecommend = value; }
        }
        
        /// <summary>
        /// 排序
        /// </summary>
        public System.Int32 Sort 
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        
        /// <summary>
        /// 创建日期
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
                return "shop_Brand";
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
                return "AutoID,BrandName,CompanyName,LogoPath,OfficialSite,IndName,BrandDesc,IsRecommend,Sort,AutoTimeStamp";
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
                    
                    _listField.Add("BrandName");
                    
                    _listField.Add("CompanyName");
                    
                    _listField.Add("LogoPath");
                    
                    _listField.Add("OfficialSite");
                    
                    _listField.Add("IndName");
                    
                    _listField.Add("BrandDesc");
                    
                    _listField.Add("IsRecommend");
                    
                    _listField.Add("Sort");
                    
                    _listField.Add("AutoTimeStamp");
                    
                }

                return _listField;
            }
        }
        
        #endregion
        
    }
}