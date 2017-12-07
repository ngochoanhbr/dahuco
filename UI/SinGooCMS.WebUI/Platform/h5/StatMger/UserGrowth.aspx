<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="UserGrowth.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.UserGrowth" %>

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
            <ul id="myTab" class="nav nav-tabs container-fluid">
                <li class="active"><a href="#panel1" data-toggle="tab" id="tab1">Trong tuần</a></li>
                <li><a href="#panel2" data-toggle="tab" id="tab2">Theo thời gian</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade in active" id="panel1">
                    <div class="profile-body mb-20">
                        <div class="form-group mt-20 fix">
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
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="sr-only">
                                từ</label>
                            <div class="fl col-md-2">
                                <asp:TextBox ID="timestart" runat="server" CssClass="form-control" date-selector="true" data-date-format="yyyy-mm-dd"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                đến</label>
                            <div class="fl col-md-2">
                                <asp:TextBox ID="timeend" runat="server" CssClass="form-control" date-selector="true" data-date-format="yyyy-mm-dd"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                Bấm</label>
                            <div class="fl col-md-2">
                                <select id="statby" class="form-control">
                                    <option value="月">Tháng</option>
                                    <option value="年">Năm</option>
                                </select>
                            </div>
                            <label for="firstname" class="sr-only">
                                Hình dạng</label>
                            <div class="col-md-2">
                                <select id="shape2" class="form-control">
                                    <option value="MSLine.swf">Line graph</option>
                                    <option value="MSColumn2D.swf">2D bar chart</option>
                                    <option value="MSColumnLine3D.swf">3D bar chart</option>
                                </select>
                            </div>
                            <label for="firstname" class="sr-only">
                            </label>
                            <div class="col-md-2">
                                <input type="button" id="btnstat2" value="Thống kê" class="btn btn-danger" />
                            </div>
                        </div>
                        <!--Thống kê S-->
                        <div id='chartDiv2' style="margin: 50px auto; clear: both; width: 700px;">
                            loading...
                        </div>
                        <!--Thống kê E-->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //DatetimeLựa chọn 
        $("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });

        function showChart(timestart, timeend, statby, shape, showdiv) {
            var flashname = "/Include/Plugin/FusionCharts/Charts/" + shape;
            $.get("XmlProvider/UserGrowthStat.ashx", { t: "getchart", st: timestart, dt: timeend, sy: statby, temp: Math.random() }, function (result) {
                var chart_chartDay = new FusionCharts(flashname, "chartDay", "700", "280", "0", "0", "", "noScale", "CN");
                chart_chartDay.setDataXML(result);
                chart_chartDay.render(showdiv);
            });
        }
        $(function () {
            //默认显示Trong tuầnThống kê
            showChart('<%=System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd")%>', '<%=System.DateTime.Now.ToString("yyyy-MM-dd")%>', '周', $("#shape").find("option:selected").val(), 'chartDiv');
        });
        $("#tab1,#btnstat1").click(function () {
            showChart('<%=System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd")%>', '<%=System.DateTime.Now.ToString("yyyy-MM-dd")%>', '周', $("#shape").find("option:selected").val(), 'chartDiv');
        });
        $("#tab2,#btnstat2").click(function () {
            showChart($("#<%=timestart.ClientID %>").val(), $("#<%=timeend.ClientID %>").val(), $("#statby").find("option:selected").val(), $("#shape2").find("option:selected").val(), 'chartDiv2');
        });
    </script>
</asp:Content>
