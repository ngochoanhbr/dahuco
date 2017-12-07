<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyCoupons.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.MarketMger.ModifyCoupons" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='Coupons.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Phiếu giảm giá</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa Phiếu giảm giá</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm Phiếu giảm giá" : "Chỉnh sửa Phiếu giảm giá"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tiêu đề</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                                placeholder="Tiêu đề phiếu giảm giá" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Mệnh giá (VNĐ)</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox2" runat="server" class="form-control" required="required"
                                placeholder="Nhập phiếu giảm giá ví dụ 50.000" MaxLength="10"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Giới hạn(VNĐ)</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox3" runat="server" class="form-control" MaxLength="10"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian có hiệu lực</label>
                        <div class="col-md-6" style="width: 450px;">
                            <div class="w200 fl">
                                <div class="input-group">
                                    <span class="input-group-addon">Bắt đầu</span>
                                    <asp:TextBox placeholder="Datetime" ID="TextBox4" runat="server" CssClass="form-control"
                                        date-selector="true" data-date-format="yyyy-mm-dd"></asp:TextBox>
                                </div>
                            </div>
                            <div class="w200 fl ml-20">
                                <div class="input-group">
                                    <span class="input-group-addon">Kết thúc</span>
                                    <asp:TextBox placeholder="Datetime" ID="TextBox5" runat="server" CssClass="form-control"
                                        date-selector="true" data-date-format="yyyy-mm-dd"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thành viên bắt buộc</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <input type="button" value="Lựa chọn thành viên" id="selUser" onclick="$.dialog.open('../Selector/UserForSelect.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&type=single&elementid=<%=TextBox6.ClientID %>&attr=value&backtype=names',{title:'Lựa chọn thành viên',width:680,height:420},false);"
                                class="btn btn-default" /></div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kích hoạt</label>
                        <div class="col-md-4">
                            <input type="checkbox" runat="server" id="chkhasused" class="ios-switch" />
                        </div>
                    </div>
                </div>
                <div class="profile-body">
                    <div class="datafrom text-right">
                        <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                        <input id="btncancel" onclick="location='Coupons.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                            type="button" value="Hủy bỏ" class="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        //DatetimeLựa chọn 
        $("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });
    </script>
</asp:Content>
