using System;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace SinGooCMS.Entity
{
    public enum WeixinMsgType
    {
        [Description("文本")]
        Text,
        [Description("图片")]
        Image,
        [Description("语音")]
        Voice,
        [Description("视频")]
        Video,
        [Description("地理位置")]
        Location,
        [Description("连接信息")]
        Link,
        [Description("事件推送")]
        Event
    }

    /// <summary>
    /// 微信自动回复
    /// </summary>
    public partial class AutoRlyInfo
    {
        public WeixinMsgType MsgType
        {
            get
            {
                string[] arrImg = { ".jpg", ".jpeg", ".png", ".gif" };
                string[] arrVoice = { ".avi", ".wav", ".mp3", ".mid", ".mod", ".ra" };
                string[] arrVideo = { ".mp3", ".3gp", ".rmb", ".flv" };
                if (!string.IsNullOrEmpty(this.MediaPath))
                {
                    string strExt = Path.GetExtension(this.MediaPath).ToLower();
                    if (arrImg.Contains(strExt))
                        return WeixinMsgType.Image; //图文
                    else if (arrVoice.Contains(strExt))
                        return WeixinMsgType.Voice;
                    else if (arrVideo.Contains(strExt))
                        return WeixinMsgType.Video;
                }

                return WeixinMsgType.Text;
            }
        }
    }
}
