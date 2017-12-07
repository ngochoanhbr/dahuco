<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="TemplateFileList.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.TemplateMger.TemplateFileList" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='TemplateList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Quản lý bản mẫu</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Danh sách tập tin</a></li>
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
                    </div>
                </div>
                <div class="batchHandleArea fix">
                    <div class="checkall hidden">
                        <input type="checkbox" id="checkall" class="checkbox_radio" />
                    </div>
                    <div class="btn-group">
                        <a href="javascript:void(0);" class="btn btn-warning" onclick="openEditorDialog();">Thêm hồ sơ Bản mẫu</a>
                    </div>
                </div>
                <asp:Literal ID="lblMsg" runat="server"></asp:Literal>
                <table class="table">
                    <tr class="headerStyle">
                        <th style="width: 50%;">
                            Thư mục (tập tin) tên
                        </th>
                        <th style="width: 10%;">
                            Kiểu
                        </th>
                        <th style="width: 10%;">
                            kích thước
                        </th>
                        <th style="width: 20%;">
                            Thời gian chỉnh sửa cuối cùng
                        </th>
                        <th style="width: 10%;">
                            Xử lý
                        </th>
                    </tr>
                    <tr class="tr2" style="display: <%=ViewUp%>;">
                        <td colspan="5" style="cursor: pointer;">
                            <table class="table">
                                <tr>
                                    <td>                                        
                                        <a href="<%=GetParentFolder() %>"><img src="../Images/up2.gif" width="18" height="18" alt="" /> thư mục cha</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <asp:Repeater ID="list_folder" runat="server" OnItemCommand="list_folder_ItemCommand">
                        <ItemTemplate>
                            <tr class="rowStyle">
                                <td>
                                    <img src="../Images/Folder/folder.gif" height="16" alt="" />
                                    <a href="?CatalogID=<%=base.CurrentCatalogID %>&Module=<%= base.CurrentModuleCode %>&folder=<%=GetBaseFolder()%><%# Eval("Name")%>&action=View&opid=<%=OpID %>">
                                        <%# Eval("Name")%></a>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <%# ((DateTime)Eval("LastWriteTime")).ToString("yyyy-MM-dd hh:mm:ss")%>
                                </td>
                                <td>
                                    <span style="display: none">
                                        <asp:LinkButton ID="lnkDelFolder" Style="color: #aaa" OnClientClick="return confirm('Bạn có chắc chắn xóa?Xóa không thể phục hồi được')"
                                            runat="server" Text="Xóa" CommandName="DelFolder" CommandArgument='<%#Eval("Name") %>'></asp:LinkButton></span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="tr3">
                        <td colspan="6" style="height: 6px;">
                        </td>
                    </tr>
                    <asp:Repeater ID="list_file" runat="server" OnItemCommand="list_file_ItemCommand">
                        <ItemTemplate>
                            <tr class="rowStyle">
                                <td>
                                    <%#GetFileNameLink(Eval("Name").ToString(), Eval("Extension").ToString())%>
                                </td>
                                <td>
                                    <%# Eval("Extension")%>
                                </td>
                                <td>
                                    <%#SinGooCMS.Utility.FileUtils.GetFileSize(SinGooCMS.Utility.WebUtils.GetDecimal(Eval("Length"))) %>
                                </td>
                                <td>
                                    <%# Eval("LastWriteTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%#GetDisplayLink(Eval("Name").ToString(),Eval("Extension").ToString())%>
                                    <asp:LinkButton Style="color: #aaa" OnClientClick="return confirm('Bạn có chắc chắn xóa?Xóa không thể phục hồi được')"
                                        CommandName="DelFile" ID="lnkDelFile" Text="Xóa" runat="server" CommandArgument='<%#Eval("Name") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="tr3">
                        <td colspan="6" style="height: 6px;">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <input id="btncancel" onclick="location='TemplateList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                    type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script type="text/javascript">
        $("a[title='Sửa nội dung']").click(function () {
            var vFileName = $(this).attr("id");
            $.dialog.open('TemplateEditor.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&folder=<%=GetBaseFolder()%>&opid=<%=base.OpID%>&file=' + vFileName, { title: 'Sửa Bản mẫu', width: '90%', height: '90%', fixed: true }, false);
        });
        function openEditorDialog() {
            $.dialog.open('TemplateEditor.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add&folder=<%=GetBaseFolder()%>&opid=<%=base.OpID %>', { title: 'Thêm Bản mẫu', width: '90%', height: '90%', fixed: true }, false);
        }
    </script>
</asp:Content>
