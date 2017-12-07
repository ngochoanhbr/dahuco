<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="VisitorLog.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.VisitorLog" %>

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
                        <div class="form-group fix">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Địa chỉ IP"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
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
                        <div class="batchHandleArea fix hidden">
                            <div class="checkall">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="$.dialog.open('ModifyCatalog.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add',{title:'Thêm menu',width:680,height:300},false);">
                                    Thêm</button>
                            </div>
                        </div>
                    </div>
                    <!--筛选条件 end-->
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th style="width:100px">
                                        IP
                                    </th>
                                    <th>
                                        Trang tiền thân
                                    </th>
                                    <th>
                                        Trang truy cập
                                    </th>
                                    <th style="width:150px">
                                        Thời gian
                                    </th>
                                    <th style="width: 60px">
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
                                                <input type="hidden" name="autoid" value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("IPAddress")%>
                                            </td>
                                            <td>
                                                <div class="ellipsis" style="width:300px;" data-toggle="tooltip" data-placement="top" title='<%#Eval("ApproachUrl")%>'>
                                                    <%#Eval("ApproachUrl")%>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="ellipsis" style="width:300px;" data-toggle="tooltip" data-placement="top" title='<%#Eval("VPage")%>'>
                                                    <%#Eval("VPage")%>
                                                </div>
                                            </td>
                                            <td>
                                                <%#Eval("AutoTimeStamp")%>
                                            </td>
                                            <td class="text-muted">
                                                <a class="show" href="VisitDetail.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&opid=<%#Eval("AutoID") %>">
                                                    Chi tiết</a>
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
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="10" CssClass="paginator"
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
    </script>
</asp:Content>
