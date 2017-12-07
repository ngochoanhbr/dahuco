using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.WebIM
{
    /// <summary>
    /// 长连接管理类
    /// </summary>
    public class CometMgr
    {
        private CometMgr()
        {
            //
        }
        private readonly static CometMgr _Instance = new CometMgr();
        public static CometMgr Instance()
        {
            return _Instance;
        }
        private List<CometResult> _clients = new List<CometResult>();
        public List<CometResult> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
            }
        }
        /// <summary>
        /// 通知回发
        /// </summary>
        public void Notify(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var clients = Clients.Where(
                    p => (p.Context.Request.Params["recivertype"] != null
                        && p.Context.Request.Params["reciverid"] != null
                        && (p.Context.Request.Params["recivertype"].ToString() + "-" + p.Context.Request.Params["reciverid"].ToString()) == key));
                foreach (CometResult item in clients)
                {
                    item.Send(key);
                }
            }
        }
    }
}
