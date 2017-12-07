<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ThirdLogin.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.ThirdLogin" %>

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
                    <div class="profile-body mb-20 hidden">
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
                            <div class="checkall">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group">
                            </div>
                            <div class="btn-group">
                            </div>
                            <div class="btn-group">
                            </div>
                        </div>
                    </div>
                    <!--筛选条件 end-->
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th style="width: 150px;">
                                        Loại đăng nhập
                                    </th>
                                    <th>
                                        Giải thích
                                    </th>
                                    <th style="width: 100px;">
                                        Kích hoạt
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
                                                <img src='<%#Eval("ShowImg") %>' alt='<%#Eval("OAuthName") %>' /><br />
                                                <%#Eval("OAuthName") %>
                                            </td>
                                            <td>
                                                <%#Eval("Remark") %>
                                                <p>
                                                    <br />
                                                    <a href='<%#Eval("ApplyUrl") %>' target="_blank" class="btn btn-default">Đăng ký trực tuyến</a></p>
                                            </td>
                                            <td>
                                                <%#Eval("IsEnabled").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                            </td>
                                            <td class="text-muted">
                                                <a href="javascript:void(0)" onclick="$.dialog.open('ThirdLoginConfig.aspx?Module=<%=base.CurrentModuleCode %>&action=Modify&key=<%#Eval("OAuthKey") %>',{title:'Đăng nhập cấu hình',width:580,height:220},false);">
                                                    Cấu hình</a>
                                                <asp:LinkButton ID="btn_Switch" CommandName="Switch" CommandArgument='<%#Eval("OAuthKey") %>'
                                                    runat="server" class="edit" OnClick="lnk_Switch_Click"><%#Eval("IsEnabled").ToString()=="True"?"Close":"Open"%></asp:LinkButton>
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
