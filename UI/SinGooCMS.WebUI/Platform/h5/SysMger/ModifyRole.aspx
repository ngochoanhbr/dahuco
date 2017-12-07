<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyRole.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.ModifyRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên vai trò:</span>
                    <div class="w400 fl">
                        <asp:TextBox placeholder="Vui lòng nhập tên vai trò" ID="TextBox1" runat="server" class="form-control"
                            required="required" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Giải thích:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                            class="form-control" lenlimit="1000"></asp:TextBox>
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