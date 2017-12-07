using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// configuration���ù���
    /// </summary>
    public static class ConfigUtils
    {
        /// <summary>
        /// ��ȡ����ֵ
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
        /// �����������
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
        /// ��ȡ�����ַ���
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (settings == null)
                throw new Exception(string.Format("û���ҵ�����Ϊ'{0}'�������ַ���", connectionStringName));

            return settings.ConnectionString;
        }
    }
}
