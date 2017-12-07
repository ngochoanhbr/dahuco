<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyShippingAddr.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.ModifyShippingAddr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Include/Plugin/areaOPT/jquery.areaopt.js" type="text/javascript"></script>
    <link href="/Include/Plugin/areaOPT/theme/jquery.areaopt.css" rel="stylesheet" type="text/css" />    
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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='ShippingAddr.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Địa chỉ giao hàng</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Chỉnh sửa địa chỉ giao hàng</a></li>
            </ul>
            <!--筛选条件 end-->
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action=="Add"?"Thêm địa chỉ giao hàng":"Sửa địa chỉ giao hàng " %></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Người nhận thư</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Vui lòng điền tên thật để nhận thư" required="required" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Thuộc thành viên</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="theusername" runat="server" CssClass="form-control" Enabled="false" required="required"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Vui lòng nhập địa chỉ thành viên">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Quốc gia</label>
                        <div class="col-md-2">
                            <select id="country" name="_country" class="form-control">
                                <option value="Việt Nam">Việt Nam</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tỉnh/thành</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Địa chỉ</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Width="360" tip="Điền vào chi tiết địa chỉ" required="required" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mã số bưu điện</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Điện thoại</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" required="required" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đặt làm địa chỉ mặc định</label>
                        <div class="col-md-3">
                            <input type="checkbox" id="CheckBox7" runat="server" class="ios-switch" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='ShippingAddr.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $().ready(function () {
            jQuery.areaopt.bind('#<%=txtArea.ClientID %>');
        });
    </script>
</asp:Content>