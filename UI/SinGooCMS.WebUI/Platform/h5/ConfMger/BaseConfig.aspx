<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="BaseConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.BaseConfig" %>

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
            <!--筛选条件 end-->
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Cấu hình cơ bản</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tên trang web</label>
                        <div class="col-md-4">
                            <asp:TextBox placeholder="Nhập tên trang web" ID="TextBox1" runat="server" class="form-control"
                                required="required" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            URL của trang web</label>
                        <div class="col-md-4">
                            <jweb:H5TextBox Mode="Url" placeholder="Cần http: // hoặc https: // ở đầu" ID="TextBox2"
                                runat="server" class="form-control" MaxLength="100"></jweb:H5TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Sau khi trang web triển khai chính thức, vui lòng xác nhận xem tên miền chính thức. Việc mua hàng hoá, sẽ cần đến để thanh toán！">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hình ảnh logo</label>
                        <div class="col-md-1">
                            <div class="images_model">
                                <div class="box-hd">
                                   <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox4.ClientID %>,imgarea,imgarea','value,src,data-original');">&#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <img src="<%=SinGooCMS.Config.ConfigProvider.Configs.SiteLogo %>" alt="" id="imgarea" viewer='true' data-original="<%=SinGooCMS.Config.ConfigProvider.Configs.SiteLogo %>" />
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control" MaxLength="100"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix hidden">
                        <label for="firstname" class="col-md-3 control-label">
                            Hình ảnh banner</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox10" Width="300px" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thông tin bản quyền</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox5" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Record number</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox6" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Ngôn ngữ mặc định</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="showlang" runat="server" class="form-control select">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Chế độ duyệt web</label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="DropDownList10" runat="server" class="form-control select">
                                <asp:ListItem Text="aspxAddress overload (không có phần mở rộng)" Value="UrlRewriteNoAspx"></asp:ListItem>
                                <asp:ListItem Text="Hiển thị động aspx" Value="Aspx"></asp:ListItem>
                                <%--<asp:ListItem Text="aspx地址重载(含扩展名)" Value="UrlRewriteAndAspx"></asp:ListItem>--%>
                                <asp:ListItem Text="Giả web tĩnh html" Value="HtmlRewrite"></asp:ListItem>
                                <%--<asp:ListItem Text="html静态显示" Value="Html"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Giả web tĩnh html cần phải được cấu hình ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Số lượng bài viết khi phân trang</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="globalpagesize" runat="server" class="form-control"
                                Text="10"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Quy tắc menu tĩnh</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="nodehtmlrule" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Quy tắc nội dung tĩnh</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="contenthtmlrule" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tập tin mở rộng cho html</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="htmlext" runat="server" class="form-control" MaxLength="5"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <!--end 右侧内联框架-->
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>