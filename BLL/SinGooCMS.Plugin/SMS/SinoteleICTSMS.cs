using System;

using SinGooCMS.Utility;
/*
 * 华铁广通 短信接口
 * 接口Webservice地址：http://222.35.90.25/SMS_ICT_WBS/SMSService.asmx
 * 简易短信发送接口
    int toSendSMS(string EnteCode, string SMSPwd, string Mobile, string ShortMsg, out string FilterWord)
    参数：
    EnteCode：企业代码(不能为空)
    SMSpwd：接口密码(不能为空)
    Mobile：手机号(不能为空)
    ShortMsg：短信内容(不能为空)
    FilterWord：敏感词(如果短信内容中存在敏感词，将会由此参数返回，方法返回值为-2时，此参数值为短信内容中包含的敏感词。)
    返回值：
    -6：短信发送成功 
    -7：企业代码或者接口密码错误
    -4：企业短信余额不足
    -3：企业管理员不存在（短信都是以企业管理员的身份发送的，如果返回参数为该值，请咨询我公司）
    -2：敏感词（短信内容中含有敏感词，敏感词为参数FilterWord的值）
    -11：目标手机号码为黑名单用户（如果启用了黑名单功能才可能会有此返回值）
    -12：目标手机号码为非白名单用户（如果启用了白名单功能才可能会有此返回值）
 */
namespace SinGooCMS.Plugin.SMS
{
    /// <summary>
    /// 华铁广通 短信接口
    /// 登录入口 http://www.400ict.com/sms/login.aspx
    /// </summary>
    public class SinoteleICTSMS : ISMS
    {
        public SinoteleICTSMS(string _strMobile, string _strSMSContent)
        {
            SMSUrl = Config.ConfigProvider.Configs.SMSUrl;
            SMSUid = Config.ConfigProvider.Configs.SMSUid;
            SMSPwd = Config.ConfigProvider.Configs.SMSPwd;

            Mobile = _strMobile;
            SMSContent = _strSMSContent;
        }
        public SinoteleICTSMS()
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

        public string SendMsg()
        {
            return SendMsg(this.Mobile, this.SMSContent);   
        }
        public string SendMsg(string strMobile, string strContent)
        {
            //int toSendSMS(string EnteCode, string SMSPwd, string Mobile, string ShortMsg, out string FilterWord)
            string[] args = new string[5];
            args[0] = this.SMSUid;
            args[1] = this.SMSPwd;
            args[2] = strMobile;
            args[3] = strContent;
            args[4] = "out FilterWord";
            object objTemp = WebServiceUtils.InvokeWebService(this.SMSUrl, "toSendSMS", args);
            if (objTemp != null && objTemp.ToString() == "-6") //发送成功
                _issuccess = true;

            return objTemp.ToString();
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
