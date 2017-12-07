<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyIPStrategy.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.ModifyIPStrategy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Địa chỉ IP:</span>
                    <div class="w200 fl">
                        <asp:TextBox required="required" ID="TextBox1" runat="server" CssClass="form-control" placeholder="Địa chỉ IP bắt đầu" style="width:150px;"></asp:TextBox>
                    </div>
                    <div class="w200 fl">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Địa chỉ IP kết thúc" style="width:150px;"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Chiến thuật:</span>
                    <div class="w200 fl">
                        <asp:DropDownList ID="DropDownList3" runat="server" class="form-control select">
                            <asp:ListItem Text="cho phép truy cập" Value="ALLOW"></asp:ListItem>
                            <asp:ListItem Text="truy cập bị từ chối" Value="DENY"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </li>
            </ul>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
