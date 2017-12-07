<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AreaType.ascx.cs" Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.AreaType" %>
<script src="/Include/Plugin/areaOPT/jquery.areaopt.js" type="text/javascript"></script>
<link href="/Include/Plugin/areaOPT/theme/jquery.areaopt.css" rel="stylesheet" type="text/css" />
<asp:TextBox ID="txtField" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
<script type="text/javascript">
    jQuery(function () {
        jQuery.areaopt.bind('#<%=txtField.ClientID %>');
    });
</script>
