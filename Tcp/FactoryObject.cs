using Model;
using System;
using Tool;

namespace Tcp
{
    /// <summary>
    /// 根据端口号返回指定类型的数据处理对象
    /// </summary>
    public class FactoryObject
    {
        static DataProcessCommon common = new DataProcessCommon();

        //根据端口号确定传感器以及数据类型
        public static IDataProcess GetDataProcess(int port, out string dataType, out int xmno)
        {
            try
            {
                IDataProcess dataProcess = null;
                DataProcessCommon dataProcessCommon = new DataProcessCommon();

                //根据端口号获取项目编号
                //string xmno = dataProcessCommon.GetXmno(port);
                //根据端口号判断数据类型
                dataType = null;
                xmno = 0;
                dataProcessCommon.GetDataType(port, out dataType, out xmno);
                Console.WriteLine("收到来自端口{0}的{1}数据", port.ToString(), dataType);

                string info = string.Format("收到来自端口{0}的{1}数据", port.ToString(), dataType);
                FileOperation.WriteAppenFile(info);

                switch (dataType)
                {
                    //震弦式
                    case "应变":
                    case "土压":
                    case "锚索":
                    case "钢筋":
                    case "轴力":
                    case "水压":
                        dataProcess = new DataProcessZX(); break;

                        //485式

                        //    dataProcess = new DataProcess485(); break;
                }
                return dataProcess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //
        //根据通道编号获取传感器信息
        public static IDataProcess GetDataProcess(string data, out string dataType)
        {
            try
            {
                string tdno = common.GetTdno(data);
                Device device = common.GetDeviceByTdno(tdno);
                dataType = device.type;

                IDataProcess dataProcess = null;
                switch (device.type)
                {
                    //震弦式
                    case "轴力-单弦":
                    case "土压":
                    case "轴力-三弦":
                    case "锚索-三弦":
                    case "表面应变":
                    case "钢筋":
                    case "渗压":
                        dataProcess = new DataProcessZX(); break;

                        //485式
                }
                return dataProcess;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("判断传感器类型失败，" + ex.Message);
                throw ex;
            }
        }
    }
}
