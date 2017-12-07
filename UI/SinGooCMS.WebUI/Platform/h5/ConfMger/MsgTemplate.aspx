<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="MsgTemplate.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.MsgTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li><span class="formitemtitle" style="width:100px;"><em>*</em>Kiểu tin nhắn:</span>
                    <div class="w400 fl" style="width:580px;">
                        <asp:Literal ID="msgtype" runat="server"></asp:Literal>
                    </div>
                </li>
                <asp:Panel ID="PanelTitle" runat="server" Visible="true">
                    <li><span class="formitemtitle" style="width:100px;"><em>*</em>Tiêu đề:</span>
                        <div class="w400 fl" style="width:580px;">
                            <asp:TextBox ID="TextBox1" runat="server" required="required" require="true" CssClass="form-control"
                                Style="width: 300px;"></asp:TextBox>
                        </div>
                    </li>
                </asp:Panel>
                <li><span class="formitemtitle" style="width:100px;">Tag giải thích:</span>
                    <div class="w400 fl" style="width:580px;">
                        <asp:Literal ID="tagdesc" runat="server"></asp:Literal>
                    </div>
                </li>
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <li><span class="formitemtitle" style="width:100px;"><em>*</em>Nội dung bản mẫu:</span>
                        <div class="w400 fl" style="width:580px;">
                            <asp:TextBox ID="TextBox3" runat="server" required="required" require="true" CssClass="form-control"
                                Style="width: 100%; height: 120px;" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </li>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" Visible="false">
                    <li><span class="formitemtitle" style="width:100px;"><em>*</em>Nội dung bản mẫu:</span>
                        <div class="w400 fl" style="width:580px;">
                            <CKEditor:CKEditorControl Toolbar="Basic" ID="TextBox2" PasteFromWordPromptCleanup="true"
                                runat="server" Width="98%" Height="200"></CKEditor:CKEditorControl>
                        </div>
                    </li>
                </asp:Panel>
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
