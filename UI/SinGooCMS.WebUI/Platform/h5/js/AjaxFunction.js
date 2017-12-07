$(function () {
    $('input').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '10%'
    });
    $("[data-toggle='tooltip']").tooltip();
})
if ($('#checkall').length > 0) {
    $('#checkall').on('ifChecked', function (event) {
        $("#rowItems").find("input[type='checkbox']").iCheck('check');
    });
    $('#checkall').on('ifUnchecked', function (event) {
        $("#rowItems").find("input[type='checkbox']").iCheck('uncheck');
    });
}
//Chọn tệp tải lên
$('.Gallery_Popup_cr-imgs li').click(function () {
    if ($(this).attr("seltype") == "single" && !$(this).hasClass("selected")) {
        $('.Gallery_Popup_cr-imgs li').removeClass("selected");
    }
    $(this).toggleClass('selected');
});
//Xem
$("img[viewer='true'],div[viewer='true']").viewer({ url: "data-original" });
//DatetimeLựa chọn 
$("input[type='text'][date-selector='true']").datetimepicker({ minView: 2, startView: 2, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });
//Lựa chọn 
$("input[type='text'][date-selectmonth='true']").datetimepicker({ minView: 3, startView: 3, weekStart: 1, todayBtn: true, todayHighlight: true, forceParse: true, showMeridian: true, autoclose: true, language: 'zh-CN', pickerPosition: "top-left" });


//Ẩn tất cả danh mục
$("#closeAll").click(function () {
    $("#rowItems tr").each(function (index, item) {
        if (index != 0 && $(this).html().indexOf("depth=\"1\"") < 0) { $(item).hide(); }
    })
    $("#rowItems tr td ico[depth=\"1\"][canExpand='1']").attr("class", "glyphicon glyphicon-plus-sign");
});
//Hiển thị tất cả danh mục
$("#openAll").click(function () {
    $("#rowItems tr").each(function (index, item) {
        if (index != 0 && $(this).html().indexOf("depth=\"1\"") < 0) { $(item).show(); }
    })
    $("#rowItems tr td span ico[canExpand='1']").attr("class", "glyphicon glyphicon-minus-sign");
});
$("#rowItems ico[canexpand='1']").click(function () {
    var currentTrNode = $(this).parents("tr").next();
    var isexpand = $(this).attr("class") == "glyphicon glyphicon-plus-sign"; 
    while (true) {
        if (typeof (currentTrNode.html()) != "string" || parseInt($(currentTrNode).find("ico").attr("depth")) <= parseInt($(this).attr("depth"))) { break; }
        if (isexpand) { currentTrNode.show(); } else { currentTrNode.hide() };
        currentTrNode = currentTrNode.next();
    }
    $(this).attr("class", (isexpand ? "glyphicon glyphicon-minus-sign" : "glyphicon glyphicon-plus-sign"));
});