<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SellStat.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.SellStat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Include/Plugin/FusionCharts/FusionCharts.js" type="text/javascript"></script>
    <script src="/Include/Plugin/FusionCharts/FusionMaps.js" type="text/javascript"></script>
    <script src="/Include/Plugin/FusionCharts/FusionCharts.HC.china.js" type="text/javascript"></script>
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
                <li class="active"><a href="#panel1" data-toggle="tab" id="tab1">Tỷ lệ doanh thu</a></li>
                <li><a href="#panel2" data-toggle="tab" id="tab2">Xếp hạng</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade in active" id="panel1">
                    <div class="profile-body mb-20">
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="sr-only">
                                Hình dạng</label>
                            <div class="col-md-2">
                                <select id="shape" class="form-control">
                                    <option value="Pie2D.swf">Biểu đồ hình tròn 2D</option>
                                    <option value="Pie3D.swf">Biểu đồ hình tròn 3D</option>
                                </select>
                            </div>
                            <label for="firstname" class="sr-only">
                            </label>
                            <div class="col-md-2">
                                <input type="button" id="btnstat1" value="Thống kê" class="btn btn-danger" />
                            </div>
                        </div>
                        <!--Thống kê S-->
                        <div class="profile-body mb-20">
                            <div id='chartDiv' style="margin: 50px auto; clear: both; width: 700px;">
                                loading...
                            </div>
                        </div>
                        <!--Thống kê E-->
                    </div>
                </div>
                <div class="tab-pane fade" id="panel2">
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="prosellrank">
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showChart(shape, showdiv) {
            var flashname = "/Include/Plugin/FusionCharts/Charts/" + shape;
            $.get("XmlProvider/GetSellStat.ashx", { t: "getchart", temp: Math.random() }, function (result) {
                var chart_chartDay = new FusionCharts(flashname, "chartDay", "600", "280", "0", "0", "", "noScale", "CN");
                chart_chartDay.setDataXML(result);
                chart_chartDay.render(showdiv);
            });
        }
        function showRank() {
            $.get("XmlProvider/GetSellStat.ashx", { t: "getjson", temp: Math.random() }, function (result) {
                $("#prosellrank").empty();
                var str = "";
                if (result != "") {
                    var json = eval(result);
                    str = "<thead><tr class='active'><th class='text-center' style='width: 80px;'>Xếp hạng</th><th>Tên hàng hóa</th><th style='width:120px;'>Bán hàng</th><th style='width: 120px;'>Bán hàng</th></tr></thead><tbody>";
                    $.each(json, function (i, item) {
                        str += "<tr><td class='text-center'><strong>" + (i < 3 ? "<img src='../Images/jiangzhang/jz" + (i + 1) + ".gif' alt='' />" : (i + 1).toString()) + "</strong></td><td>" + item.pro + "</td><td>" + item.sellnum + "</td><td>" + item.sellamount + "</td></tr>";
                    });
                    str += "</tbody>";
                } else {
                    str = "Chúng tôi không tìm thấy bất kỳ dữ liệu";
                }
                $("#prosellrank").html(str);
            });
        }
        $(function () {
            showChart($("#shape").find("option:selected").val(), 'chartDiv');
        });
        $("#tab1,#btnstat1").click(function () {
            showChart($("#shape").find("option:selected").val(), 'chartDiv');
        });
        $("#tab2").click(function () {
            showRank();
        });
    </script>
</asp:Content>
