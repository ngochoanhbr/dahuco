<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifySettingCate.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.ModifySettingCate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên cấu hình:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên loại cấu hình ví dụ 'UserInfo'" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên hiển thị:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên hiển thị ví dụ 'Tùy chọn thành viên'" MaxLength="100"></asp:TextBox>
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
