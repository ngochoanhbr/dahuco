<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.SendMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid all">
        <div class="sidebar" id="left-panel">
        </div>
        <div class="profile-wrapper">
            <ol class="breadcrumb breadcrumb-quirk">
                <%=ShowNavigate()%>
            </ol>
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Gửi e-mail</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            Người nhận</label>
                        <div class="col-md-6">
                            <ul id="myTab" class="nav nav-tabs container-fluid">
                                <li class="active"><a href="#panel_1" data-toggle="tab">Tất cả thành viên</a></li>
                                <li><a href="#panel_2" data-toggle="tab">Nhóm thành viên</a></li>
                                <li><a href="#panel_3" data-toggle="tab">Cấp bậc thành viên</a></li>
                                <li><a href="#panel_4" data-toggle="tab">Hộp thư cụ thể</a></li>
                            </ul>
                            <div id="myTabContent" class="tab-content">
                                <div class="tab-pane fade in active" id="panel_1">
                                </div>
                                <div class="tab-pane fade" id="panel_2">
                                    <asp:DropDownList ID="ddlUserGroup" runat="server" CssClass="form-control" Style="width: 200px;">
                                    </asp:DropDownList>
                                </div>
                                <div class="tab-pane fade" id="panel_3">
                                    <asp:DropDownList ID="ddlUserLevel" runat="server" CssClass="form-control" Style="width: 200px;">
                                    </asp:DropDownList>
                                </div>
                                <div class="tab-pane fade" id="panel_4">
                                    <div class="col-md-10" style="padding:0">
                                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="5" Columns="60"
                                        CssClass="form-control" placeholder="Vui lòng nhập hộp thư của người nhận, nhiều hộp thư thì cách nhau bằng dấu phẩy','分隔"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                    <input type="button" value="Chọn hộp thư" id="selUser" onclick="$.dialog.open('../Selector/UserForSelectEmail.aspx?Module=<%=base.CurrentModuleCode %>&type=mutil&elementid=<%=TextBox2.ClientID %>&attr=value&backtype=names',{title:'Lựa chọn thành viên邮箱',width:680,height:450},false);"
                                        class="btn btn-default" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Vui lòng kiểm tra cấu hình Mail, gửi 1 lúc nhiều mail có thể mất nhiều thời gian, vui lòng chờ...！">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            Tiêu đề Mail</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox3" runat="server" placeholder="Vui lòng nhập tiêu đề thông báo" required="required"
                                CssClass="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            Nội dung Mail</label>
                        <div class="col-md-8">
                            <CKEditor:CKEditorControl Toolbar="Basic" ID="TextBox4" PasteFromWordPromptCleanup="true"
                                runat="server" Width="100%" Height="360"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Gửi" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="TargetType" runat="server" Value="ToAllUser" />
    <script type="text/javascript">
        $(function () { document.getElementById("<%=TextBox2.ClientID %>").setCustomValidity(""); });
        var arrTab = ["ToAllUser", "ToUserGrup", "ToUserLevel", "ToCustomUser"];
        $("#myTab>li>a").click(function () {
            $("#<%=TargetType.ClientID %>").val(arrTab[$(this).parent().index()]); //点击选项卡赋值
        });
    </script>
</asp:Content>
