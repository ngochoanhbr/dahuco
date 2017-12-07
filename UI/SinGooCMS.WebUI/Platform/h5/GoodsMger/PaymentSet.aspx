<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="PaymentSet.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.PaymentSet" %>

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
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
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
                                <button type="button" class="btn btn-warning" onclick="location='ModifyPaymentSet.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add'">
                                    Thêm</button>
                            </div>
                            <div class="fr">
                                <div class="col-md-12">
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
                                        Thanh toán
                                    </th>
                                    <th >
                                        Mã thanh toán
                                    </th>
                                    <th style="width: 100px;">
                                        Thanh toán trực tuyến
                                    </th>
                                    <th style="width: 100px;">
                                        Di động
                                    </th>
                                    <th style="width: 100px;">
                                        Wechat
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
                                                <%#Eval("DisplayName")%>
                                            </td>
                                            <td>
                                                <%#Eval("PayCode")%>
                                            </td>
                                            <td>
                                                <%#Eval("IsOnline").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td>
                                                <%#Eval("IsMobile").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td>
                                                <%#Eval("IsWeixin").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td class="text-muted">
                                                <a href="ModifyPaymentSet.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>">
                                                    Sửa</a>
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
