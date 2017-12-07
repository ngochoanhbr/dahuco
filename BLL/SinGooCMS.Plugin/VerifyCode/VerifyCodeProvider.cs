using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinGooCMS.Plugin.VerifyCode
{
    public class VerifyCodeProvider
    {
        /// <summary>
        /// 创建一个默认的验证码
        /// </summary>
        /// <returns></returns>
        public static IVerifyCode Create()
        {
            return Create("ImageCode");
        }

        /// <summary>
        /// 创建一个指定的验证码
        /// </summary>
        /// <param name="smsClassName"></param>
        /// <returns></returns>
        public static IVerifyCode Create(string smsClassName)
        {
            return (IVerifyCode)(Activator.CreateInstance("SinGooCMS.Plugin", "SinGooCMS.Plugin.VerifyCode." + smsClassName).Unwrap());
        }
    }
}
