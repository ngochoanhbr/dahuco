<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyMessageKeyRly.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.WeixinMger.ModifyMessageKeyRly" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='MessageKeyRly.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                     từ khóa Trả lời</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa từ khóa Trả lời</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm từ khóaTrả lời" : "Chỉnh sửa  từ khóaTrả lời"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em> từ khóa</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" required="required" MaxLength="50" placeholder=" từ khóa"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>text</label>
                        <div class="col-md-5">
                            <asp:TextBox TextMode="MultiLine" ID="TextBox1" runat="server" Rows="5" Columns="80"
                                CssClass="form-control" required="required" placeholder="Nhập văn bản trả lời" lenlimit="2000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            image</label>
                        <div class="col-md-5">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox2.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <jweb:FullImage ID="Image1" runat="server" viewer='true'/>
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mô Tả</label>
                        <div class="col-md-5">
                            <asp:TextBox ID="TextBox3" runat="server" Rows="5" Columns="60" CssClass="form-control"
                                TextMode="MultiLine" placeholder="Nhập mô tả" lenlimit="2000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            liên kết</label>
                        <div class="col-md-5">
                            <jweb:H5TextBox Mode="Url" ID="TextBox4" runat="server" CssClass="form-control" MaxLength="255"></jweb:H5TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                    <input id="btncancel" onclick="location='MessageKeyRly.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
