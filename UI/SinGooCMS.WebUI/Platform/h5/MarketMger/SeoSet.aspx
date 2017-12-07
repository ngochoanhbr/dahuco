<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SeoSet.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.MarketMger.SeoSet" %>

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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20">
                        <div class="form-group fix">
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Tên hàng hóa"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:Button ID="searchbtn" Text="Tìm" runat="server" CssClass="btn btn-success" OnClick="searchbtn_Click" />
                            </div>                            
                        </div>
                        <div class="batchHandleArea fix">
                            <div class="checkall">
                                <input type="checkbox" id="checkall" class="checkbox_radio" />
                            </div>
                            <div class="btn-group">
                                <asp:Button Text="Tiêu đề=> từ khóa" ID="btn_SetKey" runat="server" OnClick="btn_SetKey_Click"
                                    OnClientClick="return singoo.getCheckCount('rowItems')>0" CssClass="btn btn-default ml20 fl" />
                                <asp:Button Text="Tiêu đề=>Mô Tả Sản Phẩm" ID="btn_SetDesc" runat="server" OnClick="btn_SetDesc_Click"
                                    OnClientClick="return singoo.getCheckCount('rowItems')>0" CssClass="btn btn-default ml20 fl" />
                            </div>
                            <div class="fr">
                                <div class="col-md-12">
                                    <asp:DropDownList class="form-control select" ID="drpPageSize" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--筛选条件 end-->
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="rowItems">
                            <thead>
                                <tr class="active">
                                    <th style="width:50px;">
                                    </th>
                                    <th style="width:260px;">
                                        Các mặt hàng
                                    </th>
                                    <th style="width:30%;">
                                        Tìm từ khóa
                                    </th>
                                    <th style="width:30%;">
                                        Tìm Mô Tả Sản Phẩm
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                            </td>
                                            <td>
                                                <span data-toggle="tooltip" data-placement="top" title='<%#Eval("ProductName")%>'><%#SinGooCMS.Utility.StringUtils.Cut(Eval("ProductName").ToString(),30,"...")%></span>
                                            </td>
                                            <td>                                                
                                                <input type="text" value='<%#Eval("SEOKey")%>' data-original='<%#Eval("SEOKey")%>' style='width:100%' onblur="changeSeoKey(<%#Eval("AutoID") %>,this)" />
                                            </td>
                                            <td>
                                                <input type="text" value='<%#Eval("SEODescription")%>' data-original='<%#Eval("SEODescription")%>' style='width:100%' onblur="changeSeoDesc(<%#Eval("AutoID") %>,this)"/>
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
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="20" CssClass="paginator"
                        SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script type="text/javascript">
        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });
        function changeSeoKey(id, e) {
            var originalData = $(e).attr("data-original");
            var thisVal=$(e).val();
            if (originalData != thisVal) {
                $.post("../Ajax/AjaxMethod.aspx", { type: "ChangeSeo", _id: id, _key: encodeURI(thisVal), temp: Math.random() }, function (data) {
                    if(data.ret == "success"){
                        $(e).attr("data-original",thisVal);
                        showtip("Xử lý thành công");
                    }else{
                        showtip(data.msg);
                    }
                }, "JSON");
            }
        }
        function changeSeoDesc(id, e) {
            var originalData = $(e).attr("data-original");
            var thisVal = $(e).val();
            if (originalData != thisVal) {
                $.post("../Ajax/AjaxMethod.aspx", { type: "ChangeSeo", _id: id, _description: encodeURI($(e).val()), temp: Math.random() }, function (data) {
                    if (data.ret == "success") {
                        $(e).attr("data-original",thisVal);
                        showtip("Xử lý thành công");
                    } else {
                        showtip(data.msg);
                    }
                }, "JSON");
            }
        }
    </script>
</asp:Content>