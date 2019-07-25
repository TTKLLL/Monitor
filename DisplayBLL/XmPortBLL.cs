using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using Tool;

namespace DisplayBLL
{
    public class XmPortBLL
    {
        public bool AddXmPort(XmPortModel model)
        {
            try
            {
                string sql = string.Format("insert into xmport(port, xmno, dataType) values({0}, {1}, '{2}')", model.port, model.xmno, model.dataType);
                bool res = SqlHelper.ExecuteNoQuery(sql);
                return res;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("添加端口" + model.port.ToString() + "出错 " + ex.Message);
                throw ex;
            }
        }

        //获取所有端口
        public List<XmPortModel> GetPorts()
        {
            try
            {
                List<XmPortModel> xmPorts = new List<XmPortModel>();
                string sql = string.Format("select * from xmport order by port");
                DataTable dt = SqlHelper.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    XmPortModel xmport = new XmPortModel()
                    {
                        port = int.Parse(dt.Rows[i]["port"].ToString()),
                        xmno = int.Parse(dt.Rows[i]["xmno"].ToString()),
                        dataType = dt.Rows[i]["dataType"].ToString()
                    };
                    xmPorts.Add(xmport);
                }
                return xmPorts;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取端口数据出错 " + ex.Message);
                throw ex;
            }
        }

        //判断端口的开启状态
        public bool TestPort(string ip, int port)
        {
            bool tcpListen = false;
            System.Net.IPAddress myIpAddress = IPAddress.Parse(ip);
            System.Net.IPEndPoint myIpEndPoint = new IPEndPoint(myIpAddress, port);
            try
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(myIpEndPoint);//对远程计算机的指定端口提出TCP连接请求
                tcpListen = true;
            }
            catch { return false; }
            if (tcpListen == false)
                return false;
            else //端口已开启
                return true;
        }

        //判断端口号是否已存在
        public bool PortIsExist(int port)
        {
            try
            {
                string sql = string.Format("select port  from xmport where port = {0}", port);
                DataTable dt = SqlHelper.GetTable(sql);
                return dt.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("查询端口数据出错 " + ex.Message);
                throw ex;
            }
        }

        //更新端口对应的项目编号
        public bool ChangeXmno(XmPortModel model)
        {
            try
            {
                string sql = string.Format("update xmport set xmno = {0} where port = {1}", model.xmno, model.port);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("更新端口{0}对应的项目编号出错 {1}", model.port.ToString(), ex.Message));
                throw ex;
            }
        }

        //根据端口号获取数据类型和项目编号
        public int GetXmno(int port)
        {
            int xmno;
            string sql = string.Format("select xmno from xmport where port = {0}", port);

            DataTable dt = SqlHelper.GetTable(sql);
            try
            {
                xmno = int.Parse(dt.Rows[0]["xmno"].ToString());
                return xmno;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("获取端口{0}项目编号失败 {1}", port.ToString(), ex.Message));
                throw ex;
            }
        }

        //删除端口
        public bool  DeletePort(int port)
        {
            try
            {
                string sql = string.Format("delete from xmport where port = {0}", port);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch(Exception ex)
            {
                FileOperation.WriteAppenFile("删除端口号" + port.ToString() + "出错 " + ex.Message);
                throw ex;
            }
        }
    }
}
