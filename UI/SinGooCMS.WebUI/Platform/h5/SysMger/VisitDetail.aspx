<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="VisitDetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.VisitDetail" %>

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
            <!--筛选条件 end-->
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Xem Log Chi tiết</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Địa chỉ IP</label>
                        <div class="col-md-9">
                            <%=VInfo.IPAddress %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Xử lýThuộc hệ thống</label>
                        <div class="col-md-9">
                            <%=VInfo.OPSystem %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Ngôn ngữ</label>
                        <div class="col-md-9">
                            <%=VInfo.CustomerLang %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            trình duyệt</label>
                        <div class="col-md-9">
                            <%=VInfo.Navigator %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thông tin Người sử dụng</label>
                        <div class="col-md-9">
                            <%=VInfo.UserAgent %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Địa chỉ IP</label>
                        <div class="col-md-9">
                            <%=VInfo.IPAddress %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thiết bị di động</label>
                        <div class="col-md-9">
                            <%=VInfo.IsMobileDevice %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hỗ trợ cookie</label>
                        <div class="col-md-9">
                            <%=VInfo.IsSupportCookie %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Hỗ trợ Javascript：</label>
                        <div class="col-md-9">
                            <%=VInfo.IsSupportJavascript %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix hidden">
                        <label for="firstname" class="col-md-3 control-label">
                            Phiên bản .NET：</label>
                        <div class="col-md-9">
                            <%=VInfo.NETVer %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thu thập dữ liệu</label>
                        <div class="col-md-9">
                            <%=VInfo.IsCrawler %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Công cụ tìm kiếm</label>
                        <div class="col-md-9">
                            <%=VInfo.Engine %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                             từ khóa</label>
                        <div class="col-md-9">
                            <%=VInfo.KeyWord %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Trang tiền lệ</label>
                        <div class="col-md-9">
                            <%=VInfo.ApproachUrl %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            truy cập trang</label>
                        <div class="col-md-9">
                            <%=VInfo.VPage %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            GET thông số</label>
                        <div class="col-md-9">
                            <%=VInfo.GETParameter %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Post thông số</label>
                        <div class="col-md-9">
                            <%=VInfo.POSTParameter %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Cookie</label>
                        <div class="col-md-9" style="word-break:break-all">
                            <%=VInfo.CookieParameter %>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Thời gian</label>
                        <div class="col-md-9">
                            <%=VInfo.AutoTimeStamp %>
                        </div>
                    </div>
                </div>
                <div class="profile-body mb-20">
                    <div class="datafrom text-right">
                        <button type="button" class="btn btn-default" onclick="location='VisitorLog.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'">
                            Quay lại</button>
                    </div>
                </div>
            </div>
            <!--end 右侧内联框架-->
        </div>
</asp:Content>
