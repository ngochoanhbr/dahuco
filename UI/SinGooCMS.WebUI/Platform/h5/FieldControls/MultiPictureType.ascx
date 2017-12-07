<%@ Control Language="C#" AutoEventWireup="true" Inherits="SinGooCMS.WebUI.h5.Controls.FieldControls.MultiPictureType" %>
<div style="text-align: right"><span id="jia<%=ClientID %>" style="cursor:pointer">Thêm dòng[+]</span></div>
<table class="table" id="imgarea<%=ClientID %>">
    <asp:Repeater ID="rpt_img" runat="server">
        <ItemTemplate>
            <tr>
                <td class="left">
                    Mô tả file：<input type="text" name="imgdesc-<%=ClientID %>" value='<%#Eval("Remark") %>' class="input-txt" style="width:100px" />
                    Chọn hình ảnh：<input type="text" name="imgselect-<%=ClientID %>" id='img<%#Eval("AutoID") %>'  value='<%#Eval("FilePath") %>' class="input-txt" style="width: 200px" />
                    <input type="button" value="Xem" class="btn btn-default" onclick="showimg($('#img<%#Eval("AutoID") %>').val());" />&nbsp;
                    <input type="button" value="Chọn hình ảnh" class="btn btn-default" onclick="h5.openUploadTool('single','img<%#Eval("AutoID") %>','value');" />
                    <span style="cursor:pointer" onclick="$(this).parent().parent().remove();" class="jian" title="Di chuyển">[-]</span>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<script type="text/javascript">
    $("#jia<%=ClientID %>").click(function () {
        var trid = singoo.getRnd();
        $("#imgarea<%=ClientID %>").append('<tr><td class="left">Mô tả file：<input type="text" name="imgdesc-<%=ClientID %>" class="input-txt" style="width:100px"/> Chọn hình ảnh：<input type="text" id="img' + trid + '" name="imgselect-<%=ClientID %>" class="input-txt" style="width:200px" /> <input type="button" value="Xem" class="btn btn-default" onclick="showimg($(\'#img' + trid + '\').val());"/>&nbsp;<input type="button" value="Chọn hình ảnh" class="btn btn-default" onclick="h5.openUploadTool(\'single\',\'img' + trid + '\',\'value\');" /> <span onclick="$(this).parent().parent().remove();" class="jian" title="Di chuyển" style="cursor:pointer">[-]</span> </td></tr>');
    });   
</script>
