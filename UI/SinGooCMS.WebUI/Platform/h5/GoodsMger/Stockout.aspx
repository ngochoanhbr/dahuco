﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="Stockout.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.Stockout" %>

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
                <li <%=kcType.Equals(0)?"class='active'":"" %>><a href="Stockout.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&type=0&action=View">
                    Cảnh báo tồn kho</a></li>
                <li <%=kcType.Equals(1)?"class='active'":"" %>><a href="Stockout.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&type=1&action=View">
                    Hết hàng trong kho</a></li>                
            </ul>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix hidden">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control" placeholder="Tên hàng hóa"></asp:TextBox>
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
                            <div class="btn-group">
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
                                    <th >
                                        Tên hàng hóa
                                    </th>
                                    <th>
                                        Đăng ký tồn kho
                                    </th>
                                    <th >
                                        Người đặt
                                    </th>
                                    <th style="width: 150px;">
                                        Thời gian
                                    </th>
                                    <th style="width: 60px;">
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
                                                <%#Eval("ProName")%>
                                            </td>
                                            <td>
                                                <%#Eval("CurrStock")%>
                                            </td>
                                            <td>
                                                <%#Eval("UserName") %>
                                            </td>
                                            <td>
                                                <%#Eval("AutoTimeStamp") %>
                                            </td>
                                            <td class="text-muted">
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CssClass="del" runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa?')"
                                                    CommandArgument='<%#Eval("AutoID") %>' OnClick="lnk_Delete_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='6'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
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
