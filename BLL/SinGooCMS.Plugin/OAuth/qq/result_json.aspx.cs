using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using LitJson;
using SinGooCMS.Plugin.OAuth;
using SinGooCMS.BLL;
using SinGooCMS.Entity;
using SinGooCMS.Utility;

namespace SinGooCMS.Plugin.OAuth.qq
{
    public partial class result_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string oauth_access_token = string.Empty;
            string oauth_openid = string.Empty;
            string oauth_name = string.Empty;

            if (Session["oauth_name"] == null || Session["oauth_access_token"] == null || Session["oauth_openid"] == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，Access Token已过期或不存在！\"}");
                return;
            }
            oauth_name = Session["oauth_name"].ToString();
            oauth_access_token = Session["oauth_access_token"].ToString();
            oauth_openid = Session["oauth_openid"].ToString();
            Dictionary<string, object> jd = QQAuth.get_user_info(oauth_access_token, oauth_openid);
            if (jd == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            try
            {
                if (jd["ret"].ToString() != "0")
                {
                    Response.Write("{\"ret\":\"" + jd["ret"].ToString() + "\", \"msg\":\"出错信息:" + jd["msg"].ToString() + "！\"}");
                    return;
                }

                #region 弃用
                /*
                StringBuilder str = new StringBuilder();
                str.Append("{");
                str.Append("\"ret\": \"" + jd["ret"].ToString() + "\", ");
                str.Append("\"msg\": \"" + jd["msg"].ToString() + "\", ");
                str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
                str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
                str.Append("\"oauth_openid\": \"" + oauth_openid + "\", ");
                str.Append("\"nick\": \"" + jd["nickname"].ToString() + "\", ");
                str.Append("\"avatar\": \"" + jd["figureurl_qq_2"].ToString() + "\", ");
                str.Append("\"sex\": \"" + jd["gender"].ToString() + "\", ");
                str.Append("\"birthday\": \"\"");
                str.Append("}");
                Response.Write(str.ToString());
                 */
                #endregion

                #region 保存并登录会员

                //用户名称
                string strUserName = jd["nickname"].ToString();
                //个人会员 企业会员
                var uGroup = BLL.UserGroup.GetCacheUserGroupList().Where(p => p.GroupName.Equals("个人会员"));
                //会员等级
                var uLevel = BLL.UserLevel.GetCacheUserLevelList().Where(p => p.LevelName.Equals("初级会员"));
                UserInfo user = new UserInfo()
                {
                    UserName = strUserName,
                    Password = BLL.User.GetEncodePwd("123456"),
                    GroupID = uGroup.Count() > 0 ? uGroup.First().AutoID : 0,
                    LevelID = uLevel.Count() > 0 ? uLevel.First().AutoID : 0,
                    Email = StringUtils.GetRandomNumber() + "@qq.com",
                    Mobile = string.Empty,
                    Status = (int)UserStatus.Success, //默认为审核状态
                    AutoTimeStamp = System.DateTime.Now
                };
                IList<UserFieldInfo> listField = BLL.UserField.GetFieldListByModelID(user.GroupID, true);
                Dictionary<string, UserFieldInfo> fieldDicWithValues = new Dictionary<string, UserFieldInfo>();
                if (!BLL.User.IsExistsByName(strUserName))
                {
                    //不存在则注册会员
                    BLL.User.Reg(user, fieldDicWithValues);
                }

                //会员登录
                UserInfo userLogin = new UserInfo();
                LoginStatus status = BLL.User.UserLogin(strUserName, user.Password, ref userLogin);
                if (status == LoginStatus.Success)
                {
                    Response.Redirect("/User/InfoCenter.aspx"); //返回到会员中心
                }

                #endregion
            }
            catch
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
            }
            return;
        }
    }
}