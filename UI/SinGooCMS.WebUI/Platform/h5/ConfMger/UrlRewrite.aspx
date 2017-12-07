<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="UrlRewrite.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.UrlRewrite" %>

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
                <div class="form-group fix hidden">
                    <label for="firstname" class="sr-only">
                        singoocms</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="search_text" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label for="firstname" class="sr-only">
                        singoocms</label>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="batchHandleArea fix">
                    <div class="checkall hidden">
                        <input type="checkbox" id="checkall" class="checkbox_radio" />
                    </div>
                    <div class="btn-group hidden">
                        
                    </div>
                    <div class="btn-group">
                        <button type="button" class="btn btn-warning" id="addrule">
                            Thêm nguyên tắc</button>
                    </div>
                    <div class="fr">
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <table class="table tableed table-hover" id="urlrewrite">
                    <thead>
                        <tr class="active">
                            <th class="hidden">
                            </th>
                            <th style="width:150px;">
                                Key Name
                            </th>
                            <th >
                                Địa chỉ nguồn
                            </th>
                            <th >
                                Địa chỉ đích
                            </th>
                            <th style="width:80px;">
                                Trường hệ thống
                            </th>
                            <th style="width:60px;">
                                Xử lý
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("ID") %>
                                        <input type='hidden' name='_keyname' value='<%#Eval("ID") %>'/>
                                        <input type='hidden' name='_issystem' value='<%#Eval("IsSystem").ToString() == "True"?"1":"0"%>' />
                                    </td>
                                    <td>
                                        <%#Eval("Url")%>
                                        <input type='hidden' name='_sourceurl' value='<%#Eval("Url") %>'/>                                        
                                    </td>
                                    <td>
                                        <%#Eval("To") %>
                                        <input type='hidden' name='_tourl' value='<%#Eval("To") %>'/>                                        
                                    </td>
                                    <td>
                                        <%#Eval("IsSystem").ToString() == "True" ? "<i class=\"iconfont font-22 text-success\">&#xe62f;</i>" : "<i class=\"iconfont font-22 \">&#xe62e;</i>"%>
                                    </td>
                                    <td class="text-muted">
                                        <%#Eval("IsSystem").ToString() == "True" ? "<a href='javascript:;' class='del' style='text-decoration: line-through'/>Di chuyển</a>" : "<a href='javascript:;' class='del' onclick=\"$(this).parent().parent().remove()\" />Di chuyển</a>"%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <%=Repeater1.Items.Count == 0 ? "<tr><td colspan='5'> Chúng tôi không tìm thấy bất kỳ dữ liệu</td></tr>" : ""%>
                            </FooterTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btn_SaveRule_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script type="text/javascript">
        //Thêm nguyên tắc
        $("#addrule").click(function () {
            var trid = Math.random();
            $("#urlrewrite").append("<tr id='" + trid + "'>"
		                            + "  <td>"
                                    + "      <input placeholder='Nhập tên rewrite url' type='text' class='form-control' value=''  name='_keyname' />"
                                    + "  </td><td>"
                                    + "      <input placeholder='Nhập url nguồn' type='text' class='form-control' value='' name='_sourceurl' style='width:96%' />"
                                    + "  </td><td>"
                                    + "      <input placeholder='Nhập url đích' type='text' class='form-control' value='' name='_tourl' style='width:96%' />"
                                    + "  </td><td>"
                                    + "      <i class='iconfont font-22'>&#xe62e;</i><input type='hidden' name='_issystem' value='0' />"
                                    + "  </td><td>"
                                    + "      <a href='javascript:;' id='delterule' class='del' onclick=\"removenewtr(" + trid + ")\">Di chuyển</a>"
                                    + "  </td>"
	                                + "</tr>");

            $("tr[id='" + trid + "']").find("input").eq(1).focus();
        });
        function removenewtr(trid) {
            $("tr[id='" + trid + "']").remove();
            return false;
        }
    </script>
</asp:Content>