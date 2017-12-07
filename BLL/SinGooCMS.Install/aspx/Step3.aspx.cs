using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SinGooCMS.DAL;
using SinGooCMS.Install;

namespace SinGooCMS.WebUI.Install
{
    public partial class Step3 : Page
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
        /// TextBox1 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TextBox1;

        /// <summary>
        /// TextBox2 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TextBox2;

        /// <summary>
        /// TextBox3 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TextBox3;

        /// <summary>
        /// TextBox4 控件。
        /// </summary>
        /// <remarks>
        /// 自动生成的字段。
        /// 若要进行修改，请将字段声明从设计器文件移到代码隐藏文件。
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TextBox4;

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
            InstallSetting set = new InstallSetting();
            set.DB_Server = TextBox1.Text;
            set.DB_Name = TextBox2.Text;
            set.DB_Uid = TextBox3.Text;
            set.DB_Pwd = TextBox4.Text;

            //AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", "server=" + set.DB_Server + ";database=" + set.DB_Name + ";uid=" + set.DB_Uid + ";pwd=" + set.DB_Pwd + ";");
            try
            {
                //序列化设置
                SinGooCMS.Install.InstallUtil.EditDBConfig(set.DB_Server, set.DB_Name, set.DB_Uid, set.DB_Pwd);
                System.IO.File.WriteAllText(Server.MapPath("/Install/setting.xml"), SinGooCMS.Utility.XmlSerializerUtils.Serialize<InstallSetting>(set));
                Response.Redirect("Step4.aspx");
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('数据库配置无效，无法连接到数据库')</script>");
            }
        }
    }
}