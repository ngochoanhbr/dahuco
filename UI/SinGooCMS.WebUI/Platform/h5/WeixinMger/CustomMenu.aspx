<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="CustomMenu.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.WeixinMger.CustomMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid all">
        <div class="sidebar" id="left-panel">
        </div>
        <div class="profile-wrapper" style="position: relative">
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
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control" placeholder="Vui lòng nhập tên menu"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                            </div>
                        </div>
                        <div class="batchHandleArea fix">
                            <div class="checkall hidden">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group">
                                <a id="openAll" class="btn btn-info btn-sm">Hiển thị tất cả danh mục<span class="glyphicon glyphicon-plus-sign"
                                    aria-hidden="true"></span></a> <a id="closeAll" class="btn btn-info btn-sm">Ẩn tất cả danh mục<span
                                        class="glyphicon glyphicon-minus-sign" aria-hidden="true"></span></a>
                            </div>
                            <div class="btn-group">
                                <asp:Button ID="reloadX" Text="Cập nhật lại" runat="server" OnClick="reloadX_Click" class="btn btn-default" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="$.dialog.open('ModifyCustomMenu.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add',{title:'Thêm菜单',width:680,height:450},false);">
                                    Thêm</button>
                            </div>
                        </div>
                    </div>
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th style="width: 200px;">
                                        Menu Name
                                    </th>
                                    <th>
                                        Ứng phó với các sự kiện
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
                                                <asp:CheckBox ID="chk" runat="server" class="checkbox_radio hidden" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                                #<%#Eval("AutoID") %>
                                            </td>
                                            <td>
                                                <span class='CateLevel<%#Eval("ParentID").ToString()=="0"?"1":"2" %>'>
                                                    <ico style="cursor: pointer;" class='glyphicon glyphicon-minus-sign' <%#SinGooCMS.Utility.WebUtils.GetInt(Eval("ChildCount")).Equals(0)?"canexpand='0'":"canexpand='1'"%>
                                                        depth='<%#Eval("ParentID").ToString()=="0"?"1":"2" %>'></ico>
                                                    <%#SinGooCMS.Utility.WebUtils.GetInt(Eval("ParentID")).Equals(0) ? "<b>" + Eval("Name").ToString() + "</b>" : Eval("Name").ToString()%>
                                                    (ID:#<%#Eval("AutoID") %>) </span>
                                            </td>
                                            <td>
                                                <%#Eval("Type").ToString()=="click"?"Graphic push":"Address jump："+Eval("Url").ToString()%>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Style="width: 80px;" ID="txtsort" class="form-control text-center"
                                                    Text='<%#Eval("Sort") %>'></asp:TextBox>
                                            </td>
                                            <td class="text-muted">
                                                <a href="javascript:void(0)" onclick="$.dialog.open('ModifyCustomMenu.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>',{title:'Chỉnh sửa 菜单',width:680,height:450},false);">Sửa</a>
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
                    <div class="profile-body mb-20">
                        <div class="datafrom text-right">
                            <asp:Button ID="btnok" Text="Update to the server" runat="server" OnClick="btnok_Click" class="btn btn-danger" OnClientClick="return confirm('The update will replace the current WeChat menu，OK？')"/>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
