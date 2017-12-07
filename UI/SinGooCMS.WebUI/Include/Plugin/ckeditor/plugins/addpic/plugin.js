(function () {
    //Section 1 : 按下自定义按钮时执行的代码
    var a = {
        exec: function (editor) {
            h5.openUploadTool('ckeditor', editor.name, null);
        }
    },
    b = 'addpic';
    CKEDITOR.plugins.add(b, {
        init: function (editor) {
            editor.addCommand(b, a);
            editor.ui.addButton('addpic', {
                label: '添加图片',
                icon: this.path + 'img.gif',
                command: b
            });
        }
    });
})();