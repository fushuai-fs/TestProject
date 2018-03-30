
;(function($,window,document,undefined){
    var myplugin=function (element,options) {
        this.$element=element;
        this.defaults={
            'color': 'red',
            'fontSize': '12px',
            'textDecoration':'none'
        },
            this.options=$.extend({}, this.defaults, options)
    };
    myplugin.prototype={
        testMethods:function () {
            return this.$element.css({
                'color': this.options.color,
                'fontSize': this.options.fontSize,
                'textDecoration': this.options.textDecoration
            });
        },
        test2:function () {
            alert('test2');
        }
    }
    $.fn.myPlugin= function (options) {
        var myp = new myplugin(this,options);
        return myp.testMethods();

    }
})(jQuery,window,document);