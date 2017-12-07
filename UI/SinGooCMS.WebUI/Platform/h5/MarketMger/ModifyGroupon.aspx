<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyGroupon.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.MarketMger.ModifyGroupon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/platform/h5/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="/platform/h5/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="/platform/h5/js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='Groupon.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                     Nhóm mua</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa Nhóm mua</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action == "Add" ? "Thêm Nhóm mua" : "Chỉnh sửa  Nhóm mua"%></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Lựa chọn Các mặt hàng</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" required="required"
                                Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hdf_ProID" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <input id="btn_selectlist" type="button" value="Lựa chọn Các mặt hàng" class="btn btn-default" onclick="$.dialog.open('../Selector/GoodsForActSelect.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&type=single&elementid=null&attr=dowork&backtype=ids',{title:'Lựa chọn Các mặt hàng',width:680,height:470},false);" /></div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Tên hiển thị</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox2" runat="server" class="form-control" required="required"
                                placeholder="Nhập để hiện thị tên hàng hóa" MaxLength="100"></asp:TextBox>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Tên hiển thị khác với bản gốc Tên hàng hóa ">
                            &#xe613;</i>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Thời gian có hiệu lực</label>
                        <div class="col-md-6">
                            <div class="fl w200">
                                <div class="input-group">
                                    <span class="input-group-addon">Bắt đầu</span>
                                    <asp:TextBox Style="width: 160px;" ID="TextBox4" runat="server" CssClass="form-control"
                                        date-selector='true'></asp:TextBox></div>
                            </div>
                            <div class="fl w200 ml-20">
                                <div class="input-group">
                                    <span class="input-group-addon">Kết thúc</span>
                                    <asp:TextBox Style="width: 160px;" ID="TextBox5" runat="server" CssClass="form-control"
                                        date-selector='true'></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hiển thị hình ảnh</label>
                        <div class="col-md-4">
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
                            Số người tham gia</label>
                        <div class="col-md-2">
                            <jweb:H5TextBox Mode="Number" ID="TextBox6" runat="server" CssClass="form-control"></jweb:H5TextBox>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thang giá</label>
                        <div class="col-md-8">
                            <div style="text-align: right">
                                <a href="javascript:void(0)" class="jia">Thêm dòng[+]</a></div>
                            <table class="table" id="jttab">
                                <asp:Repeater ID="rpt_img" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="fl w200">
                                                    Số lượng đạt được：<input type="number" name="numdd" value='<%#Eval("JoinNum") %>' class="input-txt" />
                                                </div>
                                                <div class="fl w200">
                                                    Giá thích hợp：<input type="number" name="xsprice" value='<%#SinGooCMS.Utility.WebUtils.GetDecimal(Eval("Price")).ToString("f2")%>'
                                                        class="input-txt" />
                                                </div>
                                                <a href="javascript:void(0)" onclick="$(this).parent().parent().remove();" class="jian"
                                                    title="Remove">[-]</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Tóm tắt hoạt động</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="TextBox7" TextMode="MultiLine" Rows="3" Columns="60" runat="server"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="profile-body">
                    <div class="datafrom text-right">
                        <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                        <input id="btncancel" onclick="location='Groupon.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                            type="button" value="Hủy bỏ" class="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $("#<%=TextBox4.ClientID %>,#<%=TextBox5.ClientID %>").datetimepicker({ minView: 0, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left", format: "yyyy-mm-dd hh:ii:ss" });
        function dowork(params) {
            $.getJSON("/Platform/h5/Ajax/AjaxMethod.aspx?type=getpro&pid=" + params + "&temp=" + Math.random(), function (data) {
                $("#<%=TextBox1.ClientID %>").val(data.ProductName);
                $("#<%=hdf_ProID.ClientID %>").val(data.AutoID);
                $("#<%=TextBox2.ClientID %>").val(data.ProductName);
                $("#<%=Image1.ClientID %>").attr("src", data.ProImg);
                $("#<%=TextBox3.ClientID %>").val(data.ProImg);
            });
        }
        $(".jia").click(function () {
            $("#jttab").append('<tr><td><div class="fl w200">Số lượng đạt được：<input type="number" name="numdd" class="input-txt" /></div> <div class="fl w200">Giá thích hợp：<input type="number" name="xsprice" class="input-txt"/></div> <a href="javascript:void(0)" onclick="$(this).parent().parent().remove();" class="jian" title="移除">[-]</a></td></tr>');
            return false;
        });
    </script>
</asp:Content>
