/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Config.cs
    文件功能描述：全局设置
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using SinGooCMS.Utility;

namespace Senparc.Weixin
{
    /// <summary>
    /// 全局设置
    /// </summary>   
    public class Config
    {
        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = 10000;

        /// <summary>
        /// 应用ID
        /// </summary>
        public static string AppID
        {
            get
            {
                return new XmlUtils(System.Web.HttpContext.Current.Server.MapPath("~/Config/weixinconfig.xml")).GetXmlNodeValue("Config/AppID").Trim();
            }
        }
        /// <summary>
        /// 应用密钥
        /// </summary>
        public static string AppSecret
        {
            get
            {
                return new XmlUtils(System.Web.HttpContext.Current.Server.MapPath("~/Config/weixinconfig.xml")).GetXmlNodeValue("Config/AppSecret").Trim();
            }
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string URL
        {
            get
            {
                return new XmlUtils(System.Web.HttpContext.Current.Server.MapPath("~/Config/weixinconfig.xml")).GetXmlNodeValue("Config/URL").Trim();
            }
        }
        /// <summary>
        /// 令牌
        /// </summary>
        public static string Token
        {
            get
            {
                return new XmlUtils(System.Web.HttpContext.Current.Server.MapPath("~/Config/weixinconfig.xml")).GetXmlNodeValue("Config/Token").Trim();
            }
        }
        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public static string EncodingAESKey
        {
            get
            {
                return new XmlUtils(System.Web.HttpContext.Current.Server.MapPath("~/Config/weixinconfig.xml")).GetXmlNodeValue("Config/EncodingAESKey").Trim();
            }
        }
        /// <summary>
        /// 每一条消息上下文过期时间
        /// </summary>
        public static int ExpireMinutes
        {
            get
            {
                int ret = WebUtils.GetInt(new XmlUtils(System.Web.HttpContext.Current.Server.MapPath("~/Config/weixinconfig.xml")).GetXmlNodeValue("Config/ExpireMinutes"));
                return ret == 0 ? 3 : ret;
            }
        }
    }
}
