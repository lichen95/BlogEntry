/*
 * @Author: Paco
 * @Date:   2017-07-24
 * +----------------------------------------------------------------------
 * | jqadmin [ jq酷打造的一款懒人后台模板 ]
 * | Copyright (c) 2017 http://jqadmin.jqcool.net All rights reserved.
 * | Licensed ( http://jqadmin.jqcool.net/licenses/ )
 * | Author: Paco <admin@jqcool.net>
 * +----------------------------------------------------------------------
 */

layui.define(['jquery', 'form', 'layer'], function(exports) {
    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form,
        jqbind = function() {
            this.options = {
                type: 1,
                title: false,
                maxmin: false,
                shadeClose: true,
                shade: 0.3,
                content: "",
                area: '',
                anim: 5,
                bindCall: ""
            }
        };

    /**
     * @todo 初始化需要绑定事件的元素
     * 
     */
    jqbind.prototype.init = function() {
        var _this = this;

        $(".ajax-all:not([bind])").each(function() {
            _this.bind($(this));
            $(this).on('click', function() {
                _this.checkall(this);
            });
        });


        $(".modal:not([bind])").each(function() {
            _this.bind($(this));
            $(this).bind('click', function() {
                _this.modal(this);
            });
        });

        $(".tab-menu:not([bind])").each(function() {
            _this.bind($(this));
            $(this).bind('click', function() {
                _this.menu(this);
            });
        });
    }


    /**
     * @todo 绑定单击事件
     * @param string obj ID或是class 如#id、.id
     * @param string call 回调的方法
     */
    jqbind.prototype.click = function(obj, call) {
        var _this = this;
        $(obj).on('click', _this[call]);
    }

    /**
     * @todo 结已绑定的元素添加绑定标识
     */
    jqbind.prototype.bind = function($obj) {
        $obj.attr("bind", 1);
    };

    jqbind.prototype.modal = function(obj) {
        var _this = this,
            params = getParams($(obj), "data-params", $),
            options = $.extend({}, _this.options, params),
            _area = ["auto", "auto"];
        if (options.area != "" || options.area != "auto,auto") {
            _area = options.area.split(',');
            var width = parseInt(_area[0]),
                dpr = window.devicePixelRatio,
                maxWidth = $(window).width() - 20;
            if (width > maxWidth) {
                _area[0] = (maxWidth) + "px";
            }
            var height = parseInt(_area[1]),
                maxHeight = $(window).height() - 20;
            if (height > maxHeight) {
                _area[1] = (maxHeight) + "px";
            }
        }
        if (parseInt(options.type) == 2) {
            options.content = options.content + "?" + options.data;
        } else {
            options.content = $(options.content);
        }
        if (!options.area) {
            var l = layer.open({
                type: parseInt(options.type),
                title: options.title,
                shade: options.shade,
                shadeClose: options.shadeClose,
                content: options.content
            });
            layer.full(l);
        } else {
            var l = layer.open({
                type: parseInt(options.type),
                title: options.title,
                shade: options.shade,
                shadeClose: options.shadeClose,
                area: _area,
                content: options.content
            });
        }
    }

    /**
     * 绑定打开菜单
     */
    jqbind.prototype.menu = function(obj) {
        if (top.global.menu) {
            var menu = top.global.menu;
            menu.menuSetOption($(obj));
        } else {
            layer.alert("菜单dataName名称设置错误或不存在", { icon: 6 });
            return false;
        }

    }

    var bind = new jqbind();
    bind.init();
    exports('jqbind', bind);
});