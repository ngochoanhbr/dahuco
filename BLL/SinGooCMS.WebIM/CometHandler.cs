using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Text;

using SinGooCMS.Utility;

namespace SinGooCMS.WebIM
{
    public class CometHandler : IHttpAsyncHandler
    {
        public bool IsReusable { get { return false; } }

        public CometHandler()
        {

        }
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        {
            //消息接受者类型
            string strReciverType = context.Request.QueryString["recivertype"];
            //消息接受者ID
            int intReciverID = context.Request.QueryString["reciverid"] == null ? 0 : int.Parse(context.Request.QueryString["reciverid"].ToString());
            string currKey = strReciverType + "-" + intReciverID.ToString();

            CometResult asynch = new CometResult(context, cb, extraData);
            CometMgr.Instance().Clients.Add(asynch);
            return asynch;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            CometMgr.Instance().Clients.Remove((CometResult)result);//从列表中删除
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }
    }
}
