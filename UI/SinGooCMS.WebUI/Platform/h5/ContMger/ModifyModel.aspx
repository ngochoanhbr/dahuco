<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyModel.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ContMger.ModifyModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên model:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Tên model" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Bảng dữ liệu:</span>
                    <div class="w400 fl">
                        <div class="input-group">
                            <span class="input-group-addon">cms_C_</span>
                            <asp:TextBox Style="width: 200px;" ID="TextBox2" runat="server" class="form-control"
                                required="required" placeholder="Vui lòng nhập tên bảng dữ liệu, ví dụ 'Common'" MaxLength="50" onkeyup="value=value.replace(/[^\w@-]/ig,'')"
                                onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\w@-]/ig,''))"></asp:TextBox>
                        </div>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Giải thích:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
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
