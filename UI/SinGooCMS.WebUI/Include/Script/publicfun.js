document.write("<script src='/Include/Script/md5.js' type='text/javascript'></script>");
var singoo = new Object();
singoo = {
    submitKey: function (evt, id_btn) { //Nút Kích hoạt nhấp vào sự kiện gọi singoo.submitKey(event,'btnid');
        var evt = (evt) ? evt : ((window.event) ? window.event : "");
        var key = evt.keyCode ? evt.keyCode : evt.which;
        if (key == 13) { document.getElementById(id_btn).click(); return false; }
    },
    request: {},
    initRequest: function () { //Nhận các tham số truy vấn
        var s = location.search, m, reg = /([a-z_\d]+)=([^&]+)/gi;
        s = s == '' ? '' : s.substring(1);
        while (m = reg.exec(s)) { this.request[m[1].toLowerCase()] = m[2]; }
    },
    /* Gửi một cuộc gọi tin nhắn singoo.feedback.init("lee", "aa", "bb")Hoặc chỉ định một giá trị cho mỗi tham số và cuối cùng gửi nó singoo.feedback.postData();
    jQuery("form").submit(function () {
        singoo.feedback.init(jQuery("#contact_name").val(), jQuery("#contact_message").val(), jQuery("#contact_code").val());
        singoo.feedback.postData(function () { jQuery("#checkimg").attr("src", "/Include/CheckCodeImg.aspx?style=2&id" + Math.random()) });
        return false;
    });
    */
    feedback: {
        uname: "", title: "", content: "", email: "", mobile: "", phone: "", verifyCode: "", //Tiêu đề, email, số điện thoại di động, số điện thoại
        init: function (uname, content, vcode) { this.uname = uname; this.content = content; this.verifyCode = vcode; },
        postData: function (callback) {
            if (this.uname == "")
                showtip("Vui lòng nhập tên của bạn!");
            else if (this.content == "")
                showtip("Nội dung tin nhắn không được để trống!");
            else {
                if (this.title == "") { this.title = "Tin nhắn của " + this.uname };
                $.post("/include/ajax/ajaxmethod", { t: "feedback", _uname: this.uname, _title: this.title, _content: this.content, _email: this.email, _mobile: this.mobile, _phone: this.phone, _yzm: this.verifyCode }, function (data) { showtip(data.msg); if (callback != null && typeof callback === "function") callback(); }, "JSON");
            }
        }
    },
    /*    
    Ví dụ post Call：singoo.httpPost('/shop/addtocart', { _protype: 'Đơn đặt hàng thông thường', _pid:'${item.AutoID}', _buynumber: 1, _price: '$price1' });
    */
    httpPost: function (url, params) {
        var temp = document.createElement("form");
        temp.action = URL;
        temp.method = "post";
        temp.style.display = "none";
        for (var x in params) {
            var opt = document.createElement("textarea");
            opt.name = x;
            opt.value = params[x];
            temp.appendChild(opt);
        }
        document.body.appendChild(temp);
        temp.submit();
        return temp;
    },
    chkAll: function (ctrl, name_chk) {
        $.each($("input[name='" + name_chk + "']"), function (index, item) { $(ctrl).attr("checked") ? $(item).attr("checked", "checked") : $(item).removeAttr("checked") });
    },
    chkByParent: function (ctrl, id_parent) {
        var parentElement = document.getElementById(id_parent); 
        var eles = parentElement.getElementsByTagName("input");
        if (eles.length > 0) {
            for (var i = 0; i < eles.length; i++) {
                if (eles[i].type == "checkbox" && !eles[i].disabled) eles[i].checked = ctrl.checked;
            }
        }
    },
    getCheckCount: function (areaTagID) { 
        var counter = 0;
        $.each($("#" + areaTagID).find("input[type='checkbox']"), function (i, item) {
            if (item.checked)
                counter++;
        });

        return counter;
    },
    getRnd: function () { //Nhận số ngẫu nhiên
        var now = new Date();
        return now.getSeconds() + "" + now.getMilliseconds() + "" + Math.ceil(Math.random() * 10);
    },
    getIds: function (name_chk) { 
        var ids = "";
        $.each($("input[name='" + name_chk + "']"), function (index, item) { if (item.checked) ids += jQuery(item).val() + ","; });
        return ids.endWith(",") ? ids.cutRight(ids) : ids;
    },
    getStart: function (num) {
        var str = "";
        for (var i = 0; i < num; i++) {
            str += "<img src='/Include/Plugin/jquery.raty/img/star-on.png' alt='' />";
        }
        return str;
    },
    writeStart: function (num) {
        for (var i = 0; i < num; i++) {
            document.write("<img src='/Include/Plugin/jquery.raty/img/star-on.png' alt='' />");
        }
    },
    getRandStr: function (len) { //Nhận chuỗi trường ngẫu nhiên
        len = len || 32;
        var temp = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';
        var maxPos = temp.length;
        var str = '';
        for (i = 0; i < len; i++) { str += temp.charAt(Math.floor(Math.random() * maxPos)); }
        return str;
    },
    pwdEncode: function (str) { //Mã hóa md5
        var md5 = hex_md5(str).toUpperCase().replace("-", "").substr(6, 13);
        return hex_md5(md5).toUpperCase().replace("-", "").substr(6, 13);
    },
    initArea: function () {
        var objProvince = jQuery("#" + arguments[0]);
        var objCity = jQuery("#" + arguments[1]);
        var objCounty = jQuery("#" + arguments[2]);

        objProvince.html(this.getAreaOption(0)); //Điền vào tỉnh
        objCity.html(this.getAreaOption(1));  //Thành phố
        objCounty.html(this.getAreaOption(35));  //Quốc gia

        objProvince.change(function () {
            var pid = objProvince.find("option:selected").attr("rel"); 
            objCity.html(singoo.getAreaOption(pid));

            var cid = objCity.find("option:eq(0)").attr("rel");
            objCounty.html(singoo.getAreaOption(cid)); //Hiển thị danh sách các huyện và quận của thành phố đầu tiên
        });

        objCity.change(function () {
            var cid = objCity.find("option:selected").attr("rel");
            objCounty.html(singoo.getAreaOption(cid));
        });
    },
    getAreaOption: function (parentID) {
        var str = "";
        jQuery.ajaxSettings.async = false;
        jQuery.getJSON("/Config/zone.json", function (data) {
            jQuery.each(data, function (i, item) {
                if (item.ParentID == parentID)
                    str += "<option rel=" + item.AutoID + " value=\"" + item.ZoneName + "\">" + item.ZoneName + "</option>";
            });
        });
        jQuery.ajaxSettings.async = true;
        return str;
    }
}

String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }
String.prototype.ltrim = function () { return this.replace(/(^\s*)/g, ""); }
String.prototype.rtrim = function () { return this.replace(/(\s*$)/g, ""); }
String.prototype.alltrim = function () { return this.replace(/\s+/g, ""); }
String.prototype.cut = function (len) { return this.length > len ? this.substr(0, len) : this; }
String.prototype.cutLeft = function (str) { return this.length > 1 ? this.substr(1, this.length) : this; }
String.prototype.cutRight = function (str) { return this.length > 1 ? this.substr(0, this.length - 1) : this; }
String.prototype.replaceAll = function (s1, s2) { return this.replace(new RegExp(s1, "gm"), s2); }
String.prototype.startWith = function (str) { return str != null && str != "" && this.substr(0, str.length) == str; }
String.prototype.endWith = function (str) { return str != null && str != "" && this.substring(this.length - str.length) == str; }
String.prototype.IsPicture = function () {
    var strFilter = ".jpeg|.gif|.jpg|.png|.bmp|"
    if (this.indexOf(".") > -1) {
        var p = this.lastIndexOf(".");
        var strPostfix = this.substring(p, this.length) + '|';
        strPostfix = strPostfix.toLowerCase();
        if (strFilter.indexOf(strPostfix) > -1) {
            return true;
        }
    }

    return false;
}
Date.prototype.format = function (format) { //Định dạng ngày tháng
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(), //day
        "h+": this.getHours(), //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
function showmsg(msg) {
    $.dialog({ title: 'Thông báo', content: msg, width: 260, height: 60, lock: false })
}
function showalert(msg) {
    $.dialog({ title: 'Thông báo', content: msg, width: 260, height: 60, icon: 'warning', lock: false })
}
function showsuccess(msg) {
    $.dialog({ title: 'Thông báo', content: msg, width: 260, height: 60, icon: 'succeed', lock: false })
}
function showtip(msg) {
    $.dialog.tips(msg, 3);
}
function showimg(imgsrc) {
    if (imgsrc == null || imgsrc == "")
        showalert("Không tìm thấy tệp");
    else if (!imgsrc.IsPicture())
        window.open(imgsrc); //showalert("Định dạng không phải là hình ảnh");
    else
        $.dialog({ padding: 0, title: 'Xem hình', content: '<a href="' + imgsrc + '" target="_blank"><img src="' + imgsrc + '" style="max-width:400px"/><a/>', lock: false });
}
$(function () {
    singoo.initRequest();
    if ($("table").hasClass("stripe_tb")) {
        $(".stripe_tb tr").mouseover(function () {
            //如果鼠标移到class为stripe_tb的表格的tr上时，执行函数
            $(this).addClass("over");
        }).mouseout(function () {
            //给这行添加class值为over，并且当鼠标一出该行时执行函数
            $(this).removeClass("over");
        }) //移除该行的class
        $(".stripe_tb tr:even").addClass("alt");
        $(".stripe_tb tr").click(function () { $(".stripe_tb tr").removeClass("click"); $(this).addClass("click"); });
        $(".stripe_tb tr").dblclick(function () { $(this).removeClass("click"); });
    }
});
function AddFavorite(sURL, sTitle) {
    try { window.external.addFavorite(sURL, sTitle); } catch (e) { try { window.sidebar.addPanel(sTitle, sURL, ""); } catch (e) { alert("加入收藏失败，请使用Ctrl+D进行添加"); } }
}
function SetHome(obj, sURL) {
    try { obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(sURL); }
    catch (e) {
        if (window.netscape) {
            try { netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); }
            catch (e) { alert("Điều này bị trình duyệt loại bỏ!"); }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch); prefs.setCharPref("browser.startup.homepage", sURL);
        }
    }
}
function copyToClipboard(txt) {
    if (window.clipboardData) { window.clipboardData.clearData(); window.clipboardData.setData("Text", txt); } else if (navigator.userAgent.indexOf("Opera") != -1) { window.location = txt; } else if (window.netscape) {
        try { netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); } catch (e) { alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'"); }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard); if (!clip)
            return; var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable); if (!trans)
            return; trans.addDataFlavor('text/unicode'); var str = new Object(); var len = new Object(); var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString); var copytext = txt; str.data = copytext; trans.setTransferData("text/unicode", str, copytext.length * 2); var clipid = Components.interfaces.nsIClipboard; if (!clip)
            return false; clip.setData(trans, null, clipid.kGlobalClipboard); alert("复制成功！")
    }
}
/*html5,bootstarp 功能*/
var h5 = new Object();
h5 = {
    ajaxLogin: function () {
        var element = arguments[0];
        if (arguments[1] == "") { this.tip("Vui lòng nhập tên tài khoản"); }
        else if (arguments[2] == "") { this.tip("Vui lòng nhập mật khẩu tài khoản của bạn"); }
        else {
            $(element).val("Đăng nhập...");
            var pwdEncode = arguments[2];
            pwdEncode = CryptoJS.SHA512(pwdEncode).toString();
            $.post("/platform/h5/Ajax/AjaxMethod.aspx", { type: "AdminLogin", _accountname: arguments[1], _accountpwd: pwdEncode, _checkcode: "", _skin: "h5", temp: Math.random() }, function (data) {
                if (data.url == "") { h5.tip(data.msg); $(element).val("Đăng nhập"); } else { location = data.url }
            }, "JSON");
        }
    },
    tipOk: function (msg) {
        this.tip("<img src='/Include/Images/ok.gif' alt='' /> " + msg);
    },
    tipWarn: function (msg) {
        this.tip("<img src='/Include/Images/warning.gif' alt='' /> " + msg);
    },
    tip: function (msg) {
        this.dialog("Thông báo", msg, 450, true);
    },
    dialog: function () { //title,content,width,autoClose
        var title = arguments[0], content = arguments[1], width = arguments.length > 2 ? arguments[2] : 600, autoClose = arguments.length > 3 ? arguments[3] : false; //默认参数
        var msgid = "dialog-" + singoo.getRandStr(5);
        var str = "<div class=\"modal fade\" id=\"" + msgid + "\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"myModalLabel\" aria-hidden=\"true\">"
                  + "  <div class=\"modal-dialog\" style='width:" + width + "px;'>"
                  + "      <div class=\"modal-content\">"
                  + "          <div class=\"modal-header\">"
                  + "              <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">"
                  + "                  &times;"
                  + "              </button>"
                  + "              <h4 class=\"modal-title\" id=\"myModalLabel\">" + title + " </h4>"
                  + "          </div>"
                  + "          <div class=\"modal-body\">" + content + " </div>"
                  + "      </div>"
                  + "  </div>"
                  + "</div>";
        $("body").append(str);
        var d = $('#' + msgid).modal({ show: true });
        if (autoClose) setTimeout(function () { d.modal('hide'); }, 3000); //3秒后自动关闭
    },
    about: function () {
        this.dialog("Giới thiệu về chúng tôi", "<p><strong>Database Internal v1.3</strong></p><br /><p> Copyright©2017 <a href=\"http://singoo.top\" target=\"_blank\">DAHUCO</a>&nbsp;&nbsp;<a href=\"http://www.ue.net.cn\" target=\"_blank\">ahihi</a>Cung cấp hỗ trợ kỹ thuật<br /><br /><img src=\"/Include/Images/weixin.jpg\" alt=\"扫一扫关注我们\" style=\"max-width:150px;\" /><img src=\"/Include/Images/alipay.jpg\" alt=\"扫一扫支付\" style=\"max-width:150px;\"  /></p><br/><p class=\"text-danger\"><strong>如果您有任何建议及合作方式，请联系<a href=\"mailto:admin@singoocms.com\">admin@singoocms.com</a>，<a href=\"mailto:service@ue.net.cn\">service@ue.net.cn</a>或者致电：086-0755-88824568 13760195274</strong></p>", 600, false);
    },
    openUploadTool: function (type, elementids, attrs) { //type:single mutil,elementids 需要被赋值的标签 attrs 各标签的属性
        $.dialog.open('/platform/h5/Tools/UploadTools.aspx?type=' + type + '&elementid=' + elementids + '&attr=' + attrs, { title: '上传工具', width: 800, height: 490 }, false);
    }
}

$(function () {
    $("input[maxlength],textarea[lenlimit]").bind("keydown keyup paste", function () { //检查输入长度
        var len = $(this).val().length;
        var maxlen = 0;
        if (typeof ($(this).attr("lenlimit")) != "undefined")
            maxlen = parseInt($(this).attr("lenlimit"));
        else if (typeof ($(this).attr("maxlength")) != "undefined")
            maxlen = parseInt($(this).attr("maxlength"));

        if (len >= maxlen) {
            $(this).attr("data-toggle", "tooltip");
            $(this).attr("data-placement", "top");
            $(this).attr("title", "最多可输入 " + maxlen + " 个字符");
            $(this).tooltip('show');

            $(this).val($(this).val().cut(maxlen)); //截断超出的长度
        } else {
            $(this).removeAttr("data-toggle");
            $(this).removeAttr("data-placement");
            $(this).removeAttr("title");
            $(this).tooltip('destroy')
        }
    });
})