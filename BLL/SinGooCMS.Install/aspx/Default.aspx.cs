using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinGooCMS.Extensions;

namespace SinGooCMS.WebUI.Install
{
    public partial class Default : Page
    {
        #region 组件
        /// <summary>
        /// form1 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.HtmlControls.HtmlForm form1;

        /// <summary>
        /// btn_ok 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.Button btn_ok;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_ok_Click(object sender, EventArgs e)
        {
            if (SinGooCMS.Install.InstallUtil.IsInstalled())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('软件已安装！如需要重新安装请删除/Config/data.config文件')</script>"); //存在安装
            else
                Response.Redirect("Step2.aspx");
        }
    }
}