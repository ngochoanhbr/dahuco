<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyAreaModel.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyAreaModel" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='AreaModelList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Bản mẫu tỉnh thành</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa bản mẫu tỉnh thành</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm bản mẫu tỉnh thành" : "Chỉnh sửa  bản mẫu tỉnh thành"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            <em>*</em>Tên bản mẫu</label>
                        <div class="col-md-4">
                            <asp:TextBox tip="Tên bản mẫu" ID="TextBox1" runat="server" required="required" CssClass="form-control"
                                MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            <em>*</em>khu vực giao hàng</label>
                        <div class="col-md-10">
                            <table class="table" id="zonearea">
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClientClick="return save();" OnClick="btnok_Click"
                        CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='AreaModelList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <input id="hfIDs" type="hidden" runat="server" value="" />
    <script>
        $(function () {
            var jsondata = JSON.parse('<%=JsonForProvinceAndCity %>')
            var s = "";
            if (jsondata != null && jsondata.length > 0) {
                $.each(jsondata, function (i, item) {
                    s += "<tr>"
                     + "   <td style=\"width: 17%\">"
                     + "       <input type=\'checkbox\' sname='p1' class='checkbox_radio'/> " + item.Province
                     + "   </td>"
                     + "   <td style=\"width: 83%\">";
                    $.each(item.Citys, function (j, item2) {
                        s += "       <div class='w100 fl ml-20 line-30'><input " + (item2.IsChecked == 1 ? "checked='checked'" : "") + " name='c2' value='" + item2.AutoID + "' type='checkbox' class='checkbox_radio'/> " + item2.City + "</div>"
                    });
                    s += "   </td></tr>";
                });
                $("#zonearea").html(s);
            }

            $("input[sname='p1']").on('ifChecked', function (event) {
                $(this).parents("tr").find("td").eq(1).find("input[type='checkbox']").iCheck('check');
            });
            $("input[sname='p1']").on('ifUnchecked', function (event) {
                $(this).parents("tr").find("td").eq(1).find("input[type='checkbox']").iCheck('uncheck');
            });
        });

        function save() {
            var ids = "";
            $("input[type='checkbox'][name='c2']").each(function () {
                if ($(this).prop("checked")) {
                    ids += $(this).attr('value') + ',';
                }
            });
            $("#<%=hfIDs.ClientID%>").val(ids);
            if (ids == "") {
                showtip("Đã không chọn bất kỳ thành phố nào");
                return false;
            } else {
                return true;
            }
        }    
    </script>
</asp:Content>
