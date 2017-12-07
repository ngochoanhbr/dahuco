<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyDictItem.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.ModifyDictItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Khóa:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập khóa nội dung từ điển ví dụ Color" MaxLength="50"></asp:TextBox>
                    </div>
                    <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nội dung từ điển là duy nhất, không được trùng">
                        &#xe613;</i> </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Giá trị:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox2" placeholder="Vui lòng nhập giá trị từ điển ví dụ 'Green'" runat="server" class="form-control"
                            required="required" MaxLength="100"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Thứ tự:</span>
                    <div class="w200 fl">
                        <jweb:H5TextBox style="width:100px;" Mode="Number" ID="TextBox3" runat="server" class="form-control" MaxLength="10" Text="999"></jweb:H5TextBox>                     
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
