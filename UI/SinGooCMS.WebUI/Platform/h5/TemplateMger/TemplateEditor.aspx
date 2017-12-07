<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/IFrame.Master" AutoEventWireup="true"
    CodeBehind="TemplateEditor.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.TemplateMger.TemplateEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Include/Plugin/CodeMirror/codemirror.js" type="text/javascript"></script>
    <link href="/Include/Plugin/CodeMirror/codemirror.css" rel="stylesheet" type="text/css" />
    <script src="/Include/Plugin/CodeMirror/mode/xml.js" type="text/javascript"></script>
    <script src="/Include/Plugin/CodeMirror/mode/javascript.js" type="text/javascript"></script>
    <script src="/Include/Plugin/CodeMirror/mode/css.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table">
        <tr>
            <td>
                <div class="w200 fl" style="width: 360px;">
                    <asp:TextBox ID="txtFolderPath" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="w200 fl ml-20" style="width: 100px;">
                    <asp:TextBox ID="txtFileName" runat="server" placeholder="Hãy nhập tên tập tin" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="w200 fl ml-20" style="width: 100px;">
                    <asp:DropDownList ID="ddlFileType" runat="server" CssClass="form-control">
                        <asp:ListItem Text=".html" Value=".html"></asp:ListItem>
                        <asp:ListItem Text=".htm" Value=".htm"></asp:ListItem>
                        <asp:ListItem Text=".shtml" Value=".shtml"></asp:ListItem>
                        <asp:ListItem Text=".xml" Value=".xml"></asp:ListItem>
                        <asp:ListItem Text=".js" Value=".js"></asp:ListItem>
                        <asp:ListItem Text=".css" Value=".css"></asp:ListItem>
                        <asp:ListItem Text=".txt" Value=".txt"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="fr">
                    <div class="col-md-12">
                        <asp:Button ID="btnok" Text="Xác nhận" runat="server" OnClick="btnok_Click" CssClass="btn btn-danger" />
                        <asp:Button ID="btnapply" Text="Apply" runat="server" OnClick="btnapply_Click" CssClass="btn btn-danger" />
                        <input id="btncancel" onclick="$.dialog.close();" type="button" value="Hủy bỏ (Esc)"
                            class="btn btn-default" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="FileContent" runat="server" TextMode="MultiLine" name="code" />
            </td>
        </tr>
    </table>
    <script>
        var editor = CodeMirror.fromTextArea(document.getElementById("<%=FileContent.ClientID %>"), {
            mode: "application/xml",
            styleActiveLine: true,
            lineNumbers: true,
            lineWrapping: true
        });
        editor.setSize('auto','500');
    </script>
</asp:Content>
