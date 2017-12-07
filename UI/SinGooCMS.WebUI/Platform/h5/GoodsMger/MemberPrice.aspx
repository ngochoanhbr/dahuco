<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="MemberPrice.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.MemberPrice" %>

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
            <div class="profile-body">
                <table class="table tableed text-center table-hover">
                    <thead>
                        <tr class="active">
                            <th class="text-center">
                                Cấp bậc thành viên
                            </th>
                            <th class="text-center">
                                Giá
                            </th>
                            <th class="text-center">
                                Mặc định
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("UserLevelName") %>
                                        <input type="hidden" name="userlevelid" value='<%#Eval("UserLevelID") %>' />
                                        <input type="hidden" name="userlevelname" value='<%#Eval("UserLevelName") %>' />
                                    </td>
                                    <td>
                                        <input type='text' class='input-txt' value='<%#SinGooCMS.Utility.WebUtils.GetDecimal(Eval("Price")).ToString("f2") %>'
                                            name='userprice' />
                                    </td>
                                    <td>
                                        <%#SinGooCMS.Utility.WebUtils.GetDecimal(Eval("DiscoutPrice")).ToString("C2")%>
                                        <input type="hidden" name="discountprice" value='<%#Eval("DiscoutPrice")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <asp:Button ID="btn_Save" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btn_Save_Click" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>