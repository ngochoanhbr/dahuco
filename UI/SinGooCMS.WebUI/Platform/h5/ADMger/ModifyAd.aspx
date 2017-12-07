<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyAd.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.ModifyAd" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='AdsPlaceList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Vị trí quảng cáo</a></li>
                <li><a href="javascript:;" data-toggle="tab" onclick="location='AdsList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&PlaceID=<%=adPlace.AutoID%>&action=View'">
                    Quản lý quảng cáo</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa quảng cáo</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action=="Add"?"Thêm quảng cáo":"Sửa quảng cáo" %></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Quảng cáo</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlADPlace" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên quảng cáo</label>
                        <div class="col-md-4">
                            <asp:TextBox placeholder="Vui lòng nhập tên của quảng cáo" ID="TextBox1" runat="server" required="required"
                                CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Văn bản quảng cáo</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" TextMode="MultiLine" Rows="3" Columns="60" runat="server"
                                CssClass="form-control" lenlimit="1000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hình ảnh quảng cáo</label>
                        <div class="col-md-3">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox6.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">&#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                    <jweb:FullImage ID="Image1" runat="server" viewer='true'/>
                                    </div>
                                    <span class="hidden"><asp:TextBox placeholder="Vui lòng chọn hình ảnh quảng cáo" ID="TextBox6" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Địa chỉ link</label>
                        <div class="col-md-4">
                            <asp:TextBox placeholder="Cần http: // hoặc https: // ở đầu" ID="TextBox2" runat="server"
                                CssClass="form-control" Text="#" MaxLength="100"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nhập # để không nhảy link khi bấm quảng cáo ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian hiệu lực</label>
                        <div class="col-md-6">
                            <div class="w200 fl">
                                <div class="input-group">                                
                                    <span class="input-group-addon">Bắt đầu</span>
                                    <asp:TextBox placeholder="Datetime" ID="timestart" runat="server" CssClass="form-control" date-selector="true" data-date-format="yyyy-mm-dd"></asp:TextBox>
                                </div>
                            </div>
                            <div class="w200 fl ml-20">
                                <div class="input-group">                                
                                    <span class="input-group-addon">Kết thúc</span>
                                    <asp:TextBox placeholder="Datetime" ID="timeend" runat="server" CssClass="form-control" date-selector="true" data-date-format="yyyy-mm-dd"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Quảng cáo hiển thị trên trang chưa hết hạn">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Duyệt</label>
                        <div class="col-md-4">
                            <input type="checkbox" id="isaudit" runat="server" class="ios-switch" checked="checked"/>
                        </div>
                    </div>
                </div>
                <div class="profile-body">
                    <div class="datafrom text-right">
                        <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                        <input id="btncancel" onclick="location='AdsList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&PlaceID=<%=adPlace.AutoID%>&action=View'"
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
