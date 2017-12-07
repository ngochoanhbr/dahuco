using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SinGooCMS.Plugin.SMS
{
    /*
     * 功能说明
    SMS短信通API下行接口参数 即发送短信接口
    GBK编码发送接口地址：
    http://gbk.sms.webchinese.cn/?Uid=本站用户名&Key=接口安全密码&smsMob=手机号码&smsText=短信内容 
    UTF-8编码发送接口地址：
    http://utf8.sms.webchinese.cn/?Uid=本站用户名&Key=接口安全密码&smsMob=手机号码&smsText=短信内容
    获取短信数量接口地址(UTF8)：
    http://sms.webchinese.cn/web_api/SMS/?Action=SMS_Num&Uid=本站用户名&Key=接口安全密码
    获取短信数量接口地址(GBK)：
    http://sms.webchinese.cn/web_api/SMS/GBK/?Action=SMS_Num&Uid=本站用户名&Key=接口安全密码
      
    * 提示:HTTP调用URL接口时, 参数值必须URL编码后再调用

    参数变量 说明 
    Gbk编码Url http://gbk.sms.webchinese.cn/ 
    Utf-8编码Url http://utf8.sms.webchinese.cn/ 
    Uid 本站用户名（如您无本站用户名请先注册）[免费注册] 
    Key 注册时填写的接口安全密码（可到用户平台修改安全密码）[立刻修改] 
    smsMob 目的手机号码（多个手机号请用半角逗号隔开） 
    smsText 短信内容，最多支持300个字，普通短信70个字/条，长短信64个字/条计费 

    多个手机号请用半角,隔开
    如：13888888886,13888888887,1388888888 一次最多对100个手机发送
    短信内容支持长短信，最多300个字，普通短信66个字/条，长短信64个字/条计费

    短信发送后返回值 说　明 
    -1  没有该用户账户 
    -2 密钥不正确 [查看密钥] 
    -3 短信数量不足 
    -11 该用户被禁用 
    -14 短信内容出现非法字符 
    -4 手机号格式不正确 
    -41 手机号码为空 
    -42 短信内容为空 
    -51 短信签名格式不正确 接口签名格式为：【签名内容】 
    大于0 短信发送数量 
 
    注：调用API接口，请登录平台，申请106网关发送，即发即到！
　　    发送测试短信请勿输入：短信测试等词语，请直接提交您要发送的短信内容；
　　    接口发送短信时请在内容后加签名：【XX公司或XX网名称】，否者会被屏蔽。
　　    短信签名可在用户平台平台上设置，也可以在短信内容后，直接加入。 
     * 
     * 使用范例
     * WebChineseSMS.GetHtmlFromUrl("http://utf8.sms.webchinese.cn/?Uid=本站用户名&Key=接口安全密码&smsMob=手机号码&smsText=短信内容")
     */
    public class WebChineseSMS : ISMS
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_strMobile"></param>
        /// <param name="_strSMSContent"></param>
        public WebChineseSMS(string _strMobile, string _strSMSContent)
        {
            SMSUrl = Config.ConfigProvider.Configs.SMSUrl;
            SMSUid = Config.ConfigProvider.Configs.SMSUid;
            SMSPwd = Config.ConfigProvider.Configs.SMSPwd;

            Mobile = _strMobile;
            SMSContent = _strSMSContent;
        }

        public WebChineseSMS()
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
        /// 发送手机短信
        /// </summary>
        /// <returns></returns>
        public string SendMsg()
        {
            return SendMsg(this.Mobile, this.SMSContent);
        }
        public string SendMsg(string strMobile, string strContent)
        {
            //短信内容(内容不超过66个中文字)
            string strUrl = "http://utf8.sms.webchinese.cn/?Uid=" + SMSUid + "&Key=" + SMSPwd + "&smsMob=" + strMobile + "&smsText=" + strContent;
            string strResult = GetHtmlFromUrl(strUrl);
            if (SinGooCMS.Utility.WebUtils.GetInt(strResult) > 0) 
                _issuccess = true; //发送成功

            return strResult;
        }

        private string GetHtmlFromUrl(string url)
        {
            string strRet = null;
            if (url == null || url.Trim().ToString() == "")
            {
                return strRet;
            }
            string targeturl = url.Trim().ToString();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                strRet = ser.ReadToEnd();
            }
            catch
            {
                strRet = null;
            }
            return strRet;
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
