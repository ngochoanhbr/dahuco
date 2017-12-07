<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyVote.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.ModifyVote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Đề tài khảo sát:</span>
                    <div class="w400 fl">
                        <asp:TextBox placeholder="Vui lòng nhập tên đề tài khảo sát" ID="TextBox1" runat="server" required="required" class="form-control" MaxLength="255"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Có trắc nghiệm hay không:</span>
                    <div class="w200 fl">
                        <asp:CheckBox ID="chkdx" runat="server" class="checkbox_radio"/>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Cho phép vô danh:</span>
                    <div class="w200 fl">
                        <asp:CheckBox ID="chknm" runat="server" class="checkbox_radio"/>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Duyệt:</span>
                    <div class="w200 fl">
                        <asp:CheckBox ID="chksh" runat="server" class="checkbox_radio"/>
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
