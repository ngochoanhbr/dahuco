<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SendDingYueMail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.SendDingYueMail" %>

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
            <ul id="myTab" class="nav nav-tabs container-fluid">
                <li><a href="javascript:;" data-toggle="tab" onclick="location='DingYue.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Email đăng ký</a></li>
                <li><a href="javascript:;" data-toggle="tab" onclick="location='MailList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Danh sách gửi thư</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab" onclick="location='SendDingYueMail.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Gửi e-mail</a></li>
            </ul>
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Gửi tin cho email đăng ký</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Dịch vụ email</label>
                        <div class="col-md-5">
                            <asp:Literal ID="servermailbox" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tiêu đề</label>
                        <div class="col-md-5">
                            <asp:TextBox ID="TextBox2" placeholder="Vui lòng nhập tiêu đề thông báo" runat="server" required="require"
                                CssClass="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Nội dung</label>
                        <div class="col-md-8">
                            <CKEditor:CKEditorControl ID="TextBox3" PasteFromWordPromptCleanup="true" runat="server"
                                Width="100%" Height="260" Toolbar="Basic"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Gửi" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                    <input id="btncancel" onclick="location='DingYue.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Hủy bỏ" class="btn btn-default" />
                </div>
            </div>
        </div>
        <!--end 右侧内联框架-->
    </div>
</asp:Content>
