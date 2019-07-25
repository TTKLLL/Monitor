using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Tcp.CalculateModel;
using Tool;

namespace Tcp
{

    //在处理接收数据时要用的的公共方法
    public class DataProcessCommon
    {
        public DataProcessCommon()
        {
            //给TcpServer额事件添加行为
            TcpServer.ProcessDataEvent += new TcpServer.ProcessData(ProcessData);
        }

        //根据端口号获取项目编号
        public string GetXmno(int port)
        {
            return null;
        }

        //根据端口号获取数据类型和项目编号
        public bool GetDataType(int port, out string dataType, out int xmno)
        {
            dataType = null;
            xmno = 0;

            string sql = string.Format("select dataType, xmno from xmport where port = {0}", port);

            DataTable dt = SqlHelper.GetTable(sql);
            try
            {
                dataType = dt.Rows[0]["dataType"].ToString();
                xmno = int.Parse(dt.Rows[0]["xmno"].ToString());
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //根据设备编号+通道号得到点名
        public string GetPointName(string sno, string tdno)
        {
            string sql = string.Format(@"select pointname from point, pointInfo
where id = pointid and sno = '{0}' and point.tdno = '{1}'", sno, tdno);

            DataTable dt = SqlHelper.GetTable(sql);
            try
            {
                return dt.Rows[0]["pointname"].ToString();
            }
            catch (Exception)
            {
                return null;
            }

        }

        //获取指定测点的t0
        public double GetT0(string pointName)
        {
            string sql = string.Format(@"select t0 from pointinfo where pointName = '{0}'", pointName);
            DataTable dt = SqlHelper.GetTable(sql);
            try
            {
                return double.Parse(dt.Rows[0]["t0"].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //根据传感器类型获取计算公式
        public Func<Model, double> GetFunc(string dataType)
        {
            Func<Model, double> calcuFunc;
            switch (dataType)
            {
                case "表面应变":
                    calcuFunc = new YingBian().Calculate; break;
                case "土压":
                    calcuFunc = new TuYa().Calculate; break;
                case "轴力-单弦":
                case "轴力-三弦":
                    calcuFunc = new ZhouLi().Calculate; break;
                case "锚索-三弦":
                    calcuFunc = new MaoSuo().Calculate; break;
                case "水压":
                    calcuFunc = new ShuiYa().Calculate; break;
                case "钢筋":
                    calcuFunc = new GangJin().Calculate; break;
                case "渗压":
                    calcuFunc = new ShenYa().Calculate; break;
                default: calcuFunc = null; break;
            }

            if (calcuFunc == null)
            {
                FileOperation.WriteAppenFile("未找到" + dataType + "类型数据的计算公式");
                return null;
            }
            return calcuFunc;
        }

        //根据不同的传感器类型计算数据
        public List<Model> Calculate(List<Model> models, string dataType)
        {
            try
            {
                //指向不同计算公式的委托
                Func<Model, double> calcuFunc = GetFunc(dataType);
                foreach (var item in models)
                    item.res = calcuFunc(item);

                return models;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("计算{0}类型的数据出错，{1}", dataType, ex.Message));
                return null;
            }

        }

        //计算数据
        public double Calculate(Model model)
        {
            try
            {
                //指向不同计算公式的委托
                Func<Model, double> calcuFunc = GetFunc(model.dataType);
                return calcuFunc(model);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("计算{0}类型的数据出错，{1}", model.dataType, ex.Message));
                throw ex;
            }
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="port"></param>
        public void ProcessData(string data, int port)
        {
            // string dataType;
            // int xmno;
            //根据端口判断收到的数据类型
            // IDataProcess process = FactoryObject.GetDataProcess(int.Parse(port), out dataType, out xmno);

            //根据通道号判断传感器类型
            //IDataProcess process = FactoryObject.GetDataProcess(data, out dataType);

            IDataProcess process = new DataProcessZX();
            process.AnalysisData(data, port.ToString(), null);
        }

        public void ShowDataInfo(List<Model> models)
        {
            foreach (var item in models)
            {
                string info = string.Format("添加项目编号:{0} 点名:{1} 采集时间为{2}的{3}数据", item.xmno, item.pointName, item.time, item.dataType);
                FileOperation.WriteAppenFile(info);
            }
        }

        //根据通道编号获取设备
        public Device GetDeviceByTdno(string tdno)
        {
            try
            {
                string otehrTdno = tdno;
                //去除通道编号前面的0
                while (tdno.Length > 1 && tdno.StartsWith("0"))
                    tdno = tdno.Substring(1);

                string sql = string.Format(" select distinct device.deviceId, type from device, td where  device.deviceId = td.deviceId and ( tdno = '{0}' or tdno = '{1}') ", tdno, otehrTdno);
                DataTable dt = SqlHelper.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    FileOperation.WriteAppenFile(string.Format("根据通道号{0}获取设备信息出错}", tdno));
                    return null;
                }

                Device model = new Device()
                {
                    deviceId = dt.Rows[0]["deviceId"].ToString(),
                    type = dt.Rows[0]["type"].ToString()
                };
                return model;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("根据通道号{0}获取设备信息出错, {1}", tdno, ex.Message));
                throw ex;
            }
        }

        //从接收的数据中提取通道号
        public string GetTdno(string data)
        {
            try
            {
                string pattern = "td\\w+";
                string tdno = Regex.Match(data, pattern).Value;
                if (tdno == null || tdno == "")
                {
                    FileOperation.WriteAppenFile("提取通道号失败");
                    return null;
                }
                return tdno.Substring(2);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("提取通道号出错" + ex.Message);
                return null;
            }
        }
    }

}


