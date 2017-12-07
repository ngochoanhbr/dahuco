<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Management Platform</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-index fix">
            <div class="col-md-4 col-m row">
                <div class="col-md-12">
                    <div class="col-l text-center ion-stats-bars">
                        <i class="iconfont" style="font-size: 50px;">&#xe67d;</i>
                    </div>
                    <div class="col-r">
                        <h4 class="panel-title">
                            Today's order amount</h4>
                        <h3>
                            ￥<%=SinGooCMS.DeskSource.dayOrderAmount.ToString("f2")%></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-m row">
                <div class="col-md-12">
                    <div class="col-l text-center ion-eye">
                        <i class="iconfont" style="font-size: 50px;">&#xe67f;</i>
                    </div>
                    <div class="col-r">
                        <h4 class="panel-title">
                            Today's order number</h4>
                        <h3>
                            <%=SinGooCMS.DeskSource.dayOrderNum%></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-m row">
                <div class="col-md-12">
                    <div class="col-l text-center ion-clock">
                        <i class="iconfont" style="font-size: 50px;">&#xe67e;</i>
                    </div>
                    <div class="col-r">
                        <h4 class="panel-title">
                            Add a member today</h4>
                        <h3>
                            <%=SinGooCMS.DeskSource.dayUserNum%></h3>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-index-1 fix" style="width: 66%; margin-right: 20px;">
            <h3>
                Đơn hàng đang chờ</h3>
            <ul>
                <li>
                    <div>
                        Đơn hàng chờ<b><a href="OrderMger/OrderList.aspx?CatalogID=2&Module=3B7A1D6D0D798260&action=View&orderstatus=10"><%=SinGooCMS.DeskSource.waitSendGoodsNum %>cái</a></b></div>
                </li>
                <li>
                    <div>
                        Các thư chưa đọc<b><a href="Tools/MyMessageBox.aspx"><%=SinGooCMS.DeskSource.noReadMsgNum %>cái</a></b></div>
                </li>
                <li>
                    <div>
                        Hàng tham khảo<b><a href="GoodsMger/GoodsQA.aspx?CatalogID=1&Module=3DB75D2B46FC4473&action=view"><%=SinGooCMS.DeskSource.proQANum %>cái</a></b></div>
                </li>
                <li>
                    <div>
                        Cảnh báo hết hàng<b><a href="GoodsMger/Stockout.aspx?CatalogID=1&Module=209C1555C83AB9BF&type=0&action=view"><%=SinGooCMS.DeskSource.stockAlertNum %>cái</a></b></div>
                </li>
                <li>
                    <div>
                        Hết hàng<b><a href="GoodsMger/Stockout.aspx?CatalogID=1&Module=209C1555C83AB9BF&type=1&action=view"><%=SinGooCMS.DeskSource.stockOutNum %>cái</a></b></div>
                </li>
            </ul>
        </div>
        <div class="col-index-1" style="width: 32%">
            <h3>
                Thông tin thống kê bán hàng</h3>
            <ol>
                <li>
                    <div>
                        <p>
                            <span><%=SinGooCMS.DeskSource.sellTotal.ToString("f2") %>VNĐ</span>
                        </p>
                        <span>Tổng doanh thu</span>
                    </div>
                </li>
                <li>
                    <div>
                        <p>
                            <span>
                                <%=SinGooCMS.DeskSource.orderTotal %></span>
                        </p>
                        <span>Tổng số lượng đơn đặt hàng</span>
                    </div>
                </li>
                <li>
                    <div style="border: 0">
                        <p>
                            <span>
                                <%=SinGooCMS.DeskSource.goodsTotal %></span>
                        </p>
                        <span>Tổng số hàng hóa</span>
                    </div>
                </li>
                <li>
                    <div style="border: 0">
                        <p>
                            <span>
                                <%=SinGooCMS.DeskSource.userTotal %></span>
                        </p>
                        <span>Tổng số thành viên</span>
                    </div>
                </li>
            </ol>
        </div>
    </div>
    <div class="dialog fix container">
        <div class="col-md-4  col-m row" style="margin-right: 20px;">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Đơn đặt hàng sẽ được giao
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="inbox-widget mx-box bar">
                        <asp:Repeater ID="rpt_NewOrder" runat="server">
                            <ItemTemplate>
                                <a href="OrderMger/OrderDetail.aspx?CatalogID=2&Module=3B7A1D6D0D798260&action=Modify&opid=<%#Eval("AutoID") %>">
                                    <div class="inbox-item">
                                        <div class="inbox-item-img">
                                            <img src='<%#string.IsNullOrEmpty(Eval("HeaderPhoto").ToString()) ? "/include/images/userheader.png" : Eval("HeaderPhoto").ToString()%>'
                                                class="img-circle" alt="">
                                        </div>
                                        <p class="inbox-item-author">
                                            Số đơn:
                                            <%#Eval("OrderNo") %></p>
                                        <p class="inbox-item-text">
                                            Người nhận:<%#Eval("Consignee")%>
                                            <%#Eval("Country")%>,<%#Eval("Province")%>,<%#Eval("City")%></p>
                                        <p class="inbox-item-phone">
                                            Điện thoại:<%#Eval("Mobile") %></p>
                                        <p class="inbox-item-date">
                                            <%#Eval("daystr")%></p>
                                    </div>
                                </a>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%=rpt_NewOrder.Items.Count == 0 ? "<div style='width: 323px;height: 180px;line-height: 180px;text-align: center;'><i class='iconfont font-100'>&#xe68f;</i></div>" : ""%>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4  col-m row" style="margin-right: 20px;">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Thành viên mới
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="inbox-widget mx-box bar">
                        <asp:Repeater ID="rpt_NewUser" runat="server">
                            <ItemTemplate>
                                <a href="UserMger/ModifyUser.aspx?CatalogID=3&Module=11D1F62751553289&action=Modify&GroupID=<%#Eval("GroupID")%>&opid=<%#Eval("AutoID") %>">
                                    <div class="inbox-item">
                                        <div class="inbox-item-img">
                                            <img src='<%#string.IsNullOrEmpty(Eval("HeaderPhoto").ToString()) ? "/include/images/userheader.png" : Eval("HeaderPhoto").ToString()%>'
                                                class="img-circle" alt="">
                                        </div>
                                        <p class="inbox-item-author">
                                            Tên thành viên:<%#Eval("UserName") %></p>
                                        <p class="inbox-item-text">
                                            Email:<%#Eval("Email")%></p>
                                        <p class="inbox-item-phone">
                                            Điện thoại:<%#Eval("Mobile") %></p>
                                        <p class="inbox-item-date">
                                            <%#Eval("daystr")%></p>
                                    </div>
                                </a>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%=rpt_NewUser.Items.Count == 0 ? "<div style='width: 323px;line-height: 180px;text-align: center;'><i class='iconfont font-100'>&#xe690;</i></div>" : ""%>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4  col-m row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Thông báo hoạt động
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="arcit_list bar">                       
                        <ul>
                            <asp:Repeater ID="rpt_UeNews" runat="server">
                                <ItemTemplate>
                                    <li class="no1"><em>
                                        <%#Eval("PublishDate")%></em> <a <%#Eval("Url").ToString()==""?"javascript:;":"href='"+Eval("Url").ToString()+"' target='_blank'" %>>·
                                            <%#Eval("Title")%></a> </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <%=rpt_UeNews.Items.Count == 0 ? "<div style='width: 323px;height: 180px;line-height: 180px;text-align: center;'><i class='iconfont font-100'>&#xe68c;</i></div>" : ""%>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel-quick-page fix container">
        <div class="col-xs-4 col-sm-5 col-md-4 page-user">
            <div class="panel">
                <a href="GoodsMger/Products.aspx?CatalogID=1&Module=88DF4B2F399A2FE1&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Danh sách sản phẩm</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe60d;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-4 col-md-4 page-products">
            <div class="panel">
                <a href="GoodsMger/Category.aspx?CatalogID=1&Module=36DA9FA28729BE82&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Thể loại</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe604;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-3 col-md-2 page-events">
            <div class="panel">
                <a href="UserMger/UserList.aspx?CatalogID=3&Module=11D1F62751553289&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Danh sách thành viên</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe650;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-3 col-md-2 page-messages">
            <div class="panel">
                <a href="NodeMger/Index.aspx?CatalogID=4&Module=108D1E0DE29E7EA0&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Menu</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe657;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-5 col-md-2 page-reports" disabled>
            <div class="panel">
                <a href="ContMger/Index.aspx?CatalogID=4&Module=93BEC1E8CA57D67E&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Danh sách bài viết</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe655;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-5 col-md-2 page-statistics">
            <div class="panel">
                <a href="ADMger/AdsPlaceList.aspx?CatalogID=8&Module=0B1130E457EC0607&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Quản lý quảng cáo</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe66a;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-4 col-md-4 page-support">
            <div class="panel">
                <a href="ConfMger/BaseConfig.aspx?CatalogID=10&Module=2ADFDA42F3BB20C9&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Cấu hình cơ bản</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe67a;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-4 col-md-2 page-privacy">
            <div class="panel">
                <a href="GoodsMger/PaymentSet.aspx?CatalogID=1&Module=C54EF4A9D068BDA6&action=view">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Cấu hình thanh toán</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe663;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-xs-4 col-sm-4 col-md-2 page-settings">
            <div class="panel">
                <a href="GoodsMger/GoodsQA.aspx?CatalogID=1&Module=3DB75D2B46FC4473&action=view"
                    class="disableCss">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            Hàng tham khảo</h4>
                    </div>
                    <div class="panel-body">
                        <div class="page-icon">
                            <i class="iconfont">&#xe60a;</i>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
    <footer class="footer">
        <div class="container text-center">
            <font>©SinGooCMS 2014-2016 <a href="http://www.ue.net.cn" target="_blank">深圳联华网络</a>提供技术支持 </font>
        </div>
    </footer>
    <!--侧栏Chỉnh sửa 滚动条样式-->
    <script src="js/jquery.nicescroll.js"></script>
    <script type="text/javascript">
        $(".bar").niceScroll({
            cursorcolor: "#9D9EA5",
            cursoropacitymax: 2,
            touchbehavior: false,
            cursorwidth: "2px",
            cursorborder: "0",
            cursorborderradius: "5px"
        }); 
    </script>
    <% System.Collections.Generic.IList<SinGooCMS.Entity.MessageInfo> lstMsg = GetMessages();
       if (lstMsg != null && lstMsg.Count > 0)
       {
    %>
    <!--最新消息Chủ đềstart-->
    <div style="margin: 5px 0; width: 290px;display:none;" id="top5newmsg">
        <%
            int i = 0;
            foreach (SinGooCMS.Entity.MessageInfo item in lstMsg)
            {
                if (i == 10) break;
                Response.Write("<p style='height:22px;line-height:22px;'><a href='javascript:void(0)' onclick=\"$.dialog.open('Tools/MyMsgBoxDetail.aspx?Module=17592077293B3D9F&action=Modify&opid=" + item.AutoID + "',{title:'Xem thông báo',width:580,height:350},false);\">" + item.MsgTitle + " " + item.SendTime.ToShortDateString() + "</a></p>");
                i++;
            }
        %>
    </div>
    <script type="text/javascript">
        artDialog.notice = function (options) {
            var opt = options || {},
        api, aConfig, hide, wrap, top,
        duration = 800;

            var config = {
                id: 'Notice',
                left: '100%',
                top: '100%',
                fixed: true,
                drag: false,
                resize: false,
                follow: null,
                lock: false,
                init: function (here) {
                    api = this;
                    aConfig = api.config;
                    wrap = api.DOM.wrap;
                    top = parseInt(wrap[0].style.top);
                    hide = top + wrap[0].offsetHeight;

                    wrap.css('top', hide + 'px')
                    .animate({ top: top + 'px' }, duration, function () {
                        opt.init && opt.init.call(api, here);
                    });
                },
                close: function (here) {
                    wrap.animate({ top: hide + 'px' }, duration, function () {
                        opt.close && opt.close.call(this, here);
                        aConfig.close = $.noop;
                        api.close();
                    });

                    return false;
                }
            };

            for (var i in opt) {
                if (config[i] === undefined) config[i] = opt[i];
            };

            return artDialog(config);
        };
        art.dialog.notice({
            title: '最新消息',
            width: 280,
            content: document.getElementById("top5newmsg"),
            time: 10,
            padding: 0,
            margin: 0
        });
    </script>
    <!--最新消息Chủ đềend-->
    <% }%>
</asp:Content>
