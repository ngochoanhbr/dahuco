﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ShippingAddr.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.ShippingAddr" %>

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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Vui lòng nhập tên đăng nhập"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
                            </div>
                        </div>
                        <div class="batchHandleArea fix">
                            <div class="checkall">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton ID="btn_DelBat" runat="server" OnClick="btn_DelBat_Click" Text="Xóa"
                                    class="btn btn-default ml20 fl" OnClientClick="return singoo.getCheckCount('rowItems')>0 && confirm('OK để xóa nó? \r\ sẽ xóa tất cả các mục đã chọn 无 thể được phục hồi, xin vui lòng thận trọng');" />
                            </div>
                            <div class="btn-group">
                                
                            </div>
                            <div class="btn-group hide">
                                
                            </div>
                            <div class="fr">
                                <div class="col-md-12">
                                    <asp:DropDownList class="form-control select" ID="drpPageSize" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--筛选条件 end-->
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th style="width:60px;">
                                    </th>
                                    <th>
                                        Thành viên
                                    </th>
                                    <th>
                                        Người nhận thư
                                    </th>
                                    <th >
                                        Địa chỉ giao hàng
                                    </th>
                                    <th >
                                        Điện thoại
                                    </th>
                                    <th  style="width: 80px;">
                                        Địa chỉ mặc định
                                    </th>
                                    <th  style="width: 80px;">
                                        Xử lý
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("UserName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Consignee")%>
                                            </td>
                                            <td>
                                                <%#Eval("Country")%>，<%#Eval("Province")%>，<%#Eval("City")%>，<%#Eval("County")%>，<%#Eval("Address")%>
                                            </td>
                                            <td>
                                                <%#Eval("ContactPhone") %>
                                            </td>
                                            <td>
                                                <%#Eval("IsDefault").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td class="text-muted">
                                                <a href="ModifyShippingAddr.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>">Sửa</a>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CssClass="del" runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa?')"
                                                    CommandArgument='<%#Eval("AutoID") %>' OnClick="lnk_Delete_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='7'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <!--表格Nội dung end-->
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="20" CssClass="paginator"
                        SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                    <!--分页 end-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script type="text/javascript">
        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });
    </script>
</asp:Content>
