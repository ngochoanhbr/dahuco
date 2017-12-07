<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="EvaluationDetail.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.UserMger.EvaluationDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg">
            <ul>
                <li class="mb-20"><span class="formitemtitle">Đánh giá sản phẩm:</span>
                    <div class="w600 fl">
                        <%=eva.ProName %>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Ngôi sao:</span>
                    <div class="w400 fl">
                        <script type="text/javascript">
                            singoo.writeStart(<%=eva.Start %>)
                        </script>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Nội dung đánh giá:</span>
                    <div class="w600 fl">
                        <textarea class="form-control" rows="3" cols="80" disabled="disabled"><%=eva.Content %></textarea>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Đến từ:</span>
                    <div class="w600 fl">
                        Người sử dụng：<%=eva.UserName %>
                        Thời gian：<%=eva.AutoTimeStamp %>
                    </div>
                </li>
                <li class="mb-20"><span class="formitemtitle">Trả lời:</span>
                    <div class="w600 fl">
                        <asp:TextBox ID="txtReply" runat="server" TextMode="MultiLine" Rows="3" Columns="80"
                            CssClass="form-control" lenlimit="1000" placeholder="Người quản trị 无 cần phải trả lời"></asp:TextBox>
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
</asp:Content>
