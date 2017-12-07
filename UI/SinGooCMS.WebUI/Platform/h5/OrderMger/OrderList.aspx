<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.OrderMger.OrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/platform/h5/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="/platform/h5/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="/platform/h5/js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
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
                <li <%=strStatus==""?"class='active'":"class='normal'" %>><a href="javascript:;"
                    onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Tất cả các đơn đặt hàng(<%=ztstat.AllCount%>)</a></li>
                <li <%=strStatus=="1"?"class='active'":"class='normal'" %>><a href="javascript:;"
                    onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&orderstatus=1'">
                    Đợi người mua thanh toán(<%=ztstat.WaitPayCount%>)</a></li>
                <li <%=strStatus=="10"?"class='active'":"class='normal'" %>><a href="javascript:;"
                    onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&orderstatus=10'">
                    Chờ vận chuyển(<%=ztstat.WaitSendGoodsCount%>)</a></li>
                <li <%=strStatus=="11"?"class='active'":"class='normal'" %>><a href="javascript:;"
                    onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&orderstatus=11'">
                    Chờ người mua ký nhận(<%=ztstat.WaitSignGoodsCount%>)</a></li>
                <li <%=strStatus=="99"?"class='active'":"class='normal'" %>><a href="javascript:;"
                    onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&orderstatus=99'">
                    Đã kết thúc(<%=ztstat.SuccessCount%>)</a></li>
                <li <%=strStatus=="101"?"class='active'":"class='normal'" %>><a href="javascript:;"
                    onclick="location='OrderList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View&orderstatus=101'">
                    Đã hủy(<%=ztstat.CancelCount%>)</a></li>
            </ul>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Tất cả các đơn đặt hàng" Value="-1" />
                                    <asp:ListItem Text="Đơn đặt hàng thông thường" Value="0" />
                                    <asp:ListItem Text="Đơn đặt hàng trả trước" Value="1" />
                                </asp:DropDownList>
                            </div>
                            <label for="firstname" class="sr-only">
                                Từ</label>
                            <div class="fl col-md-2">
                                <asp:TextBox ID="timestart" runat="server" CssClass="form-control" date-selector='true'
                                    data-date-format="yyyy-mm-dd" placeholder="Thời gian bắt đầu"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                Đến</label>
                            <div class="fl col-md-2">
                                <asp:TextBox ID="timeend" runat="server" CssClass="form-control" date-selector='true'
                                    data-date-format="yyyy-mm-dd" placeholder="Thời gian kết thúc"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Vui lòng nhập số thứ tự Tên thành viên"></asp:TextBox>
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
                                <asp:LinkButton ID="btn_DelBat" runat="server" Text="Xóa" class="btn btn-default ml20 fl"
                                    OnClientClick="return singoo.getCheckCount('rowItems')>0 && confirm('OK để xóa nó? \r\ sẽ xóa tất cả các mục đã chọn 无 thể được phục hồi, xin vui lòng thận trọng');" />
                            </div>
                            <div class="btn-group">
                                <asp:LinkButton ID="btn_Export" runat="server" OnClick="btn_Export_Click" Text="Export đơn hàng"
                                    class="btn btn-default ml20 fl" />
                            </div>
                            <%--<div class="btn-group">
                                <input type="button" value="批量发货" class="btn btn-default ml20 fl" onclick="if(singoo.getCheckCount('rowItems')>0 && confirm('OK để xóa nó? \r\ sẽ xóa tất cả các mục đã chọn 无 thể được phục hồi, xin vui lòng thận trọng')) { sendGoodsBat(); }" />
                            </div>--%>
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
                        <div class="order-list-box clear-fix">
                            <div class="order-list-hd clear-fix">
                                <span class="order-select"></span><span class="order">Đơn đặt hàng</span> <span class="order-pay">
                                    Thanh toán</span> <span class="order-money">Số tiền đơn hàng(VNĐ)</span> <span class="order-status">Trạng thái Đơn đặt hàng</span>
                                <span class="order-operating">Xử lý</span>
                            </div>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="order-list-bd clear-fix">
                                        <div class="order_hover">
                                            <div class="title" style="height:40px;">
                                                <span class="order-select">
                                                    <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                    <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                                    <asp:HiddenField ID="hdfStatus" runat="server" Value='<%#Eval("OrderStatus") %>' />
                                                </span>
                                                <span class="order">Serial đơn đặt hàng：<a href="OrderDetail.aspx?&CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>" data-toggle="tooltip" data-placement="top" title="点击XemChi tiết"><%#Eval("OrderNo") %></a></span>
                                                <span class="order-pay">Điện thoại：<%#Eval("Mobile")%></span> <span class="order-money"><%#Eval("Consignee")%>,<%#Eval("Province")%>,<%#Eval("City")%>,<%#Eval("County")%>,<%#Eval("Address")%></span>
                                                <span class="order-status">Thời gian đặt hàng：<%#Eval("OrderAddTime")%></span> 
                                                <span class="order-operating"></span>
                                            </div>
                                            <div class="order_info_text clear">
                                                <span class="order-select"></span>
                                                <span class="order"><%#Eval("UserName") %></span> 
                                                <span class="order-pay"><%#Eval("PayName").ToString() == "" ? "Không" : Eval("PayName")%></span> 
                                                <span class="order-money"><strong><%#SinGooCMS.Utility.WebUtils.GetDecimal(Eval("OrderTotalAmount")).ToString("c2")%></strong></span>
                                                <span class="order-status">
                                                    <%#SinGooCMS.Utility.EnumUtils.GetEnumDescription((SinGooCMS.OrderStatus)Convert.ToInt32(Eval("OrderStatus")))%>
                                                </span>
                                                <span class="order-operating">
                                                    <div class="btn-group dropup">
                                                        <button type="button" class="btn btn-primary btn-xs">
                                                            Xem thêm</button>
                                                        <button type="button" class="btn btn-primary btn-xs  dropdown-toggle" data-toggle="dropdown">
                                                            <span class="caret"></span><span class="sr-only">Xem thêm</span>
                                                        </button>
                                                        <ul class="dropdown-menu pull-right" role="menu">
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_Confirm" runat="server" Text="Review" CommandArgument='<%#Eval("AutoID") %>'></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_ChangeAmount" runat="server" Text="Thay đổi giá" CommandArgument='<%#Eval("AutoID") %>'></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_Pay" runat="server" Text="Thanh toán" CommandArgument='<%#Eval("AutoID") %>'></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_SendGoods" runat="server" Text="Giao hàng" CommandArgument='<%#Eval("AutoID") %>'></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_SignGoods" runat="server" Text="Nhận hàng" CommandArgument='<%#Eval("AutoID") %>'></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_Cancel" runat="server" Text="Close" CommandArgument='<%#Eval("AutoID") %>'></asp:LinkButton></li>
                                                            <li>
                                                                <asp:LinkButton Visible="false" ID="lnk_Del" runat="server" OnClick="lnk_Delete_Click"
                                                                    Text="Xóa" CommandArgument='<%#Eval("AutoID") %>' OnClientClick="return confirm('Bạn có chắc chắn xóa?')"></asp:LinkButton></li>
                                                            <li class="divider"></li>
                                                            <li><a href="OrderDetail.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&opid=<%#Eval("AutoID") %>">
                                                                Chi tiết đơn hàng</a></li>
                                                        </ul>
                                                    </div>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<div> Chúng tôi không tìm thấy bất kỳ dữ liệu</div>" : ""%>
                                </FooterTemplate>
                            </asp:Repeater>
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
        //DatetimeLựa chọn 
        $("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });

        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });        
    </script>
</asp:Content>
