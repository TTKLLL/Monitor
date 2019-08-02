using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using Tool;

namespace DisplayBLL
{
    /// <summary>
    /// 关于传感器监测数据的查询
    /// </summary>
    public class DataBLL
    {
        public List<DataVM> GetDataModels()
        {
            try
            {
                string sql = @"select sno, time, data.tdno, valueOne, valueTwo, valueThree, port, data.pointName, dataType, xmno, mkno " +
                                " from data, td " +
                                " where data.sno = td.deviceId and data.tdno = td.tdno " +
                                " order by  time desc";

                DataTable dt = SqlHelper.GetTable(sql);
                if (dt.Rows.Count == 0)
                    return null;
                List<DataVM> models = new List<DataVM>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataVM model = new DataVM()
                    {
                        number = i + 1,
                        deviceId = dt.Rows[i]["sno"].ToString(),
                        time = DateTime.Parse(dt.Rows[i]["time"].ToString()),
                        tdno = dt.Rows[i]["tdno"].ToString(),
                        valueOne = double.Parse(dt.Rows[i]["valueOne"].ToString()),
                        port = dt.Rows[i]["port"].ToString(),
                        pointName = dt.Rows[i]["pointName"].ToString(),
                        dataType = dt.Rows[i]["dataType"].ToString(),
                        xmno = int.Parse(dt.Rows[i]["xmno"].ToString()),
                        mkno = dt.Rows[i]["mkno"].ToString(),
                    };
                    models.Add(model);
                }

                return models;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("查询监测数据出错 " + ex.Message);
                throw ex;
            }
        }
    }
}
