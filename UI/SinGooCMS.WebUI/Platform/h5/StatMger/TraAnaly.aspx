﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="TraAnaly.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.TraAnaly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Include/Plugin/FusionCharts/FusionCharts.js" type="text/javascript"></script>
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
            <!--筛选条件 end-->
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Luồng Thống kê</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="sr-only">
                            từ</label>
                        <div class="fl col-md-2">
                            <asp:TextBox ID="timestart" runat="server" CssClass="form-control" date-selector='true'
                                data-date-format="yyyy-mm-dd" data-date-minview="2"></asp:TextBox>
                        </div>
                        <label for="firstname" class="sr-only">
                            đến</label>
                        <div class="fl col-md-2">
                            <asp:TextBox ID="timeend" runat="server" CssClass="form-control" date-selector='true'
                                data-date-format="yyyy-mm-dd"></asp:TextBox>
                        </div>
                        <label for="firstname" class="sr-only">
                            Nhấn</label>
                        <div class="fl col-md-2">
                            <select id="statby" class="form-control">
                                <option value="月">tháng</option>
                                <option value="年">năm</option>
                            </select>
                        </div>
                        <label for="firstname" class="sr-only">
                            Hình dạng</label>
                        <div class="col-md-2">
                            <select id="shape" class="form-control">
                                <option value="MSLine.swf">Line graph</option>
                                <option value="MSColumn2D.swf">2D bar chart</option>
                                <option value="MSColumnLine3D.swf">3D bar chart</option>
                            </select>
                        </div>
                        <label for="firstname" class="sr-only">
                        </label>
                        <div class="col-md-2">
                            <input type="button" id="btnstat" value="Thống kê" class="btn btn-danger" />
                        </div>
                    </div>
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
    <script type="text/javascript">
        //DatetimeLựa chọn 
        $("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });

        //Bắt đầuDatetime
        var timetart;
        //Kết thúcDatetime
        var timeend;
        //Theo thời gian
        var statby;
        //Hình dạng
        var shape;
        function getParameter() {
            timetart = $("#<%=timestart.ClientID %>").val();
            timeend = $("#<%=timeend.ClientID %>").val();
            statby = $("#statby").find("option:selected").val();
            shape = $("#shape").val();
        }
        function showChart() {
            var flashname = "/Include/Plugin/FusionCharts/Charts/" + shape;
            $.get("XmlProvider/GetTrafficStatData.ashx", { t: "getchart", st: timetart, dt: timeend, sy: statby, temp: Math.random() }, function (result) {
                var chart_chartDay = new FusionCharts(flashname, "chartDay", "700", "280", "0", "0", "", "noScale", "CN");
                chart_chartDay.setDataXML(result);
                chart_chartDay.render("chartDiv");
            });
        }
        $(function () {
            getParameter();
            showChart();
        });
        $("#btnstat").click(function () {
            getParameter();
            showChart();
        });
    </script>
</asp:Content>
