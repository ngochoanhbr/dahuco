//obj 对象,type指示提示的状态，值为1、2、3、4，msg 文字
//1：正在检测/提交数据
//2：通过验证
//3：验证失败
//4：提示ignore状态
function uiitemstatus(obj,type,msg){
	var errorCss = "ui-form-item-error";
	var texterrorCss = "ui-tiptext ui-tiptext-error";
	var successCss = "ui-tiptext ui-tiptext-success";
	
	obj.parent(".ui-form-item").removeClass(errorCss);
	obj.removeClass(texterrorCss);
	obj.removeClass(successCss);
	switch(type){
		case 1:
		obj.html("<i class='ui-tiptext-icon iconfont' title='等待'>&#xe608;</i>"+msg);
		break;
		case 2:
		obj.addClass(successCss)
		obj.html("<i class='ui-tiptext-icon iconfont' title='通过'>&#xe602;</i>"+msg);
		break;
		case 3:
		obj.parent(".ui-form-item").addClass(errorCss);
		obj.addClass(texterrorCss)
		obj.html("<i class='ui-tiptext-icon iconfont' title='出错'>&#xe604;</i>"+msg);
		break;
		case 4:
		obj.html("<i class='ui-tiptext-icon iconfont' title='提示'>&#xe605;</i>"+msg);
		break;
	}
	
}

//显示在右侧的信息
function uiitemrightstatus(obj,type,msg){
	var errorCss = "ui-form-item-error";
	var texterrorCss = "ui-tiptext-error";
	var successCss = "ui-tiptext-success";
	
	obj.parent(".ui-form-item").removeClass(errorCss);
	obj.removeClass(texterrorCss);
	obj.removeClass(successCss);
	switch(type){
		case 1:
		obj.html("");
		break;
		case 2:
		obj.addClass(successCss)
		obj.html("");
		break;
		case 3:
		obj.parent(".ui-form-item").addClass(errorCss);
		obj.addClass(texterrorCss)
		obj.html(msg);
		break;
		case 4:
		obj.html("");
		break;
	}
	
}