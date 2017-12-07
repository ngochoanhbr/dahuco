singoo.initRequest();

var ids = "";
var names = "";
function selectOk() {
    $.each($('#rowItems').find("input"), function (i, item) {
        if ($(item).prop("checked")) { //Đã được chọn
            if ($(this).attr("type") == "single") {
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
        showtip("Không Lựa chọn bất kỳ mục nào");
    }
}

function callback(strIds, strNames) {
    for (var i = 0; i < singoo.request["elementid"].split(',').length; i++) {
        var t = singoo.request["backtype"].split(',')[i]; //Kiểu gọi lại
        var e = singoo.request["elementid"].split(',')[i]; //Mẫu chuyển nhượng
        var a = singoo.request["attr"].split(',')[i]; //Thuộc tính Assignment
        if (a == "value")
            $($.dialog.open.origin.document.getElementById(e)).val(t == "ids" ? strIds : strNames);
        else if(a=="dowork")
            $.dialog.open.origin.dowork(t == "ids" ? strIds : strNames);
        else
            $($.dialog.open.origin.document.getElementById(e)).attr(a, t == "ids" ? strIds : strNames);
    }
}