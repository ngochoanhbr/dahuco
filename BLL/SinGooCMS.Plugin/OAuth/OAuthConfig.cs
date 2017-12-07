using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Plugin.OAuth
{
    [Serializable]
    public class OAuthConfig
    {
        /// <summary>
        /// 第三方登录认证名称
        /// </summary>
        public string OAuthName
        {
            get;
            set;
        }

        /// <summary>
        /// APP ID
        /// </summary>
        public string OAuthAppId
        {
            get;
            set;
        }

        /// <summary>
        /// APP KEY
        /// </summary>
        public string OAuthAppKey
        {
            get;
            set;
        }

        /// <summary>
        /// 回传的URL
        /// </summary>
        public string ReturnUri
        {
            get;
            set;
        }

        #region 第三方登录

        public static List<OAuthConfig> LoadOAuthConfig()
        {
            try
            {
                string xmlString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("/Config/oauth.config"));
                return SinGooCMS.Utility.XmlSerializerUtils.Deserialize<List<OAuthConfig>>(xmlString);
            }
            catch
            {
                return new List<OAuthConfig>();
            }
        }

        /// <summary>
        /// 获取第三方登录认证配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static OAuthConfig GetOAuthConfig(string name)
        {
            return LoadOAuthConfig().Where(p => p.OAuthName == name).FirstOrDefault();
        }

        #endregion
    }
}
