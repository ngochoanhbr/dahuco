<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/MasterDefault.Master"
    AutoEventWireup="true" CodeBehind="MessageSet.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.ConfMger.MessageSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableed > tbody > tr > td
        {
            line-height: 16px;
        }
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
            <div class="profile-body mb-20">
                <div class="datafrom">
                    <h2 class="title">
                        Cài đặt tin nhắn cảnh báo người sử dụng</h2>
                    <div class="profile-body mb-20">
                        <table class="table tableed table-hover" id="urlrewrite">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th>
                                        Kiểu tin nhắn
                                    </th>
                                    <th style="width: 120px;">
                                        Chữ cái
                                    </th>
                                    <th style="width: 120px;">
                                        Email
                                    </th>
                                    <th style="width: 120px;">
                                        Điện thoại di động SMS
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden">
                                                <input type="checkbox" class="checkbox_radio" class="checkbox_radio" />
                                            </td>
                                            <td>
                                                <%#Eval("SetType") %>
                                                <input type="hidden" name="_setkey" value='<%#Eval("SetKey") %>' />
                                            </td>
                                            <td>
                                                <input type="checkbox" class="checkbox_radio" name="_chk_<%#Eval("SetKey") %>_msg" <%#Eval("IsSendMsg").ToString()=="True"?"checked='checked'":"" %> />
                                                <a href="javascript:;" onclick="$.dialog.open('MsgTemplate.aspx?Module=<%=base.CurrentModuleCode %>&setkey=<%#Eval("SetKey") %>&type=Msg&action=Modify',{title:'Bản mẫuChi tiết',width:750,height:350},false);">
                                                    Chi tiết</a>
                                            </td>
                                            <td>
                                                <input type="checkbox" class="checkbox_radio" name="_chk_<%#Eval("SetKey") %>_mail" <%#Eval("IsSendMail").ToString()=="True"?"checked='checked'":"" %> />
                                                <a href="javascript:;" onclick="$.dialog.open('MsgTemplate.aspx?Module=<%=base.CurrentModuleCode %>&setkey=<%#Eval("SetKey") %>&type=Mail&action=Modify',{title:'Bản mẫuChi tiết',width:750,height:450},false);">
                                                    Chi tiết</a>
                                            </td>
                                            <td>
                                                <input type="checkbox" class="checkbox_radio" name="_chk_<%#Eval("SetKey") %>_sms" <%#Eval("IsSendSMS").ToString()=="True"?"checked='checked'":"" %> />
                                                <a href="javascript:;" onclick="$.dialog.open('MsgTemplate.aspx?Module=<%=base.CurrentModuleCode %>&setkey=<%#Eval("SetKey") %>&type=SMS&action=Modify',{title:'Bản mẫuChi tiết',width:750,height:330},false);">
                                                    Chi tiết</a>
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
                </div>
            </div>
            <div class="profile-body mb-20 fix">
                <div class="datafrom">
                    <h2 class="title">
                        Cài đặt thông báo tin quản trị</h2>
                    <div class="profile-body mb-20">
                        <div class="datafrom">
                            <div class="form-group fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    hộp thư quản trị viên</label>
                                <div class="col-md-4">
                                    <asp:TextBox placeholder="Vui lòng nhập hộp thư Administrator" ID="managermail" runat="server" class="form-control" MaxLength="255"></asp:TextBox>
                                </div>
                                <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="多个邮箱以逗号','分隔">
                                    &#xe613;</i>
                            </div>
                            <div class="form-group mt-20 fix">
                                <label for="firstname" class="col-md-3 control-label">
                                    Số điện thoại quản trị viên</label>
                                <div class="col-md-4">
                                    <asp:TextBox placeholder="Nhập số điện thoại quản trị viên" ID="managermobile" runat="server" class="form-control" MaxLength="255"></asp:TextBox>
                                </div>
                                <i class="iconfont ml-20" data-toggle="tooltip" data-placement="right" title="Nhập nhiều số điện thoại cách nhau bởi dấu ','">
                                    &#xe613;</i>
                            </div>
                        </div>
                        <table class="table tableed table-hover" id="Table1">
                            <thead>
                                <tr class="active">
                                    <th class="hidden">
                                    </th>
                                    <th>
                                        Kiểu tin nhắn
                                    </th>
                                    <th style="width: 120px;">
                                        Chữ cái
                                    </th>
                                    <th style="width: 120px;">
                                        Email
                                    </th>
                                    <th style="width: 120px;">
                                        Điện thoại di động SMS
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="Repeater2" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden">
                                                <input type="checkbox" class="checkbox_radio" class="checkbox_radio" />
                                            </td>
                                            <td>
                                                <%#Eval("SetType") %>
                                                <input type="hidden" name="_setkey" value='<%#Eval("SetKey") %>' />
                                            </td>
                                            <td>
                                                <input type="checkbox" class="checkbox_radio" name="_chk_<%#Eval("SetKey") %>_msg" <%#Eval("IsSendMsg").ToString()=="True"?"checked='checked'":"" %> />
                                                <a href="javascript:;" onclick="$.dialog.open('MsgTemplate.aspx?Module=<%=base.CurrentModuleCode %>&setkey=<%#Eval("SetKey") %>&type=Msg&action=Modify',{title:'Bản mẫuChi tiết',width:750,height:350},false);">
                                                    Chi tiết</a>
                                            </td>
                                            <td>
                                                <input type="checkbox" class="checkbox_radio" name="_chk_<%#Eval("SetKey") %>_mail" <%#Eval("IsSendMail").ToString()=="True"?"checked='checked'":"" %> />
                                                <a href="javascript:;" onclick="$.dialog.open('MsgTemplate.aspx?Module=<%=base.CurrentModuleCode %>&setkey=<%#Eval("SetKey") %>&type=Mail&action=Modify',{title:'Bản mẫuChi tiết',width:750,height:450},false);">
                                                    Chi tiết</a>
                                            </td>
                                            <td>
                                                <input type="checkbox" class="checkbox_radio" name="_chk_<%#Eval("SetKey") %>_sms" <%#Eval("IsSendSMS").ToString()=="True"?"checked='checked'":"" %> />
                                                <a href="javascript:;" onclick="$.dialog.open('MsgTemplate.aspx?Module=<%=base.CurrentModuleCode %>&setkey=<%#Eval("SetKey") %>&type=SMS&action=Modify',{title:'Bản mẫuChi tiết',width:750,height:330},false);">
                                                    Chi tiết</a>
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
                </div>
            </div>
            <div class="profile-body mb-20">
                <div class="datafrom text-right">
                    <asp:Button ID="btnok" Text="Lưu" runat="server" OnClick="btn_SaveRule_Click" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
