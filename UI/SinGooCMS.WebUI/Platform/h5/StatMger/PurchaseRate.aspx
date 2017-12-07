<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="PurchaseRate.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.PurchaseRate" %>

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
            <div class="profile-body mb-20 fix">
                <div class="datafrom">
                    <h2 class="title">
                        Tỷ lệ mua hàng</h2>
                    <div class="profile-body mb-20 fix">
                        <table class="table tableed table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="text-center" style="width:80px;">
                                        Xếp hạng
                                    </th>
                                    <th>
                                        Tên hàng hóa
                                    </th>
                                    <th style="width:80px">
                                        Số lần duyệt qua
                                    </th>
                                    <th style="width:80px">
                                        Phổ biến
                                    </th>
                                    <th style="width:80px">
                                        Số lần mua hàng
                                    </th>
                                    <th style="width:80px">
                                        tỷ lệ mua
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                 <strong><%#Container.ItemIndex < 3 ? "<img src='../Images/jiangzhang/jz" + (Container.ItemIndex + 1) + ".gif' alt='' />" : (Container.ItemIndex + 1).ToString()%> </strong>
                                            </td>
                                            <td>
                                                <%#Eval("ProductName") %>
                                            </td>
                                            <td>
                                                <%#Eval("Views")%>
                                            </td>
                                            <td>
                                                <%#Eval("Popularity")%>
                                            </td>
                                            <td>
                                                <%#Eval("Purchases")%>
                                            </td>
                                            <td>
                                                <%#Eval("Rate")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
