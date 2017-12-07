<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="UserConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.UserConfig" %>

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
            <!--筛选条件 end-->
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Cấu hình thành viên</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đặc điểm tên thành viên</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox2" Text="^[a-zA-Z][a-zA-Z0-9_-]{5,19}" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Thành viên khi đăng ký，Tên thành viên phải tuân thủ quy tắc này">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Giữ lại tên thành viên</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox3" TextMode="MultiLine" Rows="5" Columns="60" runat="server"
                                CssClass="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Thành viên khi đăng ký，Tên thành viên phải tuân thủ quy tắc này">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thỏa thuận đăng ký thành viên</label>
                        <div class="col-md-8">
                            <CKEditor:CKEditorControl Toolbar="Basic" ID="TextBox6" PasteFromWordPromptCleanup="true"
                                runat="server" Width="98%" Height="360"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đăng ký điểm thưởng</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox7" Text="10" runat="server" CssClass="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Điểm khuyến mãi</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox8" Text="10" runat="server" CssClass="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đăng ký cần phải được xác nhận</label>
                        <div class="col-md-4">
                            <asp:CheckBox ID="CheckBox1" runat="server" class="checkbox_radio" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cần đăng nhập để xác minh</label>
                        <div class="col-md-4">
                            <asp:CheckBox ID="CheckBox2" runat="server" class="checkbox_radio" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cần phải xác nhận khi quên mật khẩu</label>
                        <div class="col-md-4">
                            <asp:CheckBox ID="CheckBox3" runat="server" class="checkbox_radio" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số lần đăng nhập sai bị khóa</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox9" Text="50" runat="server" CssClass="form-control" MaxLength="10"></jweb:H5TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Liên tục nỗ lực đăng nhập thất bại sẽ vô hiệu hóa đăng nhập trong một thời gian">&#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian lưu trạng thái đăng nhập</label>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" CssClass="checkbox_radio">
                                <asp:ListItem Text="Đến khi đóng trình duyệt" Value="浏览器Close即失效"></asp:ListItem>
                                <asp:ListItem Text="1 tuần" Value="一周"></asp:ListItem>
                                <asp:ListItem Text="1 năm" Value="一年"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <!--end 右侧内联框架-->
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
