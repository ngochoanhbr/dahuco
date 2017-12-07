<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="UploadBat.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Tools.UploadBat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Include/Plugin/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Plugin/uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
        var ASPSESSID = "<%= Session.SessionID %>";
        var errmsg = "";
        $(function () {
            $("#uploadify").uploadify({
                'debug': false,
                //Chỉ định thành viênswf文件
                'swf': '/Include/Plugin/uploadify/uploadify.swf', //IE9下如果出现问题请用全地址 http://www.xxx.com/Include/Plugin/uploadify/uploadify.swf
                //后台处理的页面
                'uploader': '/Include/AjaxUploader/Uploader.aspx',
                //按钮显示的文字
                'buttonText': 'Lựa chọn 文件',
                //显示的高度和宽度，默认 height 30；width 120
                'height': 30,
                'width': 120,
                //上传文件的Kiểu  默认为所有文件    'All Files'  ;  '*.*'
                //在浏览窗口底部的文件Kiểu下拉菜单中显示的文本
                //'fileTypeDesc': '允许上传Kiểu(*.gif; *.jpg; *.png; *.txt;*.rar)',
                'fileTypeDesc': '允许上传Kiểu(<%=SinGooCMS.Config.ConfigProvider.Configs.EnableUploadExt.Replace(".","*.").Replace("|",";") %>)',
                //允许上传的文件后缀
                'fileTypeExts': '<%=SinGooCMS.Config.ConfigProvider.Configs.EnableUploadExt.Replace(".","*.").Replace("|",";") %>',
                //发送给后台的其他参数通过formDataChỉ định thành viên 注意这里只能传送静态参数
                //'formData': { 't': 'uploadbymanager', 'vfolderid': vFolderID, 'ASPSESSID': ASPSESSID, 'AUTHID': auth }, //_vfolderid是虚拟目录的ID
                //上传文件页面中，你想要用来作为文件队列的元素的id, 默认为false  自动生成,  不带#
                'queueID': 'fileQueue',
                //一个队列上传文件数限制 
                'queueSizeLimit': 10,
                //完成时是否清除队列 默认true  
                'removeCompleted': false,
                //完成时清除队列显示秒数,默认3秒
                'removeTimeout': 1,
                //队列上传出错，是否继续回滚队列
                'requeueErrors': false,
                //上传超时 
                'successTimeout': 300,
                //允许上传的最多张数  
                'uploadLimit': 99,
                //Lựa chọn 文件后自动上传
                'auto': false,
                //设置为true将允许Multi File Input
                'multi': true,
                //文件大小限制 0为无限制 默认KB 
                'fileSizeLimit': '<%=SinGooCMS.Config.ConfigProvider.Configs.UploadSizeLimit %>KB',
                //默认percentage 进度显示方式            
                'progressData': 'speed',
                //检测FLASH失败调用           
                'onFallback': function () {
                    alert("FLASH player chưa được cài, hoặc chưa được enable trên trình duyệt");
                },
                'onUploadStart': function (file) {
                    $("#uploadify").uploadify("settings", "formData", { 't': 'uploadbymanager', 'vfolderid': $("#folderid").val(), 'ASPSESSID': ASPSESSID, 'AUTHID': auth });
                    //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
                },
                //上传成功后执行
                'onUploadSuccess': function (file, data, response) {
                    var json = eval('(' + data + ')');
                    if (json.status == 0) {
                        errmsg += ",Cảnh báo：" + json.errmsg;
                        showtip("Cảnh báo：" + json.errmsg); //上传失败
                    }
                },
                'onQueueComplete': function () {
                    if (errmsg == "") {
                        //上传成功后刷新父页
                        $.dialog.close();
                        $.dialog.open.origin.location.reload();
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (!config.EnableFileUpload)
      { %>
    <div style="margin-top: 100px; font-size: 14px; text-align: center">
        File tải lên chưa mở</div>
    <%}
      else if (!base.IsAuthorizedOp("UploadFile"))
      { %>
    <div style="margin-top: 100px; font-size: 14px; text-align: center">
        không có thẩm quyền</div>
    <%}
      else
    { %>
    <div class="container-fluid">
        <div id="myTabContent" class="tab-content areacolumn">
            <div class="fix white-bg">
                <input type="hidden" id="folderid" />
                <!--左边 Lựa chọn -->
                <div class="fl w200">
                    <label><b>nộp hồ sơ</b></label>
                    <asp:DropDownList ID="ddlFolder" runat="server" CssClass="form-control" size="8">
                    </asp:DropDownList>
                </div>
                <!--右边 上传列表-->
                <div class="fl w400 ml20" style="width: 410px;">
                    <div style="text-align: left">
                        <label><b>hàng đợi tập tin</b></</label></div>
                    <div id="fileQueue" class="bar" style="height: 262px; overflow: auto; text-align: center;border:1px solid #BDC3D1">
                   
                    </div>
                </div>
            </div>
            <div class="profile-body">
                <div class="datafrom">
                    <div class="mt-20">
                        <input type="file" name="uploadify" id="uploadify" />
                    </div>
                    <div class="text-right">
                    <input type="button" onclick="$('#uploadify').uploadify('upload','*');" class="btn btn-danger"
                        value="Tải lên OK" />
                    <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                        class="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
    <script type="text/javascript">
        $("#<%=ddlFolder.ClientID %>").change(function () {
            $("#folderid").val($(this).val());
        });
    </script>    
</asp:Content>