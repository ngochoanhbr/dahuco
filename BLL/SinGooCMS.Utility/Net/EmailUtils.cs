/*
 * 1).net邮件发送
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace SinGooCMS.Utility
{
    public enum EmailType
    {
        text, html
    }

    public class EmailUtils
    {
        #region 私有成员
        //邮件紧急程度
        private int emailPriority = 1;
        //邮件类型
        private EmailType mailFormat = EmailType.text;
        //发件人邮箱地址
        private string fromMail;
        //收件人地址
        private string toMail;
        //邮件主题  
        private string subject;
        //邮件内容  
        private string body;
        //邮件编码   
        private Encoding mailEncoding = System.Text.Encoding.UTF8;
        //是否启用本地smtp服务器
        private int isLocalSmtp = 2;
        //smtp服务器
        private string smtpServer;
        private int smtpPort = 25;
        //是否需要验证
        private int isSmtpAuthenticate = 1;
        //是否需要SSL认证
        private bool isSSL = false;
        //用户名
        private string mailUserName;
        //密码
        private string mailPassWord;
        #endregion

        #region 公共属性
        /// <summary>
        /// 邮件紧急程度，0为低，1为普通，2为高
        /// </summary>
        public int EmailPriority
        {
            get { return emailPriority; }
            set { emailPriority = value; }

        }

        /// <summary>
        /// 邮件类型 text为纯文本型，html为Html编码型
        /// </summary>
        public EmailType MailFormat
        {
            get { return mailFormat; }
            set { mailFormat = value; }

        }

        /// <summary>
        /// 发件人邮箱地址
        /// </summary>
        public string FromMail
        {
            get { return fromMail; }
            set { fromMail = value; }

        }

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string ToMail
        {
            get { return toMail; }
            set { toMail = value; }

        }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set { subject = value; }

        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            get { return body; }
            set { body = value; }

        }

        /// <summary>
        /// 邮件编码格式
        /// </summary>
        public Encoding MailEncoding
        {
            get { return mailEncoding; }
            set { mailEncoding = value; }
        }

        /// <summary>
        /// 是否启用本地smtp服务器,1为启用本地，2为启用远程smtp，默认为2
        /// </summary>
        public int IsLocalSmtp
        {
            get { return isLocalSmtp; }
            set { isLocalSmtp = value; }
        }

        /// <summary>
        /// 远程stmp服务器名称
        /// </summary>
        public string SmtpServer
        {
            get { return smtpServer; }
            set { smtpServer = value; }
        }

        /// <summary>
        /// 远程stmp服务器端口号
        /// </summary>
        public int SmtpPort
        {
            get { return smtpPort; }
            set { smtpPort = value; }
        }

        /// <summary>
        /// 是否需要验证  0为不验证，1为BASIC验证，2为NTLM验证方式
        /// </summary>
        public int IsSmtpAuthenticate
        {
            get { return isSmtpAuthenticate; }
            set { isSmtpAuthenticate = value; }
        }

        /// <summary>
        /// 是否需要SSL认证
        /// </summary>
        public bool IsSSL
        {
            get { return isSSL; }
            set { isSSL = value; }
        }

        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public string MailUserName
        {
            get { return mailUserName; }
            set { mailUserName = value; }

        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string MailPassWord
        {
            get { return mailPassWord; }
            set { mailPassWord = value; }
        }

        //20140915增加发件人的显示名称
        public string FromDisplayName
        {
            get;
            set;
        }
        public string ToDisplayName
        {
            get;
            set;
        }

        #endregion

        public bool SendEmail()
        {
            string strResult = string.Empty;
            return SendEmail(out strResult);
        }
        /// <summary>
        /// 发送邮件,strResult为返回结果信息
        /// </summary>
        /// <param name="strResult"></param>
        /// <returns></returns>
        public bool SendEmail(out string strResult)
        {
            bool boolReturn = false;

            try
            {
                if (!ValidateUtils.IsEmail(FromMail))
                    strResult = "发送方邮件格式不正确";
                else if (string.IsNullOrEmpty(MailUserName))
                    strResult = "没有设置邮件发送方用户名称";
                else if (string.IsNullOrEmpty(mailPassWord))
                    strResult = "没有设置邮件发送用户密码";
                else if (!ValidateUtils.IsEmail(ToMail))
                    strResult = "接收方邮件格式不正确";
                else
                {
                    #region 发送邮件

                    MailAddress addrFrom = new MailAddress(FromMail);
                    if (!string.IsNullOrEmpty(FromDisplayName))
                        addrFrom = new MailAddress(FromMail, FromDisplayName);
                    MailAddress addrTo = new MailAddress(ToMail);
                    if (!string.IsNullOrEmpty(ToDisplayName))
                        addrTo = new MailAddress(ToMail, ToDisplayName);

                    MailMessage msg = new MailMessage(addrFrom, addrTo);
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.BodyEncoding = mailEncoding;

                    //是否启用html
                    switch (mailFormat)
                    {
                        case EmailType.text:
                            msg.IsBodyHtml = false;
                            break;
                        case EmailType.html:
                            msg.IsBodyHtml = true;
                            break;
                    }
                    //优先级
                    switch (emailPriority)
                    {
                        case 2:
                            msg.Priority = MailPriority.High;
                            break;
                        case 1:
                            msg.Priority = MailPriority.Normal;
                            break;
                        case 0:
                            msg.Priority = MailPriority.Low;
                            break;
                    }

                    SmtpClient SC = new SmtpClient(smtpServer, smtpPort);
                    SC.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SC.UseDefaultCredentials = false; //不用默认的凭据
                    SC.Credentials = new NetworkCredential(mailUserName, mailPassWord); //身份凭据                

                    SC.EnableSsl = isSSL;
                    SC.Send(msg);
                    boolReturn = true;
                    strResult = "success"; //表示成功

                    #endregion
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message; //返回错误信息
                //throw new Exception(ex.Message);
            }

            return boolReturn;
        }
    }
}
