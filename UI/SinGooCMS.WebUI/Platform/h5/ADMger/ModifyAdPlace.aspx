<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyAdPlace.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.ModifyAdPlace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Tên vị trí quảng cáo:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                            placeholder="Vui lòng nhập tên quảng cáo" MaxLength="50"></asp:TextBox>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Bản mẫu:</span>
                    <div class="w200 fl">
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="w200 fl ml-20">
                        <input id="btn_selecttemplate" type="button" value="Lựa chọn bản mẫu" class="btn btn-danger" /></div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Kích thước:</span>
                    <div class="w200 fl" style="width:150px">
                        <div class="input-group">
                            <span class="input-group-addon">Chiều rộng</span>
                            <jweb:H5TextBox Mode="Number" ID="TextBox2" runat="server" class="form-control" placeholder="Vui lòng nhập chiều rộng"
                                MaxLength="10"></jweb:H5TextBox>
                            <span class="input-group-addon">px</span>
                        </div>
                    </div>
                    <div class="w200 fl ml-20" style="width:150px">
                        <div class="input-group">
                            <span class="input-group-addon">Chiều cao</span>
                            <jweb:H5TextBox Mode="Number" ID="TextBox3" runat="server" class="form-control" placeholder="Vui lòng nhập chiều cao"
                                MaxLength="10"></jweb:H5TextBox>
                            <span class="input-group-addon">px</span>
                        </div>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Giải thích:</span>
                    <div class="w400 fl">
                        <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Rows="3" Columns="60"
                            class="form-control" lenlimit="255"></asp:TextBox>
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
    <script type="text/javascript">
        $("#btn_selecttemplate").click(function () {
            $.dialog.open('../TemplateMger/TemplateFileListForSelect.aspx?Module=3DB75D2B46FC4473&action=View&elementid=<%=TextBox5.ClientID %>', { title: 'Mẫu quảng cáo', width: 680, height: 500 }, false);
        });
    </script>
</asp:Content>
