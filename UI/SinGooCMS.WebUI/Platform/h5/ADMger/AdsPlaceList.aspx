<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="AdsPlaceList.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.AdsPlaceList" %>

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
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control" placeholder="Vui lòng nhập tên quảng cáo"></asp:TextBox>
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
                                <asp:LinkButton ID="btn_SaveSort" runat="server" OnClick="btn_SaveSort_Click" Text="Save thứ tự"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="$.dialog.open('ModifyAdPlace.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add',{title:'Thêm quảng cáo位',width:680,height:360},false);">
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
                    <!--筛选条件 end-->
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th  style="width: 60px;">
                                    </th>
                                    <th >
                                        Tên vị trí quảng cáo
                                    </th>
                                    <th >
                                        Kích thước
                                    </th>
                                    <th  style="width: 120px;">
                                        Thứ tự
                                    </th>
                                    <th  style="width: 100px;">
                                        Xử lý
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("PlaceName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Width")%>(px) x <%#Eval("Height")%>(px)
                                            </td>
                                            <td>
                                                <asp:TextBox style="width:80px;" runat="server" ID="txtsort" class="form-control text-center" Text='<%#Eval("Sort") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <div class="btn-group dropup">
                                                    <button type="button" class="btn btn-primary btn-xs">
                                                        Xem thêm</button>
                                                    <button type="button" class="btn btn-primary btn-xs  dropdown-toggle" data-toggle="dropdown">
                                                        <span class="caret"></span><span class="sr-only">Xem thêm</span>
                                                    </button>
                                                    <ul class="dropdown-menu pull-right" role="menu">
                                                        <li><a href="javascript:;" onclick="$.dialog.open('ModifyAdPlace.aspx?Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>',{title:'Sửa quảng cáo位',width:680,height:360},false);">
                                                            Sửa</a></li>
                                                        <li><a href="javascript:;" onclick="getadcode(<%#Eval("AutoID") %>)">Lấy mã quảng cáo</a></li>
                                                        <li><a href='AdsList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&PlaceID=<%#Eval("AutoID") %>&action=View'>
                                                            Chi tiết quảng cáo</a></li>
                                                        <li class="divider"></li>
                                                        <li>
                                                            <asp:LinkButton ID="lnk_Delete" Text="Xóa" CommandArgument='<%#Eval("AutoID")%>' runat="server"
                                                                OnClientClick="return confirm('Bạn có chắc chắn xóa?')" OnClick="lnk_Delete_Click" /></li>
                                                    </ul>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='5'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
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
        function getadcode(placeid) {
            $.dialog({ title: "Lấy mã quảng cáo", content: "&lt;script type='text/javascript' src='/include/generatead?placeid=" + placeid + "'&gt;&lt;/script&gt; <a href='javascript:;' onclick='copyadcode(" + placeid + ")'>复制</a> <p><br/><i class='iconfont mt-20'title=''>&#xe613;</i>如复制按钮无效，请手动复制！</p>" });
        }
        function copyadcode(placeid) {
            copyToClipboard("<script" + " type='text/javascript' src='/include/generatead?placeid=" + placeid + "'></" + "script>");
        } 
    </script>
</asp:Content>
