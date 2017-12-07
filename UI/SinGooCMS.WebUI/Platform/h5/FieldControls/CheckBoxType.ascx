<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckBoxType.ascx.cs" Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.CheckBoxType" %>
<asp:CheckBoxList ID="cblField" runat="server" RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Flow" class="checkbox_radio">
</asp:CheckBoxList>
<!--Ajax数据--> 
<%if (base.Settings.DataFrom.Equals("AjaxData")){%>
    <script type="text/javascript">
        $.get("<%=this.Settings.DataSource %>", {}, function (data) {
            var json = eval('(' + data + ')');
            jQuery.each(json, function (i, item) {
                var newid = Math.random();
                if (item.KeyValue == "<%=this.Value %>")
                    jQuery("#<%=cblField.ClientID%>").append("<input id='" + newid + "' type='checkbox' name='<%=cblField.ClientID%>chk' checked='checked' value='" + item.KeyName + "' /><label for=''>" + item.KeyValue + "</label>");
                else
                    jQuery("#<%=cblField.ClientID%>").append("<input id='" + newid + "' type='checkbox' name='<%=cblField.ClientID%>chk' value='" + item.KeyName + "' /><label for=''>" + item.KeyValue + "</label>");
            });
        });
    </script> 
<%} %>