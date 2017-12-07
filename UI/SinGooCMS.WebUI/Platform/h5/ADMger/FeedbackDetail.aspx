<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="FeedbackDetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ADMger.FeedbackDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle">Nội dung phản hồi:</span>
                    <div class="w600 fl">
                        <textarea class="form-control" rows="3" cols="80" disabled="disabled"><%=feedback.Content%></textarea>                        
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Đến từ:</span>
                    <div class="w600 fl">
                        Người sử dụng：<%=string.IsNullOrEmpty(feedback.UserName)?"Vô danh":feedback.UserName%>，邮箱：<%=string.IsNullOrEmpty(feedback.Email)?"无":feedback.Email%>，Điện thoại di động：<%=string.IsNullOrEmpty(feedback.Mobile)?"Không":feedback.Mobile%>，Điện thoại：<%=string.IsNullOrEmpty(feedback.Telephone)?"Không":feedback.Telephone %>，IP：<%=feedback.IPaddress %>，Thời gian：<%=feedback.AutoTimeStamp %>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle"><em>*</em>Trả lời:</span>
                    <div class="w600 fl">
                        <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" Rows="3" Columns="80"
                            class="form-control" required="required" lenlimit="2000"></asp:TextBox>
                        <asp:CheckBox ID="chkReply2Mail" runat="server" class="checkbox_radio"/>Đồng thời trả lời vào mail
                        <jweb:H5TextBox Mode="Email" Enabled="false" ID="txtMail" runat="server" Width="200px" CssClass="form-control"></jweb:H5TextBox>
                    </div>
                </li>
            </ul>
        </div>
        <div class="profile-body">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="Xác nhận" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("#<%=chkReply2Mail.ClientID %>").on('ifChecked', function () {
            $("#<%=txtMail.ClientID %>").removeAttr("disabled");
        });
        $("#<%=chkReply2Mail.ClientID %>").on('ifUnchecked', function () {
            $("#<%=txtMail.ClientID %>").attr("disabled", "disabled");
        });
    </script>
</asp:Content>
