;(function (jQuery){
    this.params={province:"",city:"",county:""};
	this.doml = ''; //文本框表单
	this.id = '';
	this.init = function(f){
		if($("#areaopt_div").is(':visible')){
			$("#areaopt_div").remove();
		}
		$("body").after("<div id='areaopt_div'> <div id='areaopt'> <div class='areaopt_province'> <div class='areaopt_con'>省份选择</div></div><div class='clear'></div> <div class='areaopt_city'><div class='areaopt_con'>城市选择</div> <div class='clear'></div> </div><div class='clear'></div> <div class='areaopt_zone'><div class='areaopt_con'>地区选择</div> <div class='clear'></div> </div><div class='clear'></div> <div class='areaopt_tools'> <span class='areaopt_info'></span> <input type='button' value='确定' rel='confirm' class='areaopt_btn' /> <input type=button rel='close' value='关闭' class='areaopt_btn' /><div class='clear'></div></div> </div> <iframe frameborder=0 id='areaopt_back' height=40></iframe> <input type=hidden id='areaopt_province' /><input type='hidden' id='areaopt_city' /><input type='hidden' id='areaopt_zone' /> </div> <input type=hidden id='"+id+"' name='"+id+"' />");
		$("#areaopt .areaopt_btn[rel='close']").click(function(){
			$('#areaopt_div').remove();
		});
		$("#areaopt .areaopt_btn[rel='confirm']").click(function(){ //点击确定按钮，返回选择的省市区
            var area=getParams();
            if(area=="") $("#areaopt_info").text('未选择任何地区！');
            else{ $(doml).val(area);$('#areaopt_div').remove(); }
		});
		this.getprovince();
	}
	this.bind = function(dom){
		this.handle(dom);
	}
	this.beforeinit = function(dom){
		$(dom).attr('readonly', 'true');
	}
	this.setframe = function(){
		$("#areaopt_back").css({width : getFullWidth($('#areaopt')[0]), height : getFullHeight($('#areaopt')[0])});
		dx = pageX($(doml)[0]);
		dy = pageY($(doml)[0]);
		dw = getFullWidth($(doml)[0]);
		dh = getFullHeight($(doml)[0]);
		aw = getFullWidth($("#areaopt")[0]);
		ah = getFullHeight($("#areaopt")[0]);
		ww = windowWidth();
		wh = windowHeight();
        
		if(dx+dw+aw>ww){
			$('#areaopt_div').css('left',dx-aw-15);
		}else{
			$('#areaopt_div').css('left',dx+dw+1);
		}
        $('#areaopt_div').css('top',dy+dh-5);
	}
	this.handle = function(dom){
		if(dom.indexOf(',')==-1){					
			doml = dom;
			id = dom.substr(1)+'_id';
			beforeinit(dom);
			$(dom).click(function(){
				init(dom);
			});
		}else{
			doms = dom.split(',');
			for(i=0; i<doms.length; i++){
				beforeinit(doms[i]);
				$(doms[i]).click(function(){
					doml = '#'+$(this).attr('id');
					id = $(this).attr('id')+'_id';
					init(doml);
				});
			}
		}
	}
	this.getprovince = function(){ //获取省份
		var pdatas = this.getData(0);
		$("#areaopt .areaopt_province .areaopt_con").html(pdatas);
		$('#areaopt_div').show();
		setframe();
		$("#areaopt .areaopt_province a").click(function(){ //省份点击
			$(this).parent().find('.areaopt_mouseon').removeClass('areaopt_mouseon');
			$(this).addClass('areaopt_mouseon');
			if($("#areaopt .areaopt_zone").css('display')!='none'){
				$("#areaopt .areaopt_zone").hide();
			}
            setParams($(this).text(),"","");
			$("span.areaopt_info").text(getParams());			
			$("#areaopt .areaopt_city").show();
			setframe();
			getcity($(this).attr('rev'));
			return false;
		});		
		return false;
	}
	this.getcity = function(provinceId){
		var cdatas = this.getData(provinceId);
		$("#areaopt .areaopt_city .areaopt_con").html(cdatas);
		$("#areaopt_back").css({width : getFullWidth($('#areaopt')[0]), height:getFullHeight($('#areaopt')[0])});
		$("#areaopt .areaopt_city a").click(function(){
			$(this).parent().find('.areaopt_mouseon').removeClass('areaopt_mouseon');
			$(this).addClass('areaopt_mouseon');
            setParams(params.province,$(this).text(),"");
			$("span.areaopt_info").html(getParams());
			
			$("#areaopt .areaopt_zone").show();
			setframe();
			getzone($(this).attr('rev'));
			return false;
		});
		return false;
	}
	this.getzone = function(cityId){
		var zdatas = this.getData(cityId);
		$("#areaopt .areaopt_zone .areaopt_con").html(zdatas);
		$("#areaopt_back").css({width : getFullWidth($('#areaopt')[0]), height:getFullHeight($('#areaopt')[0])});
		$("#areaopt .areaopt_zone a").click(function(){ //区县点击
            setParams(params.province,params.city,$(this).text());
			$(doml).val(getParams());
			$('#areaopt_div').remove();
			return false;
		});
		return false;
	}
    this.setParams=function(p,c,z){
        this.params.province=p;
        this.params.city=c;
        this.params.county=z;
    }
    this.getParams=function(){
        var str=this.params.province;
        if(str!=""){
            str=this.params.city==""?str:str+","+this.params.city;
            return this.params.county==""?str:str+","+this.params.county;
        }
        return "";
    }
    this.getData=function(parentId){
        var zdatas = '';
        $.ajaxSettings.async = false;
        $.getJSON("/Config/zone.json",function(data){ 
            $.each(data,function(i,item){
                if(item.ParentID==parentId){
                    zdatas+= "<a href='javascript:void(0)' rev='"+item.AutoID+"' title='"+item.ZoneName+"'>"+item.ZoneName+"</a> ";
                }
            });
        }); 
        $.ajaxSettings.async = true;

        return zdatas;
    }
	this.pageX = function(elem){
		return elem.offsetParent?(elem.offsetLeft+pageX(elem.offsetParent)):elem.offsetLeft;
	}
	this.pageY = function(elem){
    	return elem.offsetParent?(elem.offsetTop+pageY(elem.offsetParent)):elem.offsetTop;
	}
	this.getStyle = function(elem,name){
		if(elem.style[name]){
			return elem.style[name];
			}else if(elem.currentStyle){
				return elem.currentStyle[name];
			}else if(document.defaultView&&document.defaultView.getComputedStyle){
				name=name.replace(/([A-Z])/g,"-$1");
				name=name.toLowerCase();
				var s=document.defaultView.getComputedStyle(elem,"");
				return s&&s.getPropertyValue(name);
		}else{
			return null;
		}
    }
	this.getFullHeight = function(elem){
		if(getStyle(elem,"display")!="none"){
       		return getHeight(elem)||elem.offsetHeight;
        }else{
    		var old=resetCss(elem,{display:"block",visibility:"hidden",position:"absolute"});
			var h=elem.clientHeight||getHeight(elem);
       		restoreCss(elem,old);
        	return h;
        }
    }
	this.getFullWidth = function(elem){
    	if(getStyle(elem,"display")!="none"){
        	return getWidth(elem)||elem.offsetWidth;
        }else{
			var old=resetCss(elem,{display:"block",visibility:"hidden",position:"absolute"});
			var w=elem.clientWidth||getWidth(elem);
			restoreCss(elem,old);
			return w;
        }
    }
	this.resetCss = function(elem,prop){
		var old={};
		for(var i in prop){
			old[i]=elem.style[i];
			elem.style[i]=prop[i];
		}
       	return old;
    }
	this.restoreCss = function(elem,prop){
   		for(var i in prop){
			elem.style[i]=prop[i];
        }
    }
	this.getHeight = function(elem){
		return parseInt(getStyle(elem,"height"));
    }
	this.getWidth = function(elem){
		return parseInt(getStyle(elem,"width"));
    }
	this.windowHeight = function(){
		var de = document.documentthisent;
		return self.innerHeight||(de && de.offsetHeight)||document.body.offsetHeight;
	}
	this.windowWidth = function(){
		var de = document.documentthisent;
		return self.innerWidth||( de && de.offsetWidth )||document.body.offsetWidth;
	}

	jQuery.areaopt = this;
    return jQuery;
})(jQuery);