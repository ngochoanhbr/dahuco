<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SetPurview.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.SetPurview" %>

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
                <li><a href="Role.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View">
                    quản lý vai trò</a></li>
                <li class="active"><a href="javascript:;">
                    thiết lập quyền</a></li>
            </ul>
            <!--筛选条件 end-->
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Thiết lập Vai trò[<%=role.RoleName %>]thẩm quyền</h2>
                    <asp:Literal runat="server" ID="lbtext" Visible="false"></asp:Literal>
                    <div class="rank">
                        <input type="checkbox" id="checkall" class='checkbox_radio' /><a
                            class="check-txt">Chọn / Bỏ chọn tất cả</a>
                    </div>
                    <div id="rowItems">
                    </div>
                </div>                
            </div>
            <!--end 右侧内联框架-->
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                    <button type="button" class="btn btn-default" onclick="location='Role.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode%>&action=View'">Hủy bỏ</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var json = JSON.parse('<%=jsondata %>');
        var str = "";
        $.each(json, function (i, item) {
            str += "<div class=\"catalog-title\">" + item.CatalogName + "</div>";
            $.each(item.Modules, function (i2, item2) {
                str += "<div class=\"form-group mt-20 fix\">"
                    + "<label for=\"firstname\" class=\"col-md-3 control-label\">"
                    + "    <input type=\"checkbox\" class='checkbox_radio' sname='s1' />"
                    //+ "    <input type='checkbox' sname='s1' />"
                    + "    " + item2.ModuleName + "</label>"
                    + "  <div class=\"col-md-6\">"
                    + "    <div class=\"input-group\">";
                $.each(item2.Operates, function (i3, item3) {
                    str += " <input " + (item3.HasPurview == 1 ? "checked='checked'" : "") + " type=\"checkbox\" name='purviewcollect' class='checkbox_radio' value=\"" + item3.ModuleID + "," + item3.OperateCode + "\" /> " + item3.OperateName;
                    //str += " <input " + (item3.HasPurview == 1 ? "checked='checked'" : "") + " type=\"checkbox\" value=\"" + item3.ModuleID + "," + item3.OperateCode + "\" /> " + item3.OperateName;
                });
                str += "    </div>"
                str += "  </div>";
                str += "</div>";
            })
            str += "</div>";
        });

        $("#rowItems").html(str);

        $("input[sname='s1']").on('ifChecked', function (event) {
            $(this).parents(".form-group").find("input[type='checkbox']").iCheck('check');
        });
        $("input[sname='s1']").on('ifUnchecked', function (event) {
            $(this).parents(".form-group").find("input[type='checkbox']").iCheck('uncheck');
        });
    </script>
</asp:Content>
