using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Plugin.VerifyCode
{
    /// <summary>
    /// Web/win状态下的验证码
    /// </summary>
    public enum VerifyCodeType
    {
        Web,
        Win
    };

    public interface IVerifyCode
    {
        #region 公共属性

        VerifyCodeType CheckCodeType { set; get; }
        string CheckCodeString { get; }
        System.Drawing.Image CheckCodeImg { get; }

        #endregion

        //生成验证码
        string GenerateCheckCode();
        //输出图片
        void CreateCheckCodeImage();
    }    
}
