<%@ Page Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.Log.ShowLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>错误日志</title>
    <link href="/Include/Style/base.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Script/jquery.min.js" type="text/javascript"></script>
    <script src="/Include/Script/jquery.updatepanel.js" type="text/javascript"></script>
    <script src="/Include/Plugin/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../../Platform/blue/css/commen.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <!-- 选项卡开始 -->
        <div class="nTab">
            <!-- 标题开始 -->
            <div class="TabTitle">
                <h1>
                    错误日志</h1>
            </div>
            <!-- 内容开始 -->
            <div class="TabContent">
                <div id="myTab0_Content0">
                    <!--内容-->
                    <div class="outborder" style="text-align: right">
                        日期
                        <asp:TextBox ID="timestart" runat="server" Style="width: 100px;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        -
                        <asp:TextBox ID="timeend" runat="server" Style="width: 100px;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        关键字
                        <asp:TextBox ID="search_text" runat="server"></asp:TextBox>
                        <asp:Button ID="searchbtn" runat="server" Text="Tìm" OnClick="searchbtn_Click" />
                    </div>
                    <div class="outborder">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView CssClass="table-normal stripe_tb" AllowPaging="false" AllowSorting="true"  CellSpacing="0" GridLines="none" Width="100%" AutoGenerateColumns="false" runat="server" ID="GridView1" DataKeyNames="AutoID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ck" runat="server" />
                                                #<%#Eval("AutoID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="IP" DataField="IPAddress" ItemStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="错误信息" ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <a href="ShowLogDetail.aspx?action=View&opid=<%#Eval("AutoID") %>">
                                                    <%#Eval("ErrMessage")%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="跟踪信息" DataField="StackTrace" ItemStyle-Width="30%" />
                                        <asp:BoundField HeaderText="发生日期" DataField="AutoTimeStamp" ItemStyle-Width="15%" />
                                        <asp:TemplateField HeaderText="操作" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <a href="ShowLogDetail.aspx?action=View&opid=<%#Eval("AutoID") %>">详情</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div style="margin: 5px;">
                                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="20" CssClass="paginator" TemplatePath="/Include/Log/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="searchbtn" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <!--内容end-->
                </div>
            </div>
        </div>
        <!-- 选项卡结束 -->
    </div>
    </form>
</body>
</html>
