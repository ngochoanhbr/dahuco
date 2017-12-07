<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyRefundApply.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.OrderMger.ModifyRefundApply" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='RefundApply.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Yêu cầu trả hàng</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Chi tiết yêu cầu trả hàng</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        Chi tiết yêu cầu trả hàng</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Serial đơn đặt hàng</label>
                        <div class="col-md-8">
                            <%=apply.OrderNo%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Các mặt hàng</label>
                        <div class="col-md-8">
                            <table class="table table-bordered">
                                <tr class="active"><th>Các mặt hàng</th><th>Số lượng</th></tr>
                                <tr><td><%=apply.ProductName%></td><td><%=apply.RefundGoodsNum%></td></tr>
                            </table>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Lý do trả lại</label>
                        <div class="col-md-8">
                            <%=apply.Reason%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số tiền hoàn trả</label>
                        <div class="col-md-8">
                            <%=apply.Amount.ToString("C2")%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hoàn lại tiền</label>
                        <div class="col-md-8">
                            <%=apply.RefundAmountType%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Người trả</label>
                        <div class="col-md-8">
                            <%=apply.RefunderName%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Điện thoại</label>
                        <div class="col-md-8">
                            <%=apply.RefunderPhone%>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Chú ý</label>
                        <div class="col-md-5">
                            <asp:TextBox placeholder="Please enter a comment" ID="bz" runat="server" TextMode="MultiLine" Rows="3" Columns="60" CssClass="form-control" lenlimit="1000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian trả hàng</label>
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
                    <input onclick="location='RefundApply.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
