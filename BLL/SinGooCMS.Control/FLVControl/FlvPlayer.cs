/*
 * JWPlayer 直接播放FLV
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SinGooCMS.Control
{
    [DefaultProperty("MediaPath")]
    [ToolboxData("<{0}:FlvPlayer runat=server></{0}:FlvPlayer>")]
    public class FlvPlayer : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        string _playerid = string.Empty; //播放器的页面ID
        Unit _player_width = 500;//播放器的宽度
        Unit _player_height = 400; //播放器的高度
        string _previmgpath = "/FlvPlayer/preview.jpg"; //预先展示图片的路径 如:/FlvPlayer/preview.jpg
        string _allowfullscreen = "true"; //默认可全屏
        string _autostart = "true"; //是否自动播放
        string _mediapath = string.Empty; //需要播放的文件路径 

        #region 属性组
        /// <summary>
        /// 播放器宽度
        /// </summary>
        public override Unit Width
        {
            get
            {
                return _player_width;
            }
            set
            {
                _player_width = value;
            }
        }
        /// <summary>
        /// 播放器高度
        /// </summary>
        public override Unit Height
        {
            get
            {
                return _player_height;
            }
            set
            {
                _player_height = value;
            }
        }
        //播放器的页面ID，只能有一个,该ID由ufo.js调用
        public string PlayerID
        {
            get
            {
                return _playerid;
            }
        }
        //预先展示图片的路径
        public string PrevImagePath
        {
            get
            {
                return _previmgpath;
            }
            set
            {
                _previmgpath = value;
            }
        }
        //是否全屏设置
        public string AllowFullScreen
        {
            get
            {
                return _allowfullscreen;
            }
            set
            {
                _allowfullscreen = value;
            }
        }
        //是否自动播放
        public string AutoStart
        {
            get
            {
                return _autostart;
            }
            set
            {
                _autostart = value;
            }
        }
        /// <summary>
        /// 媒体文件路径
        /// </summary>
        public string MediaPath
        {
            get
            {
                return _mediapath;
            }
            set
            {
                _mediapath = value;
            }
        }
        #endregion

        protected override void RenderContents(HtmlTextWriter output)
        {
            _playerid = "JsonLeeCMSFLVPlayer_" + this.ID;

            string strTemp = "<!-- START OF THE PLAYER EMBEDDING TO COPY-PASTE -->"
                            + "<script type=\"text/javascript\" src=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.NewJWPlayer.swfobject.js") + "\"></script>"
                            + "<script type=\"text/javascript\">"
                            + "	swfobject.registerObject(\"player\",\"9.0.98\",\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.NewJWPlayer.expressInstall.swf") + "\");"
                            + "</script>"
                            + "<object id=\"" + _playerid + "\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" name=\"" + _playerid + "\" width=\"" + _player_width + "\" height=\"" + _player_height + "\">"
                            + "	<param name=\"movie\" value=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.NewJWPlayer.player.swf") + "\" />"
                            + "	<param name=\"allowfullscreen\" value=\"" + _allowfullscreen + "\" />"
                            + "	<param name=\"allowscriptaccess\" value=\"always\" />"
                            + "	<param name=\"flashvars\" value=\"file=" + _mediapath + "&image=" + _previmgpath + "&skin=&autostart=" + _autostart + "\" />"
                            + "	<object type=\"application/x-shockwave-flash\" data=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.NewJWPlayer.player.swf") + "\" width=\"" + _player_width + "\" height=\"" + _player_height + "\">"
                            + "		<param name=\"movie\" value=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.NewJWPlayer.player.swf") + "\" />"
                            + "		<param name=\"allowfullscreen\" value=\"" + _allowfullscreen + "\" />"
                            + "		<param name=\"allowscriptaccess\" value=\"always\" />"
                            + "		<param name=\"flashvars\" value=\"file=" + _mediapath + "&image=" + _previmgpath + "&skin=&autostart=" + _autostart + "\" />"
                            + "		<p><a href=\"http://get.adobe.com/flashplayer\">Get Flash</a> to see this player.</p>"
                            + "	</object>"
                            + "</object>"
                            + "<!-- END OF THE PLAYER EMBEDDING -->";

            output.Write(strTemp);
        }
    }
}
