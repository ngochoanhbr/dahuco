using SinGooCMS.DAL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SinGooCMS.BLL
{
	public class Message : BizBase
	{
		public static int MaxSort
		{
			get
			{
				return BizBase.dbo.GetValue<int>(" SELECT MAX(Sort) FROM sys_Message ");
			}
		}

		public static int Add(MessageInfo entity)
		{
			int result;
			if (entity == null)
			{
				result = 0;
			}
			else
			{
				result = BizBase.dbo.InsertModel<MessageInfo>(entity);
			}
			return result;
		}

		public static bool Update(MessageInfo entity)
		{
			return entity != null && BizBase.dbo.UpdateModel<MessageInfo>(entity);
		}

		public static bool Delete(int intPrimaryKeyIDValue)
		{
			return intPrimaryKeyIDValue > 0 && BizBase.dbo.DeleteTable(" DELETE FROM sys_Message WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
		}

		public static bool Delete(string strArrIdList)
		{
			return !string.IsNullOrEmpty(strArrIdList) && BizBase.dbo.DeleteTable(" DELETE FROM sys_Message WHERE AutoID in (" + strArrIdList + ") ");
		}

		public static MessageInfo GetDataById(int intPrimaryKeyIDValue)
		{
			MessageInfo result;
			if (intPrimaryKeyIDValue <= 0)
			{
				result = null;
			}
			else
			{
				result = BizBase.dbo.GetModel<MessageInfo>(" SELECT TOP 1 AutoID,MsgType,SenderType,Sender,ReceiverType,Receiver,MsgTitle,MsgBody,SendTime,IsRead,ReadTime,Lang FROM sys_Message WHERE AutoID=" + intPrimaryKeyIDValue.ToString());
			}
			return result;
		}

		public static MessageInfo GetTopData()
		{
			return Message.GetTopData(" Sort ASC,AutoID desc ");
		}

		public static MessageInfo GetTopData(string strSort)
		{
			string text = " SELECT TOP 1 AutoID,MsgType,SenderType,Sender,ReceiverType,Receiver,MsgTitle,MsgBody,SendTime,IsRead,ReadTime,Lang FROM sys_Message ";
			if (!string.IsNullOrEmpty(strSort))
			{
				text = text + " order by " + strSort;
			}
			return BizBase.dbo.GetModel<MessageInfo>(text);
		}

		public static IList<MessageInfo> GetAllList()
		{
			return Message.GetList(0, string.Empty);
		}

		public static IList<MessageInfo> GetTopNList(int intTopCount)
		{
			return Message.GetTopNList(intTopCount, string.Empty);
		}

		public static IList<MessageInfo> GetTopNList(int intTopCount, string strSort)
		{
			return Message.GetList(intTopCount, string.Empty, strSort);
		}

		public static IList<MessageInfo> GetList(int intTopCount, string strCondition)
		{
			string strSort = " Sort asc,AutoID desc ";
			return Message.GetList(intTopCount, strCondition, strSort);
		}

		public static IList<MessageInfo> GetList(int intTopCount, string strCondition, string strSort)
		{
			StringBuilder stringBuilder = new StringBuilder(" select ");
			if (intTopCount > 0)
			{
				stringBuilder.Append(" top " + intTopCount.ToString());
			}
			stringBuilder.Append(" AutoID,MsgType,SenderType,Sender,ReceiverType,Receiver,MsgTitle,MsgBody,SendTime,IsRead,ReadTime,Lang from sys_Message ");
			if (!string.IsNullOrEmpty(strCondition))
			{
				stringBuilder.Append(" where " + strCondition);
			}
			if (!string.IsNullOrEmpty(strSort))
			{
				stringBuilder.Append(" order by " + strSort);
			}
			return BizBase.dbo.GetList<MessageInfo>(stringBuilder.ToString());
		}

		public static int GetCount()
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Message", "", "") ?? 0;
		}

		public static int GetCount(string strCondition)
		{
			return BizBase.dbo.GetValue<int?>("Count(*)", "sys_Message", strCondition, "") ?? 0;
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex)
		{
			int num = 0;
			int num2 = 0;
			return Message.GetPagerData(intPageSize, intCurrentPageIndex, ref num, ref num2);
		}

		public static DataSet GetPagerData(int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Message.GetPagerData("*", string.Empty, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Message.GetPagerData("*", strCondition, intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			return Message.GetPagerData(strFilter, strCondition, " Sort asc,AutoID desc ", intPageSize, intCurrentPageIndex, ref intTotalCount, ref intTotalPage);
		}

		public static DataSet GetPagerData(string strFilter, string strCondition, string strSort, int intPageSize, int intCurrentPageIndex, ref int intTotalCount, ref int intTotalPage)
		{
			DataSet result = new DataSet();
			if (strFilter == string.Empty || strFilter == "*")
			{
				strFilter = "AutoID,MsgType,SenderType,Sender,ReceiverType,Receiver,MsgTitle,MsgBody,SendTime,IsRead,ReadTime,Lang";
			}
			Pager pager = new Pager();
			pager.PagerFilter = strFilter;
			pager.PagerTable = "sys_Message";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetData();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static IList<MessageInfo> GetPagerList(int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			return Message.GetPagerList("", "Sort ASC,AutoID DESC", intCurrentPageIndex, intPageSize, ref intTotalCount, ref intTotalPage);
		}

		public static IList<MessageInfo> GetPagerList(string strCondition, string strSort, int intCurrentPageIndex, int intPageSize, ref int intTotalCount, ref int intTotalPage)
		{
			IList<MessageInfo> result = new List<MessageInfo>();
			Pager pager = new Pager();
			pager.PagerFilter = "AutoID,MsgType,SenderType,Sender,ReceiverType,Receiver,MsgTitle,MsgBody,SendTime,IsRead,ReadTime,Lang";
			pager.PagerTable = "sys_Message";
			pager.PagerCondition = strCondition;
			pager.PagerSort = strSort;
			pager.PagerSize = intPageSize;
			pager.PagerIndex = intCurrentPageIndex;
			result = pager.GetPagerList<MessageInfo>();
			intTotalCount = pager.PagerTotalCount;
			intTotalPage = pager.PagerTotalPage;
			return result;
		}

		public static void UpdateSort(int intPrimaryKeyValue, int intSort)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Message SET Sort=" + intSort.ToString() + " WHERE AutoID=" + intPrimaryKeyValue.ToString());
		}

		public static bool UpdateSort(Dictionary<int, int> dicIDAndSort)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (dicIDAndSort.Count > 0)
			{
				foreach (KeyValuePair<int, int> current in dicIDAndSort)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						" UPDATE sys_Message SET Sort =",
						current.Value.ToString(),
						" WHERE AutoID=",
						current.Key.ToString(),
						"; "
					}));
				}
			}
			return BizBase.dbo.ExecSQL(stringBuilder.ToString());
		}

		public static void SendStockAlarm(string strMsg)
		{
			MessageInfo model = BizBase.dbo.GetModel<MessageInfo>(" select top 1 * from sys_Message where MsgBody='" + strMsg + "' order by AutoID desc  ");
			if (model == null || (model != null && (DateTime.Now - model.SendTime).TotalDays > 3.0))
			{
				Message.SendMsg(MsgType.SystemMsg, UserType.System, "系统", UserType.System, "系统", "商品库存报警", strMsg);
			}
		}

		public static void SendSysMsg(string strMsg)
		{
			Message.SendSysMsg("来自系统的消息", strMsg);
		}

		public static void SendSysMsg(string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.SystemMsg, UserType.System, "系统", UserType.System, "系统", strTitle, strMsg);
		}

		public static void SendMsg(string strReceiver, string strMsg)
		{
			Message.SendMsg(strReceiver, "来自系统的消息", strMsg);
		}

		public static void SendMsg(string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.SystemMsg, UserType.System, "系统", UserType.User, strReceiver, strTitle, strMsg);
		}

		public static void SendM2UMsg(string strSender, string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.ManagerMsg, UserType.Manager, strSender, UserType.User, strReceiver, strTitle, strMsg);
		}

		public static void SendU2MMsg(string strSender, string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.UserMsg, UserType.User, strSender, UserType.Manager, strReceiver, strTitle, strMsg);
		}

		public static void SendM2MMsg(string strSender, string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.ManagerMsg, UserType.Manager, strSender, UserType.Manager, strReceiver, strTitle, strMsg);
		}

		public static void SendU2UMsg(string strSender, string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.UserMsg, UserType.User, strSender, UserType.User, strReceiver, strTitle, strMsg);
		}

		public static void SendS2UMsg(string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(MsgType.SystemMsg, UserType.System, "系统", UserType.User, strReceiver, strTitle, strMsg);
		}

		public static void SendMsg(MsgType msgType, UserType senderType, string strSender, UserType receiverType, string strReceiver, string strTitle, string strMsg)
		{
			Message.SendMsg(msgType, senderType, strSender, receiverType, new List<string>
			{
				strReceiver
			}, strTitle, strMsg);
		}

		public static void SendMsg(MsgType msgType, UserType senderType, string strSender, UserType receiverType, IList<string> lstReceiver, string strTitle, string strMsg)
		{
			string str = " INSERT INTO sys_Message(MsgType, SenderType, Sender, ReceiverType, Receiver,MsgTitle,MsgBody,IsRead,Lang) ";
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string current in lstReceiver)
			{
				stringBuilder.Append(string.Concat(new string[]
				{
					" SELECT '",
					EnumUtils.GetEnumDescription<MsgType>(msgType),
					"','",
					senderType.ToString(),
					"','",
					strSender.Replace("'", "''"),
					"','",
					receiverType.ToString(),
					"','",
					current.Replace("'", "''"),
					"','",
					strTitle.Replace("'", "''"),
					"','",
					strMsg.Replace("'", "''"),
					"',0,'",
					JObject.cultureLang,
					"' union all "
				}));
				if ((num + 1) % 10 == 0 || num == lstReceiver.Count - 1)
				{
					BizBase.dbo.ExecSQL(str + " " + stringBuilder.ToString().Trim().Substring(0, stringBuilder.ToString().Trim().LastIndexOf("union all")));
					stringBuilder.Remove(0, stringBuilder.Length);
				}
				num++;
			}
		}

		public static void Read(string strIdList)
		{
			BizBase.dbo.ExecSQL(" UPDATE sys_Message SET IsRead =1,ReadTime =getdate() WHERE AutoID IN (" + strIdList + ") ");
		}

		public static int GetNewMsgCount(string strUserName)
		{
			return BizBase.dbo.GetValue<int>(string.Concat(new string[]
			{
				" select COUNT(*) from sys_Message where ReceiverType='",
				UserType.User.ToString(),
				"' AND Receiver='",
				strUserName,
				"' and IsRead=0 "
			}));
		}
	}
}
