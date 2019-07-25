<%@ WebHandler Language="C#" Class="PortManage" %>

using System;
using System.Web;
using Tcp;
using System.Threading;
using Tool;
using DisplayBLL;

/// <summary>
/// 打开和关闭端口
/// </summary>
public class PortManage : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            int port = int.Parse(context.Request.Form["port"]);
            int state = int.Parse(context.Request.Form["state"]);

            if (state == 1)
            {
                //string ip = new TcpBLL().GetIp();
                //Thread thread1 = new Thread(() => TcpServer.StartListening(ip, port));
                //thread1.Start();
                TcpBLL bll = new TcpBLL();
                bll.StartTcpListen(port);
                context.Response.Write("True");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}