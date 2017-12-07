using System;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using System.Text;

using SinGooCMS.Utility;

namespace SinGooCMS.WebIM
{
    public class CometResult : IAsyncResult
    {
        private bool _completed;
        private Object _state;
        private AsyncCallback _callback;
        private HttpContext _context;

        public HttpContext Context { get { return _context; } }
        bool IAsyncResult.IsCompleted { get { return _completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return _state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } } //设置true不输出

        public CometResult(HttpContext context, AsyncCallback callback, object extraData)
        {
            _context = context;
            _callback = callback;
            _state = extraData;
            _completed = false;
        }

        /*
        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }
        
        private void StartAsyncTask(Object workItemState)
        {    
            //读取信息,如果有自己的信息则回调
            //消息接受者类型
            string strReciverType = _context.Request.QueryString["rtype"];
            //消息接受者ID
            int intReciverID = _context.Request.QueryString["rTID"] == null ? 0 : int.Parse(_context.Request.QueryString["rTID"].ToString());

            List<IMMessage> listMsg = (List<IMMessage>)IMMessage.GetMessageListBySender(strReciverType, intReciverID);
            //如果有给对方的消息则推送
            if (listMsg != null && listMsg.Count > 0)
            {
                List<ShortMessage> shortmsgs = new List<ShortMessage>();
                foreach (IMMessage item in listMsg)
                {
                    ShortMessage shortmsg = new ShortMessage()
                    {
                        MsgID = item.AutoID,
                        SenderID = item.SenderID,
                        SenderName = item.Sender,
                        SendMsg = item.Msg,
                        DateStr = item.SendTime.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    shortmsgs.Add(shortmsg);
                }

                //输出消息并返回            
                _context.Response.Write(SinGooCMS.Utility.JsonUtils.ObjectToJson<List<ShortMessage>>(shortmsgs));
                _completed = true;
                _callback(this);
            }
        }
         * */

        public void Send(string key)
        {
            //消息接受者类型
            string strReciverType = key.IndexOf("-") == -1 ? string.Empty : key.Split('-')[0];
            //消息接受者ID
            int intReciverID = key.IndexOf("-") == -1 ? 0 : int.Parse(key.Split('-')[1]);

            List<IMMessage> listMsg = (List<IMMessage>)IMMessage.GetMessageListByReciver(strReciverType, intReciverID);
            List<ShortMessage> shortmsgs = new List<ShortMessage>();
            foreach (IMMessage item in listMsg)
            {
                ShortMessage shortmsg = new ShortMessage()
                {
                    MsgID = item.AutoID,
                    SenderID = item.SenderID,
                    SenderName = item.Sender,
                    SendMsg = item.Msg,
                    DateStr = item.SendTime.ToString("yyyy-MM-dd HH:mm:ss")
                };
                shortmsgs.Add(shortmsg);
            }

            //输出消息并返回            
            _context.Response.Write(SinGooCMS.Utility.JsonUtils.ObjectToJson<List<ShortMessage>>(shortmsgs));
            _completed = true;
            _callback(this);
        }
    }
}
