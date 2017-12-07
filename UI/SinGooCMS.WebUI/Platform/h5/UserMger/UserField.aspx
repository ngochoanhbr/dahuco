<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="UserField.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.UserField" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='UserGroup.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Nhóm thành viên</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Trường mở rộng</a></li>
            </ul>
            <div class="profile-body mb-20">
                <div class="form-group fix hidden">
                    <label for="firstname" class="sr-only">
                        singoocms</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="search_text" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:Button Text="Kích hoạt" ID="btn_Enabled" runat="server" OnClick="btn_Enabled_Click"
                            CssClass="btn btn-default ml20 fl" />
                        <asp:Button Text="Vô hiệu hóa" ID="btn_UnEnabled" runat="server" OnClick="btn_UnEnabled_Click"
                            CssClass="btn btn-default ml20 fl" />
                    </div>
                    <div class="btn-group">
                        <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                            class="btn btn-default ml20 fl" />
                    </div>
                    <div class="btn-group">
                        <input type="checkbox" id="ckViewUnEnabled" runat="server" class="checkbox_radio" />Hiển thị vô hiệu hóa
                    </div>
                    <div class="btn-group">
                        <button type="button" class="btn btn-warning" onclick="location='ModifyField.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&GroupID=<%=intModelID %>&action=Add'">
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th style="width: 60px;">
                                    </th>
                                    <th>
                                        Tên trường
                                    </th>
                                    <th>
                                        Tên hiển thị
                                    </th>
                                    <th>
                                        Kiểu
                                    </th>
                                    <th style="width: 120px;">
                                        Thứ tự
                                    </th>
                                    <th style="width: 100px;">
                                        Trường hệ thống
                                    </th>
                                    <th style="width: 100px;">
                                        Kích hoạt
                                    </th>
                                    <th style="width: 80px;">
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
                                                <%#Eval("FieldName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Alias")%>
                                            </td>
                                            <td>
                                                <%#SinGooCMS.Utility.EnumUtils.GetEnumDescription((SinGooCMS.FieldType)Convert.ToInt32( Eval("FieldType"))) %>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 80px;" runat="server" ID="txtsort" class="form-control text-center"
                                                    Text='<%#Eval("Sort") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <%#Eval("IsSystem").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td>
                                                <%#Eval("IsUsing").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td class="text-muted">
                                                <a href="javascript:void(0)" onclick="location='ModifyField.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&GroupID=<%=intModelID %>&opid=<%#Eval("AutoID") %>&action=Modify'">
                                                    Sửa</a>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CssClass="del" runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa?')"
                                                    CommandArgument='<%#Eval("AutoID") %>' OnClick="lnk_Delete_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='4'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="20" CssClass="paginator"
                        SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script type="text/javascript">
        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });
        $('#<%=ckViewUnEnabled.ClientID %>').on('ifChecked', function (event) {
            $("#<%=searchbtn.ClientID %>").click();
        });
        $('#<%=ckViewUnEnabled.ClientID %>').on('ifUnchecked', function (event) {
            $("#<%=searchbtn.ClientID %>").click();
        });
    </script>
</asp:Content>
