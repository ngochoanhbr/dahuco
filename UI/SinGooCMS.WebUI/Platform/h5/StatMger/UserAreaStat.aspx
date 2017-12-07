<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="UserAreaStat.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.StatMger.UserAreaStat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Include/Plugin/FusionCharts/FusionCharts.js" type="text/javascript"></script>
    <script src="/Include/Plugin/FusionCharts/FusionMaps.js" type="text/javascript"></script>
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
            <div class="profile-body mb-20" style="position:relative;">
                <div class="datafrom">
                    <h2 class="title">
                        phân bố địa lý</h2>
                    <div class="form-group mt-20 fix">
                        <div id='chartDiv' style="margin: 20px auto; clear: both; width: 700px;">
                        </div>
                    </div>
                    <div style="position:absolute;width:160px;height:60px;top:80px;left:30px;">
                        <div style="background:#CD5C5C;width:60px;height:15px;"><img src="../Images/jiangzhang/jz1.gif" alt="" /></div><br />
                        <div style="background:#CD853F;width:60px;height:15px;"><img src="../Images/jiangzhang/jz2.gif" alt="" /></div><br />
                        <div style="background:#FF7F50;width:60px;height:15px;"><img src="../Images/jiangzhang/jz3.gif" alt="" /></div><br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $.get("XmlProvider/GetUserAreaStat.ashx", { temp: Math.random() }, function (result) {
            var myMap = new FusionCharts("/Include/Plugin/FusionCharts/Charts/FCMap_China.swf", "myMap", "800", "500", "0");
            myMap.setDataXML(result);
            myMap.setTransparent(true);
            myMap.render("chartDiv");
        });         
    </script>
</asp:Content>
