<%@ WebHandler Language="C#" Class="GetAllPoint" %>

using System;
using System.Web;
using DisplayBLL;
using System.Web.Script.Serialization;
using Model;
using System.Collections.Generic;

public class GetAllPoint : IHttpHandler
{
    /// <summary>
    /// 获取所有测点信息
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "text/plain";
            PointInfoBLL bll = new PointInfoBLL();
            List<PointInfo> pointInfos = bll.GetPointInfos();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string res = js.Serialize(pointInfos);

            context.Response.Write(res);
        }
        catch(Exception ex)
        {
            context.Response.Write("False " + ex.Message);
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