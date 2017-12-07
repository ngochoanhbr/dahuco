<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyProduct.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #dropmenu
        {
            position: absolute;
            top: 36px;
            left: 0px;
            z-index: 999;
            height: 30px;
            background: white;
            border: 1px solid #ccc;
            padding: 2px 5px;
            line-height: 20px;
        }
        #dropmenu table
        {
            padding: 2px;
            margin: 2px;
        }
        #dropmenu table td
        {
            padding: 2px 8px;
            margin: 2px;
        }
    </style>
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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='Products.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">Các mặt hàng列表</a></li>
                <li class="active"><a href="#Order_1" data-toggle="tab">Thông tin cơ bản</a></li>
                <li><a href="#Order_2" data-toggle="tab">Giá và hàng tồn kho</a></li>
                <li><a href="#Order_3" data-toggle="tab">Chi tiết Nội dung</a></li>
                <li><a href="#Order_4" data-toggle="tab">Thư viện ảnh</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade in active" id="Order_1">
                    <div class="profile-body mb-20">
                        <div class="datafrom">
                            <%if (base.IsEdit && proInit != null)
                              { %>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Các mặt hàngID:</label>
                                <div class="col-md-8">
                                    #<%=proInit.AutoID%> <a href="/shop/goods/<%=proInit.AutoID%>" target="_blank">Xem Các mặt hàng >></a>
                                </div>
                            </div>
                            <%} %>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Thể loại:</label>
                                <div class="col-md-8">
                                    <asp:Literal ID="catepath" runat="server"></asp:Literal>
                                    <a href="GoodsPulish.aspx?CatalogID=1&Module=570A68562C422060&opid=<%=proInit==null?0:proInit.AutoID %>&action=<%=Action %>">
                                        Sửa</a>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Tên hàng hóa:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Nhập Tên hàng hóa" required="required"
                                        CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Tên hàng hóa là cần thiết ">&#xe613;</i></div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Thương hiệu:</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Code:</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TextBox4" MaxLength="30" runat="server" CssClass="form-control"
                                        required="required"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Nó chỉ là duy nhất, không được trùng nhau">&#xe613;</i></div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Ảnh hàng hóa:</label>
                                <div class="col-md-4">
                                    <div class="images_model">
                                        <div class="box-hd">
                                            <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox5.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
                                                &#xe682;</i>
                                        </div>
                                        <div class="box-bd">
                                            <div class="figure-img">
                                                <jweb:FullImage ID="Image1" runat="server" />
                                            </div>
                                            <span class="hidden">
                                                <asp:TextBox ID="TextBox5" runat="server" class="form-control"></asp:TextBox></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix hidden">
                                <label for="firstname" class="col-md-3 control-label">
                                    Bán hàng:</label>
                                <div class="col-md-4 line-36">
                                    <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="Sản phẩm duy nhất" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Ghép nhiều sản phẩm" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <div id="suitpros" class="hide" runat="server">
                                        <div style="text-align: right">
                                            <a href="javascript:void(0)" class="suitjia">Thêm dòng[+]</a></div>
                                        <div id="suitproarea">
                                            <asp:Repeater ID="rpt_suitprolist" runat="server">
                                                <ItemTemplate>
                                                    <div class="suititem" id='suititem<%#Container.ItemIndex %>'>
                                                        Lựa chọn Các mặt hàng：<input type="text" name="suit_pro" class="form-control" value='<%#Eval("ProductName") %>|<%#Eval("ProductID")%>'
                                                            id='suit-selpro<%#Container.ItemIndex %>' />
                                                        <input class="btn-normal" type="button" value="Lựa chọn Các mặt hàng" onclick="$.dialog.open('../Selector/ProductForSelect.aspx?Module=1FA35D0106A6D027&type=mutil&style=2&elementid=suit-selpro<%#Container.ItemIndex %>', { title: 'Lựa chọn Các mặt hàng', width: 680, height: 430 }, false);" />
                                                        Số lượng：<input type="text" name="suit_pronum" class="form-control" value='<%#Eval("Quantity") %>'
                                                            style="width: 80px" />
                                                        <a href="javascript:void(0)" onclick="removenewtr('suititem<%#Container.ItemIndex %>')"
                                                            class="jian" title="Di chuyển">[-]</a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Meta từ khóa:</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="metaKey" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                                        CssClass="form-control" lenlimit="255"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Nhiều từ khóa (,) ngăn cách bởi dấu phẩy ">
                                        &#xe613;</i></div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Meta mô tả:</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="metaDescription" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                                        CssClass="form-control" lenlimit="255"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Nhiều từ khóa (,) ngăn cách bởi dấu phẩy ">
                                        &#xe613;</i></div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Hiển thị bắt mắt:</label>
                                <div class="col-md-4 line-36">
                                    <label class="margin-right-10">
                                        <asp:CheckBox ID="chkHot" runat="server" class="checkbox_radio" />Hàng HOT&nbsp;</label>
                                    <label class="margin-right-10">
                                        <asp:CheckBox ID="chkRecommand" runat="server" class="checkbox_radio" />Hàng đề nghị&nbsp;</label>
                                    <label class="margin-right-10">
                                        <asp:CheckBox ID="chkNew" runat="server" class="checkbox_radio" />Hàng mới&nbsp;</label>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Hàng hóa ảo:</label>
                                <div class="col-md-1">
                                    <asp:CheckBox ID="isvirtual" runat="server" class="checkbox_radio" />
                                </div>
                                <div class="col-md-2">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Hàng hóa ảo không có chi phí vận chuyển">&#xe613;</i>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Đặt trước:</label>
                                <div class="col-md-1">
                                    <asp:CheckBox ID="isbooking" runat="server" class="checkbox_radio" />
                                </div>
                                <div class="col-md-2">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Hàng không thể đặt trước trực tuyến">
                                        &#xe613;</i>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                     bản mẫu tỉnh thành:</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlAreaModel" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Bản mẫu miễn phí:</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlPostageModel" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Trạng thái:</label>
                                <div class="col-md-4 line-36">
                                    <input type="checkbox" id="auditstatus" runat="server" class="ios-switch" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="Order_2">
                    <div class="profile-body mb-20">
                        <div class="datafrom fix">
                            <h2 class="title">
                                Tổng hợp và hàng tồn kho</h2>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    đơn vị:</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TextBox12" Text="cái" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Giá thị trường:</label>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <span class="input-group-addon"> đ </span>
                                        <jweb:H5TextBox Mode="Number" ID="TextBox10" MaxLength="10" runat="server" CssClass="form-control"
                                            Text="0.00" step="0.0001"></jweb:H5TextBox>
                                        <span class="input-group-addon">VNĐ</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Giá website:</label>
                                <div class="col-md-3">
                                    <div class="input-group">
                                        <span class="input-group-addon"> đ </span>
                                        <jweb:H5TextBox Mode="Number" ID="TextBox11" MaxLength="10" runat="server" CssClass="form-control"
                                            Text="0.00" step="0.0001"></jweb:H5TextBox>
                                        <span class="input-group-addon">VNĐ</span>
                                    </div>
                                    <input type="hidden" name="hdf_memberprice" id="hdf_memberprice" />
                                </div>
                                <div class="col-md-2" id="openmemprice">
                                    <a href="javascript:;" onclick="$.dialog.open('MemberPrice.aspx?Module=<%=base.CurrentModuleCode %>&type=goods&opid=<%=proInit==null?-1:proInit.AutoID%>&price='+$('#<%=TextBox11.ClientID %>').val()+'&retid=hdf_memberprice&action=Modify',{title:'Chỉnh sửa giá thành viên',width:580,height:430},false);">
                                        Chỉnh sửa giá thành viên</a>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Danh mục hàng hóa:</label>
                                <div class="col-md-3">
                                    <asp:Literal ID="splm" runat="server"></asp:Literal>
                                    <a href="javascript:;" onclick="location='SelectClass.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&CateID=<%=currCate.AutoID %>&opid=<%=proInit==null?0:proInit.AutoID %>&action=<%=Action %>&Status=1'">
                                        Chọn loại </a>
                                    <asp:Panel ID="panelswitch" runat="server" Visible="false">
                                        <input type="button" class="btn btn-default" value="Open" id="btn_open" onclick="openGuiGe();" /><br />
                                    </asp:Panel>
                                </div>
                                <div class="col-md-3">
                                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Sau khi thông số kỹ thuật mở, mua hàng cần lựa chọn thông số kỹ thuật">
                                        &#xe613;</i>
                                </div>
                            </div>
                            <asp:Panel ID="panelgg" runat="server" Visible="false" CssClass="form-group mt-20 fix">
                                <table class="table">
                                    <tr id="ggtr">
                                        <td>
                                            <input type="button" class="btn btn-default" value="Thêm chi tiết kỹ thuật" onclick="createRow();" />
                                            <input type="button" class="btn btn-default" value="Tất cả chi tiết kỹ thuật" onclick="createAll();" />
                                            <input type="button" class="btn btn-default" value="Close" id="btn_close" onclick="closeGuiGe();" />
                                            <input type="button" class="btn btn-default" value="Thông số kỹ thuật Hình ảnh" id="btn_addpic"/>
                                            <div style="min-height: 260px; background: white; margin-top: 3px;">
                                                <table class="table" id="ggtab">
                                                    <thead id="ggthead">
                                                    </thead>
                                                    <tbody id="ggtbody">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <input type="hidden" id="hdf_backguigepic" name="hdf_backguigepic" />
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Điểm bắt buộc:</label>
                                <div class="col-md-2">
                                    <jweb:H5TextBox Mode="Number" ID="TextBox13" MaxLength="10" runat="server" CssClass="form-control"
                                        Text="0"></jweb:H5TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="margin-right-10">
                                        <asp:CheckBox ID="chkExchange" runat="server" class="checkbox_radio" />Trao đổi mặt hàng</label>
                                </div>
                                <i class="iconfont fl" data-toggle="tooltip" data-placement="right" title="Kiểm tra trao đổi hàng，Điều này dẫn đến sự cần thiết phải đổi điểm；Nếu không mua hàng, nó cần thiết cho việc nhập kho">
                                    &#xe613;</i>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Điểm thưởng:</label>
                                <div class="col-md-2">
                                    <jweb:H5TextBox Mode="Number" ID="TextBox14" MaxLength="10" runat="server" CssClass="form-control"
                                        Text="0"></jweb:H5TextBox>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Hàng hóa trong kho:</label>
                                <div class="col-md-2">
                                    <jweb:H5TextBox Mode="Number" ID="TextBox15" MaxLength="10" runat="server" CssClass="form-control"
                                        Text="0"></jweb:H5TextBox>
                                </div>
                                <i class="iconfont fl" data-toggle="tooltip" data-placement="right" title="Nếu hàng hóa không có trong tồn kho, sẽ không đặt hàng trên website được">
                                    &#xe613;</i>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Giới hạn mua:</label>
                                <div class="col-md-2">
                                    <jweb:H5TextBox Mode="Number" ID="TextBox16" MaxLength="10" runat="server" CssClass="form-control"
                                        Text="0"></jweb:H5TextBox>
                                </div>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Số lượng cảnh báo:</label>
                                <div class="col-md-2">
                                    <jweb:H5TextBox Mode="Number" ID="alartnum" MaxLength="10" runat="server" CssClass="form-control"
                                        Text="0"></jweb:H5TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="Order_3">
                    <div class="profile-body mb-20">
                        <div class="datafrom fix">
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Tag:</label>
                                <div class="col-md-9">
                                    <asp:Repeater ID="rpt_Tags" runat="server">
                                        <ItemTemplate>
                                            <input type="checkbox" name="chk_tag" class="checkbox_radio" value='<%#Eval("AutoID") %>' <%#(IsEdit && proInit!=null && !string.IsNullOrEmpty(proInit.Tags) && proInit.Tags.Contains(Eval("AutoID").ToString()))?"checked='checked'":"" %> /> <%#Eval("TagName") %>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <asp:Repeater ID="rptDetail" runat="server" OnItemDataBound="rptDetail_ItemDataBound">
                                <ItemTemplate>
                                    <div class="form-group mt-20 fix">
                                        <label for="firstname" class="col-md-3 control-label">
                                            <%# Eval("Alias")%>:</label>
                                        <div class="col-md-9">
                                            <jweb:FieldControl ID="field" runat="server">
                                            </jweb:FieldControl>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="Order_4">
                    <div class="profile-body mb-20">
                        <div class="batchHandleArea fix">
                            <a href="javascript:;" class="btn btn-warning ml20 fl" onclick="h5.openUploadTool('mutil','','');">
                                <i class="iconfont">&#xe62b;</i> Thêm hình ảnh</a>
                        </div>
                    </div>
                    <div class="profile-body mb-20 filemanager fix" id="imgarea">
                        <asp:Repeater ID="rpt_img" runat="server">
                            <ItemTemplate>
                                <div class="col-md-3">
                                    <div class="thumbnail">
                                        <div style="height: 200px; text-align: center;">
                                            <img viewer="true" style="max-height: 200px;" src='<%#Eval("ImgThumbSrc") %>' alt=""
                                                data-original='<%#Eval("ImgSrc") %>' />
                                        </div>
                                        <div class="caption">
                                            <h3>
                                                <input type="text" name="imgdesc" value='<%#Eval("Remark") %>' class="input-txt"
                                                    style="width: 160px;" placeholder="Ảnh hàng hóa文字Giải thích" />
                                                <input type="hidden" id="img<%#Eval("AutoID") %>" name="proimg" class="input-mid"
                                                    value='<%#Eval("ImgSrc") %>' />
                                                <a href="javascript:void(0)" onclick="$(this).parent().parent().parent().parent().remove();"
                                                    class="jian" title="Remove">[-]</a>
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <asp:Button ID="btnok_andcontinue" Text="Xác nhận và tiếp tục thêm" runat="server" OnClick="btnok_andcontinue_Click"
                        CssClass="btn btn-danger" Visible="false" />
                    <input id="btncancel" onclick="location='Products.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var ggs = '<%=goodsClass==null?"":goodsClass.GuiGeCollection %>'
        var classID =parseInt("<%=goodsClass==null?-1:goodsClass.AutoID %>");
        var arrHeader = new Array();
        var arrFirstLevelValue = new Array(); //第一阶级属性
        var arrGuiGeValue = new Array(); //所有的规格属性
        var json = new Object();
        var showPicGuiGeIndex = -1; //可上传图片的规格索引
        var showPicGuiGeName = ""; //可上传图片的规格名称
        var arrShowPicGuiGeItemNames = new Array(); //可上传图片的规格单项名称
        if (ggs != "") {
            json = eval('(' + ggs + ')');
            $.each(json, function (i, item) {
                arrHeader[i] = item.GuiGeName;
                arrFirstLevelValue[i] = item.GuiGeValue;
                if (item.IsImageShow != undefined && item.IsImageShow) {
                    showPicGuiGeIndex = i; //该列的规格允许添加图片
                    showPicGuiGeName = item.GuiGeName;
                }
            });
        }        
        //生成所有的键值对
        switch (arrFirstLevelValue.length) {
            case 1:
                arrGuiGeValue = arrFirstLevelValue[0].split(',');
                break;
            case 2:
                {
                    var a = arrFirstLevelValue[0].split(',');
                    var b = arrFirstLevelValue[1].split(',');
                    for (var i = 0; i < a.length; i++) {
                        for (var j = 0; j < b.length; j++) {
                            arrGuiGeValue.push(a[i] + "," + b[j]);
                        }
                    }
                }
                break;
            case 3:
                {
                    var a = arrFirstLevelValue[0].split(',');
                    var b = arrFirstLevelValue[1].split(',');
                    var c = arrFirstLevelValue[2].split(',');
                    for (var i = 0; i < a.length; i++) {
                        for (var j = 0; j < b.length; j++) {
                            for (var k = 0; k < c.length; k++) {
                                arrGuiGeValue.push(a[i] + "," + b[j] + "," + c[k]);
                            }
                        }
                    }
                }
                break;
            case 4:
                {
                    var a = arrFirstLevelValue[0].split(',');
                    var b = arrFirstLevelValue[1].split(',');
                    var c = arrFirstLevelValue[2].split(',');
                    var d = arrFirstLevelValue[3].split(',');
                    for (var i = 0; i < a.length; i++) {
                        for (var j = 0; j < b.length; j++) {
                            for (var k = 0; k < c.length; k++) {
                                for (var m = 0; m < d.length; m++) {
                                    arrGuiGeValue.push(a[i] + "," + b[j] + "," + c[k] + "," + d[m]);
                                }
                            }
                        }
                    }
                }
                break;
            case 5:
                {
                    var a = arrFirstLevelValue[0].split(',');
                    var b = arrFirstLevelValue[1].split(',');
                    var c = arrFirstLevelValue[2].split(',');
                    var d = arrFirstLevelValue[3].split(',');
                    var e = arrFirstLevelValue[4].split(',');
                    for (var i = 0; i < a.length; i++) {
                        for (var j = 0; j < b.length; j++) {
                            for (var k = 0; k < c.length; k++) {
                                for (var m = 0; m < d.length; m++) {
                                    for (var n = 0; n < e.length; n++) {
                                        arrGuiGeValue.push(a[i] + "," + b[j] + "," + c[k] + "," + d[m] + "," + e[n]);
                                    }
                                }
                            }
                        }
                    }
                }
                break;
            case 6:
                {
                    var a = arrFirstLevelValue[0].split(',');
                    var b = arrFirstLevelValue[1].split(',');
                    var c = arrFirstLevelValue[2].split(',');
                    var d = arrFirstLevelValue[3].split(',');
                    var e = arrFirstLevelValue[4].split(',');
                    var f = arrFirstLevelValue[5].split(',');
                    for (var i = 0; i < a.length; i++) {
                        for (var j = 0; j < b.length; j++) {
                            for (var k = 0; k < c.length; k++) {
                                for (var m = 0; m < d.length; m++) {
                                    for (var n = 0; n < e.length; n++) {
                                        for (var p = 0; p < f.length; p++) {
                                            arrGuiGeValue.push(a[i] + "," + b[j] + "," + c[k] + "," + d[m] + "," + e[n] + "," + f[p]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                break;
        }

        $(function () {
            $("#ggtr").hide(); //默认Close的
            initHeader();
            initEditGuiGe();
        });
        function initHeader() { //生成规格的头部
            var str = "<tr>";
            for (var i = 0; i < arrHeader.length; i++) {
                str += "<th style=\"width:60px;\">" + arrHeader[i] + "</th>";
            }
            str += "<th>Hạng mục</th><th style='width: 160px;'>giá bán ra</th><th style='width: 100px;'>Kho </th><th style='width: 100px;'>Số lượng cảnh báo</th><th style='width: 50px;'>Xử lý</th>";
            str += "</tr>";
            $("#ggthead").html(str);
        }
        function initEditGuiGe() { //初始化Nội dung đặc điểm kỹ thuật
            var cont = "<%=JsonForGuiGeInitValue %>";
            if (cont != "") {
                $("#ggtbody").html(cont);
                $("#ggtr").show();
            }
        }
        function openGuiGe() { //Open规格
            $("#ggtr").show();
            $("#ggtbody").empty();
            $("#btn_open").hide();
            $("#openmemprice").hide();
        }
        function closeGuiGe() { //Close规格
            $("#ggtr").hide();
            $("#ggtbody").empty();
            $("#btn_open").show();
            $("#openmemprice").show();
        }
        function createAll() { //创建所有规格
            var isCreate = true;
            if (arrGuiGeValue.length > 30 && !confirm("Sự kết hợp dẫn đến hơn 30 tiêu chuẩn. OK？"))
                isCreate = false;
            if (isCreate) {
                $("#ggtbody").empty();
                for (var i = 0; i < arrGuiGeValue.length; i++) {
                    createRow(arrGuiGeValue[i]);
                }
            }
        }
        function createRow() { //创建规格的行 selectEnabled()
            var arrdata = new Array();
            if (arguments.length > 0) {
                arrdata = arguments[0].split(',');
            } else {
                var arrEnabledData = selectEnabled();
                if (arrEnabledData.length > 0)
                    arrdata = arrEnabledData[0].split(','); //默认的数据
            }

            var proSN = $("#<%=TextBox4.ClientID %>").val() + "-" + (parseInt($("#ggtbody").find("tr").index()) + 2);   //货号
            var sellPrice = parseFloat($("#<%=TextBox11.ClientID %>").val()); //giá bán ra
            sellPrice = isNaN(sellPrice) ? "0.00" : sellPrice.toFixed(2);

            var mid = singoo.getRnd();
            var str = "<tr>";
            $.each(json, function (i, item) {
                str += "<td class='ggname' style='background:#ffa;position:relative;' onclick=\"createMenu(this,'" + item.GuiGeValue + "')\"><span>" + (arrdata.length > 0 ? arrdata[i] : "") + "</span><input type='hidden' name='gg_name' value='" + (arrdata.length > 0 ? arrdata[i] : "") + "' /></td>";
            });
            str += "<td><input type='text' value='" + proSN + "' name='gg_hh' class='input-txt' style='width:120px;' /></td>" + "<td><input type='text' value='" + sellPrice + "' name='gg_xsj' class='input-txt' style='width:60px;' /><input type='hidden' name='gg_memberprice' id='" + mid + "'/> <a href=\"javascript:;\" onclick=\"$.dialog.open('MemberPrice.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&type=guige&opid=-1&price='+$('#<%=TextBox11.ClientID %>').val()+'&retid=" + mid + "&action=Modify',{title:'Chỉnh sửa giá thành viên',width:580,height:430},false);\" >giá thành viên</a></td>" + "<td><input type='text' value='0' name='gg_kc' class='input-txt' style='width:60px;' /></td>" + "<td><input type='text' value='0' name='gg_alarmnum' class='input-txt' style='width:60px;' /></td>" + "<td><a href='javascript:;' onclick='$(this).parent().parent().remove();'>移除</a></td>";
            str += "</tr>";
            $("#ggtbody").append(str);
        }
        function createMenu(element, vals) { //创建菜单
            var arrFiltered = dataFilter(element);
            delDropMenu(); //Xóa之前的下拉菜单，下拉菜单只能有一个
            var str = "<div id='dropmenu'><table><tr>";
            for (var i = 0; i < vals.split(',').length; i++) {
                str += "<td><input style='cursor:pointer' " + ($.inArray(vals.split(',')[i], arrFiltered) == -1 ? "disabled='disabled'" : "") + " type='button' onclick=\"attrClick(this,'" + vals.split(',')[i] + "',event)\" value='" + vals.split(',')[i] + "' /></td>";
            }
            str += "</tr></table></div>";
            $(element).append(str)
        }
        function dataFilter(element) { //筛选
            var arrEnabled = selectEnabled(element); //所有可选的组合
            //Hiện tại行的属性集合
            var arrCurrRowVal = new Array();
            var columnIndex = $(element).index(); //在行里的列下标
            $.each($(element).parent().find("input[type='hidden']"), function (i, item) {
                if (columnIndex == $(item).parent().index())
                    arrCurrRowVal.push("");
                else
                    arrCurrRowVal.push($(item).val());
            });
            var arrFiltered = new Array();
            for (var i = 0; i < arrEnabled.length; i++) {
                var arrEnabledItem = arrEnabled[i].split(',');
                if (arrCurrRowVal.toString().replaceAll(",", "").length == 0) {
                    if ($.inArray(arrEnabledItem[columnIndex], arrFiltered) == -1) arrFiltered.push(arrEnabledItem[columnIndex]);
                } else {
                    var shouldAdd = true;
                    for (var j = 0; j < arrEnabledItem.length; j++) {
                        if (arrCurrRowVal[j] != "" && arrEnabledItem[j] != arrCurrRowVal[j])
                            shouldAdd = false;
                    }
                    if (shouldAdd && $.inArray(arrEnabledItem[columnIndex], arrFiltered) == -1) arrFiltered.push(arrEnabledItem[columnIndex]);
                }
            }

            return arrFiltered;
        }
        function selectEnabled() { //可选的属性
            var arrEnabled = new Array();
            var arrHasPush = arguments.length > 0 ? selectHasPush(arguments[0]) : selectHasPush();
            for (var i = 0; i < arrGuiGeValue.length; i++) {
                if (arrHasPush.length == 0 || (arrHasPush.length > 0 && $.inArray(arrGuiGeValue[i], arrHasPush) == -1)) {
                    arrEnabled.push(arrGuiGeValue[i]);
                }
            }
            return arrEnabled;
        }
        function selectHasPush() { //已放置属性         
            var arrHasPush = new Array();
            var allElement = arguments.length > 0 ? $("#ggtbody tr").not($(arguments[0]).parent()) : $("#ggtbody tr"); //Lựa chọn 规格属性时需要排除Hiện tại，添加时自动规格属性不需要排除
            $.each(allElement, function (i_tr, item_tr) {
                var temp = "";
                $.each($(item_tr).find("input[name='gg_name']"), function (i, item) {
                    if ($(item).val() != "")
                        temp += $(item).val() + ",";
                });
                if (temp != "")
                    arrHasPush.push(temp.cutRight(','));
            });

            return arrHasPush;
        }
        function attrClick(element, val, event) { //点击属性
            $(element).parents('.ggname').find('span').html(val);
            $(element).parents('.ggname').find('input').val(val);
            delDropMenu();
            event.stopPropagation(); //阻止上级元素td重复生成下拉菜单
        }
        $(document).bind("click", function (e) { //在下拉菜单、属性名称区之外点击，移除下拉菜单
            if ($(e.target).closest("#dropmenu").length == 0 && $(e.target).closest(".ggname").length == 0) {
                delDropMenu();
            }
        })
        function delDropMenu() {
            if ($(document).find("#dropmenu").length > 0) {
                $("#dropmenu").remove();
            }
        }
        $("#btn_addpic").click(function () {
            arrShowPicGuiGeItemNames = []; //重置
            $("#ggtbody tr").each(function (index, item) {
                var tempGuiGe = $(item).find("td").eq(showPicGuiGeIndex).find("input[name='gg_name']").val();
                if (arrShowPicGuiGeItemNames.indexOf(tempGuiGe) == -1) {
                    arrShowPicGuiGeItemNames.push(tempGuiGe);
                }
            });

            //alert(classID + "|" + showPicGuiGeIndex + "|" + showPicGuiGeName + "|" + arrShowPicGuiGeItemNames.toString());
            $.dialog.open("ModifyGuiGePic.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&retid=hdf_backguigepic&pid=<%=proInit==null?0:proInit.AutoID %>&classid=" + classID + "&guigename=" + encodeURI(showPicGuiGeName) + "&guigeitems=" + encodeURI(arrShowPicGuiGeItemNames.toString()), { title: "规格图片", width: 680, height: 450 }, false);
        });
        function dowork(e) {
            var str = "";
            var thumb = ""; //缩略图
            for (var i = 0; i < e.split(',').length; i++) {
                if (e.split(',')[i] != "" && e.split(',')[i].IsPicture()) {
                    thumb = e.split(',')[i].substring(0, e.split(',')[i].lastIndexOf('.')) + "_thumb" + e.split(',')[i].substring(e.split(',')[i].lastIndexOf('.'));
                    str += "<div class=\"col-md-3\"><div class=\"thumbnail\">"
                        + "     <div style=\"height:200px;text-align:center;\"><img onclick=\"$('#imgarea').viewer();\" style=\"max-height:200px;\" src='" + thumb + "' alt='' data-original='" + e.split(',')[i] + "' /></div>"
                        + "     <div class=\"caption\">"
                        + "         <h3>"
                        + "             <input type=\"text\" name=\"imgdesc\" value='' class=\"input-txt\" style=\"width: 160px;\" placeholder=\"Giải thích\" />"
                        + "             <input type=\"hidden\" name=\"proimg\" class=\"input-mid\" value='" + e.split(',')[i] + "' />"
                        + "             <a href=\"javascript:void(0)\" onclick=\"$(this).parent().parent().parent().parent().remove();\" class=\"jian\" title=\"Remove\">[-]</a>"
                        + "         </h3>"
                        + "     </div>"
                        + " </div></div>";
                }
            }
            $("#imgarea").append(str);
        }
    </script>
</asp:Content>
