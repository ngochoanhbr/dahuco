/*
1001 	AppKey错误
1002 	短信/语音内容中含有脏字
1003 	未填AppKey
1004 	REST API请求参数有误
1006 	没有权限
1007 	其他错误
1008 	下发短信超时
1009 	用户IP不在白名单中
1011 	REST API命令字错误
1012 	短信内容格式错误
1013 	下发短信频率限制
1014 	模版未审批
1015 	黑名单手机
1016 	错误的手机号格式
1017 	短信内容过长
1018 	语音验证码格式错误
1019 	sdkappid不存在 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.IO;

// newtonsoft json 模块请自行到 http://www.newtonsoft.com/json 下载
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SinGooCMS.Plugin.SMS
{
    public class QcloudSMS : ISMS
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strMobile"></param>
        /// <param name="_strSMSContent"></param>
        public QcloudSMS(string _strMobile, string _strSMSContent)
        {
            SMSUrl = Config.ConfigProvider.Configs.SMSUrl;
            SMSUid = Config.ConfigProvider.Configs.SMSUid;
            SMSPwd = Config.ConfigProvider.Configs.SMSPwd;

            Mobile = _strMobile;
            SMSContent = _strSMSContent;
        }

        public QcloudSMS()
        {
            SMSUrl = Config.ConfigProvider.Configs.SMSUrl;
            SMSUid = Config.ConfigProvider.Configs.SMSUid;
            SMSPwd = Config.ConfigProvider.Configs.SMSPwd;
        }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string SMSUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string SMSUid
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
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
        /// 发送手机短信
        /// </summary>
        /// <returns></returns>
        public string SendMsg()
        {
            return SendMsg(this.Mobile, this.SMSContent);
        }

        public string SendMsg(string strMobile, string strContent)
        {
            return SendMsg("86", strMobile, strContent);
        }

        public string SendMsg(string nationCode, string phoneNumber, string content)
        {
            string sig = stringMD5(SMSPwd + phoneNumber);

            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append("{");
            sbMsg.Append("\"tel\": {");
            sbMsg.Append("\"nationcode\": \"" + nationCode + "\",");
            sbMsg.Append("\"phone\": \"" + phoneNumber + "\"");
            sbMsg.Append("},");
            sbMsg.Append("\"type\": \"0\",");
            sbMsg.Append("\"msg\": \"" + content + "\",");
            sbMsg.Append("\"sig\": \"" + sig + "\",");
            sbMsg.Append(" \"extend\": \"\",");
            sbMsg.Append(" \"ext\": \"\"");
            sbMsg.Append("}");

            string msgString = sbMsg.ToString();

            string retString = string.Empty;
            try
            {
                // 发送 POST 请求
                Random rnd = new Random();
                int random = rnd.Next(1000000) % (900000) + 1000000;
                string wholeUrl = SMSUrl + "?sdkappid=" + SMSUid + "&random=" + random;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(wholeUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] requestData = Encoding.UTF8.GetBytes(msgString);
                request.ContentLength = requestData.Length; 
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestData, 0, requestData.Length);
                requestStream.Close();

                // 接收返回包
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                
                if (retString.Contains("\"result\": \"0\""))
                {
                    _issuccess = true;
                }
                
            }
            catch (Exception e)
            {
                WriteErrLog(e.ToString());
            }
            return retString;
        }

        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }

        private static string stringMD5(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] targetData = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return byteToHexStr(targetData);
        }

        // 将二进制的数值转换为 16 进制字符串，如 "abc" => "616263"
        private static string byteToHexStr(byte[] input)
        {
            string returnStr = "";
            if (input != null)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    returnStr += input[i].ToString("x2");
                }
            }
            return returnStr;
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
