<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyTemplate.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.TemplateMger.ModifyTemplate" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='TemplateList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Quản lý bản mẫu</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa Bản mẫu</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm Bản mẫu" : "Chỉnh sửa Bản mẫu"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên bản mẫu</label>
                        <div class="col-md-3">
                            <asp:TextBox placeholder="Tên bản mẫu" ID="TextBox1" runat="server" required="required"
                                class="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Đường dẫn Bản mẫu</label>
                        <div class="col-md-4">
                            <asp:TextBox placeholder="đường dẫn Bản mẫu /Templates/tcl/" ID="TextBox2" runat="server"
                                Text="/Templates/html/" required="required" class="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Bản mẫu chính</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox3" placeholder="Trang chủ mặc định：index.html" Text="index.html" runat="server"
                                class="form-control" MaxLength="50"></asp:TextBox>                            
                        </div>
                        <div class="col-md-4">
                            <input id="btn_selecthome" class="btn btn-default" type="button" value="Lựa chọn bản mẫu" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            hình ảnh Xem trước</label>
                        <div class="col-md-3">                            
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=previmg.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <asp:Image runat="server" ID="Image1" ImageUrl="/Include/Images/nophoto.jpg" viewer="true" />
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="previmg" runat="server" Width="300" class="form-control"></asp:TextBox>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            tác giả</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox6" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Bản mẫu giới thiệu ngắn gọn</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                                class="form-control select_2" lenlimit="1000"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='TemplateList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("#btn_selecthome").click(function () {
            $.dialog.open('TemplateFileListForSelect.aspx?Module=3DB75D2B46FC4473&action=View&elementid=<%=TextBox3.ClientID %>', { title: 'Lựa chọn Bản mẫu', width: 680, height: 500 }, false);
        });
    </script>
</asp:Content>