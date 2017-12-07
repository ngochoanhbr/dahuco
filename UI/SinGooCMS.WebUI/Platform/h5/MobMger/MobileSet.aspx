<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="MobileSet.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.MobMger.MobileSet" %>

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
                        Cài đặt điện thoại</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kích hoạt di động</label>
                        <div class="col-md-2">
                            <input type="checkbox" id="ismobopen" runat="server" class="ios-switch" />
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Chỉ kết thúc khi tương ứng trang web mẫu，Kích hoạt mobile site có giá trị. Không kích hoạt trạng thái mobile site sẽ hiển thị vấn đề khác trên PC.">
                            &#xe613;</i>
                    </div>
                </div>
            </div>
            <!--end 右侧内联框架-->
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button Text="Lưu cài đặt" ID="btn_Save" runat="server" OnClick="btn_Save_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
