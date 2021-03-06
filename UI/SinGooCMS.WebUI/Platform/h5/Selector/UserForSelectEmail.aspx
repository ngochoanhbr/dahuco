﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="UserForSelectEmail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Selector.UserForSelectEmail" %>

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
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Vui lòng nhập tên đăng nhập"></asp:TextBox>
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
                                        Tên thành viên
                                    </th>
                                    <th>
                                        Email
                                    </th>
                                    <th>
                                        Thời gian đăng ký
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type='<%=(SelectType=="single"?"radio":"checkbox") %>' name="inputcheck" id='<%#Eval("AutoID") %>'
                                                    title="Lựa chọn 器" value='<%# Eval("Email") %>' class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("UserName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Email")%>
                                            </td>
                                            <td>
                                                <%#Eval("AutoTimeStamp")%>
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
