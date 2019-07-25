<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="left" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>传感器接入测试工具</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template">
    <meta name="author" content="GeeksLabs">
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal">
    <link rel="shortcut icon" href="img/favicon.png">
    <script src="js/jquery-v2.1.1.min.js"></script>
    <!-- Bootstrap CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <!-- bootstrap theme -->
    <link href="css/bootstrap-theme.css" rel="stylesheet">
    <!--external css-->
    <!-- font icon -->
    <link href="css/elegant-icons-style.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <!-- Custom styles -->
    <link href="css/style.css" rel="stylesheet">
    <link href="css/style-responsive.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 -->
    <!--[if lt IE 9]>
      <script src="js/html5shiv.js"></script>
      <script src="js/respond.min.js"></script>
      <script src="js/lte-ie7.js"></script>
    <![endif]-->
    <style type="text/css">
        body {
            overflow: hidden;
        }
    </style>
    <script type="text/javascript">

        //方法一
        function changeFrameHeight() {
            var ifm = document.getElementById("mainiframe");
            ifm.height = document.documentElement.clientHeight - 56;
        }
        window.onresize = function () { changeFrameHeight(); }
        $(function () { changeFrameHeight(); });


        function switchSysBar() {
            var ssrc = document.all("frmTitle").style.display;
            if (ssrc == "none") {
                document.all("frmTitle").style.display = "";
            }
            else {
                document.all("frmTitle").style.display = "none"
            }
        }

        $(function () {
            $(".changeExamType").click(function () {
                var id = $(this).siblings("span").html();
                window.location.href = "?examTypeId=" + id;
            })
        })

    </script>
    <style type="text/css">
        .sidebar-menu ul {
            width: 220px;
            padding-left: 15px;
        }

        .sidebar-menu li {
            height: 40px;
            padding-bottom: 5px;
            margin-top: 0;
        }

            .sidebar-menu li a {
                height: 40px;
                line-height: 40px;
                padding-top: 0 !important;
                padding-bottom: 0 !important;
                padding-left: 30px !important;
            }

                .sidebar-menu li a span {
                    line-height: 15px;
                }

        .myShow {
            display: block;
        }

        #sidebar {
            width: 220px;
        }

        .reverse {
            /*切换按钮*/
            width: 230px;
            bottom: 10px;
            margin-left: 30px;
            margin-top: 15px;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            //设置滚动条始终在最底部
            //    alert($('#mainiframe')[0].scrollHeight)
            $('#mainiframe').scrollTop($('#mainiframe')[0].scrollHeight);
            $('#mainiframe').scrollTop(592)
            //展开下拉列表
            $(".openDrop").click(function () {

                $("#dropDown").toggleClass("myShow")
            })

            //点击列表中的一行
            $("#dropDown li").click(function () {
                $("#dropDown").toggleClass("myShow")
            })

            //退出
            $(".exit").click(function () {
                window.opener = null;
                window.open('', '_self');
                window.close();
            })



            var newWith = parseInt($("#myFram").width()) + 20;
            $(this).css("width", newWith + 'px')
        })
    </script>
</head>
<body>
    <section id="container" class="">
        <!--header start-->
        <header class="header dark-bg">
            <div class="toggle-nav">
                <div class="icon-reorder tooltips" data-original-title="Toggle Navigation" data-placement="bottom"></div>
            </div>

            <!--logo start-->
            <a style="font-size: 26px;" href="#" class="logo">传感器接入测试</a>
            <!--logo end-->



            <div class="top-nav notification-row">
            </div>
        </header>
        <!--header end-->

        <!--sidebar start-->
        <aside>
            <div id="sidebar" class="nav-collapse ">
                <!-- sidebar menu start-->

                <ul class="sidebar-menu">
                    <li>
                        <a class="" target="main" href="ShowData.aspx">
                            <i class="glyphicon glyphicon-check"></i>
                            <span><b>正在接收的信息</b></span>
                        </a>
                    </li>
                    <li>
                        <a class="" target="main" href="SettingXmPort.aspx">
                            <i class="glyphicon glyphicon-plus-sign"></i>
                            <span><b>项目端口设置</b></span>
                        </a>
                    </li>
                    <li>
                        <a class="" target="main" href="SearchData.aspx">
                            <i class="glyphicon glyphicon-inbox"></i>
                            <span><b>已收到的数据</b></span>
                        </a>
                    </li>
                    <li>
                        <a class="" target="main" href="SettingPointInfo.aspx">
                            <i class="glyphicon glyphicon-list-alt"></i>
                            <span><b>测点信息设置</b></span>
                        </a>
                    </li>
                    <li>
                        <a class="" target="main" href="SettingPointInfo.aspx">
                            <i class="glyphicon glyphicon-list-alt"></i>
                            <span><b>传感器信息管理</b></span>
                        </a>
                    </li>
                    <li>
                        <a class="" target="main" href="SettingPointInfo.aspx">
                            <i class="glyphicon glyphicon-list-alt"></i>
                            <span><b>模块信息管理</b></span>
                        </a>
                    </li>
                </ul>
                <!-- sidebar menu end-->
            </div>

        </aside>
        <!--sidebar end-->

        <section id="main-content">
            <section id="myFram" class="wrapper" style="margin: 0; margin-top: 62px; margin-left: 40px; padding: 0;">
                <iframe src="ShowData.aspx" name="main" id="mainiframe" width="97%" height="100%" src="ShowData.aspx" frameborder="0" scrolling="auto"></iframe>
            </section>
        </section>
    </section>


    <script src="js/jquery.js"></script>
    <script src="js/jquery-ui-1.10.4.min.js"></script>
    <script src="js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>
    <!-- bootstrap -->
    <%--    <script src="js/bootstrap.min.js"></script>--%>
    <!-- nice scroll -->
    <script src="js/jquery.scrollTo.min.js"></script>
    <script src="js/jquery.nicescroll.js" type="text/javascript"></script>
    <!-- charts scripts -->
    <script src="assets/jquery-knob/js/jquery.knob.js"></script>
    <script src="js/jquery.sparkline.js" type="text/javascript"></script>
    <script src="assets/jquery-easy-pie-chart/jquery.easy-pie-chart.js"></script>
    <script src="js/owl.carousel.js"></script>
    <!-- jQuery full calendar -->
    <<script src="js/fullcalendar.min.js"></script>
    <!-- Full Google Calendar - Calendar -->
    <script src="assets/fullcalendar/fullcalendar/fullcalendar.js"></script>
    <!--script for this page only-->
    <script src="js/calendar-custom.js"></script>
    <script src="js/jquery.rateit.min.js"></script>
    <!-- custom select -->
    <script src="js/jquery.customSelect.min.js"></script>
    <script src="assets/chart-master/Chart.js"></script>
    <!--custome script for all page-->
    <script src="js/scripts.js"></script>
    <!-- custom script for this page-->
    <script src="js/sparkline-chart.js"></script>
    <script src="js/easy-pie-chart.js"></script>
    <script src="js/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="js/jquery-jvectormap-world-mill-en.js"></script>
    <script src="js/xcharts.min.js"></script>
    <script src="js/jquery.autosize.min.js"></script>
    <script src="js/jquery.placeholder.min.js"></script>
    <script src="js/gdp-data.js"></script>
    <script src="js/morris.min.js"></script>
    <script src="js/sparklines.js"></script>
    <script src="js/charts.js"></script>
    <script src="js/jquery.slimscroll.min.js"></script>
    <script>

        //knob
        $(function () {
            $(".knob").knob({
                'draw': function () {
                    $(this.i).val(this.cv + '%')
                }
            })
        });

        //carousel
        $(document).ready(function () {
            $("#owl-slider").owlCarousel({
                navigation: true,
                slideSpeed: 300,
                paginationSpeed: 400,
                singleItem: true

            });
        });

        //custom select box

        $(function () {
            $('select.styled').customSelect();
        });

        /* ---------- Map ---------- */
        $(function () {
            $('#map').vectorMap({
                map: 'world_mill_en',
                series: {
                    regions: [{
                        values: gdpData,
                        scale: ['#000', '#000'],
                        normalizeFunction: 'polynomial'
                    }]
                },
                backgroundColor: '#eef3f7',
                onLabelShow: function (e, el, code) {
                    el.html(el.html() + ' (GDP - ' + gdpData[code] + ')');
                }
            });
        });
    </script>
</body>
</html>
