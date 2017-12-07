<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyCont.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ContMger.ModifyCont" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='Index.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Danh sách</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa điều khoản</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm điều khoản" : "Chỉnh sửa điều khoản"%></h2>
                    <%if (IsEdit && contInit != null)
                      { %>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            ID</label>
                        <div class="col-md-9">
                            #<%=contInit.AutoID%> <a href="/article/detail/<%=contInit.AutoID %>" target="_blank">Xem điều khoản>></a>
                        </div>
                    </div>
                    <%} %>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mục hiện tại</label>
                        <div class="col-md-9">
                            <asp:Literal ID="nodepath" runat="server"></asp:Literal>                            
                        </div>
                    </div>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    <%# Eval("Alias")%></label>
                                <div class="col-md-9">
                                    <jweb:FieldControl ID="field" runat="server">
                                    </jweb:FieldControl>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểm tra trạng thái</label>
                        <div class="col-md-9">
                            <input type="checkbox" id="isaudit" runat="server" class="ios-switch" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='Index.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script>
        //DatetimeLựa chọn 
        $("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });
    </script>
</asp:Content>