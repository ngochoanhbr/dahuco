<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifySearchRank.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifySearchRank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tìm từ khóa:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Tìm từ khóa" MaxLength="255"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>số lần Tìm:</span>
                    <div class="w100 fl">
                        <jweb:H5TextBox Mode="Number" ID="TextBox2" runat="server" class="form-control" required="required"
                            MaxLength="10"></jweb:H5TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Đề nghị:</span>
                    <div class="w100 fl" style="width:50px;">
                        <asp:CheckBox ID="CheckBox3" runat="server" CssClass="checkbox_radio" />
                    </div>
                    <i class="iconfont" data-toggle="tooltip" data-placement="right" title="Đề nghị xuất hiện trước từ khóa">
                        &#xe613;</i>
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
