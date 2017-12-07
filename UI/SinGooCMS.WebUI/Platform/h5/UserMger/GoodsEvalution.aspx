<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="GoodsEvalution.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.GoodsEvalution" %>

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
                <li <%=status==0?"class='active'":"" %>><a href="javascript:;" onclick="location='GoodsEvalution.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&status=0&action=View'"
                    data-toggle="tab">无 trả lời</a></li>
                <li <%=status==1?"class='active'":"" %>><a href="javascript:;" onclick="location='GoodsEvalution.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&status=1&action=View'"
                    data-toggle="tab">Đã trả lời</a></li>
            </ul>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix hidden">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder=""></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
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
                    <!--筛选条件 end-->
                    <div class="profile-body mb-20 fix">
                        <div class="p_replyed">
                            <div class="p_replyed_title">
                                <span class="p_replyed_user">Người sử dụng đánh giá</span> <span class="p_replyed_por">Đánh giá sản phẩm</span>
                                <span class="p_replyed_time">Thời gian đánh giá</span> <span class="p_replyed_operation">Xử lý</span>
                            </div>
                        </div>
                        <div id="rowItems">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="order_hover">
                                        <div class="replyed_info_title">
                                            <div class="replyed_info_use">
                                                <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                                <%#Eval("UserName")%>
                                            </div>
                                            <div class="replyed_info_pname">
                                                <%#Eval("ProName") %>
                                            </div>
                                            <div class="replyed_info_time">
                                                <%#Eval("AutoTimeStamp") %>
                                            </div>
                                            <div class="replyed_info_operation">
                                                <a href="javascript:;" onclick="$.dialog.open('EvaluationDetail.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&opid=<%#Eval("AutoID") %>',{title:'评价Chi tiết',width:680,height:430},false);">
                                                    Chi tiết</a>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CommandArgument='<%#Eval("AutoID")%>' runat="server"
                                                    OnClientClick="return confirm('Bạn có chắc chắn xóa?')" OnClick="lnk_Delete_Click" />
                                            </div>
                                        </div>
                                        <p class="comment_info_text">
                                            <font>Nội dung đánh giá：</font>
                                            <%#WriteStart(SinGooCMS.Utility.WebUtils.GetInt(Eval("Start"))) %> <%#Eval("Content") %>
                                        </p>
                                        <%if (status == 1)
                                          { %>
                                        <p class="replyed_info_text">
                                            <font>Nội dung trả lời：</font> <span class="line">
                                                <%#Eval("ReplyContent") %></span> <em class="colorD">
                                                    <%#Eval("ReplyTime") %></em>
                                        </p>
                                        <%} %>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <%=Repeater1.Items.Count == 0 ? "<div style='line-height:30px;text-align:center;'>Chúng tôi không tìm thấy bất kỳ dữ liệu</div>" : ""%>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <!--表格Nội dung end-->
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
    </script>
</asp:Content>
