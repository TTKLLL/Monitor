<%@ WebHandler Language="C#" Class="GetIp" %>

using System;
using System.Web;
using DisplayBLL;

public class GetIp : IHttpHandler
{
    /// <summary>
    /// 获取监听的ip地址
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "text/plain";
            TcpBLL bll = new TcpBLL();
            string ip = bll.GetIp();
            context.Response.Write(ip);
        }
        catch (Exception ex)
        {
            context.Response.Write("获取ip地址出错" + ex.Message);
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