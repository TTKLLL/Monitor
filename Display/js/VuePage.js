//设置最完成div的id为vueDiv
//设置用于填写查询信息的form id为queryForm
//设置显示分页链接的<ul>标签的id为page




var Person = (function () {
    //静态私有属性方法

    function sayHome(name) {
        alert("dd")
        //  console.log(name + "'s home in " + home);
    }


    //构造函数
    function _person(url) {

        this.url = url;
        Vue.config.devtools = true  //      
        var _this = this;

        this.vue = new Vue({
            el: '#vueDiv',
            data: {
                res: "aa"
            },
            created: function () {
                that = this;

                //页面首次打开时加载的数据
                $.post(_this.url, "", function (returnData) {

                    that.res = JSON.parse(returnData)
                    $("#page").html(that.res[1]);
                })


            },
            methods: {

            }
        })




    }
    //静态共有属性方法
    _person.prototype = {
        constructor: _person,

        sayWord: function () {
            //alert(this.vue.res)
        },
        //通过ajax通过参数获取数据
        AjaxGetData: function (data) {
            $.post(this.url, data, function (returnData) {
                this.vm.res = JSON.parse(returnData)
                $("#page").html(this.vm.res);
            })
        },

        //通过查询条件和页码获取数据
        GetDataByPageNumber: function (pageNumber) {
            AjaxGetData(url + '?' + $("#queryForm").serialize(),
             { pageNumber: number }, this.vm)
        }






    }
    return _person;
})();


