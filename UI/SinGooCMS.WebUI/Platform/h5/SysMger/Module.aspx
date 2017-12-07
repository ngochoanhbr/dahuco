<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="Module.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.Module" %>

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
                <li><a href="Menu.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View">
                    Quản lý cột</a></li>
                <li class="active"><a href="Module.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View">
                    Mô-đun quản lý</a></li>
            </ul>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix">
                            <label for="firstname" class="sr-only">
                                Phần</label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddl_lm" runat="server" class="form-control select">
                                </asp:DropDownList>
                            </div>
                            <label for="firstname" class="sr-only">
                                 từ khóa</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Vui lòng nhập tên mô-đun"></asp:TextBox>
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
                                <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="$.dialog.open('ModifyModule.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add',{title:'Thêm模块',width:680,height:410},false);">
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
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th  style="width: 60px;">
                                    </th>
                                    <th >
                                        Tên mô-đun
                                    </th>
                                    <th >
                                        mã Module
                                    </th>
                                    <th  style="width: 120px;">
                                        Thứ tự
                                    </th>
                                    <th  style="width: 100px;">
                                        Trường hệ thống
                                    </th>
                                    <th  style="width: 130px;">
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
                                                <%#Eval("ModuleName")%>
                                            </td>
                                            <td>
                                                <%#Eval("ModuleCode")%>
                                            </td>
                                            <td>
                                                <asp:TextBox style="width:80px;" runat="server" ID="txtsort" class="form-control text-center" Text='<%#Eval("Sort") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <%#Eval("IsSystem").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td>
                                                <a href="javascript:void(0)" onclick="$.dialog.open('ModifyModule.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>',{title:'Sửa模块',width:680,height:410});">
                                                    Sửa</a>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CommandArgument='<%#Eval("AutoID")%>' runat="server"
                                                    OnClientClick="return confirm('Bạn có chắc chắn xóa?')" OnClick="lnk_Delete_Click" />
                                                <a href="javascript:void(0)" onclick="$.dialog.open('SetOperation.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=SetOperate&opid=<%#Eval("AutoID") %>',{title:'设置[<%#Eval("ModuleName") %>]Xử lý种类',width:700,height:460},false);">
                                                    Xử lý thiết lập</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
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
