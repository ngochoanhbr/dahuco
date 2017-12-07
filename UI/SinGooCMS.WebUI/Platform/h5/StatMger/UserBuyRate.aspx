<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="UserBuyRate.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.UserBuyRate" %>

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
                        Thống kê thành viên mua</h2>
                    <div class="profile-body mb-20 fix">
                        <table class="table tableed  table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="text-center" style="width: 80px;">
                                        Xếp hạng
                                    </th>
                                    <th>
                                        Tên thành viên
                                    </th>
                                    <th style="width: 120px;">
                                        Số lần đăng nhập
                                    </th>
                                    <th style="width: 120px;">
                                        Số lượng Các mặt hàng sưu tầm
                                    </th>
                                    <th style="width: 120px;">
                                        Số lượng Các mặt hàng mua
                                    </th>
                                    <th style="width: 120px;">
                                        Chi phí mua
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
                                                <%#Eval("UserName")%>
                                            </td>
                                            <td>
                                                <%#Eval("LoginCount")%>
                                            </td>
                                            <td>
                                                <%#Eval("Collectionquantity")%>
                                            </td>
                                            <td>
                                                <%#Eval("Buyquantity")%>
                                            </td>
                                            <td>
                                                ￥<%#Eval("Purchasespend")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <!--表格Nội dung end-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
