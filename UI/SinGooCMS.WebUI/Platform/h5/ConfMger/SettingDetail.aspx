<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SettingDetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.SettingDetail" %>

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
                <li><a href="setting.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View">
                    Cấu hình tùy chọn</a></li>
                <li class="active"><a href="javascript:void(0)">Cấu hình Chi tiết</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        Cấu hình Chi tiết</h2>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    <%# Eval("Alias")%></label>
                                <div class="col-md-4">
                                    <jweb:FieldControl ID="field" runat="server">
                                    </jweb:FieldControl>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='setting.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
