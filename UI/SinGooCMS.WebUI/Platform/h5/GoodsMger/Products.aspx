<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.Products" %>

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
                <li <%=Status==1?"class='active'":"" %>><a href="javascript:;" data-toggle="tab"
                    onclick="location='Products.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&Status=1&action=View'">
                    Hàng Online</a></li>
                <li <%=Status==0?"class='active'":"" %>><a href="javascript:;" data-toggle="tab"
                    onclick="location='Products.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&Status=0&action=View'">
                    Hàng Offline</a></li>
            </ul>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Tên hàng hóa"></asp:TextBox>
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
                                <asp:Button Text="Kích hoạt" ID="btn_AuditOK" runat="server" OnClick="btn_AuditOK_Click"
                                    OnClientClick="return singoo.getCheckCount('rowItems')>0" CssClass="btn btn-default ml20 fl" />
                                <asp:Button Text="Vô hiệu" ID="btn_AuditCancel" runat="server" OnClick="btn_AuditCancel_Click"
                                    OnClientClick="return singoo.getCheckCount('rowItems')>0" CssClass="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <asp:DropDownList ID="ddlMoveTo" runat="server" CssClass="form-control fl" Style="width: 150px;">
                                </asp:DropDownList>
                            </div>
                            <div class="btn-group">
                                <asp:Button Text="Chuyển đến" ID="btn_MoveTo" runat="server" OnClick="btn_MoveTo_Click"
                                    OnClientClick="return singoo.getCheckCount('rowItems')>0" CssClass="btn btn-default fl" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="location='GoodsPulish.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add'">
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
                                    <th style="width: 60px;">
                                    </th>
                                    <th>
                                        Các mặt hàng
                                    </th>
                                    <th style="width: 100px;">
                                        Giá bán
                                    </th>
                                    <th style="width: 100px;">
                                        Đã bán/tổng
                                    </th>
                                    <th style="width: 120px;">
                                        Thứ tự
                                    </th>
                                    <th style="width: 100px;">
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
                                                <a href='/shop/goods/<%#Eval("AutoID") %>' target="_blank" data-toggle="tooltip" data-placement="top" title="点击浏览Các mặt hàng信息">
                                                    <img src='<%#Eval("ProImg").ToString()==""?"/Include/Images/waitupload.gif":SinGooCMS.Utility.WebUtils.GetThumb(Eval("ProImg").ToString()) %>'
                                                        alt="" style="max-width: 50px" />
                                                    <%#Eval("ProductName") %> (ID:#<%#Eval("AutoID") %>)
                                                </a>
                                            </td>
                                            <td>
                                                <%#SinGooCMS.Utility.WebUtils.GetDecimal(Eval("SellPrice")).ToString("C2")%>
                                            </td>
                                            <td>
                                                <%#Eval("Sales") %>
                                                /
                                                <%#Eval("Stock") %>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 80px;" runat="server" ID="txtsort" class="form-control text-center"
                                                    Text='<%#Eval("Sort") %>'></asp:TextBox>
                                            </td>
                                            <td class="text-muted">
                                                <a href="ModifyProduct.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>">
                                                    Sửa</a>
                                                <asp:LinkButton ID="lnk_Copy" Text="Copy" runat="server" OnClick="lnk_Copy_Click" CommandArgument='<%#Eval("AutoID") %>' />
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
    </div>
    <script type="text/javascript">
        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });
    </script>
</asp:Content>
