<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="ModifyPaymentSet.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyPaymentSet" %>

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
                <li><a href="javascript:;" data-toggle="tab" onclick="location='PaymentSet.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                    Cấu hình thanh toán</a></li>
                <li class="active"><a href="javascript:;" data-toggle="tab">Sửa thanh toán</a></li>
            </ul>
            <div class="profile-body mb-20 areacolumn">
                <div class="datafrom">
                    <h2 class="title">
                        <%=Action=="Add"?"Thêm Cấu hình thanh toán":"Chỉnh sửa Cấu hình thanh toán" %></h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            <em>*</em>Thanh toán</label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control select">
                            </asp:DropDownList>
                        </div>
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Thanh toán sản phẩm chỉ có 1">
                            &#xe613;</i>
                    </div>
                    <div id="paramarea">
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Địa chỉ yêu cầu thanh toán</label>
                        <div class="col-md-4">
                            <input type="text" id="TextBox2" class="form-control" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            URL return</label>
                        <div class="col-md-4">
                            <input type="text" id="TextBox3" class="form-control" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Payment notice address</label>
                        <div class="col-md-4">
                            <input type="text" id="TextBox4" class="form-control" disabled="disabled" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thanh toán trực tuyến</label>
                        <div class="col-md-3">
                            <input type="checkbox" disabled="disabled" id="CheckBox5" class="checkbox_radio" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thanh toán di động</label>
                        <div class="col-md-3">
                            <input type="checkbox" disabled="disabled" id="CheckBox6" class="checkbox_radio" />
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thanh toán qua wechat</label>
                        <div class="col-md-3">
                            <input type="checkbox" disabled="disabled" id="CheckBox7" class="checkbox_radio" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                    <input id="btncancel" onclick="location='PaymentSet.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var paramsVal = <%=ParametersValue %>;
        $().ready(function () {
            getPayTemplate($('#<%=DropDownList1.ClientID %>').val());
        });
        $('#<%=DropDownList1.ClientID %>').change(function () {
            getPayTemplate($(this).val());
        });
        function getPayTemplate(paymentcode) {
            $.getJSON("../Ajax/AjaxMethod.aspx?type=GetPayTemplate&paycode=" + paymentcode + "&temp=" + Math.random(), function (data) {
                $("#paramarea").html("");
                $("#TextBox2").val(data.PayUrl);
                $("#TextBox3").val(data.ReturnUrl);
                $("#TextBox4").val(data.NotifyUrl);
                if (data.IsOnline) $("#CheckBox5").iCheck("check");
                else $("#CheckBox5").iCheck("uncheck");
                if (data.IsMobile) $("#CheckBox6").iCheck("check");
                else $("#CheckBox6").iCheck("uncheck");
                if (data.IsWeixin) $("#CheckBox7").iCheck("check");
                else $("#CheckBox7").iCheck("uncheck");

                //初始化参数
                var params = eval('(' + data.Parameters + ')')                
                var str = "";
                for (var key in params) {
                    str+="<div class='form-group mt-20 fix'><label for='firstname' class='col-md-3 control-label'><em>*</em>"+params[key]+"</label>"
                         +"   <div class='col-md-4'>"
                         +"       <input type='hidden' name='p-key' value='"+key+"'/><input type='text' name='p-value' id=\"p-" + key + "\" class='form-control' value=\""+(paramsVal[key]==null?"":paramsVal[key])+"\" />"
                         +"   </div>"
                         +"</div>";
                    //str += "<tr><td class=\"left\">" + params[key] + "</td>"
                    //    + " <td class=\"right\"><input type='hidden' name='p-key' value='"+key+"'/><input type=\"text\" name='p-value' id=\"p-" + key + "\" class=\"input-normal\" value=\""+(paramsVal[key]==null?"":paramsVal[key])+"\"  /></td>"
                    //    + " </tr>";
                }
                $("#paramarea").html(str);
            });
        }
    </script>
</asp:Content>
