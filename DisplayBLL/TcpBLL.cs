﻿using DAL;
using System;
using Tool;
using System.Threading;

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

        //开启监听
        public bool StartTcpListen(int port)
        {
            try
            {
                string ip = GetIp();
                return  TcpServer.StartListenPort(ip, port);
            }
            catch(Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("开启端口{0}出错 {1}", port.ToString(), ex.Message));
                throw ex;
            }
        }

    }
}
