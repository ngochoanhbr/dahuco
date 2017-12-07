<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SMSConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.MobMger.SMSConfig" %>

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
                        Cấu hình SMS</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>SMS interface plugin</label>
                        <div class="col-md-4">
                            <asp:TextBox list="sms_list" placeholder="Vui lòng nhập tên của plugin này trên giao diện" ID="TextBox1" runat="server"
                                CssClass="form-control" MaxLength="50" required="required"></asp:TextBox>
                            <datalist id="sms_list">
                                <option label="WebChineseSMS" value="WebChineseSMS" />
                                <option label="MomingSMS" value="MomingSMS" />
                                <option label="SinoteleICTSMS" value="SinoteleICTSMS" />
                            </datalist>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên tài khoản</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox3" runat="server" class="form-control" required="required"
                                MaxLength="50" placeholder="Nhập tên tài khoản của Người sử dụng SMS"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Mật khẩu</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" TextMode="Password" runat="server" class="form-control"
                                MaxLength="50" placeholder="Vui lòng nhập Mật khẩu SMS"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Mật khẩu phải ứng với tài khoản">
                            &#xe613;</i>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        SMS test</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Gửi một tin nhắn SMS thử nghiệm</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox5" onfocus="this.select();" runat="server" class="form-control"
                                MaxLength="20" pattern="1[0-9]{10}" placeholder="Nhập Số điện thoại"></asp:TextBox>
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
            var mobileNo = $("#<%=TextBox5.ClientID %>").val();
            if (mobileNo == "") {
                showtip("Vui lòng nhập Số điện thoại");
                return false;
            }

            return true;
        });
    </script>
</asp:Content>
