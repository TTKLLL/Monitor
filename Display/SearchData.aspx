<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchData.aspx.cs" Inherits="SearchData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/layer/layer.js"></script>
    <script src="js/MyAlert.js"></script>


    <%--   分页以及vue--%>

    <script src="js/vue.js"></script>

    <style type="text/css">
        #dataTable {
            margin-top: 15px;
        }
    </style>
    <link href="css/AjaxPage.css" rel="stylesheet" />
</head>
<body>
        <div id="vueDiv">
            <table class="table table-hover table-bordered table-striped" id="dataTable">
                <caption>
                    <h4 style="text-align: center"><b>传感器数据</b></h4>
                </caption>          
                <thead>
                    <tr class="info">
                        <td>序号</td>
                        <td>设备编号</td>
                        <td>通道号</td>
                        <td>端口</td>
                        <td>点名</td>
                        <td>数据类型</td>
                        <td>时间</td>
                        <td>结果</td>
                    </tr>
                </thead>

                <tbody>
                    <tr v-for="(item, index) in res[0]" >
                        <td>{{(index+1) + (res[1].nowPage-1)*res[1].pageSize}}</td>
                        <td>{{item.sno}}</td>
                        <td>{{item.tdno}}</td>
                        <td>{{item.port}}</td>
                        <td>{{item.pointName}}</td>
                        <td>{{item.dataType}}</td>
                        <td>{{item.time}}</td>
                        <td>{{item.res}}</td>
                    </tr>
                </tbody>
            </table>
        
            
            <div style="float: right; margin-right: 50px;">
                <button v-on:click="prePage" id="prev" type="button" class="btn btn-primary btn-xs">上一页</button>

                <button id="next" v-on:click="nextPage" type="button" class="btn btn-primary btn-xs">下一页</button>

                &nbsp;第<input  :value="res[1].nowPage" id="targetPage" style="width: 25px; height: 22px;" />页
                
                <button type="button" class="btn btn-primary btn-xs" v-on:click="turnTo" id="turnTo">转到</button>
                共&nbsp;<b><label id="totalPage">{{res[1].totalPage}}</label>&nbsp;页 &nbsp;&nbsp;&nbsp;&nbsp;
                    共&nbsp;<b>{{res[1].totlaNumber}}</b>&nbsp;条记录
            </div>
      
         <form id="form1" runat="server" action="GetData.ashx" method="get">
            <table style="line-height: 50px; background-color: #F9F9F9; margin-bottom: 0 !important; padding-bottom: 0 !important" id="basicQuery" class="table table-bordered table-condensed">
                <tr>
                    <td style="line-height: 35px; width: 120px;">请选择数据类型</td>
                    <td style="width: 150px;">
                        <select id="dataTypeSelect" name="dataType" class="form-control" style="width: 150px;">
                            <option  value="all">全部</option>
                            <option v-for="item in res2" :key="item.dataType" :value="item.dataType" >{{item.dataType}}</option>
                        </select>
                    </td>
                    <td><input  type="button" v-on:click="search" id="search" class="btn btn-primary" value="查找"/></td>
                </tr>
            </table>
          </form> 
        </div>

    <script type="text/javascript">
        function AjaxGetData(data, vm) {
            var dataTypeSelect = $("#dataTypeSelect")
            var formData = decodeURIComponent($("#form1").serialize(), true)

            $.get("GetData.ashx?" + formData + "&para=" + Math.random(), data,
                function (returnData) {
                    vm.res = JSON.parse(returnData)
                })
        }

        function GetAllDataType(vm) {

            $.get("GetAllDataType.ashx?para=" + Math.random(),
                function (data) {
                    vm.res2 = JSON.parse(data)
                })
        }

        //VUE对象
        Vue.config.devtools = true
        var vm = new Vue({
            el: '#vueDiv',
            data: {
                res: "",
                res2: "",
                rowNumber: 1
            },
            created: function () {
                var that = this;
                //跟她说你非常适合这个岗位  想去面试
                //页面首次打开时加载的数据
                AjaxGetData({ nowPage: 1 }, that)

                GetAllDataType(that)
            },
            methods: {
                //前一页
                prePage: function () {
                    var queryInfo = this.res[1]
                    var nowPage = queryInfo.nowPage;
                    if (nowPage - 1 <= 0) {
                        autoAlert("已是首页")
                        return
                    }
                    AjaxGetData({ nowPage: nowPage - 1 }, this)
                },

                //下一页  
                nextPage: function () {
                    var queryInfo = this.res[1]
                    var nowPage = queryInfo.nowPage;
                    //  alert(this.totalPage)
                    if (nowPage + 1 > parseInt(this.res[1].totalPage)) {
                        autoAlert("已是尾页");
                        return;
                    }
                    AjaxGetData({ nowPage: nowPage + 1 }, this)
                },
                turnTo: function () {
                    var queryInfo = this.res[1]
                    var nowPage = queryInfo.nowPage;
                    var totalPage = queryInfo.totalPage
                    var targetPage = this.$el.querySelector("#targetPage").value
                    if (isNaN(targetPage)) {
                        errAlert("清输入正确的页码")

                    }
                    else {
                        if (targetPage >= '1' && targetPage <= totalPage)
                            AjaxGetData({ nowPage: targetPage }, this)
                        else {
                            errAlert("清输入正确的页码")
                            return
                        }
                    }

                },
                //查找    
                search: function () {
                    //  alert(data)
                    AjaxGetData({ nowPage: 1 }, this)
                }
            }
        })
    </script>
</body>
</html>