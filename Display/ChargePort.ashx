<%@ WebHandler Language="C#" Class="ChargePort" %>

using System;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using DisplayBLL;

public class ChargePort : IHttpHandler
{
    /// <summary>
    /// 判断端口状态
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            TcpBLL bll = new TcpBLL();
            context.Response.ContentType = "text/plain";
            //获取端口绑定的ip地址
            string ip = bll.GetIp();
            string portString = context.Request.Form["port"].ToString();
            var portArray = portString.Split(',');
            int[] resArray = new int[portArray.Length];

            ////使用异步方式检测端口状态
            //List<Task> taskList = new List<Task>();
            //TaskFactory taskFactory = new TaskFactory();
            //Action<int, int[], string[], string> action = new Action<int, int[], string[], string>(GetTestPortRes);
            //for (int i = 0; i < resArray.Length; i++)
            //{
            //    taskList.Add(taskFactory.StartNew(() => { action(i, resArray, portArray, ip); }));
            //}
            //Task.WaitAll(taskList.ToArray());

            for (int i = 0; i < resArray.Length; i++)
            {
                resArray[i] = TestPort(ip, int.Parse(portArray[i])) == true ? 1 : 0;
            }
            context.Response.Write(string.Join(",", resArray));
        }
        catch (Exception ex)
        {
            context.Response.Write("监测端口出错 因为" + ex.Message);
        }
    }

    public void GetTestPortRes(int i, int[] resArray, string[] portArray, string ip)
    {
        resArray[i] = TestPort(ip, int.Parse(portArray[i])) == true ? 1 : 0;
    }

    public bool TestPort(string ip, int port)
    {
        bool tcpListen = false;
        System.Net.IPAddress myIpAddress = IPAddress.Parse(ip);
        System.Net.IPEndPoint myIpEndPoint = new IPEndPoint(myIpAddress, port);
        try
        {
            System.Net.Sockets.TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(myIpEndPoint);//对远程计算机的指定端口提出TCP连接请求
            tcpListen = true;
        }
        catch { return false; }
        if (tcpListen == false)
            return false;
        else //端口已开启
            return true;
    }





    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}