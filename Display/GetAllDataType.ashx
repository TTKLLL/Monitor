<%@ WebHandler Language="C#" Class="GetAllDataType" %>

using System;
using System.Web;
using System.Data;
using DAL;
using System.Collections.Generic;
using Model;
using System.Web.Script.Serialization;

public class GetAllDataType : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        List<XmPortModel> models = GetAllDataTye();
        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = js.Serialize(models);
        context.Response.Write(json);

        //  context.Response.Write("Hello World");
    }

    public List<XmPortModel> GetAllDataTye()
    {
        try
        {
            //string sql = "select distinct datatype from xmport ORDER BY port";

             
            string sql = "SELECT distinct datatype  FROM `data` order by dataType";
            DataTable dt = SqlHelper.GetTable(sql);
            List<XmPortModel> models = new List<XmPortModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmPortModel model = new XmPortModel()
                {
                    dataType = dt.Rows[i]["datatype"].ToString()

                };
                models.Add(model);
            }
            return models;
        }
        catch (Exception ex)
        {
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