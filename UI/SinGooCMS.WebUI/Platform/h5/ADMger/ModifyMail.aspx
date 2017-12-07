<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyMail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.ModifyMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên người sử dụng:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên người sử dụng" MaxLength="100"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Địa chỉ email:</span>
                    <div class="w400 fl">
                        <jweb:H5TextBox Mode="Email" ID="TextBox2" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập địa chỉ email" MaxLength="50"></jweb:H5TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Có thể nhận mail:</span>
                    <div class="w400 fl">
                        <asp:CheckBox ID="istuiding" runat="server" class="checkbox_radio" />
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
