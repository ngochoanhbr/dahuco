<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="GoodsPulish.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.GoodsPulish" %>

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
                        Lựa chọn phân loại mặt hàng</h2>
                    <div class="results fix" id="cates">                       
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <input type="button" value="Bước tiếp theo(Chọn loại)" class="btn btn-danger" id="btn_nextwithlm" disabled="disabled" />
                    <input type="button" value="Bước tiếp theo(Sửa Các mặt hàng)" class="btn btn-danger" id="btn_next" disabled="disabled" />
                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="top" title="Bạn cần phải chọn hoàn tất các mặt hàng，Nhấn vào đây để Bước tiếp theo">
                        &#xe613;</i>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var cateid = 0; //点击即保存此分类ID
        $(function () {
            getCates(0);
        });
        function getCates(cateid) { //读取分类
            $.get("/Platform/h5/Ajax/AjaxMethod.aspx?type=GetProCateListForProXML&id=" + cateid + "&temp=" + Math.random(), function (data) {
                var json = eval('(' + data + ')');
                if (json.length > 0) {
                    var str = "<div class='results_z0 bar'><ul>";
                    $.each(json, function (index, item) {
                        str += "<li value='" + item.id + "' onclick=\"itemClick(this);" + (item.isParent ? "cateid=0;$('#btn_nextwithlm').attr('disabled','disabled');;$('#btn_next').attr('disabled','disabled');" : "cateid=$(this).val();$('#btn_nextwithlm').removeAttr('disabled');;$('#btn_next').removeAttr('disabled');") + ";$(this).parent().parent().nextAll().remove();getCates(" + item.id + ")\">" + item.name + "</li>";
                    });
                    str += "</ul></div>";
                    $("#cates").append(str);
                }
            });
        }
        function itemClick(obj) {
            $(obj).addClass("results_s1").siblings().removeClass("results_s1");
        }
        $("#btn_nextwithlm").click(function () { //Bước tiếp theoChọn loại
            if (cateid == 0) showtip("Lựa chọn phân loại mặt hàng");
            else location = "SelectClass.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&CateID=" + cateid + "&opid=" + (typeof (singoo.request["opid"]) == "undefined" ? 0 : singoo.request["opid"]) + "&action=" + (singoo.request["action"] == "view" ? "Add" : singoo.request["action"]) + "&Status=1";
        });
        $("#btn_next").click(function () { //Bước tiếp theo直接SửaCác mặt hàngChi tiết
            if (cateid == 0) showtip("Lựa chọn phân loại mặt hàng");
            else location = "ModifyProduct.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=88DF4B2F399A2FE1&CateID=" + cateid + "&opid=" + (typeof (singoo.request["opid"]) == "undefined" ? 0 : singoo.request["opid"]) + "&action=" + (singoo.request["action"] == "view" ? "Add" : singoo.request["action"]) + "&Status=1";
        });
    </script>
</asp:Content>
