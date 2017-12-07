<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="OrderSet.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.OrderMger.OrderSet" %>

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
                       Cài đặt tự động</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đơn đặt hàng vượt quá Hạn chế mua</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <jweb:H5TextBox Mode="Number" ID="TextBox1" runat="server" class="form-control"></jweb:H5TextBox>
                                <span class="input-group-addon">phút</span>
                            </div>
                        </div>
                        <label for="firstname" class="col-md-3 control-label">
                            Không thanh toán，Đơn đặt hàng tự động hủy</label>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đơn hang vượt quá</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <jweb:H5TextBox Mode="Number" ID="TextBox2" runat="server" class="form-control"></jweb:H5TextBox>
                                <span class="input-group-addon">giờ</span>
                            </div>
                        </div>
                        <label for="firstname" class="col-md-3 control-label">
                            Không thanh toán，Đơn đặt hàng tự động hủy</label>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Vận chuyển quá</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <jweb:H5TextBox Mode="Number" ID="TextBox3" runat="server" class="form-control"></jweb:H5TextBox>
                                <span class="input-group-addon">ngày</span>
                            </div>
                        </div>
                        <label for="firstname" class="col-md-3 control-label">
                            Không tiếp nhận, đơn hàng tự động hoàn tất</label>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đơn đặt hàng hoàn tất hơn</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <jweb:H5TextBox Mode="Number" ID="TextBox4" runat="server" class="form-control"></jweb:H5TextBox>
                                <span class="input-group-addon">ngày</span>
                            </div>
                        </div>
                        <label for="firstname" class="col-md-3 control-label">
                            Tự động kết thúc, đơn hàng không thể thực hiện</label>
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
