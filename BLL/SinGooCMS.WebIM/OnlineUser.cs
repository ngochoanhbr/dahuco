using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SinGooCMS.Entity;
using SinGooCMS.DAL;
using SinGooCMS.Utility;

namespace SinGooCMS.WebIM
{
    /// <summary>
    /// 在线会员表 global全局定时维护
    /// </summary>
    public class OnlineUser : ComtBase, IEntity
    {
        #region 属性
        /// <summary>
        /// 自动编号
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime InTime { get; set; }
        /// <summary>
        /// 最后发言时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        public string PKName
        {
            get
            {
                return "AutoID";
            }
        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        public string DBTableName
        {
            get
            {
                return "sys_OnlineUser";
            }
        }
        /// <summary>
        /// 数据表字段
        /// </summary>
        public List<System.String> FieldList
        {
            get
            {
                List<string> _listField = new List<string>();

                _listField.Add("AutoID");
                _listField.Add("UserID");
                _listField.Add("UserName");
                _listField.Add("UserType");
                _listField.Add("InTime");
                _listField.Add("LastUpdateTime");

                return _listField;
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
                return "AutoID,UserID,UserName,UserType,InTime,LastUpdateTime";
            }
        }
        #endregion

        private const string CKEY_ONLINEUSER = "CacheForSaveOnlieUserList";

        #region 方法

        #region CACHE OP
        /// <summary>
        /// 判断是否已经存在相同名称的在线会员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool ExistsOnlineUser(OnlineUser user)
        {
            IList<OnlineUser> listSource = GetCacheOnlineUserList();
            if (listSource != null && listSource.Count > 0)
            {
                foreach (OnlineUser item in listSource)
                {
                    if (item.UserID.Equals(user.UserID) && item.UserType.Equals(user.UserType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        private static void UpdateCache()
        {
            //更新缓存
            SinGooCMS.Utility.CacheUtils.Del(CKEY_ONLINEUSER);
        }
        #endregion

        #region 获取在线会员
        /// <summary>
        /// 获取在线会员
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public static OnlineUser GetOnlieUserByID(string strUserType, int intUserID)
        {
            IList<OnlineUser> listSource = GetCacheOnlineUserList();
            if (listSource != null && listSource.Count > 0)
            {
                foreach (OnlineUser item in listSource)
                {
                    if (item.UserType.Equals(strUserType) && item.UserID.Equals(intUserID))
                        return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取在线会员
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public static OnlineUser GetOnlieUserByName(string strUserName, string strUserType)
        {
            IList<OnlineUser> listSource = GetCacheOnlineUserList();
            if (listSource != null && listSource.Count > 0)
            {
                foreach (OnlineUser item in listSource)
                {
                    if (item.UserType.Equals(strUserType) && item.UserName.Equals(strUserName))
                        return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取在线会员列表
        /// </summary>
        /// <returns></returns>
        public static IList<OnlineUser> GetCacheOnlineUserList()
        {
            IList<OnlineUser> list = (IList<OnlineUser>)SinGooCMS.Utility.CacheUtils.Get(CKEY_ONLINEUSER);
            if (list == null)
            {
                list = dbo.GetList<OnlineUser>(" SELECT * FROM sys_OnlineUser ORDER BY LastUpdateTime DESC ");
                SinGooCMS.Utility.CacheUtils.Insert(CKEY_ONLINEUSER, list);
            }

            return list;
        }

        /// <summary>
        /// 获取在线的管理员
        /// </summary>
        /// <returns></returns>
        public static IList<OnlineUser> GetCacheOnlineManager()
        {
            IList<OnlineUser> listSource = GetCacheOnlineUserList();
            IList<OnlineUser> listResult = new List<OnlineUser>();
            if (listSource != null && listSource.Count > 0)
            {
                foreach (OnlineUser item in listSource)
                {
                    if (item.UserType.Equals("manager"))
                        listResult.Add(item);
                }
            }

            return listResult;
        }
        /// <summary>
        /// 获取在线的会员
        /// </summary>
        /// <returns></returns>
        public static IList<OnlineUser> GetCacheOnlineUser()
        {
            IList<OnlineUser> listSource = GetCacheOnlineUserList();
            IList<OnlineUser> listResult = new List<OnlineUser>();
            if (listSource != null && listSource.Count > 0)
            {
                foreach (OnlineUser item in listSource)
                {
                    if (item.UserType.Equals("user"))
                        listResult.Add(item);
                }
            }

            return listResult;
        }

        #endregion

        #region 在线会员ACTION
        /// <summary>
        /// 创建在线会员
        /// </summary>
        /// <param name="user"></param>
        public static void CreateOnlineUser(OnlineUser user)
        {
            user.InTime = System.DateTime.Now;
            user.LastUpdateTime = System.DateTime.Now;
            dbo.InsertModel<OnlineUser>(user);

            UpdateCache();
        }

        /// <summary>
        /// 轮询会员在线表.超过5分钟没有发表即不在线
        /// </summary>
        public static void CheckOnlineUser()
        {
            IList<OnlineUser> list = GetCacheOnlineUserList();
            if (list != null && list.Count > 0)
            {
                foreach (OnlineUser item in list)
                {
                    TimeSpan ts = (System.DateTime.Now - item.LastUpdateTime);
                    if (ts.Minutes >= 5) //超过5分钟没有活动则认为是已经退出
                        dbo.DeleteTable("sys_OnlineUser", "AutoID="+item.AutoID.ToString());
                }

                UpdateCache();
            }
        }
        /// <summary>
        /// 更新会员最后活动时间
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateUserLastActTime(OnlineUser user)
        {
            user.LastUpdateTime = System.DateTime.Now;
            dbo.UpdateModel<OnlineUser>(user);

            UpdateCache();
        }
        #endregion

        #region 聊天对象列表

        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserDT()
        {
            DataTable dtSource = dbo.GetDataTable(" SELECT AutoID AS UserID,UserName,Sort,UserType='user',IsOnline='0' FROM cms_User WHERE UserName<>'游客' ORDER BY Sort ASC "); //dbo.ExecProcReDT("p_System_GetIMUsers", null);
            List<OnlineUser> listOnline = (List<OnlineUser>)GetCacheOnlineUser();
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSource.Rows)
                {
                    int intUserID = Convert.ToInt32(dr["UserID"]);
                    if (listOnline.Exists(delegate(OnlineUser parameterA) { return parameterA.UserID == intUserID; }))
                        dr["IsOnline"] = 1;//如果在线则更新状态
                }
            }

            return dtSource;
        }

        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetManagerDT()
        {
            DataTable dtSource = dbo.GetDataTable(" SELECT AutoID AS UserID,AccountName AS UserName,Sort='0',UserType='manager',IsOnline='0' FROM cms_Account WHERE AccountName!='SuperAdmin' "); //dbo.ExecProcReDT("p_System_GetIMManagers", null);
            List<OnlineUser> listOnline = (List<OnlineUser>)GetCacheOnlineManager();
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSource.Rows)
                {
                    int intManagerID = Convert.ToInt32(dr["UserID"]);
                    if (listOnline.Exists(delegate(OnlineUser parameterA) { return parameterA.UserID == intManagerID; }))
                        dr["IsOnline"] = 1;//如果在线则更新状态
                }
            }

            return dtSource;
        }

        #endregion

        #region 获取聊天记录的对象列表
        public static DataTable GetRecordUserList(string strUserType, int intUserID)
        {
            SqlParameter[] parameters = { 
                                            new SqlParameter("@usertype", strUserType),
                                            new SqlParameter("@userid", intUserID) 
                                        };
            return dbo.ExecProcReDT("p_System_GetIMRecordUserList", parameters);
        }
        #endregion

        #region 获取即时通讯用户列表

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
            if (strFilter == string.Empty || strFilter == "*")
                strFilter = "UserID,UserName,Sort,UserType,IsOnline";

            Pager pager = new Pager();
            pager.PagerFilter = strFilter;
            pager.PagerTable = "v_System_ChatUserList";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;

            dsReturn = pager.GetData();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;

            return dsReturn;
        }
        public static int GetMaxUserListPage(string strCondition)
        {
            string strSQL = " SELECT count(*) FROM v_System_ChatUserList ";
            if (!string.IsNullOrEmpty(strCondition))
                strSQL += " WHERE "+strCondition;

            return dbo.GetValue<int>(strSQL);
        }

        #endregion

        #endregion
    }
}
