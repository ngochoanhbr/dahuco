<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.AddUser" %>

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
                        Chọn nhóm thành viên</h2>
                    <div class="results fix" id="nodes">
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <input type="button" value="Bước tiếp theo" class="btn btn-danger" id="btn_next" disabled="disabled" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var groupid = 0;
        $(function () {
            getNodes();
        });
        function getNodes() { //读取分类
            $.get("/Platform/h5/Ajax/AjaxMethod.aspx?type=GetUserGroupXML&temp=" + Math.random(), function (data) {
                var json = eval('(' + data + ')');
                if (json.length > 0) {
                    var str = "<div class='results_z0 bar'><ul>";
                    $.each(json, function (index, item) {
                        str += "<li value='" + item.id + "' onclick=\"itemClick(this);groupid=$(this).val();$('#btn_next').removeAttr('disabled');$(this).parent().parent().nextAll().remove();\">" + item.name + "</li>";
                    });
                    str += "</ul></div>";
                    $("#nodes").append(str);
                }
            });
        }
        function itemClick(obj) {
            $(obj).addClass("results_s1").siblings().removeClass("results_s1");
        }
        $("#btn_next").click(function () { //Bước tiếp theo直接SửaCác mặt hàngChi tiết
            if (groupid == 0) showtip("Chọn nhóm thành viên");
            else location = "ModifyUser.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=93BEC1E8CA57D67E&GroupID=" + groupid + "&opid=" + (typeof (singoo.request["opid"]) == "undefined" ? 0 : singoo.request["opid"]) + "&action=" + singoo.request["action"] + "";
        });
    </script>
</asp:Content>
