﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.TemplateFileType" %>
<asp:TextBox ID="txtTemplate" runat="server" CssClass="form-control fl" MaxLength="255"></asp:TextBox>
<div class="fl ml-20"><input class="btn btn-default" id="btn_selecttemplate" type="button" onclick="$.dialog.open('/Platform/h5/TemplateMger/TemplateFileListForSelect.aspx?Module=3DB75D2B46FC4473&action=View&elementid=<%=txtTemplate.ClientID %>',{title:'Lựa chọn bản mẫu',width:680,height:400},false);" value="Lựa chọn bản mẫu" /></div>