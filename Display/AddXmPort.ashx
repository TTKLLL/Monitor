<%@ WebHandler Language="C#" Class="AddXmPort" %>

using System;
using System.Web;
using DAL;
using Model;
using DisplayBLL;

public class AddXmPort : IHttpHandler
{
    /// <summary>
    /// 添加项目端口
    /// </summary>
    /// <param name="context"></param>
    /// 
    static XmPortBLL bll = new XmPortBLL();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        //  string dataType = context.Request.Form["dataType"];
        int port = int.Parse(context.Request.Form["port"]);
        //int  xmno = int.Parse(context.Request.Form["xmno"]);

        string dataType = "";
        int xmno = 0;


        XmPortModel model = new XmPortModel()
        {
            dataType = dataType,
            port = port,
            xmno = xmno
        };

        bool res = bll.AddXmPort(model);
        context.Response.Write(res);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}