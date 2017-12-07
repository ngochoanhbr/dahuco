using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// 获取Json格式数据
    /// </summary>
    public static class JsonUtils
    {
        /// <summary>
        /// Dataset转化为json
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DatasetToJson(DataSet ds)
        {
            var iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(ds, iso, new DataSetConverter());
        }
        /// <summary>
        /// DataTable转化为json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, new DataTableConverter());
        }
        /// <summary>
        /// 对象序列化为json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {
            var iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(t, iso);
        }
        /// <summary>
        /// json数据反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string strValue)
        {
            return JsonConvert.DeserializeObject<T>(strValue);
        }
    }
}
