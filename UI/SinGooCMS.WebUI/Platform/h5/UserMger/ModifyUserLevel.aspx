<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyUserLevel.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.ModifyUserLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên cấp bậc:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên cấp bậc" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Điểm bắt buộc:</span>
                    <div class="w100 fl">
                        <jweb:H5TextBox Mode="Number" ID="TextBox2" runat="server" class="form-control" required="required"
                            MaxLength="10" Text="0"></jweb:H5TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Giảm giá:</span>
                    <div class="w100 fl">
                        <jweb:H5TextBox Mode="Number" ID="TextBox3" runat="server" Text="1" CssClass="form-control" MaxLength="10"></jweb:H5TextBox>
                    </div>
                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Giảm còn 95% thì nhập 0.95">
                        &#xe613;</i> </li>
                <li class="mb-20"><span class="formitemtitle">Giải thích:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="3" Columns="50"
                            CssClass="form-control" lenlimit="500"></asp:TextBox>
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
