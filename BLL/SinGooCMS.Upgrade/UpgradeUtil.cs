using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using Ionic.Zip;
using SinGooCMS.Entity;
using SinGooCMS.DAL;
using SinGooCMS.BLL;
using SinGooCMS.Utility;
using SinGooCMS.Extensions;

namespace SinGooCMS.Upgrade
{
    /// <summary>
    /// 软件升级类
    /// </summary>
    public class UpgradeUtil
    {
        static AbstrctFactory dbo = DataFactory.CreateDataFactory("mssql", null);

        /// <summary>
        /// 开始启动更新
        /// </summary>
        /// <returns></returns>
        public static string Process(int intVerCode,string DBName)
        {
            StringBuilder builder = new StringBuilder();
            string messageTemplate = "{{'result':'{0}','msg':'{1}'}},";

            //当前版号本
            int intCurrVer = Int32.Parse(SinGooCMS.BLL.Ver.GetVer().Ver.Replace(".", ""));

            //对比版本号 如果不是最新版本则需要更新
            List<ServVerInfo> listServVer = new List<ServVerInfo>();
            string strServVerInfo = SinGooCMS.Utility.NetWorkUtils.HttpGet(ConfigUtils.GetAppSetting<string>("RegServEntrance")); //远程版本信息
            listServVer = SinGooCMS.Utility.XmlSerializerUtils.Deserialize<List<ServVerInfo>>(strServVerInfo);
            if (listServVer != null && listServVer.Count > 0)
            {
                //排序 顺序
                listServVer = listServVer.OrderBy(p => p.VerCode).ToList();
                //遍历更新
                WebClient client = new WebClient();
                foreach (ServVerInfo item in listServVer)
                {
                    if (intCurrVer < item.VerCode && item.VerCode <= intVerCode)
                    {
                        #region 下载文件包并解压更新

                        string strExt = Path.GetExtension(item.ZipFile);
                        if (strExt.IsNullOrEmpty())
                            strExt = "zip";

                        //下载更新包文件                        
                        string strRootPath = System.Web.HttpContext.Current.Server.MapPath("/Upgrade/Temp/" + item.VerCode.ToString()); //存放更新包的文件夹                     
                        if (!System.IO.Directory.Exists(strRootPath))
                            FileUtils.CreateDirectory(strRootPath);

                        string strRemotePackage = item.ZipFile; //远程文件如 http://www.xxx.com/Upgrade/Package/v110.zip 
                        string strLocalPackage = strRootPath + "\\" + item.VerCode + strExt; //本地文件包
                        client.DownloadFile(strRemotePackage, strLocalPackage);

                        //解压更新包到Temp文件夹 并覆盖
                        ZipFile zip = ZipFile.Read(strLocalPackage);
                        zip.ExtractAll(strRootPath, ExtractExistingFileAction.OverwriteSilently);

                        //更新文件
                        string strData = File.ReadAllText(strRootPath + "\\files.xml");
                        List<FileData> listData = SinGooCMS.Utility.XmlSerializerUtils.Deserialize<List<FileData>>(strData);
                        if (listData != null && listData.Count > 0)
                        {
                            foreach (FileData itemFile in listData)
                            {
                                try
                                {
                                    if (itemFile.Type.Equals("File"))
                                    {
                                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("/" + Path.GetDirectoryName(itemFile.FileName))))
                                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("/" + Path.GetDirectoryName(itemFile.FileName)));

                                        //拷贝并覆盖此文件
                                        File.Copy(strRootPath + "\\" + itemFile.FileName, System.Web.HttpContext.Current.Server.MapPath("/" + itemFile.FileName), true);
                                        builder.AppendFormat(messageTemplate, "true", "更新文件/" + itemFile.FileName.Replace("\\\\", "/").Replace("\\", "/") + "成功");
                                    }
                                    else if (itemFile.Type.Equals("Folder"))
                                    {
                                        //拷贝并覆盖此文件夹
                                        SinGooCMS.Utility.FileUtils.CopyDirectory(strRootPath + "\\" + itemFile.FileName, System.Web.HttpContext.Current.Server.MapPath("/" + itemFile.FileName));
                                        builder.AppendFormat(messageTemplate, "true", "更新文件夹/" + itemFile.FileName.Replace("\\\\", "/").Replace("\\", "/") + "成功");
                                    }
                                }
                                catch
                                {
                                    builder.AppendFormat(messageTemplate, "false", "更新文件" + itemFile.FileName + "失败");
                                }
                            }
                        }

                        //更新数据库脚本
                        if (File.Exists(strRootPath + "\\script.sql"))
                        {
                            try
                            {
                                string strScript = File.ReadAllText(strRootPath + "\\script.sql", Encoding.UTF8);
                                strScript = "use " + DBName + " GO " + strScript;
                                //去掉换行等
                                strScript = Regex.Replace(strScript, @"([\r\n])[\s]+", " ", RegexOptions.IgnoreCase);
                                dbo.ExecSQLWithSplit(strScript, "GO");
                                builder.AppendFormat(messageTemplate, "true", "更新数据库script.sql成功");
                            }
                            catch
                            {
                                builder.AppendFormat(messageTemplate, "false", "更新数据库script.sql成功");
                            }
                        }

                        #endregion
                    }
                }
            }
            else
            {
                builder.AppendFormat(messageTemplate, "false", "读取服务器版本信息失败");
            }

            return ("[" + builder.ToString().Trim(',') + "]");
        }

        /// <summary>
        /// 版本检测
        /// </summary>
        /// <returns></returns>
        public static List<ServVerInfo> CheckServVer()
        {
            List<ServVerInfo> listServVer = new List<ServVerInfo>();
            string strServVerInfo = SinGooCMS.Utility.NetWorkUtils.HttpGet(ConfigUtils.GetAppSetting<string>("RegServEntrance")); //远程版本信息
            listServVer = SinGooCMS.Utility.XmlSerializerUtils.Deserialize<List<ServVerInfo>>(strServVerInfo);
            if (listServVer != null && listServVer.Count > 0)
            {
                return listServVer.Where(p => p.VerCode > Int32.Parse(BLL.Ver.GetVer().Ver.Replace(".", ""))).ToList();
            }

            return null;
        }
    }
}
