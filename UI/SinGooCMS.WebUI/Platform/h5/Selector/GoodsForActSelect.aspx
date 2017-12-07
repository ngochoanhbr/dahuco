<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="GoodsForActSelect.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Selector.GoodsForActSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableed > tbody > tr > td
        {
            line-height: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content">
        <div class="fix white-bg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body fix">
                        <div class="form-group fix">
                            <div class="col-md-3 fl">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Tên hàng hóa"></asp:TextBox>
                            </div>
                            <div class="col-md-3 fl">
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
                            </div>
                        </div>
                    </div>
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="5" CssClass="paginator"
                        SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                    <div class="profile-body fix">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th style="width: 60px;">
                                    </th>
                                    <th>
                                        Các mặt hàng
                                    </th>
                                    <th>
                                        Giá
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type='<%=(SelectType=="single"?"radio":"checkbox") %>' name="inputcheck" id='<%#Eval("AutoID") %>'
                                                    title="Lựa chọn thiết bị" value='<%# Eval("ProductName") %>' class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <img src='<%#Eval("ProImg") %>' alt="" style="max-width:50px;max-height:30px;" />
                                                <%#Eval("ProductName")%>
                                            </td>
                                            <td>
                                                <%#SinGooCMS.Utility.WebUtils.GetDecimal(Eval("SellPrice")).ToString("C2")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="profile-body" style="clear: both;">
            <div class="datafrom text-right">
                <input type="button" id="btnselect" value="Xác nhận " onclick="selectOk()" class="btn btn-danger" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
    <script src="/platform/h5/js/UpdatePanelScript/SelectCommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
            $.getScript("/platform/h5/js/UpdatePanelScript/SelectCommon.js");
        });
    </script>
</asp:Content>
