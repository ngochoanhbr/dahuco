using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using SinGooCMS.DAL;
using SinGooCMS.Install;
using SinGooCMS.Extensions;

namespace SinGooCMS.WebUI.Install
{
    public partial class Step4 : Page
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
            InstallSetting set = SinGooCMS.Utility.XmlSerializerUtils.Deserialize<InstallSetting>(System.IO.File.ReadAllText(Server.MapPath("/Install/setting.xml")));

            if (set.DB_Server.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('数据库服务器地址不为空')</script>");
            else if (set.DB_Name.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('数据库名称不为空')</script>");
            else if (set.DB_Uid.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('数据库登录名称不为空')</script>");
            else if (set.DB_Pwd.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('数据库登录密码不为空')</script>");
            else if (TextBox1.Text.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('请输入超级管理员密码')</script>");
            else if (TextBox1.Text.Trim().Length<6)
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('超级管理员密码不能少于6位')</script>");
            else if (TextBox2.Text.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('请输入管理员账户')</script>");
            else if (TextBox3.Text.IsNullOrEmpty())
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('请输入管理员密码')</script>");
            else if (TextBox3.Text.Trim().Length<6)
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('管理员密码不能少于6位')</script>");
            else
            {
                set.SuperManager = "superadmin";
                set.SuperManagerPwd = SinGooCMS.Utility.DEncryptUtils.SHA512Encrypt(TextBox1.Text);
                set.ManagerName = TextBox2.Text;
                set.ManagerPwd = SinGooCMS.Utility.DEncryptUtils.SHA512Encrypt(TextBox3.Text);

                //安装数据表
                //在setp1.sql最前插入 use [database] GO
                string strStep1 = File.ReadAllText(Server.MapPath("/Install/script/setp1.sql"), Encoding.UTF8);
                strStep1 = "use " + set.DB_Name + " GO " + strStep1;
                strStep1 = Regex.Replace(strStep1, @"([\r\n])[\s]+", " ", RegexOptions.IgnoreCase);
                SinGooCMS.Install.InstallUtil.CreateTable(strStep1);
                //安装存储过程等可编程数据
                //在step2.sql最前插入 use [database] GO
                string strStep2 = File.ReadAllText(Server.MapPath("/Install/script/setp2.sql"), Encoding.UTF8);
                strStep2 = "use " + set.DB_Name + " GO " + strStep2;
                strStep2 = Regex.Replace(strStep2, @"([\r\n])[\s]+", " ", RegexOptions.IgnoreCase);
                SinGooCMS.Install.InstallUtil.CreateStorePocedure(strStep2);
                //初始化基本数据
                //在step3.sql最前插入 use [database] GO
                string strManagerSource = " SET IDENTITY_INSERT [dbo].[cms_Account] ON "
                                          + " INSERT [dbo].[cms_Account] ([AutoID], [AccountName], [Password], [Email], [Mobile], [IsSystem], [LoginCount], [LastLoginIP], [LastLoginArea], [LastLoginTime], [AutoTimeStamp]) VALUES (1, N'superadmin', N'" + set.SuperManagerPwd + "', N'liqiang665@163.com', N'13760195274', 1, 0, N'127.0.0.1', N'本机地址', CAST(0x0000A3D900F4F8F7 AS DateTime), CAST(0x0000A09E0118CE63 AS DateTime)) "
                                          + " INSERT [dbo].[cms_Account] ([AutoID], [AccountName], [Password], [Email], [Mobile], [IsSystem], [LoginCount], [LastLoginIP], [LastLoginArea], [LastLoginTime], [AutoTimeStamp]) VALUES (2, N'admin', N'" + set.ManagerPwd + "', N'您的电子邮箱', N'您的手机号码', 1, 0, N'127.0.0.1', N'本机地址', CAST(0x0000A3D900F0A9D4 AS DateTime), CAST(0x0000A3480135DEE4 AS DateTime)) "
                                          + " SET IDENTITY_INSERT [dbo].[cms_Account] OFF GO ";
                string strStep3 = File.ReadAllText(Server.MapPath("/Install/script/setp3.sql"), Encoding.UTF8);
                strStep3 = "use " + set.DB_Name + " GO " + strManagerSource + " " + strStep3;
                strStep3 = Regex.Replace(strStep3, @"([\r\n])[\s]+", " ", RegexOptions.IgnoreCase);
                SinGooCMS.Install.InstallUtil.InitialData(strStep3);

                System.IO.File.WriteAllText(Server.MapPath("/Install/setting.xml"), SinGooCMS.Utility.XmlSerializerUtils.Serialize<InstallSetting>(set));
                Response.Redirect("Success.aspx");
            }
        }
    }
}