<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyVoteItem.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.ModifyVoteItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tùy chọn khảo sát:</span>
                    <div class="w400 fl">
                        <asp:TextBox placeholder="Vui lòng nhập tùy chọn khảo sát" ID="TextBox1" runat="server" required="required"
                            class="form-control" MaxLength="255"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Số lượng phiếu:</span>
                    <div class="w100 fl">
                        <jweb:H5TextBox Mode="Number" ID="TextBox2" runat="server" CssClass="form-control" Text="0"></jweb:H5TextBox>
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
