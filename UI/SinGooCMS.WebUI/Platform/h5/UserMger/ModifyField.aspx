<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyField.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.ModifyField" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid all">
        <div class="sidebar" id="left-panel">
        </div>
        <div class="profile-wrapper">
            <ol class="breadcrumb breadcrumb-quirk">
                <%=ShowNavigate()%>
            </ol>
            <ul id="myTab" class="nav nav-tabs container-fluid">
                <li><a href="UserGroup.aspx?CatalogID=<%=CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View">
                    Nhóm thành viên</a></li>
                <li><a href="UserField.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&ModelID=<%=intModelID%>&action=View">
                    Trường mở rộng</a></li>
                <li class="active"><a href="javascript:void(0)">Sửa trường dữ liệu</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm trường mở rộng" : "Sửa trường mở rộng"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tên mô hình</label>
                        <div class="col-md-2">
                            <asp:Literal ID="labModelName" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểu dữ liệu</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Textbox" Value="SingleTextType"></asp:ListItem>
                                <asp:ListItem Text="Multitextbox" Value="MultipleTextType"></asp:ListItem>
                                <asp:ListItem Text="CKEditor" Value="MultipleHtmlType"></asp:ListItem>
                                <asp:ListItem Text="RadioButton" Value="RadioButtonType"></asp:ListItem>
                                <asp:ListItem Text="Checkbox" Value="CheckBoxType"></asp:ListItem>
                                <asp:ListItem Text="Dropdownlist" Value="DropDownListType"></asp:ListItem>
                                <asp:ListItem Text="Picture" Value="PictureType"></asp:ListItem>
                                <asp:ListItem Text="MultiPicture" Value="MultiPictureType"></asp:ListItem>
                                <asp:ListItem Text="File Input" Value="FileType"></asp:ListItem>
                                <asp:ListItem Text="Multi File Input" Value="MultiFileType"></asp:ListItem>
                                <asp:ListItem Text="Datetime" Value="DateTimeType"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tên trường</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox2" runat="server" placeholder="Vui lòng nhập tên trường, ví dụ Title" required="required"
                                class="form-control" onkeyup="value=value.replace(/[^\w\.@-]/ig,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\w\.@-]/ig,''))"
                                MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tên hiển thị</label>
                        <div class="col-md-4">
                            <asp:TextBox placeholder="Vui lòng nhập tên hiển thị, ví dụ như 'Tiêu đề'" ID="TextBox3" runat="server" required="required"
                                class="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Chủ đề</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox4" runat="server" class="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mặc định</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox6" TextMode="MultiLine" Rows="3" Columns="50" runat="server"
                                class="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <!--不同Lựa chọn  开始-->
                    <div class="form-group mt-20 fix ext" id="attr1" style="display: none">
                        <label for="firstname" class="col-md-3 control-label">
                            宽 (px)</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="ExtTextBox1" runat="server" Text="200" class="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext" id="attr2" style="display: none">
                        <label for="firstname" class="col-md-3 control-label">
                            Chiều cao (px)</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="ExtTextBox2" runat="server" Text="200" class="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext" id="attr3" style="display: none">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểu Textbox</label>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="ExtRadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                CssClass="checkbox_radio">
                                <asp:ListItem Text="Text" Value="Text" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Password" Value="Password"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext" id="attr4" style="display: none">
                        <label for="firstname" class="col-md-3 control-label">
                            Kiểu Datetime</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="ExtTextBox4" runat="server" class="form-control" Text="yyyy-MM-dd"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext" id="attr5" style="display: none">
                        <label for="firstname" class="col-md-3 control-label">
                            Độ rộng trường</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="ExtTextBox5" runat="server" Text="50" class="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext" id="attr6" style="display: none">
                        <label for="firstname" class="col-md-3 control-label">
                            Mã nguồn</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ExtDropDownList6" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Danh mục văn bản" Value="Text"></asp:ListItem>
                                <asp:ListItem Text="Từ điển dữ liệu" Value="DataDictionary"></asp:ListItem>
                                <asp:ListItem Text="SQL Query" Value="SQLQuery"></asp:ListItem>
                                <asp:ListItem Text="Ajax Data" Value="AjaxData"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext extchild" style="display: none" id="attr7">
                        <label for="firstname" class="col-md-3 control-label">
                            Danh mục văn bản</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="ExtTextBox7" TextMode="MultiLine" Rows="3" Columns="50" runat="server"
                                class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext extchild" style="display: none" id="attr8">
                        <label for="firstname" class="col-md-3 control-label">
                            Từ điển dữ liệu</label>
                        <div class="col-md-8">
                            <asp:TextBox ID="ExtTextBox8" runat="server" class="form-control w200 fl"></asp:TextBox>
                            <div class="fl ml-20"><input type="button" id="btn_getdict" class="btn btn-default" value="Chọn từ điển" onclick="$.dialog.open('../Selector/DictForSelect.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&type=single&elementid=<%=ExtTextBox8.ClientID %>&attr=value&backtype=names',{title:'Chọn từ điển',width:680,height:450},false);" /></div>                            
                        </div>
                    </div>
                    <div class="form-group mt-20 fix ext extchild" style="display: none" id="attr9">
                        <label for="firstname" class="col-md-3 control-label">
                            SQL Query</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="ExtTextBox9" TextMode="MultiLine" Rows="3" Columns="50" runat="server"
                                class="form-control"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Return KeyName <br/> / KeyValue cặp khóa-giá trị ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix ext extchild" style="display: none" id="attr11">
                        <label for="firstname" class="col-md-3 control-label">
                            Ajax Data</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="ExtTextBox11" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="无 đồng bộ nhập đường dẫn url, dữ liệu trở lại Json <br/> (KeyName / KeyValue cặp khóa-giá trị), tùy thuộc vào ParentID ">
                            &#xe613;</i>
                    </div>
                    <!--不同Lựa chọn  Kết thúc-->
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Kích hoạt</label>
                        <div class="col-md-4">
                            <input type="checkbox" runat="server" id="CheckBox7" checked="checked" class="ios-switch" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cho phép rỗng</label>
                        <div class="col-md-4">
                            <input type="checkbox" runat="server" id="CheckBox9" checked="checked" class="ios-switch" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='UserField.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&GroupID=<%=intModelID%>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //单文本
        var group1 = ["attr1", "attr3", "attr5"];
        //普通文本
        var group2 = ["attr1", "attr2","attr5"];
        //Key Value
        var group3 = ["attr5", "attr6", "attr7"];
        //File
        var group4 = ["attr1", "attr5"];
        //Mutile File
        var group5 = ["attr5"];
        //Datetime
        var group6 = ["attr1", "attr4"];
        //HTMLCKEditor
        var group7=["attr1", "attr2"];

        $().ready(function () {
            showextcontrol(<%=ShowGroup %>);
            if(<%=ShowGroup %>==group3)
                showdatafrom("<%=DataSource %>");            
        });
        $("#<%=DropDownList5.ClientID %>").change(function () {
            $(".ext").hide();
            var type = $(this).find("option:selected").val();
            switch (type) {
                case "SingleTextType":
                    showextcontrol(group1);
                    break;
                case "MultipleTextType":
                    showextcontrol(group2);
                    break;
                case "MultipleHtmlType":
                    showextcontrol(group7);
                    break;
                case "RadioButtonType":
                case "CheckBoxType":
                case "DropDownListType":
                    showextcontrol(group3);
                    break;
                case "PictureType":
                case "FileType":
                    showextcontrol(group4);
                    break;
                case "MultiPictureType":
                case "MultiFileType":
                    showextcontrol(group5);
                    break;
                case "DateTimeType":
                    showextcontrol(group6);
                    break;
            }
        });
        function showextcontrol(controlgroup) {
            for (var i = 0; i < controlgroup.length; i++) {
                $("#" + controlgroup[i]).show();
            }
        }
        $("#<%=ExtDropDownList6.ClientID %>").click(function () {
            showdatafrom($(this).find("option:selected").val());
        });
        function showdatafrom(datafrom){
            $(".extchild").hide();
            switch (datafrom) {
                case "Text":                    
                    $("#attr7").show();
                    break;
                case "DataDictionary":
                    $("#attr8").show();
                    break;
                case "SQLQuery":
                    $("#attr9").show();
                    break;
                case "AjaxData":
                    $("#attr11").show();
                    break;
            }
        }
    </script>
</asp:Content>
