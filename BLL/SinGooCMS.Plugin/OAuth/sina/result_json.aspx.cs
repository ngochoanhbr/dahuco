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

namespace SinGooCMS.Plugin.OAuth.sina
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
            JsonData jd =SinaAuth.get_info(oauth_access_token, oauth_openid);
            if (jd == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }

            #region 弃用
            /*
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"获得用户信息成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + jd["id"].ToString() + "\", ");
            str.Append("\"nick\": \"" + jd["screen_name"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + jd["profile_image_url"].ToString() + "\", ");

            if (jd["gender"].ToString() == "m")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (jd["gender"].ToString() == "f")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"\"");
            str.Append("}");

            Response.Write(str.ToString());
            */
            #endregion

            #region 保存并登录会员

            //用户名称
            string strUserName = jd["screen_name"].ToString();
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
    }
}