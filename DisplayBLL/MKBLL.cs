using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using Tool;

namespace DisplayBLL
{
    /// <summary>
    /// 通信模块BLL
    /// </summary>
    public class MKBLL
    {
        //获取所有的模块信息
        public List<ModuleModel> GetModuleModels()
        {
            try
            {
                string sql = "SELECT * FROM `module` ORDER BY mkno";
                DataTable dt = SqlHelper.GetTable(sql);
                List<ModuleModel> models = new List<ModuleModel>();
                if (dt.Rows.Count == 0)
                    return null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ModuleModel model = new ModuleModel()
                    {
                        mkno = dt.Rows[i]["mkno"].ToString(),
                        port = dt.Rows[i]["port"].ToString()
                    };
                    models.Add(model);
                }
                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //判断通信模块是否已存在
        public bool IsHaveModule(string mkno)
        {
            try
            {
                string sql = string.Format("select mkno from module where mkno = '{0}' ", mkno);
                return SqlHelper.GetTable(sql).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //添加通信模块
        public bool AddModule(ModuleModel model)
        {
            try
            {
                string sql = string.Format("insert into module values( '{0}', '{1}' )", model.mkno, model.port);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //修改模块的端口号
        public bool UpdateModule(ModuleModel model)
        {
            try
            {
                string sql = string.Format("update module set port = {0} where mkno = '{1}' ", model.port, model.mkno);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //解析DataRow中的数据
        public string GetDataFromDt(object data)
        {
            return data is DBNull ? "" : data.ToString();
        }

        //获取某个模块的通道信息
        public List<TDVM> GetTds(string mkno)
        {
            try
            {
                List<TDVM> models = new List<TDVM>();
                string sql = string.Format("select tdno, device.deviceId, pointName, type from td left join device on " +
                    " td.deviceId = device.deviceId where mkno = '{0}' order by tdno  ", mkno);
                DataTable dt = SqlHelper.GetTable(sql);

                if (dt.Rows.Count == 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    TDVM model = new TDVM()
                    {
                        tdno = GetDataFromDt(row["tdno"]),
                        type = GetDataFromDt(row["type"]),
                        deviceId = GetDataFromDt(row["deviceId"]),
                        pointName = GetDataFromDt(row["pointName"])
                    };
                    models.Add(model);
                }
                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 判断某个通信模块的通道号是否已用
        public bool TdIsUsed(string mkno, string tdno)
        {
            try
            {
                string sql = string.Format("select tdno from td  where mkno = '{0}' and tdno = '{1}'",
                   mkno, tdno);
                return SqlHelper.GetTable(sql).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //给模块添加通道信息
        public bool AddTd(TD model)
        {
            try
            {
                string sql = string.Format("insert into td(tdno, mkno, deviceid, pointName) values('{0}', '{1}', '{2}', '{3}')",
                    model.tdno, model.mkno, model.deviceId, model.pointName);
                return SqlHelper.ExecuteNoQuery(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //更新模块信息
        public bool UpdateTd(TD model)
        {
            try
            {
                string sql = string.Format("update td set deviceId = '{0}', pointName = '{1}' where mkno = '{2}' and tdno = '{3}'",
                  model.deviceId, model.pointName, model.mkno, model.tdno);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("更新模块编号为{0} 通道号为{1} 的通道信息失败", model.mkno, model.tdno));
                throw ex;
            }
        }

        //通过模块号得到端口号
        public string GetPortBymkno(string mkno)
        {
            try
            {
                string port;
                string sql = string.Format("select port from module where mkno = '{0}' ", mkno);
                DataTable dt = SqlHelper.GetTable(sql);
                if (dt.Rows.Count == 0)
                    port = "";
                else
                    port = dt.Rows[0]["port"].ToString();
                return port;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取通信模块" + mkno + "的端口信息出错 " + ex.Message);
                throw ex;
            }
        }

    }
}
