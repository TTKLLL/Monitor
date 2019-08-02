using DAL;
using System;
using System.Data;
using Tool;

namespace DisplayBLL
{
    public class TcpBLL
    {
        //获取要监听的ip地址
        public string GetIp()
        {
            try
            {
                string sql = "SELECT value FROM `baseinfo` where name = 'ip'";
                DataTable dt = SqlHelper.GetTable(sql);
                if (dt.Rows.Count == 0)
                    return "";
                else
                    return dt.Rows[0]["value"].ToString();
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取Ip地址出错" + ex.Message);
                throw ex;
            }
        }

        //开启监听
        public bool StartTcpListen(int port)
        {
            try
            {
                string ip = GetIp();
                return TcpServer.StartListenPort(ip, port);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("开启端口{0}出错 {1}", port.ToString(), ex.Message));
                throw ex;
            }
        }

        //更新ip地址
        public bool SetIp(string ip)
        {
            try
            {
                string querySql = "SELECT value FROM `baseinfo` where name = 'ip'";
                DataTable queryDt = SqlHelper.GetTable(querySql);
                string sql;
                if (queryDt.Rows.Count == 0)
                {
                    //插入
                    sql = string.Format("insert into baseinfo(name, value) VALUES('{0}', '{1}')", "ip", ip);
                }
                else
                {
                    //更新
                    sql = string.Format("update baseinfo set value = '{0}' where name = 'ip'", ip);
                }

                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("设置ip为 " + ip + " 出错");
                throw ex;
            }
        }

    }
}
