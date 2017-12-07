<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="NodeModify.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.NodeMger.NodeModify" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='Index.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Cấu hình menu</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa menu</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action=="Add"?"Thêm menu":"Sửa menu" %></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên menu</label>
                        <div class="col-md-3">
                            <asp:TextBox placeholder="Vui lòng nhập tên menu ví dụ như 'About'" ID="TextBox1" runat="server"
                                required="required" class="form-control" MaxLength="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>IDmenu</label>
                        <div class="col-md-3">
                            <asp:TextBox placeholder="Không được trùng nhau, ví dụ như AboutUs" ID="TextBox2" required="required" runat="server"
                                class="form-control" onkeyup="value=value.replace(/[^\w\.@-]/ig,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\w\.@-]/ig,''))" MaxLength="100"></asp:TextBox><br />
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Logo menu Là một giá trị duy nhất cho địa chỉ viết lại và các trang tĩnh tạo ra con đường, nếu không được điền, mặc định là số ID, xin vui lòng điền vào tên tiếng Anh có ý nghĩa ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mục cha</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Nội dung model</label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Chú ý：Chỉnh sửa model nội dung sẽ bị mất">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Banner menu</label>
                        <div class="col-md-3">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox3.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <jweb:FullImage ID="Image1" runat="server" viewer='true'/>
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Ảnh menu</label>
                        <div class="col-md-3">
                            <div class="images_model">
                                <div class="box-hd">
                                    <i class="iconfont" onclick="h5.openUploadTool('single','<%=TextBox5.ClientID %>,<%=Image2.ClientID %>,<%=Image2.ClientID %>','value,src,data-original');">
                                        &#xe682;</i>
                                </div>
                                <div class="box-bd">
                                    <div class="figure-img">
                                        <jweb:FullImage ID="Image2" runat="server" viewer='true'/>
                                    </div>
                                    <span class="hidden">
                                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tối ưu hóa tìm kiếm từ khóa</label>
                        <div class="col-md-4">
                            <asp:TextBox TextMode="MultiLine" Rows="3" Columns="80" placeholder="Nhập nhiều từ khóa cách nhau bởi dấu ','"
                                ID="TextBox6" runat="server" class="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tối ưu hóa tìm kiếm mô tả</label>
                        <div class="col-md-4">
                            <asp:TextBox TextMode="MultiLine" Rows="3" Columns="80" placeholder="Nhập nhiều từ khóa cách nhau bởi dấu ','"
                                ID="TextBox7" runat="server" class="form-control" lenlimit="255"></asp:TextBox>
                        </div>
                    </div>
                    <div class="text-right">
                        <a id="showtab" class="add" style="cursor: pointer">
                            <ico class='glyphicon glyphicon-plus-sign' />
                            Các trường bắt buộc</a></div>
                    <div id="xtx" style="display: none">
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Giải thích</label>
                            <div class="col-md-8">
                                <CKEditor:CKEditorControl ID="NodeDesc" PasteFromWordPromptCleanup="true" runat="server"
                                    Width="100%" Height="360" Toolbar="Basic"></CKEditor:CKEditorControl>
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Số item trên trang</label>
                            <div class="col-md-2">
                                <jweb:H5TextBox Mode="Number" ID="TextBox9" runat="server" Text="10" class="form-control"></jweb:H5TextBox>
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Hiển thị trong menu</label>
                            <div class="col-md-3">
                                <asp:CheckBox ID="CheckBox10" runat="server" Checked="true" class="checkbox_radio" />
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Hiển thị trong điều hướng</label>
                            <div class="col-md-3">
                                <asp:CheckBox ID="CheckBox11" runat="server" Checked="true" class="checkbox_radio" />
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Lên TOP</label>
                            <div class="col-md-3">
                                <asp:CheckBox ID="CheckBox12" runat="server" class="checkbox_radio" />
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Lên khuyến nghị</label>
                            <div class="col-md-3">
                                <asp:CheckBox ID="CheckBox13" runat="server" class="checkbox_radio" />
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Liên kết tùy chỉnh</label>
                            <div class="col-md-4">
                                <asp:TextBox ID="TextBox14" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group mt-20 fix">
                            <label for="firstname" class="col-md-3 control-label">
                                Tùy chọn</label>
                            <div class="col-md-3">
                                <asp:CheckBox ID="CheckBox15" runat="server" Checked="true" class="checkbox_radio" />
                                Cho phép thêm menu con<br />
                                <asp:CheckBox ID="CheckBox16" runat="server" Checked="true" class="checkbox_radio" />
                                Cho phép comment<br />
                                <asp:CheckBox ID="CheckBox17" runat="server" class="checkbox_radio" />
                                Cần phải đăng nhập
                            </div>
                        </div>
                        <div class="form-group mt-20 fix" id="needusergroup" style="display: none">
                            <label for="firstname" class="col-md-3 control-label">
                                Nhóm thành viên cho phép duyệt</label>
                            <div class="col-md-3">
                                <asp:CheckBoxList RepeatDirection="Horizontal" RepeatColumns="5" ID="CheckBoxList18"
                                    runat="server" class="checkbox_radio">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="form-group mt-20 fix" id="needuserlevel" style="display: none">
                            <label for="firstname" class="col-md-3 control-label">
                                Cấp bậc thành viên cho phép duyệt</label>
                            <div class="col-md-8">
                                <asp:CheckBoxList RepeatDirection="Horizontal" RepeatColumns="5" ID="CheckBoxList19"
                                    RepeatLayout="Table" runat="server" class="checkbox_radio table">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Bản mẫu menu trang chủ</label>
                        <div class="col-md-5">
                            <div class="col-md-8 row">
                                <asp:TextBox ID="TextBox20" runat="server" Text="栏目首页.html" class="form-control" MaxLength="100"></asp:TextBox></div>
                            <div class="col-md-4">
                                <input id="btn_selectindex" type="button" value="Lựa chọn bản mẫu" class="btn btn-default" /></div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Bản mẫu danh sách</label>
                        <div class="col-md-5">
                            <div class="col-md-8 row">
                                <asp:TextBox ID="TextBox21" runat="server" Text="栏目列表页.html" class="form-control" MaxLength="100"></asp:TextBox></div>
                            <div class="col-md-4">
                                <input id="btn_selectlist" type="button" value="Lựa chọn bản mẫu" class="btn btn-default" /></div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Bản mẫu Nội dung Chi tiết</label>
                        <div class="col-md-5">
                            <div class="col-md-8 row">
                                <asp:TextBox ID="TextBox22" runat="server" Text="Noidungchitiet.html" class="form-control" MaxLength="100"></asp:TextBox></div>
                            <div class="col-md-4">
                                <input id="btn_selectcontent" type="button" value="Lựa chọn bản mẫu" class="btn btn-default" /></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='Index.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function(){
            $('#<%=CheckBox17.ClientID %>').on('ifChecked', function (event) {
                $("#needusergroup").show();
                $("#needuserlevel").show();
            });
        });
        $('#<%=CheckBox17.ClientID %>').on('ifChecked', function (event) {
            $("#needusergroup").show();
            $("#needuserlevel").show();
        });
        $('#<%=CheckBox17.ClientID %>').on('ifUnchecked', function (event) {
            $("#needusergroup").hide();
            $("#needuserlevel").hide();
        });

        //Lựa chọn bản mẫu
        $("#btn_selectindex").click(function () { $.dialog.open('../TemplateMger/TemplateFileListForSelect.aspx?Module=3DB75D2B46FC4473&action=View&elementid=<%=TextBox20.ClientID %>',{title:'Chọn danh mục首页Bản mẫu',width:680,height:500},false); });
        $("#btn_selectlist").click(function () { $.dialog.open('../TemplateMger/TemplateFileListForSelect.aspx?Module=3DB75D2B46FC4473&action=View&elementid=<%=TextBox21.ClientID %>',{title:'Chọn danh mục列表Bản mẫu',width:680,height:500},false); });
        $("#btn_selectcontent").click(function () { $.dialog.open('../TemplateMger/TemplateFileListForSelect.aspx?Module=3DB75D2B46FC4473&action=View&elementid=<%=TextBox22.ClientID %>',{title:'Chọn danh mụcNội dungBản mẫu',width:680,height:500},false); });
        
        //插入到ckeditorSửa器中
        function upimg(str) {
            if (str == "undefined" || str == "") {
                return;
            }
            str = "<img src='" + str + "' />";
            CKEDITOR.instances.<%=NodeDesc.ClientID %>.insertHtml(str);
        }
        //展开显示
        $("#showtab").click(function(){		
            $("#xtx").toggle();
        });
    </script>
</asp:Content>
