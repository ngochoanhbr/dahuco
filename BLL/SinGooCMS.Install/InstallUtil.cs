using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;

using SinGooCMS.DAL;
using SinGooCMS.Utility;

namespace SinGooCMS.Install
{
    public class InstallUtil
    {
        private static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);
        public const string dbScriptPath = @"script\";//数据库脚本文件存放路径

        /// <summary>
        /// 返回系统环境检测结果
        /// </summary>
        /// <returns></returns>
        public static string InitialSystemValidCheck()
        {
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string messageTemplate = "{{'result':'{0}','msg':'{1}'}},";

            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            //检查是否支持.net 4.0
            /*
            if (bc != null && (bc.ClrVersion.Major.Equals(4) && bc.ClrVersion.Minor.Equals(0)))
                sb.AppendFormat(messageTemplate, "true", "支持.net Framework 4.0");
            else
                sb.AppendFormat(messageTemplate, "false", "不支持.net Framework 4.0，请安装或者升级.net Framework 4.0 (注意：此功能IE有效，请在IE浏览器中执行)");
            */
            if (Environment.Version.Major.Equals(4) && Environment.Version.Minor.Equals(0))
                sb.AppendFormat(messageTemplate, "true", "支持.net Framework 4.0");
            else
                sb.AppendFormat(messageTemplate, "false", "不支持.net Framework 4.0，请安装或者升级.net Framework 4.0 (注意：此功能IE有效，请在IE浏览器中执行)");
            //系统BIN目录检查
            sb.Append(IISSystemBINCheck());
            //检查系统目录的有效性
            string folderstr = "Config,Templates,Install,Upgrade,Html,Upload";
            foreach (string foldler in folderstr.Split(','))
            {
                if (!SystemFolderCheck(foldler))
                {
                    sb.AppendFormat(messageTemplate, "false", "对 " + foldler + " 目录没有写入和删除权限！");
                }
                else
                    sb.AppendFormat(messageTemplate, "true", "对 " + foldler + " 目录权限验证通过！");
            }
            string filestr = "/Config/data.config";
            foreach (string file in filestr.Split(','))
            {
                if (!SystemFileCheck(file))
                {
                    sb.AppendFormat(messageTemplate, "false", "对 " + file + " 文件没有写入和删除权限！");
                }
                else
                    sb.AppendFormat(messageTemplate, "true", "对 " + file + " 文件权限验证通过！");
            }

            if (!TempTest())
            {
                sb.AppendFormat(messageTemplate, "false", "您没有开启对 " + Path.GetTempPath() + " 文件夹访问权限，详情参见安装文档.");
            }

            return ("[" + sb.ToString().Trim(',') + "]").Replace("\\", "\\\\");
        }

        /// <summary>
        /// 检测目录是否有读写权限
        /// </summary>
        /// <returns></returns>
        public static bool SystemRootCheck()
        {
            HttpContext context = HttpContext.Current;

            string physicsPath = context != null ? context.Server.MapPath("/") : AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                using (FileStream fs = new FileStream(physicsPath + "\\a.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fs.Close();
                }
                System.IO.File.Delete(physicsPath + "\\a.txt");
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 返回bin文件的检测结果
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string IISSystemBINCheck()
        {
            string binfolderpath = HttpRuntime.BinDirectory;
            string messageTemplate = "{{'result':'{0}','msg':'{1}'}},";

            string result = "";
            try
            {
                string[] assemblylist = new string[] {"SinGooCMS.Core.dll", "SinGooCMS.Entity.dll", "SinGooCMS.DAL.dll", "SinGooCMS.BLL.dll",
                    "SinGooCMS.Config.dll","SinGooCMS.Control.dll","SinGooCMS.Common.dll","SinGooCMS.Utility.dll","SinGooCMS.Plugin.dll","SinGooCMS.Extensions.dll",
                    "SinGooCMS.WebUI.dll","SinGooCMS.BLL.Custom.dll","SinGooCMS.Upgrade.dll","SinGooCMS.PlatForm.dll","AspNetPager.dll","CKEditor.NET.dll",
                    "Intelligencia.UrlRewriter.dll","Ionic.Zip.dll","MyXls.SL2.dll","NVelocity.dll", "Newtonsoft.Json.Net35.dll" };
                bool isAssemblyInexistence = false;
                ArrayList inexistenceAssemblyList = new ArrayList();
                foreach (string assembly in assemblylist)
                {
                    if (!File.Exists(binfolderpath + assembly))
                    {
                        isAssemblyInexistence = true;
                        inexistenceAssemblyList.Add(assembly);
                    }
                }
                if (isAssemblyInexistence)
                {
                    foreach (string assembly in inexistenceAssemblyList)
                    {
                        result += string.Format(messageTemplate, "false", assembly + " 文件放置不正确,请将所有的dll文件复制到目录" + binfolderpath + " 中.");
                    }
                }
            }
            catch
            {
                result += string.Format(messageTemplate, "false", "请将所有的dll文件复制到目录 " + binfolderpath + " 中.");
            }
            return result;
        }

        /// <summary>
        /// 检测指定目录是否有读写权限
        /// </summary>
        /// <param name="foldername"></param>
        /// <returns></returns>
        public static bool SystemFolderCheck(string foldername)
        {
            string physicsPath = WebUtils.GetMapPath(@"..\" + foldername);
            try
            {
                using (FileStream fs = new FileStream(physicsPath + "\\a.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fs.Close();
                }
                if (File.Exists(physicsPath + "\\a.txt"))
                {
                    System.IO.File.Delete(physicsPath + "\\a.txt");
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检测是否有操作文件的权限
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool SystemFileCheck(string filename)
        {
            filename = WebUtils.GetMapPath(@"..\" + filename);
            try
            {
                if (filename.IndexOf("systemfile.aspx") == -1 && !File.Exists(filename))
                    return false;
                if (filename.IndexOf("systemfile.aspx") != -1)  //做删除测试
                {
                    File.Delete(filename);
                    return true;
                }
                StreamReader sr = new StreamReader(filename);
                string content = sr.ReadToEnd();
                sr.Close();
                content += " ";
                StreamWriter sw = new StreamWriter(filename, false);
                sw.Write(content);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 服务器temp目录权限检测
        /// </summary>
        /// <returns></returns>
        public static bool TempTest()
        {
            string UserGuid = Guid.NewGuid().ToString();
            string TempPath = Path.GetTempPath();
            string path = TempPath + UserGuid;
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(DateTime.Now);
                }

                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建表和相关索引，约束
        /// </summary>
        /// <returns></returns>
        public static string CreateTable(string strSQL)
        {
            dbo.ExecSQLWithSplit(strSQL, "GO");
            return "{result:true,message:1}";
        }

        /// <summary>
        /// 创建存储过程
        /// </summary>
        /// <returns></returns>
        public static string CreateStorePocedure(string strSQL)
        {
            dbo.ExecSQLWithSplit(strSQL, "GO");
            return "{result:true,message:1}";
        }

        /// <summary>
        /// 初始化起始数据
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="adminPassword"></param>
        /// <returns></returns>
        public static string InitialData(string strSQL)
        {
            try
            {
                dbo.ExecSQLWithSplit(strSQL, "GO");
                return "{result:true,message:\"数据初始化完毕\"}";
            }
            catch (Exception e)
            {
                return "{result:false,message:\"初始化过程出现错误(" + JsonCharFilter(e.Message) + ")\"}";
            }
        }

        /// <summary>
        /// 将用户填写的数据库信息写入data.config文件
        /// </summary>
        /// <param name="dataSource">数据库地址</param>
        /// <param name="userID">数据库账号</param>
        /// <param name="password">数据库账号密码</param>
        /// <param name="databaseName">数据库名</param>
        public static void EditDBConfig(string Server, string DBName, string Uid, string Pwd)
        {
            string strDataConfig = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><connectionStrings><add name=\"SQLConnSTR\" connectionString=\"server={0};database={1};uid={2};pwd={3};Application Name=SinGooCMS;pooling=true;min pool size=5;max pool size=512;connect timeout = 20\" /></connectionStrings>",
                Server, DBName, Uid, Pwd);
            System.IO.File.WriteAllText(WebUtils.GetMapPath("/Config/data.config"), strDataConfig, Encoding.UTF8);
        }

        public static SqlConnection connection = new SqlConnection();

        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "");
            sourceStr = sourceStr.Replace("\b", "");
            sourceStr = sourceStr.Replace("\t", "");
            sourceStr = sourceStr.Replace("\n", "");
            sourceStr = sourceStr.Replace("\f", "");
            sourceStr = sourceStr.Replace("\r", "");
            sourceStr = sourceStr.Replace("'", "\\'");
            return sourceStr.Replace("\"", "\\\"");
        }

        /// <summary>
        /// 测试用户填写的数据库信息是否正确
        /// </summary>
        /// <returns>false:数据库用户名或密码错误</returns>
        public static string CheckDBConnection(string sqlIp, string sqlUsername, string sqlPassword, string dbName)
        {
            string result = "";
            dbName = string.IsNullOrEmpty(dbName) ? "master" : dbName;
            try
            {
                connection.ConnectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                  sqlIp, sqlUsername, sqlPassword, dbName);
                connection.Open();
            }
            catch (SqlException e)
            {
                result = "{result:false,message:\"" + JsonCharFilter(e.Message) + "\",code:\"" + e.Number + "\"}";
            }
            finally
            {
                connection.Close();
            }
            return string.IsNullOrEmpty(result) ? "{result:true,message:\"连接成功\"}" : result.Replace("'", "\'");
        }

        public static string CheckDBCollation(string sqlIp, string sqlUsername, string sqlPassword, string dbName)
        {
            string result = "";
            try
            {
                string dbCollation = GetDBDefaultCollation(string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                      sqlIp, sqlUsername, sqlPassword, dbName));
                if (dbCollation.IndexOf("Chinese-PRC") < 0)
                    result = "{result:false,message:\"数据库排序规则不是简体中文,请调整为简体中文后重新运行安装程序\"}";
            }
            catch (SqlException e)
            {
                result = "{result:false,message:\"" + JsonCharFilter(e.Message) + "\",code:\"" + e.Number + "\"}";
            }

            return string.IsNullOrEmpty(result) ? "{result:true,message:\"字符集检测完毕\"}" : result.Replace("'", "\'");

        }

        /// <summary>
        /// 执行SQL语句，用来测试指定数据库是否存在
        /// </summary>
        /// <param name="commandText">t-sql</param>
        public static void ExcuteSQL(string commandText, string connectionString)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                connection.ConnectionString = connectionString;
                connection.Open();
                sqlCommand = new SqlCommand(commandText, connection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                sqlCommand.Connection.Close();
            }
        }

        public static string DBSourceExist(string sqlIp, string sqlUsername, string sqlPassword, string dbName, string tablePrefix)
        {
            string result = "";
            try
            {
                ExcuteSQL("SELECT COUNT(1) FROM [" + tablePrefix + "users]", string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                      sqlIp, sqlUsername, sqlPassword, dbName));
            }
            catch (SqlException e)
            {
                result = "{result:false,message:\"" + JsonCharFilter(e.Message) + "\",code:\"" + e.Number + "\"}";
            }

            return string.IsNullOrEmpty(result) ? "{result:true,message:\"数据库已存在\",code:0}" : result.Replace("'", "\'");
        }

        /// <summary>
        /// 检测数据库版本
        /// </summary>
        /// <param name="commandText">t-sql</param>
        /// <param name="connectionString">数据库连接串</param>
        public static string GetSqlVersion(string connectionString)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            connection.ConnectionString = connectionString;
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT @@VERSION", connection);
            string sqlVersion = sqlCommand.ExecuteScalar().ToString().Trim();
            sqlCommand.Connection.Close();
            return sqlVersion;
        }

        public static string GetDBDefaultCollation(string connectionString)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
            connection.ConnectionString = connectionString;
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("sp_helpsort", connection);
            string collation = sqlCommand.ExecuteScalar().ToString().Trim();
            sqlCommand.Connection.Close();
            return collation;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        public static string CreateDatabase(string sqlIp, string sqlManager, string sqlManagerPassword, string sqlName)
        {
            string connectionString = string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=false",
                                                  sqlIp, sqlManager, sqlManagerPassword, "master");
            string commandText = string.Format("CREATE DATABASE [{0}]", sqlName);
            try
            {
                ExcuteSQL(commandText, connectionString);//执行创建数据库的TSQL；
                return "{result:true,message:\"数据库创建成功\"}";
            }
            catch (SqlException e)
            {
                return "{result:false,message:\"" + JsonCharFilter(e.Message) + "\",code:\"" + e.Number + "\"}";
            }
        }

        /// <summary>
        /// 是否已安装
        /// </summary>
        /// <param name="strDBName"></param>
        /// <returns></returns>
        public static bool IsInstalled()
        {
            string strSQL = " IF ( object_id('sys_Ver') IS NOT NULL ) select 1 ELSE SELECT 0 ";
            using (SqlConnection cn = new SqlConnection(SinGooCMS.DAL.ConnectionSource.SQLConnectionString))
            {
                using (SqlCommand cm1 = new SqlCommand("USE " + SinGooCMS.DAL.ConnectionSource.Database, cn))
                {
                    try
                    {
                        cm1.ExecuteNonQuery();
                        using (SqlCommand cm2 = new SqlCommand(strSQL, cn))
                        {
                            object obj = cm2.ExecuteScalar();
                            if (obj != null && Int32.Parse(obj.ToString()) == 1)
                                return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return false;
        }

    }
}