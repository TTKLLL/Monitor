<%@ WebHandler Language="C#" Class="GetLog" %>

using System;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using Tool;


public class GetLog : IHttpHandler
{

    /// <summary>
    /// 从文本读取日志信息
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            int lineNumber = 100;
            if (context.Request.QueryString["lineNumber"] != null)
                lineNumber = int.Parse(context.Request.QueryString["lineNumber"]);


            string res = FileOperation.ReadLastLine(lineNumber);
            context.Response.Write(res);
        }
        catch (Exception ex)
        {
            context.Response.Write("False");
            throw ex;
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