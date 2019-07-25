using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

public partial class SettingXmPort : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetXmPort();
    }

    public  List<XmPortModel> xmPorts = new List<XmPortModel>();
    //获取数据类型与端口的对应关系
    public void GetXmPort()
    {
        xmPorts = new List<XmPortModel>();
        string sql = string.Format("select * from xmport order by port");
        DataTable dt = SqlHelper.GetTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            XmPortModel xmport = new XmPortModel()
            {
                port = int.Parse(dt.Rows[i]["port"].ToString()),
                xmno = int.Parse(dt.Rows[i]["xmno"].ToString()),
                dataType = dt.Rows[i]["dataType"].ToString()
            };
            xmPorts.Add(xmport);
        }
    }

}