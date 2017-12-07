<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ShowCache.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Tools.ShowCache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid all">
        <div class="sidebar" id="left-panel">
            <ul class="nav">
                <li class="has-sub"><a href="/platform/h5/Tools/MyMessageBox.aspx"><i class="iconfont navbar-left">
                    </i><span>Tin nhắn</span> </a></li>
                <li class="has-sub"><a href="/platform/h5/Tools/ChangePwd.aspx"><i class="iconfont navbar-left">
                    </i><span>Chỉnh sửa mật khẩu</span> </a></li>
                <li class="has-sub"><a class="on" href="/platform/h5/Tools/ShowCache.aspx"><i class="iconfont navbar-left">
                    </i><span>Xem Cache</span> </a></li>
            </ul>
        </div>
        <div class="profile-wrapper">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="/Platform/h5/Main.aspx"><i class="iconfont mr5"></i> Home</a></li>
                <li class="active">Xem Cache</li>
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
                            <div class="checkall hidden">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group">
                                <asp:Button ID="btn_Clear" runat="server" Text="Xóa bộ nhớ cache" CssClass="btn btn-default"
                                    OnClick="btn_Clear_Click" />
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
                                    <th class="hidden">
                                    </th>
                                    <th>
                                        bộ nhớ cache từ khóa
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
                                            </td>
                                            <td>
                                                <%#Eval("CacheKey") %>
                                            </td>
                                            <td class="text-muted">
                                                <asp:LinkButton ID="lnkdel" Text="Clear cache" CssClass="del" runat="server" CommandArgument='<%#Eval("CacheKey") %>'
                                                    OnClick="lnkdel_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='3'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
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
