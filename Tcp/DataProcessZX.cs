using DAL;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text.RegularExpressions;
using Tool;

namespace Tcp
{
    /// <summary>
    /// 处理传感器发送的数据
    /// </summary>
    public class DataProcessZX : IDataProcess
    {
        static DataProcessCommon dataProcessCommon = new DataProcessCommon();

        //从字符串中提取有效数据
        public bool AnalysisData(string data, string port, string dataType, int xmno = 0)
        {
            List<Model> models = new List<Model>();
            try
            {
                #region 根据协议解析数据
                string tmp = Regex.Match(data, @"DY=\d+.\d+V").Value;
                int startIndex = tmp.IndexOf('=') + 1;
                int endIndex = tmp.Length - 2;
                string dy = tmp.Substring(startIndex, endIndex - startIndex + 1);


                string pattern2 = @"\w+:\s+(\w+\s+){2}\d{4}/\d{2}/\d{2}\s+\d{2}:\d{2}:\d{2}\s+\w+\s+\d+.\d+\s+((\d+.\d+)|\d+)\s*";
                MatchCollection match2 = Regex.Matches(data, pattern2);
                if (match2.Count == 0)
                {
                    string info = "未能提取有效数据";
                    FileOperation.WriteAppenFile(info);
                    return false;
                }
                else
                {
                    string info = string.Format("收到{0}条有效数据", match2.Count);
                    FileOperation.WriteAppenFile(info);
                }
                #endregion

                #region 遍历每条数据 将原始数据写入到文本中
                foreach (Match item in match2)
                {
                    Match match1 = Regex.Match(item.Value, @"\w+:\s");
                    MatchCollection collection = Regex.Matches(item.Value, @"\d+\.\d");
                    string[] fields = Regex.Split(item.Value, @"\s+");

                    Model model = new Model()
                    {
                        xmno = xmno,
                        sno = fields[0].Substring(0, fields[0].Length - 1),
                        number = fields[1],
                        cycle = fields[2],
                        time = DateTime.Parse(fields[3] + " " + fields[4]),
                        tdno = fields[5].Substring(2),
                        valueOne = Double.Parse(fields[6]),
                        dy = Double.Parse(dy),
                        port = port,
                        valueTwo = Double.Parse(fields[7])
                    };
                    //将收到的数据保存到文本
                    FileOperation.WriteReceiveData(model.ToString());

                    models.Add(model);
                }
                #endregion

                #region  添加其他信息并根据公式计算得到结果
                // List<Model> resModels = dataProcessCommon.Calculate(models);
                foreach (var item in models)
                {
                    //获取该条数据所属的传感器类型
                    item.dataType = dataProcessCommon.GetDeviceByTdno(item.tdno).type;
                    //计算数据
                    item.res = dataProcessCommon.Calculate(item);

                    //获取点名
                    //model.pointName = dataProcessCommon.GetPointName(model.sno, model.tdno);
                }
                #endregion

                #region 将计算结果保存到数据库
                if (SaveData(models, 0))
                {
                    string info = string.Format("成功保存来自端口{0}的数据", port);
                    FileOperation.WriteAppenFile(info);
                    FileOperation.WriteAppenFile(info);
                    dataProcessCommon.ShowDataInfo(models);
                    return true;
                }
                else
                    return false;
                #endregion
            }
            catch (Exception ex)
            {
                string info = string.Format("保存数据失败，" + ex.Message);
                FileOperation.WriteAppenFile(info);
                return false;
            }
        }

        //将数据保持到数据库
        public bool SaveData(List<Model> models, int xmno)
        {
            DataProcessCommon common = new DataProcessCommon();
            OdbcConnection conn = DbContext.GetConn(xmno);
            conn.Open();
            OdbcTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (var item in models)
                {
                    string sql = string.Format("insert into data(sno, cycle, time, tdno, res, pointName, dy, port, number, dataType) values('{0}', '{1}', '{2}', '{3}', {4}, '{5}', {6}, {7}, {8}, '{9}')",
                        item.sno, item.cycle, item.time, item.tdno, item.res, item.pointName, item.dy, item.port, item.number, item.dataType);
                    OdbcCommand comm = new OdbcCommand(sql, conn);
                    comm.Transaction = trans;
                    if (comm.ExecuteNonQuery() <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                }

                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string info = string.Format("数据保存的数据库失败，" + ex.Message);
                FileOperation.WriteAppenFile(info);
                FileOperation.WriteAppenFile(info);
                trans.Rollback();
                return false;
            }
            finally
            {
                trans.Dispose();
               conn.Close();
            }
        }


    }
}
