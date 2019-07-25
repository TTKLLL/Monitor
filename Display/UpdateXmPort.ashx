<%@ WebHandler Language="C#" Class="UpdateXmPort" %>

using System;
using System.Web;
using DAL;

public class UpdateXmPort : IHttpHandler
{

    /// <summary>
    /// 更新项目端口设置
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string dataType = context.Request.Form["dataType"];
        int port = int.Parse(context.Request.Form["port"]);
        int xmno = int.Parse(context.Request.Form["xmno"]);

        string sql = string.Format(@"update xmport set dataType = '{0}' where port = {1} and xmno = {2}", dataType, port, xmno);
        bool res = SqlHelper.ExecuteNoQuery(sql);
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