<%@ Page Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.Log.ShowLogDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>错误日志详情</title>
    <link href="/Include/Style/base.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Script/jquery.min.js" type="text/javascript"></script>
    <script src="/Include/Script/jquery.updatepanel.js" type="text/javascript"></script>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            font-size: 12px;
        }
        .managetable
        {
            width: 100%;
            border: 0;
        }
        .managetable td
        {
            background: #f0f0f0;
            height: 30px;
            vertical-align: middle;
            padding: 2px;
        }
        .managetable td.left
        {
            width: 15%;
            text-align: left;
        }
        .managetable td.right
        {
            width: 85%;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- 选项卡开始 -->
    <div class="nTab">
        <!-- 标题开始 -->
        <div class="TabTitle">
            <h1>
                错误日志详情</h1>
        </div>
        <!-- 内容开始 -->
        <div class="TabContent">
            <div id="myTab0_Content0">
                <!--内容-->
                <%if (error != null)
                  { %>
                <table class="managetable">
                    <tr>
                        <td class="left">
                            IP地址
                        </td>
                        <td class="right">
                            <%=error.IPAddress%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            操作系统：
                        </td>
                        <td class="right">
                            <%=error.OPSystem%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            语言文化：
                        </td>
                        <td class="right">
                            <%=error.CustomerLang%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            浏览器：
                        </td>
                        <td class="right">
                            <%=error.Navigator%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            用户信息：
                        </td>
                        <td class="right">
                            <%=error.UserAgent%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            是否移动设备：
                        </td>
                        <td class="right">
                            <%=error.IsMobileDevice%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            是否支持ActiveX：
                        </td>
                        <td class="right">
                            <%=error.IsSupportActiveX%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            是否支持Cookie：
                        </td>
                        <td class="right">
                            <%=error.IsSupportCookie%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            是否支持Javascript：
                        </td>
                        <td class="right">
                            <%=error.IsSupportJavascript%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            .NET版本：
                        </td>
                        <td class="right">
                            <%=error.NETVer%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            是否网络爬虫访问：
                        </td>
                        <td class="right">
                            <%=error.IsCrawler%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            Tìm引擎：
                        </td>
                        <td class="right">
                            <%=error.Engine%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            关键字：
                        </td>
                        <td class="right">
                            <%=error.KeyWord%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            来路页：
                        </td>
                        <td class="right">
                            <%=error.ApproachUrl%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            访问页：
                        </td>
                        <td class="right">
                            <%=error.VPage%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            Get参数：
                        </td>
                        <td class="right">
                            <%=error.GETParameter%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            Post参数：
                        </td>
                        <td class="right">
                            <%=error.POSTParameter%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            Cookie参数：
                        </td>
                        <td class="right">
                            <%=error.CookieParameter%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            报错信息：
                        </td>
                        <td class="right">
                            <%=error.ErrMessage%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            跟踪信息：
                        </td>
                        <td class="right">
                            <%=error.StackTrace%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            访问时间：
                        </td>
                        <td class="right">
                            <%=error.AutoTimeStamp%>
                        </td>
                    </tr>
                </table>
                <%} %>
                <!--内容end-->
            </div>
        </div>
    </div>
    <!-- 选项卡结束 -->
    <div style="margin-top: 20px; width: 100%; text-align: center;">
        <input id="btncancel" onclick="location='ShowLog.aspx'" type="button" value="返 回" />
    </div>
    <br />
    </form>
</body>
</html>
