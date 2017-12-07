<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyPostageModel.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyPostageModel" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='PostageModelList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Bản mẫu miễn phí</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa Bản mẫu miễn phí</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm Bản mẫu miễn phí" : "Chỉnh sửa Bản mẫu miễn phí"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên bản mẫu</label>
                        <div class="col-md-3">
                            <asp:TextBox tip="Tên bản mẫu" ID="TextBox1" runat="server" required="required" CssClass="form-control"
                                MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Giải thích</label>
                        <div class="col-md-6">
                            <asp:TextBox tip="Giải thích" ID="TextBox2" runat="server" CssClass="form-control" Rows="3"
                                Columns="50" TextMode="MultiLine" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label"></label>
                        <div class="col-md-8">
                            <table id="tbArea" class="table table-hover">
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" OnClientClick="return save();" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='PostageModelList.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                    <input id="hfOldIDs" type="hidden" runat="server" value="" />
                    <input id="hfNewIDs" type="hidden" runat="server" value="" />
                </div>
            </div>
        </div> 
    </div>
    <script type="text/javascript">
        $().ready(function () {
            initData(); //加载邮费设置
        });
        function initData() {
            $("#tbArea").empty();
            var tbcontent = "";
            var oldval = $("#<%=hfOldIDs.ClientID%>").val(); //原有的设置
            if (oldval == "")
                tbcontent = addDefRow(0, 0);
            else {
                var json = eval('(' + oldval + ')')
                $.each(json, function (index, item) {
                    if (item.AreaIDs == "-1") //AreaIDS=-1为默认运费
                        tbcontent += addDefRow(item.ExpFee, item.ExpAddoneFee);
                    else
                        tbcontent += addItemRow(index, item.AreaIDs, item.AreaNames, item.ExpFee, item.ExpAddoneFee);
                });
            }

            tbcontent = tbcontent + "<tr><td style='width: 100%; height: 22px'><a onclick='add(this)' href='javascript:;'>＋Chỉ định thành viên giao hàng theo khu vực</a></td></tr>";
            $("#tbArea").html(tbcontent);
        }
        var addnum = 999;
        function add(e) {            ;
            $(e).parent().parent().parent().find("tr:last").before(addItemRow(++addnum, 9999, '', 0, 0));
        }
        function del(e) {
            $(e).parent().parent().remove();
        }
        function addDefRow(expFee, addoneFee) { //加入默认行
            return "<tr><td>Thiết lập giao hàng mặc định：<input type ='hidden' name='hidareaid' value ='-1'/><input type='hidden' name='txtzonename' value='Mặc định' /><input type ='number' min='0' name='txtfreight' class='input-txt' value='" + expFee + "' /> VNĐ, vận chuyển nhiều hàng hóa thì cần thêm：<input type ='number' name='txtadditional' class='input-txt' value='" + addoneFee + "' min='0'/> VNĐ</td></tr>";
        }
        function addItemRow(index,areaIDs,areaNames, expFee, addoneFee) { //加入行
            var str = "<tr><td>đến <input type ='text' id='txtzone" + index + "' name='txtzonename' onclick='selProvince(this)' class='input-txt' readonly='readonly' value='" + areaNames + "' style='width:130px;' data-toggle='tooltip' data-placement='top' title='" + areaNames + "'/>";
            str += "<input type ='hidden' id='hidareaid" + index + "' name='hidareaid' value ='" + areaIDs + "'/>Cước hàng：";
            str += "<input name='txtfreight' type ='number' class='input-txt' value='" + expFee + "' min='0'/> VNĐ， ";
            str += "Giao nhiều hàng 1 lần cần thêm phí：<input name='txtadditional' type ='number' class='input-txt' value='" + addoneFee + "' min='0'/> VNĐ <a onclick='del(this)' href='javascript:;'>Xóa</a></td></tr>";

            return str;
        }
        function selProvince(omg) { //打开窗口Lựa chọn 省份
            var ids = $(omg).parent().find("input[name='hidareaid']").attr("value");
            var hidid = $(omg).parent().find("input[name='hidareaid']").attr("id");
            $.dialog.open('../Selector/ProvinceForSelect.aspx?type=mutil&elementid=' + $(omg).attr("id") + "," + hidid + '&attr=value,value&backtype=names,ids&original_data=' + ids, { title: 'Chọn khu vực', width: 630, height: 320 }, false);
        }
        function save() {
            var newstr = "["; //[{"AreaIDs":"-1","AreaNames":"默认","ExpFee":0.0,"ExpAddoneFee":0.0}]
            $("#tbArea").find("tr").each(function (i,item) { //遍历每一行
                var areaids = $(item).find("input[name='hidareaid']").val();
                var areanames = $(item).find("input[name='txtzonename']").val();
                var expfee = parseFloat($(item).find("input[name='txtfreight']").val());
                if (isNaN(expfee)) expfee = 0.0;
                var expaddonefee = parseFloat($(item).find("input[name='txtadditional']").val());
                if (isNaN(expaddonefee)) expaddonefee = 0.0;
                if (areanames != null && areanames != "" && areanames != "undefined")
                    newstr += "{\"AreaIDs\":\"" + areaids + "\",\"AreaNames\":\"" + areanames + "\",\"ExpFee\":" + expfee + ",\"ExpAddoneFee\":" + expaddonefee + "},";
            });
            if (newstr.endsWith(','))
                newstr = newstr.substr(0, newstr.length - 1);

            newstr += "]";
            $("#<%=hfNewIDs.ClientID%>").val(newstr);

            return true;
        }
    </script>
</asp:Content>
