using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SinGooCMS.Plugin.OAuth;

namespace SinGooCMS.Plugin.OAuth.qq
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获得配置信息
            OAuthConfig config = OAuthConfig.GetOAuthConfig("qq");
            if (config == null)
            {
                Response.Write("出错了，您尚未配置QQ互联的API信息！");
                return;
            }
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["oauth_state"] = state;
            string send_url = "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=" + config.OAuthAppId + "&state=" + state + "&redirect_uri=" + Server.UrlEncode(config.ReturnUri) + "&scope=get_user_info,get_info";
            //开始发送
            Response.Redirect(send_url);
        }
    }
}