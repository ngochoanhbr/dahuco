<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyCustomMenu.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.WeixinMger.ModifyCustomMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .formitemtitle
        {
            width: 80px !important;
        }
        .xhide
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <div class="fl" style="width: 250px" id="menu-left">
                <ul>
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>menu cha:</span>
                        <div class="w100 fl">
                            <asp:DropDownList ID="parentmenu" runat="server">
                            </asp:DropDownList>
                        </div>
                    </li>
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>Menu Name:</span>
                        <div class="w100 fl" style="width: 130px;">
                            <asp:TextBox ID="menuname" runat="server" CssClass="form-control" required="required"
                                placeholder="Vui lòng nhập tên của menu" lenlimit="100"></asp:TextBox>
                        </div>
                    </li>
                    <li class="mb-20"><span class="formitemtitle"><em>*</em>Kiểu:</span>
                        <div class="w100 fl">
                            <asp:DropDownList ID="menutype" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Hình ảnh" Value="click"></asp:ListItem>
                                <asp:ListItem Text="Địa chỉ" Value="view"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="fr" style="width: 410px; min-height: 320px;" id="menu-right">
                <ul>
                    <li class="mb-20 xhide"><span class="formitemtitle">nội dung:</span>
                        <div class="w400 fl">
                            <asp:TextBox TextMode="MultiLine" ID="TextBox1" runat="server" Rows="3" Columns="80"
                                CssClass="form-control" placeholder="Please enter the push text" lenlimit="2000"></asp:TextBox>
                        </div>
                    </li>
                    <li class="mb-20 xhide"><span class="formitemtitle">image:</span>
                        <div class="w400 fl">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox2.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <jweb:FullImage ID="Image1" runat="server" viewer='true' />
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li class="mb-20 xhide"><span class="formitemtitle">Mô Tả:</span>
                        <div class="w400 fl">
                            <asp:TextBox ID="TextBox3" runat="server" Rows="3" Columns="60" CssClass="form-control"
                                TextMode="MultiLine" placeholder="Please enter a push description" lenlimit="2000"></asp:TextBox>
                        </div>
                    </li>
                    <li class="mb-20 xhide"><span class="formitemtitle">liên kết:</span>
                        <div class="w400 fl">
                            <jweb:H5TextBox Mode="Url" ID="TextBox4" runat="server" CssClass="form-control" MaxLength="255"></jweb:H5TextBox>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" OnClientClick="return checkSubmit()" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var action = "<%=base.Action %>";
        $(function () {
            changeSelect("<%=InitMenuType %>");
        });
        $("#<%=menutype.ClientID %>").change(function () {
            var e = $(this).val();
            changeSelect(e);
        });
        function changeSelect(e) {
            switch (e) {
                case "click":
                    $("#menu-right").find("ul>li").show();
                    break;
                case "view":
                    $("#menu-right").find("ul>li").eq(3).show();
                    $("#menu-right").find("ul>li").not($("#menu-right").find("ul>li").eq(3)).hide();
                    break;
            }
        }
        function checkSubmit() { //提交前检查
            var type = $("#<%=menutype.ClientID %>").val();
            var txt = $("#<%=TextBox1.ClientID %>").val();
            var url = $("#<%=TextBox4.ClientID %>").val();
            if (action!="Modify" && type == "click" && txt == "") {
                showtip("The text of the text push event can not be empty");
                return false;
            } else if (action != "Modify" &&  type == "view" && url == "") {
                showtip("The address of the address jump event can not be empty");
                return false;
            }
        }
    </script>
</asp:Content>
