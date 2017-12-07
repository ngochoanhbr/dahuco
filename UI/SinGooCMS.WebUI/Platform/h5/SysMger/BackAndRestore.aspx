<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="BackAndRestore.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.BackAndRestore" %>

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
            <div class="profile-body mb-20">
                <div class="form-group fix">
                    <label for="firstname" class="sr-only">
                        singoocms</label>
                    <div class="col-md-3">
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
                        <asp:Button ID="btn_CreateDBBack" runat="server" Text="Tạo một bản sao lưu cơ sở dữ liệu" OnClick="btn_CreateDBBack_Click"
                            class="btn btn-default ml20 fl" />
                        <asp:Button ID="btn_CreateSiteBack" runat="server" Text="Tạo một bản sao lưu của toàn bộ trạm" OnClick="btn_CreateSiteBack_Click"
                            class="btn btn-default ml20 fl" />
                        <asp:Button ID="btn_CreateTempateBack" runat="server" Text="Sao lưu bản mẫu hiện tại" OnClick="btn_CreateTempateBack_Click"
                            class="btn btn-default ml20 fl" />
                        <asp:Button ID="btn_CreateUploadBack" runat="server" Text="Tạo một bản sao lưu của tập tin tải lên" OnClick="btn_CreateUploadBack_Click"
                            class="btn btn-default ml20 fl" />
                    </div>
                    <div class="btn-group">
                    </div>
                    <div class="fr">
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <!--筛选条件 end-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th >
                                        tên backup
                                    </th>
                                    <th >
                                        Kích thước tập tin
                                    </th>
                                    <th >
                                        Kiểu sao lưu
                                    </th>
                                    <th style="width:150px">
                                        Thời gian thời gian
                                    </th>
                                    <th style="width:100px">
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
                                            </td>
                                            <td>
                                                <%#Eval("BakFileName")%>
                                            </td>
                                            <td>
                                                <%#Eval("BakFileSize")%>
                                            </td>
                                            <td>
                                                <%#GetBakFileType(Eval("BakFileName").ToString().ToLower())%>
                                            </td>
                                            <td class="text-warning">
                                                <%#Eval("UploadDate")%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CommandArgument='<%#Eval("BakFilePath") %>'
                                                    runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa?')" OnClick="lnk_Delete_Click" />
                                                <a href='/include/Download.aspx?file=<%#SinGooCMS.Utility.DEncryptUtils.DESEncode(Eval("VirtualPath").ToString())%>'
                                                    target="_blank">Tải về</a>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--分页 end-->
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script type="text/javascript">
        $("#<%=btn_CreateDBBack.ClientID %>,#<%=btn_CreateSiteBack.ClientID %>,#<%=btn_CreateTempateBack.ClientID %>,#<%=btn_CreateUploadBack.ClientID %>").click(function () {
            $.dialog({ title: "Được sao lưu", content: "<img src='/Include/Images/loading.gif' /> Đang tạo file backup.... Đừng đóng trang này" });
        });
    </script>
</asp:Content>
