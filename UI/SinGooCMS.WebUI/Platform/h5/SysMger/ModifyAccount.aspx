<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyAccount.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.ModifyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên tài khoản:</span>
                    <div class="w400 fl">
                        <asp:TextBox placeholder="Tên tài khoản" ID="TextBox1" runat="server" class="form-control" required="required" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Password:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" MaxLength="50"></asp:TextBox>                        
                    </div>
                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="top" title="Sửa Trạng thái sẽ Chỉnh sửa mọi thứ trong sản phẩm ">
                        &#xe613;</i>
                </li>
                <li class="mb-20"><span class="formitemtitle">Email:</span>
                    <div class="w400 fl">
                        <jweb:H5TextBox Mode="Email" ID="TextBox3" runat="server" class="form-control" MaxLength="50"></jweb:H5TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Số điện thoại:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox4" runat="server" class="form-control" MaxLength="50" pattern="1[0-9]{10}"></asp:TextBox>
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
