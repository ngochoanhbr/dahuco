<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="UploadTools.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Tools.UploadTools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/platform/h5/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="/platform/h5/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="/platform/h5/js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <link href="/Include/Plugin/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Plugin/uploadify/jquery.uploadify.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //传递的参数 Lựa chọn 的Kiểutype: single mutil 回调的Tagelementid 回调的属性attr: val src
        singoo.initRequest();
        var type = "<%=SelectType %>";

        var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
        var ASPSESSID = "<%= Session.SessionID %>";
        var errmsg = "";
        var filename = "";
        $(function () {
            $("#uploadify").uploadify({
                'debug': false,
                //Chỉ định thành viênswf文件
                'swf': '/Include/Plugin/uploadify/uploadify.swf', //IE9下如果出现问题请用全地址 http://www.xxx.com/Include/Plugin/uploadify/uploadify.swf
                //后台处理的页面
                'uploader': '/Include/AjaxUploader/Uploader.aspx',
                //按钮显示的文字
                'buttonText': 'Chọn tập tin',
                //显示的高度和宽度，默认 height 30；width 120
                'height': 30,
                'width': 120,
                //上传文件的Kiểu  默认为所有文件    'All Files'  ;  '*.*'
                //在浏览窗口底部的文件Kiểu下拉菜单中显示的文本
                //'fileTypeDesc': '允许上传Kiểu(*.gif; *.jpg; *.png; *.txt;*.rar)',
                'fileTypeDesc': 'Kiểu cho phép tải lên(<%=SinGooCMS.Config.ConfigProvider.Configs.EnableUploadExt.Replace(".","*.").Replace("|",";") %>)',
                //允许上传的文件后缀
                'fileTypeExts': '<%=SinGooCMS.Config.ConfigProvider.Configs.EnableUploadExt.Replace(".","*.").Replace("|",";") %>',
                //发送给后台的其他参数通过formDataChỉ định thành viên 注意这里只能传送静态参数
                //'formData': { 't': 'uploadbymanager', 'vfolderid': vFolderID, 'ASPSESSID': ASPSESSID, 'AUTHID': auth }, //_vfolderid是虚拟目录的ID
                //上传文件页面中，你想要用来作为文件队列的元素的id, 默认为false  自动生成,  不带#
                'queueID': 'fileQueue',
                //一个队列上传文件数限制 
                'queueSizeLimit': 20,
                //完成时是否清除队列 默认true  
                'removeCompleted': false,
                //完成时清除队列显示秒数,默认3秒
                'removeTimeout': 1,
                //队列上传出错，是否继续回滚队列
                'requeueErrors': false,
                //上传超时 
                'successTimeout': 300,
                //允许上传的最多张数  
                'uploadLimit': 20,
                //Lựa chọn 文件后自动上传
                'auto': false,
                //设置为true将允许Multi File Input
                'multi': type == "single" ? false : true,
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
                    if (json.status == 0)
                        errmsg = "Không thể upload：" + json.errmsg;
                    else {
                        if (type == "single") filename = json.filename;
                        else filename += json.filename+",";
                    }
                },
                'onQueueComplete': function (data) {
                    if (errmsg == "") {
                        callback(filename.lastIndexOf(",") != -1 ? filename.cutRight(',') : filename);
                        $.dialog.close();
                    } else {
                        showtip(errmsg);
                    }
                }
            });
        });
        
        var fileForSelected = "";
        function selectFiles() {
            $.each($('.Gallery_Popup_cr-imgs li'), function (i, item) {
                if ($(item).hasClass("selected")) { //被选中
                    if (type == "single") fileForSelected = $(item).find("img").attr("original");
                    else fileForSelected += $(item).find("img").attr("original") + ",";
                }
            });
            if (fileForSelected != "") {
                callback(fileForSelected.lastIndexOf(",") != -1 ? fileForSelected.cutRight(',') : fileForSelected);
                fileForSelected = ""; //重置
                $.dialog.close();
            } else {
                showtip("Xin vui lòng chọn tập tin");
            }
        }
        function selectRightNow(element) {
            callback($(element).attr("original"));
            $.dialog.close();
        }
        function upNetworkFile() { //上传网络文件
            var wlfile = $("#networkfile").val();
            if (wlfile == "")
                showtip("Nhập các tập tin địa chỉ mạng");
            else if (wlfile.length > 255)
                showtip("Địa chỉ mạng không thể dài hơn 255 ký tự");
            else {
                callback(wlfile);
                $.dialog.close();
            }
        }

        //回调,给调用的Tag赋值
        function callback(strFileName) {
            if (type == "single") {
                for (var i = 0; i < singoo.request["elementid"].split(',').length; i++) {
                    $($.dialog.open.origin.document.getElementById(singoo.request["elementid"].split(',')[i])).attr(singoo.request["attr"].split(',')[i], strFileName);
                } 
            }else if (type=="mutil") {
                $.dialog.open.origin.dowork(strFileName); //让父页自行处理数据
            } else if (type = "ckeditor") {
                $.dialog.open.origin.upimg(strFileName, singoo.request["elementid"].split(',')[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="fix white-bg profile-wrapper">
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#panel_1" data-toggle="tab">tập tin tải lên</a></li>
                <li><a href="#panel_2" data-toggle="tab">Lựa chọn tập tin</a></li>
                <li><a href="#panel_3" data-toggle="tab">Network file</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <!--上传本地文件-->
                <div class="tab-pane fade in active" id="panel_1">
                    <%if (!config.EnableFileUpload)
                      { %>
                    <div style="margin-top: 100px; font-size: 14px; text-align: center">
                        File tải lên không Mở</div>
                    <%}
                      else if (!base.IsAuthorizedOp(41, "UploadFile"))
                      { %>
                    <div style="margin-top: 100px; font-size: 14px; text-align: center">
                        không có thẩm quyền</div>
                    <%}
                      else
                      { %>
                    <!--左边 Lựa chọn -->
                    <input type="hidden" id="folderid" />
                    <div class="fl w200">
                        <label>
                            <b>tập tin</b></label>
                        <asp:DropDownList ID="ddlFolder" runat="server" size="8" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <!--右边 上传列表-->
                    <div class="fl w400 ml20" style="width: 533px;">
                        <div style="text-align: left">
                            <label>
                                <b>thông tin ảnh</b></label></div>
                        <div id="fileQueue" style="height: 262px; overflow: auto; text-align: center; border: 1px solid #BDC3D1">
                        </div>
                    </div>
                    <div class="profile-body clear fixed">
                        <div class="datafrom ">
                            <input type="file" name="uploadify" id="uploadify"/>
                            <div class="text-right">
                                <input type="button" onclick="$('#uploadify').uploadify('upload','*');" class="btn btn-danger" value="Tải lên OK" />
                                <input onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
                    <%} %>
                </div>
                <!--Lựa chọn 文件-->
                <div class="tab-pane fade" id="panel_2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <!--上传列表-->
                            <div class="fl w600 ml20">
                                <div class="form-group fix">
                                    <div class="w200 fl" style="width:100px;">
                                        <asp:DropDownList ID="ddlFolder2" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="w200 fl ml-20" style="width:120px;">
                                        <asp:TextBox ID="selectdate" runat="server" CssClass="form-control" placeholder="Lựa chọn upload trong tháng" date-selectmonth='true' data-date-format="yyyy-mm"></asp:TextBox>
                                    </div>
                                    <div class="w200 fl ml-20" style="width:150px;">
                                        <asp:TextBox ID="search_text" runat="server" CssClass="form-control" placeholder="Hãy nhập tên tập tin"></asp:TextBox>
                                    </div>
                                    <div class="w200 fl ml-20" style="width:70px;">
                                        <asp:CheckBox ID="imgonly" runat="server" class="checkbox_radio" Checked="true"/>chỉ hình ảnh
                                    </div>
                                    <div class="w200 fl" style="width:80px;">
                                        <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
                                    </div>
                                    <div class="fr">
                                <div class="col-md-12">
                                    <asp:DropDownList class="form-control select" ID="drpPageSize" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                        <asp:ListItem Text="14" Value="14" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                        <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                        <asp:ListItem Text="64" Value="64"></asp:ListItem>
                                        <asp:ListItem Text="140" Value="140"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                                </div>
                                <div id="filepanel" class="Gallery_Popup_cr-imgs fix" style="width: 780px; overflow: auto;
                                    height: 260px">
                                    <ul>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <li seltype='<%=SelectType %>'>
                                                    <div class="u-img">
                                                        <img ondblclick="selectRightNow(this);" src='<%#Eval("Thumb") %>' alt="" title='<%#Eval("FileName") %>' original='<%#Eval("VirtualPath") %>'/>
                                                    </div>
                                                    <div class="u-name text-center">
                                                        <p title='<%#Eval("FileName") %>'><%#Eval("FileName") %></p>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%=Repeater1.Items.Count == 0 ? "<div style='text-align:center;line-height:50px;'>Chúng tôi không tìm thấy bất kỳ dữ liệu</div>" : ""%>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                            <div class="profile-body clear fixed" style="padding-left: 0; padding-right: 28px;">
                                <div class="modal-footer-left text-left">
                                    <button type="button" class="btn btn-danger" onclick="selectFiles()";>Chọn file </button>
                                    <input onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)" class="btn btn-default" />
                                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nhập vào 1 hình ảnh có thể chọn nhanh chóng  ">&#xe613;</i>
                                </div>
                                <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="28" CssClass="paginator"
                                    SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!--Lựa chọn 网络文件-->
                <div class="tab-pane fade" id="panel_3">
                    <div class="form-group mt-20" style="min-height: 160px;">
                        <label for="firstname" class="w200 control-label fl">
                            <b>tệp sổ địa chỉ:</b></label>
                        <div class="w400 fl" style="width:400px;">
                            <input id="networkfile" class="form-control" placeholder="Với http:// hoặc https:// ở đầu" type="url" maxlength="255" />
                        </div>
                    </div>
                    <div class="profile-body clear fixed" style="padding-left: 0; padding-right: 28px;">
                        <div class="datafrom text-right">
                            <input type="button" class="btn btn-danger" value="Tải lên" onclick="upNetworkFile();" />
                            <input onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)" class="btn btn-default" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //月Lựa chọn 
        $("input[type='text'][date-selectmonth='true']").datetimepicker({ minView: 3, startView: 3, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });
        // item selection
        $('.Gallery_Popup_cr-imgs li').click(function () {
            if ($(this).attr("seltype") == "single" && !$(this).hasClass("selected")) { //仅单选
                $('.Gallery_Popup_cr-imgs li').removeClass("selected");
            }
            $(this).toggleClass('selected');
        });        
        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });
        $("#<%=ddlFolder.ClientID %>").change(function () {
            $("#folderid").val($(this).val());
        });
    </script>
</asp:Content>
