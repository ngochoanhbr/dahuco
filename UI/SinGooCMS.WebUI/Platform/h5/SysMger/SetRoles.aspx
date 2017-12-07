<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="SetRoles.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.SetRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableed > tbody > tr > td
        {
            line-height: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20 fix">
                        <table class="table tableed text-center table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th class="text-center" style="width: 60px;">
                                    </th>
                                    <th class="text-center">
                                        Tên vai trò
                                    </th>
                                    <th class="text-center">
                                        Giải thích
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="checkbox" runat="server" class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" value='<%#Eval("AutoID") %>'/>
                                            </td>
                                            <td>
                                                <%#Eval("RoleName")%>
                                            </td>
                                            <td>
                                                <%#Eval("Remark")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <!--表格Nội dung end-->
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="5" CssClass="paginator"
                        SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                    <!--分页 end-->
                </ContentTemplate>
            </asp:UpdatePanel>            
        </div>
        <div class="profile-body" style="clear: both;">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="OK" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
