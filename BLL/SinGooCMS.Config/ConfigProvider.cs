using System;
using System.Collections.Generic;
using System.Text;

using SinGooCMS.DAL;
using SinGooCMS.Utility;

namespace SinGooCMS.Config
{
    public class ConfigProvider
    {
        static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);

        #region 代码生成器提供的基本功能

        #region 更新记录
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public static bool Update(BaseConfigInfo entity)
        {            
            //更新基本配置
            return dbo.UpdateModel<BaseConfigInfo>(entity);
        }
        #endregion

        #region 获取实体
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public static BaseConfigInfo GetEntity()
        {
            return dbo.GetModel<BaseConfigInfo>(" SELECT * FROM sys_BaseConfig ");
        }
        #endregion

        #endregion

        #region 扩展功能
        public static BaseConfigInfo Configs
        {
            get
            {
                return GetCacheBaseConfig();
            }
        }
        /// <summary>
        /// 获取缓存基本配置
        /// </summary>
        /// <returns></returns>
        public static BaseConfigInfo GetCacheBaseConfig()
        {
            BaseConfigInfo baseConfig = (BaseConfigInfo)CacheUtils.Get(CacheKey.CKEY_BASECONFIG);
            if (baseConfig == null)
            {
                baseConfig = dbo.GetModel<BaseConfigInfo>(" select top 1 * from sys_BaseConfig ");
                CacheUtils.Insert(CacheKey.CKEY_BASECONFIG, baseConfig);
            }

            return baseConfig;
        }
        #endregion
    }
}
