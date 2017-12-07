using System;

namespace SinGooCMS.Entity
{
    public partial class UserLevelInfo
    {
        /*
        /// <summary>
        /// 会员等级是否过期
        /// </summary>
        public bool IsExpired
        {
            get
            {
                if (Config.ConfigProvider.Configs.IsNeverExpired)
                    return false; //会员等级永远不过期
                else
                {
                    //有过期设置是判断是否过期
                    DateTime dtBegin = this._LastModifyDate;
                    DateTime dtEnd = dtBegin.AddMonths(Config.ConfigProvider.Configs.ExpireMonth);
                    if ((System.DateTime.Now - dtEnd).Days > 0)
                        return true;
                }

                return false;
            }
        }*/
    }
}
