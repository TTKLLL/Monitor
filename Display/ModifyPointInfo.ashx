<%@ WebHandler Language="C#" Class="ModifyPointInfo" %>

using System;
using System.Web;
using DisplayBLL;
using Model;

public class ModifyPointInfo : IHttpHandler
{
    /// <summary>
    /// 添加 修改  删除 测点信息
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "text/plain";
            string pointName = context.Request.Form["pointName"];
            string tdno = context.Request.Form["tdno"];
            string t0 = context.Request.Form["t0"];
            string k0 = context.Request.Form["k0"];
            string type = context.Request.Form["type"];

            PointInfoBLL bll = new PointInfoBLL();
            PointInfo model = new PointInfo()
            {
                pointName = pointName,
                tdno = tdno,
                t0 = t0,
                k0 = k0
            };
            string res;
            switch (type)
            {
                //添加测点
                case "1": res = bll.AddPoint(model).ToString(); break;
                case "2": res = bll.UpdatePoint(model).ToString(); break;
                default: res = "操作类型错误"; break;
            }
            context.Response.Write(res);
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