<%@ Control Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.SingleTextType" %>
<asp:TextBox ID="txtField" runat="server" CssClass="form-control"></asp:TextBox>
<%if (base.FieldName == "TagKey"){ %>
<script type="text/javascript">jQuery("#<%=txtField.ClientID %>").click(function () {
    $.dialog.open('../Selector/TagForSelect.aspx?Module=1FA35D0106A6D027&type=mutil&elementid=<%=txtField.ClientID %>&attr=value&backtype=ids', { title: 'Lựa chọn Tag', width: 680, height: 430 }, false);
    });
</script>
<%}%>