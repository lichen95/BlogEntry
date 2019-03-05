/*
 * @Author: Paco
 * @Date:   2017-02-15
 * +----------------------------------------------------------------------
 * | jqadmin [ jq酷打造的一款懒人后台模板 ]
 * | Copyright (c) 2017 http://jqadmin.jqcool.net All rights reserved.
 * | Licensed ( http://jqadmin.jqcool.net/licenses/ )
 * | Author: Paco <admin@jqcool.net>
 * +----------------------------------------------------------------------
 */

layui.define(['jquery', 'jqtags', 'jqform', 'upload'], function(exports) {
    var $ = layui.jquery,
        upload = layui.upload,
        ueditor = layui.ueditor,
        form = layui.jqform,
        jqtags = layui.jqtags;


    //定义提交表单时的回调方法
    form.ajaxTest = function (data, options) {
        console.log(data);
        console.log(options);

        alert('定义了表单提交处理方法，可以在此封装ajax提交方法');

        add();

        
    }

    function add() {
        var oValue = $("[name=yzm]").val().toUpperCase();
        console.log(oValue);
        if (oValue == 0) {
            return layer.msg('请输入验证码');
        }
        else if (oValue != code) {
            oValue = ' ';
            createCode();
            layer.msg('验证码不正确，请重新输入');
            return false;
        }
        else {
            $.ajax({
                url: "/users/Login?username=" + $("[name=username]").val() + "&password=" + $("[name=password]").val(),
                type: "post",
                success: function (data) {
                    console.log(data);
                    if (data == "200")
                        location.href = '/users/index';
                    else {
                        layer.msg('账号或密码不正确');
                        oValue = ' ';
                        createCode();
                    }
                }
            })
        }
    }

    //标签初始化
    jqtags.init();
    form.init({
        "form": "#form1",
        "ajax": "ajaxTest"
    });


    //上传组件初始化
    upload.render({
        elem: '.test',
        done: function(res, index, upload) {
            //获取当前触发上传的元素，一般用于 elem 绑定 class 的情况，注意：此乃 layui 2.1.0 新增
            var item = this.item;
        }
    })

    


    exports('myform', {});
});