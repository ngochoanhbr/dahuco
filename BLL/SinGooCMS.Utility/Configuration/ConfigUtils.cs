using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// configuration配置工具
    /// </summary>
    public static class ConfigUtils
    {
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appSettingName"></param>
        /// <returns></returns>
        public static T GetAppSetting<T>(string appSettingName) where T : IConvertible
        {
            return GetAppSettingInternal<T>(appSettingName, false, default(T));
        }
        public static T GetAppSetting<T>(string appSettingName, T defaultValue) where T : IConvertible
        {
            return GetAppSettingInternal<T>(appSettingName, true, defaultValue);
        }
        private static T GetAppSettingInternal<T>(string appSettingName, bool useDefaultOnUndefined, T defaultValue) where T : IConvertible
        {
            string value = ConfigurationManager.AppSettings[appSettingName];
            if (value == null)
            {
                if (useDefaultOnUndefined)
                    return defaultValue;
                else
                    throw new Exception(string.Format("{0} not defined in appSettings.", appSettingName));
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
        /// <summary>
        /// 添加配置属性
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="properties"></param>
        public static void AddConfigurationProperties(ConfigurationPropertyCollection collection, IEnumerable<ConfigurationProperty> properties)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (properties == null)
                throw new ArgumentNullException("properties");

            foreach (ConfigurationProperty property in properties)
                collection.Add(property);
        }
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (settings == null)
                throw new Exception(string.Format("没有找到名称为'{0}'的连接字符串", connectionStringName));

            return settings.ConnectionString;
        }
    }
}
