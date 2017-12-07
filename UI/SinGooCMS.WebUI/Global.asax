<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Security.Principal" %>
<script RunAt="server">

    private System.Threading.Timer timeAuto;
    void Application_AcquireRequestState(object sender, EventArgs e)
    {
        //
    }

    void Application_Start(object sender, EventArgs e)
    {
        //定时器 间隔60秒 随应用程序启动            
        //timeAuto = new System.Threading.Timer(new System.Threading.TimerCallback(CheckUserOnLine), this, 0, 60 * 1000); 
    }
    private void CheckUserOnLine(object obj)
    {
        //SinGooCMS.WebIM.OnlineUser.CheckOnlineUser();
    }
    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码
    }

    void Application_Error(object sender, EventArgs e)
    {
        //错误日志记录
        Exception ex = Server.GetLastError();
        int code = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 200;
        if (code != 404) //文件不存在 的错误不提醒 交给404处理
        {
            SinGooCMS.BLL.LogManager log = new SinGooCMS.BLL.LogManager();
            log.AddErrLog(ex.GetBaseException().Message, ex.GetBaseException().StackTrace);
            Server.ClearError();
            this.Response.Redirect("~/Include/Error/ErrorMsg.htm?msg=" + Server.UrlEncode(ex.GetBaseException().Message));
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        //在新会话启动时运行的代码
        /*
        Application.Lock();
        Application["count"] = Convert.ToInt32(Application["count"]) + 1;
        File.WriteAllText(Server.MapPath("~/Include/counter.txt"), Application["count"].ToString());
        Application["countnow"] = Convert.ToInt32(Application["countnow"]) + 1;
        Application.UnLock();
         */
    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。
        /*
        Application.Lock();
        Application["countnow"] = Convert.ToInt32(Application["countnow"]) - 1;
        Application.UnLock();
         * */
    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        //Application["start"] = DateTime.Now; //开始加载

        /* we guess at this point session is not already retrieved by application so we recreate cookie with the session id... */
        try
        {
            string session_param_name = "ASPSESSID";
            string session_cookie_name = "ASP.NET_SessionId";

            if (HttpContext.Current.Request.Form[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
            }
        }
        catch
        {
            //
        }

        try
        {
            string auth_param_name = "AUTHID";
            string auth_cookie_name = FormsAuthentication.FormsCookieName;

            if (HttpContext.Current.Request.Form[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
            }

        }
        catch
        {
            //
        }
    }

    private void UpdateCookie(string cookie_name, string cookie_value)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        if (null == cookie)
        {
            cookie = new HttpCookie(cookie_name);
        }
        cookie.Value = cookie_value;
        HttpContext.Current.Request.Cookies.Set(cookie);
    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {
        /*
        System.TimeSpan LoadTime = DateTime.Now - (System.DateTime)Application["start"];
        Application["PageLoadTime"] = LoadTime.TotalSeconds.ToString("#00.000");
         * */
    }
    /// <summary>
    /// 认证请求,manager角色进入后台管理,membership会员进入会员中心
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        //
    }
       
</script>
