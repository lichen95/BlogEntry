/*
 * @Author: Paco
 * @Date:   2017-07-19
 * @lastModify 2017-07-24
 * +----------------------------------------------------------------------
 * | jqadmin [ jq酷打造的一款懒人后台模板 ]
 * | Copyright (c) 2017 http://jqadmin.jqcool.net All rights reserved.
 * | Licensed ( http://jqadmin.jqcool.net/licenses/ )
 * | Author: Paco <admin@jqcool.net>
 * +----------------------------------------------------------------------
 */

layui.define(['jquery', 'jqbind', 'form', 'table', 'laydate'], function (exports) {
    var $ = layui.jquery,
        jqbind = layui.jqbind,
        form = layui.form,
        table = layui.table;




    table.on('tool(test)', function (obj) { //注：tool是工具条事件名（内置的tool方法，解释得一点看不懂，算我弱智），test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象

        if (layEvent === 'detail') { //查看
             
          
        } else if (layEvent === 'del') { //删除
            layer.confirm('真的删除行么', function (index) {
                obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
                layer.close(index);
                //向服务端发送删除指令
            });
        } else if (layEvent === 'edit') { //编辑
            //do something，可以通过ajax修改后端的数据，返回成功后更新缓存值
            var _this =  $(tr).find('[lay-event="edit"]');
            jqbind.modal(_this);
 
            //同步更新缓存对应的值
            // obj.update({
            //     username: '123',
            //     title: 'xxx'
            // });
        }
    });

    exports('cat-list', {});
});