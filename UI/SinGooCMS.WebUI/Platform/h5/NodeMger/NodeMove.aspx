<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="NodeMove.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.NodeMger.NodeMove" %>

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
                <li class="active"><a href="javascript:;" data-toggle="tab">Di chuyển menu</a></li>
            </ul>
            <div class="profile-body mb-20 fix">
                <div class="datafrom">
                <h2 class="title">Di chuyển menu</h2>


                <div class="mobile-column-view">
                    <div class="mobile-column-left">
                        <div class="title">Menu hiện tại</div>
                        <asp:ListBox ID="lbSourceNode" CssClass="selectoption" Rows="20" Width="100%" Height="380"
                                        runat="server"></asp:ListBox>
                    </div>
                    <div class="mobile-column-center text-center">
                        <i class="iconfont font-30">&#xe688;</i>
                    </div>
                    <div class="mobile-column-right">
                        <div class="title">Menu đích</div>
                        <asp:ListBox ID="lbTargetNode" CssClass="selectoption" Rows="20" Width="100%" Height="380"
                                        runat="server"></asp:ListBox>

                    </div>

                </div>


<!--

                    <table class="table">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Menu hiện tại</legend>
                                   
                                </fieldset>
                            </td>
                            <td>
                                <fieldset>
                                    <legend>Menu đích</legend>
                                  
                                </fieldset>
                            </td>
                        </tr>
                    </table>-->
                </div>
            </div>




            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnMove" CssClass="btn btn-danger" Text="Xác nhận di chuyển" runat="server" OnClick="btnMove_Click" />
                    <input id="btncancel" onclick="location='Index.aspx?CatalogID=<%=base.CurrentCatalogID %>&Module=<%=base.CurrentModuleCode %>&action=View'"
                        type="button" value="Quay lại" class="btn btn-default" />
                </div>
            </div>
        </div>
    </div>

    <script src="js/jquery.nicescroll.js"></script>
    <script type="text/javascript">
        $(".bar").niceScroll({
            cursorcolor: "#9D9EA5",
            cursoropacitymax: 2,
            touchbehavior: false,
            cursorwidth: "2px",
            cursorborder: "0",
            cursorborderradius: "5px"
        }); 
    </script>
</asp:Content>
