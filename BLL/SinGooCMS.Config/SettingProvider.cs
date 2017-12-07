using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using SinGooCMS.Utility;
using SinGooCMS.DAL;
using SinGooCMS.DAL.Utils;

namespace SinGooCMS.Config
{
    public class SettingProvider
    {
        private static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);
        private static SettingProvider _instance;
        private static object lockObject = new object();

        private SettingProvider()
        {
            //单例模式
        }

        #region 代码生成器提供的基本功能

        #region 添加记录
        /// <summary>
        /// 添加一条记录,并返回记录的ID
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public static int Add(SettingInfo entity)
        {
            return dbo.InsertModel<SettingInfo>(entity);
        }

        #endregion

        #region 更新记录
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public static bool Update(SettingInfo entity)
        {
            return dbo.UpdateModel<SettingInfo>(entity);
        }
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="intPrimaryKeyID">主键ID</param>
        /// <returns></returns>
        public static bool Delete(int intPrimaryKeyID)
        {
            return dbo.DeleteTable("sys_Setting", "AutoID=" + intPrimaryKeyID.ToString());
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="idsList">主键ID多列</param>
        /// <returns></returns>
        public static bool Delete(string strArrIdList)
        {
            return dbo.DeleteTable("sys_Setting", " AutoID in (" + strArrIdList + ") ");
        }
        #endregion

        #region 获取实体
        /// <summary>
        /// 根据主键ID获取实体
        /// </summary>
        /// <param name="intPrimaryKeyID">主键ID</param>
        /// <returns></returns>
        public static SettingInfo GetEntityById(int intPrimaryKeyID)
        {
            return dbo.GetModel<SettingInfo>("sys_Setting", "AutoID=" + intPrimaryKeyID.ToString());
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取表记录列表
        /// </summary>
        /// <returns></returns>
        public static IList<SettingInfo> GetList()
        {
            return GetList(0, string.Empty);
        }
        /// <summary>
        /// 获取表记录列表
        /// </summary>
        /// <param name="intTopCount">前N条记录</param>
        /// <returns></returns>
        public static IList<SettingInfo> GetTopNList(int intTopCount)
        {
            return GetList(intTopCount, string.Empty);
        }
        /// <summary>
        /// 获取表记录列表
        /// </summary>
        /// <param name="intTopCount">前N条记录</param>
        /// <param name="strCondition">查询条件</param>
        /// <returns></returns>
        public static IList<SettingInfo> GetList(int intTopCount, string strCondition)
        {
            string strSQL = " select ";

            if (intTopCount > 0)
                strSQL = strSQL + " top " + intTopCount;

            strSQL = strSQL + " * from sys_Setting ";

            if (!string.IsNullOrEmpty(strCondition))
                strSQL = strSQL + " where " + strCondition;

            return dbo.GetList<SettingInfo>(strSQL);
        }

        #endregion

        #region 分页相关
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            return dbo.GetValue<int?>("Count(*)", "sys_Setting", "", "") ?? 0;
        }
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <param name="strCondition">查询条件</param>
        /// <returns></returns>
        public static int GetCount(string strCondition)
        {
            return dbo.GetValue<int?>("Count(*)", "sys_Setting", strCondition, "") ?? 0;
        }
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentPageIndex">当前页号</param>
        /// <returns></returns>
        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
        {
            int intTotalCount = new int();
            int intTotalPage = new int();
            return GetPagerData(intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentPageIndex">当前页号</param>
        /// <param name="intTotalCount">回传总记录</param>
        /// <param name="intTotalPage">回传总页数</param>
        /// <returns></returns>
        public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="strFilter">字段</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentPageIndex">当前页号</param>
        /// <param name="intTotalCount">回传总记录</param>
        /// <param name="intTotalPage">回传总页数</param>
        /// <returns></returns>
        public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="strFilter">字段</param>
        /// <param name="strCondition">条件</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentPageIndex">当前页号</param>
        /// <param name="intTotalCount">回传总记录</param>
        /// <param name="intTotalPage">回传总页数</param>
        /// <returns></returns>
        public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            return GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
        }
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="strFilter">字段</param>
        /// <param name="strCondition">条件</param>
        /// <param name="strSort">排序</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentPageIndex">当前页号</param>
        /// <param name="intTotalCount">回传总记录</param>
        /// <param name="intTotalPage">回传总页数</param>
        /// <returns></returns>
        public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            DataSet dsReturn = new DataSet();

            Pager pager = new Pager();
            pager.PagerFilter = strFilter;
            pager.PagerTable = "sys_Setting";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;

            dsReturn = pager.GetData();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;

            return dsReturn;
        }
        /// <summary>
        /// 获取分页记录列表
        /// </summary>
        /// <param name="intCurrentPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="intTotalCount"></param>
        /// <param name="intTotalPage"></param>
        /// <returns></returns>
        public static IList<SettingInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            return GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
        }
        /// <summary>
        /// 获取分页记录列表
        /// </summary>
        /// <param name="strCondition"></param>
        /// <param name="strSort"></param>
        /// <param name="intCurrentPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="intTotalCount"></param>
        /// <param name="intTotalPage"></param>
        /// <returns></returns>
        public static IList<SettingInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
        {
            IList<SettingInfo> listResult = new List<SettingInfo>();

            Pager pager = new Pager();
            pager.PagerFilter = "*";
            pager.PagerTable = "sys_Setting";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;

            listResult = pager.GetPagerList<SettingInfo>();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;

            return listResult;
        }
        #endregion

        #endregion

        /// <summary>
        /// 获取缓存自定义设置
        /// </summary>
        /// <returns></returns>
        public static IList<SettingInfo> GetCacheSetting()
        {
            IList<SettingInfo> listSet = (List<SettingInfo>)CacheUtils.Get(CacheKey.CKEY_SETTING);
            if (listSet == null)
            {
                listSet = dbo.GetList<SettingInfo>(" select * from sys_Setting ");
                CacheUtils.Insert(CacheKey.CKEY_SETTING, listSet);
            }

            return listSet;
        }

        /// <summary>
        /// 获取缓存的自定义配置项
        /// </summary>
        /// <returns></returns>
        public static SettingInfo GetCacheSettingByName(string strKeyName)
        {
            var result = GetCacheSetting().Where(param => param.KeyName.Equals(strKeyName));
            return result.Count() > 0 ? result.First() : null;
        }

        /// <summary>
        /// 获取缓存设置
        /// </summary>
        /// <param name="intSettingCateID"></param>
        /// <returns></returns>
        public static IList<SettingInfo> GetCacheSettingByCateID(int intSettingCateID)
        {
            var result = GetCacheSetting().Where(param => param.CateID.Equals(intSettingCateID));
            return result.Count() > 0 ? result.ToList() : new List<SettingInfo>();
        }

        /// <summary>
        /// 获取缓存的自定义设置字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, SettingInfo> GetCacheSettingDictionary()
        {
            object cache = CacheUtils.Get(CacheKey.CKEY_SETTINGDICTIONARY);
            if (cache != null)
            {
                return (Dictionary<string, SettingInfo>)cache;
            }

            IList<SettingInfo> allSettings = GetCacheSetting();
            Dictionary<string, SettingInfo> dictionary = new Dictionary<string, SettingInfo>();
            foreach (SettingInfo info in allSettings)
            {
                if (!dictionary.ContainsKey(info.KeyName))
                {
                    dictionary.Add(info.KeyName, info);
                }
            }
            CacheUtils.Insert(CacheKey.CKEY_SETTINGDICTIONARY, dictionary);
            return dictionary;
        }
        public static SettingProvider Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new SettingProvider();
                    }
                }
            }
            return _instance;
        }
        /// <summary>
        /// 是否存在重复设置
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <returns></returns>
        public static bool ExistsByName(string strKeyName)
        {
            return dbo.GetValue<int>(" SELECT TOP 1 1 FROM sys_Setting WHERE KeyName='" + strKeyName + "' ") > 0;
        }

        #region 更新配置值
        public void UpdateSettings(List<SettingInfo> settingsList)
        {
            foreach (SettingInfo item in settingsList)
            {
                this.UpdateSettingsValue(item);
            }
        }
        private void UpdateSettingsValue(SettingInfo setting)
        {
            string commandText = " UPDATE sys_Setting SET KeyValue=@KeyValue WHERE AutoID=@AutoID ";
            SqlParameter[] parameters = new SqlParameter[] { SqlDbHelper.MakeInParam("@KeyValue", SqlDbType.NVarChar, setting.KeyValue), SqlDbHelper.MakeInParam("@AutoID", SqlDbType.Int, setting.AutoID) };
            DBHelper.ExecuteNonQuery(commandText, parameters);
        }
        #endregion
    }
}
