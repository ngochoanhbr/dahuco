<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.Account" %>

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
                        <div class="form-group fix hidden">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control" placeholder="Nhập Tên tài khoản"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
                            </div>
                        </div>
                        <div class="batchHandleArea fix">
                            <div class="checkall hidden">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group hidden">
                                <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="$.dialog.open('ModifyAccount.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add',{title:'Thêm帐户',width:580,height:325},false);">
                                    Thêm</button>
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
                        <table class="table tableed table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th>
                                        Tên tài khoản
                                    </th>
                                    <th style="width:150px">
                                        Email
                                    </th>
                                    <th style="width:150px">
                                        Số điện thoại
                                    </th>
                                    <th style="width:80px">
                                        Trường hệ thống
                                    </th>
                                    <th style="width:80px">
                                        Số lần đăng nhập
                                    </th>
                                    <th style="width:150px">
                                        Đăng nhập cuối
                                    </th>
                                    <th style="width:130px">
                                        Xử lý
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden">
                                                <input type="checkbox" class="checkbox_radio" />
                                                <input type="hidden" name="autoid" value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("AccountName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Email")%>
                                            </td>
                                            <td>
                                                <%#Eval("Mobile")%>
                                            </td>
                                            <td>
                                                <%#Eval("IsSystem").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22\">&#xe62e;</i>"%>
                                            </td>
                                            <td>
                                                <%#Eval("LoginCount")%>
                                            </td>
                                            <td>
                                                <%#Eval("LastLoginTime")%>
                                            </td>
                                            <td class="text-muted">
                                                <a href="javascript:void(0)" onclick="$.dialog.open('ModifyAccount.aspx?Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>',{title:'Sửa tài khoản',width:580,height:325},false);">
                                                    Sửa</a>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CssClass="del" runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa?')"
                                                    CommandArgument='<%#Eval("AutoID") %>' OnClick="lnk_Delete_Click" />
                                                <a href="javascript:void(0)" class="set" onclick="if('superadmin'=='<%#Eval("AccountName").ToString().ToLower() %>') showtip('Bạn không thể đặt vai trò superadmin'); else $.dialog.open('SetRoles.aspx?Module=<%=base.CurrentModuleCode %>&action=SetRole&opid=<%#Eval("AutoID") %>',{title:'Thiết lập Vai trò',width:680,height:350},false);">
                                                    Thiết lập Vai trò</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='8'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <!--表格Nội dung end-->
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="10" CssClass="paginator"
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
