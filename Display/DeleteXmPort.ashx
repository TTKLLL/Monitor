<%@ WebHandler Language="C#" Class="DeleteXmPort" %>

using System;
using System.Web;
using DAL;

public class DeleteXmPort : IHttpHandler
{

    /// <summary>
    /// 删除端口对应
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "text/plain";

            int port = int.Parse(context.Request.Form["port"]);
            string sql = string.Format("delete from xmport where port = {0}", port);
            bool res = SqlHelper.ExecuteNoQuery(sql);
            context.Response.Write(res);
        }
        catch
        {
            context.Response.Write("False");
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