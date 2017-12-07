<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="OrderAction.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.OrderMger.OrderAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <asp:Panel ID="panelGJ" runat="server" Visible="false">
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>Số tiền đơn hàng:</span>
                        <div class="w400 fl">
                            <jweb:H5TextBox Mode="Number" ID="newamount" runat="server" CssClass="form-control"
                                required="required" step="0.01"></jweb:H5TextBox>
                            Giá hiện nay：<%=order.OrderTotalAmount.ToString("f2") %>
                        </div>
                    </li>
                </asp:Panel>
                <asp:Panel ID="panelPay" runat="server" Visible="false">
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>Thanh toán trực tuyến:</span>
                        <div class="w400 fl">
                            <asp:DropDownList ID="paymenttype" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </li>
                </asp:Panel>
                <asp:Panel ID="panelCompnay" runat="server" Visible="false">
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>Công ty vận chuyển:</span>
                        <div class="w400 fl">
                            <asp:DropDownList ID="kuaidicompany" runat="server">
                            </asp:DropDownList>
                        </div>
                    </li>
                </asp:Panel>
                <asp:Panel ID="panelNo" runat="server" Visible="false">
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>số vận đơn:</span>
                        <div class="w400 fl">
                            <asp:TextBox ID="shippingno" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </li>
                </asp:Panel>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Xử lý vấn đề:</span>
                    <div class="w400 fl">
                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" Columns="60" ID="txtRemark"
                            CssClass="form-control" required="required" placeholder="Vui lòng nhập 1 chủ đề"></asp:TextBox>
                    </div>
                </li>
            </ul>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <asp:Button OnClientClick="return confirm('Bạn muốn xử lý??')" Visible="false" ID="btn_Confirm"
                    OnClick="btn_Confirm_Click" runat="server" Text="Review" CssClass="btn btn-danger" />
                <asp:Button OnClientClick="return confirm('Bạn muốn xử lý??')" Visible="false" ID="btn_ChangeAmount"
                    OnClick="btn_ChangeAmount_Click" runat="server" Text="Thay đổi giá" CssClass="btn btn-danger" />
                <asp:Button OnClientClick="return confirm('Bạn muốn xử lý??')" Visible="false" ID="btn_PayOk"
                    OnClick="btn_PayOk_Click" runat="server" Text="Các khoản phải thu" CssClass="btn btn-danger" />
                <asp:Button OnClientClick="return confirm('Bạn muốn xử lý??')" Visible="false" ID="btn_SendGoods"
                    OnClick="btn_SendGoods_Click" runat="server" Text="Giao hàng" CssClass="btn btn-danger" />
                <asp:Button OnClientClick="return confirm('Bạn muốn xử lý??')" Visible="false" ID="btn_Delivered"
                    OnClick="btn_Delivered_Click" runat="server" Text="Biên lai" CssClass="btn btn-danger" />
                <asp:Button OnClientClick="return confirm('Bạn muốn xử lý??')" Visible="false" ID="btn_Cancel"
                    OnClick="btn_Cancel_Click" runat="server" Text="Close" CssClass="btn btn-danger" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
