<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ModifyGuiGePic.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.GoodsMger.ModifyGuiGePic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .picitem
        {
            color: black;
            height: 28px;
            line-height: 28px;
            text-align: center;
            background: #efefef;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content areacolumn">
        <div class="fix white-bg" style="height:380px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="profile-body mb-20 fix">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div class="w100 ml-20 fl" viewer="true">
                                    <div class="images_model">
                                        <div class="box-hd">
                                            <i class="iconfont" onclick="h5.openUploadTool('single','imgpath_<%#Container.ItemIndex %>,img_<%#Container.ItemIndex %>,img_<%#Container.ItemIndex %>','value,src,data-original');">
                                                &#xe682;</i>
                                        </div>
                                        <div class="box-bd">
                                            <div class="figure-img">
                                                <img id="img_<%#Container.ItemIndex %>" src='<%#Eval("ImgPath").ToString()==""?"/platform/h5/images/empty.png":Eval("ImgPath").ToString() %>' style="border-width:0px;max-width:100px;max-height:100px;" />
                                            </div>                                            
                                        </div>
                                    </div>
                                    <div class="picitem">
                                        <%#Eval("GuiGeItem")%>
                                    </div>
                                    <input type="hidden" name="guigeitem" value='<%#Eval("GuiGeItem")%>' />
                                    <input type="hidden" name="imgpath" id="imgpath_<%#Container.ItemIndex %>" value='<%#Eval("ImgPath") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!--表格Nội dung end-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="profile-body" style="clear: both;">
            <div class="datafrom text-right">
                <asp:Button ID="btnok" Text="OK" runat="server" class="btn btn-danger" OnClick="btnok_Click" />
                <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                    class="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
