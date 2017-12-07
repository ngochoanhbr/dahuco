/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'zh-cn';
    //config.uiColor = '#AADC6E';

    config.skin = 'kama'; //选择皮肤，源文件在“ckeditor\skins\”中
    config.resize_enabled = false;

    // 基础工具栏
    // config.toolbar = "Basic";    
    // 全能工具栏
    config.toolbar = "Full";    
    // 自定义工具栏
    config.toolbar_Full =
    [
        ['Source', '-', 'Preview'], ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'ShowBlocks'], '/',
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'], ['Link', 'Unlink', 'Anchor'],
        ['addpic', 'Flash', 'Table', 'HorizontalRule', 'SpecialChar'], '/',
        ['Styles', 'Format', 'Font', 'FontSize'], ['TextColor', 'BGColor'], ['Maximize', '-', 'About']
    ];
    config.toolbar_Basic =
    [
        ['Source', '-', 'Preview'], ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'FontSize', 'TextColor', 'BGColor', '-', 'About'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock']
    ];

    config.extraPlugins = 'addpic';

    config.filebrowserWindowWidth = '800';  //“浏览服务器”弹出框的size设置
    config.filebrowserWindowHeight = '500';
    config.font_names = '宋体/宋体;黑体/黑体;仿宋/仿宋 _GB2312;楷体/楷体_GB2312;隶书/隶书;幼圆/幼圆;' + config.font_names;

};
