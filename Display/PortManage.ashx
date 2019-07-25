<%@ WebHandler Language="C#" Class="PortManage" %>

using System;
using System.Web;
using Tcp;
using System.Threading;

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
                Thread thread1 = new Thread(() => TcpServer.StartListening(port));
                thread1.Start();
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