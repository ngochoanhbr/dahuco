<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SelectClass.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.SelectClass" %>

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
                        Lựa chọn Danh mục hàng hóa</h2>
                    <div class="results fix" id="classes">
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <input type="button" value="上一步(Lựa chọn 分类)" class="btn btn-danger" id="btn_prev" />
                    <input type="button" value="Bước tiếp theo(SửaCác mặt hàng)" class="btn btn-danger" id="btn_next" />
                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="top" title="Danh mục hàng hóa ảnh hưởng đến giá và tồn kho">
                        &#xe613;</i>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var classid = 0; //类目ID 
        $(function () {
            getClass(0);
        });
        function getClass(classid) { //读取类目
            var str = "<div class='results_z0 bar'><ul>";
            var ajaxdata = "";
            $.get("/Platform/h5/Ajax/AjaxMethod.aspx?type=GetGoodsClassListXML&id=" + classid + "&temp=" + Math.random(), function (data) {
                var json = eval('(' + data + ')');
                if (json.length > 0) {
                    $.each(json, function (index, item) {
                        ajaxdata += "<li value='" + item.id + "' onclick=\"itemClick(this);classid=$(this).val();$(this).parent().parent().nextAll().remove();getClass(" + item.id + ");\">" + item.name + "</li>";
                    });
                }
                if (classid == 0)
                    ajaxdata = "<li value='0' onclick=\"itemClick(this);classid=$(this).val();$(this).parent().parent().nextAll().remove();\">不Chọn loại</li>" + ajaxdata;
                if (ajaxdata != "") {
                    str = str + ajaxdata + "</ul></div>";
                    $("#classes").append(str);
                }
            });
        }
        function itemClick(obj) {
            $(obj).addClass("results_s1").siblings().removeClass("results_s1");
        }
        $("#btn_prev").click(function () { //上一步
            location = "GoodsPulish.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&opid=" + (typeof (singoo.request["opid"]) == "undefined" ? 0 : singoo.request["opid"]) + "&action=" + singoo.request["action"] + "&Status=1";
        });
        $("#btn_next").click(function () { //Bước tiếp theo
            location = "ModifyProduct.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=88DF4B2F399A2FE1&CateID=<%=cate.AutoID %>&ClassID=" + classid + "&opid=" + (typeof (singoo.request["opid"]) == "undefined" ? 0 : singoo.request["opid"]) + "&action=" + singoo.request["action"] + "&Status=1";
        });
    </script>
</asp:Content>
