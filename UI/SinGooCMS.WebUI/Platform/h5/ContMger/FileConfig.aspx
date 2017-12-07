<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="FileConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ContMger.FileConfig" %>

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
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        Cài đặt upload</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cho phép upload file</label>
                        <div class="col-md-4">
                            <input type="checkbox" id="CheckBox1" runat="server" class="ios-switch" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Thông số tập tin</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Đuôi tập tin được phép tải lên</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox2" TextMode="MultiLine" Rows="3" Columns="50" runat="server"
                                CssClass="form-control" lenlimit="100"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nhập nhiều đuôi cách nhau bởi dấu '|'">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Giới hạn kích thước tập tin tải lên</label>
                        <div class="col-md-2">
                            <div class="input-group">
                                <jweb:H5TextBox Mode="Number" ID="TextBox3" runat="server" CssClass="form-control" Style="width: 100px;"></jweb:H5TextBox>
                                <span class="input-group-addon">KB</span>
                            </div>                            
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Quy tắc đặt tên tập tin</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox><br />
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Không điền tên tập tin mặc định ban đầu">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Chế độ thumbnail</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddl_cutmode" runat="server" CssClass="form-control select">
                                <asp:ListItem Text="Cut" Value="Cut"></asp:ListItem>
                                <asp:ListItem Text="Chỉ định chiều rộng và chiều cao" Value="HW"></asp:ListItem>
                                <asp:ListItem Text="Chỉ định chiều rộng" Value="W"></asp:ListItem>
                                <asp:ListItem Text="Chỉ định chiều cao" Value="H"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kích thước thumbnails</label>
                        <div class="col-md-3" style="width:200px">
                            <div class="input-group">
                                <span class="input-group-addon">chiều rộng</span>
                                <jweb:H5TextBox Mode="Number" ID="TextBox5" runat="server" CssClass="form-control"></jweb:H5TextBox>
                                <span class="input-group-addon">px</span>
                            </div>
                        </div>
                        <div class="col-md-3" style="width:200px">
                            <div class="input-group">
                                <span class="input-group-addon">chiều cao</span>
                                <jweb:H5TextBox Mode="Number" ID="TextBox6" runat="server" CssClass="form-control"></jweb:H5TextBox>
                                <span class="input-group-addon">px</span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            vị trí watermark：</label>
                        <div class="col-md-4">
                            <asp:Literal ID="position" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểu watermark</label>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="sytype" RepeatDirection="Horizontal" RepeatColumns="2"
                                RepeatLayout="Flow" class="checkbox_radio">
                                <asp:ListItem Text="văn bản Watermark" Value="文字水印" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="hình ảnh watermark" Value="图片水印"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểu văn bản watermark</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Text="HOLYBOT Co. LTD" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Phông chữ watermark văn bản</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="watermarkfontname" runat="server" CssClass="form-control select">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cỡ chữ watermark</label>
                        <div class="col-md-4">
                            <jweb:H5TextBox Mode="Number" ID="TextBox11" Width="100px" runat="server" CssClass="form-control"
                                Text="12"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Màu chữ</label>
                        <div class="col-md-4">
                            <jweb:H5TextBox Mode="Color" ID="TextBox12" Width="80px" runat="server" CssClass="form-control"
                                Text="#ff0000" MaxLength="10"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểu watermark hình ảnh</label>
                        <div class="col-md-4">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="$.dialog.open('../Tools/UploadTools.aspx?type=single&elementid=<%=TextBox13.ClientID %>,watermark,watermark&attr=value,src,data-original',{title:'上传工具',width:800,height:490},false );">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <img id="watermark" viewer="true" data-original="<%=SinGooCMS.Config.ConfigProvider.Configs.WaterMarkImage %>"
                                        src="<%=SinGooCMS.Config.ConfigProvider.Configs.WaterMarkImage %>" alt="" />
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hình ảnh watermark trong suốt</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox14" Width="100px" runat="server" CssClass="form-control"
                                Text="0.6"></jweb:H5TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Chỉ số là 1 thì che mất hình, chỉ số càng nhỏ thì độ sáng càng lớn ">
                            &#xe613;</i>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>    
</asp:Content>
