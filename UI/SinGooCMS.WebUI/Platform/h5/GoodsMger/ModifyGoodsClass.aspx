<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyGoodsClass.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyGoodsClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ggunit
        {
            border: 1px solid #ccc;
            float: left;
            width: 80px;
            text-align: center;
            margin: 1px 0 1px 3px;
            padding:1px 1px;
        }
        .ggunit input
        {
            text-align: center;
            line-height: 22px;
            width: 40px;
        }
        .ggunit .close
        {
            float: right;
            background: #eee;
            width: 20px;
        }
    </style>
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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='GoodsClass.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Danh mục hàng hóa</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa Danh mục hàng hóa</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm Danh mục hàng hóa" : "Chỉnh sửa Danh mục hàng hóa"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            <em>*</em>Tên danh mục</label>
                        <div class="col-md-3">
                            <asp:TextBox placeholder="Tên danh mục" ID="TextBox1" runat="server" required="required"
                                MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            <em>*</em>Danh mục cha</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            <em>*</em>Nội dung đặc điểm kỹ thuật</label>
                        <div class="col-md-10">
                            <div style="min-height: 260px; background: white;">
                                <div style="width: 100%; text-align: right; background: #eee; height: 30px; line-height: 30px">
                                    <a href="javascript:;" id="addgg">+ Thêm chi tiết kỹ thuật</a></div>
                                <table id="ggtab" class="table">
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 90px;">
                                                    <input title='Điền vào thông số kỹ thuật như: màu' type="text" value='<%#Eval("GuiGeName") %>' style="width: 80px;" class="input-txt" name="ggcate" />
                                                </td>
                                                <td>
                                                    <script type="text/javascript">
                                                        var lst = '<%#Eval("GuiGeValue") %>';
                                                        var arr = lst.split(',');
                                                        for (var i = 0; i < arr.length; i++) {
                                                            document.write('<div class="ggunit"><input title="Điền thông tin vào giá trị của thông số kỹ thuật như: đỏ" type="text" value="' + arr[i] + '" name="ggvalue" /><span class="close"><a href="javascript:;" onclick="$(this).parent().parent().remove();">x</a></span></div>');
                                                        }
                                                    </script>
                                                </td>
                                                <td style="width: 90px;">
                                                    <input type="radio" name="youtu" value="1" <%#Eval("IsImageShow").ToString()=="True"?"checked='checked'":"" %>/>Hiển thị hình ảnh
                                                </td>
                                                <td style="width: 80px;">
                                                    <a href="javascript:;" title='Thêm chi tiết kỹ thuật' onclick="$(this).parents('tr').find('td').eq(1).append(element);">
                                                       Thêm vào</a> | <a href="javascript:;" onclick="$(this).parent().parent().remove();">Di chuyển</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" OnClientClick="return saveClass();" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='GoodsClass.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" name="hdf_guigejson" id="hdf_guigejson" />
    <script type="text/javascript">
        var element = "<div class=\"ggunit\"><input type=\"text\" value=\"\" name=\"ggvalue\" /><span class=\"close\"><a href=\"javascript:;\" onclick=\"$(this).parent().parent().remove()\">x</a></span></div>";
        $("#addgg").click(function () {
            addGuiGe();
        });
        function addGuiGe() { //Thêm chi tiết kỹ thuật，不超过6组
            if ($('#ggtab tr').length >= 6)
                showtip("Thông số kỹ thuật không thể vượt quá 6 nhóm");
            else {
                var str = "<tr>"
                      + "     <td style=\"width:90px;\"><input title='Điền vào thông số kỹ thuật như: màu' type=\"text\" value=\"\" class=\"input-txt\" style=\"width:80px;\" name=\"ggcate\" /></td>"
                      + "     <td><div class=\"ggunit\"><input title='Điền thông tin vào giá trị của thông số kỹ thuật như: đỏ' type=\"text\" value=\"\" name=\"ggvalue\" /><span class=\"close\"><a href=\"javascript:;\" onclick=\"$(this).parent().parent().remove();\">x</a></span></div></td>"
                      + "     <td style=\"width: 90px;\"><input type=\"radio\" name=\"youtu\" value='1' />Hiển thị hình ảnh</td>"
                      + "     <td style=\"width:80px;\"><a href=\"javascript:;\" title='Thêm chi tiết kỹ thuật' onclick=\"$(this).parents('tr').find('td').eq(1).append(element);\">thêm vào</a> | <a href=\"javascript:;\" onclick=\"$(this).parent().parent().remove();\">Di chuyển</a></td>"
                      + "</tr>";
                $("#ggtab").append(str);
            }
        }
        function saveClass() {
            var str = ""; // [{"GuiGeName":"颜色","GuiGeValue":"红色,灰白,紫色","IsImageShow":true}]
            $.each($("#ggtab").find("tr"), function (index, item) {
                var classname = $(item).find("td").eq(0).find("input").val(); //Tên danh mục
                var isImageShow = $(item).find("td").eq(2).find("[name='youtu']:checked").val() == 1; //是否Hiển thị hình ảnh
                var ggvalue = "";
                $.each($(item).find("td").eq(1).find("input[name='ggvalue']"), function (i, d) {
                    if ($(d).val() != "")
                        ggvalue += $(d).val() + ",";
                });
                if (classname != "" && ggvalue != "") {
                    str += "{\"GuiGeName\":\"" + classname + "\"," + "\"GuiGeValue\":\"" + ggvalue.cutRight(',') + "\",\"IsImageShow\":" + isImageShow + "},";
                }
            });

            if (str != "") {
                str = "[" + str.cutRight(',') + "]"; //拼凑成json格式
                $("#hdf_guigejson").val(str);
                return true;
            } else {
                showtip("Vui lòng nhập nội dung đặc điểm kỹ thuật");
                return false;
            }
        }
    </script>
</asp:Content>
