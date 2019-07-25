<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettingPointInfo.aspx.cs" Inherits="SettingPointInfo" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/layer/layer.js"></script>
    <script src="js/MyAlert.js"></script>
    <script src="js/vue.js"></script>

     <style type="text/css">
         body {
             margin-top: 20px;
         }
     </style>

    <script type="text/javascript">
        $(function () {
            //添加
            $("#add").click(function () {
                var btn = $(this)
                var tbody = $("#pointTableBody");
                if (btn.val() == '添加') {
                    btn.val("保存")

                    var tr = '<tr id="new"><td> ' +
                        ' <input style="width: 100px;"  class="form-control port" required name="pointName" /> ' +
                        ' </td>  <td  >' +
                        '    <input style="width: 70px;" class="form-control xmno" required name="tdno" /> ' +
                        '  </td> ' +
                        '  <td class=""> ' +
                        '      <input class="form-control"  name="t0" required style="width: 120px;"  /> ' +
                        '  </td> ' +
                        '   <td class=""> ' +
                        '      <input class="form-control"  name="k0" required style="width: 120px;"  /> ' +
                        '  </td></tr>'
                    tbody.append(tr)
                }
                else {
                    var newLine = tbody.find("#new")
                    var pointNameInput = newLine.find("input[name='pointName']")
                    var tdnoInput = newLine.find("input[name='tdno']")
                    var t0Input = newLine.find("input[name='t0']")
                    var k0Input = newLine.find("input[name='k0']")
                    if (pointNameInput.val() == '' || tdnoInput.val() == '') {
                        errAlert('请输入正确的内容')
                        return
                    }
                    if (t0Input.val() != '' && isNaN(t0Input.val()) == true) {
                        errAlert('请输入正确的t0')
                        return
                    }
                    if (k0Input.val() != '' && isNaN(k0Input.val()) == true) {
                        errAlert('请输入正确的k0')
                        return
                    }

                    $.ajax({
                        url: "ModifyPointInfo.ashx",
                        type: "POST",
                        data: { pointName: pointNameInput.val(), tdno: tdnoInput.val(), t0: t0Input.val(), k0: k0Input.val(), type: '1' },
                        success: function (data) {
                            if (data == 'True') {
                                layer.alert("添加成功", function () {
                                    newLine.removeAttr("id")
                                    window.location = "";
                                })
                            }
                            else {
                                alert("添加失败")
                            }
                        }
                    })
                }
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="veuDev">
            <table class="table table-hover table-bordered table-striped" id="portTable">
                <caption>
                    <h4 style="text-align: center"><b>测点设置</b></h4>
                </caption>
                <thead>
                    <tr class="info">
                        <td>测点名称
                        </td>
                        <td>通道号
                        </td>
                        <td>t0
                        </td>
                        <td>k0
                        </td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="pointTableBody">
                    <tr v-for="(item, index) in res">
                        <td class="">
                            <input style="width: 100px;" readonly="true" v-model="item.pointName" class="form-control port" required name="pointName" />
                        </td>
                        <td class="">
                            <input style="width: 70px;" readonly="true" v-model="item.tdno" class="form-control xmno" required name="tdno" />
                        </td>
                        <td class="">
                            <input class="form-control" readonly="true" name="t0" required style="width: 120px;" v-model="item.t0" />
                        </td>
                        <td class="">
                            <input class="form-control" readonly="true" name="k0" required style="width: 120px;" v-model="item.k0" />
                        </td>
                        <td>
                            <input type="button" @click="change(index)" name="change" value="修改" class="btn btn-primary change" />
                            <input type="button" value="删除" class="btn btn-danger delete" />
                        </td>
                    </tr>
                </tbody>

                <tfoot>
                    <%--    <tr>
                        <td>本机IP地址</td>
                        <td><input class="form-control" /></td>
                    </tr>--%>
                    <tr>
                        <td colspan="5">
                            <input style="margin-left: 20px;" value="添加" id="add" type="button" class="btn btn-primary" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </form>

    <script type="text/javascript">
        Vue.config.devtools = true;
        var vm = new Vue({
            el: "#veuDev",
            data: {
                res: ""
            },
            created: function () {

                var that = this
                $.ajax({
                    url: "GetAllPoint.ashx",
                    type: "GET",
                    success: function (data) {
                        that.res = JSON.parse(data)

                        $("input").click(function () {
                            alert('a')
                        })
                    }
                })
            },
            methods: {
                //修改
                change: function (index) {
                    var tr = $("#pointTableBody").find("tr")[index];
                    var pointNameInput = $(tr).find("input[name='pointName']")
                    var tdnoInput = $(tr).find("input[name='tdno']")
                    var t0Input = $(tr).find("input[name='t0']")
                    var k0Input = $(tr).find("input[name='k0']")
                    var btn = $(tr).find("input[name='change']")

                    if (btn.val() == "修改") {
                        btn.val("确认修改")
                        tdnoInput.removeAttr("readonly")
                        t0Input.removeAttr("readonly")
                        k0Input.removeAttr("readonly")

                    }
                    else {
                        btn.val("确认修改")
                        if (tdnoInput.val() == '') {
                            errAlert('请输入正确的内容')
                            return
                        }
                        if (t0Input.val() != '' && isNaN(t0Input.val()) == true) {
                            errAlert('请输入正确的t0')
                            return
                        }
                        if (k0Input.val() != '' && isNaN(k0Input.val()) == true) {
                            errAlert('请输入正确的k0')
                            return
                        }

                        $.ajax({
                            url: "ModifyPointInfo.ashx",
                            type: "POST",
                            data: { pointName: pointNameInput.val(), tdno: tdnoInput.val(), t0: t0Input.val(), k0: k0Input.val(), type: '2' },
                            success: function (data) {
                                if (data == 'True') {
                                    SuAlert("修改成功")
                                    tdnoInput.attr("readonly", "readonly")
                                    t0Input.attr("readonly", "readonly")
                                    k0Input.attr("readonly", "readonly")
                                    btn.val("修改")
                                }
                                else {
                                    errAlert("修改失败")
                                }
                            }
                        })
                    }
                }
            }
        })
    </script>
</body>
</html>
