<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="KuaidiCompany.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.KuaidiCompany" %>

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
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Cấu hình giao nhận</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-2 control-label">
                            Auth Key</label>
                        <div class="col-md-5">
                            <asp:TextBox ID="AuthKey" runat="server" class='form-control' MaxLength="100" />
                        </div>
                        <asp:Button Text="Lưu cài đặt" ID="btn_SaveKey" runat="server" CssClass="btn btn-danger"
                            OnClick="btn_SaveKey_Click" />
                        <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Đến trang web chính thức để đăng ký và áp dụng cho khóa ủy quyền">
                            &#xe613;</i>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20 fix">
                <div class="datafrom">
                    <div class="title">
                        <span>
                            Mã công ty chuyển phát</span>
                        <div class="fr">
                            <div class="col-md-12">
                                <button type="button" class="btn btn-warning" id="addrule">
                                    Thêm</button>
                            </div>
                        </div>
                    </div>
                    <div class="profile-body">
                        <table class="table tableed table-bordered" id="urlrewrite">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th style="width: 180px;">
                                        Tên công ty chuyển phát
                                    </th>
                                    <th>
                                         Mã công ty chuyển phát
                                    </th>
                                    <th style="width: 80px;">
                                        Xử lý
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden">
                                                <input type="checkbox" class="checkbox_radio" />
                                            </td>
                                            <td>
                                                <input type='text' class='form-control' value='<%#Eval("CompanyName") %>' name='_companyname' />
                                            </td>
                                            <td>
                                                <input type='text' class='form-control' value='<%#Eval("CompanyCode") %>' name='_companycode' />
                                            </td>
                                            <td>
                                                <a href='javascript:;' class='del' onclick="$(this).parent().parent().remove()">Removed</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='4'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btn_SaveRule" Text="Lưu" runat="server" OnClick="btn_SaveRule_Click"
                        class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //Thêm nguyên tắc
        $("#addrule").click(function () {
            var trid = Math.random();
            $("#urlrewrite").append("<tr id='" + trid + "'>"
                                    + "  <td class='hidden'><input type='checkbox' class='checkbox_radio' /></td>"
                                    + "  <td><input type='text' class='form-control' value='' name='_companyname'/></td>"
                                    + "  <td><input type='text' class='form-control' value='' name='_companycode'/></td>"
                                    + "  <td><a href='javascript:;' id='delterule' class='del' onclick=\"removenewtr(" + trid + ")\">Removed</a></td>"
	                                + "</tr>");

            $("tr[id='" + trid + "']").find("input").eq(1).focus();
        });
        function removenewtr(trid) {
            $("tr[id='" + trid + "']").remove();
            return false;
        }
    </script>
</asp:Content>
