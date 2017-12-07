<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ExtConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.ExtConfig" %>

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
                        Cấu hình khác</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Những từ nhạy cảm</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="badword" runat="server" TextMode="MultiLine" Rows="3" Columns="50"
                                CssClass="form-control" lenlimit="1000"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nhập nhiều từ cách nhau bằng dấu ',' ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thay thế những từ nhạy cảm</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="bwreplaceword" runat="server" CssClass="form-control" lenlimit="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cước chuyển phát nhanh mặc định</label>
                        <div class="col-md-2">
                            <div class="input-group">
                                <span class="input-group-addon">VNĐ</span>
                                <jweb:H5TextBox Mode="Number" ID="txtdefkdfee" runat="server" CssClass="form-control" Text="10.0"></jweb:H5TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Vận chuyển EMS mặc định</label>
                        <div class="col-md-2">
                            <div class="input-group">
                                <span class="input-group-addon">VNĐ</span>
                                <jweb:H5TextBox Mode="Number" ID="txtdefemsfee" runat="server" CssClass="form-control"
                                Text="30.0"></jweb:H5TextBox>
                            </div>                            
                        </div>
                    </div>
                    <div class="form-group mt-20 fix hidden">
                        <label for="firstname" class="col-md-3 control-label">
                            Kích hoạt chuyển đổi đơn giản</label>
                        <div class="col-md-2">
                            <input id="cntwtrans" type="checkbox" class="ios-switch" runat="server" />
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Kích hoạt sau khi phiên bản truyền thống sẽ tự động dịch các văn bản, xin vui lòng hoàn thành bản dịch tùy chỉnh /Include/Language/lib/ văn bản đơn giản dưới các cửa hàng tương ứng">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Sửa tập tin mặc định</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" Text="CKEditor" MaxLength="30"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thống kê mã liên kết</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" Rows="3" Columns="50"
                                CssClass="form-control" lenlimit="255"></asp:TextBox>
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
