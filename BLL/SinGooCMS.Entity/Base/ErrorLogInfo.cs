//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     Author:liqiang665@163.com www.sz3w.net
//     生成时间为:2014-9-1 15:52:09
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
    public partial class ErrorLogInfo : JObject, IEntity
    {        
        #region 私有字段
        
        private System.Int32 _AutoID=0;
        
        private System.String _IPAddress=string.Empty;
        
        private System.String _OPSystem=string.Empty;
        
        private System.String _CustomerLang=string.Empty;
        
        private System.String _Navigator=string.Empty;
        
        private System.String _Resolution=string.Empty;
        
        private System.String _UserAgent=string.Empty;
        
        private System.Boolean _IsMobileDevice=false;
        
        private System.Boolean _IsSupportActiveX=false;
        
        private System.Boolean _IsSupportCookie=false;
        
        private System.Boolean _IsSupportJavascript=false;
        
        private System.Boolean _IsSupportJavaApplets=false;
        
        private System.String _NETVer=string.Empty;
        
        private System.Boolean _IsCrawler=false;
        
        private System.String _Engine=string.Empty;
        
        private System.String _KeyWord=string.Empty;
        
        private System.String _ApproachUrl=string.Empty;
        
        private System.String _VPage=string.Empty;
        
        private System.String _GETParameter=string.Empty;
        
        private System.String _POSTParameter=string.Empty;
        
        private System.String _CookieParameter=string.Empty;
        
        private System.String _ErrMessage=string.Empty;
        
        private System.String _StackTrace=string.Empty;
        
        private System.String _Lang=string.Empty;
        
        private System.DateTime _AutoTimeStamp=new DateTime(1900, 1, 1);
        
        private static List<System.String> _listField = null;
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ErrorLogInfo() 
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
        public System.String IPAddress 
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String OPSystem 
        {
            get { return _OPSystem; }
            set { _OPSystem = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String CustomerLang 
        {
            get { return _CustomerLang; }
            set { _CustomerLang = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String Navigator 
        {
            get { return _Navigator; }
            set { _Navigator = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String Resolution 
        {
            get { return _Resolution; }
            set { _Resolution = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String UserAgent 
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsMobileDevice 
        {
            get { return _IsMobileDevice; }
            set { _IsMobileDevice = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsSupportActiveX 
        {
            get { return _IsSupportActiveX; }
            set { _IsSupportActiveX = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsSupportCookie 
        {
            get { return _IsSupportCookie; }
            set { _IsSupportCookie = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsSupportJavascript 
        {
            get { return _IsSupportJavascript; }
            set { _IsSupportJavascript = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsSupportJavaApplets 
        {
            get { return _IsSupportJavaApplets; }
            set { _IsSupportJavaApplets = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String NETVer 
        {
            get { return _NETVer; }
            set { _NETVer = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsCrawler 
        {
            get { return _IsCrawler; }
            set { _IsCrawler = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String Engine 
        {
            get { return _Engine; }
            set { _Engine = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String KeyWord 
        {
            get { return _KeyWord; }
            set { _KeyWord = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String ApproachUrl 
        {
            get { return _ApproachUrl; }
            set { _ApproachUrl = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String VPage 
        {
            get { return _VPage; }
            set { _VPage = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String GETParameter 
        {
            get { return _GETParameter; }
            set { _GETParameter = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String POSTParameter 
        {
            get { return _POSTParameter; }
            set { _POSTParameter = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String CookieParameter 
        {
            get { return _CookieParameter; }
            set { _CookieParameter = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String ErrMessage 
        {
            get { return _ErrMessage; }
            set { _ErrMessage = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String StackTrace 
        {
            get { return _StackTrace; }
            set { _StackTrace = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String Lang 
        {
            get { return _Lang; }
            set { _Lang = value; }
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
                return "sys_ErrorLog";
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
                return "AutoID,IPAddress,OPSystem,CustomerLang,Navigator,Resolution,UserAgent,IsMobileDevice,IsSupportActiveX,IsSupportCookie,IsSupportJavascript,IsSupportJavaApplets,NETVer,IsCrawler,Engine,KeyWord,ApproachUrl,VPage,GETParameter,POSTParameter,CookieParameter,ErrMessage,StackTrace,Lang,AutoTimeStamp";
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
                    
                    _listField.Add("IPAddress");
                    
                    _listField.Add("OPSystem");
                    
                    _listField.Add("CustomerLang");
                    
                    _listField.Add("Navigator");
                    
                    _listField.Add("Resolution");
                    
                    _listField.Add("UserAgent");
                    
                    _listField.Add("IsMobileDevice");
                    
                    _listField.Add("IsSupportActiveX");
                    
                    _listField.Add("IsSupportCookie");
                    
                    _listField.Add("IsSupportJavascript");
                    
                    _listField.Add("IsSupportJavaApplets");
                    
                    _listField.Add("NETVer");
                    
                    _listField.Add("IsCrawler");
                    
                    _listField.Add("Engine");
                    
                    _listField.Add("KeyWord");
                    
                    _listField.Add("ApproachUrl");
                    
                    _listField.Add("VPage");
                    
                    _listField.Add("GETParameter");
                    
                    _listField.Add("POSTParameter");
                    
                    _listField.Add("CookieParameter");
                    
                    _listField.Add("ErrMessage");
                    
                    _listField.Add("StackTrace");
                    
                    _listField.Add("Lang");
                    
                    _listField.Add("AutoTimeStamp");
                    
                }

                return _listField;
            }
        }
        
        #endregion
        
    }
}