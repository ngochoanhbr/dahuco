/*
 * 缓存操作类
 * 因此获取缓存时为1小时,更新数据后需要更新缓存
 */

using System;
using System.Web;
using System.Web.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace SinGooCMS.Utility
{
    /// <summary>
    /// Cache操作类
    /// </summary>
    public static class CacheUtils
    {
        //缓存操作
        private static System.Web.Caching.Cache cache;

        static CacheUtils()
        {
            cache = System.Web.HttpRuntime.Cache;
        }

        #region 获取缓存值
        /// <summary>
        /// 读Cache对象的值，前提是这个值是字符串形式的
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        /// <returns>Cache字符串值</returns>
        public static object Get(string strCacheName)
        {
            return cache[strCacheName];
        }
        #endregion

        #region 获取缓存所有键值
        /// <summary>
        /// 获取缓存键列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetKeyList()
        {
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
            List<string> listResult = new List<string>();
            while (CacheEnum.MoveNext())
            {
                listResult.Add(CacheEnum.Key.ToString());
            }

            return listResult;
        }
        #endregion

        #region 添加新的缓存
        /// <summary>
        /// 添加缓存,默认缓存有效为1小时
        /// </summary>
        /// <param name="strCacheName"></param>
        /// <param name="objValue"></param>
        public static void Insert(string strCacheName, object objValue)
        {
            Insert(strCacheName, objValue, 3600, 1);
        }
        /// <summary>
        /// 创建Cache 同名缓存新的将覆盖旧的缓存
        /// 缓存有可能会被系统强制回收
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        /// <param name="strValue">Cache值</param>
        /// <param name="intExpires">有效期，秒数（使用的是当前时间+秒数得到一个绝对到期值）</param>
        /// <param name="priority">保留优先级，1最不会被清除，6最容易被内存管理清除（1:NotRemovable；2:High；3:AboveNormal；4:Normal；5:BelowNormal；6:Low）</param>
        public static void Insert(string strCacheName, object objValue, int intExpires, int intPriority)
        {
            TimeSpan ts = new TimeSpan(0, 0, intExpires);
            CacheItemPriority cachePriority;
            switch (intPriority)
            {
                case 6:
                    cachePriority = CacheItemPriority.Low;
                    break;
                case 5:
                    cachePriority = CacheItemPriority.BelowNormal;
                    break;
                case 4:
                    cachePriority = CacheItemPriority.Normal;
                    break;
                case 3:
                    cachePriority = CacheItemPriority.AboveNormal;
                    break;
                case 2:
                    cachePriority = CacheItemPriority.High;
                    break;
                case 1:
                    cachePriority = CacheItemPriority.NotRemovable;
                    break;
                default:
                    cachePriority = CacheItemPriority.Default;
                    break;
            }

            if (objValue != null)
                cache.Insert(strCacheName, objValue, null, DateTime.Now.Add(ts), System.Web.Caching.Cache.NoSlidingExpiration, cachePriority, null);

        }
        #endregion

        #region 删除缓存
        /// <summary>
        /// 删除Cache对象
        /// </summary>
        /// <param name="strCacheName">Cache名称</param>
        public static void Del(string strCacheName)
        {
            cache.Remove(strCacheName);
        }
        /// <summary>
        /// 删除相关的Cache对象 DelByPattern(@"CMS_ModelList_\S*");
        /// </summary>
        /// <param name="pattern"></param>
        public static void DelByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            List<string> list = new List<string>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    list.Add(enumerator.Key.ToString());
                }
            }
            foreach (string str in list)
            {
                HttpRuntime.Cache.Remove(str);
            }
        }
        /// <summary>
        /// 清空当前站所有缓存
        /// </summary>
        public static int ClearAll()
        {
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                cache.Remove(key);
            }
            //返回清空的缓存数量
            return al.Count;
        }
        #endregion

    }
}
