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

layui.define(['jquery', 'jqbind', 'form', 'table', 'laydate'], function(exports) {
    var $ = layui.jquery,
        jqbind = layui.jqbind,
        form = layui.form,
        laydate = layui.laydate,
        table = layui.table;


    //初始化日历
    laydate.render({
        elem: '.start-date' //指定元素
    });

    laydate.render({
        elem: '.end-date' //指定元素
    });

    table.render({
        elem: '#article',
        height: 'full-200',
        page: true,
        limits: [2, 60, 90, 150, 300],
        limit: 60, //默认采用60
        url: './data/article.json',
        cols: [
            [
                { field: 'id',  width:"5%", sort: true, title: "ID" },
                { field: 'title', width:"30%", title: "标题" },
                { field: 'from',   width:"20%",sort: true, title: "来源" },
                { field: 'author',  width:"5%", title: "作者" },
                { field: 'cat_id',  width:"5%", title: "分类" },
                { field: 'order',   width:"3%",sort: true, title: "排序" },
                { toolbar: '#command',  width:"5%",title: "推荐" },
                { toolbar: '#switch', width:"5%",  title: "审核" },
                { align: 'center', toolbar: '#barDemo', width:"15%", title: "操作" }
            ]
        ],
        //注意了：这里的done方法，通过js获取数据渲染才能执行，一个小坑,如果不用这个方法绑定，可以使用layuitable的监听事件实现弹窗
        done: function(res, curr, count) {
            //jqbind.init();
        }
    });

    table.on('tool(article)', function(obj) { //注：tool是工具条事件名（内置的tool方法，解析得一点看不懂，算我弱智），test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        if (layEvent === 'detail') { //查看
            //do somehing
        } else if (layEvent === 'del') { //删除
            layer.confirm('真的删除行么', function(index) {
                obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
                layer.close(index);
                //向服务端发送删除指令
            });
        } else if (layEvent === 'edit') { //编辑
            //do something，可以通过ajax修改后端的数据，返回成功后更新缓存值
            var _this = $(tr).find('[lay-event="edit"]');
            jqbind.modal(_this);

            //同步更新缓存对应的值
            obj.update({
                username: '123',
                title: 'xxx'
            });
        }
    });





    exports('list', {});
});