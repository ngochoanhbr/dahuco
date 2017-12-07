<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="Upfiles.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ContMger.Upfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/platform/h5/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <script src="/platform/h5/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="/platform/h5/js/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
    <style type="text/css">
        .thumb { cursor: pointer;max-width: 215px; max-height: 215px; }
    </style>
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
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlFolder" runat="server" CssClass="form-control select">
                                </asp:DropDownList>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="selectdate" runat="server" CssClass="form-control" placeholder="Vui lòng nhập tháng tải lên"
                                    date-selectmonth="true" data-date-format="yyyy-mm"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-3">
                                <asp:TextBox ID="search_text" runat="server" CssClass="form-control wicon" placeholder="Hãy nhập tên tập tin"></asp:TextBox>
                            </div>
                            <label for="firstname" class="sr-only">
                                singoocms</label>
                            <div class="col-md-2">
                                <asp:CheckBox ID="showimgonly" runat="server" Text="Chỉ hiển thị hình ảnh" class="checkbox_radio" />
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
                                <asp:LinkButton ID="btn_DelBat" runat="server" OnClick="btn_DelBat_Click" Text="Xóa"
                                    class="btn btn-default ml20 fl" OnClientClick="return singoo.getCheckCount('rowItems')>0 && confirm('OK để xóa nó? \r\ sẽ xóa tất cả các mục đã chọn 无 thể được phục hồi, xin vui lòng thận trọng');" />
                            </div>                            
                            <div class="btn-group">
                                <asp:DropDownList ID="ddlMoveTo" runat="server" CssClass="form-control select">
                                </asp:DropDownList>
                            </div>
                            <div class="btn-group">
                                <asp:Button Text="Chuyển đến" ID="btn_MoveTo" runat="server" OnClick="btn_MoveTo_Click"
                                    class="btn btn-default ml20 fl" OnClientClick="return singoo.getCheckCount('rowItems')>0" />
                            </div>
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning" onclick="$.dialog.open('../Tools/UploadBat.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=Add',{title:'tải lên hàng loạt',width:680,height:410},false);">
                                    Tải lên hàng loạt</button>
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
                    <div class="profile-body mb-20 filemanager fix">
                        <div class="row" id="rowItems" viewer="true">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="thumbnail" style="height: 326px;">
                                            <div style="height:215px;">
                                                <%#ShowPreview(Eval("VirtualPath").ToString(),Eval("Thumb").ToString())%>
                                            </div>
                                            <div class="caption">
                                                <h3>
                                                    <asp:CheckBox ID="chk" runat="server" class="checkbox_radio" />
                                                    <asp:HiddenField ID="autoid" runat="server" Value='<%#Eval("AutoID") %>' />
                                                    <a href='<%#Eval("VirtualPath") %>' target="_blank"><%#Eval("FileName")%></a> (<%#SinGooCMS.Utility.FileUtils.GetFileSize(SinGooCMS.Utility.WebUtils.GetDecimal(Eval("FileSize")))%>)</h3>
                                                <p class="mt-20">
                                                    <asp:LinkButton ID="lnk_Delete" Text="Xóa" class="btn btn-default" runat="server"
                                                        OnClientClick="return confirm('Bạn có chắc chắn xóa')" CommandArgument='<%#Eval("AutoID") %>'
                                                        OnClick="lnk_Delete_Click" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <jweb:SinGooPager ID="SinGooPager1" runat="server" PageSize="20" CssClass="paginator"
                        SplitTag="li" TemplatePath="/platform/h5/pagertemplate.html" OnPageIndexChanged="SinGooPager1_PageIndexChanged" />
                    <!--分页 end-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end 右侧内联框架-->
    </div>
    <!--dialog-->
    <script type="text/javascript">
        //月Lựa chọn 
        $("input[type='text'][date-selectmonth='true']").datetimepicker({ minView: 3, startView: 3, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });

        $('#<%=UpdatePanel1.ClientID %>').panelUpdated(function () {
            $.getScript("/platform/h5/js/AjaxFunction.js");
        });
    </script>
</asp:Content>
