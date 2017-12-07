<%@ Page Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.Include.AjaxUploader.Uploadify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传工具</title>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            font-size: 12px;
        }
        .uploadtable
        {
            width: 100%;
        }
        .uploadtable td
        {
            text-align: center;
            border: 1px solid #ccc;
            vertical-align: top;
            padding: 10px;
        }
        .swfupload
        {
            left: 0px;
            cursor: pointer;
        }
    </style>
    <script src="/Include/Script/jquery.min.js" type="text/javascript"></script>
    <script src="/Include/Script/publicfun.js" type="text/javascript"></script>
    <link href="/Include/Plugin/zTree/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Plugin/zTree/jquery.ztree.core-3.5.min.js" type="text/javascript"></script>
    <link href="/Include/Plugin/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Plugin/uploadify/jquery.uploadify.min.js" type="text/javascript"></script>    
    <script type="text/javascript">
        var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
        var ASPSESSID = "<%= Session.SessionID %>";
        var chkval = ""; //选中的值,用于返回
        $(function () {
            $("#uploadify").uploadify({
                'debug': false,
                //指定swf文件
                'swf': '/Include/Plugin/uploadify/uploadify.swf', //IE9下如果出现问题请用全地址 http://www.xxx.com/Include/Plugin/uploadify/uploadify.swf
                //后台处理的页面
                'uploader': '/Include/AjaxUploader/Uploader.aspx',
                //按钮显示的文字
                'buttonText': '选择文件',
                //显示的高度和宽度，默认 height 30；width 120
                'height': 30,
                'width': 120,
                //上传文件的类型  默认为所有文件    'All Files'  ;  '*.*'
                //在浏览窗口底部的文件类型下拉菜单中显示的文本
                //'fileTypeDesc': '允许上传类型(*.gif; *.jpg; *.png; *.txt;*.rar)',
                'fileTypeDesc': '允许上传类型(<%=SinGooCMS.Config.ConfigProvider.Configs.EnableUploadExt.Replace(".","*.").Replace("|",";") %>)',
                //允许上传的文件后缀
                'fileTypeExts': '<%=SinGooCMS.Config.ConfigProvider.Configs.EnableUploadExt.Replace(".","*.").Replace("|",";") %>',
                //发送给后台的其他参数通过formData指定 注意这里只能传送静态参数
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
                //选择文件后自动上传
                'auto': false,
                //设置为true将允许多文件上传
                'multi': true,
                //文件大小限制 0为无限制 默认KB 
                'fileSizeLimit': '<%=SinGooCMS.Config.ConfigProvider.Configs.UploadSizeLimit %>KB',
                //默认percentage 进度显示方式            
                'progressData': 'speed',
                //检测FLASH失败调用           
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                'onUploadStart': function (file) {
                    $("#uploadify").uploadify("settings", "formData", { 't': 'uploadbymanager', 'vfolderid': $("#folderid").val(), 'ASPSESSID': ASPSESSID, 'AUTHID': auth });
                    //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
                },
                //上传成功后执行
                'onUploadSuccess': function (file, data, response) {
                    var json = eval('(' + data + ')');
                    if (json.status == 0)
                        $("#msg").html("警告：" + json.errmsg); //上传失败
                    else {
                        $('#' + file.id).find('.data').html("上传完毕");
                        $('#' + file.id).find('.fileName').html("<input type='checkbox' checked=\"checked\" name='upchk' filename='" + json.filename + "' />" + $('#' + file.id).find('.fileName').html());
                        if (json.filename.IsPicture()) //显示图片格式文件
                            $("#showpic").html("<img alt='上传图片' src='" + json.filename + "' style='max-width:250px;max-height:260px' />");

                        $("input[name='upchk']").each(function (item) {
                            var currfilename = $(this).attr("filename");
                            if ($(this).attr("checked")) { //选中
                                if (!existsvalue(currfilename)) {
                                    if (chkval == "")
                                        chkval = currfilename;
                                    else
                                        chkval = chkval + "," + currfilename;
                                }
                            } else { //未选中
                                var arr = chkval.split(',');
                                var str = "";
                                for (var i = 0; i < arr.length; i++) {
                                    if (arr[i] != currfilename)
                                        str += arr[i] + ",";
                                }
                                str = str.substr(0, str.length - 1);
                                chkval = str;
                            }

                            parent.document.getElementById("hdfselectfile").value = chkval;
                        });
                    }
                    $("input[name='upchk']").change(function () {
                        var currfilename = $(this).attr("filename");
                        if ($(this).attr("checked")) { //选中
                            if (!existsvalue(currfilename)) {
                                if (chkval == "")
                                    chkval = currfilename;
                                else
                                    chkval = chkval + "," + currfilename;
                            }
                        } else { //未选中
                            var arr = chkval.split(',');
                            var str = "";
                            for (var i = 0; i < arr.length; i++) {
                                if (arr[i] != currfilename)
                                    str += arr[i] + ",";
                            }
                            str = str.substr(0, str.length - 1);
                            chkval = str;
                        }

                        parent.document.getElementById("hdfselectfile").value = chkval;
                    });
                    function existsvalue(val) {
                        var retvalues = chkval;
                        if (retvalues.indexOf(",") != -1) { //多个包含
                            var arrval = retvalues.split(",");
                            for (var i = 0; i < arrval.length; i++) {
                                if (arrval[i] == val)
                                    return true;
                            }
                        }
                        else if (retvalues == val)
                            return true; //其中值就是

                        return false;
                    }
                },
                'onQueueComplete': function () {
                    //$('#fileQueue').attr('style', 'visibility :hidden');
                }
            });
        });

        //异步加载文件夹树
        var setting = {
            async: {
                enable: true,
                url: "/Platform/blue/Ajax/getFolders.ashx",
                autoParam: ["id"]
            },
            view: {
                selectedMulti: false
            }
        };
        $(document).ready(function () {
            $.fn.zTree.init($("#folderTree"), setting);
            var treeObj = $.fn.zTree.getZTreeObj("folderTree");
            var newNode = { id: '-1', name: '未归档', isParent: false, iconSkin: 'leaf', 'click': "AppendVal(-1,'未归档')" }; //添加默认文件夹
            newNode = treeObj.addNodes(null, newNode);
        });
        function AppendVal(id, name) {
            $("#foldername").text(name);
            jQuery("#folderid").val(id);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%if (config.EnableFileUpload)
      { %>
    <div>
        <div style="float: left; width: 160px;">
            <%--文件将保存在：<%=base.UploadFolder %>--%>
            文件归档：<label id="foldername">未归档</label>
            <input type="hidden" id="folderid" />
        </div>
        <div style="float: right; width: 480px; color: Red" id="msg">
        </div>
    </div>
    <div style="margin-top: 10px;clear:both;">
        <div class="folderleft" style="float: left; width: 160px; height: 260px; border: 1px solid #ccc;
            overflow: auto">
            <ul id="folderTree" class="ztree">
            </ul>
        </div>
        <div class="uploadright" style="float: right; width: 480px;">
            <table cellpadding="2" cellspacing="2" class="uploadtable">
                <tr>
                    <td style="width: 90px;">
                        <input type="file" name="uploadify" id="uploadify" />
                        <p>
                            <a href="javascript:$('#uploadify').uploadify('upload','*')">上传</a>&nbsp;|&nbsp;
                            <a href="javascript:$('#uploadify').uploadify('cancel','*')">取消上传</a> <span id="result">
                            </span>
                        </p>
                    </td>
                    <td style="width: 200px;">
                        <div style="text-align: left">
                            文件队列：</div>
                        <div id="fileQueue">
                        </div>
                    </td>
                    <td style="width: 220px; display: none">
                        <div style="text-align: left;">
                            图片预览：</div>
                        <span id="showpic">
                            <img src="/Include/Images/waitupload.gif" alt="请上传图片" />
                        </span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%}
      else
      { %>
    <div style="margin-top: 100px; font-size: 14px; text-align: center">
        文件上传未开启</div>
    <%} %>
    </form>
</body>
</html>
