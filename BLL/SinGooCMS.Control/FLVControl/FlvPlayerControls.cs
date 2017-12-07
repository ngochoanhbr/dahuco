/*
 *　create by liqiang665@163.com QQ:16826375 Date:2009-04-30 东莞．广东．中国
 *  这是一个flvplayer播放器控件．改自一个国外flv播放器
 *  改进为播放器调用一个GetMediaFilePath.cs的Web服务．返回该flv的真实路径．
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SinGooCMS.Control
{
    [DefaultProperty("FlvFileID")]
    [ToolboxData("<{0}:FlvPlayerControl runat=server></{0}:FlvPlayerControl>")]
    public class FlvPlayerControl : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        string _playerid = string.Empty; //播放器的页面ID
        Unit _player_width = 500;//播放器的宽度
        Unit _player_height = 400; //播放器的高度
        string _player_bgcolor = "#FFFFFF"; //播放器的背景颜色
        int _idkey = new int(); //flv文件ID参数 如:19
        string _previmgpath = "/FlvPlayer/prev.gif"; //预先展示图片的路径 如:/FlvPlayer/prev.gif
        string _allowfullscreen = "true"; //默认可全屏
        string _autostart = "true"; //是否自动播放

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
        //播放器的背景颜色
        public string PlayerBgColor
        {
            get
            {
                return _player_bgcolor;
            }
            set
            {
                _player_bgcolor = value;
            }
        }
        //文件的ID参数
        public int FlvFileID
        {
            get
            {
                return _idkey;
            }
            set
            {
                int intTemp = new int();
                try
                {
                    intTemp = value;
                    if (intTemp < 0) throw new Exception("flv影片ID号必须大于0！");
                }
                catch
                {
                    intTemp = 0;
                }
                _idkey = intTemp;
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
        /// WebService服务的名称
        /// 因为这个服务名称已经在FLASH播放器中写死,因此无法改变
        /// </summary>
        public string WebServiceName
        {
            get
            {
                return "GetMediaFilePath.asmx";
            }
        }
        /// <summary>
        /// WEB服务的方法名称
        /// 传入ID参数返回 FLV的真实路径 达到隐藏路径的目的
        /// </summary>
        public string WebServiceMethod
        {
            get
            {
                return "ReturnPath";
            }
        }
        #endregion

        /// <summary>
        /// 添加资源文件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            string scriptUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.ufo.js");
            Page.ClientScript.RegisterClientScriptInclude("flvjs", scriptUrl);
            base.OnPreRender(e);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            _playerid = "JsonLeeCMSFLVPlayer_" + this.ID;

            string strTemp = string.Empty;
            strTemp += "<p id=\"" + _playerid + "\">"
                + "<a href=\"http://www.macromedia.com/go/getflashplayer\">Get the Flash Player</a> to  see this player.</p>"
                + "<script type=\"text/javascript\">"
                + "var FO = {movie:\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "SinGooCMS.Control.FLVControl.Flvplayer.swf") + "\",width:\"" + _player_width.ToString() + "\",height:\"" + _player_height.ToString() + "\",majorversion:\"7\",build:\"0\",bgcolor:\"" + _player_bgcolor + "\",allowfullscreen:\"" + _allowfullscreen + "\""
                + ",flashvars:\"file=" + _idkey.ToString() + "&image=" + _previmgpath + "&autostart=" + _autostart + "\" };"
                + "UFO.create(FO, \"" + _playerid + "\");"
                + "</script>";

            output.Write(strTemp);
        }
    }
}
