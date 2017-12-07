<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="GoodsClass.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.GoodsClass" %>

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
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
                            </div>
                        </div>
                        <div class="batchHandleArea fix">
                            <div class="checkall hidden">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group hidden">
                                <asp:LinkButton ID="btn_DelBat" runat="server" OnClick="btn_DelBat_Click" Text="Xóa"
                                    class="btn btn-default ml20 fl" OnClientClick="return singoo.getCheckCount('rowItems')>0 && confirm('OK để xóa nó? \r\ sẽ xóa tất cả các mục đã chọn 无 thể được phục hồi, xin vui lòng thận trọng');" />
                            </div>
                            <div class="btn-group">
                                <a id="openAll" class="btn btn-info btn-sm">Hiển thị tất cả danh mục<span class="glyphicon glyphicon-plus-sign"
                                    aria-hidden="true"></span></a> <a id="closeAll" class="btn btn-info btn-sm">Ẩn tất cả danh mục<span
                                        class="glyphicon glyphicon-minus-sign" aria-hidden="true"></span></a>
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="location='ModifyGoodsClass.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add'">
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
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th>
                                        Tên danh mục
                                    </th>
                                    <th style="width: 120px;">
                                        Thứ tự
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
                                            <td class="hidden">
                                                <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <span class='CateLevel<%#Eval("Depth") %>'>
                                                    <ico style="cursor: pointer;" class='glyphicon glyphicon-minus-sign' <%#SinGooCMS.Utility.WebUtils.GetInt(Eval("ChildCount")).Equals(0)?"canexpand='0'":"canexpand='1'"%>
                                                        depth='<%#Eval("Depth") %>'></ico>
                                                    <%#SinGooCMS.Utility.WebUtils.GetInt(Eval("ParentID")).Equals(0) ? "<b>" + Eval("ClassName").ToString() + "</b>" : Eval("ClassName").ToString()%>
                                                </span>
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 80px;" runat="server" ID="txtsort" class="form-control text-center"
                                                    Text='<%#Eval("Sort") %>'></asp:TextBox>
                                            </td>
                                            <td class="text-muted">
                                                <a href="javascript:void(0)" onclick="location='ModifyGoodsClass.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>'">
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
        //以下是扩展与收缩按钮事件
        //Ẩn tất cả danh mục
        $("#closeAll").click(function () {
            $("#rowItems tr").each(function (index, item) {
                if (index != 0 && $(this).html().indexOf("depth=\"1\"") < 0) { $(item).hide(); }
            })
            $("#rowItems tr td ico[depth=\"1\"][canExpand='1']").attr("class", "glyphicon glyphicon-plus-sign");
        });
        //Hiển thị tất cả danh mục
        $("#openAll").click(function () {
            $("#rowItems tr").each(function (index, item) {
                if (index != 0 && $(this).html().indexOf("depth=\"1\"") < 0) { $(item).show(); }
            })
            $("#rowItems tr td span ico[canExpand='1']").attr("class", "glyphicon glyphicon-minus-sign");
        });
        $("#rowItems ico[canexpand='1']").click(function () {
            var currentTrNode = $(this).parents("tr").next();
            var isexpand = $(this).attr("class") == "glyphicon glyphicon-plus-sign"; //是否展开
            while (true) {
                if (typeof (currentTrNode.html()) != "string" || parseInt($(currentTrNode).find("ico").attr("depth")) <= parseInt($(this).attr("depth"))) { break; }
                if (isexpand) { currentTrNode.show(); } else { currentTrNode.hide() };
                currentTrNode = currentTrNode.next();
            }
            $(this).attr("class", (isexpand ? "glyphicon glyphicon-minus-sign" : "glyphicon glyphicon-plus-sign"));
        });
    </script>
</asp:Content>
