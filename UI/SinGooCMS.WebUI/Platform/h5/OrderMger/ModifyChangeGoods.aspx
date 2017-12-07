<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyChangeGoods.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.OrderMger.ModifyChangeGoods" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='ChangeGoods.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Đổi hàng </a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab"> Chi tiết Đổi hàng</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        Chi tiết đổi hàng</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Serial đơn đặt hàng</label>
                        <div class="col-md-8">
                            <%=apply.OrderNo%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hàng thay thế</label>
                        <div class="col-md-8">
                            <%=apply.GoodsName%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Lý do đổi hàng</label>
                        <div class="col-md-8">
                            <%=apply.Reason%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Người nhận</label>
                        <div class="col-md-8">
                            <%=apply.Consignee%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Địa chỉ người đổi</label>
                        <div class="col-md-8">
                            <%=apply.ShippingAddr%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Điện thoại</label>
                        <div class="col-md-8">
                            <%=apply.ContactPhone%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Chú ý</label>
                        <div class="col-md-5">
                            <asp:TextBox placeholder="Please enter a comment" ID="bz" runat="server" TextMode="MultiLine" Rows="3"
                                Columns="60" CssClass="form-control" lenlimit="1000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian đổi hàng</label>
                        <div class="col-md-8">
                            <%=apply.AutoTimeStamp%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Đồng ý" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <asp:Button ID="btncancel" Text="Hủy bỏ" runat="server" OnClick="btncancel_Click" CssClass="btn btn-danger" />
                    <input onclick="location='ChangeGoods.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
