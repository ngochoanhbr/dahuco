using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SinGooCMS.Upgrade;

namespace SinGooCMS.WebUI.Upgrade
{
    public partial class Step2 : System.Web.UI.Page
    {
        #region 组件
        /// <summary>
        /// Head1 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlHead Head1;

        /// <summary>
        /// form1 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlForm form1;

        /// <summary>
        /// Repeater1 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.Repeater Repeater1;

        /// <summary>
        /// showlastinfo 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.Literal showlastinfo;

        /// <summary>
        /// lblcurrver 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.Literal lblcurrver;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SinGooCMS.Utility.CacheUtils.Del(CacheKey.CKEY_VER);
            lblcurrver.Text = SinGooCMS.BLL.Ver.GetVer().Ver;

            List<ServVerInfo> list = SinGooCMS.Upgrade.UpgradeUtil.CheckServVer();
            if (list != null && list.Count > 0)
            {
                Repeater1.DataSource = list;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.Visible = false;
                showlastinfo.Visible = true; //显示当前为最新版本，无需更新
            }
        }
    }
}