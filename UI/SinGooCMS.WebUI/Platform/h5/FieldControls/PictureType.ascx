<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PictureType.ascx.cs"
    Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.PictureType" %>
<div class="images_model">
    <div class="box-hd">
        <i class="iconfont" onclick="h5.openUploadTool('single','<%=txtImgUrl.ClientID %>,<%=Image1.ClientID %>,<%=Image1.ClientID %>','value,src,data-original');">
            &#xe682;</i>
    </div>
    <div class="box-bd">
        <div class="figure-img">
            <jweb:FullImage ID="Image1" runat="server" viewer='true'/>
        </div>
        <span class="hidden">
            <asp:TextBox ID="txtImgUrl" runat="server" class="form-control"></asp:TextBox></span>
    </div>
</div>
