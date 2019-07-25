<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowData.aspx.cs" Inherits="ShowData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/layer/layer.js"></script>
    <script src="js/MyAlert.js"></script>

    <style type="text/css">
        #content {
            margin: 10px;
        }

        body {
        }
     
        .hideOverflow {
            overflow: hidden;
        }

        #GetDataBen {
            margin-left: 10px;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            GetData()

            //$("#DeleteHistory").click(function () {
            //    layer.confirm("时候确定删除历史数据？", function () {

            //    })
            //})

            GetIP()
            //获取IP地址
            function GetIP() {
                $.ajax({
                    url: "GetIp.ashx",
                    type: "GET",
                    success: function (res) {
                        $("#ip").val(res)
                    }
                })
            }

            //滚动条在底部
            function ScrollToEnd() {
                //在页面底部添加锚点
                $("#content").append('<p id="a01"></p>')
                //跳转到锚点
                window.location.href = "#a01";
            }

            var interTime = 200
            getdataInterval = setInterval(GetData, interTime);

            //从日志文件中读取数据
            function GetData() {
                var url = "GetLog.ashx?randomPara=" + Math.random();
                //   var url = "Log/Log.txt?randomPara=" + Math.random()

                $.ajax({
                    url: url,
                    data: { lineNumber: $("#lineNumberSelect option:selected").val() },
                    type: "GET",
                    dataTYpe: "txt",
                    async: true,
                    success: function (data) { 
                        $("#content").html(data)
                        ScrollToEnd()
                    }
                })
            }

            $("#GetDataBen").click(function () {
                var name = $(this).val();

                if (name == '暂停刷新数据') {
                    $(this).val("继续刷新数据")
                    $("body").removeClass("hideOverflow");
                    $(this).removeClass('btn-danger')
                    $(this).addClass('btn-primary')
                    clearInterval(getdataInterval)

                    //其他input 可用
                    $("input").removeAttr("disabled")
                    $("select").removeAttr("disabled")
                }
                else {
                    $(this).val("暂停刷新数据")
                    $("body").addClass("hideOverflow");
                    $(this).removeClass('btn-primary')
                    $(this).addClass('btn-danger')
                    getdataInterval = setInterval(GetData, interTime)

                    $("input").attr("disabled", "disabled")
                    $("select").attr("disabled", "disabled")
                    $(this).removeAttr("disabled")

                }
            })

        })

    </script>
</head>
<body class="hideOverflow">
    <form id="form1" runat="server">
        <div id="content">
            <span style="font-weight: 600">正在加载数据</span>
        </div>

        <table style="line-height: 50px; background-color: #F9F9F9; margin-bottom: 0 !important; padding-bottom: 0 !important" id="basicQuery" class="table table-bordered table-condensed">
            <tr>

                <td style="width: 150px;">
                    <input type="button" id="GetDataBen" class="btn btn-danger" value="暂停刷新数据" />
                </td>
                <td style="width: 100px; line-height: 43px;">本机IP地址</td>
                <td style="width: 142px;">
                    <input type="text" id="ip" disabled="disabled" value="192" style="width: 140px" class="form-control" /></td>
                <td style="width: 88px;">
                    <input type="button" id="changeIp" disabled="disabled" class="btn btn-primary" value="修改" /></td>
                <td style="width: 95px; line-height: 43px;">显示的行数:</td>
                <td>
                    <select disabled="disabled" id="lineNumberSelect" class="form-control" style="width: 98px;">
                        <option value="100">100</option>
                        <option value="300">300</option>
                        <option value="500">500</option>
                    </select>
                </td>
            </tr>
        </table>


        <%--  <input type="button" id="DeleteHistory" style="margin-left: 150px;" class="btn btn-danger" value="清除历史数据" />--%>
    </form>
</body>
</html>
