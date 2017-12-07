<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="WXConfig.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.WeixinMger.WXConfig" %>

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
                        Cấu hình WeChat public number</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>AppID</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" required="required" MaxLength="100"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Platform Wechat(https://mp.weixin.qq.com)Apply for a subscription number or service number">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>AppSecret</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required"
                                MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>server address（URL）</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox3" runat="server" class="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Điền vào địa chỉ trang web để gọi giao diện WECHAT">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Token</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" runat="server" class="form-control" required="required"
                                MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>EncodingAESKey</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox5" runat="server" class="form-control" required="required"
                                MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            ExpireMinutes</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox6" runat="server" class="form-control" required="required"
                                MaxLength="10"></jweb:H5TextBox>
                        </div>
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
