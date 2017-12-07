<%@ Control Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.FileType" %>
<asp:TextBox ID="txtFile"  runat="server" CssClass="form-control fl"></asp:TextBox>
<div class="fl ml-20"><input type="button" class="btn btn-default" onclick="h5.openUploadTool('single','<%=txtFile.ClientID %>','value');" value="上传/浏览" /></div>