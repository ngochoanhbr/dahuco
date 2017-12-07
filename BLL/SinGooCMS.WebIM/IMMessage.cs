using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using SinGooCMS.DAL;

namespace SinGooCMS.WebIM
{
    /// <summary>
    /// 消息
    /// </summary>
    public class IMMessage : ComtBase, IEntity
    {
        #region 属性
        /// <summary>
        /// 消息ID
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// 通道ID
        /// </summary>
        public string ChannelId { get; set; }
        /// <summary>
        /// 发送方类型名称 管理员/经销商
        /// </summary>
        public string SenderType { get; set; }
        /// <summary>
        /// 发送方用户ID
        /// </summary>
        public int SenderID { get; set; }
        /// <summary>
        /// 发送方名称
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 接收方类型
        /// </summary>
        public string ReciverType { get; set; }
        /// <summary>
        /// 接收方ID
        /// </summary>
        public int ReciverID { get; set; }
        /// <summary>
        /// 接收方名称
        /// </summary>
        public string Reciver { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 消息是否阅读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 消息阅读时间
        /// </summary>
        public DateTime ReadTime { get; set; }

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
                return "sys_IMMessage";
            }
        }
        public string Fields
        {
            get
            {
                return "AutoID,ChannelId,SenderType,SenderID,Sender,ReciverType,ReciverID,Reciver,Msg,SendTime,IsRead,ReadTime";
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
                _listField.Add("ChannelId");
                _listField.Add("SenderType");
                _listField.Add("SenderID");
                _listField.Add("Sender");
                _listField.Add("ReciverType");
                _listField.Add("ReciverID");
                _listField.Add("Reciver");
                _listField.Add("Msg");
                _listField.Add("SendTime");
                _listField.Add("IsRead");
                _listField.Add("ReadTime");
                return _listField;
            }

        }
        #endregion

        #region 方法
        /// <summary>
        /// 存储信息并返回消息ID
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int SaveMessage(IMMessage msg)
        {
            return dbo.InsertModel<IMMessage>(msg);
        }
        /// <summary>
        /// 存储消息,包括同时发给多人
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <param name="senderType"></param>
        /// <param name="senderID"></param>
        /// <param name="senderName"></param>
        /// <param name="reciverInfo"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool SaveMessage(string ChannelId, string senderType, int senderID, string senderName, string reciverInfo, string Message)
        {
            SqlParameter[] parameters = { 
                                            new SqlParameter("@ChannelId",ChannelId),
                                            new SqlParameter("@SenderType",senderType),
                                            new SqlParameter("@SenderID",senderID),
                                            new SqlParameter("@Sender",senderName),
                                            new SqlParameter("@ReciverInfo",reciverInfo),
                                            new SqlParameter("@Msg",Message)
                                        };

            return dbo.ExecProc("p_System_AddChatMessage", parameters);
        }
        /// <summary>
        /// 获取当前用户未读的消息
        /// </summary>
        /// <param name="strChannelId"></param>
        /// <returns></returns>
        public static IList<IMMessage> GetMessageListByReciver(string strUserType, int intUserID)
        {
            return dbo.GetList<IMMessage>(" SELECT * FROM sys_IMMessage WHERE ReciverType='" + strUserType + "' AND ReciverID=" + intUserID + " AND IsRead=0 ORDER BY SendTime asc ");
        }
        /// <summary>
        /// 用户正确取回消息后设置消息为已读
        /// </summary>
        /// <param name="strIds"></param>
        /// <returns></returns>
        public static bool ReadMessage(string strIds)
        {
            return dbo.UpdateTable(" UPDATE sys_IMMessage SET IsRead = 1,ReadTime = getdate() WHERE AutoID IN (" + strIds + ")  ");
        }
        /// <summary>
        /// 是否存在消息标识
        /// </summary>
        /// <param name="strFlag"></param>
        /// <returns></returns>
        public static bool ExistsMessage(string strFlag)
        {
            return dbo.GetValue<int>(" SELECT Count(*) FROM sys_IMMessage WHERE ChannelId='" + strFlag + "' ") > 0;
        }
        /// <summary>
        /// 是否有新消息
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int HasNewMessage(string userType, int userID)
        {
            SqlParameter[] parameters = { 
                                            new SqlParameter("@ReciverType",userType),
                                            new SqlParameter("@ReciverID",userID)
                                        };

            object obj = dbo.ExecProcReValue("p_System_HasNewMessage", parameters);
            return (obj != null && obj != DBNull.Value) ? int.Parse(obj.ToString()) : 0;
        }

        #region 获取分页记录

        /// <summary>
        /// 获取分页记录数
        /// </summary>
        /// <param name="strCondition">条件</param>
        /// <param name="strSort">排序</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intCurrentPageIndex">当前页号</param>
        /// <param name="intTotalCount">回传总记录</param>
        /// <param name="intTotalPage">回传总页数</param>
        /// <returns></returns>
        public static DataSet GetPagerData(string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
        {
            DataSet dsReturn = new DataSet();

            Pager pager = new Pager();
            pager.PagerTable = "sys_IMMessage";
            pager.PagerCondition = strCondition;
            pager.PagerSort = strSort;
            pager.PagerSize = intPageSize;
            pager.PagerIndex = intCurrentPageIndex;

            dsReturn = pager.GetData();
            intTotalCount = pager.PagerTotalCount;
            intTotalPage = pager.PagerTotalPage;

            return dsReturn;
        }

        #endregion

        #region 删除记录
        /// <summary>
        /// 删除与我相关的的记录
        /// </summary>
        /// <param name="strUserType"></param>
        /// <param name="intUserID"></param>
        /// <returns></returns>
        public static bool DelMyMsg(string strUserType, int intUserID)
        {
            return dbo.ExecSQL(" DELETE FROM sys_IMMessage WHERE (SenderID=" + intUserID + " AND SenderType='" + strUserType + "') OR (ReciverID=" + intUserID + " AND ReciverType='" + strUserType + "') ");
        }
        /// <summary>
        /// 清空所有记录并重置
        /// </summary>
        /// <returns></returns>
        public static bool DelMsgAll()
        {
            return dbo.ExecSQL(" TRUNCATE TABLE sys_IMMessage ");
        }
        #endregion

        #endregion

    }
}
