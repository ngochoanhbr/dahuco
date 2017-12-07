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
    /// 
    /// </summary>
    [Serializable]
    public partial class SupplierInfo : JObject, IEntity
    {        
        #region Khai báo
        
        private System.Int32 _AutoID=0;
        
        private System.String _Name=string.Empty;
        
        private System.String _Address=string.Empty;
        
        private System.String _Country=string.Empty;
        
        private System.DateTime _AutoTimeStamp=new DateTime(1900, 1, 1);
        
        private static List<System.String> _listField = null;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public SupplierInfo() 
        {
            //
        }
        
        #region Khai báo lớp
        
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
        public System.String Name 
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String Address 
        {
            get { return _Address; }
            set { _Address = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String Country 
        {
            get { return _Country; }
            set { _Country = value; }
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
        /// 
        /// </summary>
        public string DBTableName
        {
            get
            {
                return "dh_supplier";
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
        /// 
        /// </summary>
        /// <returns></returns>
        public string Fields
        {
            get
            {
                return "AutoID, Name, Address, Country, AutoTimeStamp";
            }            
        }
        /// <summary>
        ///
        /// </summary>
        public List<System.String> FieldList
        {
            get
            {
                if (_listField == null)
                {     
                    _listField = new List<System.String>();

                    _listField.Add("AutoID");

                    _listField.Add("Name");

                    _listField.Add("Address");

                    _listField.Add("Country");

                    _listField.Add("AutoTimeStamp");                 
                    
                    
                }

                return _listField;
            }
        }
        
        #endregion
        
    }
}