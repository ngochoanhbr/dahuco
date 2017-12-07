<%@ Page Title="" Language="C#" MasterPageFile="~/Platform/h5/MasterPage/IFrame.Master"
    AutoEventWireup="true" CodeBehind="ProvinceForSelect.aspx.cs" Inherits="SinGooCMS.WebUI.Platform.h5.Selector.ProvinceForSelect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="myTabContent" class="tab-content">
        <div class="fix white-bg">
            <div class="form-group mb-20 fix" id="rowItems">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class="w100 fl ml-20 line-30">
                            <input <%=SelectType=="single"?"type='radio'":"type='checkbox'" %> <%#Original_Data.Split(',').Contains(Eval("AutoID").ToString())?"checked='checked'":"" %>
                                name='inputcheck' title="Lựa chọn 器" value='<%#Eval("ZoneName")%>' id='<%#Eval("AutoID") %>'
                                class="checkbox_radio" />
                            <%#Eval("ZoneName")%>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="fix text-right mr-20">
                <input type="checkbox" id="checkall" class="checkbox_radio" />
                <input type="button" id="btnselect" value="Xác nhận " onclick="selectOk()" class="btn btn-danger" />
            </div>
        </div>
    </div>
    <script>
        //传递的参数 Lựa chọn 的Kiểutype: single mutil 回调的Tagelementid 回调的属性attr: val src 回调的Kiểubacktype names ids
        singoo.initRequest();

        var type = "<%=SelectType %>";
        var ids = "";
        var names = "";
        function selectOk() {
            $.each($('#rowItems').find("input"), function (i, item) {
                if ($(item).prop("checked")) { //被选中
                    if (type == "single") {
                        ids = $(item).attr("id");
                        names = $(item).attr("value");
                    } else {
                        ids += $(item).attr("id") + ",";
                        names += $(item).attr("value") + ",";
                    }
                }
            });
            if (ids != "" && names != "") {
                callback((ids.lastIndexOf(',') != -1 ? ids.cutRight() : ids), (names.lastIndexOf(',') != -1 ? names.cutRight() : names));
                $.dialog.close();
            } else {
                showtip("Chưa chọn mục nào");
            }
        }
        //回调,给调用的Tag赋值
        function callback(strIds, strNames) {
            for (var i = 0; i < singoo.request["elementid"].split(',').length; i++) {
                switch (singoo.request["backtype"].split(',')[i]) {
                    case "ids":
                        $($.dialog.open.origin.document.getElementById(singoo.request["elementid"].split(',')[i])).attr(singoo.request["attr"].split(',')[i], strIds);
                        break;
                    case "names":
                        $($.dialog.open.origin.document.getElementById(singoo.request["elementid"].split(',')[i])).attr(singoo.request["attr"].split(',')[i], strNames);
                        break;
                }
            }
        }
    </script>
</asp:Content>
