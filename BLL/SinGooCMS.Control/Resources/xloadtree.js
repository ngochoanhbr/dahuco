

webFXTreeConfig.loadingText="加载中...";
webFXTreeConfig.loadErrorTextTemplate="Error loading \"%1%\"";
webFXTreeConfig.emptyErrorTextTemplate="Error \"%1%\" does not contain any tree items";

function WebFXLoadTree(sText,sXmlSrc,sAction,sBehavior,sIcon,sOpenIcon){
	this.WebFXTree=WebFXTree;
	this.WebFXTree(sText,sAction,sBehavior,sIcon,sOpenIcon);
	this.src=sXmlSrc;
	this.loading=false;
	this.loaded=false;
	this.errorText="";
	if(this.open)
		_startLoadXmlTree(this.src,this);
	else{
		this._loadingItem=new WebFXTreeItem(webFXTreeConfig.loadingText);
		this.add(this._loadingItem)
	}
}

function WebFXLoadTree(sText,sXmlSrc,sAction,sBehavior,sIcon,sOpenIcon,sTarget){
	this.WebFXTree=WebFXTree;
	this.WebFXTree(sText,sAction,sBehavior,sIcon,sOpenIcon);
	this.src=sXmlSrc;
	this.loading=false;
	this.loaded=false;
	this.errorText="";
	this.target=sTarget;
	if(this.open)
		_startLoadXmlTree(this.src,this);
	else{
		this._loadingItem=new WebFXTreeItem(webFXTreeConfig.loadingText);
		this.add(this._loadingItem)
	}
}

WebFXLoadTree.prototype=new WebFXTree;
WebFXLoadTree.prototype._webfxtree_expand=WebFXTree.prototype.expand;
WebFXLoadTree.prototype._webfxtreeitem_expand=WebFXTree.prototype.expand;
WebFXLoadTree.prototype.expand=function(){
	if(!this.loaded&&!this.loading){
		_startLoadXmlTree(this.src,this)
	}
	this._webfxtree_expand()
};
	
function WebFXLoadTreeItem(
	sText,sXmlSrc,sAction,eParent,sIcon,sOpenIcon,
	nodeId,arrModelId,arrModelName,expand,
	anchorType,nodeType,arrPurview,title){
	this.WebFXTreeItem=WebFXTreeItem;
	this.WebFXTreeItem(sText,sAction,eParent,sIcon,sOpenIcon,nodeId,arrModelId,arrModelName,expand,anchorType,nodeType,arrPurview,title);
	this.src=sXmlSrc;
	this.loading=false;
	this.loaded=false;
	this.errorText="";
	this.open=(webFXTreeHandler.cookies.getCookie(this.id.substr(18,this.id.length-18))=='1')?true:false;
	if(this.open)_startLoadXmlTree(this.src,this);
	else{
		this._loadingItem=new WebFXTreeItem(webFXTreeConfig.loadingText);
		this.add(this._loadingItem)
	}
}

WebFXLoadTreeItem.prototype=new WebFXTreeItem;

WebFXLoadTreeItem.prototype._webfxtreeitem_expand=WebFXTreeItem.prototype.expand;
WebFXLoadTreeItem.prototype.expand=function(){
	if(!this.loaded&&!this.loading){_startLoadXmlTree(this.src,this)}
	this._webfxtreeitem_expand()
};
	
WebFXLoadTree.prototype.reload=WebFXLoadTreeItem.prototype.reload=function(){
	if(this.loaded){
		var open=this.open;
		while(this.childNodes.length>0)
			this.childNodes[this.childNodes.length-1].remove();
		this.loaded=false;
		this._loadingItem=new WebFXTreeItem(webFXTreeConfig.loadingText);
		this.add(this._loadingItem);
		if(open)this.expand()
	}else if(this.open&&!this.loading)
		_startLoadXmlTree(this.src,this)
};
		
function _startLoadXmlTree(sSrc,jsNode){
	if(jsNode.loading||jsNode.loaded)return;
	jsNode.loading=true;
	var xmlHttp=XmlHttp.create();
	xmlHttp.open("GET",sSrc,true);
	xmlHttp.onreadystatechange=function(){
		if(xmlHttp.readyState==4){
			_xmlFileLoaded(xmlHttp.responseXML,jsNode)
		}
	};
	window.setTimeout(function(){xmlHttp.send(null)},10)
}
	
function _xmlTreeToJsTree(oNode){
	var text=oNode.getAttribute("text");
	var action=oNode.getAttribute("action");
	var parent=null;
	var icon=oNode.getAttribute("icon");
	var openIcon=oNode.getAttribute("openIcon");
	var src=oNode.getAttribute("src");
	var target=oNode.getAttribute("target");
	var nodeId=oNode.getAttribute("nodeId");
	var arrModelId=oNode.getAttribute("arrModelId");
	var arrModelName=oNode.getAttribute("arrModelName");
	var expand=oNode.getAttribute("expand");
	var anchorType=oNode.getAttribute("anchorType");
	var nodeType=oNode.getAttribute("nodeType");
	var arrPurview=oNode.getAttribute("arrPurview");
	var title=oNode.getAttribute("title");
	var jsNode;
	if(src!=null&&src!="")
		jsNode=new WebFXLoadTreeItem(text,src,action,parent,icon,openIcon,nodeId,arrModelId,arrModelName,expand,anchorType,nodeType,arrPurview,title);
	else 
		jsNode=new WebFXTreeItem(text,action,parent,icon,openIcon,nodeId,arrModelId,arrModelName,expand,anchorType,nodeType,arrPurview,title);
	if(target!="")
		jsNode.target=target;
	var cs=oNode.childNodes;
	var l=cs.length;
	for(var i=0;i<l;i++){
		if(cs[i].tagName=="tree")jsNode.add(_xmlTreeToJsTree(cs[i]),true)
	}
	return jsNode
}
	
function _xmlFileLoaded(oXmlDoc,jsParentNode){
	if(jsParentNode.loaded)return;
	var bIndent=false;
	var bAnyChildren=false;
	jsParentNode.loaded=true;
	jsParentNode.loading=false;
	if(oXmlDoc==null||oXmlDoc.documentElement==null){
		jsParentNode.errorText=parseTemplateString(webFXTreeConfig.loadErrorTextTemplate,jsParentNode.src)
	}else{
		var root=oXmlDoc.documentElement;
		var cs=root.childNodes;
		var l=cs.length;
		for(var i=0;i<l;i++){
			if(cs[i].tagName=="tree"){
				bAnyChildren=true;
				bIndent=true;
				jsParentNode.add(_xmlTreeToJsTree(cs[i]),true)
			}
		}
		if(!bAnyChildren){
			jsParentNode.errorText=parseTemplateString(webFXTreeConfig.emptyErrorTextTemplate,jsParentNode.src)
		}else{
			webFXTreeHandler.cookies.setCookie(jsParentNode.id.substr(18,jsParentNode.length-18),'1');
			jsParentNode._webfxtreeitem_expand()
		}
	}
	if(jsParentNode._loadingItem!=null){
		jsParentNode._loadingItem.remove();
		bIndent=true
	}
	if(bIndent){
		jsParentNode.indent()
	}
	OverrideXmlFileLoaded(oXmlDoc,jsParentNode)
}
	
function OverrideXmlFileLoaded(oXmlDoc,jsParentNode){}

function parseTemplateString(sTemplate){
	var args=arguments;
	var s=sTemplate;
	s=s.replace(/\%\%/g,"%");
	for(var i=1;i<args.length;i++){
		s=s.replace(new RegExp("\%"+i+"\%","g"),args[i])
	}
	return s
}