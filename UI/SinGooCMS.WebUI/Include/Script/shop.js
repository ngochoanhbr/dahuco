document.write("<script src='/Config/pro.js?session=Math.random()' type='text/javascript'></script>");
document.write("<link href='/Include/Plugin/autocomplete/jquery.autocomplete.css' rel='stylesheet' type='text/css' />");
document.write("<script src='/Include/Plugin/autocomplete/jquery.autocomplete.js' type='text/javascript'></script>");
var shop = new Object;
/*lstParams: detParams: cartParams: calParams:*/
shop = { lstParams: {}, detParams: {}, cartParams: {}, calParams: {},
    createUrl: function (url, source, keys, values) { 
        var lstStr = "", arrKey = keys.split(','), arrVals = values.split(',');
        $.each(source, function (itemkey, itemvalue) {
            lstStr += "&" + itemkey + "=" + itemvalue;
            for (var i = 0; i < arrKey.length; i++) {
                if (arrKey[i] == itemkey)
                    lstStr = lstStr.replace("&" + itemkey + "=" + itemvalue, "&" + itemkey + "=" + arrVals[i]);
            }
        });
        location = (url == "" ? "?" : url + "?") + lstStr.substr(1);
    },
    createLstUrl: function (key, value) {
        this.createUrl("/shop/search", this.lstParams, key, value);
    },
    search: function () { //
        var id_keyctrl = arguments[0];
        var key = id_keyctrl != "" ? $("#" + id_keyctrl).val() : "";
        if (arguments.length == 2)
            key = arguments[1];

        location = key == "" ? "/shop/search" : "/shop/search?key=" + encodeURI(key);
    },
    autocomplete: function () { 
        var id_inputer = arguments[0];
        var box_weight = 455;
        var box_height = 200;
        if (arguments.length == 2)
            box_weight = arguments[1];
        if (arguments.length == 3)
            box_height = arguments[2];

        $("#" + id_inputer).autocomplete(prodata, {
            minChars: 1,
            max: 10,
            autoFill: false,
            mustMatch: false,
            matchContains: true,
            scrollHeight: box_height,
            width: box_weight,
            multiple: false,
            formatItem: function (row, i, max) {
                return "<span style='width:400px'>" + row.ProductName + "</span>";
            },
            formatMatch: function (row, i, max) {
                return row.ProductName;
            },
            formatResult: function (row) {
                return row.ProductName;
            }
        });
        $("#" + id_inputer).result(function (event, row, formatted) {
            shop.search(id_inputer);
        });
    },
    plusNum: function () { 
        this.detParams.quantity = parseInt($('#' + this.detParams.id_quantity).val());
        if (this.detParams.quantity < this.detParams.stock) { this.detParams.quantity++; $("#" + this.detParams.id_quantity).val(this.detParams.quantity); }  //增1
    },
    minusNum: function () { 
        this.detParams.quantity = parseInt($('#' + this.detParams.id_quantity).val());
        if (this.detParams.quantity > 1) { this.detParams.quantity--; $('#' + this.detParams.id_quantity).val(this.detParams.quantity); }  //减1
    },
    updateNum: function () { 
        this.detParams.quantity = parseInt($('#' + this.detParams.id_quantity).val());
        if (this.detParams.quantity < 1) this.detParams.quantity = 1;
        else if (this.detParams.quantity > this.detParams.stock) this.detParams.quantity = this.detParams.stock;
        $('#' + this.detParams.id_quantity).val(this.detParams.quantity);
    },
    buyNow: function () { 
        if (!user.isLogined())
            showmsg("Bạn chưa đăng nhập, vui lòng <a href='/user/login?returnurl=" + encodeURI(location.href) + "'>đăng nhập</a>");
        else if (this.validBuyParam()) {
            location = "/shop/settlement?buytype=SingleBuy&pid=" + this.detParams.proid + "&quantity=" + this.detParams.quantity + "&attrid=" + this.detParams.attrid;
        }
    },
    addToCart: function () {
        if (this.validBuyParam()) {
            $.post('/shop/addtocart', { _pid: this.detParams.proid, _buynumber: this.detParams.quantity, _attrid: this.detParams.attrid, temp: Math.random() }, function (data) {
                data.ret == "success" ? showmsg("<div class='alert-cart'><img src='/Include/Images/cart.png'><p>Đã được thêm thành công vào giỏ hàng, [<a href='/shop/cart'>Xem giỏ hàng</a>]</p></div>") : showtip(data.msg);
            }, "JSON");
        }
    },
    validBuyParam: function () { 
        if (this.detParams.quantity > this.detParams.stock) {
            showtip("Không đủ số lượng! Vui lòng nhập số mua hợp lệ");
            return false;
        }
        else if (this.detParams.hasGuiGe == 1 && this.detParams.attrid == -1) {
            showtip("Vui lòng chọn thuộc tính");
            return false;
        }

        return true;
    },
    sendGoodsQA: function () { 
        if (arguments.length > 1 && arguments[1] == "")
            showtip("Vui lòng nhập nội dung câu hỏi");
        else {
            $.post("/include/ajax/ajaxmethod", { t: "sendgoodsQA", proid: arguments[0], question: arguments[1], temp: Math.random() }, function (data) {
                showtip(data.msg);
            }, "JSON");
        }
    },
    getGoodsAttr: function () {
        shop.detParams.attrpath = "";
        $("a[name='" + arguments[0] + "']").each(function (i, item) {
            if ($(item).parent().hasClass("hover"))
                shop.detParams.attrpath += $(item).attr("attrvalue") + ",";
        });
        if (shop.detParams.attrpath.endWith(",")) {
            shop.detParams.attrpath = shop.detParams.attrpath.cutRight(); 
        }
        var pricearea = arguments[1]; 
        var stockarea = arguments[2]; 
        $.getJSON("/include/ajax/ajaxmethod", { t: "getgoodsattr", proid: shop.detParams.proid, attrpath: shop.detParams.attrpath, temp: Math.random() }, function (data) {
            shop.detParams.a = data.AutoID != null ? 1 : 0;
            shop.detParams.attrid = data.AutoID != null ? data.AutoID : -1;
            shop.detParams.stock = data.AutoID != null ? data.Stock : 0;
            shop.detParams.attrprice = data.AutoID != null ? data.SellPrice : shop.detParams.baseprice;
            $("#" + pricearea).html("$" + (shop.detParams.attrprice).toFixed(2));
            $("#" + stockarea).html("Hàng tồn kho " + shop.detParams.stock + " cái");
        });
    },
    addFavorite: function (element, goodsid) { 
        if (!user.isLogined())
            showmsg("Bạn chưa đăng nhập hoặc thoát thời gian chờ, vui lòng <a href='/user/login?returnurl=" + location.href + "'>đăng nhập</a>");
        else
            $.getJSON("/include/ajax/ajaxmethod?t=addprofavorite&pid=" + goodsid + "&temp=" + Math.random(), function (data) {
                showtip(data.msg);
                $(element).html(data.status == 1 ? "<i class='icon'></i>Đã được thu thập" : "<i class='icon gray'></i>Bộ sưu tập")
            });
    },
    addStockout: function (pid) { //缺货登记
        if (!user.isLogined())
            showmsg("Bạn chưa đăng nhập hoặc thoát thời gian chờ, vui lòng <a href='/user/login?returnurl=" + location.href + "'>đăng nhập</a>");
        else
            $.post("/include/ajax/ajaxmethod", { t: "addstockout", pid: pid, temp: Math.random() }, function (data) { showtip(data.msg); }, "JSON");
    },
    cartMinus: function (id) {
        location = "?action=minus&ids=" + id;
    },
    cartPlus: function (id) {
        location = "?action=plus&ids=" + id;
    },
    upQuantity: function (id, numctrl) {
        location = "?action=quantity&ids=" + id + "&num=" + parseInt($("#" + numctrl).val());
    },
    delCart: function (ids) {
        if (confirm("Bạn có chắc chắn để loại bỏ các mục từ giỏ mua hàng?？") && ids != "")
            location = "?action=del&ids=" + (ids.indexOf(",") != -1 ? ids.substring(0, ids.length - 1) : ids);
    },
    clearCart: function () {
        location = "?action=clear";
    },
    getCartTotal: function () {
        this.cartParams.ids = "";
        this.cartParams.goodscount = 0;
        this.cartParams.goodsamount = 0.0;
        $.each($("input[name='" + this.cartParams.name_chk + "']"), function (index, item) {
            if (item.checked) {
                shop.cartParams.ids += $(item).val() + ",";
                shop.cartParams.goodscount++;
                shop.cartParams.goodsamount += parseFloat($(item).attr("subtotal"));
            }
        });
        this.cartParams.ids = this.cartParams.ids.endWith(",") ? this.cartParams.ids.substr(0, this.cartParams.ids.length - 1) : this.cartParams.ids;
        $("#" + this.cartParams.id_gc).html(shop.cartParams.goodscount);
        $("#" + this.cartParams.id_ga).html("￥" + shop.cartParams.goodsamount.toFixed(2));
        $("#" + this.cartParams.id_submit).attr("class", (shop.cartParams.goodscount > 0 ? "submit-btn" : "submit-btn-disabled")); 
    },
    submitCart: function () {
        if (!user.isLogined())
            showmsg("Bạn chưa đăng nhập hoặc thoát thời gian chờ, vui lòng <a href='/user/login?returnurl=" + location.href + "'>đăng nhập</a>");
        else if (this.cartParams.goodscount <= 0)
            showtip("Vui lòng chọn sản phẩm");
        else
            location = "/shop/settlement?buytype=NormalBuy&ids=" + this.cartParams.ids;
    },
    calculate: function () { 
        var queryStr = "{addrid:" + this.calParams.addrid + ",pros:["; // ]}
        $.each($("input[name='" + this.calParams.name_pnpair + "']"), function (index, item) { queryStr += $(item).val() + "," });
        queryStr = (queryStr.endWith(",") ? queryStr.substring(0, queryStr.length - 1) : queryStr) + "]}";
        $.getJSON("/include/ajax/ajaxmethod?t=getshippingfee&querystr=" + queryStr + "&temp=" + Math.random(), function (data) {
            shop.calParams.shippingfee = data.totalfee;
            shop.calParams.couponsfee = parseFloat($("input[name='" + shop.calParams.name_yhq + "']:checked").attr("notes"));
            if (isNaN(shop.calParams.couponsfee)) shop.calParams.couponsfee = 0.0;
            shop.calParams.ordertotal = shop.calParams.goodstotal + shop.calParams.shippingfee - shop.calParams.couponsfee;
            $("#" + shop.calParams.id_yunfei).html("$" + shop.calParams.shippingfee.toFixed("2"));
            $("#" + shop.calParams.id_yhq).html("-$" + shop.calParams.couponsfee.toFixed("2"));
            $("#" + shop.calParams.id_total).html("Tổng số tiền phải trả：<strong>￥" + shop.calParams.ordertotal.toFixed("2") + "</strong>");
        });
    },
    emptyHistory: function () { 
        //$.cookie('SinGooShop_History', null);
        $.cookie('SinGooShop_History', '', {path: "/", expires: -1 });
    }
};

/*
* 
*/
var user = new Object();
var totaltime = 60;
var d = null;
user = { logincookie: $.cookie('singoouser'), orderParams: {},

    isLogined: function () {
        return this.logincookie != null;
    },
    getcode: function (t, type, paramval, sender) {
        dialog = $.dialog({ cancel: false, padding: 10, title: 'Đang được xử lý...', content: "<img src='/Include/Images/loading.gif' alt='' /> Vui lòng đợi, hệ thống đang xử lý...", width: 260, height: 30, lock: false });
        var sendurl = "/include/ajax/ajaxmethod?t=" + t + "&type=" + type + "&paramval=" + paramval + "&temp=" + Math.random();
        $.getJSON(sendurl, function (json) {
            if (json != null && json != "") {
                dialog.close();
                showtip(json.msg);
                if (json.timeout > 0) {
                    totaltime = json.timeout;
                    d = setInterval(showtimeout, 1000, sender);
                }
            }
        });
    },
    showTimeout: function (sender) {
        totaltime--;
        if (totaltime > 0) {
            $(sender).val("Nhận mã xác minh" + "(" + totaltime + ")");
            $(sender).attr("disabled", "disabled");
        } else {
            $(sender).val("Nhận mã xác minh");
            $(sender).removeAttr("disabled");
            d.clearInterval();
        }
    },
    getMailCode: function () { 
        var sender = arguments[0]; 
        var t = arguments[1]; 
        var id_ctrl = arguments[2]; 
        var email = id_ctrl == "" ? "" : $("#" + id_ctrl).val(); 
        if (arguments.length == 4)
            email = arguments[3];

        var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
        if (!reg.test(email))
            showtip("Vui lòng nhập địa chỉ e-mail hợp lệ");
        else {
            this.getcode(t, "byemail", email, sender);
        }
    },
    getSMSCode: function () { 
        var sender = arguments[0];
        var t = arguments[1];
        var id_ctrl = arguments[2];
        var mobile = id_ctrl == "" ? "" : $("#" + id_ctrl).val();
        if (arguments.length == 4)
            mobile = arguments[3];

        if (mobile == "")
            showtip("Vui lòng nhập 11 số điện thoại");
        else {
            this.getcode(t, "bymobile", mobile, sender);
        }
    },
    bind: function (type, paramval, validatecode) { 
        jQuery.getJSON("/include/ajax/ajaxmethod?t=userbind&type=" + type + "&paramval=" + paramval + "&checkcode=" + validatecode + "&temp=" + Math.random(), function (json) {
            if (json.ret == "success") {
                showtip(json.msg);
                location.reload();
            } else {
                showtip(json.msg);
            }
        });
    },
    readMsgAll: function (name_chk) {
        var ids = singoo.getIds(name_chk);
        if (ids != "")
            location = "?action=read&opid=" + ids;
    },
    delMsgAll: function (name_chk) {
        if (confirm('Bạn có chắc chắn muốn xóa nó?？')) {
            var ids = singoo.getIds(name_chk);
            if (ids != "")
                location = "?action=delete&opid=" + ids;
        }
    },
    delAccAll: function (name_chk) {
        if (confirm('Bạn có chắc chắn muốn xóa nó?？')) {
            var ids = singoo.getIds(name_chk);
            if (ids != "")
                location = "?action=delete&opid=" + ids;
        }
    },
    delIntegralAll: function (name_chk) {
        if (confirm('Bạn có chắc chắn muốn xóa nó?？')) {
            var ids = singoo.getIds(name_chk);
            if (ids != "")
                location = "?action=delete&opid=" + ids;
        }
    },
    createOrderUrl: function (key, value) {
        shop.createUrl("", this.orderParams, key, value);
    },
    orderCancel: function (oid) {
        if (confirm('Bạn có chắc chắn sẽ hủy đơn đặt hàng không?')) {
            jQuery.post("/user/myorders", { action: "cancel", oid: oid, temp: Math.random() }, function (data) {
                data.ret == "fail" ? showtip(data.msg) : location = data.url;
            }, "JSON");
        }
    },
    orderPay: function (oid) {
        location = "/shop/cashier?oid=" + oid;
    },
    orderSign: function (oid) {
        if (confirm('Bạn có chắc là bạn có thể đặt hàng?')) {
            jQuery.post("/user/myorders", { action: "sign", oid: oid, temp: Math.random() }, function (data) {
                data.ret == "fail" ? showtip(data.msg) : location = data.url;
            }, "JSON");
        }
    },
    orderDelete: function (oid) {
        jQuery.post("/user/myorders", { action: "delete", oid: oid, temp: Math.random() }, function (data) {
            data.ret == "fail" ? showtip(data.msg) : location = data.url;
        }, "JSON");
    },
    goodsEva: function (oitemid) {
        location = "/shop/evalution?oitemid=" + oitemid;
    },
    addCoupons: function (code) {
        if (code == "") { showtip("Vui lòng nhập số phiếu mua hàng"); }
        else { jQuery.post("/include/ajax/ajaxmethod", { t: "addhyq", code: code, temp: Math.random() }, function (data) { data.ret == "success" ? location = data.url : showtip(data.msg); }, "JSON"); }
    }
}