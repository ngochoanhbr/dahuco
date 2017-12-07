<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Tools.ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid all">
        <div class="sidebar" id="left-panel">
            <ul class="nav">
                <li class="has-sub"><a href="/platform/h5/Tools/MyMessageBox.aspx"><i class="iconfont navbar-left">
                    </i><span>Tin nhắn</span> </a></li>
                <li class="has-sub"><a class="on" href="/platform/h5/Tools/ChangePwd.aspx"><i class="iconfont navbar-left">
                    </i><span>Chỉnh sửa mật khẩu</span> </a></li>
                <li class="has-sub"><a href="/platform/h5/Tools/ShowCache.aspx"><i class="iconfont navbar-left">
                    </i><span>Xem bộ nhớ cache</span> </a></li>
            </ul>
        </div>
        <div class="profile-wrapper">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="/Platform/h5/Main.aspx"><i class="iconfont mr5"></i> Home</a></li>
                <li class="active">Chỉnh sửa mật khẩu</li>
            </ol>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        Chỉnh sửa mật khẩu</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Mật khẩu cũ</label>
                        <div class="col-md-3">
                            <asp:TextBox TextMode="Password" placeholder="Vui lòng nhập mật khẩu " ID="oldpwd" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Mật khẩu mới</label>
                        <div class="col-md-3">
                            <asp:TextBox TextMode="Password" placeholder="Vui lòng nhập mật khẩu " ID="newpwd1" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Xác nhận mật khẩu mới</label>
                        <div class="col-md-3">
                            <asp:TextBox TextMode="Password" placeholder="Vui lòng nhập mật khẩu " ID="newpwd2" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Email</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số điện thoại</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="profile-body">
                    <div class="datafrom text-right">
                        <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click"
                            OnClientClick="return confirm('Sau khi đổi mật khẩu, cần đăng nhập lại，Bạn có muốn đổi mật khẩu？')" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
