<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="GoodsQADetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.GoodsQADetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle">Tư vấn Các mặt hàng:</span>
                    <div class="w600 fl">
                        <%=qa.ProductName %>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Tư vấn:</span>
                    <div class="w600 fl">
                        <textarea class="form-control" rows="3" cols="80" disabled="disabled"><%=qa.Question %></textarea>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Đến từ:</span>
                    <div class="w600 fl">
                        Người sử dụng：<%=qa.UserName %>
                        Thời gian：<%=qa.AutoTimeStamp %>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Trả lời:</span>
                    <div class="w600 fl">
                        <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" Rows="3" Columns="80"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                </li>
            </ul>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>