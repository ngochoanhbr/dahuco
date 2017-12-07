<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyCategory.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyCategory" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='Category.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Phân loại hàng hóa</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa phân loại</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action=="Add"?"Thêm phân loại":"Chỉnh sửa phân loại" %></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên danh mục</label>
                        <div class="col-md-4">
                            <asp:TextBox placeholder="Tên danh mục ví dụ 'Điện thoại'" ID="TextBox1" runat="server" required="required"
                                class="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Phân nhóm</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Phân loại và ghi nhãn</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox2" MaxLength="100" required="required" runat="server" class="form-control" onkeyup="value=value.replace(/[^\w\.@-]/ig,'')"
                                onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\w\.@-]/ig,''))"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Không được nhập trùng nhau, nó ảnh hưởng đến rewriteurl, nếu để trống nó sẽ mặc định là ID. Nhập tiếng Anh có nghĩa là tốt nhất. ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Kiểu hàng hóa</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thể loại hình ảnh</label>
                        <div class="col-md-3">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox5.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <asp:Image ID="Image1" runat="server" viewer='true' ImageUrl="../images/tj.png"/>
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tối ưu hóa tìm kiếm từ khóa</label>
                        <div class="col-md-5">
                            <asp:TextBox TextMode="MultiLine" Rows="3" Columns="50" tip="Nhập nhiều từ khóa cách nhau bởi dấu ','" ID="TextBox6"
                                runat="server" class="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tối ưu hóa tìm kiếm description</label>
                        <div class="col-md-5">
                            <asp:TextBox TextMode="MultiLine" Rows="3" Columns="50" tip="Nhập nhiều từ khóa cách nhau bởi dấu ','" ID="TextBox7"
                                runat="server" class="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Giải thích</label>
                        <div class="col-md-5">
                            <asp:TextBox TextMode="MultiLine" Rows="3" Columns="50" ID="TextBox8" runat="server"
                                class="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số item trên trang</label>
                        <div class="col-md-2" style="width:100px;">
                            <jweb:H5TextBox Mode="Number" ID="TextBox9" runat="server" Text="10" class="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            TOP danh mục</label>
                        <div class="col-md-3">
                            <asp:CheckBox ID="CheckBox12" runat="server" class="checkbox_radio" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            TOP danh mục</label>
                        <div class="col-md-3">
                            <asp:CheckBox ID="CheckBox13" runat="server" class="checkbox_radio" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='Category.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>