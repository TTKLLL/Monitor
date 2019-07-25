<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettingXmPort.aspx.cs" Inherits="SettingXmPort" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/layer/layer.js"></script>
    <script src="js/MyAlert.js"></script>

    <script src="js/vue.js"></script>

    <title></title>

    <style type="text/css">
        input {
            font-weight: 600;
            font-size: 14px;
            text-align: center;
        }

        #sortTab tbody tr td {
            line-height: 20px !important;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            testPort()

            //刷新端口状态
            $("#updateState").click(function () {
                testPort()
            })

            //获取监听的ip地址
            function GetIP() {
                $.ajax({
                    url: "GetIp.ashx",
                    type: "GET",
                    success: function (res) {
                        $("#ip").val(res)
                    }
                })
            }

            //监测端口状态
            function testPort() {
                index = layer.load(0, { shade: false });
                var inputs = $("body").find("input")
                inputs.each(function () {
                    $(this).attr("disabled", "disabled")
                })

                array = new Array()
                var checkboxs = $("#portTable").find("input:checkbox");
                checkboxs.each(function () {
                    port = $(this).parent().parent().find("input[name='port']")
                    array.push(port.val())
                })

                $.ajax({
                    url: "ChargePort.ashx?" + Math.random(),
                    type: "POST",
                    async: true,
                    data: { port: array.join(',') },
                    success: function (data) {
                        // alert(data)
                        layer.close(index)
                        resArray = data.split(",")
                        i = 0;
                        checkboxs.each(function () {

                            if (resArray[i] == '1')
                                $(this).attr("checked", "checked")
                            else
                                $(this).removeAttr("checked")
                            i++;
                        })
                        inputs.each(function () {
                            $(this).removeAttr("disabled")
                        })
                    }
                })
            }

            //修改
            $(".change").click(function () {
                var name = $(this).val();
                var portInput = $(this).parent().parent().find("input[name='port']")
              //  var xmnoInput = $(this).parent().parent().find("input[name='xmno']")
                var dataTypeInput = $(this).parent().parent().find("input[name='dataType']")

                var oldData = dataTypeInput.val()

                if (name == '修改') {
                    dataTypeInput.removeAttr("readonly")

                    //alert(portInput.val())
                    $(this).val("确定修改")
                }
                else {
                    if (dataTypeInput.val() == '') {
                        infoAlert("数据类型不能为空");
                        return
                    }
                    $(this).val("修改");
                    dataTypeInput.attr("readonly", "true")
                    dataTypeInput.attr("readonly", "true")
                    layer.confirm("确认修改？", function () {
                        $.ajax({
                            url: "UpdateXmPort.ashx",
                            data: { xmno: xmnoInput.val(), port: portInput.val(), dataType: dataTypeInput.val() },
                            type: "POST",
                            success: function (data) {

                                if (data == 'False') {
                                    dataTypeInput.val(oldData)
                                    errAlert("修改失败");
                                }
                                else
                                    autoAlert("修改成功");
                            }
                        })
                    })
                    dataTypeInput.attr("readonly", "true");

                }
            })

            //删除
            $(".delete").click(function () {
                var deleteBtn = $(this)

                layer.confirm("确认删除?", function () {
                    var portInput = deleteBtn.parent().parent().find("input[name='port']")
                    var deleteRow = deleteBtn.parent().parent();
                    deleteRow.attr("class", "deleteRow")

                    $.ajax({
                        url: "DeleteXmPort.ashx",
                        type: "POST",
                        data: { port: portInput.val() },
                        success: function (data) {
                            if (data == 'True') {
                                // $("#portTableBody").remove(".deleteRow");
                                window.location.href = ""
                                autoAlert('删除成功')
                            }
                            else
                                errAlert("删除失败")
                        }
                    })
                })
            })

            //添加
            $("#add").click(function () {
                var name = $(this).val()
                if (name == '添加') {
                    $(this).val('保存')

                    var text = '<tr id="newRow">' +
                        '<td >' +
                        '   <input style="width: 70px;"class="form-control port" placeholder="端口号"  required name="port" value="" /> ' +
                        ' </td> </tr>';

                    //+
                    //' <td class=""> ' +
                    //'     <input style="width: 70px;"  class="form-control xmno" placeholder="项目编号" required name="xmno" value="" /> ' +
                    //'  </td> ' +
                    //'  <td class=""> ' +
                    //'      <input class="form-control"  name="dataType" placeholder="数据类型" required style="width: 120px;" value="" /> ' +
                    //'  </td> ' +
                    //'  <td> ' +

                    //'  </td> ' +
                    //' </tr>';
                    $("#portTableBody").append(text);

                }
                else {
                    var newRow = $("#portTableBody").find("#newRow")
                    var portInput = newRow.find("input[name='port']")
                     var xmnoInput = newRow.find("input[name='xmno']")
                    //  var dataTypeInput = newRow.find("input[name='dataType']")

                    //  if (portInput.val() == '' || xmnoInput == '' || dataTypeInput == '') {
                    if (portInput.val() == '') {
                        infoAlert("添加的数据本能为空")
                        return
                    }
                    $.ajax({
                        url: 'AddXmPort.ashx',
                        data: { xmno: xmnoInput.val(), port: portInput.val(), dataType: "" },
                        type: "POST",
                        success: function (data) {
                            if (data == 'True') {
                                layer.alert("添加成功", function () {
                                    newRow.removeAttr("id")
                                    window.location = "";
                                })
                            }
                            else {
                                errAlert("添加失败")
                            }
                        }
                    })
                }
            })

            //修改端口状态
            $(".state").click(function () {
                //目标状态
                var state = $(this).attr("checked") == undefined ? 0 : 1
                var checkBox = $(this)

                var portInput = $(this).parent().parent().find("input[name='port']")
                $.ajax({
                    url: "PortManage.ashx",
                    type: "POST",
                    data: { port: portInput.val(), state: state },
                    success: function (data) {
                        if (data == 'True') {

                            autoAlert("成功开启" + portInput.val() + "端口")
                        }
                        else {
                            errAlert(data)
                            if (state == 1)
                                checkBox.removeAttr("checked")
                            else
                                checkBox.attr("checked", "checked")
                        }
                    }
                })
            })
        })
    </script>

</head>
<body>
    <form id="form1" action="SettingPoint.aspx" method="post">
        <div>
            <table class="table table-hover table-bordered table-striped" id="portTable">
                <caption>
                    <h4 style="text-align: center"><b>项目端口设置</b></h4>
                </caption>
                <thead>
                    <tr class="info">
                        <td>端口
                        </td>
                        <%--    <td>项目编号
                        </td>
                        <td>数据类型
                        </td>--%>
                        <td>端口状态</td>
                        <td>操作</td>
                    </tr>
                </thead>
                <tbody id="portTableBody">
                    <%foreach (var item in xmPorts)
                        {%>
                    <tr>
                        <td class="">
                            <input style="width: 70px;" readonly="true" class="form-control port" required name="port" value='<%= item.port %>' />
                        </td>
                        <%-- <td class="">
                            <input style="width: 70px;" readonly="true" class="form-control xmno" required name="xmno" value='<%= item.xmno %>' />
                        </td>
                        <td class="">
                            <input class="form-control" readonly="true" name="dataType" required style="width: 120px;" value='<%= item.dataType %>' />
                        </td>--%>
                        <td>
                            <input type="checkbox" class="checkbox state" name="state" />
                        </td>
                        <td>
                            <input type="button" value="修改" class="btn btn-primary change" />
                            <input type="button" value="删除" class="btn btn-danger delete" />
                        </td>

                    </tr>

                    <% } %>>
                </tbody>

                <tfoot>
                    <%--    <tr>
                        <td>本机IP地址</td>
                        <td><input class="form-control" /></td>
                    </tr>--%>
                    <tr>
                        <td colspan="5">
                            <input style="margin-left: 20px;" value="添加" id="add" type="button" class="btn btn-primary" />
                            <input style="margin-left: 20px;" value="刷新端口状态" id="updateState" type="button" class="btn btn-primary" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </form>

    <script type="text/javascript">

</script>
</body>
</html>
