<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyDict.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.ModifyDict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên từ điển:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên của từ điển 如 CertType" MaxLength="50"></asp:TextBox>
                    </div>
                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Từ điển là duy nhất, không được trùng">
                        &#xe613;</i>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tiêu đề hiển thị:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" placeholder="Vui lòng nhập tiêu đề hiển thị ví dụ Loại tài liệu" runat="server" class="form-control" required="required" MaxLength="100"></asp:TextBox>
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
