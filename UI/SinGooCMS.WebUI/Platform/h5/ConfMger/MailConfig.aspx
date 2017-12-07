<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="MailConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.MailConfig" %>

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
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        Dịch vụ email</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Thuộc hệ thống tài khoản mail</label>
                        <div class="col-md-4">
                            <jweb:H5TextBox Mode="Email" placeholder="Vui lòng nhập tài khoản mail" ID="TextBox1" runat="server" class="form-control"
                                required="required" MaxLength="50"></jweb:H5TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Toàn bộ tài khoản mail ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Thuộc hệ thống tên hộp thư người sử dụng</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required"
                                MaxLength="50"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Ghi đầy đủ email ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Mật khẩu email</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox3" TextMode="Password" runat="server" class="form-control"
                                MaxLength="50"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nhập mật khẩu email ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Máy chủ email(SMTP)：</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" runat="server" class="form-control" required="required"
                                MaxLength="50"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nếu bạn không thể gửi e-mail，Vui lòng kích hoạt SMTP trong cấu hình email của bạn">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Cổng：</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox5" runat="server" class="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kích hoạt SSL：</label>
                        <div class="col-md-4">
                            <input id="CheckBox6" type="checkbox" class="ios-switch" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Kiểm tra email</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Gửi email kiểm tra</label>
                        <div class="col-md-4">
                            <jweb:H5TextBox Mode="Email" placeholder="Vui lòng nhập các tài khoản email để nhận bài kiểm tra email" ID="txtReciver" runat="server"
                                class="form-control" MaxLength="50" onclick="this.select();"></jweb:H5TextBox>
                        </div>
                        <asp:Button ID="btnSend" runat="server" Text="Gửi" class="btn btn-danger" OnClick="btnSend_Click" />
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("#<%=btnSend.ClientID %>").click(function () {
            var mailaddr = $("#<%=txtReciver.ClientID %>").val();
            if (mailaddr == "") {
                showtip("Vui lòng nhập một tài khoản hộp thư kiểm tra");
                return false;
            }

            return true;
        });
    </script> 
</asp:Content>