<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SiteSeo.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.SiteSeo" %>

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
                        Tối ưu hóa tìm kiếm</h2>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Từ khóa công cụ tìm kiếm</label>
                        <div class="col-md-5">
                            <div class="f-textarea">
                                <asp:TextBox ID="TextBox1" TextMode="MultiLine" Rows="3" Columns="80" runat="server"
                                    CssClass="form-control" lenlimit="255" placeholder="Nhiều từ khóa cách nhau bởi dấu ','; không được nhiều hơn 255 ký tự"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group mt-20 fix">
                        <label for="firstname" class="col-md-3 control-label">
                            Mô tả cho công cụ tìm kiếm</label>
                        <div class="col-md-5">
                            <asp:TextBox ID="TextBox2" TextMode="MultiLine" Rows="5" Columns="80" runat="server"
                                CssClass="form-control" lenlimit="255" placeholder="Nhiều từ khóa cách nhau bởi dấu ','; không được nhiều hơn 255 ký tự"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20 fix">
                <div class="datafrom">
                    <h2 class="title">
                        Gửi cho công cụ tìm kiếm</h2>
                    <div class="fop-main mt-20">
                        <div class="btn-group">
                            <a href="http://zhanzhang.baidu.com/sitesubmit/index" target="_blank" class="btn btn-default">
                                Gửi tới Baidu</a> <a href="http://www.google.com/submityourcontent" target="_blank" class="btn btn-default">
                                    Gửi tới Google</a> <a href="http://bing.com/docs/submit.Aspx" target="_blank" class="btn btn-default">
                                        Gửi tới Bing</a> <a href="http://search.yahoo.com/info/submit.html" target="_blank" class="btn btn-default">
                                            Gửi tới Yahoo</a> <a href="http://search.tom.com/tools/weblog/log.php" target="_blank" class="btn btn-default">
                                                Gửi tới TOM</a> <a href="http://tellbot.youdao.com/report" target="_blank" class="btn btn-default">
                                                    Gửi tới Youdao</a> <a href="http://zhanzhang.sogou.com/index.php/urlSubmit/index" target="_blank"
                                                        class="btn btn-default">Gửi tới sogou</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btnok_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>    
</asp:Content>