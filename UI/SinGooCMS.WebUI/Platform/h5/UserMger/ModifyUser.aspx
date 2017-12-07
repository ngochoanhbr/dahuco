<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyUser.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.ModifyUser" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='UserList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Danh sách thành viên</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Chỉnh sửa thành viên</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm thành viên" : "Sửa thành viên"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Nhóm thành viên:
                        </label>
                        <div class="col-md-3">
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tên thành viên:</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox1" runat="server" required="required" placeholder="Vui lòng nhập tên đăng nhập kết hợp số + tiếng Anh"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>                    
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            密 码:
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="Password" placeholder="Mật khẩu 无 được nhỏ hơn 6 ký tự"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cấp bậc thành viên:
                        </label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Email:
                        </label>
                        <div class="col-md-3">
                            <jweb:H5TextBox Mode="Email" placeholder="Nó có thể được sử dụng để đăng nhập, lấy lại mật khẩu" ID="TextBox5" runat="server"
                                CssClass="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số điện thoại:
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox placeholder="Nó có thể được sử dụng để đăng nhập, lấy lại mật khẩu" ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <%if (IsEdit && userEdit != null)
                      { %>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số người sử dụng:
                        </label>
                        <div class="col-md-2">
                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="fl w100">
                            <a href="javascript:void(0)" onclick="$.dialog.open('ModifyUserAmount.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&Type=Amount&UserID=<%=userEdit.AutoID %>',{title:'Số tiền nạp',width:480,height:150,lock:false},false)">
                                Số tiền nạp</a>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Lồng ghép người sử dụng:
                        </label>
                        <div class="col-md-2">
                            <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="fl w100">
                            <a href="javascript:void(0)" onclick="$.dialog.open('ModifyUserAmount.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Modify&Type=Integral&UserID=<%=userEdit.AutoID %>',{title:'Số điểm nạp',width:480,height:150,lock:false},false)">
                                Số điểm nạp</a>
                        </div>
                    </div>
                    <%} %>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Dung lượng tập tin:
                        </label>
                        <div class="col-md-2">
                            <div class="input-group">
                                <asp:TextBox ID="FileSpace" runat="server" CssClass="form-control" Text="50"></asp:TextBox>
                                <span class="input-group-addon">MB</span>
                            </div>
                        </div>
                         <div class="fl w100">
                                Kích hoạt
                                <asp:Literal ID="hasupload" runat="server"></asp:Literal>
                                MB</div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Loại tài liệu:
                        </label>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="DropDownList12" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            CMND:
                        </label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    <%#Eval("Alias")%></label>
                                <div class="col-md-9">
                                    <jweb:FieldControl ID="field" runat="server">
                                    </jweb:FieldControl>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            审核Trạng thái</label>
                        <div class="col-md-9">
                            <input type="checkbox" id="isaudit" runat="server" class="ios-switch" checked="checked" />
                        </div>
                    </div>
                    <%if (IsEdit){ %>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian đăng ký</label>
                        <div class="col-md-9">
                            <%=userEdit.AutoTimeStamp.ToString("yyyy-MM-dd HH:mm:ss") %>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='UserList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script>
        //DatetimeLựa chọn 
        $("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });
    </script>
</asp:Content>
