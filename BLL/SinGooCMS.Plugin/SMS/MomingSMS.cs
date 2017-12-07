/*
功能:		莫名企信通HTTP接口 发送短信
修改日期:	2014-03-19
说明:		http://api.momingsms.com/?action=send&username=用户账号&password=MD5位32密码&phone=号码&content=内容
状态:
	100 发送成功
	101 验证失败
	102 短信不足
	103 操作失败
	104 非法字符
	105 内容过多
	106 号码过多
	107 频率过快
	108 号码内容空
	109 账号冻结
	110 禁止频繁单条发送
	111 系统暂定发送
	112 号码不正确
	120 系统升级
*/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;

namespace SinGooCMS.Plugin.SMS
{
    public class MomingSMS : ISMS
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strMobile"></param>
        /// <param name="_strSMSContent"></param>
        public MomingSMS(string _strMobile, string _strSMSContent)
        {
            SMSUrl = Config.ConfigProvider.Configs.SMSUrl;
            SMSUid = Config.ConfigProvider.Configs.SMSUid;
            SMSPwd = Config.ConfigProvider.Configs.SMSPwd;

            Mobile = _strMobile;
            SMSContent = _strSMSContent;
        }

        public MomingSMS()
        {
            SMSUrl = Config.ConfigProvider.Configs.SMSUrl;
            SMSUid = Config.ConfigProvider.Configs.SMSUid;
            SMSPwd = Config.ConfigProvider.Configs.SMSPwd;
        }

        public string SMSUrl
        {
            get;
            set;
        }
        public string SMSUid
        {
            get;
            set;
        }
        public string SMSPwd
        {
            get;
            set;
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get;
            set;
        }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent
        {
            get;
            set;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public string SendMsg()
        {
            return SendMsg(this.Mobile, this.SMSContent);
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="strMobile"></param>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public string SendMsg(string strMobile, string strContent)
        {
            StringBuilder sbTemp = new StringBuilder();
            string strPassword = this.SMSPwd;
            strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "MD5"); //密码进行MD5加密
            //POST 传值
            sbTemp.Append("action=send&username=" + this.SMSUid + "&password=" + strPassword + "&phone=" + strMobile + "&content=" + strContent);
            byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());

            string strResult = doPostRequest("http://api.momingsms.com/", bTemp);
            if (strResult == "100")
                _issuccess = true; //发送成功

            return strResult;
        }

        //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }
        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
        private bool _issuccess = false;
        public bool IsSuccess
        {
            get
            {
                return _issuccess;
            }
        }
    }
}
