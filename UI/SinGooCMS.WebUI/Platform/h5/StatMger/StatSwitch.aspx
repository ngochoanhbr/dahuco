<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="StatSwitch.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.StatSwitch" %>

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
                        Thiết lập Thống kê</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mở Thống kê</label>
                        <div class="col-md-4">
                            <input type="checkbox" id="isstatopen" runat="server" class="ios-switch" />
                        </div>
                    </div>
                </div>
            </div>
            <!--end 右侧内联框架-->
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btn_Save" Text="Lưu" runat="server" OnClick="btn_Save_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
