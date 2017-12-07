<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.TemplateMger.TemplateList" %>

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
                <button type="button" class="btn btn-warning" onclick="location='ModifyTemplate.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add'">
                    Thêm Bản mẫu</button>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20 filemanager fix">
                        <div class="template-list clear-fix">
                            <ul>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="p-img">
                                                <a href="ModifyTemplate.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>">
                                                    <img src="<%#Eval("ShowPic").ToString()==String.Empty?"/Include/Images/nophoto.jpg":Eval("ShowPic").ToString() %>"
                                                        alt="Bản mẫu" style="width: 305px; height: 230px" />
                                                </a>
                                            </div>
                                            <div class="p-list">
                                                <div class="p-name">
                                                    <%#Eval("TemplateName") %>
                                                </div>
                                                <div class="list">
                                                    <span>
                                                        <a href="TemplateFileList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&opid=<%#Eval("AutoID") %>">
                                                            <i class="iconfont">&#xe68d;</i> Danh sách tập tin</a> </span>
                                                        <span>
                                                            <asp:LinkButton ID="btn_SetDefault" runat="server" OnClientClick="return confirm('Sử dụng các thiết lập mặc định cho bản mẫu hiện tại, OK??')"
                                                                Text="<i class='iconfont'>&#xe68e;</i> thiết lập mặc định" CommandArgument='<%#Eval("AutoID") %>'
                                                                OnClick="btn_SetDefault_Click" CssClass="set"></asp:LinkButton>
                                                        </span>
                                                        <span>
                                                            <asp:LinkButton ID="lnk_Delete" CommandName="delete" CommandArgument='<%#Eval("AutoID") %>'
                                                                Text="<i class='iconfont'>&#xe684;</i> Xóa" runat="server" OnClientClick="return confirm('Bạn có chắc chắn xóa?')" OnClick="lnk_Delete_Click" />
                                                        </span>
                                                </div>
                                            </div>
                                            <div class="deftemplate">
                                                <%#Eval("IsDefault").ToString()=="True"?"<i class='iconfont'>&#xe689;</i>":""%>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
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
    </script>
</asp:Content>
