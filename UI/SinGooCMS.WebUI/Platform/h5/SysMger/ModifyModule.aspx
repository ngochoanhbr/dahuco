<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyModule.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.ModifyModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Ngành tương ứng:</span>
                    <div class="w100 fl">
                        <asp:DropDownList ID="DropDownList5" runat="server" class="form-control select">
                        </asp:DropDownList>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên mô-đun:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên mô-đun như danh sách tên cửa hàng" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>mã Module:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required"
                            placeholder="Nhập mã mô-đun như StoreList" MaxLength="50"></asp:TextBox>
                    </div>
                </li>                
                <li class="mb-20"><span class="formitemtitle"><em>*</em>file Path:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox3" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập một đường dẫn tương đối" MaxLength="255"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20 hidden"><span class="formitemtitle">Giải thích:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                            class="form-control" lenlimit="255"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">màn hình hiển thị mặc định:</span>
                    <div class="w400 fl">
                        <input type="checkbox" id="isdefault" runat="server" class="ios-switch" />
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
