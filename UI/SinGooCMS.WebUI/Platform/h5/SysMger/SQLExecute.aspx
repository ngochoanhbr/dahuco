<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="SQLExecute.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.SysMger.SQLExecute" %>

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
            <div class="profile-body mb-20 hidden">
                <div class="form-group fix hide">
                    <label for="firstname" class="sr-only">
                        singoocms</label>
                    <div class="col-md-3">
                    </div>
                    <label for="firstname" class="sr-only">
                        singoocms</label>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="batchHandleArea fix">
                    <div class="checkall">
                        <input type="checkbox" id="checkall" class="checkbox_radio" />
                    </div>
                    <div class="btn-group">
                    </div>
                    <div class="btn-group">
                        <button type="button" class="btn btn-warning">
                            Thêm</button>
                    </div>
                    <div class="fr">
                        <div class="col-md-12">
                            <asp:DropDownList class="form-control select" ID="drpPageSize" runat="server">
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
                <div class="datafrom">
                    <h2 class="title">
                        Thực hiện câu lệnh SQL</h2>
                </div>
                <div style="padding: 10px 0; color: Red; font-weight: bold;">
                    Lưu ý: Không thực hiện câu lệnh SQL Xử lý tính năng này khi không biết code！！！
                </div>
                <asp:TextBox ID="txtscript" runat="server" TextMode="MultiLine" Rows="15" Columns="100"
                    CssClass="form-control"></asp:TextBox><br />
                <asp:Button ID="btn_ExecuteSQL" runat="server" Text="Thực hiện các truy vấn SQL" OnClick="btn_ExecuteSQL_Click"
                    class="btn btn-danger" />
                <asp:Literal ID="lblmsg" runat="server"></asp:Literal>
            </div>
            <!--表格Nội dung end-->
        </div>
        <!--end 右侧内联框架-->
    </div>
    <script>
        $("#<%=btn_ExecuteSQL.ClientID %>").click(function () {
            var strSQL = $("#<%=txtscript.ClientID %>").val();
            if (strSQL == "") {
                showtip("Nhập câu lệnh SQL");
                return false;
            }
        });
    </script>
</asp:Content>
