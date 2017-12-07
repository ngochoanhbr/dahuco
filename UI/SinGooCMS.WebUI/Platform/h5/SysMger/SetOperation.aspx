<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="SetOperation.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.SetOperation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableed > tbody > tr > td
        {
            line-height: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content">
        <div class="fix white-bg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <table class="table tableed text-center table-hover">
                            <thead>
                                <tr class="active">
                                    <th class="text-center hidden" style="width: 60px;">
                                    </th>
                                    <th class="text-center">
                                        Tên xử lý
                                    </th>
                                    <th class="text-center">
                                        Mã xử lý
                                    </th>
                                    <th class="text-center" style="width: 150px;">
                                        Xử lý
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden">
                                                <input type="checkbox" class="checkbox_radio" />
                                                <input type="hidden" name="autoid" value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("OperateName")%>
                                            </td>
                                            <td>
                                                <%#Eval("OperateCode")%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnk_Delete" Text="Xóa" CommandArgument='<%#Eval("AutoID")%>' runat="server"
                                                    OnClientClick="return confirm('Bạn có chắc chắn xóa?')" OnClick="lnk_Delete_Click" />
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
            <ul class="areacolumn">
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên xử lý:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Mã xử lý:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
            </ul>
        </div>
        <div class="profile-body" style="clear: both;">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="thêm vào" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                <asp:LinkButton ID="btn_AddDefault" Text="Thêm mặc định" runat="server" class="btn btn-danger"
                    OnClick="btn_AddDefault_Click"></asp:LinkButton>
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
