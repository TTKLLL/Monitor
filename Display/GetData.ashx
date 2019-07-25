<%@ WebHandler Language="C#" Class="GetData" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using Model;
using System.Text;
using System.Data;
using DAL;
using System.Web.Script.Serialization;

public class GetData : IHttpHandler
{
    /// <summary>
    /// 获取传感器发送的数据
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        QueryModel para = new QueryModel()
        {
            //dataType = context.Request.Form["dataType"],
            //tdno = context.Request.Form["tdno"],
            //pointName = context.Request.Form["pointName"]

            dataType = context.Request.QueryString["dataType"],
            tdno = "",
            pointName = "",
            nowPage = int.Parse(context.Request.QueryString["nowPage"].ToString())
        };


        int totalCount;
        List<DataModel> models = GetModelByPara(para, out totalCount);

        //获取总记录数和总页数
        para.totlaNumber = totalCount;
        para.totalPage = totalCount % para.pageSize == 0 ? totalCount / para.pageSize : totalCount / para.pageSize + 1;

        ArrayList data = new ArrayList();
        data.Add(models);
        data.Add(para);

        JavaScriptSerializer js = new JavaScriptSerializer();
        string json = js.Serialize(data);


        context.Response.Write(json);
    }

    public List<DataModel> GetModelByPara(QueryModel para, out int totalNumber)
    {
        try
        {
            totalNumber = 0;
            string sqlHead = "select * from data ";
            string sqlGetCount = "select count(1) from data";


            StringBuilder sqlPara = new StringBuilder();
            sqlPara.Append(string.Format(" where tdno like '%{0}%' and  pointName like '%{1}%' ",
                para.tdno, para.pointName, para.dataType));

            if (para.dataType != "all")
            {
                sqlPara.Append(string.Format(" and dataType = '{0}' ", para.dataType));
            }

            totalNumber = 0;
            string sqlEnd = string.Format(" LIMIT {0}, {1}", (para.nowPage - 1) * para.pageSize, para.pageSize);

            totalNumber = int.Parse(SqlHelper.GetTable(sqlGetCount + sqlPara.ToString()).Rows[0]["count(1)"].ToString());

            DataTable dt = SqlHelper.GetTable(sqlHead + sqlPara.ToString() + sqlEnd);
            if (dt.Rows.Count <= 0)
                return null;



            //将DataTable转换成List
            List<DataModel> models = new List<DataModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataModel model = new DataModel()
                {
                    sno = dt.Rows[i]["sno"].ToString(),
                    cycle = dt.Rows[i]["cycle"].ToString(),
                    time = dt.Rows[i]["time"].ToString(),
                    tdno = dt.Rows[i]["tdno"].ToString(),
                    pointName = dt.Rows[i]["pointName"].ToString(),
                    dataType = dt.Rows[i]["dataType"].ToString(),
                    res = double.Parse(dt.Rows[i]["res"].ToString()),
                    port = dt.Rows[i]["port"].ToString()
                };
                models.Add(model);
            } 
            return models;
        }
            
        catch (Exception ex)
        {
            totalNumber = 0;
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