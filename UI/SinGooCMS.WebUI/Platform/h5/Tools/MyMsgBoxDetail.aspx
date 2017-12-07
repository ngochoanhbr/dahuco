<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="MyMsgBoxDetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Tools.MyMsgBoxDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle">Người gửi:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Người nhận:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required"
                            MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Tiêu đề:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox3" runat="server" datatype="require" require="true" msg=" *"
                            Width="300" CssClass="form-control"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Nội dung:</span>
                    <div class="w600 fl">
                        <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                            class="form-control"></asp:TextBox>
                    </div>
                </li>
            </ul>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Cancel(Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
