using System;
using System.Data;
using System.Data.Odbc;
using Tool;


namespace DAL
{
    public class DbContext
    {

        // public OdbcConnection conn;

        //根据项目编号获取数据库信息
        public static LinkAttr GetLinkAttr(int xmno)
        {
            try
            {
                string sql = string.Format("select * from linkattr where xmno = {0}", xmno);
                DataTable dt = SqlHelper.GetTable(sql);
                DataRow row = dt.Rows[0];
                LinkAttr model = new LinkAttr()
                {
                    uid = row["uid"].ToString(),
                    pwd = row["pwd"].ToString(),
                    server = row["server"].ToString(),
                    port = int.Parse(row["port"].ToString()),
                    database = row["database"].ToString(),
                    driver = row["driver"].ToString()
                };
                return model;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("获取项目编号为{0}的数据库连接信息出错, {1}", xmno.ToString(), ex.Message));
                return null;
            }
        }

        public static OdbcConnection GetConn(int xmno = 0)
        {
            try
            {
                string driver = "{MySQL ODBC 5.3 ANSI Driver}";
                LinkAttr linkAttr = null;
                //访问通用数据库
                if (xmno == 0)
                {
                    // 本地数据库连接
                    linkAttr = new LinkAttr()
                    {
                        database = "lc",
                        uid = "root",
                        pwd = "123",
                        server = "localhost",
                        driver = driver,
                        port = 3307
                    };

                    //发布数据库连接处
                    //linkAttr = new LinkAttr()
                    //{
                    //    database = "lc",
                    //    uid = "root",
                    //    pwd = "root",
                    //    server = "localhost",
                    //    driver = driver,
                    //    port = 3306
                    //};
                }
                else
                {
                    linkAttr = GetLinkAttr(xmno);
                }
                string connString = string.Format("Driver={0};database={1};uid={2};password={3};server={4};port={5}", linkAttr.driver, linkAttr.database,
                    linkAttr.uid, linkAttr.pwd, linkAttr.server, linkAttr.port);
                return new OdbcConnection(connString);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("初始化数据库连接失败" + ex.Message);
                throw ex;
            }
        }
    }
}
