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
    public class DataProcess485 : IDataProcess
    {
        static DataProcessCommon dataProcessCommon = new DataProcessCommon();

        //解析通过Tcp获取的数据
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

                string pattern = @"\w+:\s+\w+\s+\d{4}/\d{2}/\d{2}\s+\d{2}:\d{2}:\d{2}\s+\w+\s+((\d+.\d+\s+){1,2})";

                MatchCollection match2 = Regex.Matches(data, pattern);
                if (match2.Count == 0)
                {
                    string info = "未提取有效数据";
                    Console.WriteLine(info);
                    FileOperation.WriteAppenFile(info);
                    return false;
                }
                #endregion

                #region 遍历每条数据 并把数据存入List
                foreach (Match item in match2)
                {
                    Match match1 = Regex.Match(item.Value, @"\w+:\s");
                    MatchCollection collection = Regex.Matches(item.Value, @"\d+\.\d");
                    string[] fields = Regex.Split(item.Value, @"\s+");

                    Model model = new Model()
                    {
                        xmno = xmno,
                        sno = fields[0].Substring(0, fields[0].Length - 1),
                        cycle = fields[1],
                        time = DateTime.Parse(fields[2] + " " + fields[3]),
                        tdno = fields[4],
                        valueOne = Double.Parse(fields[5]),
                        dy = Double.Parse(dy),
                        port = port,
                        dataType = dataType
                    };

                    //传递过来的有value1和value2
                    if (fields.Length > 6)
                    {
                        if (fields[6] == "")
                            model.valueTwo = 0;
                        else
                            model.valueTwo = Double.Parse(fields[6]);
                    }
                    else
                        model.valueTwo = 0;

                    //将受到的数据保存到文本
                    FileOperation.WriteAppenFile("../../data.txt", model.ToString());
                    models.Add(model);
                }
                #endregion

                #region  根据公式计算得到结果
                List<Model> resModels = dataProcessCommon.Calculate(models, dataType);
                if (resModels == null)
                {
                    string info = string.Format("计算来自端口{0}的{1}数据失败", port, dataType);
                    FileOperation.WriteAppenFile(info);
                    return false;
                }
                #endregion

                #region 将计算结果保存到数据库
                if (SaveData(resModels, 0))
                {
                    dataProcessCommon.ShowDataInfo(resModels);
                    return true;
                }
                else
                    return false;
                #endregion
            }
            catch (Exception ex)
            {
                string info = string.Format("保存数据失败, {0}", ex.Message);
                FileOperation.WriteAppenFile(info);
                return false;
            }
        }



        //将数据保存到数据库
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
                    //获取点名
                  //  item.pointName = common.GetPointName(item.sno, item.tdno);
                    string sql = string.Format("insert into data(sno, cycle, time, tdno, res, pointName, dy, port, dataType) values('{0}', '{1}', '{2}', '{3}', {4}, '{5}', {6}, {7}, '{8}')",
                        item.sno, item.cycle, item.time, item.tdno, item.res, item.pointName, item.dy, item.port, item.dataType);
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
                string info = string.Format("数据保存的数据库失败, {0}", ex.Message);
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
