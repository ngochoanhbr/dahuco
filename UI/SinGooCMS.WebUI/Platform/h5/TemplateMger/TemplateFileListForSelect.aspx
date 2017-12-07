<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="TemplateFileListForSelect.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.TemplateMger.TemplateFileListForSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .table>tr>td {padding:3px 5px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <!--Nội dung-->
            <table id="tablelist" class="table">
                <tr class="headerStyle">
                    <th>
                       tên Thư mục (tập tin) 
                    </th>
                    <th style="width: 50px;">
                        Kiểu
                    </th>
                    <th style="width: 60px;">
                        kích thước
                    </th>
                    <th style="width: 150px;">
                        Thời gian chỉnh sửa
                    </th>
                    <th style="width: 60px">
                        Xử lý
                    </th>
                </tr>
                <tr class="tr2" style="display: <%=ViewUp%>;">
                    <td colspan="5" style="cursor: pointer;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img src="../Images/up2.gif" width="18" height="18" alt="" />
                                </td>
                                <td>
                                    <a href="<%=GetParentFolder() %>">thư mục cha</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <asp:Repeater ID="list_folder" runat="server">
                    <ItemTemplate>
                        <tr class="rowStyle">
                            <td>
                                <img src="../Images/Folder/folder.gif" height="16" alt="" />
                                <a href="?Module=<%= base.CurrentModuleCode %>&folder=<%=GetBaseFolder()%><%# Eval("Name")%>&elementid=<%=strElementID %>">
                                    <%# Eval("Name")%></a>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <%# ((DateTime)Eval("LastAccessTime")).ToString("yyyy-MM-dd hh:mm:ss") %>
                            </td>
                            <td>
                                <span style="display: none">
                                    <asp:LinkButton ID="lnkDelFolder" Style="color: #aaa" OnClientClick="return confirm('Bạn có chắc chắn xóa?Xóa后不可恢复')"
                                        runat="server" Text="Xóa" CommandName="DelFolder" CommandArgument='<%#Eval("Name") %>'></asp:LinkButton></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="tr3">
                    <td colspan="6" style="height: 6px;">
                    </td>
                </tr>
                <asp:Repeater ID="list_file" runat="server">
                    <ItemTemplate>
                        <tr class="rowStyle">
                            <td>
                                <img src="../Images/Folder/<%#GetIcon(Eval("Extension").ToString()) %>" width="20"
                                    height="16">
                                <%#GetDisplayLink(Eval("Name").ToString(), Eval("Extension").ToString(), Eval("Name").ToString())%>
                            </td>
                            <td>
                                <%# Eval("Extension")%>
                            </td>
                            <td>
                                <%# GetFileSize( Convert.ToDecimal(Eval("Length").ToString())) %>
                            </td>
                            <td>
                                <%# Eval("LastAccessTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                            </td>
                            <td>
                                <%#GetDisplayLink(Eval("Name").ToString(),Eval("Extension").ToString(),"Selected")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="tr3">
                    <td colspan="6" style="height: 6px;">
                    </td>
                </tr>
            </table>
            <!--Nội dungend-->
        </div>
    </div>
    <script type="text/javascript">
        singoo.initRequest();
        //需要赋值的控件ID
        var elementID = singoo.request["elementid"];
        $("a[title='Selected']").click(function () {
            if (elementID) {
                var win = $.dialog.open.origin; //来源页面
                win.document.getElementById(elementID).value = $(this).attr("id");
            }
            else
                alert("Không tham số kiểm soát ID");

            $.dialog.close();
        });
    </script>
</asp:Content>
