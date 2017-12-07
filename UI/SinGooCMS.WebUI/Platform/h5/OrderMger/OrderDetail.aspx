<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.OrderMger.OrderDetail" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Danh sách đơn đặt hàng</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Chi tiết đơn hàng</a></li>
            </ul>
            <div class="profile-body mb-20">
                <h3>
                    STT：<b class="text-danger"><%=order.OrderNo%></b> Trạng thái Hiện tại: <b class="text-danger">
                        <%=SinGooCMS.Utility.EnumUtils.GetEnumDescription((SinGooCMS.OrderStatus)order.OrderStatus)%></b></h3>
                <div class="form-group mt-20">
                    <div class="btn-group">
                        <asp:Button Visible="false" ID="btn_Confirm" runat="server" Text="Review the order" CssClass="btn btn-default" />
                        <asp:Button Visible="false" ID="btn_ChangeAmount" runat="server" Text="Chỉnh sửa giá" CssClass="btn btn-default" />
                        <asp:Button Visible="false" ID="btn_PayOk" runat="server" Text="Đơn đặt hàng đã thu" CssClass="btn btn-default" />
                        <asp:Button Visible="false" ID="btn_SendGoods" runat="server" Text="Đơn hàng đã giao" CssClass="btn btn-default" />
                        <asp:Button Visible="false" ID="btn_Delivered" runat="server" Text="Đơn hàng đã nhận" CssClass="btn btn-default" />
                        <asp:Button Visible="false" ID="btn_Cancel" runat="server" Text="Close" CssClass="btn btn-default" />
                        <input id="Button1" onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                            type="button" value="Quay lại" class="btn btn-default" />
                    </div>
                </div>
            </div>
            <!--面包屑 end-->
            <ul id="Ul1" class="nav nav-tabs container-fluid">
                <li class="active"><a href="#Order_1" data-toggle="tab">Thông tin đặt hàng</a></li>
                <li><a href="#Order_2" data-toggle="tab">Thông tin người nhận hàng và giao nhận</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade in active" id="Order_1">
                    <div class="profile-body mb-20">
                        <table class="table table-bordered mb-20">
                            <tr class="active">
                                <th class="text-center" style="width: 25%">
                                    Thành viên
                                </th>
                                <th class="text-center" style="width: 25%">
                                    Số tiền đơn hàng
                                </th>
                                <th class="text-center" style="width: 25%">
                                    Thanh toán
                                </th>
                                <th class="text-center" style="width: 25%">
                                    Các đơn tiếp theo
                                </th>
                            </tr>
                            <tr class="text-center">
                                <td>
                                    <%=order.UserName %>
                                </td>
                                <td>
                                    <%=order.OrderTotalAmount.ToString("C2") %>
                                    Vận Chuyển Bao gồm(<%=order.OrderShippingFee.ToString("C2") %>)
                                </td>
                                <td>
                                    <%=string.IsNullOrEmpty(order.PayName) ? "Không" : order.PayName%>
                                    (Trả 1 lần：<%=order.TradeNo %>)
                                </td>
                                <td>
                                    <%=order.AddOrderMethod %>
                                </td>
                            </tr>
                            <tr class="active">
                                <th class="text-center" style="width: 25%">
                                    Thời gian tạo đơn hàng
                                </th>
                                <th class="text-center" style="width: 25%">
                                    Thời gian thanh toán
                                </th>
                                <th class="text-center" style="width: 25%">
                                    Thời gian nhận hàng
                                </th>
                                <th class="text-center" style="width: 25%">
                                   Thời gian nhận biên lai
                                </th>
                            </tr>
                            <tr class="text-center">
                                <td>
                                    <%=order.OrderAddTime %>
                                </td>
                                <td>
                                    <%=order.OrderPayTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "Chưa trả" : order.OrderPayTime.ToString()%>
                                </td>
                                <td>
                                    <%=order.GoodsSendTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "Chưa giao" : order.GoodsSendTime.ToString()%>
                                </td>
                                <td>
                                    <%=order.GoodsServedTime.ToString("yyyy-MM-dd").Equals("1900-01-01") ? "Chưa sign" : order.GoodsServedTime.ToString()%>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered mb-20">
                            <tr class="active">
                                <td>
                                    Phản hồi
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%=string.IsNullOrEmpty(order.CustomerMsg) ? "Không" : order.CustomerMsg%>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered mb-20">
                            <thead>
                                <tr class="active">
                                    <th class="text-center" style="width: 40%">
                                        Các mặt hàng
                                    </th>
                                    <th class="text-center">
                                        Thuộc tính/Chú ý
                                    </th>
                                    <th class="text-center">
                                        giá
                                    </th>
                                    <th class="text-center">
                                        Số lượng
                                    </th>
                                    <th class="text-center">
                                        Tổng cộng
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rpt_Pros" runat="server">
                                    <ItemTemplate>
                                        <tr class="text-center">
                                            <td>
                                                <div class="table_img">
                                                    <a href='/shop/goods/<%#Eval("ProID") %>' target="_blank">
                                                        <img src='<%#Eval("ProImg") %>' alt="">
                                                    </a>
                                                </div>
                                                <div class="table_img_txt">
                                                    <span><a href='/shop/goods/<%#Eval("ProID") %>' data-toggle="tooltip" data-placement="top"
                                                        title="Xem Chi tiết" target="_blank">
                                                        <%#Eval("ProName") %></a></span> <span>Serial no.：<%#Eval("ProSN") %></span>
                                                </div>
                                            </td>
                                            <td>
                                                <textarea class="form-control" rows="3" cols="50"><%#Eval("Remark")%></textarea>
                                            </td>
                                            <td>
                                                <%#Convert.ToDecimal(Eval("Price")).ToString("C2")%>
                                                /
                                                <%#Eval("Integral") %>
                                                Ghép
                                            </td>
                                            <td>
                                                <%#Eval("Quantity") %>
                                            </td>
                                            <td>
                                                <%#Convert.ToDecimal(Eval("SubTotal")).Equals(0.0m) ? "0" : Convert.ToDecimal(Eval("SubTotal")).ToString("C2")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div class="mb-20 fix">
                            <div class="statistic fr">
                                <div class="list">
                                    <span>Tổng tiền：</span><em class="price" id="warePriceId">￥<%=order.GoodsTotalAmout.ToString("f2") %></em>
                                </div>
                                <div class="list">
                                    <span>Phiếu giảm giá：</span> <em class="price" id="cachBackId">-￥<%=order.CouponsValue>0?order.CouponsValue.ToString("f2"):"0" %></em>
                                </div>
                                <div class="list">
                                    <span>Phí vận chuyển：</span> <em class="price" id="freightPriceId">￥<%=order.OrderShippingFee > 0 ?  order.OrderShippingFee.ToString("f2") : "0"%></em>
                                </div>
                            </div>
                        </div>
                        <div class="fc-price-info text-right mb-20">
                            <span class="price-tit">Tổng số tiền phải trả：</span> <span class="price-num" id="sumPayPriceId">￥<%=order.OrderTotalAmount.ToString("f2") %></span>
                        </div>
                        <table class="table mt-20">
                            <tr class="active">
                                <td colspan="5" class="header">
                                    Xử lý đơn hàng
                                </td>
                            </tr>
                            <asp:Repeater ID="rpt_Log" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 15%">
                                            <%#Eval("Operator") %>
                                        </td>
                                        <td style="width: 30%">
                                            <%#Eval("ActionNote")%>
                                        </td>
                                        <td style="width: 40%">
                                            Chú ý/Vấn đề:
                                            <%#Eval("Remark") %>
                                        </td>
                                        <td style="width: 15%">
                                            <%#Eval("AutoTimeStamp")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
                <div class="tab-pane fade " id="Order_2">
                    <div class="profile-body mb-20 fix">
                        <table class="table tableed">
                            <tbody>
                                <tr>
                                    <td style="width: 160px;">
                                        <b>Người nhận thư</b>
                                    </td>
                                    <td>
                                        <%=order.Consignee %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Địa chỉ giao hàng</b>
                                    </td>
                                    <td>
                                        <%=order.Province%>，<%=order.City%>，<%=order.County%>，<%=order.Address%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Mã số bưu điện</b>
                                    </td>
                                    <td>
                                        <%=string.IsNullOrEmpty(order.PostCode) ? "Không nhập" : order.PostCode%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Điện thoại</b>
                                    </td>
                                    <td>
                                        <%=order.Mobile %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Thông tin giao nhận</b>
                                    </td>
                                    <td>
                                        <table class="table table-bordered">
                                            <tr class="active">
                                                <td>
                                                    Công ty giao nhận
                                                </td>
                                                <td>
                                                    Số đơn giao nhận
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%=order.ShippingName%>
                                                </td>
                                                <td style="width: 35%;">
                                                    <%=order.ShippingNo%>
                                                </td>
                                            </tr>
                                            <tr class="active">
                                                <td colspan="2">
                                                    Theo dõi giao nhận
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <%string kuaidi100url = GetKuaidi100Url();
                                                      if (order.OrderStatus < (int)SinGooCMS.OrderStatus.WaitSignGoods)
                                                      {%>
                                                    Đơn đặt hàng chưa vận chuyển
                                                    <%}%>
                                                    <%else if (string.IsNullOrEmpty(kuaidi100url))
{ %>
                                                    <span style="color: red">Không tìm thấy Thông tin giao nhận！</span>
                                                    <%}
else
{ %>
                                                    <iframe name="kuaidi100" src="<%=kuaidi100url%>" width="560" height="360" marginwidth="0"
                                                        marginheight="0" hspace="0" vspace="0" frameborder="0" scrolling="no"></iframe>
                                                    <%} %>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <input id="btncancel" onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
