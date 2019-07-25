using DAL;
using System;
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
                return SqlHelper.GetTable(sql).Rows[0]["value"].ToString();
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取Ip地址出错" + ex.Message);
                throw ex;
            }
        }
    }
}
